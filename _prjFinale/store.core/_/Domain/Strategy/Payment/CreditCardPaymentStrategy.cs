using store.core._.Domain.Const;
using store.core._.Domain.Interface;
namespace store.core._.Domain.Strategy.Payment;

public class CreditCardPaymentStrategy : IPaymentStrategy
{
    public string Provider => PaymentProvider.CreditCard;

    public string ExecutePayment(decimal amount) => $"CreditCard => Pagamento effettuato di: {amount}€";
}