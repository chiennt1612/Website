using EntityFramework.Web.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebClient.Helpers;
using WebClient.Models.Home;
using WebClient.Services.Interfaces;

namespace WebClient.Controllers
{
    //[SecurityHeaders]
    public class NewsController : Controller
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger<NewsController> _logger;
        private readonly IAllService _Service;
        private readonly IStringLocalizer<NewsController> _localizer;
        private const int PageSize = 15;

        public NewsController(IDistributedCache _cache, ILogger<NewsController> _logger, IAllService _Service, IStringLocalizer<NewsController> _localizer)
        {
            this._cache = _cache;
            this._logger = _logger;
            this._Service = _Service;
            this._localizer = _localizer;
        }

        [Route("/[controller]/{Id?}/{Page?}")]
        public async Task<IActionResult> Index(long? Id, int? Page)
        {
            long _Id = (Id.HasValue ? Id.Value : 4);
            int _Page = (Page.HasValue ? Page.Value : 1);
            Func<Article, object> sqlOrder = s => s.Id;
            Expression<Func<Article, bool>> sqlWhere = u => (u.CategoryMain == _Id);
            var a = await _Service.articleServices.GetListAsync(sqlWhere, sqlOrder, true, _Page, PageSize);
            a.page = _Page;
            a.pageSize = PageSize;
            a.CategoryId = _Id;
            return View(a);
        }

        //[Route("/[controller]/[action]/{Id}")]
        //public async Task<IActionResult> Details(long Id)
        //{
        //    var a = await _Service.articleServices.GetByIdAsync(Id);
        //    PathOfPage path = new PathOfPage() { IsProduct = 3, Total = 0, TotalPage = 0, Page = 1, PathName = { _localizer["Tin tức"], a.Title } };
        //    (await ViewData.InitialAsync(_Service)).SetPage(path);
        //    return View(a);
        //}

        [Route("/{Title}/{Id}")]
        public async Task<IActionResult> Details(string Title, long Id)
        {
            PathOfPage path;
            if (Id < 1)
            {
                path = new PathOfPage() { IsProduct = 3, Total = 0, TotalPage = 0, Page = 1, PathName = { _localizer["Tin tức"], "Error404" } };
                (await ViewData.InitialAsync(_Service)).SetPage(path);
                return View("Error404");
            }
            var a = await _Service.articleServices.GetByIdAsync(Id);
            if (a == default || a == null)
            {
                path = new PathOfPage() { IsProduct = 3, Total = 0, TotalPage = 0, Page = 1, PathName = { _localizer["Tin tức"], "Error404" } };
                (await ViewData.InitialAsync(_Service)).SetPage(path);
                return View("Error404");
            }
            path = new PathOfPage() { IsProduct = 3, Total = 0, TotalPage = 0, Page = 1, PathName = { _localizer["Tin tức"], a.Title } };
            (await ViewData.InitialAsync(_Service)).SetPage(path);

            Expression<Func<Article, bool>> sqlWhere = u => (u.CategoryMain == a.CategoryMain && u.Id < a.Id);
            ViewData["ArticleRelated"] = await _Service.articleServices.GetTopAsync(sqlWhere, 10);
            return View(a);
        }
    }
}
