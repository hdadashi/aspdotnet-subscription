using Subscription.Domain.Events;


namespace Subscription.Domain.Entities;


public sealed class UserSubscription
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid PlanId { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime? ActivatedAt { get; private set; }
    public DateTime? DeactivatedAt { get; private set; }


    private UserSubscription() { }


    public static UserSubscription Create(Guid userId, Guid planId)
    {
        return new UserSubscription
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            PlanId = planId,
            IsActive = false,
            ActivatedAt = DateTime.UtcNow,
        };
    }


    public void Activate()
    {
        if (IsActive) return;
        IsActive = true;
        ActivatedAt = DateTime.UtcNow;
        // domain event
        DomainEvents.DomainEvents.Raise(new SubscriptionActivatedEvent(Id, UserId, PlanId, ActivatedAt.Value));
    }


    public void Deactivate()
    {
        if (!IsActive) return;
        IsActive = false;
        DeactivatedAt = DateTime.UtcNow;
        DomainEvents.DomainEvents.Raise(new SubscriptionDeactivatedEvent(Id, UserId, PlanId, DeactivatedAt.Value));
    }
    
    public void UpdatePlan(Guid newPlanId)
    {
        if (newPlanId == Guid.Empty)
            throw new ArgumentException("Plan ID cannot be empty.", nameof(newPlanId));

        if (newPlanId == PlanId)
            return;

        PlanId = newPlanId;
    }
}