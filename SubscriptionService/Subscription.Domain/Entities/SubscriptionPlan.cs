namespace Subscription.Domain.Entities;


public sealed class SubscriptionPlan
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public decimal PricePerMonth { get; private set; }
    public string Description { get; private set; } = string.Empty;


    private SubscriptionPlan() { }
    public SubscriptionPlan(Guid id, string name, decimal pricePerMonth, string description = "")
    {
        Id = id;
        Name = name;
        PricePerMonth = pricePerMonth;
        Description = description;
    }
}