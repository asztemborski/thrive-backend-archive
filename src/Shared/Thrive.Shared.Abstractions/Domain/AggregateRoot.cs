namespace Thrive.Shared.Abstractions.Domain;

public abstract class AggregateRoot<T> where T : new()
{
    public T Id { get; protected set; } = new();
    public IEnumerable<IDomainEvent> Events => _events;

    private readonly List<IDomainEvent> _events = new();

    protected void AddEvent(IDomainEvent @event)
    {
        _events.Add(@event);
    }

    public void ClearEvents() => _events.Clear();
}