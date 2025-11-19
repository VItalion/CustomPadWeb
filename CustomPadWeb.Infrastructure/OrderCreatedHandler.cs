using CustomPadWeb.Domain.DomainEvents;
using Microsoft.Extensions.Logging;

namespace CustomPadWeb.Infrastructure
{
    public class OrderCreatedHandler : IDomainEventHandler<OrderCreatedDomainEvent>
    {
        private readonly ILogger<OrderCreatedHandler> _logger;

        public OrderCreatedHandler(ILogger<OrderCreatedHandler> logger)
        {
            _logger = logger;
        }

        public Task HandleAsync(OrderCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handled OrderCreatedEvent for Order {OrderId}", domainEvent.OrderId);
            return Task.CompletedTask;
        }
    }
}
