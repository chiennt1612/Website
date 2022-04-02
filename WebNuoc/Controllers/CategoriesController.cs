using EntityFramework.Web.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Paygate.OnePay;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebNuoc.Models.Categories;
using WebNuoc.Models.Home;
using WebNuoc.Services;
using WebNuoc.Services.Interfaces;

namespace WebNuoc.Controllers
{
    //[SecurityHeaders]
    public class CategoriesController : Controller
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger<CategoriesController> _logger;
        private readonly IAllService _Service;
        private readonly IStringLocalizer<CategoriesController> _localizer;
        private const int PageSize = 40;
        private const int ProductDelatedQty = 40;

        public CategoriesController(IDistributedCache _cache, ILogger<CategoriesController> _logger, IAllService _Service, IStringLocalizer<CategoriesController> _localizer)
        {
            this._cache = _cache;
            this._logger = _logger;
            this._Service = _Service;
            this._localizer = _localizer;
        }

        [Route("/[controller]/{Id?}/{Page?}")]
        public async Task<IActionResult> Index(long? Id, int? Page)
        {
            bool IsDiscount = (HttpContext.GetQueryString("IsDiscount") == "1");
            long _Id = (Id.HasValue ? Id.Value : 0);
            int _Page = (Page.HasValue ? Page.Value : 1);
            PathOfPage path = new PathOfPage();
            //await ViewDataParam.SetViewData(this._logger, ViewData, this._Service, path);           

            var a = Tools.GetChildList(await _Service.categoriesServices.GetAllAsync(), _Id);
            Func<Product, object> sqlOrder = s => s.Id;
            Expression<Func<Product, bool>> sqlWhere = u => (
                (a.Contains(u.CategoryMain.Value) || u.CategoryReference.HasValue && a.Contains(u.CategoryReference.Value)) &&
                (!IsDiscount || u.Discount > 0)
                );
            var b = await _Service.productServices.GetListAsync(sqlWhere, sqlOrder, true, _Page, PageSize);

            SearchInput searchInput = new SearchInput()
            {
                CategoryId = _Id,
                Keyword = "",
                max_price = 99999999,
                min_price = 0,
                Page = _Page,
                TotalPage = (b.totalRecords % PageSize > 0 ? (b.totalRecords / PageSize) + 1 : (b.totalRecords / PageSize)),
                SearchType = "Categories",
                TotalRow = b.totalRecords
            };
            ViewData[ViewDataParam.SearchInput] = searchInput;

            return View(b.list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/[controller]/[action]/{Page?}")]
        public async Task<IActionResult> Search(int? Page, SearchInput searchInput)
        {
            bool IsDiscount = (HttpContext.GetQueryString("IsDiscount") == "1");
            int _Page = (Page.HasValue ? Page.Value : 1);
            PathOfPage path = new PathOfPage() { IsProduct = 3, Total = 0, TotalPage = 0, Page = 1, PathName = { _localizer.GetString("Tìm kiếm") } };
            //await ViewDataParam.SetViewData(this._logger, ViewData, this._Service, path);
            searchInput.TotalPage = 100;
            searchInput.Page = _Page;
            var a = Tools.GetChildList(await _Service.categoriesServices.GetAllAsync(), 0);
            Func<Product, object> sqlOrder = s => s.Id;
            Expression<Func<Product, bool>> sqlWhere = u => (
                (a.Contains(u.CategoryMain.Value) ||
                u.CategoryReference.HasValue && a.Contains(u.CategoryReference.Value)) &&
                u.Price >= searchInput.min_price &&
                u.Price <= searchInput.max_price &&
                (searchInput.Keyword == "" || (u.Code.Contains(searchInput.Keyword) || u.Name.Contains(searchInput.Keyword) || u.MainCategories.Name.Contains(searchInput.Keyword))) &&
                (!searchInput.IsDiscount || u.Discount > 0)
                );
            var b = await _Service.productServices.GetListAsync(sqlWhere, sqlOrder, true, _Page, PageSize);

            searchInput.TotalPage = (b.totalRecords % PageSize > 0 ? (b.totalRecords / PageSize) + 1 : (b.totalRecords / PageSize));
            searchInput.Page = _Page;
            searchInput.TotalRow = b.totalRecords;

            ViewData[ViewDataParam.SearchInput] = searchInput;
            return View(b.list);
        }

        [HttpGet]
        [Route("/[controller]/[action]/{Page?}")]
        public async Task<IActionResult> Search(int? Page)
        {
            bool IsDiscount = (HttpContext.GetQueryString("IsDiscount") == "1");
            int _Page = (Page.HasValue ? Page.Value : 1);
            PathOfPage path = new PathOfPage() { IsProduct = 3, Total = 0, TotalPage = 0, Page = 1, PathName = { _localizer.GetString("Tìm kiếm") } };
            //await ViewDataParam.SetViewData(this._logger, ViewData, this._Service, path);

            var a = Tools.GetChildList(await _Service.categoriesServices.GetAllAsync(), 0);
            Func<Product, object> sqlOrder = s => s.Id;
            Expression<Func<Product, bool>> sqlWhere = u => (a.Contains(u.CategoryMain.Value) || u.CategoryReference.HasValue && a.Contains(u.CategoryReference.Value));
            var b = await _Service.productServices.GetListAsync(sqlWhere, sqlOrder, true, _Page, PageSize);

            SearchInput searchInput = new SearchInput()
            {
                CategoryId = 0,
                Keyword = "",
                max_price = 99999999,
                min_price = 0,
                Page = _Page,
                TotalPage = (b.totalRecords % PageSize > 0 ? (b.totalRecords / PageSize) + 1 : (b.totalRecords / PageSize)),
                SearchType = "Categories",
                TotalRow = b.totalRecords
            };
            ViewData[ViewDataParam.SearchInput] = searchInput;
            return View(b.list);
        }

        [Route("/[controller]/[action]/{Id?}")]
        public async Task<IActionResult> Details(long? Id)
        {
            long _Id = (Id.HasValue ? Id.Value : 0);

            PathOfPage path = new PathOfPage();
            //await ViewDataParam.SetViewData(this._logger, ViewData, this._Service, path);
            // Get product Infor
            var a = await _Service.productServices.GetByIdAsync((Id.HasValue ? Id.Value : 0));
            long _CateId = a.MainCategories.Id;
            // Get Product viewer
            ProductView b1 = new ProductView()
            {
                Id = a.Id,
                CategoryName = a.MainCategories.Name,
                Code = a.Code,
                Name = a.Name,
                Price = a.Price,
                Discount = a.Discount,
                Quota = a.Quota,
                Img = a.Img
            };
            //var b = HttpContext.ChangeProductView(b1).Where (u => u.Id != _Id);
            // Get Product Related
            var b2 = Tools.GetChildList(await _Service.categoriesServices.GetAllAsync(), _CateId);
            Expression<Func<Product, bool>> sqlWhere = u => (
                (b2.Contains(u.CategoryMain.Value) || u.CategoryReference.HasValue && b2.Contains(u.CategoryReference.Value)) &&
                u.Id != _Id
                );
            var c = await _Service.productServices.GetTopAsync(sqlWhere, ProductDelatedQty);
            var d = await _Service.paramSettingServices.GetAllAsync();

            ProductDetails r = new ProductDetails()
            {
                //productView = b,
                productRelated = c,
                product = a,
                paramSetting = d
            };

            return View(r);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> QuickView()
        {
            long _id = long.Parse(HttpContext.Request.Form["product"]);
            var a = await _Service.productServices.GetByIdAsync(_id);
            return View(a);
        }
    }
}
