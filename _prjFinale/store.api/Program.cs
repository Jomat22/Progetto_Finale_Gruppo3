using Microsoft.EntityFrameworkCore;
using store.api.src.Data;
using store.core.src.Interface;
using store.core.src.Strategy.Payment;
using store.api.src.Infrastructure.Service;
using store.api.src.Infrastructure.Repo;
using store.api.src.Facade;

namespace store.api;

public class Program {
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        // Recupero la stringa di connessione.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
        
        /* 
            * ATTENZIONE (per chi scarica il progetto): 
            * Anche con 'AutoDetect', l'errore non apparirà all'avvio dell'app.
            * L'applicazione sembrerà avviarsi correttamente ("Swagger" si aprirà).
            * Il "muro di testo" di errori si scatenerà solo alla prima request (es. da "Postman"),
            * perché è in quel momento che il sistema di "Dependency Injection" proverà a risolvere 
            * il 'DataContext' e fallirà la connessione reale, rendendo il debug più caotico.
        */
        /* builder.Services.AddDbContext<DataContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
        );  */

        /* 
            * Ho preferito usare la versione manuale, cosi facendo evito che il framework provi a 
            * fare 'auto-discovery' in modo disordinato durante la risoluzione delle dipendenze, 
            * permettendoci di gestire l'errore in modo pulito nel try-catch del Controller.
            * In questo caso non avverrà perché in fondo dopo il builder, troverete un controllo 
            * che ho inserito che effettua il check health all'avvio dell'app .
        */
        // Recupero la versione dal file di configurazione (appsettings.json)
        // Se la chiave "DatabaseVersion" non esiste, usa "8.0.40" come fallback.
        var versionString = builder.Configuration["DatabaseVersion"] ?? "8.0.40";
        var serverVersion = new MySqlServerVersion(new Version(versionString));

        // Configurato per supportare il retry (seppur inutile) e versione dinamica db.
        builder.Services.AddDbContext<DataContext>(options =>
            options.UseMySql(connectionString, serverVersion, mysqlOptions => 
            {
                mysqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
            })
        );

        builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        }).AddJsonOptions(options =>
        {
            /* 
            * Previene l'errore "A possible object cycle was detected" (JsonException).
            * Questo accade quando le entità si riferiscono l'una all'altra (es. Persona -> Dipendente -> Persona).
            * Con 'IgnoreCycles', il serializzatore smette di seguire le proprietà di navigazione se incontra un ciclo,
            * evitando loop infiniti e il crash del sistema durante la generazione del JSON.
            */
            options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;


            // Settando a 'null', il serializzatore di non applicherà alcuna trasformazione ai nomi delle proprietà durante la conversione in JSON.
            // Il JSON manterrà esattamente la stessa formattazione usata nelle classi C# (PascalCase).
            options.JsonSerializerOptions.PropertyNamingPolicy = null;

            // Rendo il JSON prodotto più leggibile per il debugging su Scalar/Postman.
            options.JsonSerializerOptions.WriteIndented = true;
        });

        builder.Services.AddOpenApi();
        // Invece di passargli l'assembly direttamente, gli diciamo di scansionare.
        // tutti i profili di mappatura presenti nel progetto.
        builder.Services.AddAutoMapper(cfg => {
            cfg.AddMaps(typeof(Program).Assembly);
        });

        builder.Services.AddScoped<AuthService>();
        builder.Services.AddScoped<AuthRepository>();
        builder.Services.AddScoped<PersonService>();
        builder.Services.AddScoped<PersonRepository>();
        builder.Services.AddScoped<EmployeeService>();
        builder.Services.AddScoped<EmployeeRepository>();
        builder.Services.AddScoped<ClientService>();
        builder.Services.AddScoped<ReceiptService>();
        builder.Services.AddScoped<ReceiptRepository>();
        builder.Services.AddScoped<ClientRepository>();
        builder.Services.AddScoped<ProductService>();
        builder.Services.AddScoped<ProductRepository>();
        builder.Services.AddScoped<IPaymentContext, PaymentContext>();
        builder.Services.AddScoped<IPaymentStrategy, BitcoinPaymentStrategy>();
        builder.Services.AddScoped<IPaymentStrategy, CreditCardPaymentStrategy>();
        builder.Services.AddScoped<IPaymentStrategy, LiquidPaymentStrategy>();
        builder.Services.AddScoped<IPaymentStrategy, PaypalPaymentStrategy>();
        builder.Services.AddScoped<IStoreFacade, StoreFacade>();
        
        var app = builder.Build();

        // Ecco il controllo, cosi facendo capirete immediatamente se avete la stringa db configurata male.
        using (var scope = app.Services.CreateScope())
        {
            try
            {
                // Provo a risolvere il DataContext e ad aprire una connessione.
                var context = scope.ServiceProvider.GetRequiredService<DataContext>();
                
                // Questo comando forza "EF Core" a stabilire una connessione reale.
                context.Database.OpenConnection();
                context.Database.CloseConnection();
                
                Console.WriteLine("Database connesso correttamente.");
            } catch (Exception ex)
            {
                // Se arriva qui, la stringa di connessione è quasi certamente sbagliata.
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n[ERRORE]: Connessione fallita (controllate se la stringa di connessione è corretta).");
                Console.WriteLine($"Messaggio: {ex.Message}");
                Console.ResetColor();

                // Blocco l'esecuzione immediatamente (non avrebbe senso continuare).
                Environment.Exit(1);
            }
        }

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        // app.UseHttpsRedirection(); // - Non mi serve il redirect.
        app.UseRouting();
        app.MapControllers();
        app.Run();
    } 
}
