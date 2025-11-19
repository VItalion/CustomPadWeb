namespace CustomPadWeb.Domain.DomainEvents
{
    public class OrderCreatedDomainEvent : IDomainEvent
    {
        public Guid OrderId { get; }
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
        public string CustomerEmail { get; set; }

        public OrderCreatedDomainEvent(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
