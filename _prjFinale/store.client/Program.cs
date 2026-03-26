using store.api.src.Dto.Client;
using store.api.src.Dto.Employee;
using store.api.src.Dto.Person;
using store.api.src.Dto.Product;
using store.api.src.Dto.Receipt;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Text.Json;
using store.client.Observer;
using store.client.Singleton;
using store.core.src.Const;
using System.Reflection.Metadata.Ecma335;
using store.core.src.Domain.Entity.Sales;
using Microsoft.AspNetCore.Mvc.TagHelpers;

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
    // STATO SESSIONE
    // =========================================================
    static string? _sessioneEmail    = null;
    static string? _sessioneRuolo    = null;
    static string? _sessioneNome     = null;

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
                case '1':
                    if (Login("Admin"))
                    {
                        MenuAdmin();
                        Logout();
                    }
                    break;
                case '2':
                    if (Login("Magazziniere"))
                    {
                        MenuMagazziniere();
                        Logout();
                    }
                    break;
                case '0': run = false; break;
            }
        }
        AppLogger.Instance.LogInfo("Chiusura applicazione.");
        Console.WriteLine("\nArrivederci!");
    }

    // =========================================================
    // LOGIN / LOGOUT
    // =========================================================


    static bool Login(string ruoloRichiesto)
    {
        Console.Clear();
        PrintHeader($"LOGIN — {ruoloRichiesto.ToUpper()}");

        string email    = LeggiStringa("Email aziendale: ");
        string password = LeggiStringa("Password: ");

        var payload = new { EmailAziendale = email, Password = password };

        try
        {
            var response = _http.PostAsJsonAsync("api/Auth", payload).GetAwaiter().GetResult();

            if (!response.IsSuccessStatusCode)
            {
                AppLogger.Instance.LogError("Credenziali non valide. Accesso negato.");
                Pausa();
                return false;
            }

            string body = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var doc = JsonDocument.Parse(body);
            var data = doc.RootElement.GetProperty("Data");

            _sessioneEmail = data.GetProperty("EmailAziendale").GetString();
            _sessioneRuolo = data.GetProperty("Ruolo").GetString();

            //Controllo sul ruolo dell'utente che sta accedendo
            if (!string.Equals(_sessioneRuolo, ruoloRichiesto, StringComparison.OrdinalIgnoreCase) && ruoloRichiesto =="Admin")
            {
                AppLogger.Instance.LogError($"Accesso negato: il tuo ruolo ({_sessioneRuolo}) non autorizza l'accesso a questa sezione ({ruoloRichiesto}).");
            
                // Reset delle variabili di sessione per sicurezza
                _sessioneEmail = null;
                _sessioneRuolo = null;
            
            Pausa();
            return false;
            }

            if (data.TryGetProperty("Person", out var person) &&
                person.ValueKind != JsonValueKind.Null)
            {
                string nome    = person.GetProperty("Nome").GetString() ?? "";
                string cognome = person.GetProperty("Cognome").GetString() ?? "";
                _sessioneNome  = $"{nome} {cognome}".Trim();
            }
            else
            {
                _sessioneNome = _sessioneEmail;
            }

            AppLogger.Instance.LogSuccess($"Accesso effettuato come {_sessioneNome} [{_sessioneRuolo}].");
            return true;
        }
        catch (Exception ex)
        {
            AppLogger.Instance.LogError($"Errore durante il login: {ex.Message}");
            Pausa();
            return false;
        }
    }

    static void Logout()
    {
        AppLogger.Instance.LogInfo($"Logout effettuato per {_sessioneNome} [{_sessioneRuolo}].");
        _sessioneEmail = null;
        _sessioneRuolo = null;
        _sessioneNome  = null;
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
            Console.WriteLine(" [4]  Gestione Scontrini");
            Console.WriteLine(" [5]  Report Ordini del Giorno");
            Console.WriteLine(" [0]  Torna indietro (Logout)");
            PrintSeparator();

            switch (ReadKey("Seleziona: ", '0', '5'))
            {
                case '1': MenuPersone(); break;
                case '2': MenuDipendenti(); break;
                case '3': MenuClienti(); break;
                case '4': MenuScontrini(); break;
                case '5': VisualizzaOrdiniDelGiorno(); Pausa(); break;
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
        PrintHeader("MODIFICA SELETTIVA");

        string codiceFiscale = LeggiStringa("Codice Fiscale della persona da modificare: ").ToUpper();
    
        PersonUpdateRequest req = null!;

        try 
        {
            //Recupero la risposta dall'API
            var json = GetAsync($"api/Person/{codiceFiscale}").GetAwaiter().GetResult();
            
            if (string.IsNullOrWhiteSpace(json)) {
                AppLogger.Instance.LogError("Persona non trovata.");
                Pausa();
                return;
            }

            //Navigo nel JSON per trovare il nodo "Data"
            using (JsonDocument doc = JsonDocument.Parse(json))
            {
                // Verifico se esiste la proprietà "Data" (visto che il tuo JSON la usa come contenitore)
                if (doc.RootElement.TryGetProperty("Data", out JsonElement dataElement))
                {
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    req = JsonSerializer.Deserialize<PersonUpdateRequest>(dataElement.GetRawText(), options)!;
                }
                else 
                {
                    // Se non c'è "Data", provo a deserializzare la radice
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    req = JsonSerializer.Deserialize<PersonUpdateRequest>(json, options)!;
                }
            }
        }
        catch (Exception ex)
        {
            AppLogger.Instance.LogError($"Errore nel caricamento: {ex.Message}");
            Pausa();
            return;
        }

        if (req == null) return;

        //Logica di Selezione
        bool modNome = false, modCognome = false, modSesso = false, modDataNascita = false, modCitta = false, modProvincia = false, modCap = false, modIndirizzo = false, modNumeroContatto = false, modEmail = false;
        bool selezioneInCorso = true;

        while (selezioneInCorso)
        {
            Console.Clear();
            PrintHeader($"MODIFICA PERSONA: {codiceFiscale}");

            Console.WriteLine($" [1] --- {(modNome ? "[V]" : "[X]")} Nome                   (Attuale: {req.Nome})");
            Console.WriteLine($" [2] --- {(modCognome ? "[V]" : "[X]")} Cognome                (Attuale: {req.Cognome})");
            Console.WriteLine($" [3] --- {(modSesso ? "[V]" : "[X]")} Sesso                  (Attuale: {req.Sesso})");
            Console.WriteLine($" [4] --- {(modDataNascita ? "[V]" : "[X]")} Data di Nascita        (Attuale: {req.DataNascita:yyyy-MM-dd})");
            Console.WriteLine($" [5] --- {(modCitta ? "[V]" : "[X]")} Città                  (Attuale: {req.Citta})");
            Console.WriteLine($" [6] --- {(modProvincia ? "[V]" : "[X]")} Provincia              (Attuale: {req.Provincia})");
            Console.WriteLine($" [7] --- {(modCap ? "[V]" : "[X]")} CAP                    (Attuale: {req.CodicePostale})");
            Console.WriteLine($" [8] --- {(modIndirizzo ? "[V]" : "[X]")} Indirizzo              (Attuale: {req.Indirizzo})");
            Console.WriteLine($" [9] --- {(modNumeroContatto ? "[V]" : "[X]")} Numero Contatto        (Attuale: {req.NumeroContatto})");
            Console.WriteLine($" [0] --- {(modEmail ? "[V]" : "[X]")} Email                  (Attuale: {req.Email})");
            PrintSeparator();
            Console.WriteLine(" [c] Continua e inserisci i nuovi dati");
            Console.WriteLine(" [b] Annulla ed esci");
            PrintSeparator();

            char scelta = ReadKey("Seleziona opzione: ", '0', 'c');

            switch (scelta)
            {
                case '1': modNome = !modNome; break;
                case '2': modCognome = !modCognome; break;
                case '3': modSesso = !modSesso; break;
                case '4': modDataNascita = !modDataNascita; break;
                case '5': modCitta = !modCitta; break;
                case '6': modProvincia = !modProvincia; break;
                case '7': modCap = !modCap; break;
                case '8': modIndirizzo = !modIndirizzo; break;
                case '9': modNumeroContatto = !modNumeroContatto; break;
                case '0': modEmail = !modEmail; break;
                case 'b': return;
                case 'c': selezioneInCorso = false; break;
            }
        }

        // Fase di Inserimento
        // Se non entri nell'IF, req mantiene il valore scaricato dal DB
        Console.WriteLine("\n--- Inserimento nuovi dati ---");
        if (modNome)     req.Nome = LeggiStringa("Nuovo Nome: ");
        if (modCognome)  req.Cognome = LeggiStringa("Nuovo Cognome: ");
        if (modSesso)    req.Sesso = LeggiRegex("Nuovo sesso (M/F): ", @"^[MFmf]$").ToUpper();
        if (modDataNascita) req.DataNascita = LeggiData("Nuova data di nascita (yyyy-MM-dd): ");
        if (modCitta)    req.Citta = LeggiStringa("Nuova Città: ");
        if (modProvincia) req.Provincia = LeggiStringa("Nuova Provincia: ");
        if (modCap)      req.CodicePostale = LeggiStringa("Nuovo CAP: ");
        if (modIndirizzo) req.Indirizzo = LeggiStringa("Nuovo Indirizzo: ");
        if (modNumeroContatto) req.NumeroContatto = LeggiStringa("Nuovo Numero Contatto: ");
        if (modEmail)    req.Email = LeggiStringa("Nuova Email: ");

        // Salvataggio
        Console.Write("\nSalvare le modifiche nel database? (S/N): ");
        if (Console.ReadKey(true).Key == ConsoleKey.S)
        {
            //Validazione inserimento secondo i vincoli della classe PersonUpdateRequest (Data Annotations)
            var context = new ValidationContext(req, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(req, context, results, true);

            if (isValid)
            {
                var ok = PutAsync("api/Person", req).GetAwaiter().GetResult();
                Feedback(ok, "Dati aggiornati con successo!", "Errore durante il salvataggio.");
            }
            else
            {
                AppLogger.Instance.LogError("I dati inseriti non sono validi:");
                foreach (var validationResult in results)
                {
                    Console.WriteLine($"- {validationResult.ErrorMessage}");
                }
            }
        }
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
        PrintHeader("MODIFICA SELETTIVA");

        string codMecc = LeggiStringa("Codice Meccanografico del dipendente da modificare: ").ToUpper();
    
        EmployeeUpdateRequest req = null!;

        try 
        {
            //Recupero la risposta dall'API
            var json = GetAsync($"api/Employee/{codMecc}").GetAwaiter().GetResult();
            
            if (string.IsNullOrWhiteSpace(json)) {
                AppLogger.Instance.LogError("Dipendente non trovato.");
                Pausa();
                return;
            }

            //Navigo nel JSON per trovare il nodo "Data"
            using (JsonDocument doc = JsonDocument.Parse(json))
            {
                // Verifico se esiste la proprietà "Data" (visto che il tuo JSON la usa come contenitore)
                if (doc.RootElement.TryGetProperty("Data", out JsonElement dataElement))
                {
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    req = JsonSerializer.Deserialize<EmployeeUpdateRequest>(dataElement.GetRawText(), options)!;
                }
                else 
                {
                    // Se non c'è "Data", provo a deserializzare la radice
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    req = JsonSerializer.Deserialize<EmployeeUpdateRequest>(json, options)!;
                }
            }
        }
        catch (Exception ex)
        {
            AppLogger.Instance.LogError($"Errore nel caricamento: {ex.Message}");
            Pausa();
            return;
        }

        if (req == null) return;

        //Logica di Selezione
        bool modIdPers = false, modEmail = false, modPassword = false, modRuolo = false, modSalario = false;
        bool selezioneInCorso = true;

        while (selezioneInCorso)
        {
            Console.Clear();
            PrintHeader($"MODIFICA DIPENDENTE: {codMecc}");

            Console.WriteLine($" [1] --- {(modIdPers ? "[V]" : "[X]")} Id pers. associata     (Attuale: {req.PersonId})");
            Console.WriteLine($" [2] --- {(modEmail ? "[V]" : "[X]")} Email                  (Attuale: {req.EmailAziendale})");
            Console.WriteLine($" [3] --- {(modPassword ? "[V]" : "[X]")} Password               (Attuale: {req.Password})");
            Console.WriteLine($" [4] --- {(modRuolo ? "[V]" : "[X]")} Ruolo                  (Attuale: {req.Ruolo})");
            Console.WriteLine($" [5] --- {(modSalario ? "[V]" : "[X]")} Salario                (Attuale: {req.Salario})");
            PrintSeparator();
            Console.WriteLine(" [c] Continua e inserisci i nuovi dati");
            Console.WriteLine(" [b] Annulla ed esci");
            PrintSeparator();

            char scelta = ReadKey("Seleziona opzione: ", '0', 'c');

            switch (scelta)
            {
                case '1': modIdPers = !modIdPers; break;
                case '2': modEmail = !modEmail; break;
                case '3': modPassword = !modPassword; break;
                case '4': modRuolo = !modRuolo; break;
                case '5': modSalario = !modSalario; break;
                case 'b': return;
                case 'c': selezioneInCorso = false; break;
            }
        }

        // Fase di Inserimento
        // Se non entri nell'IF, req mantiene il valore scaricato dal DB
        Console.WriteLine("\n--- Inserimento nuovi dati ---");
        if (modIdPers)   req.PersonId = LeggiIntero("Nuovo Codice Meccanografico: ");
        if (modEmail)    req.EmailAziendale = LeggiStringa("Nuova Email: ");
        if (modPassword) req.Password = LeggiStringa("Nuova Password: ");
        if (modRuolo)    req.Ruolo = LeggiStringa("Nuovo Ruolo: ");
        if (modSalario)  req.Salario = LeggiDecimale("Nuovo Salario: ");

        // Salvataggio
        Console.Write("\nSalvare le modifiche nel database? (S/N): ");
        if (Console.ReadKey(true).Key == ConsoleKey.S)
        {
            //Validazione inserimento secondo i vincoli della classe EmployeeUpdateRequest (Data Annotations)
            var context = new ValidationContext(req, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(req, context, results, true);

            if (isValid)
            {
                var ok = PutAsync("api/Employee", req).GetAwaiter().GetResult();
                Feedback(ok, "Dati aggiornati con successo!", "Errore durante il salvataggio.");
            }
            else
            {
                AppLogger.Instance.LogError("I dati inseriti non sono validi:");
                foreach (var validationResult in results)
                {
                    Console.WriteLine($"- {validationResult.ErrorMessage}");
                }
                Pausa();
            }
        }
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
        PrintHeader("MODIFICA SELETTIVA");

        string codiceCliente = LeggiStringa("Codice cliente da modificare: ").ToUpper();
    
        ClientUpdateRequest req = null!;

        try 
        {
            //Recupero la risposta dall'API
            var json = GetAsync($"api/Client/{codiceCliente}").GetAwaiter().GetResult();
            
            if (string.IsNullOrWhiteSpace(json)) {
                AppLogger.Instance.LogError("Cliente non trovato.");
                Pausa();
                return;
            }

            //Navigo nel JSON per trovare il nodo "Data"
            using (JsonDocument doc = JsonDocument.Parse(json))
            {
                // Verifico se esiste la proprietà "Data" (visto che il tuo JSON la usa come contenitore)
                if (doc.RootElement.TryGetProperty("Data", out JsonElement dataElement))
                {
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    req = JsonSerializer.Deserialize<ClientUpdateRequest>(dataElement.GetRawText(), options)!;
                }
                else 
                {
                    // Se non c'è "Data", provo a deserializzare la radice
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    req = JsonSerializer.Deserialize<ClientUpdateRequest>(json, options)!;
                }
            }
        }
        catch (Exception ex)
        {
            AppLogger.Instance.LogError($"Errore nel caricamento: {ex.Message}");
            Pausa();
            return;
        }

        if (req == null) return;

        //Logica di Selezione
        bool modIdPersAssoc = false;
        bool selezioneInCorso = true;

        while (selezioneInCorso)
        {
            Console.Clear();
            PrintHeader($"MODIFICA CLIENTE: {codiceCliente}");

            Console.WriteLine($" [1] --- {(modIdPersAssoc ? "[V]" : "[X]")} ID Persona Associata     (Attuale: {req.PersonId})");
            Console.WriteLine($" [2] --- Fidelizzato   (Attuale: {req.IsFidelizzato})");
            Console.WriteLine($" [3] --- Iscritto alla Newsletter (Attuale: {req.IsIscrittoNewsletter})");
            PrintSeparator();
            Console.WriteLine(" [c] Continua e inserisci i nuovi dati");
            Console.WriteLine(" [b] Annulla ed esci");
            PrintSeparator();

            char scelta = ReadKey("Seleziona opzione: ", '0', 'c');

            switch (scelta)
            {
                case '1': modIdPersAssoc = !modIdPersAssoc; break;
                case '2': req.IsFidelizzato = !req.IsFidelizzato; break;
                case '3': req.IsIscrittoNewsletter = !req.IsIscrittoNewsletter; break;
                case 'b': return;
                case 'c': selezioneInCorso = false; break;
            }
        }

        // Fase di Inserimento
        // Se non entri nell'IF, req mantiene il valore scaricato dal DB
        Console.WriteLine("\n--- Inserimento nuovi dati ---");
        if (modIdPersAssoc) req.PersonId = LeggiIntero("Nuovo ID Persona Associata: ");

        // Salvataggio
        Console.Write("\nSalvare le modifiche nel database? (S/N): ");
        if (Console.ReadKey(true).Key == ConsoleKey.S)
        {
            //Validazione inserimento secondo i vincoli della classe ClientUpdateRequest (Data Annotations)
            var context = new ValidationContext(req, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(req, context, results, true);

            if (isValid)
            {
                var ok = PutAsync("api/Client", req).GetAwaiter().GetResult();
                Feedback(ok, "Dati aggiornati con successo!", "Errore durante il salvataggio.");
            }
            else
            {
                AppLogger.Instance.LogError("I dati inseriti non sono validi:");
                foreach (var validationResult in results)
                {
                    Console.WriteLine($"- {validationResult.ErrorMessage}");
                }
                Pausa();
            }
        }
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

    // ---- SCONTRINI ----

    static void MenuScontrini()
    {
        bool run = true;
        while (run)
        {
            Console.Clear();
            PrintHeader("GESTIONE SCONTRINI");
            Console.WriteLine(" [1]  Aggiungi scontrino");
            Console.WriteLine(" [2]  Elimina scontrino");
            Console.WriteLine(" [3]  Cerca scontrino (Id Scontrino)");
            Console.WriteLine(" [4]  Visualizza storico scontrini cliente");
            Console.WriteLine(" [5]  Lista tutti i scontrini");
            Console.WriteLine(" [0]  Torna indietro");
            PrintSeparator();

            switch (ReadKey("Seleziona: ", '0', '6'))
            {
                case '1': AggiungiScontrino();  Pausa(); break;
                case '2': EliminaScontrino();   Pausa(); break;
                case '3': CercaScontrino();     Pausa(); break;
                case '4': VisualizzaStoricoOrdiniCliente(); Pausa(); break;
                case '5': ListaScontrini(); Pausa(); break;
                case '0': run = false; break;
            }
        }
    }

    static void AggiungiScontrino()
    {
        Console.Clear();
        PrintHeader("AGGIUNGI SCONTRINO");

        ReceiptCreateRequest req = new()
        {
            ClientId = LeggiIntero("ID Cliente associato: "),
            MetodoPagamento = LeggiStringa("Metodo di pagamento: ").ToUpper(),
            Prodotti = new List<ReceiptDetailRequest>()
        };

        while (true)
        {
            int prodottoId = LeggiIntero("ID Prodotto acquistato (0 per terminare): ");
            if (prodottoId == 0) break;

            int quantita = LeggiIntero("Quantità: ");

            bool giftWrap = ReadKey("Gift wrap? (S/N): ", 'N', 'S') == 'S';
            bool express = ReadKey("Express delivery? (S/N): ", 'N', 'S') == 'S';
            bool assicurazione = ReadKey("Assicurazione? (S/N): ", 'N', 'S') == 'S';

            req.Prodotti.Add(new ReceiptDetailRequest
            {
                ProdottoId = prodottoId,
                Quantita = quantita,
                GiftWrap = giftWrap,
                Express = express,
                Assicurazione = assicurazione
            });
        }

        if (req.Prodotti.Count == 0)
        {
            AppLogger.Instance.LogWarning("Devi aggiungere almeno un prodotto.");
            return;
        }

        if (!Valida(req)) return;

        var ok = PostAsync<ReceiptCreateRequest>("api/Receipt", req).GetAwaiter().GetResult();
        Feedback(ok, "Scontrino aggiunto con successo.", "Aggiunta scontrino fallita.");
    }

    static void EliminaScontrino()
    {
        Console.Clear();
        PrintHeader("ELIMINA SCONTRINO");

        int id = LeggiIntero("ID Scontrino da eliminare: ");
        Console.Write($"\nConfermi eliminazione dello scontrino '{id}'? (S/N): ");
        if (Console.ReadKey().Key != ConsoleKey.S) { Console.WriteLine("\nOperazione annullata."); return; }

        var ok = DeleteAsync($"api/Receipt/{id}").GetAwaiter().GetResult();
        Feedback(ok, "Scontrino eliminato con successo.", "Eliminazione scontrino fallita.");
    }

    static void CercaScontrino()
    {
        Console.Clear();
        PrintHeader("CERCA SCONTRINO");

        int id = LeggiIntero("ID Scontrino: ");
        var result = GetAsync($"api/Receipt/{id}").GetAwaiter().GetResult();

        if (result != null)
        {
            Console.WriteLine(result);
        }
        else
        {
            AppLogger.Instance.LogWarning($"Scontrino con ID {id} non trovato.");
        }
    }

    static void ListaScontrini()
    {
        Console.Clear();
        PrintHeader("LISTA SCONTRINI");
        var result = GetAsync("api/Receipt").GetAwaiter().GetResult();
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
            Console.WriteLine(" [5]  Crea ORDINE per Cliente");
            Console.WriteLine(" [0]  Torna indietro (Logout)");
            PrintSeparator();

            switch (ReadKey("Seleziona: ", '0', '5'))
            {
                case '1': MenuProdotti(); break;
                case '2': MenuVisualizzaAnagrafiche(); break;
                case '3': VisualizzaOrdiniDelGiorno(); Pausa(); break;
                case '4': VisualizzaStoricoOrdiniCliente(); Pausa(); break;
                case '5': CreaOrdineCliente(); Pausa(); break;
                case '0': run = false; break;
            }
        }
    }

    // =========================================================
    // CREAZIONE ORDINE CLIENTE (Receipt)
    // =========================================================
    static void CreaOrdineCliente()
    {
        Console.Clear();
        PrintHeader("CREA ORDINE CLIENTE");

        // 1. Ricerca cliente tramite codice cliente
        string codiceCliente = LeggiStringa("Codice Cliente: ").ToUpper();
        int clientId;

        try
        {
            var clientResponse = _http.GetAsync($"api/Client/{codiceCliente}").GetAwaiter().GetResult();
            if (!clientResponse.IsSuccessStatusCode)
            {
                AppLogger.Instance.LogError($"Cliente '{codiceCliente}' non trovato.");
                return;
            }
            string clientBody = clientResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var clientDoc = JsonDocument.Parse(clientBody);
            var clientData = clientDoc.RootElement.GetProperty("Data");
            clientId = clientData.GetProperty("Id").GetInt32();
            string clienteNome = "";
            if (clientData.TryGetProperty("Person", out var personEl) && personEl.ValueKind != JsonValueKind.Null)
                clienteNome = $"{personEl.GetProperty("Nome").GetString()} {personEl.GetProperty("Cognome").GetString()}".Trim();
            AppLogger.Instance.LogSuccess($"Cliente trovato: {clienteNome} (ID: {clientId})");
        }
        catch (Exception ex)
        {
            AppLogger.Instance.LogError($"Errore nel recupero del cliente: {ex.Message}");
            return;
        }

        // 2. Mostra prodotti disponibili
        Console.WriteLine();
        AppLogger.Instance.LogInfo("Prodotti disponibili in magazzino:");
        var prodotti = GetAsync("api/Product").GetAwaiter().GetResult();
        if (prodotti != null)
            Console.WriteLine(prodotti);
        else
            AppLogger.Instance.LogWarning("Impossibile recuperare la lista prodotti.");

        // 3. Raccolta prodotti dell'ordine
        var righeOrdine = new List<ReceiptDetailRequest>();
        decimal totaleOrdine = 0m;

        bool aggiungiProdotto = true;
        while (aggiungiProdotto)
        {
            Console.WriteLine();
            PrintHeader($"AGGIUNGI PRODOTTO ALL'ORDINE (righe: {righeOrdine.Count})");

            // Inserimento per SKU (il ProductController accetta solo SKU come parametro GET)
            string skuProdotto = LeggiStringa("SKU Prodotto: ").ToUpper();
            int    quantita    = LeggiIntero("Quantità: ");

            // Recupero Id reale e prezzo tramite SKU
            decimal prezzoBase   = 0m;
            string  nomeProdotto = skuProdotto;
            int     prodottoId   = 0;
            try
            {
                var pResp = _http.GetAsync($"api/Product/{skuProdotto}").GetAwaiter().GetResult();
                if (pResp.IsSuccessStatusCode)
                {
                    string pBody = pResp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    var pDoc     = JsonDocument.Parse(pBody);
                    var pData    = pDoc.RootElement.GetProperty("Data");
                    prodottoId   = pData.GetProperty("Id").GetInt32();
                    prezzoBase   = pData.GetProperty("Prezzo").GetDecimal();
                    nomeProdotto = pData.GetProperty("Nome").GetString() ?? nomeProdotto;
                    AppLogger.Instance.LogSuccess($"Prodotto trovato: {nomeProdotto} (ID: {prodottoId}) — Prezzo base: {prezzoBase:C}");
                }
                else
                {
                    AppLogger.Instance.LogWarning($"Prodotto con SKU '{skuProdotto}' non trovato. Riga saltata.");
                    continue;
                }
            }
            catch (Exception ex)
            {
                AppLogger.Instance.LogError($"Errore nel recupero del prodotto: {ex.Message}");
                continue;
            }

            // 4. Decorator
            (bool giftWrap, bool express, bool assicurazione) = ScegliDecorator();

            store.core.src.Interface.IProduct prodDec = new store.core.src.Domain.Entity.Catalog.Product
            {
                Nome   = nomeProdotto,
                Prezzo = prezzoBase
            };
            if (giftWrap)      prodDec = new store.api.src.Decorator.GiftWrapDecorator(prodDec);
            if (express)       prodDec = new store.api.src.Decorator.ExpressDeliveryDecorator(prodDec);
            if (assicurazione) prodDec = new store.api.src.Decorator.InsuranceDecorator(prodDec);

            decimal prezzoUnitarioFinale = prodDec.GetPrezzo();
            string  descrizioneRiga      = prodDec.Descrizione();
            decimal totalRiga            = prezzoUnitarioFinale * quantita;
            totaleOrdine                += totalRiga;

            Console.WriteLine();
            Console.WriteLine($"  Prodotto    : {descrizioneRiga}");
            Console.WriteLine($"  Prezzo unit.: {prezzoUnitarioFinale:C}  x  {quantita} = {totalRiga:C}");
            PrintSeparator();

            righeOrdine.Add(new ReceiptDetailRequest
            {
                ProdottoId    = prodottoId,
                Quantita      = quantita,
                GiftWrap      = giftWrap,
                Express       = express,
                Assicurazione = assicurazione
            });

            AppLogger.Instance.LogSuccess($"Riga aggiunta. Totale provvisorio ordine: {totaleOrdine:C}");

            aggiungiProdotto = LeggiBoolean("\nAggiungere un altro prodotto? (S/N): ");
        }

        if (righeOrdine.Count == 0)
        {
            AppLogger.Instance.LogWarning("Nessun prodotto aggiunto. Ordine annullato.");
            return;
        }

        // 5. Metodo di pagamento (Strategy)
        string metodoPagamento = ScegliMetodoPagamento();

        // 6. Riepilogo
        Console.WriteLine();
        PrintHeader("RIEPILOGO ORDINE");
        Console.WriteLine($"  Cliente     : {codiceCliente} (ID: {clientId})");
        Console.WriteLine($"  N. prodotti : {righeOrdine.Count}");
        Console.WriteLine($"  Totale est. : {totaleOrdine:C}");
        Console.WriteLine($"  Pagamento   : {metodoPagamento}");
        PrintSeparator();

        string esitoPagamento = EseguiPagamentoLocale(metodoPagamento, totaleOrdine);
        Console.WriteLine($"  Esito pag.  : {esitoPagamento}");
        PrintSeparator();

        bool conferma = LeggiBoolean("Confermare e inviare l'ordine? (S/N): ");
        if (!conferma)
        {
            AppLogger.Instance.LogWarning("Ordine annullato dall'operatore.");
            return;
        }

        // 7. POST api/Receipt
        var receiptRequest = new ReceiptCreateRequest
        {
            ClientId        = clientId,
            MetodoPagamento = metodoPagamento,
            Prodotti        = righeOrdine
        };

        var ok = PostAsync<ReceiptCreateRequest>("api/Receipt", receiptRequest).GetAwaiter().GetResult();
        Feedback(ok, "Ordine creato con successo! Sarà visibile nei report.", "Creazione ordine fallita.");

        // 8. Observer
        if (ok)
        {
            _orderPublisher.NotifyOrderCreated(new OrderCreatedEvent(
                NomeProdotto:    $"Ordine cliente {codiceCliente} ({righeOrdine.Count} prodotti)",
                PrezzoFinale:    totaleOrdine,
                MetodoPagamento: metodoPagamento,
                Timestamp:       DateTime.Now
            ));
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

        // ---- DECORATOR ----
        (bool giftWrap, bool express, bool assicurazione) = ScegliDecorator();

        store.core.src.Interface.IProduct prodDec = new store.core.src.Domain.Entity.Catalog.Product
        {
            Nome    = req.Nome,
            Prezzo  = req.Prezzo
        };
        if (giftWrap)     prodDec = new store.api.src.Decorator.GiftWrapDecorator(prodDec);
        if (express)      prodDec = new store.api.src.Decorator.ExpressDeliveryDecorator(prodDec);
        if (assicurazione) prodDec = new store.api.src.Decorator.InsuranceDecorator(prodDec);

        decimal prezzoFinale = prodDec.GetPrezzo();
        string  descrizione  = prodDec.Descrizione();

        string metodoPagamento = ScegliMetodoPagamento();
        string esitoPagamento  = EseguiPagamentoLocale(metodoPagamento, prezzoFinale);

        Console.WriteLine();
        PrintHeader("RIEPILOGO");
        Console.WriteLine($"  Prodotto    : {descrizione}");
        Console.WriteLine($"  Prezzo base : {req.Prezzo:C}");
        Console.WriteLine($"  Prezzo finale: {prezzoFinale:C}");
        Console.WriteLine($"  Pagamento   : {esitoPagamento}");
        PrintSeparator();

        var ok = PostAsync<ProductCreateRequest>("api/Product", req).GetAwaiter().GetResult();
        Feedback(ok, "Prodotto aggiunto con successo.", "Aggiunta prodotto fallita.");

        // ---- OBSERVER ----
        if (ok)
        {
            _orderPublisher.NotifyOrderCreated(new OrderCreatedEvent(
                NomeProdotto:    descrizione,
                PrezzoFinale:    prezzoFinale,
                MetodoPagamento: metodoPagamento,
                Timestamp:       DateTime.Now
            ));
        }
    }


    static (bool giftWrap, bool express, bool assicurazione) ScegliDecorator()
    {
        Console.WriteLine();
        PrintHeader("OPZIONI AGGIUNTIVE");
        Console.WriteLine("  Puoi aggiungere uno o più servizi extra al prodotto:");
        Console.WriteLine($"  [1]  Confezione regalo   (+2,00€)");
        Console.WriteLine($"  [2]  Consegna express    (+5,00€)");
        Console.WriteLine($"  [3]  Assicurazione       (+3,00€)");
        Console.WriteLine("  (premi S per aggiungere, N per saltare)");
        PrintSeparator();

        bool giftWrap     = LeggiBoolean("  Confezione regalo? (S/N): ");
        bool express      = LeggiBoolean("  Consegna express?  (S/N): ");
        bool assicurazione = LeggiBoolean("  Assicurazione?     (S/N): ");

        return (giftWrap, express, assicurazione);
    }

    static string ScegliMetodoPagamento()
    {
        Console.WriteLine();
        Console.WriteLine("  Seleziona metodo di pagamento:");
        Console.WriteLine("  [1]  Carta di credito");
        Console.WriteLine("  [2]  Contanti");
        Console.WriteLine("  [3]  PayPal");
        Console.WriteLine("  [4]  Bitcoin");
        PrintSeparator();

        while (true)
        {
            switch (ReadKey("  Metodo: ", '1', '4'))
            {
                case '1': return PaymentProvider.CreditCard;
                case '2': return PaymentProvider.Liquid;
                case '3': return PaymentProvider.Paypal;
                case '4': return PaymentProvider.Bitcoin;
            }
        }
    }


    static string EseguiPagamentoLocale(string provider, decimal importo)
    {
        return provider switch
        {
            PaymentProvider.CreditCard => new store.core.src.Strategy.Payment.CreditCardPaymentStrategy().ExecutePayment(importo),
            PaymentProvider.Liquid     => new store.core.src.Strategy.Payment.LiquidPaymentStrategy().ExecutePayment(importo),
            PaymentProvider.Paypal     => new store.core.src.Strategy.Payment.PaypalPaymentStrategy().ExecutePayment(importo),
            PaymentProvider.Bitcoin    => new store.core.src.Strategy.Payment.BitcoinPaymentStrategy().ExecutePayment(importo),
            _                          => $"Metodo '{provider}' non supportato."
        };
    }

    static void ModificaProdotto()
    {
        Console.Clear();
        PrintHeader("MODIFICA SELETTIVA");

        string sku = LeggiStringa("SKU del prodotto da modificare: ").ToUpper();
    
        ProductUpdateRequest req = null!;

        try 
        {
            //Recupero la risposta dall'API
            var json = GetAsync($"api/Product/{sku}").GetAwaiter().GetResult();
            
            if (string.IsNullOrWhiteSpace(json)) {
                AppLogger.Instance.LogError("Prodotto non trovato.");
                Pausa();
                return;
            }

            //Navigo nel JSON per trovare il nodo "Data"
            using (JsonDocument doc = JsonDocument.Parse(json))
            {
                // Verifico se esiste la proprietà "Data" (visto che il tuo JSON la usa come contenitore)
                if (doc.RootElement.TryGetProperty("Data", out JsonElement dataElement))
                {
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    req = JsonSerializer.Deserialize<ProductUpdateRequest>(dataElement.GetRawText(), options)!;
                }
                else 
                {
                    // Se non c'è "Data", provo a deserializzare la radice
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    req = JsonSerializer.Deserialize<ProductUpdateRequest>(json, options)!;
                }
            }
        }
        catch (Exception ex)
        {
            AppLogger.Instance.LogError($"Errore nel caricamento: {ex.Message}");
            Pausa();
            return;
        }

        if (req == null) return;

        //Logica di Selezione
        bool modNome = false, modPrezzo = false, modQuantita = false;
        bool selezioneInCorso = true;

        while (selezioneInCorso)
        {
            Console.Clear();
            PrintHeader($"MODIFICA PRODOTTO: {sku}");

            Console.WriteLine($" [1] --- {(modNome ? "[V]" : "[X]")} Nome     (Attuale: {req.Nome})");
            Console.WriteLine($" [2] --- {(modPrezzo ? "[V]" : "[X]")} Prezzo   (Attuale: {req.Prezzo})");
            Console.WriteLine($" [3] --- {(modQuantita ? "[V]" : "[X]")} Quantità (Attuale: {req.Quantita})");
            PrintSeparator();
            Console.WriteLine(" [c] Continua e inserisci i nuovi dati");
            Console.WriteLine(" [b] Annulla ed esci");
            PrintSeparator();

            char scelta = ReadKey("Seleziona opzione: ", '0', 'c');

            switch (scelta)
            {
                case '1': modNome = !modNome; break;
                case '2': modPrezzo = !modPrezzo; break;
                case '3': modQuantita = !modQuantita; break;
                case 'b': return;
                case 'c': selezioneInCorso = false; break;
            }
        }

        // Fase di Inserimento
        // Se non entri nell'IF, req mantiene il valore scaricato dal DB
        Console.WriteLine("\n--- Inserimento nuovi dati ---");
        if (modNome)     req.Nome = LeggiStringa("Nuovo Nome: ");
        if (modPrezzo)   req.Prezzo = LeggiDecimale("Nuovo Prezzo: ");
        if (modQuantita) req.Quantita = LeggiIntero("Nuova Quantità: ");

        // Salvataggio
        Console.Write("\nSalvare le modifiche nel database? (S/N): ");
        if (Console.ReadKey(true).Key == ConsoleKey.S)
        {
            //Validazione inserimento secondo i vincoli della classe ProductUpdateRequest (Data Annotations)
            var context = new ValidationContext(req, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(req, context, results, true);

            if (isValid)
            {
                var ok = PutAsync("api/Product", req).GetAwaiter().GetResult();
                Feedback(ok, "Dati aggiornati con successo!", "Errore durante il salvataggio.");
            }
            else
            {
                AppLogger.Instance.LogError("I dati inseriti non sono validi:");
                foreach (var validationResult in results)
                {
                    Console.WriteLine($"- {validationResult.ErrorMessage}");
                }
                Pausa();
            }
        }
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

        int clientId;
        try
        {
            var clientResp = _http.GetAsync($"api/Client/{codiceCliente}").GetAwaiter().GetResult();
            if (!clientResp.IsSuccessStatusCode)
            {
                AppLogger.Instance.LogWarning($"Cliente '{codiceCliente}' non trovato.");
                return;
            }
            string clientBody = clientResp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var clientDoc = JsonDocument.Parse(clientBody);
            clientId = clientDoc.RootElement.GetProperty("Data").GetProperty("Id").GetInt32();
            AppLogger.Instance.LogInfo($"Cliente trovato: ID {clientId}");
        }
        catch
        {
            AppLogger.Instance.LogError("Impossibile leggere l'Id del cliente dalla risposta.");
            return;
        }

        try
        {
            var response = _http.GetAsync($"api/Receipt/storico/cliente/{clientId}").GetAwaiter().GetResult();
            string body = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            AppLogger.Instance.LogInfo($"[HTTP {(int)response.StatusCode}] GET api/Receipt/storico/cliente/{clientId}");

            if (!response.IsSuccessStatusCode)
            {
                AppLogger.Instance.LogError($"Errore API: {body}");
                return;
            }

            using var doc = JsonDocument.Parse(body);
            var root = doc.RootElement;

            if (!root.TryGetProperty("Data", out var data))
            {
                AppLogger.Instance.LogWarning("Risposta API non contiene il campo Data.");
                Console.WriteLine(body);
                return;
            }

            decimal totSpeso  = data.TryGetProperty("TotaleSpeso",  out var ts) ? ts.GetDecimal() : 0;
            int     numOrdini = data.TryGetProperty("NumeroOrdini", out var no) ? no.GetInt32()  : 0;

            Console.WriteLine();
            Console.WriteLine($"  Cliente       : {codiceCliente} (ID: {clientId})");
            Console.WriteLine($"  Ordini totali : {numOrdini}");
            Console.WriteLine($"  Totale speso  : {totSpeso:C}");
            PrintSeparator();

            if (data.TryGetProperty("Scontrini", out var scontrini) && scontrini.ValueKind == JsonValueKind.Array)
            {
                if (!scontrini.EnumerateArray().Any())
                {
                    AppLogger.Instance.LogWarning($"Nessun ordine trovato per il cliente '{codiceCliente}'.");
                    return;
                }

                foreach (var s in scontrini.EnumerateArray())
                {
                    int    id      = s.TryGetProperty("Id",               out var sid) ? sid.GetInt32()   : 0;
                    string metodo  = s.TryGetProperty("MetodoPagamento",  out var mp)  ? mp.GetString()  ?? "" : "";
                    decimal tot    = s.TryGetProperty("TotaleDefinitivo", out var td)  ? td.GetDecimal()  : 0;
                    string dataE   = s.TryGetProperty("DataEmissione",    out var de)  ? de.GetString()  ?? "" : "";

                    Console.WriteLine($"  Scontrino #{id} | {metodo} | {tot:C} | {dataE}");

                    if (s.TryGetProperty("RicevutaDettagli", out var dettagli) && dettagli.ValueKind == JsonValueKind.Array)
                    {
                        foreach (var d in dettagli.EnumerateArray())
                        {
                            string nome = "";
                            if (d.TryGetProperty("Prodotto", out var prod) && prod.ValueKind != JsonValueKind.Null)
                                nome = prod.TryGetProperty("Nome", out var pn) ? pn.GetString() ?? "" : "";
                            int    qty  = d.TryGetProperty("Quantita",     out var q)   ? q.GetInt32()    : 0;
                            decimal pt  = d.TryGetProperty("PrezzoTotale", out var ptt) ? ptt.GetDecimal(): 0;
                            Console.WriteLine($"    - {nome}  x{qty}  {pt:C}");
                        }
                    }
                    PrintSeparator();
                }
            }
            else
            {
                AppLogger.Instance.LogWarning($"Nessun ordine trovato per il cliente '{codiceCliente}'.");
            }
        }
        catch (Exception ex)
        {
            AppLogger.Instance.LogError($"Errore visualizzazione storico: {ex.Message}");
        }
    }

    static void VisualizzaOrdiniDelGiorno()
    {
        Console.Clear();
        PrintHeader("ORDINI DEL GIORNO E TOTALE");

        try
        {
            var response = _http.GetAsync("api/Receipt/oggi").GetAwaiter().GetResult();
            string body = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            AppLogger.Instance.LogInfo($"[HTTP {(int)response.StatusCode}] GET api/Receipt/oggi");

            if (!response.IsSuccessStatusCode)
            {
                AppLogger.Instance.LogError($"Errore API: {body}");
                return;
            }

            using var doc = JsonDocument.Parse(body);
            var root = doc.RootElement;

            // Legge il campo Data che contiene l'oggetto anonimo { TotaleGiorno, NumeroScontrini, Scontrini }
            if (!root.TryGetProperty("Data", out var data))
            {
                AppLogger.Instance.LogWarning("Risposta API non contiene il campo Data.");
                Console.WriteLine(body);
                return;
            }

            decimal totale    = data.TryGetProperty("TotaleGiorno",    out var t) ? t.GetDecimal() : 0;
            int     numero    = data.TryGetProperty("NumeroScontrini",  out var n) ? n.GetInt32()  : 0;

            Console.WriteLine();
            Console.WriteLine($"  Ordini oggi   : {numero}");
            Console.WriteLine($"  Totale giorno : {totale:C}");
            PrintSeparator();

            if (data.TryGetProperty("Scontrini", out var scontrini) && scontrini.ValueKind == JsonValueKind.Array)
            {
                foreach (var s in scontrini.EnumerateArray())
                {
                    int    id      = s.TryGetProperty("Id",               out var sid)  ? sid.GetInt32()   : 0;
                    int    cliId   = s.TryGetProperty("ClientId",         out var cid)  ? cid.GetInt32()   : 0;
                    string metodo  = s.TryGetProperty("MetodoPagamento",  out var mp)   ? mp.GetString()  ?? "" : "";
                    decimal tot    = s.TryGetProperty("TotaleDefinitivo", out var td)   ? td.GetDecimal()  : 0;
                    string data_e  = s.TryGetProperty("DataEmissione",    out var de)   ? de.GetString()  ?? "" : "";

                    Console.WriteLine($"  Scontrino #{id} | Cliente ID: {cliId} | {metodo} | {tot:C} | {data_e}");

                    if (s.TryGetProperty("RicevutaDettagli", out var dettagli) && dettagli.ValueKind == JsonValueKind.Array)
                    {
                        foreach (var d in dettagli.EnumerateArray())
                        {
                            string nome  = "";
                            if (d.TryGetProperty("Prodotto", out var prod) && prod.ValueKind != JsonValueKind.Null)
                                nome = prod.TryGetProperty("Nome", out var pn) ? pn.GetString() ?? "" : "";
                            int    qty   = d.TryGetProperty("Quantita",    out var q)  ? q.GetInt32()   : 0;
                            decimal pt   = d.TryGetProperty("PrezzoTotale",out var ptt)? ptt.GetDecimal(): 0;
                            Console.WriteLine($"    - {nome}  x{qty}  {pt:C}");
                        }
                    }
                    PrintSeparator();
                }
            }
            else
            {
                AppLogger.Instance.LogWarning("Nessun ordine registrato oggi.");
            }
        }
        catch (Exception ex)
        {
            AppLogger.Instance.LogError($"Errore visualizzazione ordini del giorno: {ex.Message}");
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
            if (!response.IsSuccessStatusCode)
            {
                AppLogger.Instance.LogError($"GET {endpoint} -> HTTP {(int)response.StatusCode}: {body}");
                return null;
            }
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
        decimal valore;
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine()?.Replace(',', '.').Trim() ?? "";

            if (decimal.TryParse(input, System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out valore))
            {
                return valore;
            }

            Console.WriteLine("[ERRORE] Inserisci un numero valido (es: 1500 o 1500.50)");
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