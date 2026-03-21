using system.core._.Domain.Const;
using system.core._.Interface;
namespace system.core._.Strategy.Payment;

public class BitcoinPaymentStrategy : IPaymentStrategy
{
    public string Provider => PaymentProvider.Bitcoin;

    public string ExecutePayment(decimal amount) =>  $"Bitcoin => Pagamento effettuato di: {amount}€";
}