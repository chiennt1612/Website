﻿using EntityFramework.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebClient.Repository.Interfaces
{
    /// <summary>
    /// Generic repository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericRepository<T, TKey> where T : class where TKey : IEquatable<TKey>
    {
        #region write data
        Task AddAsync(T entity);
        Task AddManyAsync(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateMany(IEnumerable<T> entities);
        Task DeleteAsync(TKey id);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        #endregion

        #region read data
        Task<T> GetByIdAsync(TKey id);
        Task<T> GetAsync(Expression<Func<T, bool>> where);
        Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> where);
        Task<IEnumerable<T>> GetAllAsync();
        // Gets list by page
        Task<BaseEntityList<T>> GetListAsync(Expression<Func<T, bool>> expression, Func<T, object> sort, bool desc, int page, int pageSize);
        Task<int> CountAsync(Expression<Func<T, bool>> expression);
        #endregion
    }

}
