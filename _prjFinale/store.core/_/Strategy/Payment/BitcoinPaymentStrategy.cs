using store.core._.Domain.Const;
using store.core._.Interface;
namespace store.core._.Strategy.Payment;

public class BitcoinPaymentStrategy : IPaymentStrategy
{
    public string Provider => PaymentProvider.Bitcoin;

    public string ExecutePayment(decimal amount) =>  $"Bitcoin => Pagamento effettuato di: {amount}€";
}