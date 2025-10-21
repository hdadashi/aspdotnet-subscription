using System;
using Microsoft.EntityFrameworkCore;
using Subscription.Infrastructure.Persistence;

namespace Subscription.Tests;

public static class TestDbContextFactory
{
    public static SubscriptionDbContext Create()
    {
        var options = new DbContextOptionsBuilder<SubscriptionDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new SubscriptionDbContext(options);
        context.Database.EnsureCreated();
        return context;
    }
}