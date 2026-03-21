using system.core._.Domain.Const;
using system.core._.Interface;
namespace system.core._.Strategy.Payment;

public class CreditCardPaymentStrategy : IPaymentStrategy
{
    public string Provider => PaymentProvider.CreditCard;

    public string ExecutePayment(decimal amount) => $"Credit Card => Pagamento effettuato di: {amount}€";
}