using Microsoft.Extensions.DependencyInjection;
using Subscription.Application.Features.Queries.GetUserSubscription;

namespace Subscription.Application;

public static class AddApplicationDependencies
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(
                typeof(GetUserSubscriptionHandler).Assembly
            );
        });

        return services;
    }
}