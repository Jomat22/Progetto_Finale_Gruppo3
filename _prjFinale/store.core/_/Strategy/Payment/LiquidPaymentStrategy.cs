using store.core._.Domain.Const;
using store.core._.Interface;
namespace store.core._.Strategy.Payment;

public class LiquidPaymentStrategy : IPaymentStrategy
{
    public string Provider => PaymentProvider.Liquid;

    public string ExecutePayment(decimal amount) => $"Liquid => Pagamento effettuato di: {amount}€";
}