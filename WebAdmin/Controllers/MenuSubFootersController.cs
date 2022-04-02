using EntityFramework.Web.DBContext;
using EntityFramework.Web.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    [Authorize(Roles = "Admin")]
    public class MenuSubFootersController : Controller
    {
        private readonly ILogger<MenuSubFootersController> _logger;
        private readonly IStringLocalizer<MenuSubFootersController> _localizer;
        private readonly IMenuSubFooterServices _service;

        public MenuSubFootersController(IMenuSubFooterServices _service, ILogger<MenuSubFootersController> logger, IStringLocalizer<MenuSubFootersController> localizer)
        {
            _logger = logger;
            _localizer = localizer;
            this._service = _service;
        }

        // GET: MenuSubFooters
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = Constants.PageSize;
            int pageIndex = 1;
            pageIndex = page.HasValue ? page.Value : 1;

            Expression<Func<MenuSubFooter, bool>> sqlWhere = item => (item.IsDeleted == false);
            Func<MenuSubFooter, object> sqlOrder = s => s.Id;

            return View(await _service.GetListAsync(sqlWhere, sqlOrder, true, pageIndex, pageSize));
        }

        // GET: MenuSubFooters/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var menuSubFooter = await _context.MenuSubFooters
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (menuSubFooter == null)
            //{
            //    return NotFound();
            //}

            //return View(menuSubFooter);
            return View(await _service.GetByIdAsync(id.Value));
        }

        // GET: MenuSubFooters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MenuSubFooters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MenuSubFooter menuSubFooter)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(menuSubFooter);
                //await _context.SaveChangesAsync();
                await _service.AddAsync(menuSubFooter);
                return RedirectToAction(nameof(Index));
            }
            return View(menuSubFooter);
        }

        // GET: MenuSubFooters/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuSubFooter = await _service.GetByIdAsync(id.Value); //await _context.MenuSubFooters.FindAsync(id);
            if (menuSubFooter == null)
            {
                return NotFound();
            }
            return View(menuSubFooter);
        }

        // POST: MenuSubFooters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, MenuSubFooter menuSubFooter)
        {
            if (id != menuSubFooter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(menuSubFooter);
                    //await _context.SaveChangesAsync();
                    await _service.UpdateAsync(menuSubFooter);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await MenuSubFooterExists(menuSubFooter.Id))
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
            return View(menuSubFooter);
        }

        // GET: MenuSubFooters/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuSubFooter = await _service.GetByIdAsync(id.Value);
            //await _context.MenuSubFooters
            //.FirstOrDefaultAsync(m => m.Id == id);
            if (menuSubFooter == null)
            {
                return NotFound();
            }

            return View(menuSubFooter);
        }

        // POST: MenuSubFooters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            //var menuSubFooter = await _context.MenuSubFooters.FindAsync(id);
            //_context.MenuSubFooters.Remove(menuSubFooter);
            //await _context.SaveChangesAsync();
            await _service.DeleteByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> MenuSubFooterExists(long id)
        {
            return (await _service.GetAllAsync()).Any(e => e.Id == id);
        }
    }
}
