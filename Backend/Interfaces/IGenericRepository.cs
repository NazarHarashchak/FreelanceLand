using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Backend
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task CreateAsync(TEntity item);
        Task<TEntity> FindByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAsync(object p);
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, int v);
        Task RemoveAsync(TEntity item);
        Task UpdateAsync(TEntity item);
    }
}
