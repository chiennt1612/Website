namespace EntityFramework.Web.DBContext
{
    public class TableConsts
    {
        public const string Order = "_Order";
        public const string OrderItem = "_OrderItem";
        public const string Address = "_Address";
        public const string OrderStatus = "_OrderStatus";

        public const string About = "About";
        public const string Service = "Service";
        public const string FAQ = "FAQ";
        public const string Contact = "Contact";
        public const string Product = "Product";
        public const string Article = "Article";
        public const string Categories = "Categories";
        public const string NewsCategories = "NewsCategories";
        public const string MenuMainFooter = "MenuMainFooter";
        public const string MenuSubFooter = "MenuSubFooter";
        public const string ParamSetting = "ParamSetting";
        public const string InvoiceSave = "InvoiceSave";
        public const string Adv = "Adv";
        public const string AdvPosition = "AdvPosition";
    }
    public class Constants
    {
        public const string Authority = "https://id.bacngoctuan.com";//"https://localhost:5001";//
        public const int PageSize = 10;
        public class CommonFields
        {
            public const string CreatedBy = "UserCreator";
            public const string CreatedOn = "DateCreator";
            public const string UpdatedBy = "UserModify";
            public const string UpdatedOn = "DateModify";
            public const string IsDeleted = "IsDeleted";
            public const string UserDeleted = "UserDeleted";
            public const string DateDeleted = "DateDeleted";
        }
    }
}
