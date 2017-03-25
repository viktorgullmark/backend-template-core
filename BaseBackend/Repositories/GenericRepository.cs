using BaseBackend.Contexts;
using BaseBackend.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BaseBackend.Repositories
{
    public abstract class GenericRepository<C, T> : IRepository<T> where C : BaseContext where T : class
    {
        public C DbContext;

        protected GenericRepository()
        {
        }

        protected GenericRepository(C dbContext)
        {
            DbContext = dbContext;
        }

        public IEnumerable<T> List => DbContext.Set<T>().ToList();

        public virtual T SetCreatedDate(T entity)
        {
            var type = entity.GetType();
            var prop = type.GetProperty("Created");

            prop?.SetValue(entity, DateTime.Now, null);

            return entity;
        }

        public virtual T SetModifiedDate(T entity)
        {
            var type = entity.GetType();
            var prop = type.GetProperty("Modified");

            prop?.SetValue(entity, DateTime.Now, null);

            return entity;
        }

        public virtual T SetGuid(T entity)
        {
            var type = entity.GetType();
            var prop = type.GetProperty("Guid");

            prop?.SetValue(entity, Guid.NewGuid(), null);

            return entity;
        }

        public virtual async Task<T> Insert(T entity)
        {
            SetCreatedDate(entity);
            SetModifiedDate(entity);
            SetGuid(entity);

            DbContext.Set<T>().Add(entity);
            await SaveAsync();
            return entity;
        }

        public virtual async Task<bool> SaveAsync()
        {
            return await DbContext.SaveChangesAsync() > 0;
        }

        public virtual async Task<T> Delete(T entity)
        {
            DbContext.Set<T>().Remove(entity);
            SetModifiedDate(entity);
            await SaveAsync();
            return entity;
        }

        public virtual async Task<T> Update(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            SetModifiedDate(entity);
            await SaveAsync();
            return entity;
        }

        public virtual async Task<T> FindAsync(params object[] keyValues)
        {
            return await DbContext.Set<T>().FindAsync(keyValues);
        }
        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await DbContext.Set<T>().FirstOrDefaultAsync(predicate);
        }
        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = DbContext.Set<T>();
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return await query.FirstOrDefaultAsync(predicate);
        }
    }
}
