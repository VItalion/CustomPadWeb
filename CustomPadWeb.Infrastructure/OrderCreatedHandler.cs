using CustomPadWeb.Domain.DomainEvents;
using CustomPadWeb.Domain.Interfaces;

namespace CustomPadWeb.Infrastructure
{
    public class OrderCreatedHandler : IDomainEventHandler<OrderCreated>
    {
        private readonly IEventBus _bus;
        public OrderCreatedHandler(IEventBus bus) => _bus = bus;

        public async Task HandleAsync(OrderCreated domainEvent)
        {
            var integrationEvent = new OrderSubmittedIntegrationEvent(domainEvent.OrderId, domainEvent.UserId, domainEvent.OccurredOn);
            await _bus.PublishAsync(integrationEvent);
        }
    }
}
