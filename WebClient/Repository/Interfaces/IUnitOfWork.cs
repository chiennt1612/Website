using System;
using System.Threading.Tasks;

namespace WebClient.Repository.Interfaces
{
    /// <summary>
    /// Unit of work interface
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IAboutRepository aboutRepository { get; }
        IArticleRepository articleRepository { get; }
        ICategoriesRepository categoriesRepository { get; }
        INewsCategoriesRepository newsCategoriesRepository { get; }
        IMenuMainFooterRepository menuMainFooterRepository { get; }
        IMenuSubFooterRepository menuSubFooterRepository { get; }
        IParamSettingRepository paramSettingRepository { get; }
        IProductRepository productRepository { get; }
        IAdvRepository advRepository { get; }
        IContactRepository contactRepository { get; }

        IOrderRepository orderRepository { get; }
        IOrderItemRepository orderItemRepository { get; }
        IOrderStatusRepository orderStatusRepository { get; }

        void Save();
        Task SaveAsync();
    }

}
