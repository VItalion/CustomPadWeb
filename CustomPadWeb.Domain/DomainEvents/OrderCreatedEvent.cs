namespace CustomPadWeb.Domain.DomainEvents
{
    public class OrderCreatedEvent : IDomainEvent
    {
        public Guid OrderId { get; }
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
        public string CustomerEmail { get; set; }

        public OrderCreatedEvent(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
