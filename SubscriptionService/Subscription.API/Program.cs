using Microsoft.EntityFrameworkCore;
using MediatR;
using MassTransit;
using Subscription.Application.Features.Commands.ActivateSubscription;
using Subscription.Application.Features.Commands.DeactivateSubscription;
using Subscription.Application.Features.Queries.GetPlans;
using Subscription.Application.Features.Queries.GetUserSubscription;
using Subscription.Domain.Common;
using Subscription.Infrastructure.Repositories;
using Subscription.Infrastructure.Contracts;
using Subscription.Infrastructure.Persistence;
using Subscription.Infrastructure.Seeds;
using Subscription.Infrastructure.UnitOfWorks;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// config for Postgres connection string via env or appsettings
var conn = builder.Configuration.GetConnectionString("SubscriptionConnectionString") 
           ?? "Host=localhost;Port=5432;Database=subscriptiondb;Username=admin;Password=123456Aa@";
builder.Services.AddDbContext<SubscriptionDbContext>(opt => opt.UseNpgsql(conn));


// MassTransit (RabbitMQ)
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        var rabbitSection = builder.Configuration.GetSection("RabbitMQ");

        var host = rabbitSection.GetValue<string>("Host") ?? "localhost";
        var user = rabbitSection.GetValue<string>("User") ?? "guest";
        var pass = rabbitSection.GetValue<string>("Pass") ?? "guest";

        cfg.Host(host, h =>
        {
            h.Username(user);
            h.Password(pass);
        });
    });
});

// Repos & UoW
builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(
        typeof(Program).Assembly,
        typeof(GetUserSubscriptionHandler).Assembly
    );
});

var app = builder.Build();

// Seed initial data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SubscriptionDbContext>();
    await db.Database.MigrateAsync();
    await SeedData.InitializeAsync(db);
}

app.MapGet("/plans", async (IMediator mediator) => await mediator.Send(new GetPlansQuery()));
app.MapGet("/users/{userId:guid}/subscription", async (Guid userId, IMediator mediator) => await mediator.Send(new GetUserSubscriptionQuery(userId)));
app.MapPost("/users/{userId:guid}/subscription/activate", async (Guid userId, ActivateRequest req, IMediator mediator) => await mediator.Send(new ActivateSubscriptionCommand(userId, req.PlanId)));
app.MapPost("/users/{userId:guid}/subscription/deactivate", async (Guid userId, IMediator mediator) => await mediator.Send(new DeactivateSubscriptionCommand(userId)));


app.Run();
