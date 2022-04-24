using System.Linq.Expressions;
using JosephHungerman.Data.Models;

namespace JosephHungerman.Data.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        Task<IList<TEntity>?> GetAsync(Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "");
        Task<TEntity?> GetFirstAsync(Expression<Func<TEntity, bool>>? filter = null,
            string includeProperties = "");
        Task<TEntity?> GetByIdAsync(object id);
        Task<IList<TEntity>?> AddAllAsync(IList<TEntity> models);
        Task<TEntity?> AddAsync(TEntity model);
        Task<IList<TEntity>> UpdateAllAsync(IList<TEntity> models);
        Task<TEntity> UpdateAsync(TEntity model);
        Task DeleteAllAsync(IList<TEntity> models);
        Task DeleteAsync(TEntity model);
        Task DeleteByIdAsync(object id);
    }
}
