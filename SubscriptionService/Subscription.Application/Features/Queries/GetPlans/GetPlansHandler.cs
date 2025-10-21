using MediatR;
using Subscription.Infrastructure.Contracts;

namespace Subscription.Application.Features.Queries.GetPlans;


public class GetPlansHandler(ISubscriptionRepository subscriptionRepository) : IRequestHandler<GetPlansQuery, IEnumerable<PlanDto>>
{
    public async Task<IEnumerable<PlanDto>> Handle(GetPlansQuery request, CancellationToken ct)
    {
        var plans = await subscriptionRepository.GetPlansAsync(ct);
        return plans.Select(p => new PlanDto(p.Id, p.Name, p.PricePerMonth, p.Description));
    }
}