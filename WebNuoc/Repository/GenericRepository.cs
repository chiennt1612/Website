﻿using EntityFramework.Web.DBContext;
using EntityFramework.Web.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebNuoc.Repository.Interfaces;

namespace WebNuoc.Repository
{
    /// <inheritdoc />
    /// <summary>
    /// Generic repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class GenericRepository<T, TKey> : IGenericRepository<T, TKey> where T : class where TKey : IEquatable<TKey>
    {
        #region Properties
        private readonly Type _type;
        private readonly HttpContext _httpContext;
        private readonly AppDbContext _dbContext;
        private readonly DbSet<T> _dbSet;
        #endregion

        /// <summary>
        /// GenericRepository constructor
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="contextAccessor"></param>
        protected GenericRepository(AppDbContext dbContext, IHttpContextAccessor contextAccessor)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
            _httpContext = contextAccessor?.HttpContext;
            _type = typeof(T);
        }

        #region Implementation
        #region write data
        public virtual async Task<T> AddAsync(T entity)
        {
            var a = await _dbSet.AddAsync(entity);
            return a.Entity;
        }
        public async Task AddManyAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }
        public virtual void Update(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
        public virtual void UpdateMany(IEnumerable<T> entities)
        {
            _dbSet.AttachRange(entities);
            _dbContext.Entry(entities).State = EntityState.Modified;
        }
        public async Task DeleteAsync(TKey id)
        {
            var a = await _dbSet.FindAsync(id);
            Delete(a);
        }
        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
        public virtual void DeleteRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }
        #endregion

        #region read data
        /// <summary>
        /// Get T entity by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<T> GetByIdAsync(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        /// <summary>
        /// Get all entities by some condition
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public virtual async Task<BaseEntityList<T>> GetListAsync(Expression<Func<T, bool>> expression, Func<T, object> sort, bool desc, int page, int pageSize)
        {
            var a = new BaseEntityList<T>();
            a.totalRecords = await CountAsync(expression);
            a.page = page;
            a.pageSize = pageSize;

            int skipRows = (page - 1) * pageSize;
            if (desc)
                a.list = (await _dbSet.AsNoTracking().Where(expression).ToListAsync()).OrderByDescending(sort).Skip(skipRows).Take(pageSize);
            else
                a.list = (await _dbSet.AsNoTracking().Where(expression).ToListAsync()).OrderBy(sort).Skip(skipRows).Take(pageSize);
            return a;
        }

        /// <summary>
        /// Get T entity by condition
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual async Task<T> GetAsync(Expression<Func<T, bool>> where)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(where);
        }

        public virtual async Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> where)
        {
            return await _dbSet.AsNoTracking().Where(where).ToListAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AsNoTracking().CountAsync(expression);
        }
        #endregion
        #endregion
    }

}
