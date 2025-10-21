FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5001
EXPOSE 5002

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SubscriptionService/Subscription.API/Subscription.API.csproj", "SubscriptionService/Subscription.API/"]
COPY ["SubscriptionService/Subscription.Domain/Subscription.Domain.csproj", "SubscriptionService/Subscription.Domain/"]
COPY ["SubscriptionService/Subscription.Application/Subscription.Application.csproj", "SubscriptionService/Subscription.Application/"]
COPY ["SubscriptionService/Subscription.Infrastructure/Subscription.Infrastructure.csproj", "SubscriptionService/Subscription.Infrastructure/"]
RUN dotnet restore "SubscriptionService/Subscription.API/Subscription.API.csproj"
COPY . .
WORKDIR "/src/SubscriptionService/Subscription.API"
RUN dotnet build "./Subscription.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Subscription.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Subscription.API.dll"]
