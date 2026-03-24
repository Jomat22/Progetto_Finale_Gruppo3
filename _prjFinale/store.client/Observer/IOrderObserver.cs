namespace store.client.Observer;

public interface IOrderObserver
{
    void OnOrderCreated(OrderCreatedEvent evento);
}

public interface IOrderSubject
{
    void Subscribe(IOrderObserver observer);
    void Unsubscribe(IOrderObserver observer);
    void NotifyOrderCreated(OrderCreatedEvent evento);
}

public record OrderCreatedEvent(
    string NomeProdotto,
    decimal PrezzoFinale,
    string MetodoPagamento,
    DateTime Timestamp
);