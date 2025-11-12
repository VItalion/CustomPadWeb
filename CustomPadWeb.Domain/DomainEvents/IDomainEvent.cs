namespace CustomPadWeb.Domain.DomainEvents
{
    public interface IDomainEvent
    {
        DateTime OccurredOn { get; }
    }
}