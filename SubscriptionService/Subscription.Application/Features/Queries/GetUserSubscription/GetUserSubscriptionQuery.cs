using MediatR;

namespace Subscription.Application.Features.Queries.GetUserSubscription;


public record GetUserSubscriptionQuery(Guid UserId) : IRequest<UserSubscriptionDto?>;
