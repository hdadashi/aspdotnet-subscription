using Microsoft.EntityFrameworkCore;
using Subscription.Domain.Entities;

namespace Subscription.Infrastructure.Persistence;


public class SubscriptionDbContext(DbContextOptions<SubscriptionDbContext> options) : DbContext(options)
{
    public DbSet<SubscriptionPlan> Plans { get; set; } = null!;
    public DbSet<UserSubscription> UserSubscriptions { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SubscriptionPlan>(b => {
            b.HasKey(x => x.Id);
            b.Property(x => x.Name).IsRequired();
            b.Property(x => x.PricePerMonth).HasPrecision(18,2);
        });


        modelBuilder.Entity<UserSubscription>(b => {
            b.HasKey(x => x.Id);
            b.Property<Guid>("UserId");
            b.Property<Guid>("PlanId");
            b.Property(x => x.IsActive);
            b.Property(x => x.ActivatedAt);
            b.Property(x => x.DeactivatedAt);
        });
    }
}