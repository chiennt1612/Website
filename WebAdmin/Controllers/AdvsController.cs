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

namespace WebApplication4.Controllers
{
    [SecurityHeaders]
    [Authorize(Roles = "Admin,Mod")]
    public class AdvsController : Controller
    {
        private readonly ILogger<AdvsController> _logger;
        private readonly IStringLocalizer<AdvsController> _localizer;
        private readonly IAdvServices _service;

        public AdvsController(IAdvServices _service, ILogger<AdvsController> logger, IStringLocalizer<AdvsController> localizer)
        {
            _logger = logger;
            _localizer = localizer;
            this._service = _service;
        }

        // GET: Advs
        public async Task<IActionResult> Index(int? page, string Keyword)
        {
            int pageSize = Constants.PageSize;
            int pageIndex = 1;
            pageIndex = page.HasValue ? page.Value : 1;

            Expression<Func<Adv, bool>> sqlWhere;
            if (!String.IsNullOrEmpty(Keyword))
            {
                ViewData["Keyword"] = Keyword;
                sqlWhere = item => (
                    item.IsDeleted == false &&
                    (
                        item.CustomerName.Contains(Keyword) || item.Mobile.Contains(Keyword) ||
                        item.Email.Contains(Keyword) || item.Website.Contains(Keyword) || item.AdvScript.Contains(Keyword)
                    )
                );
            }
            else
            {
                ViewData["Keyword"] = "";
                sqlWhere = item => (item.IsDeleted == false);
            }
            Func<Adv, object> sqlOrder = s => s.Id;

            return View(await _service.GetListAsync(sqlWhere, sqlOrder, true, pageIndex, pageSize));
        }

        // GET: Advs/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return View(await _service.GetByIdAsync(id.Value));
            //var adv = await _context.Advs
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (adv == null)
            //{
            //    return NotFound();
            //}

            //return View(adv);
        }

        // GET: Advs/Create
        public async Task<IActionResult> Create()
        {
            ViewData["Pos"] = new SelectList(await _service.PositionGetAllAsync(), "Id", "Name");
            return View();
        }

        // POST: Advs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Adv adv)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(adv);
                //await _context.SaveChangesAsync();
                if (adv.AdvScript != null) adv.AdvScript = adv.AdvScript.Replace(@"../../Upload/", Util.UrlHostUpload(Request));
                await _service.AddAsync(adv);
                return RedirectToAction(nameof(Index));
            }
            return View(adv);
        }

        // GET: Advs/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["Pos"] = new SelectList(await _service.PositionGetAllAsync(), "Id", "Name");
            var adv = await _service.GetByIdAsync(id.Value);
            //var adv = await _context.Advs.FindAsync(id);
            if (adv == null)
            {
                return NotFound();
            }
            return View(adv);
        }

        // POST: Advs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Adv adv)
        {
            if (id != adv.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(adv);
                    //await _context.SaveChangesAsync();
                    if (adv.AdvScript != null) adv.AdvScript = adv.AdvScript.Replace(@"../../Upload/", Util.UrlHostUpload(Request));
                    await _service.UpdateAsync(adv);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await AdvExists(adv.Id))
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
            return View(adv);
        }

        // GET: Advs/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var adv = await _service.GetByIdAsync(id.Value);
            //var adv = await _context.Advs
            //    .FirstOrDefaultAsync(m => m.Id == id);
            if (adv == null)
            {
                return NotFound();
            }

            return View(adv);
        }

        // POST: Advs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            //var adv = await _context.Advs.FindAsync(id);
            //_context.Advs.Remove(adv);
            //await _context.SaveChangesAsync();
            await _service.DeleteByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> AdvExists(long id)
        {
            return (await _service.GetAllAsync()).Any(e => e.Id == id);
        }
    }
}
