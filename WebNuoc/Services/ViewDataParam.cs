using EntityFramework.Web.Entities;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using WebNuoc.Models.Home;
using WebNuoc.Services.Interfaces;

namespace WebNuoc.Services
{
    public static class ViewDataParam //: IViewDataParam
    {
        //private ILogger<ViewDataParam> logger;
        //public ViewDataDictionary viewData { get; set; }
        //private IAllService _Service;
        //public ViewDataParam
        public static async System.Threading.Tasks.Task SetViewData
            (ILogger logger, ViewDataDictionary viewData, IAllService _Service, WebNuoc.Models.Home.PathOfPage path = default, string _Notification = "")
        {
            //this.logger = logger;
            //this.viewData = viewData;
            //this._Service = _Service;
            viewData[WebNuoc.Services.ViewDataParam.PageOfPage] = path;
            if (viewData[ViewDataParam.Notification] == null)
            {
                viewData[ViewDataParam.Notification] = _Notification;
                logger.LogInformation($"Setting Notification");
            }
            if (viewData[ViewDataParam.SearchInput] == null)
            {
                viewData[ViewDataParam.SearchInput] = new SearchInput();
                logger.LogInformation($"Setting SearchInput");
            }
            //if (viewData[ViewDataParam.AboutList] == null)
            //{
            //    viewData[ViewDataParam.AboutList] = await _Service.aboutServices.GetAllAsync();
            //    logger.LogInformation($"Setting AboutList");
            //}
            //else
            //{
            //    logger.LogInformation($"Not Setting about list");
            //}

            if (viewData[ViewDataParam.ProductGroup] == null)
            {
                viewData[ViewDataParam.ProductGroup] = await _Service.categoriesServices.GetAllAsync();
                logger.LogInformation($"Setting Product group");
            }
            else
            {
                logger.LogInformation($"Not Setting product group");
            }

            //if (viewData[ViewDataParam.NewsGroup] == null)
            //{
            //    viewData[ViewDataParam.NewsGroup] = await _Service.newsCategoriesServices.GetAllAsync();
            //    logger.LogInformation($"Setting News group");
            //}
            //else
            //{
            //    logger.LogInformation($"Not Setting news group");
            //}

            //if (viewData[ViewDataParam.MenuMainFooter] == null)
            //{
            //    viewData[ViewDataParam.MenuMainFooter] = await _Service.menuMainFooterServices.GetAllAsync();
            //    logger.LogInformation($"Setting Main menu footer");
            //}
            //else
            //{
            //    logger.LogInformation($"Not Setting main menu footer");
            //}

            //if (viewData[ViewDataParam.MenuSubFooter] == null)
            //{
            //    viewData[ViewDataParam.MenuSubFooter] = await _Service.menuSubFooterServices.GetAllAsync();
            //    logger.LogInformation($"Setting Sub menu footer");
            //}
            //else
            //{
            //    logger.LogInformation($"Not Setting sub menu footer");
            //}

            if (viewData[ViewDataParam.ParamSetting] == null)
            {
                viewData[ViewDataParam.ParamSetting] = await _Service.paramSettingServices.GetAllAsync();
                logger.LogInformation($"Setting Param setting");
            }
            else
            {
                logger.LogInformation($"Not Setting Param setting");
            }
        }
        //public void setViewData()
        //{
        //    if (viewData[ViewDataParam.AboutList] == null)
        //    {
        //        viewData[ViewDataParam.AboutList] = _Service.aboutServices.GetAllAsync().GetAwaiter();
        //        logger.LogInformation($"Setting AboutList");
        //    }
        //    else
        //    {
        //        logger.LogInformation($"Not Setting about list");
        //    }

        //    if (viewData[ViewDataParam.ProductGroup] == null)
        //    {
        //        viewData[ViewDataParam.ProductGroup] = _Service.categoriesServices.GetAllAsync().GetAwaiter();
        //        logger.LogInformation($"Setting Product group");
        //    }
        //    else
        //    {
        //        logger.LogInformation($"Not Setting product group");
        //    }

        //    if (viewData[ViewDataParam.NewsGroup] == null)
        //    {
        //        viewData[ViewDataParam.NewsGroup] = _Service.newsCategoriesServices.GetAllAsync().GetAwaiter();
        //        logger.LogInformation($"Setting News group");
        //    }
        //    else
        //    {
        //        logger.LogInformation($"Not Setting news group");
        //    }

        //    if (viewData[ViewDataParam.MenuMainFooter] == null)
        //    {
        //        viewData[ViewDataParam.MenuMainFooter] = _Service.menuMainFooterServices.GetAllAsync().GetAwaiter();
        //        logger.LogInformation($"Setting Main menu footer");
        //    }
        //    else
        //    {
        //        logger.LogInformation($"Not Setting main menu footer");
        //    }

        //    if (viewData[ViewDataParam.MenuSubFooter] == null)
        //    {
        //        viewData[ViewDataParam.MenuSubFooter] = _Service.menuSubFooterServices.GetAllAsync().GetAwaiter();
        //        logger.LogInformation($"Setting Sub menu footer");
        //    }
        //    else
        //    {
        //        logger.LogInformation($"Not Setting sub menu footer");
        //    }

        //    if (viewData[ViewDataParam.ParamSetting] == null)
        //    {
        //        viewData[ViewDataParam.ParamSetting] = _Service.paramSettingServices.GetAllAsync().GetAwaiter();
        //        logger.LogInformation($"Setting Param setting");
        //    }
        //    else
        //    {
        //        logger.LogInformation($"Not Setting Param setting");
        //    }
        //}

        public static string GenProductMenuMain(long? Id, IList<Categories> Cate)
        {
            long _Id = (Id.HasValue ? Id.Value : 0);
            string menu = "";


            return menu;
        }

        public static string ProductGroup = "ProductGroup";
        public static string NewsGroup = "NewsGroup";
        public static string AboutList = "AboutList";
        public static string MenuMainFooter = "MenuMainFooter";
        public static string MenuSubFooter = "MenuSubFooter";
        public static string ParamSetting = "ParamSetting";

        public static string SearchInput = "SearchInput";
        public static string Notification = "Notification";
        public static string PageOfPage = "PageOfPage";
    }
}
