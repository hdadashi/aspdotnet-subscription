using MassTransit;
using Subscription.Infrastructure.Contracts;
using Subscription.Infrastructure.Persistence;

namespace Subscription.Infrastructure.UnitOfWorks;



public class UnitOfWork(SubscriptionDbContext subscriptionContext, IPublishEndpoint publishEndpoint)
    : IUnitOfWork
{
    // MassTransit
    public async Task SaveChangesAsync() => await subscriptionContext.SaveChangesAsync();
    public async Task PublishDomainEventsAsync(IEnumerable<object> events)
    {
        foreach (var e in events)
        {
            await publishEndpoint.Publish(e);
        }
    }
}