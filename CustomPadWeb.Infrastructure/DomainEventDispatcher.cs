using CustomPadWeb.Domain.Abstractions;
using CustomPadWeb.Domain.DomainEvents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CustomPadWeb.Infrastructure
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public DomainEventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task DispatchDomainEventsAsync(DbContext context)
        {
            var entities = context.ChangeTracker.Entries<Entity>()
                .Select(e => e.Entity)
                .Where(e => e.DomainEvents.Any())
                .ToArray();

            var events = entities.SelectMany(e => e.DomainEvents).ToList();

            foreach (var @event in events)
            {
                var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(@event.GetType());
                var handlers = _serviceProvider.GetServices(handlerType);

                foreach (var handler in handlers)
                {
                    await (Task)handlerType
                        .GetMethod(nameof(IDomainEventHandler<IDomainEvent>.HandleAsync))!
                        .Invoke(handler, new[] { @event })!;
                }
            }

            foreach (var entity in entities)
                entity.ClearDomainEvents();
        }
    }

}
