using store.client.Singleton;
namespace store.client.Observer;

public class LoggerObserver : IOrderObserver
{
    public void OnOrderCreated(OrderCreatedEvent evento)
    {
        AppLogger.Instance.LogSuccess(
            $"Ordine registrato — Prodotto: '{evento.NomeProdotto}' | " +
            $"Totale: {evento.PrezzoFinale:C} | Pagamento: {evento.MetodoPagamento}");
    }
}

public class MagazziniereObserver : IOrderObserver
{
    private readonly string _nomeMagazziniere;

    public MagazziniereObserver(string nomeMagazziniere)
    {
        _nomeMagazziniere = nomeMagazziniere;
    }

    public void OnOrderCreated(OrderCreatedEvent evento)
    {
        Console.WriteLine($"\n  [NOTIFICA → {_nomeMagazziniere}]");
        Console.WriteLine($"  Nuovo ordine alle {evento.Timestamp:HH:mm:ss}");
        Console.WriteLine($"  Prodotto : {evento.NomeProdotto}");
        Console.WriteLine($"  Totale   : {evento.PrezzoFinale:C}");
    }
}

public class PagamentiObserver : IOrderObserver
{
    public void OnOrderCreated(OrderCreatedEvent evento)
    {
        Console.WriteLine($"\n  [NOTIFICA → Ufficio Pagamenti]");
        Console.WriteLine($"  Metodo: {evento.MetodoPagamento} | Importo: {evento.PrezzoFinale:C}");
    }
}
