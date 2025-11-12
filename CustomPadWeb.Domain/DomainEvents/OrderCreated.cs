namespace CustomPadWeb.Domain.DomainEvents
{
    public record OrderCreated(Guid OrderId, Guid UserId) : IDomainEvent
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
