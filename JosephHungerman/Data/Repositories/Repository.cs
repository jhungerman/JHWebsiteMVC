using System.Linq.Expressions;
using JosephHungerman.Models;
using Microsoft.EntityFrameworkCore;

namespace JosephHungerman.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        private readonly DbSet<TEntity> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _dbSet = context.Set<TEntity>();
        }

        public async Task<IList<TEntity>?> GetAsync(Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                         (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity?> GetFirstAsync(Expression<Func<TEntity, bool>>? filter = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                         (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return await query.AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<TEntity?> GetByIdAsync(object id)
        {
            TEntity? entity = await _dbSet.FindAsync(id);

            return entity;
        }

        public async Task<IList<TEntity>?> AddAllAsync(IList<TEntity> models)
        {
            await _dbSet.AddRangeAsync(models);

            return models;
        }

        public async Task<TEntity?> AddAsync(TEntity model)
        {
            await _dbSet.AddAsync(model);

            return model;
        }

        public Task<IList<TEntity>> UpdateAllAsync(IList<TEntity> models)
        {
            _dbSet.UpdateRange(models);

            return Task.FromResult(models);
        }

        public Task<TEntity> UpdateAsync(TEntity model)
        {
            _dbSet.Update(model);

            return Task.FromResult(model);
        }

        public Task DeleteAllAsync(IList<TEntity> models)
        {
            _dbSet.RemoveRange(models);

            return Task.CompletedTask;
        }

        public Task DeleteAsync(TEntity model)
        {
            _dbSet.Remove(model);

            return Task.CompletedTask;
        }

        public async Task DeleteByIdAsync(object id)
        {
            TEntity? entity = await _dbSet.FindAsync(id);

            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }
    }
}
