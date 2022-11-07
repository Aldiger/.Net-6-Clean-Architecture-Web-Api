using WebApi.Core.Entities;
using System.Linq.Expressions;

namespace WebApi.Core.Repositories
{
    public interface IRepository<T> : IBaseRepository where T : class, IEntity
    {
        /// <summary>
        /// To add entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Added entity</returns>
        T Add(T entity);
        /// <summary>
        /// To add multiple entities at once
        /// </summary>
        /// <param name="entities">Added entities</param>
        /// <returns></returns>
        IEnumerable<T> AddRange(IEnumerable<T> entities);
        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Updated entity</returns>
        T Update(T entity);
        /// <summary>
        /// Update list of entities at once
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>Updated entity list</returns>
        IEnumerable<T> UpdateRange(IEnumerable<T> entities);

        /// <summary>
        /// Deletes entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>True if action succeed</returns>
        T Delete(T entity);

        /// <summary>
        /// Deletes entity
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>True if action succeed</returns>
        List<T> Delete(List<T> entities);

        /// <summary>
        /// filters and returns list of entities
        /// </summary>
        /// <param name="orderBy"></param>
        /// <param name="skip">Paging parameter</param>
        /// <param name="take">Paging size</param>
        /// <param name="filter"></param>
        /// <returns>Returns list of entities</returns>
        List<T> GetList(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int skip = 0, int take = 0, params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// filters and returns list of entities
        /// </summary>
        /// <param name="orderBy"></param>
        /// <param name="skip">Paging parameter</param>
        /// <param name="take">Paging size</param>
        /// <param name="filter"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int skip = 0, int take = 0, params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Gets single entity 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="includes"></param>
        /// <returns>Filtered entity</returns>
        T Get(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Gets single entity 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="includes"></param>
        /// <returns>Filtered entity</returns>
        Task<T> GetAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Queriable dbSet
        /// </summary>
        /// <returns></returns>
        IQueryable<T> Query();

        Task<long> GetCountAsync(Expression<Func<T, bool>> expression = null);
        long GetCount(Expression<Func<T, bool>> expression = null);
    }
}
