using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TimeTrackerBackend.Interfaces
{
    public interface IGenericRepository<TEntity>
    where TEntity : class
    {
        Task<IList<TEntity>> List();
        Task<TEntity> Insert(TEntity entity);
        Task<TEntity> Delete(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task<bool> SaveAsync();
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
