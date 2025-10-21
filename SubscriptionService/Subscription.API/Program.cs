using Microsoft.EntityFrameworkCore;
using MediatR;
using MassTransit;
using Subscription.Application;
using Subscription.Application.Features.Commands.ActivateSubscription;
using Subscription.Application.Features.Commands.DeactivateSubscription;
using Subscription.Application.Features.Queries.GetPlans;
using Subscription.Application.Features.Queries.GetUserSubscription;
using Subscription.Domain.Common;
using Subscription.Infrastructure;
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

builder.Services.AddInfrastructure();
builder.Services.AddApplication();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Seed initial data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SubscriptionDbContext>();
    await db.Database.MigrateAsync();
    await SeedData.InitializeAsync(db);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Subscription API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseRouting();
app.MapControllers();


app.Run();