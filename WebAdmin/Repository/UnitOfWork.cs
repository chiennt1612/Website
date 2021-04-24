using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityFramework.Web.DBContext;
using EntityFramework.Web.Entities;
using WebAdmin.Repository.Interfaces;

namespace WebAdmin.Repository
{
    /// <inheritdoc />
    /// <summary>
    /// Unit of work class
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        #region inject field variables
        private readonly AppDbContext _appDbContext;
        #endregion

        #region data members

        private readonly IHttpContextAccessor _httpContext;
        private IArticleRepository _ArticleRepository ;
        private ICategoriesRepository _CategoriesRepository ;
        private INewsCategoriesRepository _NewsCategoriesRepository ;
        private IMenuMainFooterRepository _MenuMainFooterRepository ;
        private IMenuSubFooterRepository _MenuSubFooterRepository ;
        private IParamSettingRepository _ParamSettingRepository ;
        private IProductRepository _ProductRepository ;
        #endregion

        /// <summary>
        /// Unit of work constructor
        /// </summary>
        /// <param name="appDbContext"></param>
        /// <param name="contextAccessor"></param>
        public UnitOfWork(AppDbContext appDbContext, IHttpContextAccessor contextAccessor)
        {
            _appDbContext = appDbContext;
            _httpContext = contextAccessor;
        }

        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        #region Properties
        /// <summary>
        /// Get AppDbContext
        /// </summary>
        public AppDbContext AppDbContext => _appDbContext;

        /// <summary>
        /// Get UserRepository
        /// </summary>
        public IArticleRepository articleRepository
        {
            get
            {
                return _ArticleRepository = _ArticleRepository ?? new ArticleRepository(_appDbContext, _httpContext);
            }
        }

        public ICategoriesRepository categoriesRepository
        {
            get
            {
                return _CategoriesRepository = _CategoriesRepository ?? new CategoriesRepository(_appDbContext, _httpContext);
            }
        }

        public INewsCategoriesRepository newsCategoriesRepository
        {
            get
            {
                return _NewsCategoriesRepository = _NewsCategoriesRepository ?? new NewsCategoriesRepository(_appDbContext, _httpContext);
            }
        }

        public IMenuMainFooterRepository menuMainFooterRepository
        {
            get
            {
                return _MenuMainFooterRepository = _MenuMainFooterRepository ?? new MenuMainFooterRepository(_appDbContext, _httpContext);
            }
        }

        public IMenuSubFooterRepository menuSubFooterRepository
        {
            get
            {
                return _MenuSubFooterRepository = _MenuSubFooterRepository ?? new MenuSubFooterRepository(_appDbContext, _httpContext);
            }
        }

        public IParamSettingRepository paramSettingRepository
        {
            get
            {
                return _ParamSettingRepository = _ParamSettingRepository ?? new ParamSettingRepository(_appDbContext, _httpContext);
            }
        }

        public IProductRepository productRepository
        {
            get
            {
                return _ProductRepository = _ProductRepository ?? new ProductRepository(_appDbContext, _httpContext);
            }
        }

        /// <summary>
        /// Save
        /// </summary>
        public void Save()
        {
            _appDbContext.SaveChanges();
        }
        /// <summary>
        /// Save Async
        /// </summary>
        public async Task SaveAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }
        #endregion

        #region dispose
        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _appDbContext.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

}
