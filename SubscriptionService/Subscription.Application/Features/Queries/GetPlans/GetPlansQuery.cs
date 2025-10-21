using MediatR;

namespace Subscription.Application.Features.Queries.GetPlans;

public record GetPlansQuery() : IRequest<IEnumerable<PlanDto>>;