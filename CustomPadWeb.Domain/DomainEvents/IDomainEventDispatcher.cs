using Microsoft.EntityFrameworkCore;

namespace CustomPadWeb.Domain.DomainEvents
{
    public interface IDomainEventDispatcher
    {
        Task DispatchAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default);
    }
}
