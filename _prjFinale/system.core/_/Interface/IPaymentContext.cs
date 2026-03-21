namespace system.core._.Interface;

public interface IPaymentContext
{
    public string ExecuteStrategy(string providerName, decimal amount);
}