using system.core._.Domain.Const;
using system.core._.Interface;
namespace system.core._.Strategy.Payment;

public class PaypalPaymentStrategy : IPaymentStrategy
{
    public string Provider => PaymentProvider.Paypal;

    public string ExecutePayment(decimal amount) => $"PayPal => Pagamento effettuato di: {amount}€";
}