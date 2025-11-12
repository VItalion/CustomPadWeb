using Microsoft.EntityFrameworkCore;

namespace CustomPadWeb.Infrastructure
{
    public interface IDomainEventDispatcher
    {
        Task DispatchDomainEventsAsync(DbContext context);
    }
}
