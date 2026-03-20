using system.core._.Interface;
namespace system.core._.Strategy.Payment;

public class PaypalPaymentStrategy : IPaymentStrategy
{
    public string ExecutePayment(decimal amount)
    {
        return $"PayPal => Pagamento effettuato di: {amount}€";
    }
}