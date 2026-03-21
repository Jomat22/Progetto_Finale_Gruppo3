using Microsoft.Extensions.Logging;
using store.core._.Interface;

namespace store.core._.Strategy.Payment;

public class PaymentContext : IPaymentContext
{
    // Il dizionario sostituisce il singolo campo '_strategy'
    private readonly IReadOnlyDictionary<string, IPaymentStrategy> _strategyMap;
    private readonly ILogger<IPaymentContext> _logger;

    public PaymentContext(IEnumerable<IPaymentStrategy> strategies, ILogger<IPaymentContext> logger)
    {
        _logger = logger;

        // Trasformiamo la lista iniettata da .NET in un dizionario rapido
        // Usiamo GroupBy per gestire eventuali registrazioni duplicate nel 'Program.cs'
        _strategyMap = strategies
            .GroupBy(s => s.Provider)
            .ToDictionary(
                group => group.Key,
                group => group.First(),
                StringComparer.OrdinalIgnoreCase // <-- troverà "Paypal" anche se l'utente scrive "pAyPaL", senza trasformazioni manuali.
            );
    }

    // Ora l'Execute riceve il nome del provider (es. "Paypal") e l'importo
    public string ExecuteStrategy(string providerName, decimal amount)
    {
        return _strategyMap.TryGetValue(providerName ?? "", out var strategy) switch
        {
            true => strategy.ExecutePayment(amount),
            false => HandleUnsupportedProvider(providerName)
        };
    }

    private string HandleUnsupportedProvider(string? name)
    {
        _logger.LogWarning("Tentativo di pagamento con provider non supportato: {Provider}", name ?? "NULL");
        return $"Errore: Metodo di pagamento '{name}' non supportato.";
    }
}