using CustomPadWeb.Domain.Entities;

namespace CustomPadWeb.Domain.DomainEvents
{
    public record OrderStatusChangedDomainEvent(Guid OrderId, OrderStatus NewStatus) : IDomainEvent
    {
        public Guid OrderId { get; } = OrderId;

        public OrderStatus NewStatus { get; } = NewStatus;

        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
