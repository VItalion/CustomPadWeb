using CustomPadWeb.Domain.Entities;

namespace CustomPadWeb.Domain.DomainEvents
{
    public record OrderStatusChanged(Guid OrderId, OrderStatus NewStatus) : IDomainEvent
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
