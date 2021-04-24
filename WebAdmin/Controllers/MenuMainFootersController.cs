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
using WebAdmin.Services.Interfaces;

namespace WebAdmin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MenuMainFootersController : Controller
    {
        private readonly ILogger<MenuMainFootersController> _logger;
        private readonly IStringLocalizer<MenuMainFootersController> _localizer;
        private readonly IMenuMainFooterServices _service;

        public MenuMainFootersController(IMenuMainFooterServices _service, ILogger<MenuMainFootersController> logger, IStringLocalizer<MenuMainFootersController> localizer)
        {
            _logger = logger;
            _localizer = localizer;
            this._service = _service;
        }

        // GET: MenuMainFooters
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = Constants.PageSize;
            int pageIndex = 1;
            pageIndex = page.HasValue ? page.Value : 1;

            Expression<Func<MenuMainFooter, bool>> sqlWhere = item => (item.IsDeleted == false);
            Func<MenuMainFooter, string> sqlOrder = s => "DateCreator";

            return View(await _service.GetListAsync(sqlWhere, sqlOrder, true, pageIndex, pageSize));
        }

        // GET: MenuMainFooters/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var menuMainFooter = await _context.MenuMainFooters
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (menuMainFooter == null)
            //{
            //    return NotFound();
            //}

            return View(await _service.GetByIdAsync(id.Value));
        }

        // GET: MenuMainFooters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MenuMainFooters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UrlText,UrlAddress,Status,Id,UserCreator,DateCreator,UserModify,DateModify,IsDeleted,UserDeleted,DateDeleted")] MenuMainFooter menuMainFooter)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(menuMainFooter);
                //await _context.SaveChangesAsync();
                await _service.AddAsync(menuMainFooter);
                return RedirectToAction(nameof(Index));
            }
            return View(menuMainFooter);
        }

        // GET: MenuMainFooters/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuMainFooter = await _service.GetByIdAsync(id.Value); //await _context.MenuMainFooters.FindAsync(id);
            if (menuMainFooter == null)
            {
                return NotFound();
            }
            return View(menuMainFooter);
        }

        // POST: MenuMainFooters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("UrlText,UrlAddress,Status,Id,UserCreator,DateCreator,UserModify,DateModify,IsDeleted,UserDeleted,DateDeleted")] MenuMainFooter menuMainFooter)
        {
            if (id != menuMainFooter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(menuMainFooter);
                    //await _context.SaveChangesAsync();
                    await _service.UpdateAsync(menuMainFooter);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await MenuMainFooterExists(menuMainFooter.Id))
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
            return View(menuMainFooter);
        }

        // GET: MenuMainFooters/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuMainFooter = await _service.GetByIdAsync(id.Value);
                //await _context.MenuMainFooters
                //.FirstOrDefaultAsync(m => m.Id == id);
            if (menuMainFooter == null)
            {
                return NotFound();
            }

            return View(menuMainFooter);
        }

        // POST: MenuMainFooters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            //var menuMainFooter = await _context.MenuMainFooters.FindAsync(id);
            //_context.MenuMainFooters.Remove(menuMainFooter);
            //await _context.SaveChangesAsync();
            await _service.DeleteByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> MenuMainFooterExists(long id)
        {
            return (await _service.GetAllAsync()).Any(e => e.Id == id);
        }
    }
}
