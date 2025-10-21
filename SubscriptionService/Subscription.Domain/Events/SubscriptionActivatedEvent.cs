namespace Subscription.Domain.Events;

public sealed record SubscriptionActivatedEvent(Guid SubscriptionId, Guid UserId, Guid PlanId, DateTime ActivatedAt);
public sealed record SubscriptionDeactivatedEvent(Guid SubscriptionId, Guid UserId, Guid PlanId, DateTime DeactivatedAt);
