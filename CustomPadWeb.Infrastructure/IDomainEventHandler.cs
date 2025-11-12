using CustomPadWeb.Domain.DomainEvents;

namespace CustomPadWeb.Infrastructure
{
    public interface IDomainEventHandler<in TEvent> where TEvent : IDomainEvent
    {
        Task HandleAsync(TEvent domainEvent);
    }
}
