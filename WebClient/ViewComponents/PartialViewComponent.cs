using EntityFramework.Web.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Paygate.OnePay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebClient.Helpers;
using WebClient.Models.Order;
using WebClient.Models.Partial;
using WebClient.Services.Interfaces;

namespace WebClient.ViewComponents
{
    public class PartialViewComponent : ViewComponent
    {
        private readonly ILogger<PartialViewComponent> _logger;
        private readonly IAllService _Service;
        private readonly IStringLocalizer<PartialViewComponent> _localizer;
        private readonly string requestCulture;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PartialViewComponent(IHttpContextAccessor _httpContextAccessor, ILogger<PartialViewComponent> _logger, IAllService _Service, IStringLocalizer<PartialViewComponent> _localizer)
        {
            this._httpContextAccessor = _httpContextAccessor;
            this._logger = _logger;
            this._Service = _Service;
            this._localizer = _localizer;
            this._logger.LogInformation($"Start PartialViewComponent at: {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}");
            requestCulture = this._httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
        }

        public async Task<IViewComponentResult> InvokeAsync(string _PartialName, long? Id, int? page, int? pageSize)
        {
            long _id = (Id.HasValue ? Id.Value : 0);
            int _page = (page.HasValue ? page.Value : 1);
            int _pageSize = (pageSize.HasValue ? pageSize.Value : 7);
            switch (_PartialName)
            {
                #region Order
                case "_RightElement":
                case "_ViewCareMobile":
                case "_RightElenmentMobile":
                    var order1 = PartialGetOrderInfo();
                    _logger.LogInformation($"Partial {_PartialName} ");
                    return View(_PartialName, order1);
                #endregion

                #region All                
                //case "_PathOfPage":
                //case "_LeftElementMobile":
                //case "_LeftElement":
                //    var lem = await PartialLeftElement();
                //    _logger.LogInformation($"Partial {_PartialName} model {lem}");
                //    return View(_PartialName, lem);
                case "_HeaderPolicy":
                    var a = await PartialHeaderPolicy();
                    _logger.LogInformation($"Partial {_PartialName} model {a}");
                    return View(_PartialName, a);
                case "_HeaderContact":
                    var b = await PartialHeaderContact();
                    _logger.LogInformation($"Partial {_PartialName} model {b}");
                    return View(_PartialName, b);
                case "_AboutList":
                    var c = await PartialAboutList();
                    _logger.LogInformation($"Partial {_PartialName} model {c.Count()}");
                    return View(_PartialName, c);
                case "_CateFooter":
                    var d = await PartialCateFooter();
                    _logger.LogInformation($"Partial {_PartialName} model {d.Count()}");
                    return View(_PartialName, d);
                case "_CompanyInfo":
                    var e = await PartialCompanyInfo();
                    _logger.LogInformation($"Partial {_PartialName} model {e.Count()}");
                    return View(_PartialName, e);
                case "_MainMenuLeft":
                case "_MenuProductWithId1":
                case "_MenuProductWithId2":
                case "_MenuProductWithId3":
                case "_MainMenuHeader":
                case "_MainMenuMobile":
                    var f = await PartialMenuProduct();
                    _logger.LogInformation($"Partial {_PartialName} model {f.Count()}");
                    return View(_PartialName, f);
                case "PartialBannerRight":
                    var h11 = await PartialBannerRightInfo();
                    _logger.LogInformation($"Partial {_PartialName} model {h11.Count()}");
                    return View(_PartialName, h11);
                #endregion All

                // Home page -------------------------------------------------
                #region Home page
                case "PartialBannerHome":
                    var h1 = await PartialBannerHomeInfo();
                    _logger.LogInformation($"Partial {_PartialName} model {h1.Count()}");
                    return View(_PartialName, h1);
                case "PartialFlashSale":
                    var h2 = await PartialFlashSaleInfo();
                    _logger.LogInformation($"Partial {_PartialName} model {h2.Count()}");
                    return View(_PartialName, h2);
                case "PartialNewsHome":
                    var h3 = await PartialNewHomeInfo();
                    _logger.LogInformation($"Partial {_PartialName} model {h3.Count()}");
                    return View(_PartialName, h3);
                case "PartialProductHome":
                    var h4 = await PartialProductHomeInfo();
                    _logger.LogInformation($"Partial {_PartialName} model {h4.Count()}");
                    return View(_PartialName, h4);
                case "PartialProductNew":
                    var h5 = await PartialProductNewInfo();
                    _logger.LogInformation($"Partial {_PartialName} model {h5.Count()}");
                    return View(_PartialName, h5);
                // End Home page -------------------------------------------------
                #endregion

                // New page -------------------------------------------------
                #region New page
                case "NewHomeLastest":
                    var n1 = await PartialNewHomeInfo();
                    _logger.LogInformation($"Partial {_PartialName} model {n1.Count()}");
                    return View(_PartialName, n1);
                case "NewByCate":
                    var n2 = await PartialNewByCateInfo();
                    _logger.LogInformation($"Partial {_PartialName} model {n2.Count()}");
                    return View(_PartialName, n2);
                case "NewList":
                    var n3 = await PartialNewListInfo(_id);
                    _logger.LogInformation($"Partial {_PartialName} model {n3.Count()}");
                    return View(_PartialName, n3);
                case "NewListByPage":
                    var n4 = await PartialNewListByPageInfo(_id, _page, _pageSize);
                    _logger.LogInformation($"Partial {_PartialName} ");
                    return View(_PartialName, n4);
                    // End New page -------------------------------------------------
                    #endregion
            }
            _logger.LogInformation($"Partial {_PartialName} is not found model Non");
            return View("Index", _PartialName);
        }

