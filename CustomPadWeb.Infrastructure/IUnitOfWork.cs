using CustomPadWeb.Domain.Entities;
using CustomPadWeb.Domain.Repositories;

namespace CustomPadWeb.Infrastructure
{
    public interface IUnitOfWork
    {
        IRepository<Order> Orders { get; }
        IRepository<GamepadConfiguration> GamepadConfigurations { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
