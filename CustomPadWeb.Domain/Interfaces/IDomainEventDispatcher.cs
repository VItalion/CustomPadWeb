using Microsoft.EntityFrameworkCore;

namespace CustomPadWeb.Domain.Interfaces
{
    public interface IDomainEventDispatcher
    {
        Task DispatchDomainEventsAsync(DbContext context);
    }
}