        #region Partial Function Newpage
        private async Task<IList<Article>> PartialNewHomeInfo()
        {
            Expression<Func<Article, bool>> sqlWhere = u => (u.Id == 4 || u.Id == 5 || u.Id == 6);
            var b = (await _Service.articleServices.GetTopAsync(sqlWhere, 5)).OrderBy(u => u.Id).ToList();

            return b;
        }

        private async Task<IEnumerable<NewsCategories>> PartialNewByCateInfo()
        {
            var b = await _Service.articleServices.CateGetAllAsync();

            return b;
        }

        private async Task<IEnumerable<Article>> PartialNewListInfo(long _id)
        {
            Expression<Func<Article, bool>> sqlWhere = u => (u.CategoryMain == _id || _id == 0);
            var b = (await _Service.articleServices.GetTopAsync(sqlWhere, 3));

            return b;
        }

        private async Task<BaseEntityList<Article>> PartialNewListByPageInfo(long _id, int page, int pageSize)
        {
            Func<Article, object> sqlOrder = s => s.Id;
            Expression<Func<Article, bool>> sqlWhere = u => (u.CategoryMain == _id || _id == 0);
            var a = await _Service.articleServices.GetListAsync(sqlWhere, sqlOrder, true, page, pageSize);
            a.page = page;
            a.pageSize = pageSize;
            a.CategoryId = _id;
            return a;
        }

        private async Task<IEnumerable<Adv>> PartialBannerRightInfo()
        {
            var b = (await _Service.advServices.GetAllAsync()).Where(u => u.Pos == 15);

            return b;
        }
        #endregion

        #region Partial Function Homepage
        private async Task<IEnumerable<Adv>> PartialBannerHomeInfo()
        {
            var b = (await _Service.advServices.GetAllAsync()).Where(u => u.Pos == 91);

            return b;
        }

        private async Task<IEnumerable<Product>> PartialFlashSaleInfo()
        {
            Expression<Func<Product, bool>> sqlWhere = u => (u.IsSale);//(u.Discount > 0)
            var b = (await _Service.productServices.GetTopAsync(sqlWhere, 5));

            return b;
        }

        private async Task<IEnumerable<Product>> PartialProductNewInfo()
        {
            Expression<Func<Product, bool>> sqlWhere = u => (u.IsNews);
            var b = (await _Service.productServices.GetTopAsync(sqlWhere, 5));

            return b;
        }

