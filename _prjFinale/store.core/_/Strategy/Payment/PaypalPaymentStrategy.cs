using store.core._.Domain.Const;
using store.core._.Interface;
namespace store.core._.Strategy.Payment;

public class PaypalPaymentStrategy : IPaymentStrategy
{
    public string Provider => PaymentProvider.Paypal;

    public string ExecutePayment(decimal amount) => $"PayPal => Pagamento effettuato di: {amount}€";
}