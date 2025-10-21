using Subscription.Domain.Entities;

namespace Subscription.Infrastructure.Contracts;

public interface ISubscriptionRepository
{
    Task<UserSubscription?> GetByUserIdAsync(Guid userId, CancellationToken ct);
    Task AddAsync(UserSubscription entity, CancellationToken ct);
    void Update(UserSubscription entity);
    Task<IEnumerable<SubscriptionPlan>> GetPlansAsync(CancellationToken ct);
    Task<UserSubscription?> GetUserSubscriptionAsync(Guid userId, CancellationToken ct);
}