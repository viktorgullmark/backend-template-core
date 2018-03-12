using TimeTrackerBackend.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TimeTrackerBackend.Models;

namespace TimeTrackerBackend.Repositories
{

    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : class
    {
        private readonly BaseDbContext _dbContext;

        public GenericRepository(BaseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IList<TEntity>> List()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public virtual TEntity SetCreatedDate(TEntity entity)
        {
            var type = entity.GetType();
            var prop = type.GetProperty("Created");

            prop?.SetValue(entity, DateTime.Now, null);

            return entity;
        }

        public virtual TEntity SetModifiedDate(TEntity entity)
        {
            var type = entity.GetType();
            var prop = type.GetProperty("Modified");

            prop?.SetValue(entity, DateTime.Now, null);

            return entity;
        }

        public virtual TEntity SetGuid(TEntity entity)
        {
            var type = entity.GetType();
            var prop = type.GetProperty("Guid");

            prop?.SetValue(entity, Guid.NewGuid(), null);

            return entity;
        }

        public virtual async Task<TEntity> Insert(TEntity entity)
        {
            SetCreatedDate(entity);
            SetModifiedDate(entity);
            SetGuid(entity);

            _dbContext.Set<TEntity>().Add(entity);
            await SaveAsync();
            return entity;
        }

        public virtual async Task<bool> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public virtual async Task<TEntity> Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            SetModifiedDate(entity);
            await SaveAsync();
            return entity;
        }

        public virtual async Task<TEntity> Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            SetModifiedDate(entity);
            await SaveAsync();
            return entity;
        }

        public virtual async Task<TEntity> FindAsync(params object[] keyValues)
        {
            return await _dbContext.Set<TEntity>().FindAsync(keyValues);
        }
        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }
        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return await query.FirstOrDefaultAsync(predicate);
        }
    }
}
