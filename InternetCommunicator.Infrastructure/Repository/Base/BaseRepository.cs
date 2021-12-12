using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using InternetCommunicator.Domain.Models;
using InternetCommunicator.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace InternetCommunicator.Infrastructure.Repository
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : class, new()
    {
        protected readonly CommunicatorDbContext DbContext;

        protected BaseRepository(CommunicatorDbContext dbContext)
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

        public virtual async Task UpdateAsync(int id, TEntity entity)
        {
            var entityToUpdate = await GetByIdAsync(id);
            UpdateEntity(entityToUpdate, entity);
            DbContext.Set<TEntity>().Update(entityToUpdate);
            await DbContext.SaveChangesAsync();
        }

        protected virtual TEntity UpdateEntity(TEntity dbEntity, TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entityToDelete = await GetByIdAsync(id);
            DbContext.Set<TEntity>().Remove(entityToDelete);
            await DbContext.SaveChangesAsync()
                .ConfigureAwait(false);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = DbContext.Set<TEntity>();

            if (filter is not null)
                query = query.Where(filter);

            return await (orderBy != null ? orderBy(query) : query)
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await DbContext.Set<TEntity>()
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }
}
