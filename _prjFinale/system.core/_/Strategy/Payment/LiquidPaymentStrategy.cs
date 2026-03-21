using system.core._.Domain.Const;
using system.core._.Interface;
namespace system.core._.Strategy.Payment;

public class LiquidPaymentStrategy : IPaymentStrategy
{
    public string Provider => PaymentProvider.Liquid;

    public string ExecutePayment(decimal amount) => $"Liquid => Pagamento effettuato di: {amount}€";
}