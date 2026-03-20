using system.core._.Interface;
namespace system.core._.Strategy.Payment;

public class CreditCardPaymentStrategy : IPaymentStrategy
{
    public string ExecutePayment(decimal amount)
    {
        return $"Credit Card => Pagamento effettuato di: {amount}€";
    }
}