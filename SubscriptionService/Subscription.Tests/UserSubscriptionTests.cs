using System;
using Subscription.Domain.Entities;
using Xunit;

namespace Subscription.Tests;

public class UserSubscriptionTests
{
    [Fact]
    public void UpdatePlan_Should_Change_PlanId_Using_Reflection()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var oldPlan = Guid.NewGuid();
        var newPlan = Guid.NewGuid();
        var sub = UserSubscription.Create(userId, oldPlan);

        // Act
        sub.UpdatePlan(newPlan);

        // Assert
        Assert.Equal(newPlan, sub.PlanId);
    }
}