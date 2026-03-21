using Microsoft.Extensions.Logging;
using system.core._.Interface;

namespace system.core._.Strategy.Payment;

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
            .GroupBy(s => s.Provider.ToLower())
            .ToDictionary(
                group => group.Key,
                group => group.First() 
            );
    }

    // Ora l'Execute riceve il nome del provider (es. "Paypal") e l'importo
    public string ExecuteStrategy(string providerName, decimal amount)
    {
        if (string.IsNullOrWhiteSpace(providerName)) return "Errore: Provider non specificato.";

        if (_strategyMap.TryGetValue(providerName.ToLower(), out var strategy))
        {
            return strategy.ExecutePayment(amount);
        }

        _logger.LogWarning("Tentativo di pagamento con provider non supportato: {Provider}", providerName);
        return $"Errore: Metodo di pagamento '{providerName}' non supportato.";
    }
}