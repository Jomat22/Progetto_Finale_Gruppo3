using store.core._.Domain.Const;
using store.core._.Domain.Interface;
namespace store.core._.Domain.Strategy.Payment;

public class PaypalPaymentStrategy : IPaymentStrategy
{
    public string Provider => PaymentProvider.Paypal;

    public string ExecutePayment(decimal amount) => $"PayPal => Pagamento effettuato di: {amount}€";
}