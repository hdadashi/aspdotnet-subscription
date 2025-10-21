using Microsoft.EntityFrameworkCore;
using Subscription.Domain.Entities;
using Subscription.Infrastructure.Contracts;
using Subscription.Infrastructure.Persistence;

namespace Subscription.Infrastructure.Repositories;

public class SubscriptionRepository(SubscriptionDbContext subscriptionContext) : ISubscriptionRepository
{
    public async Task<UserSubscription?> GetByUserIdAsync(Guid userId,CancellationToken ct) => await subscriptionContext.UserSubscriptions
        .FirstOrDefaultAsync(u => u.UserId == userId, cancellationToken: ct);
    public async Task AddAsync(UserSubscription entity, CancellationToken ct) => await subscriptionContext.UserSubscriptions.AddAsync(entity, ct);
    public void Update(UserSubscription entity) => subscriptionContext.UserSubscriptions.Update(entity);
    public async Task<IEnumerable<SubscriptionPlan>> GetPlansAsync(CancellationToken ct)
        => await subscriptionContext.Plans.ToListAsync(ct);

    public async Task<UserSubscription?> GetUserSubscriptionAsync(Guid userId, CancellationToken ct)
        => await subscriptionContext.UserSubscriptions.FirstOrDefaultAsync(
            u => u.UserId == userId, ct
        );
}