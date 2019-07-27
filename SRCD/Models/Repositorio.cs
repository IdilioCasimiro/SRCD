using SRCD.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SRCD.Models
{
    public class Repositorio<TEntity> : IRepositorio<TEntity>
        where TEntity : class
    {
        private readonly ApplicationDbContext dbContext;

        public Repositorio(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<int> CreateAsync(TEntity entity)
        {
            dbContext.Set<TEntity>().Add(entity);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> CountAsync()
        {
            return await dbContext.Set<TEntity>().CountAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await dbContext.Set<TEntity>().FindAsync(id);
            if (entity != null)
            {
                dbContext.Set<TEntity>().Remove(entity);
                await dbContext.SaveChangesAsync();
            }
            else
                throw new ApplicationException("Entidade não encontrada!");

        }

        public async Task<TEntity> FindByIdAsync(int id)
        {
            return await dbContext.Set<TEntity>().FindAsync(id);
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return dbContext.Set<TEntity>().Where(predicate).AsNoTracking();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dbContext.Set<TEntity>().ToListAsync();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            dbContext.Set<TEntity>().Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }
    }
}
