using store.core.src.Const;
using store.core.src.Interface;
namespace store.core.src.Strategy.Payment;

public class BitcoinPaymentStrategy : IPaymentStrategy
{
    public string Provider => PaymentProvider.Bitcoin;

    public string ExecutePayment(decimal amount) =>  $"Bitcoin => Pagamento effettuato di: {amount}€";
}