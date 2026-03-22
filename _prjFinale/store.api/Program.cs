using Microsoft.EntityFrameworkCore;
using AutoMapper;
using store.api.src.Data;
using store.core.src.Interface;
using store.core.src.Strategy.Payment;
using store.api.src.Infrastructure.Service;
using store.api.src.Infrastructure.Repo;

namespace store.api;

public class Program {
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
        builder.Services.AddDbContext<DataContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
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

            // Rendo il JSON prodotto più leggibile per il debugging su Scalar/Postman
            options.JsonSerializerOptions.WriteIndented = true;
        });

        builder.Services.AddOpenApi();
        // Invece di passargli l'assembly direttamente, gli diciamo di scansionare 
        // tutti i profili di mappatura presenti nel progetto.
        builder.Services.AddAutoMapper(cfg => {
            cfg.AddMaps(typeof(Program).Assembly);
        });

        builder.Services.AddScoped<PersonService>();
        builder.Services.AddScoped<PersonRepository>();
        builder.Services.AddScoped<IPaymentContext, PaymentContext>();
        builder.Services.AddScoped<IPaymentStrategy, BitcoinPaymentStrategy>();
        builder.Services.AddScoped<IPaymentStrategy, CreditCardPaymentStrategy>();
        builder.Services.AddScoped<IPaymentStrategy, LiquidPaymentStrategy>();
        builder.Services.AddScoped<IPaymentStrategy, PaypalPaymentStrategy>();
        
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();
        app.MapControllers();
        app.Run();
    } 
}
