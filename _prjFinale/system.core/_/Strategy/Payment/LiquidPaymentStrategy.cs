using system.core._.Interface;
namespace system.core._.Strategy.Payment;

public class LiquidPaymentStrategy : IPaymentStrategy
{
    public string ExecutePayment(decimal amount)
    {
        return $"Liquid => Pagamento effettuato di: {amount}€";
    }
}