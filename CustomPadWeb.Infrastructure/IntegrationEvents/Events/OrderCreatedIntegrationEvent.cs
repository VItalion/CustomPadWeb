namespace CustomPadWeb.Infrastructure.IntegrationEvents.Events
{
    public class OrderCreatedIntegrationEvent : IntegrationEvent
    {
        public Guid OrderId { get; }
        public string CustomerEmail { get; }

        public OrderCreatedIntegrationEvent(Guid orderId, string customerEmail)
        {
            OrderId = orderId;
            CustomerEmail = customerEmail;
        }
    }
}
