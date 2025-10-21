using MediatR;

namespace Subscription.Application.Features.Commands.DeactivateSubscription;

public record DeactivateSubscriptionCommand(Guid UserId) : IRequest<bool>;