using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SRCD.Models.Interfaces
{
    public interface IRepositorio<TEntity> 
        where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<int> CreateAsync(TEntity entity);
        Task<int> CountAsync();
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FindByIdAsync(int id);
        Task UpdateAsync(TEntity entity);
        Task Delete(int id);
    }
}
