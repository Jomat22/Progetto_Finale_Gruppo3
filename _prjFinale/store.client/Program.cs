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
            }
        }

    }

    public static void AddEntity()
    {
        
    }

    public static void VisualizeEntity()
    {
        
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