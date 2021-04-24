using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using EntityFramework.Web.DBContext;
using EntityFramework.Web.Entities;
using WebAdmin.Helpers;
using WebAdmin.Repository.Interfaces;
using WebAdmin.Services.Interfaces;
using X.PagedList;

namespace WebAdmin.Controllers
{
    [Authorize(Roles = "Admin,Mod")]
    public class NewsCategoriesController : Controller
    {
        private readonly ILogger<NewsCategoriesController> _logger;
        private readonly IStringLocalizer<NewsCategoriesController> _localizer;
        //private readonly AppDbContext _context;
        //private readonly IUnitOfWork _unitOfWork;
        private readonly INewsCategoriesServices _service;

        public NewsCategoriesController(INewsCategoriesServices _service, ILogger<NewsCategoriesController> logger, IStringLocalizer<NewsCategoriesController> localizer)
        {
            _logger = logger;
            _localizer = localizer;
            this._service = _service;
        }

        // GET: NewsCategories
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = Constants.PageSize;
            int pageIndex = 1;
            pageIndex = page.HasValue ? page.Value : 1;

            Expression<Func<NewsCategories, bool>> sqlWhere = item => (item.IsDeleted == false);
            Func<NewsCategories, string> sqlOrder = s => "DateCreator";

            return View(await _service.GetListAsync(sqlWhere, sqlOrder, true, pageIndex, pageSize));
        }

        // GET: NewsCategories/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var newsCategories = await _context.NewsCategoriess
            //    .Include(n => n.Parent)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (newsCategories == null)
            //{
            //    return NotFound();
            //}

            return View(await _service.GetByIdAsync(id.Value));
        }

        // GET: NewsCategories/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ParentId"] = new SelectList(await _service.GetAllAsync(), "Id", "Name", 0);
            return View();
        }

        // POST: NewsCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ParentId,Status,DisplayOnMenuMain,Id")] NewsCategories newsCategories)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(newsCategories);
                //await _context.SaveChangesAsync();
                await _service.AddAsync(newsCategories);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentId"] = new SelectList(await _service.GetAllAsync(), "Id", "Name", newsCategories.ParentId);
            return View(newsCategories);
        }

        // GET: NewsCategories/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var newsCategories = await _context.NewsCategoriess.FindAsync(id);
            //if (newsCategories == null)
            //{
            //    return NotFound();
            //}
            var newsCategories = await _service.GetByIdAsync(id.Value);
            if (newsCategories == null)
            {
                return NotFound();
            }
            ViewData["ParentId"] = new SelectList(await _service.GetAllAsync(), "Id", "Name", newsCategories.ParentId, "Parent");
            return View(newsCategories);
        }

        // POST: NewsCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Name,ParentId,Status,DisplayOnMenuMain,Id")] NewsCategories newsCategories)
        {
            if (id != newsCategories.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(newsCategories);
                    //await _context.SaveChangesAsync();
                    await _service.UpdateAsync(newsCategories);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await NewsCategoriesExists(newsCategories.Id))
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
            ViewData["ParentId"] = new SelectList(await _service.GetAllAsync(), "Id", "Name", newsCategories.ParentId, newsCategories.Parent.Name);
            return View(newsCategories);
        }

        // GET: NewsCategories/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var newsCategories = await _context.NewsCategoriess
            //    .Include(n => n.Parent)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (newsCategories == null)
            //{
            //    return NotFound();
            //}
            var newsCategories = await _service.GetByIdAsync(id.Value);
            if (newsCategories == null)
            {
                return NotFound();
            }

            return View(newsCategories);
        }

        // POST: NewsCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            //var newsCategories = await _context.NewsCategoriess.FindAsync(id);
            //_context.NewsCategoriess.Remove(newsCategories);
            //await _context.SaveChangesAsync();
            await _service.DeleteByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> NewsCategoriesExists(long id)
        {
            return (await _service.GetAllAsync()).Any(e => e.Id == id);
        }
    }
}
