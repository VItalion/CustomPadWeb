namespace CustomPadWeb.Infrastructure.IntegrationEvents
{
    public abstract class IntegrationEvent
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public DateTime OccurredOnUtc { get; private set; } = DateTime.UtcNow;
    }
}
