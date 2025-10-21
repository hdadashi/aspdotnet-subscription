namespace Subscription.Application.Features.Queries.GetPlans;

public record PlanDto(Guid Id, string Name, decimal PricePerMonth, string Description);