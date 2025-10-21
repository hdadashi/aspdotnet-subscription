using MediatR;
using Subscription.Domain.DomainEvents;
using Subscription.Infrastructure.Contracts;

namespace Subscription.Application.Features.Commands.DeactivateSubscription;


public class DeactivateSubscriptionHandler(ISubscriptionRepository subscriptionRepository, IUnitOfWork uow)
    : IRequestHandler<DeactivateSubscriptionCommand, bool>
{


    public async Task<bool> Handle(DeactivateSubscriptionCommand request, CancellationToken ct)
    {
        var existing = await subscriptionRepository.GetByUserIdAsync(request.UserId, ct);
        if (existing == null) return false;
        existing.Deactivate();
        subscriptionRepository.Update(existing);
        await uow.SaveChangesAsync();
        var events = DomainEvents.ReadAll().ToList();
        DomainEvents.Clear();
        await uow.PublishDomainEventsAsync(events);
        return true;
    }
}