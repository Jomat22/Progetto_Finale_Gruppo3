namespace system.core._.Interface;

public interface IPaymentContext
{
    public void SetStrategy(IPaymentStrategy strategy);
    public void ClearStrategy();
    public string ExecuteStrategy(decimal amount);
}