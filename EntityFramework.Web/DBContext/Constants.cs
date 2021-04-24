using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFramework.Web.DBContext
{
    public class TableConsts
    {
        public const string Product = "Product";
        public const string Article = "Article";
        public const string Categories = "Categories";
        public const string NewsCategories = "NewsCategories";
        public const string MenuMainFooter = "MenuMainFooter";
        public const string MenuSubFooter = "MenuSubFooter";
        public const string ParamSetting = "ParamSetting";
    }
    public class Constants
    {
        public const string Authority = "https://id.bacngoctuan.com:4343/";
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
