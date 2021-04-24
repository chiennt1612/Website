using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityFramework.Web.Entities;

namespace WebAdmin.Repository.Interfaces
{
    /// <summary>
    /// Unit of work interface
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IArticleRepository articleRepository { get; }
        ICategoriesRepository categoriesRepository { get; }
        INewsCategoriesRepository newsCategoriesRepository { get; }
        IMenuMainFooterRepository menuMainFooterRepository { get; }
        IMenuSubFooterRepository menuSubFooterRepository { get; }
        IParamSettingRepository paramSettingRepository { get; }
        IProductRepository productRepository { get; }

        void Save();
        Task SaveAsync();
    }

}
