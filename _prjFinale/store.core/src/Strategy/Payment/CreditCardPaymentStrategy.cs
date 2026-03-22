using store.core.src.Const;
using store.core.src.Interface;
namespace store.core.src.Strategy.Payment;

public class CreditCardPaymentStrategy : IPaymentStrategy
{
    public string Provider => PaymentProvider.CreditCard;

    public string ExecutePayment(decimal amount) => $"CreditCard => Pagamento effettuato di: {amount}€";
}