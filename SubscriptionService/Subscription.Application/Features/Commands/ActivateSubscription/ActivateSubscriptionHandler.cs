using MediatR;
using Subscription.Domain.DomainEvents;
using Subscription.Domain.Entities;
using Subscription.Infrastructure.Contracts;

namespace Subscription.Application.Features.Commands.ActivateSubscription;


public class ActivateSubscriptionHandler(ISubscriptionRepository subscriptionRepository, IUnitOfWork uow)
    : IRequestHandler<ActivateSubscriptionCommand, bool>
{


    public async Task<bool> Handle(ActivateSubscriptionCommand request, CancellationToken ct)
    {
        var existing = await subscriptionRepository.GetByUserIdAsync(request.UserId, ct);
        if (existing == null)
        {
            existing = UserSubscription.Create(request.UserId, request.PlanId);
            existing.Activate();
            await subscriptionRepository.AddAsync(existing, ct);
        }
        else
        {
            // switch plan
            existing = UpdatePlan(existing, request.PlanId);
            existing.Activate();
            subscriptionRepository.Update(existing);
        }
        
        await uow.SaveChangesAsync();
        
        // gather domain events and return them to infrastructure for publishing
        var events = DomainEvents.ReadAll().ToList();
        DomainEvents.Clear();
        await uow.PublishDomainEventsAsync(events);
        
        return true;
    }


    private static UserSubscription UpdatePlan(UserSubscription sub, Guid newPlan)
    {
        // Since the setter in the UserSubscription entity is private, reflection is used to access it and assign a value.
        var field = typeof(UserSubscription).GetProperty("PlanId", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
        if (field != null) field.SetValue(sub, newPlan);
        return sub;
    }
}