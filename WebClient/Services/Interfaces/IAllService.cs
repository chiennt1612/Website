namespace WebClient.Services.Interfaces
{
    public interface IAllService
    {
        IAboutServices aboutServices { get; }
        IAdvServices advServices { get; }
        IArticleServices articleServices { get; }
        ICategoriesServices categoriesServices { get; }
        INewsCategoriesServices newsCategoriesServices { get; }
        IMenuMainFooterServices menuMainFooterServices { get; }
        IMenuSubFooterServices menuSubFooterServices { get; }
        IParamSettingServices paramSettingServices { get; }
        IProductServices productServices { get; }
        IContactServices contactServices { get; }
        IOrderServices orderServices { get; }
    }

}
