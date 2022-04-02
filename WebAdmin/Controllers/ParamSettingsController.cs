using EntityFramework.Web.DBContext;
using EntityFramework.Web.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
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
    public class ParamSettingsController : Controller
    {
        private readonly ILogger<ParamSettingsController> _logger;
        private readonly IStringLocalizer<ParamSettingsController> _localizer;
        private readonly IParamSettingServices _service;
        public ParamSettingsController(IParamSettingServices _service, ILogger<ParamSettingsController> logger, IStringLocalizer<ParamSettingsController> localizer)
        {
            _logger = logger;
            _localizer = localizer;
            this._service = _service;
        }

        // GET: ParamSettings
        public async Task<IActionResult> Index(string Keyword, int? page)
        {
            int pageSize = Constants.PageSize;
            int pageIndex = 1;
            pageIndex = page.HasValue ? page.Value : 1;

            Expression<Func<ParamSetting, bool>> sqlWhere;
            if (!String.IsNullOrEmpty(Keyword))
            {
                ViewData["Keyword"] = Keyword;
                sqlWhere = item => (
                item.IsDeleted == false &&
                (item.ParamKey == Keyword || item.ParamValue.Contains(Keyword))
                );
            }
            else
            {
                ViewData["Keyword"] = "";
                sqlWhere = item => (item.IsDeleted == false);
            }

            Func<ParamSetting, object> sqlOrder = s => s.Id;

            return View(await _service.GetListAsync(sqlWhere, sqlOrder, true, pageIndex, pageSize));
        }

        // GET: ParamSettings/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var paramSetting = await _context.ParamSettings
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (paramSetting == null)
            //{
            //    return NotFound();
            //}

            return View(await _service.GetByIdAsync(id.Value));
        }

        // GET: ParamSettings/Create
        public IActionResult Create()
        {
            ParamSetting paramSetting = new ParamSetting() { Language = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name };
            return View(paramSetting);
        }

        // POST: ParamSettings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ParamSetting paramSetting)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(paramSetting);
                //await _context.SaveChangesAsync();
                await _service.AddAsync(paramSetting);
                return RedirectToAction(nameof(Index));
            }
            return View(paramSetting);
        }

        // GET: ParamSettings/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paramSetting = await _service.GetByIdAsync(id.Value); //await _context.ParamSettings.FindAsync(id);
            if (paramSetting == null)
            {
                return NotFound();
            }
            return View(paramSetting);
        }

        // POST: ParamSettings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, ParamSetting paramSetting)
        {
            if (id != paramSetting.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(paramSetting);
                    //await _context.SaveChangesAsync();
                    await _service.UpdateAsync(paramSetting);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ParamSettingExists(paramSetting.Id))
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
            return View(paramSetting);
        }

        // GET: ParamSettings/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paramSetting = await _service.GetByIdAsync(id.Value);
            //await _context.ParamSettings
            //.FirstOrDefaultAsync(m => m.Id == id);
            if (paramSetting == null)
            {
                return NotFound();
            }

            return View(paramSetting);
        }

        // POST: ParamSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            //var paramSetting = await _context.ParamSettings.FindAsync(id);
            //_context.ParamSettings.Remove(paramSetting);
            //await _context.SaveChangesAsync();
            await _service.DeleteByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ParamSettingExists(long id)
        {
            return (await _service.GetAllAsync()).Any(e => e.Id == id);
        }
    }
}
