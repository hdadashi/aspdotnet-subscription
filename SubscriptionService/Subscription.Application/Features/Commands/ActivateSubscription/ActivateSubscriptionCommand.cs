using MediatR;

namespace Subscription.Application.Features.Commands.ActivateSubscription;

public record ActivateSubscriptionCommand(Guid UserId, Guid PlanId) : IRequest<bool>;