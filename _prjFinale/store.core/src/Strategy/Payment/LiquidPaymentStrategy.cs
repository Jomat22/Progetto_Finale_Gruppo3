using store.core.src.Const;
using store.core.src.Interface;
namespace store.core.src.Strategy.Payment;

public class LiquidPaymentStrategy : IPaymentStrategy
{
    public string Provider => PaymentProvider.Liquid;

    public string ExecutePayment(decimal amount) => $"Liquid => Pagamento effettuato di: {amount}€";
}