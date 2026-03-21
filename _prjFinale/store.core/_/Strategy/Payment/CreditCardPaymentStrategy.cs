using store.core._.Domain.Const;
using store.core._.Interface;
namespace store.core._.Strategy.Payment;

public class CreditCardPaymentStrategy : IPaymentStrategy
{
    public string Provider => PaymentProvider.CreditCard;

    public string ExecutePayment(decimal amount) => $"CreditCard => Pagamento effettuato di: {amount}€";
}