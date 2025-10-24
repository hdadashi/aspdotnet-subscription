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
            existing.UpdatePlan(request.PlanId);
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
}