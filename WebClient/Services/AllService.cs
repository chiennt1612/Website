using WebClient.Services.Interfaces;

namespace WebClient.Services
{
    /// <inheritdoc />
    /// <summary>
    /// Unit of work class
    /// </summary>
    public class AllService : IAllService
    {
        #region data members
        public IAboutServices aboutServices { get; }
        public IAdvServices advServices { get; }
        public IArticleServices articleServices { get; }
        public ICategoriesServices categoriesServices { get; }
        public INewsCategoriesServices newsCategoriesServices { get; }
        public IMenuMainFooterServices menuMainFooterServices { get; }
        public IMenuSubFooterServices menuSubFooterServices { get; }
        public IParamSettingServices paramSettingServices { get; }
        public IProductServices productServices { get; }
        public IContactServices contactServices { get; }
        public IOrderServices orderServices { get; }
        #endregion

        /// <summary>
        /// Unit of work constructor
        /// </summary>
        /// <param name="appDbContext"></param>
        /// <param name="contextAccessor"></param>
        public AllService(IAboutServices aboutServices,
            IAdvServices advServices,
        IArticleServices articleServices,
        ICategoriesServices categoriesServices,
        INewsCategoriesServices newsCategoriesServices,
        IMenuMainFooterServices menuMainFooterServices,
        IMenuSubFooterServices menuSubFooterServices,
        IParamSettingServices paramSettingServices,
        IProductServices productServices,
        IContactServices contactServices,
        IOrderServices orderServices)
        {
            this.aboutServices = aboutServices;
            this.articleServices = articleServices;
            this.categoriesServices = categoriesServices;
            this.categoriesServices = categoriesServices;
            this.newsCategoriesServices = newsCategoriesServices;
            this.menuMainFooterServices = menuMainFooterServices;
            this.menuSubFooterServices = menuSubFooterServices;
            this.paramSettingServices = paramSettingServices;
            this.productServices = productServices;
            this.contactServices = contactServices;
            this.advServices = advServices;
            this.orderServices = orderServices;
        }
    }

}
