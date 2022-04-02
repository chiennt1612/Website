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
    public class CategoriesController : Controller
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly IStringLocalizer<CategoriesController> _localizer;
        private readonly ICategoriesServices _service;

        public CategoriesController(ICategoriesServices _service, ILogger<CategoriesController> logger, IStringLocalizer<CategoriesController> localizer)
        {
            _logger = logger;
            _localizer = localizer;
            this._service = _service;
        }

        // GET: Categories
        public async Task<IActionResult> Index(int? page, string Keyword)
        {
            int pageSize = Constants.PageSize;
            int pageIndex = 1;
            pageIndex = page.HasValue ? page.Value : 1;

            Expression<Func<Categories, bool>> sqlWhere;
            if (!String.IsNullOrEmpty(Keyword))
            {
                ViewData["Keyword"] = Keyword;
                sqlWhere = item => (
                item.IsDeleted == false &&
                (item.Name.Contains(Keyword))
                );
            }
            else
            {
                ViewData["Keyword"] = "";
                sqlWhere = item => (item.IsDeleted == false);
            }
            Func<Categories, object> sqlOrder = s => s.Id;

            return View(await _service.GetListAsync(sqlWhere, sqlOrder, true, pageIndex, pageSize));
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var categories = await _service.Categoriess
            //    .Include(c => c.Parent)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (categories == null)
            //{
            //    return NotFound();
            //}

            return View(await _service.GetByIdAsync(id.Value));
        }

        // GET: Categories/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ParentId"] = new SelectList((await _service.GetAllAsync()), "Id", "Name");
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Categories categories)
        {
            if (ModelState.IsValid)
            {
                //_service.Add(categories);
                //await _service.SaveChangesAsync();
                await _service.AddAsync(categories);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentId"] = new SelectList(await _service.GetAllAsync(), "Id", "Name", categories.ParentId);
            return View(categories);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var categories = await _service.Categoriess.FindAsync(id);
            //if (categories == null)
            //{
            //    return NotFound();
            //}
            var categories = await _service.GetByIdAsync(id.Value);
            ViewData["ParentId"] = new SelectList(await _service.GetAllAsync(), "Id", "Name", categories.ParentId);
            return View(categories);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Categories categories)
        {
            if (id != categories.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_service.Update(categories);
                    //await _service.SaveChangesAsync();
                    await _service.UpdateAsync(categories);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CategoriesExists(categories.Id))
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
            ViewData["ParentId"] = new SelectList(await _service.GetAllAsync(), "Id", "Name", categories.ParentId);
            return View(categories);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categories = await _service.GetByIdAsync(id.Value);
            //= await _service.Categoriess
            //.Include(c => c.Parent)
            //.FirstOrDefaultAsync(m => m.Id == id);
            if (categories == null)
            {
                return NotFound();
            }

            return View(categories);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            //var categories = await _service.Categoriess.FindAsync(id);
            //_service.Categoriess.Remove(categories);
            //await _service.SaveChangesAsync();
            await _service.DeleteByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CategoriesExists(long id)
        {
            return (await _service.GetAllAsync()).Any(e => e.Id == id);
        }
    }
}
