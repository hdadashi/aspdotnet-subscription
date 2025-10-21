namespace Subscription.Infrastructure.Contracts;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
    Task PublishDomainEventsAsync(IEnumerable<object> events);
}