using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using InternetCommunicator.Domain.Models;

namespace InternetCommunicator.Infrastructure.Repository.Base
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : class, new()
    {
        protected readonly CommunicatorDBContext DbContext;

        protected BaseRepository(CommunicatorDBContext dbContext)
        {
            DbContext = dbContext;
        }


        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await DbContext.Set<TEntity>()
                .FindAsync(id)
                .ConfigureAwait(false);
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            var createdUser = await DbContext.Set<TEntity>().AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return createdUser.Entity;
        }

        public async Task UpdateAsync(int id, TEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>> orderBy = null)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
