using EntityFramework.Web.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebNuoc.Models.News;
using WebNuoc.Services.Interfaces;

namespace WebNuoc.Controllers
{
    //[SecurityHeaders]
    public class NewsController : Controller
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger<NewsController> _logger;
        private readonly IAllService _Service;
        private readonly IStringLocalizer<NewsController> _localizer;
        private const int PageSize = 10;

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
            await Task.Delay(0);
            long _Id = (Id.HasValue ? Id.Value : 0);
            int _Page = (Page.HasValue ? Page.Value : 1);
            Func<Article, object> sqlOrder = s => s.Id;
            Expression<Func<Article, bool>> sqlWhere = u => (u.CategoryMain == _Id || _Id == 0);
            Expression<Func<Article, bool>> sqlWhereNew = u => (u.IsNews);

            var listArticle = await _Service.articleServices.GetListAsync(sqlWhere, sqlOrder, true, _Page, PageSize);
            listArticle.pageSize = PageSize;

            var a = new ListNews()
            {
                listArticle = listArticle,
                articles = await _Service.articleServices.GetTopAsync(sqlWhereNew, 7),
                CategoryId = _Id,
                Keyword = "",
                rightAdvs = (await _Service.advServices.GetAllAsync()).Where(u => u.Pos == 15).OrderByDescending(u => u.StartDate)
            };

            return View(a);
        }
        [Route("/[controller]/[action]/{Id?}")]
        public async Task<IActionResult> Details(long? Id)
        {
            Expression<Func<Article, bool>> sqlWhereNew = u => (u.IsNews);
            var article = await _Service.articleServices.GetByIdAsync((Id.HasValue ? Id.Value : 0));
            Expression<Func<Article, bool>> sqlWhere = u => (u.CategoryMain == article.CategoryMain && u.Id < article.Id);
            var a = new NewsDetails()
            {
                article = article,
                articles = await _Service.articleServices.GetTopAsync(sqlWhereNew, 7),
                rightAdvs = (await _Service.advServices.GetAllAsync()).Where(u => u.Pos == 15).OrderByDescending(u => u.StartDate),
                articleRelated = await _Service.articleServices.GetTopAsync(sqlWhere, 10)
            };

            return View(a);
        }
    }
}
