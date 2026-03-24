using store.api.src.Dto.Person;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
namespace store.client;

class Program
{
    static void Main()
    {
        bool flagMainLoop = true;
        while (flagMainLoop)
        {
            ConsoleKeyInfo opzione;
            do {
                Console.Clear();
                Console.WriteLine(new string('=', 5) + "Terminale"+ new string('=', 5));
                Console.WriteLine("Seleziona:\n1. Aggiungi (Anagrafica)\n2. Visualizza (Anagrafica)\n3. Aggiungi vendita (Prodotto)\n4. (Esci)");
                opzione = Console.ReadKey();
            } while (opzione.Key is < ConsoleKey.D1 or > ConsoleKey.D4);

            switch (opzione.Key)
            {
                case ConsoleKey.D1:
                    AddEntity();
                    ContinueAndClear();
                    break;
                case ConsoleKey.D2:
                    VisualizeEntity();
                    ContinueAndClear();
                    break;
                case ConsoleKey.D3:
                    AddSale();
                    ContinueAndClear();
                    break;
                case ConsoleKey.D4:
                    flagMainLoop = false;
                    Console.WriteLine("\n\nEsco dal programma...");
                    break;
            }
        }

    }

    public static void AddEntity()
    {
        PersonCreateRequest richiesta = new PersonCreateRequest();
        var risposta = new ConsoleKeyInfo();

        bool inserimento = true;
        while (inserimento)
        {
            Console.WriteLine("Inserisci il codice fiscale: ");
            richiesta.CodiceFiscale = Console.ReadLine();

            Console.WriteLine("Inserisci il nome: ");
            richiesta.Nome = Console.ReadLine();

            Console.WriteLine("Inserisci il cognome: ");
            richiesta.Cognome = Console.ReadLine();

            Console.WriteLine("Inserisci il sesso (M/F): ");
            richiesta.Sesso = Console.ReadLine().ToUpper();

            Console.WriteLine("Inserisci la data di nascita (yyyy-MM-dd): ");
            string dataStr = Console.ReadLine();
            if (DateOnly.TryParse(dataStr, out DateOnly data))
            {
                richiesta.DataNascita = data;
            }
            else
            {
                Console.WriteLine("Data non valida.");
            }

            Console.WriteLine("Inserisci la città: ");
            richiesta.Citta = Console.ReadLine();

            Console.WriteLine("Inserisci la provincia: ");
            richiesta.Provincia = Console.ReadLine();

            Console.WriteLine("Inserisci il CAP: ");
            richiesta.CodicePostale = Console.ReadLine();

            Console.WriteLine("Inserisci l'indirizzo: ");
            richiesta.Indirizzo = Console.ReadLine();

            Console.WriteLine("Inserisci il numero di contatto: ");
            richiesta.NumeroContatto = Console.ReadLine();

            Console.WriteLine("Inserisci l'email: ");
            richiesta.Email = Console.ReadLine();

            // Validazione
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(richiesta);
            bool isValid = Validator.TryValidateObject(richiesta, validationContext, validationResults, true);

            if (!isValid)
            {
                Console.WriteLine("Errori di validazione:");
                foreach (var error in validationResults)
                {
                    Console.WriteLine($"- {error.ErrorMessage}");
                }
            }
            else
            {
                bool successo = PostPerson(richiesta).GetAwaiter().GetResult();

                if (successo)
                {
                    Console.WriteLine("Persona registrata con successo!");
                }
                else
                {
                    Console.WriteLine("Registrazione fallita.");
                }
            }
            do
            {
                Console.WriteLine("\nVuoi continuare ad inserire anagrafiche? (S/N)");
                risposta = Console.ReadKey(true);
                switch (risposta.Key)
                {
                    case ConsoleKey.S:
                        Console.WriteLine("Continuo ad inserire...");
                        break;
                    case ConsoleKey.N:
                        Console.WriteLine("Torno al menu principale...");
                        inserimento = false;
                        break;
                    default:
                        Console.WriteLine("Opzione non valida. Riprova.");
                        continue;
                }
            }
            while(risposta.Key != ConsoleKey.S && risposta.Key != ConsoleKey.N);
        }
    }



    public static async Task<bool> PostPerson(PersonCreateRequest richiesta)
    {
        string baseAddress = "http://localhost:5102/";

        using (HttpClient client = new HttpClient())
        {
            try
            {
                Console.WriteLine("\nInvio dati al server in corso...");

                HttpResponseMessage response = await client.PostAsJsonAsync($"{baseAddress}api/Person", richiesta);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    string errore = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Il server ha risposto con un errore: {response.StatusCode} - {errore}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore di connessione: {ex.Message}");
                return false;
            }
        }
    }

    /* public static async Task<bool> GetPerson(PersonGetByRequest richiesta)
    {
        string baseAddress = "http://localhost:5102/";

        using (HttpClient client = new HttpClient())
        {
            try
            {
                Console.WriteLine("\nInvio dati al server in corso...");

                HttpResponseMessage response = await client.PostAsJsonAsync($"{baseAddress}api/Person", richiesta);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    string errore = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Il server ha risposto con un errore: {response.StatusCode} - {errore}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore di connessione: {ex.Message}");
                return false;
            }
        }
    } */

    public static void VisualizeEntity()
    {
        /* PersonGetByRequest richiesta = new PersonGetByRequest();
        Console.WriteLine("Inserisci il codice fiscale della persona da visualizzare: ");
        richiesta.CodiceFiscale = Console.ReadLine();

        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(richiesta);
        bool isValid = Validator.TryValidateObject(richiesta, validationContext, validationResults, true);

            if (!isValid)
            {
                Console.WriteLine("Errori di validazione:");
                foreach (var error in validationResults)
                {
                    Console.WriteLine($"- {error.ErrorMessage}");
                }
            }
            else
            {
                bool successo = GetPerson(richiesta).GetAwaiter().GetResult();

                if (successo)
                {
                    Console.WriteLine("Persona registrata con successo!");
                }
                else
                {
                    Console.WriteLine("Registrazione fallita.");
                }
            } */

    }

    public static void AddSale()
    {
        
    }

    public static void ContinueAndClear()
    {
        Console.WriteLine("\nPremere un tasto per continuare...");
        Console.ReadKey(true);
        Console.Write("\x1b[3J");
        Console.Clear();
    }
}