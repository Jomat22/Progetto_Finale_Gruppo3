namespace store.core.src.Interface;

public interface IPaymentStrategy
{
    string Provider { get; }
    string ExecutePayment(decimal amount);
}