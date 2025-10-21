# Subscription Service

## Project Summary

This project is a **microservice-based subscription management system** built with .NET 9. It allows users to:

- View available subscription plans
- Activate or deactivate a subscription
- Manage their subscription status

The project follows **Domain-Driven Design (DDD)**, **CQRS**, and **event-driven architecture** principles.

---

## Technologies Used

| Technology | Purpose |
|------------|---------|
| **.NET 9** | Core framework for building the API and application layers |
| **PostgreSQL** | Primary relational database for storing plans and subscriptions |
| **Entity Framework Core (EF Core)** | ORM for database access and migrations |
| **MediatR** | Implements CQRS pattern for clean separation of commands and queries |
| **MassTransit + RabbitMQ** | Event-driven communication between services |
| **Docker** | Containerization for easy deployment and consistent environment |
| **xUnit + InMemory EF Core** | Unit testing framework and in-memory database for tests |

---

## Why These Technologies Were Chosen

- **.NET 9**: Latest stable .NET version for performance, modern language features, and long-term support.
- **PostgreSQL**: Reliable, widely-used relational database with strong support for transactions and constraints.
- **EF Core**: Simplifies database access, migrations, and supports code-first DDD modeling.
- **MediatR (CQRS)**: Enforces separation of queries (read) and commands (write), improving maintainability and testability.
- **MassTransit + RabbitMQ**: Enables asynchronous communication for microservices, ensuring scalability and decoupling.
- **Docker**: Makes the service environment-independent, easy to deploy, and consistent across dev/staging/production.
- **xUnit + InMemory**: Fast and reliable testing of business logic without requiring a real database.

---

## Improvement Ideas

1. **Add Authentication/Authorization**  
   - Secure subscription operations per user with JWT or IdentityServer.

2. **Expand Event-Driven Features**  
   - Publish events on subscription activation/deactivation to trigger notifications or billing workflows.

3. **Implement Read Models / Caching**  
   - Use separate read models (e.g., with Dapper or Redis) for high-performance queries.

4. **Support Multiple User Subscriptions**  
   - Allow a user to hold multiple subscriptions simultaneously with different plans.

5. **Add Integration & End-to-End Tests**  
   - Test interactions with Postgres, RabbitMQ, and external services in CI pipelines.

---

## Conclusion

This project demonstrates a clean architecture approach to a subscription system using modern .NET technologies. It highlights **CQRS, DDD, microservices, and event-driven design** principles.  

The system is **ready for extension**, easily testable, and containerized, making it production-ready with clear pathways for future improvements.
