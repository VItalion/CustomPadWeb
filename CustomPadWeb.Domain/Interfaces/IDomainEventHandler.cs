using CustomPadWeb.Domain.DomainEvents;

namespace CustomPadWeb.Domain.Interfaces
{
    public interface IDomainEventHandler<in TEvent> where TEvent : IDomainEvent
    {
        Task HandleAsync(TEvent domainEvent);
    }
}
