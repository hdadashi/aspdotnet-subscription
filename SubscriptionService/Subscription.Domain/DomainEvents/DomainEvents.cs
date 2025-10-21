namespace Subscription.Domain.DomainEvents;


public static class DomainEvents
{
    private static readonly List<object> _raised = [];
    public static void Raise(object @event)
    {
        _raised.Add(@event);
    }
    public static IReadOnlyCollection<object> ReadAll() => _raised.AsReadOnly();
    public static void Clear() => _raised.Clear();
}