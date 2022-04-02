using EntityFramework.Web.DBContext;
using EntityFramework.Web.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAdmin.Repository.Interfaces;
using X.PagedList;

namespace WebAdmin.Repository
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
        /// <summary>
        /// Add T entity
        /// </summary>
        /// <param name="entity"></param>
        public virtual async Task AddAsync(T entity)
        {
            SetCreated(entity);
            await _dbSet.AddAsync(entity);
        }

        public async Task AddManyAsync(IEnumerable<T> entities)
        {
            foreach (var item in entities)
            {
                SetCreated(item);
            }

            await _dbSet.AddRangeAsync(entities);
        }

        /// <summary>
        /// Update T entity
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(T entity)
        {
            SetUpdated(entity);
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void UpdateMany(IEnumerable<T> entities)
        {
            foreach (var item in entities)
            {
                SetUpdated(item);
            }

            _dbSet.AttachRange(entities);
            _dbContext.Entry(entities).State = EntityState.Modified;
        }

        public async Task DeleteAsync(TKey id)
        {
            var a = await _dbSet.FindAsync(id);
            Delete(a);
        }

        /// <summary>
        /// Delete T entity
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(T entity)
        {
            //_dbSet.Remove(entity);
            SetDeleted(entity);
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual async Task<IPagedList<T>> GetListByPage(
            Expression<Func<T, bool>> expression, Func<T, object> sort, bool desc = false,
            int pageIndex = 1, int pageSize = Constants.PageSize)
        {
            IQueryable<T> r = _dbSet.AsNoTracking().Where(expression);
            IOrderedEnumerable<T> r1;
            if (!desc)
            {
                r1 = r.OrderBy(sort);
            }
            else
            {
                r1 = r.OrderByDescending(sort);
            }
            return await r1.ToPagedListAsync(pageIndex, pageSize);
        }

        /// <summary>
        /// Delete a list entities
        /// </summary>
        /// <param name="entities"></param>
        public virtual void DeleteRange(IEnumerable<T> entities)
        {
            //_dbSet.RemoveRange(entities);
            foreach (var item in entities)
            {
                SetDeleted(item);
            }

            _dbSet.AttachRange(entities);
            _dbContext.Entry(entities).State = EntityState.Modified;
        }

        /// <summary>
        /// Delete a list entities
        /// </summary>
        /// <param name="entities"></param>
        public virtual void DeleteRange(params T[] entities)
        {
            //_dbSet.RemoveRange(entities);
            foreach (var item in entities)
            {
                SetDeleted(item);
            }

            _dbSet.AttachRange(entities);
            _dbContext.Entry(entities).State = EntityState.Modified;
        }
        /// <summary>
        /// Delete T entity by condition
        /// </summary>
        /// <param name="where"></param>
        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = _dbSet.Where(where).AsEnumerable();
            foreach (T obj in objects)
            {
                //_dbSet.Remove(obj);
                Delete(obj);
            }
        }

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
            int skipRows = (page - 1) * pageSize;
            if (desc)
                a.list = (await _dbSet.AsNoTracking().Where(expression).ToListAsync()).OrderByDescending(sort).Skip(skipRows).Take(pageSize).ToList();
            else
                a.list = (await _dbSet.AsNoTracking().Where(expression).ToListAsync()).OrderBy(sort).Skip(skipRows).Take(pageSize).ToList();
            return a;
        }

        /// <summary>
        /// Get many T entity by condition
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetMany(Func<T, bool> where)
        {
            return _dbSet.AsNoTracking().Where(where);
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

        public async Task<int> CountAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AsNoTracking().CountAsync(expression);
        }

        #endregion

        #region Method to check common fields existed
        /// <summary>
        /// Check model have a specific property
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        protected bool HasProperty(string property)
        {
            return _type.GetProperty(property) != null;
        }

        protected void SetProperty(T entity, string property, object value)
        {
            if (value != null)
            {
                Guid a;
                if (Guid.TryParse(value.ToString(), out a))
                {
                    entity.GetType().GetProperty(property).SetValue(entity, a);
                    return;
                }

                entity.GetType().GetProperty(property).SetValue(entity, value);
            }
        }

        protected void SetCreated(T entity)
        {
            if (HasProperty(Constants.CommonFields.CreatedBy))
            {
                var accountId = _httpContext?.User?.Claims?
                    .FirstOrDefault(m => m.Type == ClaimTypes.NameIdentifier)?.Value;

                if (!string.IsNullOrEmpty(accountId))
                {
                    SetProperty(entity, Constants.CommonFields.CreatedBy, long.Parse(accountId));
                }
            }
            if (HasProperty(Constants.CommonFields.CreatedOn))
            {
                SetProperty(entity, Constants.CommonFields.CreatedOn, DateTime.UtcNow);
            }

            if (HasProperty(Constants.CommonFields.IsDeleted))
            {
                SetProperty(entity, Constants.CommonFields.IsDeleted, false);
            }
        }

        protected void SetUpdated(T entity)
        {
            if (HasProperty(Constants.CommonFields.UpdatedBy))
            {
                var accountId = _httpContext?.User?.Claims?
                    .FirstOrDefault(m => m.Type == ClaimTypes.NameIdentifier)?.Value;

                if (!string.IsNullOrEmpty(accountId))
                {
                    SetProperty(entity, Constants.CommonFields.UpdatedBy, long.Parse(accountId));
                }
            }
            if (HasProperty(Constants.CommonFields.UpdatedOn))
            {
                SetProperty(entity, Constants.CommonFields.UpdatedOn, DateTime.UtcNow);
            }
        }

        protected void SetDeleted(T entity)
        {
            if (HasProperty(Constants.CommonFields.UserDeleted))
            {
                var accountId = _httpContext?.User?.Claims?
                    .FirstOrDefault(m => m.Type == ClaimTypes.NameIdentifier)?.Value;

                if (!string.IsNullOrEmpty(accountId))
                {
                    SetProperty(entity, Constants.CommonFields.UserDeleted, long.Parse(accountId));
                }
            }
            if (HasProperty(Constants.CommonFields.DateDeleted))
            {
                SetProperty(entity, Constants.CommonFields.DateDeleted, DateTime.UtcNow);
            }
            if (HasProperty(Constants.CommonFields.IsDeleted))
            {
                SetProperty(entity, Constants.CommonFields.IsDeleted, true);
            }
        }

        #endregion
    }

}
