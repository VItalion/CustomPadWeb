using CustomPadWeb.Domain.Entities;
using CustomPadWeb.Domain.Repositories;
using CustomPadWeb.Infrastructure.Repositories;

namespace CustomPadWeb.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;
        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            Orders = new EfRepository<Order>(_db);
            Users = new EfRepository<User>(_db);
            GamepadConfigurations = new EfRepository<GamepadConfiguration>(_db);
        }

        public IRepository<Order> Orders { get; private set; }

        public IRepository<User> Users { get; private set; }

        public IRepository<GamepadConfiguration> GamepadConfigurations { get; private set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => _db.SaveChangesAsync(cancellationToken);
    }
}
