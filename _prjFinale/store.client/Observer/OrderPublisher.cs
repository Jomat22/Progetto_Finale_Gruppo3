namespace store.client.src.Observer;

public class OrderPublisher : IOrderSubject
{
    private readonly List<IOrderObserver> _observers = [];

    public void Subscribe(IOrderObserver observer)
    {
        if (!_observers.Contains(observer))
            _observers.Add(observer);
    }

    public void Unsubscribe(IOrderObserver observer)
    {
        _observers.Remove(observer);
    }

    public void NotifyOrderCreated(OrderCreatedEvent evento)
    {
        foreach (var observer in _observers)
            observer.OnOrderCreated(evento);
    }
}
