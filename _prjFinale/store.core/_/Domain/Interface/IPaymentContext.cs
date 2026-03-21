namespace store.core._.Domain.Interface;

public interface IPaymentContext
{
    public string ExecuteStrategy(string providerName, decimal amount);
}