using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Subscription.Infrastructure.Seeds;
using Xunit;

namespace Subscription.Tests;

public class SeedDataTests
{
    [Fact]
    public async Task Seed_Plans_And_UserSubscription_Should_Work()
    {
        // Arrange
        var context = TestDbContextFactory.Create();

        // Act
        await SeedData.InitializeAsync(context);

        // Assert
        var plans = await context.Plans.ToListAsync();
        Assert.Equal(3, plans.Count);

        var userSubs = await context.UserSubscriptions.ToListAsync();
        Assert.Single(userSubs);
        var sub = userSubs.First();
        Assert.False(sub.IsActive); // check factory default
        Assert.Equal("Standard", (await context.Plans.FindAsync(sub.PlanId))!.Name);
    }
}