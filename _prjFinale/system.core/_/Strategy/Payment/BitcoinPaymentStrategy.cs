using system.core._.Interface;
namespace system.core._.Strategy.Payment;

public class BitcoinPaymentStrategy : IPaymentStrategy
{
    public string ExecutePayment(decimal amount)
    {
        return $"Bitcoin => Pagamento effettuato di: {amount}€";
    }
}