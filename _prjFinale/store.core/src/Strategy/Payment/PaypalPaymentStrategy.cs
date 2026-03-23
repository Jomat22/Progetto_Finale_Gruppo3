using store.core.src.Const;
using store.core.src.Interface;
namespace store.core.src.Strategy.Payment;

public class PaypalPaymentStrategy : IPaymentStrategy
{
    public string Provider => PaymentProvider.Paypal;

    public string ExecutePayment(decimal amount) => $"PayPal => Pagamento effettuato di: {amount}€";
}