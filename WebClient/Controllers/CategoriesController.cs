using EntityFramework.Web.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Paygate.OnePay;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebClient.Helpers;
using WebClient.Models.Category;
using WebClient.Models.Home;
using WebClient.Models.Order;
using WebClient.Services.Interfaces;

namespace WebClient.Controllers
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

        [Route("/[controller]/{Id?}/{Page?}/{OrderBy?}")]
        public async Task<IActionResult> Index(long? Id, int? Page, string OrderBy)
        {
            bool IsDiscount = (HttpContext.GetQueryString("IsDiscount") == "1");
            long _Id = (Id.HasValue ? Id.Value : 48);
            int _Page = (Page.HasValue ? Page.Value : 1);

            var _allCate = await _Service.categoriesServices.GetAllAsync();
            ViewData["CATEGORIES"] = _allCate;
            var a = Tools.GetChildList(_allCate, _Id);
            var IsDesc = true;
            Func<Product, object> sqlOrder = Tools.GetFunc(OrderBy, out IsDesc);
            Expression<Func<Product, bool>> sqlWhere = u => (
                (a.Contains(u.CategoryMain.Value) || u.CategoryReference.HasValue && a.Contains(u.CategoryReference.Value)) &&
                (!IsDiscount || u.Discount > 0)
                );
            var b = await _Service.productServices.GetListAsync(sqlWhere, sqlOrder, IsDesc, _Page, PageSize);
            var TotalPage = (b.totalRecords % PageSize > 0 ? (b.totalRecords / PageSize) + 1 : (b.totalRecords / PageSize));
            SearchInput searchInput = _SetSearch(
                                            _Id, "", 99999999, 0,
                                            _Page, TotalPage, "Categories", b.totalRecords, (String.IsNullOrEmpty(OrderBy) ? "date" : OrderBy));

            return View(b.list);
        }

        [Route("/[controller]/[action]/{Page?}/{OrderBy?}")]
        public async Task<IActionResult> ProductHot(int? Page, string OrderBy)
        {
            bool IsDiscount = (HttpContext.GetQueryString("IsDiscount") == "1");
            int _Page = (Page.HasValue ? Page.Value : 1);

            var IsDesc = true;
            Func<Product, object> sqlOrder = Tools.GetFunc(OrderBy, out IsDesc);
            Expression<Func<Product, bool>> sqlWhere = u => (
                (!IsDiscount || u.Discount > 0) && u.IsHot
                );
            var b = await _Service.productServices.GetListAsync(sqlWhere, sqlOrder, IsDesc, _Page, PageSize);
            var TotalPage = (b.totalRecords % PageSize > 0 ? (b.totalRecords / PageSize) + 1 : (b.totalRecords / PageSize));
            SearchInput searchInput = _SetSearch(
                                            0, "", 99999999, 0,
                                            _Page, TotalPage, "ProductHot", b.totalRecords, (String.IsNullOrEmpty(OrderBy) ? "date" : OrderBy));

            PathOfPage path = new PathOfPage()
            {
                IsProduct = 2,
                Page = _Page,
                TotalPage = TotalPage,
                PageSize = PageSize,
                Total = b.totalRecords,
                PathName = { _localizer.GetString("SALE SỐC") }
            };
            (await ViewData.InitialAsync(_Service)).SetPage(path);

            return View("ProductNew", b.list);
        }

        [Route("/[controller]/[action]/{Page?}/{OrderBy?}")]
        public async Task<IActionResult> ProductNew(int? Page, string OrderBy)
        {
            bool IsDiscount = (HttpContext.GetQueryString("IsDiscount") == "1");
            int _Page = (Page.HasValue ? Page.Value : 1);

            var IsDesc = true;
            Func<Product, object> sqlOrder = Tools.GetFunc(OrderBy, out IsDesc);
            Expression<Func<Product, bool>> sqlWhere = u => (
                (!IsDiscount || u.Discount > 0) && u.IsNews
                );
            var b = await _Service.productServices.GetListAsync(sqlWhere, sqlOrder, IsDesc, _Page, PageSize);
            var TotalPage = (b.totalRecords % PageSize > 0 ? (b.totalRecords / PageSize) + 1 : (b.totalRecords / PageSize));
            SearchInput searchInput = _SetSearch(
                                            0, "", 99999999, 0,
                                            _Page, TotalPage, "ProductNew", b.totalRecords, (String.IsNullOrEmpty(OrderBy) ? "date" : OrderBy));

            PathOfPage path = new PathOfPage()
            {
                IsProduct = 2,
                Page = _Page,
                TotalPage = TotalPage,
                PageSize = PageSize,
                Total = b.totalRecords,
                PathName = { _localizer.GetString("SẢN PHẨM MỚI") }
            };
            (await ViewData.InitialAsync(_Service)).SetPage(path);

            return View("ProductNew", b.list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/[controller]/[action]/{Page?}/{OrderBy?}")]
        public async Task<IActionResult> Search(int? Page, string OrderBy, SearchInput searchInput)
        {
            bool IsDiscount = (HttpContext.GetQueryString("IsDiscount") == "1");
            int _Page = (Page.HasValue ? Page.Value : 1);
            long _cateId = (searchInput.CategoryId.HasValue ? searchInput.CategoryId.Value : 48);
            double min_price = ((searchInput.min_price.HasValue && searchInput.min_price.Value > 0) ? searchInput.min_price.Value : 0);
            double max_price = ((searchInput.max_price.HasValue && searchInput.max_price.Value > 0) ? searchInput.max_price.Value : 99999999);

            var a = Tools.GetChildList(await _Service.categoriesServices.GetAllAsync(), _cateId);
            var IsDesc = true;
            Func<Product, object> sqlOrder = Tools.GetFunc(OrderBy, out IsDesc);
            Expression<Func<Product, bool>> sqlWhere = u => (
                (a.Contains(u.CategoryMain.Value) ||
                u.CategoryReference.HasValue && a.Contains(u.CategoryReference.Value)) &&
                u.Price >= min_price &&
                u.Price <= max_price &&
                (
                    String.IsNullOrEmpty(searchInput.Keyword) || (
                    u.Code.Contains(searchInput.Keyword) ||
                    u.Name.Contains(searchInput.Keyword) ||
                    u.Summary.Contains(searchInput.Keyword) ||
                    u.Description.Contains(searchInput.Keyword) ||
                    u.MainCategories.Name.Contains(searchInput.Keyword))
                ) &&
                (!searchInput.IsDiscount || u.Discount > 0)
                );
            var b = await _Service.productServices.GetListAsync(sqlWhere, sqlOrder, IsDesc, _Page, PageSize);

            var TotalPage = (b.totalRecords % PageSize > 0 ? (b.totalRecords / PageSize) + 1 : (b.totalRecords / PageSize));
            searchInput.TotalPage = TotalPage;
            searchInput.Page = _Page;
            searchInput.TotalRow = b.totalRecords;
            searchInput.OrderValue = (String.IsNullOrEmpty(OrderBy) ? "date" : OrderBy);
            searchInput.SearchType = "ByPrice";

            ViewData.SetSearchInput(searchInput);

            var r = new ProductSearchModel()
            {
                CategoryId = _cateId,
                Keyword = (String.IsNullOrEmpty(searchInput.Keyword) ? "" : searchInput.Keyword),
                products = b.list,
                paramSetting = await _Service.paramSettingServices.GetAllAsync(),
                min_price = min_price,
                max_price = max_price,
                Page = _Page,
                TotalPage = TotalPage,
                TotalRow = b.totalRecords
            };

            return View(r);
        }

        [HttpGet]
        [Route("/[controller]/[action]/{Page?}/{OrderBy?}")]
        public async Task<IActionResult> Search(int? Page, string OrderBy)
        {
            bool IsDiscount = (HttpContext.GetQueryString("IsDiscount") == "1");
            int _Page = (Page.HasValue ? Page.Value : 1);
            PathOfPage path = new PathOfPage() { IsProduct = 3, Total = 0, TotalPage = 0, Page = 1, PathName = { _localizer.GetString("Tìm kiếm") } };
            (await ViewData.InitialAsync(_Service)).SetPage(path);

            var _allCate = await _Service.categoriesServices.GetAllAsync();
            var a = Tools.GetChildList(_allCate, 0);
            var IsDesc = true;
            Func<Product, object> sqlOrder = Tools.GetFunc(OrderBy, out IsDesc);
            Expression<Func<Product, bool>> sqlWhere = u => (a.Contains(u.CategoryMain.Value) || u.CategoryReference.HasValue && a.Contains(u.CategoryReference.Value));
            var b = await _Service.productServices.GetListAsync(sqlWhere, sqlOrder, IsDesc, _Page, PageSize);

            SearchInput searchInput = _SetSearch(
                                            0, "", 99999999, 0,
                                            _Page, (b.totalRecords % PageSize > 0 ? (b.totalRecords / PageSize) + 1 : (b.totalRecords / PageSize)),
                                            "ByPrice", b.totalRecords, (String.IsNullOrEmpty(OrderBy) ? "date" : OrderBy));
            return View(b.list);
        }

        [Route("/[controller]/[action]/{Id}")]
        public async Task<IActionResult> Details(long Id)
        {
            long _Id = Id;// (Id.HasValue ? Id.Value : 0);

            // Get product Infor
            var a = await _Service.productServices.GetByIdAsync(_Id);
            long _CateId = a.MainCategories.Id;
            // Get Product viewer
            ProductView b1 = new ProductView()
            {
                Id = a.Id,
                CategoryName = a.MainCategories.Name,
                CategoryId = a.CategoryMain.Value,
                Code = a.Code,
                Name = a.Name,
                Price = a.Price,
                Discount = a.Discount,
                Quota = a.Quota,
                Img = a.Img
            };
            var _allCate = await _Service.categoriesServices.GetAllAsync();
            var b = HttpContext.ChangeProductView(b1).Where(u => u.Id != _Id);
            // Get Product Related
            var b2 = Tools.GetChildList(_allCate, _CateId);
            Expression<Func<Product, bool>> sqlWhere = u => (
                (b2.Contains(u.CategoryMain.Value) || u.CategoryReference.HasValue && b2.Contains(u.CategoryReference.Value)) &&
                u.Id != _Id
                );
            var c = await _Service.productServices.GetTopAsync(sqlWhere, ProductDelatedQty);
            var d = await _Service.paramSettingServices.GetAllAsync();

            ProductDetails r = new ProductDetails()
            {
                productView = b,
                productRelated = c,
                product = a,
                paramSetting = d
            };

            // var pOrders = new List<POrder>();
            PathOfPage path = new PathOfPage()
            {
                IsProduct = 3,
                Page = 0,
                TotalPage = 0,
                //pOrders = pOrders,
                Total = 0,
                PathName = Tools.GetPath(await _Service.categoriesServices.GetAllAsync(), _CateId)
            };
            (await ViewData.InitialAsync(_Service)).SetPage(path);

            SearchInput searchInput = _SetSearch(
                                            _CateId, "", 99999999, 0,
                                            1, 0, "Categories", 0, "date");
            ViewData["CATEGORIES"] = _allCate;
            return View(r);
        }

        //[Route("/{ProductCode}.html")]
        [Route("/{ProductName}-{ProductCode}")]
        public async Task<IActionResult> Details(string ProductName, string ProductCode)
        {
            // Get product Infor
            var a = await _Service.productServices.GetByCodeAsync(ProductCode);
            if (a == null)
                return View(new ProductDetails()
                {
                    productView = null,
                    productRelated = null,
                    product = null,
                    paramSetting = null
                });
            long _Id = a.Id;
            long _CateId = a.MainCategories.Id;
            // Get Product viewer
            ProductView b1 = new ProductView()
            {
                Id = a.Id,
                CategoryName = a.MainCategories.Name,
                CategoryId = a.CategoryMain.Value,
                Code = a.Code,
                Name = a.Name,
                Price = a.Price,
                Discount = a.Discount,
                Quota = a.Quota,
                Img = a.Img
            };
            var _allCate = await _Service.categoriesServices.GetAllAsync();
            var b = HttpContext.ChangeProductView(b1).Where(u => u.Id != _Id);
            // Get Product Related
            var b2 = Tools.GetChildList(_allCate, _CateId);
            Expression<Func<Product, bool>> sqlWhere = u => (
                (b2.Contains(u.CategoryMain.Value) || u.CategoryReference.HasValue && b2.Contains(u.CategoryReference.Value)) &&
                u.Id != _Id
                );
            var c = await _Service.productServices.GetTopAsync(sqlWhere, ProductDelatedQty);
            var d = await _Service.paramSettingServices.GetAllAsync();

            ProductDetails r = new ProductDetails()
            {
                productView = b,
                productRelated = c,
                product = a,
                paramSetting = d
            };

            SearchInput searchInput = _SetSearch(
                                            _CateId, "", 99999999, 0,
                                            1, 0, "Categories", 0, "date");
            ViewData["CATEGORIES"] = _allCate;
            return View(r);
        }

        //[Route("/{ProductCode}.html")]
        //public async Task<IActionResult> Detail(string ProductCode)
        //{
        //    // Get product Infor
        //    var a = await _Service.productServices.GetByCodeAsync(ProductCode);
        //    if (a == null)
        //        return View(new ProductDetails()
        //        {
        //            productView = null,
        //            productRelated = null,
        //            product = null,
        //            paramSetting = null
        //        });
        //    long _Id = a.Id;
        //    long _CateId = a.MainCategories.Id;
        //    // Get Product viewer
        //    ProductView b1 = new ProductView()
        //    {
        //        Id = a.Id,
        //        CategoryName = a.MainCategories.Name,
        //        CategoryId = a.CategoryMain.Value,
        //        Code = a.Code,
        //        Name = a.Name,
        //        Price = a.Price,
        //        Discount = a.Discount,
        //        Quota = a.Quota,
        //        Img = a.Img
        //    };
        //    var _allCate = await _Service.categoriesServices.GetAllAsync();
        //    var b = HttpContext.ChangeProductView(b1).Where(u => u.Id != _Id);
        //    // Get Product Related
        //    var b2 = Tools.GetChildList(_allCate, _CateId);
        //    Expression<Func<Product, bool>> sqlWhere = u => (
        //        (b2.Contains(u.CategoryMain.Value) || u.CategoryReference.HasValue && b2.Contains(u.CategoryReference.Value)) &&
        //        u.Id != _Id
        //        );
        //    var c = await _Service.productServices.GetTopAsync(sqlWhere, ProductDelatedQty);
        //    var d = await _Service.paramSettingServices.GetAllAsync();

        //    ProductDetails r = new ProductDetails()
        //    {
        //        productView = b,
        //        productRelated = c,
        //        product = a,
        //        paramSetting = d
        //    };

        //    SearchInput searchInput = _SetSearch(
        //                                    _CateId, "", 99999999, 0,
        //                                    1, 0, "Categories", 0, "date");
        //    ViewData["CATEGORIES"] = _allCate;
        //    return View(r);
        //}

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> QuickView()
        {
            long _id = long.Parse(HttpContext.Request.Form["product"]);
            var a = await _Service.productServices.GetByIdAsync(_id);
            return View(a);
        }

        [Route("/[controller]/[action]")]
        public async Task<IActionResult> QuickSearch(string template, string q)
        {
            bool IsDesc = true;
            Func<Product, object> sqlOrder = u => u.DateCreator;
            Expression<Func<Product, bool>> sqlWhere = u => (
                u.Code.Contains(q) || u.Name.Contains(q)
            );
            var b = await _Service.productServices.GetListAsync(sqlWhere, sqlOrder, IsDesc, 1, 20);
            return View(b.list);
        }

        private SearchInput _SetSearch(
            long _Id, string Keyword, double max_price, double min_price,
            int _Page, int TotalPage, string SearchType, int TotalRow, string OrderBy)
        {
            SearchInput searchInput = ViewData.GetSearchInput();
            if (searchInput == null)
            {
                searchInput = new SearchInput()
                {
                    CategoryId = _Id,
                    Keyword = Keyword,
                    max_price = max_price,
                    min_price = min_price,
                    Page = _Page,
                    TotalPage = TotalPage,
                    SearchType = SearchType,
                    TotalRow = TotalRow,
                    OrderValue = OrderBy
                };
            }
            else
            {
                searchInput.CategoryId = _Id;
                searchInput.Keyword = Keyword;
                searchInput.max_price = max_price;
                searchInput.min_price = min_price;
                searchInput.Page = _Page;
                searchInput.TotalPage = TotalPage;
                searchInput.SearchType = SearchType;
                searchInput.TotalRow = TotalRow;
                searchInput.OrderValue = (String.IsNullOrEmpty(OrderBy) ? "date" : OrderBy);
            }
            ViewData.SetSearchInput(searchInput);
            return searchInput;
        }
    }
}
