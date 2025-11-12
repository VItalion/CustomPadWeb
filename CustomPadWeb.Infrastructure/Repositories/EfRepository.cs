using CustomPadWeb.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CustomPadWeb.Infrastructure.Repositories
{
    public class EfRepository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _db;


        public EfRepository(AppDbContext db) => _db = db;


        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _db.Set<T>().FindAsync(new object?[] { id }, cancellationToken);
        }


        public async Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _db.Set<T>();
            if (predicate is not null)
                query = query.Where(predicate);


            return await query.ToListAsync(cancellationToken);
        }


        public async Task AddAsync(T entity, CancellationToken cancellationToken = default) => await _db.Set<T>().AddAsync(entity, cancellationToken);
        public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default) => await _db.Set<T>().AddRangeAsync(entities, cancellationToken);
        public void Update(T entity) => _db.Set<T>().Update(entity);
        public void Remove(T entity) => _db.Set<T>().Remove(entity);
    }
}
