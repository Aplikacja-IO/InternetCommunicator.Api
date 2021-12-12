using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InternetCommunicator.Infrastructure.Repository
{
    public interface IBaseRepository<TEntity>
    where TEntity : class
    {
        Task<TEntity> GetByIdAsync(int id);

        Task<TEntity> CreateAsync(TEntity entity);

        Task UpdateAsync(int id, TEntity entity);

        Task DeleteAsync(int id);

        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);

        Task<IEnumerable<TEntity>> GetAllAsync();
    }

    public interface IBaseRepository<TEntity, TSearchParams> : IBaseRepository<TEntity>
    where TEntity : class
    where TSearchParams : notnull
    {
        abstract Task<IEnumerable<TEntity>> SearchAsync(TSearchParams searchParams);
    }
}
