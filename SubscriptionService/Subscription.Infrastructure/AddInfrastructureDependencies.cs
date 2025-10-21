using Microsoft.Extensions.DependencyInjection;
using Subscription.Infrastructure.Contracts;
using Subscription.Infrastructure.Repositories;
using Subscription.Infrastructure.UnitOfWorks;

namespace Subscription.Infrastructure;

public static class AddInfrastructureDependencies
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}