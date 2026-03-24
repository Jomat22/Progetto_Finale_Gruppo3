using store.api.src.Dto.Client;
using store.api.src.Dto.Employee;
using store.api.src.Dto.Person;
using store.api.src.Dto.Product;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Text.Json;
using store.client.Observer;
using store.client.Singleton;

namespace store.client;

class Program
{
    const string BASE_URL = "http://localhost:5102/";
    static readonly HttpClient _http = new HttpClient { BaseAddress = new Uri(BASE_URL) };

    static readonly OrderPublisher _orderPublisher = new();

    static readonly JsonSerializerOptions _jsonOpt = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };

    // =========================================================
    // ENTRY POINT
    // =========================================================
    static void Main()
    {

        AppLogger.Instance.LogInfo("Terminale Negozio avviato.");
        

        _orderPublisher.Subscribe(new LoggerObserver());
        _orderPublisher.Subscribe(new MagazziniereObserver("Sistema Centrale"));
        _orderPublisher.Subscribe(new PagamentiObserver());

        bool run = true;
        while (run)
        {
            Console.Clear();
            PrintHeader("TERMINALE NEGOZIO");
            Console.WriteLine(" [1]  Admin");
            Console.WriteLine(" [2]  Magazziniere");
            Console.WriteLine(" [0]  Esci");
            PrintSeparator();

            switch (ReadKey("Seleziona ruolo: ", '0', '2'))
            {
                case '1': MenuAdmin(); break;
                case '2': MenuMagazziniere(); break;
                case '0': run = false; break;
            }
        }
        AppLogger.Instance.LogInfo("Chiusura applicazione.");
        Console.WriteLine("\nArrivederci!");
    }

    // =========================================================
    // MENU ADMIN
    // =========================================================
    static void MenuAdmin()
    {
        bool run = true;
        while (run)
        {
            Console.Clear();
            PrintHeader("ADMIN");
            Console.WriteLine(" [1]  Gestione Anagrafica Persone");
            Console.WriteLine(" [2]  Gestione Dipendenti");
            Console.WriteLine(" [3]  Gestione Clienti");
            Console.WriteLine(" [4]  Report Ordini del Giorno"); 
            Console.WriteLine(" [0]  Torna indietro");
            PrintSeparator();

            switch (ReadKey("Seleziona: ", '0', '4'))
            {
                case '1': MenuPersone(); break;
                case '2': MenuDipendenti(); break;
                case '3': MenuClienti(); break;
                case '4': VisualizzaOrdiniDelGiorno(); Pausa(); break; 
                case '0': run = false; break;
            }
        }
    }

    // ---- PERSONE ----
    static void MenuPersone()
    {
        bool run = true;
        while (run)
        {
            Console.Clear();
            PrintHeader("ANAGRAFICA PERSONE");
            Console.WriteLine(" [1]  Aggiungi persona");
            Console.WriteLine(" [2]  Modifica persona");
            Console.WriteLine(" [3]  Elimina persona");
            Console.WriteLine(" [4]  Cerca persona (CF)");
            Console.WriteLine(" [5]  Lista tutte le persone");
            Console.WriteLine(" [0]  Torna indietro");
            PrintSeparator();

            switch (ReadKey("Seleziona: ", '0', '5'))
            {
                case '1': AggiungiPersona();   Pausa(); break;
                case '2': ModificaPersona();   Pausa(); break;
                case '3': EliminaPersona();    Pausa(); break;
                case '4': CercaPersona();      Pausa(); break;
                case '5': ListaPersone();      Pausa(); break;
                case '0': run = false; break;
            }
        }
    }

    static void AggiungiPersona()
    {
        Console.Clear();
        PrintHeader("AGGIUNGI PERSONA");

        PersonCreateRequest req = new();
        req.CodiceFiscale   = LeggiStringa("Codice Fiscale (16 car.): ").ToUpper();
        req.Nome            = LeggiStringa("Nome: ");
        req.Cognome         = LeggiStringa("Cognome: ");
        req.Sesso           = LeggiRegex("Sesso (M/F): ", @"^[MFmf]$").ToUpper();
        req.DataNascita     = LeggiData("Data di nascita (yyyy-MM-dd): ");
        req.Citta           = LeggiStringa("Citta: ");
        req.Provincia       = LeggiStringa("Provincia (max 5 car.): ");
        req.CodicePostale   = LeggiStringa("CAP: ");
        req.Indirizzo       = LeggiStringa("Indirizzo: ");
        req.NumeroContatto  = LeggiStringa("Numero di contatto: ");
        req.Email           = LeggiStringa("Email: ");

        if (!Valida(req)) return;

        var ok = PostAsync<PersonCreateRequest>("api/Person", req).GetAwaiter().GetResult();
        Feedback(ok, "Persona aggiunta con successo.", "Aggiunta persona fallita.");
    }

    static void ModificaPersona()
    {
        Console.Clear();
        PrintHeader("MODIFICA PERSONA");

        PersonUpdateRequest req = new();
        req.CodiceFiscale   = LeggiStringa("Codice Fiscale persona da modificare (16 car.): ").ToUpper();
        req.Nome            = LeggiStringa("Nuovo Nome: ");
        req.Cognome         = LeggiStringa("Nuovo Cognome: ");
        req.Sesso           = LeggiRegex("Sesso (M/F): ", @"^[MFmf]$").ToUpper();
        req.DataNascita     = LeggiData("Data di nascita (yyyy-MM-dd): ");
        req.Citta           = LeggiStringa("Citta: ");
        req.Provincia       = LeggiStringa("Provincia (max 5 car.): ");
        req.CodicePostale   = LeggiStringa("CAP: ");
        req.Indirizzo       = LeggiStringa("Indirizzo: ");
        req.NumeroContatto  = LeggiStringa("Numero di contatto: ");
        req.Email           = LeggiStringa("Email: ");

        if (!Valida(req)) return;

        var ok = PutAsync<PersonUpdateRequest>("api/Person", req).GetAwaiter().GetResult();
        Feedback(ok, "Persona modificata con successo.", "Modifica persona fallita.");
    }

    static void EliminaPersona()
    {
        Console.Clear();
        PrintHeader("ELIMINA PERSONA");

        string cf = LeggiStringa("Codice Fiscale persona da eliminare: ").ToUpper();
        Console.Write($"\nConfermi eliminazione di '{cf}'? (S/N): ");
        if (Console.ReadKey().Key != ConsoleKey.S) { Console.WriteLine("\nOperazione annullata."); return; }

        var ok = DeleteAsync($"api/Person/{cf}").GetAwaiter().GetResult();
        Feedback(ok, "Persona eliminata con successo.", "Eliminazione persona fallita.");
    }

    static void CercaPersona()
    {
        Console.Clear();
        PrintHeader("CERCA PERSONA");
        string cf = LeggiStringa("Codice Fiscale: ").ToUpper();
        var result = GetAsync($"api/Person/{cf}").GetAwaiter().GetResult();
        Console.WriteLine(result ?? "Nessun risultato.");
    }

    static void ListaPersone()
    {
        Console.Clear();
        PrintHeader("LISTA PERSONE");
        var result = GetAsync("api/Person").GetAwaiter().GetResult();
        StampaLista(result);
    }

    // ---- DIPENDENTI ----
    static void MenuDipendenti()
    {
        bool run = true;
        while (run)
        {
            Console.Clear();
            PrintHeader("ANAGRAFICA DIPENDENTI");
            Console.WriteLine(" [1]  Aggiungi dipendente");
            Console.WriteLine(" [2]  Modifica dipendente");
            Console.WriteLine(" [3]  Elimina dipendente");
            Console.WriteLine(" [4]  Cerca dipendente (Cod. Meccanografico)");
            Console.WriteLine(" [5]  Lista tutti i dipendenti");
            Console.WriteLine(" [0]  Torna indietro");
            PrintSeparator();

            switch (ReadKey("Seleziona: ", '0', '5'))
            {
                case '1': AggiungiDipendente();  Pausa(); break;
                case '2': ModificaDipendente();  Pausa(); break;
                case '3': EliminaDipendente();   Pausa(); break;
                case '4': CercaDipendente();     Pausa(); break;
                case '5': ListaDipendenti();     Pausa(); break;
                case '0': run = false; break;
            }
        }
    }

    static void AggiungiDipendente()
    {
        Console.Clear();
        PrintHeader("AGGIUNGI DIPENDENTE");
        Console.WriteLine("N.B.: La persona deve essere gia presente in anagrafica.\n");

        EmployeeCreateRequest req = new();
        req.PersonId             = LeggiIntero("ID Persona associata: ");
        req.CodiceMeccanografico = LeggiStringa("Codice Meccanografico: ").ToUpper();
        req.EmailAziendale       = LeggiStringa("Email aziendale: ");
        req.Password             = LeggiStringa("Password (8-24 car.): ");
        req.Ruolo                = LeggiStringa("Ruolo (es. Admin, Magazziniere, Cassiere): ");
        req.Salario              = LeggiDecimale("Salario (euro): ");

        if (!Valida(req)) return;

        var ok = PostAsync<EmployeeCreateRequest>("api/Employee", req).GetAwaiter().GetResult();
        Feedback(ok, "Dipendente aggiunto con successo.", "Aggiunta dipendente fallita.");
    }

    static void ModificaDipendente()
    {
        Console.Clear();
        PrintHeader("MODIFICA DIPENDENTE");

        EmployeeUpdateRequest req = new();
        req.CodiceMeccanografico = LeggiStringa("Codice Meccanografico dipendente da modificare: ").ToUpper();
        req.PersonId             = LeggiIntero("Nuovo ID Persona associata: ");
        req.EmailAziendale       = LeggiStringa("Nuova Email aziendale: ");
        req.Password             = LeggiStringa("Nuova Password (8-24 car.): ");
        req.Ruolo                = LeggiStringa("Nuovo Ruolo: ");
        req.Salario              = LeggiDecimale("Nuovo Salario (euro): ");

        if (!Valida(req)) return;

        var ok = PutAsync<EmployeeUpdateRequest>("api/Employee", req).GetAwaiter().GetResult();
        Feedback(ok, "Dipendente modificato con successo.", "Modifica dipendente fallita.");
    }

    static void EliminaDipendente()
    {
        Console.Clear();
        PrintHeader("ELIMINA DIPENDENTE");

        string codice = LeggiStringa("Codice Meccanografico dipendente da eliminare: ").ToUpper();
        Console.Write($"\nConfermi eliminazione di '{codice}'? (S/N): ");
        if (Console.ReadKey().Key != ConsoleKey.S) { Console.WriteLine("\nOperazione annullata."); return; }

        var ok = DeleteAsync($"api/Employee/{codice}").GetAwaiter().GetResult();
        Feedback(ok, "Dipendente eliminato con successo.", "Eliminazione dipendente fallita.");
    }

    static void CercaDipendente()
    {
        Console.Clear();
        PrintHeader("CERCA DIPENDENTE");
        string codice = LeggiStringa("Codice Meccanografico: ").ToUpper();
        var result = GetAsync($"api/Employee/{codice}").GetAwaiter().GetResult();
        Console.WriteLine(result ?? "Nessun risultato.");
    }

    static void ListaDipendenti()
    {
        Console.Clear();
        PrintHeader("LISTA DIPENDENTI");
        var result = GetAsync("api/Employee").GetAwaiter().GetResult();
        StampaLista(result);
    }

    // ---- CLIENTI ----
    static void MenuClienti()
    {
        bool run = true;
        while (run)
        {
            Console.Clear();
            PrintHeader("ANAGRAFICA CLIENTI");
            Console.WriteLine(" [1]  Aggiungi cliente");
            Console.WriteLine(" [2]  Modifica cliente");
            Console.WriteLine(" [3]  Elimina cliente");
            Console.WriteLine(" [4]  Cerca cliente (Cod. Cliente)");
            Console.WriteLine(" [5]  Lista tutti i clienti");
            Console.WriteLine(" [6]  Visualizza Storico Ordini Cliente"); 
            Console.WriteLine(" [0]  Torna indietro");
            PrintSeparator();

            switch (ReadKey("Seleziona: ", '0', '6')) 
            {
                case '1': AggiungiCliente();  Pausa(); break;
                case '2': ModificaCliente();  Pausa(); break;
                case '3': EliminaCliente();   Pausa(); break;
                case '4': CercaCliente();     Pausa(); break;
                case '5': ListaClienti();     Pausa(); break;
                case '6': VisualizzaStoricoOrdiniCliente(); Pausa(); break; 
                case '0': run = false; break;
            }
        }
    }

    static void AggiungiCliente()
    {
        Console.Clear();
        PrintHeader("AGGIUNGI CLIENTE");
        
        ClientCreateRequest req = new();
        req.PersonId             = LeggiIntero("ID Persona associata: ");
        req.CodiceCliente        = LeggiStringa("Codice Cliente (3-20 car.): ").ToUpper();
        req.IsFidelizzato        = LeggiBoolean("Cliente fidelizzato? (S/N): ");
        req.IsIscrittoNewsletter = LeggiBoolean("Iscritto alla newsletter? (S/N): ");

        if (!Valida(req)) return;

        var ok = PostAsync<ClientCreateRequest>("api/Client", req).GetAwaiter().GetResult();
        Feedback(ok, "Cliente aggiunto con successo.", "Aggiunta cliente fallita.");
    }

    static void ModificaCliente()
    {
        Console.Clear();
        PrintHeader("MODIFICA CLIENTE");

        ClientUpdateRequest req = new();
        req.CodiceCliente        = LeggiStringa("Codice Cliente da modificare: ").ToUpper();
        req.PersonId             = LeggiIntero("Nuovo ID Persona associata: ");
        req.IsFidelizzato        = LeggiBoolean("Cliente fidelizzato? (S/N): ");
        req.IsIscrittoNewsletter = LeggiBoolean("Iscritto alla newsletter? (S/N): ");

        if (!Valida(req)) return;

        var ok = PutAsync<ClientUpdateRequest>("api/Client", req).GetAwaiter().GetResult();
        Feedback(ok, "Cliente modificato con successo.", "Modifica cliente fallita.");
    }

    static void EliminaCliente()
    {
        Console.Clear();
        PrintHeader("ELIMINA CLIENTE");

        string codice = LeggiStringa("Codice Cliente da eliminare: ").ToUpper();
        Console.Write($"\nConfermi eliminazione di '{codice}'? (S/N): ");
        if (Console.ReadKey().Key != ConsoleKey.S) { Console.WriteLine("\nOperazione annullata."); return; }

        var ok = DeleteAsync($"api/Client/{codice}").GetAwaiter().GetResult();
        Feedback(ok, "Cliente eliminato con successo.", "Eliminazione cliente fallita.");
    }

    static void CercaCliente()
    {
        Console.Clear();
        PrintHeader("CERCA CLIENTE");
        string codice = LeggiStringa("Codice Cliente: ").ToUpper();
        var result = GetAsync($"api/Client/{codice}").GetAwaiter().GetResult();
        Console.WriteLine(result ?? "Nessun risultato.");
    }

    static void ListaClienti()
    {
        Console.Clear();
        PrintHeader("LISTA CLIENTI");
        var result = GetAsync("api/Client").GetAwaiter().GetResult();
        StampaLista(result);
    }

    // =========================================================
    // MENU MAGAZZINIERE
    // =========================================================
    static void MenuMagazziniere()
    {
        bool run = true;
        while (run)
        {
            Console.Clear();
            PrintHeader("MAGAZZINIERE");
            Console.WriteLine(" [1]  Gestione Prodotti (CRUD)");
            Console.WriteLine(" [2]  Visualizza Anagrafiche");
            Console.WriteLine(" [3]  Visualizza ORDINI DEL GIORNO"); 
            Console.WriteLine(" [4]  Visualizza STORICO ORDINI Cliente"); 
            Console.WriteLine(" [0]  Torna indietro");
            PrintSeparator();

            switch (ReadKey("Seleziona: ", '0', '4')) 
            {
                case '1': MenuProdotti(); break;
                case '2': MenuVisualizzaAnagrafiche(); break;
                case '3': VisualizzaOrdiniDelGiorno(); Pausa(); break;
                case '4': VisualizzaStoricoOrdiniCliente(); Pausa(); break;
                case '0': run = false; break;
            }
        }
    }

    // ---- PRODOTTI ----
    static void MenuProdotti()
    {
        bool run = true;
        while (run)
        {
            Console.Clear();
            PrintHeader("GESTIONE PRODOTTI");
            Console.WriteLine(" [1]  Aggiungi prodotto");
            Console.WriteLine(" [2]  Modifica prodotto");
            Console.WriteLine(" [3]  Elimina prodotto");
            Console.WriteLine(" [4]  Cerca prodotto (SKU)");
            Console.WriteLine(" [5]  Lista tutti i prodotti");
            Console.WriteLine(" [0]  Torna indietro");
            PrintSeparator();

            switch (ReadKey("Seleziona: ", '0', '5'))
            {
                case '1': AggiungiProdotto();  Pausa(); break;
                case '2': ModificaProdotto();  Pausa(); break;
                case '3': EliminaProdotto();   Pausa(); break;
                case '4': CercaProdotto();     Pausa(); break;
                case '5': ListaProdotti();     Pausa(); break;
                case '0': run = false; break;
            }
        }
    }

    static void AggiungiProdotto()
    {
        Console.Clear();
        PrintHeader("AGGIUNGI PRODOTTO");

        ProductCreateRequest req = new();
        req.Sku      = LeggiStringa("SKU (3-20 car.): ").ToUpper();
        req.Nome     = LeggiStringa("Nome prodotto: ");
        req.Prezzo   = LeggiDecimale("Prezzo (euro): ");
        req.Quantita = LeggiIntero("Quantita in magazzino: ");

        if (!Valida(req)) return;

        var ok = PostAsync<ProductCreateRequest>("api/Product", req).GetAwaiter().GetResult();
        Feedback(ok, "Prodotto aggiunto con successo.", "Aggiunta prodotto fallita.");
    }

    static void ModificaProdotto()
    {
        Console.Clear();
        PrintHeader("MODIFICA PRODOTTO");

        ProductUpdateRequest req = new();
        req.Sku      = LeggiStringa("SKU prodotto da modificare: ").ToUpper();
        req.Nome     = LeggiStringa("Nuovo Nome: ");
        req.Prezzo   = LeggiDecimale("Nuovo Prezzo (euro): ");
        req.Quantita = LeggiIntero("Nuova Quantita: ");

        if (!Valida(req)) return;

        var ok = PutAsync<ProductUpdateRequest>("api/Product", req).GetAwaiter().GetResult();
        Feedback(ok, "Prodotto modificato con successo.", "Modifica prodotto fallita.");
    }

    static void EliminaProdotto()
    {
        Console.Clear();
        PrintHeader("ELIMINA PRODOTTO");

        string sku = LeggiStringa("SKU prodotto da eliminare: ").ToUpper();
        Console.Write($"\nConfermi eliminazione di '{sku}'? (S/N): ");
        if (Console.ReadKey().Key != ConsoleKey.S) { Console.WriteLine("\nOperazione annullata."); return; }

        var ok = DeleteAsync($"api/Product/{sku}").GetAwaiter().GetResult();
        Feedback(ok, "Prodotto eliminato con successo.", "Eliminazione prodotto fallita.");
    }

    static void CercaProdotto()
    {
        Console.Clear();
        PrintHeader("CERCA PRODOTTO");
        string sku = LeggiStringa("SKU: ").ToUpper();
        var result = GetAsync($"api/Product/{sku}").GetAwaiter().GetResult();
        Console.WriteLine(result ?? "Nessun risultato.");
    }

    static void ListaProdotti()
    {
        Console.Clear();
        PrintHeader("LISTA PRODOTTI");
        var result = GetAsync("api/Product").GetAwaiter().GetResult();
        StampaLista(result);
    }

    static void MenuVisualizzaAnagrafiche()
    {
        bool run = true;
        while (run)
        {
            Console.Clear();
            PrintHeader("VISUALIZZA ANAGRAFICHE");
            Console.WriteLine(" [1]  Lista Persone");
            Console.WriteLine(" [2]  Lista Dipendenti");
            Console.WriteLine(" [3]  Lista Clienti");
            Console.WriteLine(" [4]  Cerca Persona (CF)");
            Console.WriteLine(" [5]  Cerca Dipendente (Cod. Meccanografico)");
            Console.WriteLine(" [6]  Cerca Cliente (Cod. Cliente)");
            Console.WriteLine(" [0]  Torna indietro");
            PrintSeparator();

            switch (ReadKey("Seleziona: ", '0', '6'))
            {
                case '1': ListaPersone();    Pausa(); break;
                case '2': ListaDipendenti(); Pausa(); break;
                case '3': ListaClienti();    Pausa(); break;
                case '4': CercaPersona();    Pausa(); break;
                case '5': CercaDipendente(); Pausa(); break;
                case '6': CercaCliente();    Pausa(); break;
                case '0': run = false; break;
            }
        }
    }

    // =========================================================
    // METODI REPORT 
    // =========================================================
    static void VisualizzaStoricoOrdiniCliente()
    {
        Console.Clear();
        PrintHeader("STORICO ORDINI CLIENTE");
        string codiceCliente = LeggiStringa("Inserisci Codice Cliente: ").ToUpper();
        var result = GetAsync($"api/Order/history/{codiceCliente}").GetAwaiter().GetResult();
        
        if (result != null)
        {
            Console.WriteLine(result);
            var evento = new OrderCreatedEvent(
                "Riepilogo Storico", 
                0.00m, 
                "In sola lettura", 
                DateTime.Now
            );
            _orderPublisher.NotifyOrderCreated(evento);
        }
        else
        {
            AppLogger.Instance.LogWarning($"Nessun ordine trovato per {codiceCliente}.");
        }
    }

    static void VisualizzaOrdiniDelGiorno()
    {
        Console.Clear();
        PrintHeader("ORDINI DEL GIORNO E TOTALE");
        var result = GetAsync("api/Order/daily-report").GetAwaiter().GetResult();
        
        if (result != null)
        {
            Console.WriteLine(result);
        }
        else
        {
            AppLogger.Instance.LogWarning("Nessun ordine registrato oggi.");
        }
    }

    // =========================================================
    // HTTP HELPERS 
    // =========================================================
    static async Task<bool> PostAsync<T>(string endpoint, T payload)
    {
        try
        {
            var response = await _http.PostAsJsonAsync(endpoint, payload);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex) { AppLogger.Instance.LogError($"Errore POST: {ex.Message}"); return false; }
    }

    static async Task<bool> PutAsync<T>(string endpoint, T payload)
    {
        try
        {
            var response = await _http.PutAsJsonAsync(endpoint, payload);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex) { AppLogger.Instance.LogError($"Errore PUT: {ex.Message}"); return false; }
    }

    static async Task<bool> DeleteAsync(string endpoint)
    {
        try
        {
            var response = await _http.DeleteAsync(endpoint);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex) { AppLogger.Instance.LogError($"Errore DELETE: {ex.Message}"); return false; }
    }

    static async Task<string?> GetAsync(string endpoint)
    {
        try
        {
            var response = await _http.GetAsync(endpoint);
            string body = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) return null;
            try
            {
                var doc = JsonDocument.Parse(body);
                return JsonSerializer.Serialize(doc, new JsonSerializerOptions { WriteIndented = true });
            }
            catch { return body; }
        }
        catch (Exception ex) { AppLogger.Instance.LogError($"Errore GET: {ex.Message}"); return null; }
    }

    // =========================================================
    // INPUT & UI HELPERS
    // =========================================================
    static string LeggiStringa(string prompt)
    {
        string? val;
        do { Console.Write(prompt); val = Console.ReadLine()?.Trim(); } while (string.IsNullOrWhiteSpace(val));
        return val;
    }

    static string LeggiRegex(string prompt, string pattern)
    {
        string val;
        do { val = LeggiStringa(prompt); } while (!System.Text.RegularExpressions.Regex.IsMatch(val, pattern));
        return val;
    }

    static int LeggiIntero(string prompt)
    {
        int val;
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out val) && val >= 0) return val;
        }
    }

    static decimal LeggiDecimale(string prompt)
    {
        decimal val;
        while (true)
        {
            Console.Write(prompt);
            if (decimal.TryParse(Console.ReadLine()?.Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out val)) return val;
        }
    }

    static DateOnly LeggiData(string prompt)
    {
        DateOnly val;
        while (true) { Console.Write(prompt); if (DateOnly.TryParse(Console.ReadLine(), out val)) return val; }
    }

    static bool LeggiBoolean(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            var k = Console.ReadKey(true).Key;
            Console.WriteLine(k == ConsoleKey.S ? "Si" : "No");
            if (k == ConsoleKey.S) return true; if (k == ConsoleKey.N) return false;
        }
    }

    static char ReadKey(string prompt, char min, char max)
    {
        while (true)
        {
            Console.Write(prompt);
            var k = Console.ReadKey().KeyChar;
            Console.WriteLine();
            if (k >= min && k <= max) return k;
        }
    }

    static bool Valida<T>(T obj)
    {
        var results = new List<ValidationResult>();
        var ctx = new ValidationContext(obj!);
        bool ok = Validator.TryValidateObject(obj!, ctx, results, true);
        if (!ok) foreach (var r in results) AppLogger.Instance.LogWarning(r.ErrorMessage!);
        return ok;
    }

    static void Feedback(bool ok, string msgOk, string msgKo)
    {
        if (ok) AppLogger.Instance.LogSuccess(msgOk);
        else AppLogger.Instance.LogError(msgKo);
    }

    static void StampaLista(string? json) => Console.WriteLine(json ?? "Nessun dato.");

    static void PrintHeader(string titolo)
    {
        string line = new string('=', 42);
        Console.WriteLine($"{line}\n  {titolo}\n{line}");
    }

    static void PrintSeparator() => Console.WriteLine(new string('-', 42));
    static void Pausa() { Console.WriteLine("\nPremi un tasto..."); Console.ReadKey(true); }
}