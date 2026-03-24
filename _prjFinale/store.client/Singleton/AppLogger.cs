namespace store.client.src.Singleton;

public sealed class AppLogger
{
    private static AppLogger? _instance;
    private static readonly object _lock = new();

    private AppLogger() { }

    public static AppLogger Instance
    {
        get
        {
            if (_instance is null)
            {
                lock (_lock)
                {
                    _instance ??= new AppLogger();
                }
            }
            return _instance;
        }
    }

    private readonly List<string> _storico = [];

    public void Log(string messaggio)
    {
        string riga = $"[{DateTime.Now:HH:mm:ss}] {messaggio}";
        _storico.Add(riga);
        Console.WriteLine(riga);
    }

    public void LogInfo(string messaggio)    => Log($"[INFO]    {messaggio}");
    public void LogSuccess(string messaggio) => Log($"[OK]      {messaggio}");
    public void LogWarning(string messaggio) => Log($"[WARN]    {messaggio}");
    public void LogError(string messaggio)   => Log($"[ERRORE]  {messaggio}");

    public IReadOnlyList<string> Storico => _storico.AsReadOnly();

    public void StampaStorico()
    {
        Console.WriteLine("\n--- Storico log di sessione ---");
        foreach (var riga in _storico)
            Console.WriteLine(riga);
        Console.WriteLine("-------------------------------\n");
    }
}
