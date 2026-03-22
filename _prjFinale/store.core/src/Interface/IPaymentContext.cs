namespace store.core.src.Interface;

public interface IPaymentContext
{
    public string ExecuteStrategy(string providerName, decimal amount);
}