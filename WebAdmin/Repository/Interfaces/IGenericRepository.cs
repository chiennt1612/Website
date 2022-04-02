using EntityFramework.Web.DBContext;
using EntityFramework.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace WebAdmin.Repository.Interfaces
{
    /// <summary>
    /// Generic repository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericRepository<T, TKey> where T : class where TKey : IEquatable<TKey>
    {
        // Marks an entity as new
        Task AddAsync(T entity);

        Task AddManyAsync(IEnumerable<T> entities);

        // Marks an entity as modified
        void Update(T entity);

        void UpdateMany(IEnumerable<T> entities);

        // Marks an entity to be removed
        Task DeleteAsync(TKey id);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);
        void DeleteRange(IEnumerable<T> entities);
        void DeleteRange(params T[] entities);

        Task<IPagedList<T>> GetListByPage(
            Expression<Func<T, bool>> expression, Func<T, object> sort, bool desc = false,
            int pageIndex = 1, int pageSize = Constants.PageSize);
        // Get an entity by int id
        Task<T> GetByIdAsync(TKey id);
        // Get an entity using delegate
        Task<T> GetAsync(Expression<Func<T, bool>> where);
        // Gets all entities of type T
        Task<IEnumerable<T>> GetAllAsync();
        // Gets entities using delegate
        IEnumerable<T> GetMany(Func<T, bool> where);

        Task<BaseEntityList<T>> GetListAsync(Expression<Func<T, bool>> expression, Func<T, object> sort, bool desc, int page, int pageSize);

        Task<int> CountAsync(Expression<Func<T, bool>> expression);
    }

}
