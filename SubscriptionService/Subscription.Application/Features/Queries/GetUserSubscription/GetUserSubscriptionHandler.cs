using MediatR;
using Subscription.Domain.Entities;
using Subscription.Infrastructure.Contracts;
using Subscription.Infrastructure.Repositories;

namespace Subscription.Application.Features.Queries.GetUserSubscription;

public class GetUserSubscriptionHandler(ISubscriptionRepository subscriptionRepository)
    : IRequestHandler<GetUserSubscriptionQuery, UserSubscriptionDto?>
{
    public async Task<UserSubscriptionDto?> Handle(GetUserSubscriptionQuery request, CancellationToken ct)
    {
        var sub = await subscriptionRepository.GetUserSubscriptionAsync(request.UserId, ct);
        if (sub == null) return null;

        return new UserSubscriptionDto(
            sub.Id,
            sub.PlanId,
            sub.IsActive,
            sub.ActivatedAt,
            sub.DeactivatedAt
        );
    }
}