namespace Subscription.Application.Features.Queries.GetUserSubscription;

public record UserSubscriptionDto(Guid SubscriptionId, Guid PlanId, bool IsActive, DateTime? ActivatedAt, DateTime? DeactivatedAt);