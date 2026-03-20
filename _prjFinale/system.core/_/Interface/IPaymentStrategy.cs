namespace system.core._.Interface;

public interface IPaymentStrategy
{
    public string ExecutePayment(decimal amount);
}