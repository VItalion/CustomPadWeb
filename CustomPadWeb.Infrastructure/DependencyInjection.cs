using CustomPadWeb.Domain.DomainEvents;
using CustomPadWeb.Domain.Repositories;
using CustomPadWeb.Infrastructure.IntegrationEvents;
using CustomPadWeb.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace CustomPadWeb.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var conn = configuration["Postgres:ConnectionString"]
            ?? throw new InvalidOperationException("Missing connection string");

            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(conn));

            // Add RabbitMQ Connection
            services.AddSingleton<IConnection>(sp =>
            {
                var factory = new ConnectionFactory
                {
                    HostName = configuration["RabbitMQ:Host"] ?? "localhost",
                    Port = int.TryParse(configuration["RabbitMQ:Port"], out var portVal) ? portVal : 5672,
                    UserName = configuration["RabbitMQ:User"] ?? "guest",
                    Password = configuration["RabbitMQ:Password"] ?? "guest",
                };

                return factory.CreateConnectionAsync().Result;
            });

            // Add Integration Event Bus
            services.AddSingleton<IIntegrationEventBus, RabbitMqIntegrationEventBus>();

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
            services.Scan(scan => scan
                .FromAssemblyOf<DomainEventDispatcher>()
                .AddClasses(classes => classes.AssignableTo(typeof(IDomainEventHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            return services;
        }
    }
}
