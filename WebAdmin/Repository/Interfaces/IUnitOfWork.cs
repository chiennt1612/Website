﻿using System;
using System.Threading.Tasks;

namespace WebAdmin.Repository.Interfaces
{
    /// <summary>
    /// Unit of work interface
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IAboutRepository aboutRepository { get; }
        IFAQRepository fAQRepository { get; }
        IServiceRepository serviceRepository { get; }
        IArticleRepository articleRepository { get; }
        ICategoriesRepository categoriesRepository { get; }
        INewsCategoriesRepository newsCategoriesRepository { get; }
        IMenuMainFooterRepository menuMainFooterRepository { get; }
        IMenuSubFooterRepository menuSubFooterRepository { get; }
        IParamSettingRepository paramSettingRepository { get; }
        IProductRepository productRepository { get; }
        IContactRepository contactRepository { get; }

        IAdvRepository advRepository { get; }
        IAdvPositionRepository advPositionRepository { get; }

        IOrderRepository orderRepository { get; }
        IAddressRepository addressRepository { get; }
        IOrderStatusRepository orderStatusRepository { get; }

        void Save();
        Task SaveAsync();
    }

}