        private async Task<IList<Categories>> PartialProductHomeInfo()
        {
            var b = (await _Service.categoriesServices.GetAllAsync()).Where(u => u.DisplayOnHome > 0).OrderBy(u => u.DisplayOnHome).ToList();

            return b;
        }

        //private async Task<IEnumerable<long>> GetChildId (long CateId)
        //{

        //}
        #endregion

        #region Partial Function
        //private async Task<SearchInput> PartialLeftElement()
        //{
        //    var a = _httpContextAccessor.HttpContext.GetSearchInput();
        //    if(a == null)
        //    {
        //        var b = await _Service.paramSettingServices.GetAllAsync();
        //        a = new SearchInput();
        //        a.categories = (await _Service.categoriesServices.GetAllAsync())
        //            .Select<Categories, CategoryModel>(a => new CategoryModel()
        //            {
        //                Id = a.Id,
        //                Name = a.Name,
        //                Img = a.Img,
        //                ParentId = a.ParentId,
        //                DisplayOnHome = a.DisplayOnHome,
        //                DisplayOnMenuMain = a.DisplayOnMenuMain,
        //                Status = a.Status
        //            });
        //        a.MINPRICE = double.Parse(b.Where(u => (u.Language == requestCulture || u.Language == "all") && u.ParamKey == "PRIREMIN").FirstOrDefault().ParamValue);
        //        a.MAXPRICE = double.Parse(b.Where(u => (u.Language == requestCulture || u.Language == "all") && u.ParamKey == "PRIREMAX").FirstOrDefault().ParamValue);
        //        //_httpContextAccessor.HttpContext.SetSearchInput(a);
        //    }
        //    else
        //    {
        //        if (a.categories == null)
        //            a.categories = (await _Service.categoriesServices.GetAllAsync())
        //             .Select<Categories, CategoryModel>(a => new CategoryModel()
        //             {
        //                 Id = a.Id,
        //                 Name = a.Name,
        //                 Img = a.Img,
        //                 ParentId = a.ParentId,
        //                 DisplayOnHome = a.DisplayOnHome,
        //                 DisplayOnMenuMain = a.DisplayOnMenuMain,
        //                 Status = a.Status
        //             });
        //    }
        //    return a;
        //}
        private CreateOrder PartialGetOrderInfo()
        {
            return _httpContextAccessor.HttpContext.OrderGet();
        }

        private async Task<string> PartialHeaderPolicy()
        {
            return (await _Service.paramSettingServices.GetAllAsync()).Where(u => (u.Language == requestCulture || u.Language == "all") && u.ParamKey == "SHIPFREE").FirstOrDefault().ParamValue.FormatNumber(0);
        }

        private async Task<HeaderContact> PartialHeaderContact()
        {
            var b = await _Service.paramSettingServices.GetAllAsync();
            var a = new HeaderContact()
            {
                Hotline = b.Where(u => (u.Language == requestCulture || u.Language == "all") && u.ParamKey == "HOTLINE").FirstOrDefault().ParamValue,
                TimeOpen = b.Where(u => (u.Language == requestCulture || u.Language == "all") && u.ParamKey == "TIMEOPEN").FirstOrDefault().ParamValue
            };
            return a;
        }

        private async Task<IEnumerable<About>> PartialAboutList()
        {
            var b = (await _Service.aboutServices.GetAllAsync()).Where(u => u.Id != 8 && u.Id != 9);
            return b;
        }

        private async Task<IEnumerable<ParamSetting>> PartialCompanyInfo()
        {
            var b = await _Service.paramSettingServices.GetAllAsync();
            return b;
        }

        private async Task<IEnumerable<Categories>> PartialCateFooter()
        {
            var b = (await _Service.categoriesServices.GetAllAsync()).Where(u => u.DisplayOnMenuMain);

            return b;
        }

        private async Task<IEnumerable<Categories>> PartialMenuProduct()
        {
            var b = (await _Service.categoriesServices.GetAllAsync());

            return b;
        }
        #endregion
    }
}
