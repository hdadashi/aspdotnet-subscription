using Microsoft.EntityFrameworkCore;
using Subscription.Domain.Entities;
using Subscription.Infrastructure.Persistence;

namespace Subscription.Infrastructure.Seeds;

public static class SeedData
{
    public static async Task InitializeAsync(SubscriptionDbContext context)
    {
        var canUseTransaction = context.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory";
        var transaction = canUseTransaction ? await context.Database.BeginTransactionAsync() : null;
        
        try
        {
            var hasPlan = await context.Plans.AnyAsync();
            if (!hasPlan)
            {
                var plans = new List<SubscriptionPlan>
                {
                    new (Guid.NewGuid(), "Basic", 9.99m, "Basic plan"),
                    new (Guid.NewGuid(), "Standard", 19.99m, "Standard plan"),
                    new (Guid.NewGuid(), "Premium", 29.99m, "Premium plan"),
                };

                await context.Plans.AddRangeAsync(plans);
                await context.SaveChangesAsync();
            }
            
            if (!await context.UserSubscriptions.AnyAsync())
            {
                var testUserId = Guid.NewGuid();
                var standardPlan = await context.Plans.FirstAsync(p => p.Name == "Standard");
                var userSub = UserSubscription.Create(testUserId, standardPlan.Id);
                await context.UserSubscriptions.AddAsync(userSub);
                await context.SaveChangesAsync();
            }
            if (canUseTransaction && transaction != null)
                await transaction.CommitAsync();

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            if (canUseTransaction && transaction != null)
                await transaction.RollbackAsync();
            throw;
        }
        finally
        {
            if (transaction != null)
                await transaction.DisposeAsync();
        }
    }
}