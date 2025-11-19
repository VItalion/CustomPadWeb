using CustomPadWeb.Domain.DomainEvents;
using CustomPadWeb.Infrastructure.IntegrationEvents;
using CustomPadWeb.Infrastructure.IntegrationEvents.Events;
namespace CustomPadWeb.ApiService.EventHandlers
{
    public class OrderCreatedDomainEventHandler : IDomainEventHandler<OrderCreatedDomainEvent>
    {
        private readonly IIntegrationEventBus _integrationBus;

        public OrderCreatedDomainEventHandler(IIntegrationEventBus integrationBus)
        {
            _integrationBus = integrationBus;
        }

        public async Task HandleAsync(OrderCreatedDomainEvent @event, CancellationToken token = default)
        {
            var integrationEvent = new OrderCreatedIntegrationEvent(@event.OrderId, @event.CustomerEmail);
            await _integrationBus.PublishAsync(integrationEvent, token);
        }
    }
}
