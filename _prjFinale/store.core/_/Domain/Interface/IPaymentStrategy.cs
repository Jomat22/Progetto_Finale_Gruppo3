namespace store.core._.Domain.Interface;

public interface IPaymentStrategy
{
    string Provider { get; }
    string ExecutePayment(decimal amount);
}