using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using EntityFramework.Web.DBContext;
using EntityFramework.Web.Entities;
using X.PagedList.Mvc.Core;
using X.PagedList;
using WebAdmin.Services.Interfaces;
using System.Linq.Expressions;
using WebAdmin.Helpers;

namespace WebAdmin.Controllers
{
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
        public async Task<IActionResult> IndexAsync(int? page)
        {
            int pageSize = Constants.PageSize;
            int pageIndex = 1;
            pageIndex = page.HasValue ? page.Value : 1;

            Expression<Func<Article, bool>> sqlWhere = item => (item.IsDeleted == false);
            Func<Article, string> sqlOrder = s => "DateCreator";

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
        public async Task<IActionResult> Create([Bind("CategoryMain,CategoryReference,TagsReference,Title,Img,ImgBanner,Summary,Description,IsNews,Status,Publisher,MetaTitle,MetaDescription,MetaKeyword,MetaBox,MetaRobotTag,Id,UserCreator,DateCreator,UserModify,DateModify,IsDeleted,UserDeleted,DateDeleted")] Article article)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(article);
                return RedirectToAction(nameof(IndexAsync));
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
        public async Task<IActionResult> Edit(long id, [Bind("CategoryMain,CategoryReference,TagsReference,Title,Img,ImgBanner,Summary,Description,IsNews,Status,Publisher,MetaTitle,MetaDescription,MetaKeyword,MetaBox,MetaRobotTag,Id,UserCreator,DateCreator,UserModify,DateModify,IsDeleted,UserDeleted,DateDeleted")] Article article)
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
                return RedirectToAction(nameof(IndexAsync));
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
            return RedirectToAction(nameof(IndexAsync));
        }

        private async Task<bool> ArticleExists(long id)
        {
            return (await _service.GetAllAsync()).Any(e => e.Id == id);
        }
    }
}
