using CustomPadWeb.ApiService.Entities;
using System.Linq.Expressions;

namespace CustomPadWeb.ApiService.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        TEntity GetById(Guid id);

        Task<TEntity> GetByIdAsync(Guid id);

        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        
        IEnumerable<TEntity> Get<T>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, T>> orderBy);

        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> GetAsync<T>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, T>> orderBy);

        Guid Create(TEntity entity);

        Task<Guid> CreateAsync(TEntity entity);

        void Delete(Guid id);

        Task DeleteAsync(Guid id);

        void Update(TEntity entity);

        Task UpdateAsync(TEntity entity);
    }
}
