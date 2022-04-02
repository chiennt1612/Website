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

namespace WebApplication4.Controllers
{
    [SecurityHeaders]
    [Authorize(Roles = "Admin,Mod")]
    public class FAQsController : Controller
    {
        private readonly ILogger<FAQsController> _logger;
        private readonly IStringLocalizer<FAQsController> _localizer;
        private readonly IFAQServices _service;

        public FAQsController(IFAQServices _service, ILogger<FAQsController> logger, IStringLocalizer<FAQsController> localizer)
        {
            _logger = logger;
            _localizer = localizer;
            this._service = _service;
        }

        // GET: FAQs
        public async Task<IActionResult> Index(int? page, string Keyword)
        {
            int pageSize = Constants.PageSize;
            int pageIndex = 1;
            pageIndex = page.HasValue ? page.Value : 1;

            Expression<Func<FAQ, bool>> sqlWhere;
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
            Func<FAQ, object> sqlOrder = s => s.Id;

            return View(await _service.GetListAsync(sqlWhere, sqlOrder, true, pageIndex, pageSize));
        }

        // GET: FAQs/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return View(await _service.GetByIdAsync(id.Value));
            //var fAQ = await _context.FAQs
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (fAQ == null)
            //{
            //    return NotFound();
            //}

            //return View(fAQ);
        }

        // GET: FAQs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FAQs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FAQ fAQ)
        {
            if (ModelState.IsValid)
            {
                if (fAQ.Description != null) fAQ.Description = fAQ.Description.Replace(@"../../Upload/", Util.UrlHostUpload(Request));
                if (fAQ.Summary != null) fAQ.Summary = fAQ.Summary.Replace(@"../../Upload/", Util.UrlHostUpload(Request));
                await _service.AddAsync(fAQ);
                return RedirectToAction(nameof(Index));
                //_context.Add(fAQ);
                //await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }
            return View(fAQ);
        }

        // GET: FAQs/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var fAQ = await _service.GetByIdAsync(id.Value);
            //var fAQ = await _context.FAQs.FindAsync(id);
            if (fAQ == null)
            {
                return NotFound();
            }
            return View(fAQ);
        }

        // POST: FAQs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, FAQ fAQ)
        {
            if (id != fAQ.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (fAQ.Description != null) fAQ.Description = fAQ.Description.Replace(@"../../Upload/", Util.UrlHostUpload(Request));
                    if (fAQ.Summary != null) fAQ.Summary = fAQ.Summary.Replace(@"../../Upload/", Util.UrlHostUpload(Request));
                    await _service.UpdateAsync(fAQ);
                    //_context.Update(fAQ);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await FAQExists(fAQ.Id))
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
            return View(fAQ);
        }

        // GET: FAQs/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var fAQ = await _service.GetByIdAsync(id.Value);
            //var fAQ = await _context.FAQs
            //    .FirstOrDefaultAsync(m => m.Id == id);
            if (fAQ == null)
            {
                return NotFound();
            }

            return View(fAQ);
        }

        // POST: FAQs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            //var fAQ = await _context.FAQs.FindAsync(id);
            //_context.FAQs.Remove(fAQ);
            //await _context.SaveChangesAsync();
            await _service.DeleteByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> FAQExists(long id)
        {
            return (await _service.GetAllAsync()).Any(e => e.Id == id);
        }
    }
}
