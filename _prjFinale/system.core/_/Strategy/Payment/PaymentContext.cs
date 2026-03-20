using system.core._.Interface;
namespace system.core._.Strategy.Payment;

public class PaymentContext : IPaymentContext
{
    private IPaymentStrategy? _strategy;

    public void SetStrategy(IPaymentStrategy strategy) => _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
    public void ClearStrategy() => _strategy = null;
    public string ExecuteStrategy(decimal amount) => _strategy?.ExecutePayment(amount) ?? string.Empty;
}