using EntityFramework.Web.DBContext;
using EntityFramework.Web.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebAdmin.Helpers;
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Controllers
{
    [SecurityHeaders]
    [Authorize(Roles = "Admin,Mod")]
    public class ArticlesController : Controller
    {
        private readonly ILogger<ArticlesController> _logger;
        private readonly IStringLocalizer<ArticlesController> _localizer;
        private readonly IArticleServices _service;

        public ArticlesController(IArticleServices _service, ILogger<ArticlesController> logger, IStringLocalizer<ArticlesController> localizer)
        {
            _logger = logger;
            _localizer = localizer;
            this._service = _service;
        }

        // GET: Articles
        public async Task<IActionResult> Index(string Keyword, int? page)
        {
            int pageSize = Constants.PageSize;
            int pageIndex = 1;
            pageIndex = page.HasValue ? page.Value : 1;

            Expression<Func<Article, bool>> sqlWhere;
            if (!String.IsNullOrEmpty(Keyword))
            {
                ViewData["Keyword"] = Keyword;
                sqlWhere = item => (
                item.IsDeleted == false &&
                (item.Title.Contains(Keyword) || item.Summary.Contains(Keyword))
                );
            }
            else
            {
                ViewData["Keyword"] = "";
                sqlWhere = item => (item.IsDeleted == false);
            }
            Func<Article, object> sqlOrder = s => s.Id;

            return View(await _service.GetListAsync(sqlWhere, sqlOrder, true, pageIndex, pageSize));
        }

        // GET: Articles/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var article = await _context.Articles
            //    .Include(a => a.NewsCategories)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (article == null)
            //{
            //    return NotFound();
            //}

            return View(await _service.GetByIdAsync(id.Value));
        }

        // GET: Articles/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryMain"] = new SelectList(await _service.CateGetAllAsync(), "Id", "Name");
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Article article)
        {
            if (ModelState.IsValid)
            {
                if (article.Summary != null) 
                    article.Summary = article.Summary
                                            .Replace(@"../../Upload/", Util.UrlHostUpload(Request))
                                            .Replace(@"../Upload/", Util.UrlHostUpload(Request));
                if (article.Description != null)
                    article.Description = article.Description
                                            .Replace(@"../../Upload/", Util.UrlHostUpload(Request))
                                            .Replace(@"../Upload/", Util.UrlHostUpload(Request));
                await _service.AddAsync(article);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryMain"] = new SelectList(await _service.CateGetAllAsync(), "Id", "Name", article.CategoryMain);
            return View(article);
        }

        // GET: Articles/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _service.GetByIdAsync(id.Value);
            //await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            ViewData["CategoryMain"] = new SelectList(await _service.CateGetAllAsync(), "Id", "Name", article.CategoryMain);
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Article article)
        {
            if (id != article.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(article);
                    //await _context.SaveChangesAsync();
                    if (article.Summary != null)
                        article.Summary = article.Summary
                                                .Replace(@"../../Upload/", Util.UrlHostUpload(Request))
                                                .Replace(@"../Upload/", Util.UrlHostUpload(Request));
                    if (article.Description != null) 
                        article.Description = article.Description
                                                .Replace(@"../../Upload/", Util.UrlHostUpload(Request))
                                                .Replace(@"../Upload/", Util.UrlHostUpload(Request));
                    await _service.UpdateAsync(article);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ArticleExists(article.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryMain"] = new SelectList(await _service.CateGetAllAsync(), "Id", "Name", article.CategoryMain);
            return View(article);
        }

        // GET: Articles/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _service.GetByIdAsync(id.Value);
            //await _context.Articles
            //.Include(a => a.NewsCategories)
            //.FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            //var article = await _context.Articles.FindAsync(id);
            //_context.Articles.Remove(article);
            //await _context.SaveChangesAsync();
            await _service.DeleteByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ArticleExists(long id)
        {
            return (await _service.GetAllAsync()).Any(e => e.Id == id);
        }
    }
}
