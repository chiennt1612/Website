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
using WebAdmin.Repository.Interfaces;
using WebAdmin.Services.Interfaces;

namespace WebApplication4.Controllers
{
    [SecurityHeaders]
    [Authorize(Roles = "Admin,Mod")]
    public class ServicesController : Controller
    {
        private readonly ILogger<ServicesController> _logger;
        private readonly IStringLocalizer<ServicesController> _localizer;
        private readonly IServiceServices _service;
        private readonly IUnitOfWork _unitOfWork;

        public ServicesController(IUnitOfWork _unitOfWork, IServiceServices _service, ILogger<ServicesController> logger, IStringLocalizer<ServicesController> localizer)
        {
            _logger = logger;
            _localizer = localizer;
            this._service = _service;
            this._unitOfWork = _unitOfWork;
        }

        // GET: Services
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = Constants.PageSize;
            int pageIndex = 1;
            pageIndex = page.HasValue ? page.Value : 1;

            Expression<Func<Service, bool>> sqlWhere = item => (item.IsDeleted == false);
            Func<Service, object> sqlOrder = s => s.Id;

            return View(await _service.GetListAsync(sqlWhere, sqlOrder, true, pageIndex, pageSize));
        }


        public async Task<IActionResult> Register(int? page, string Keyword)
        {
            int pageSize = Constants.PageSize;
            int pageIndex = 1;
            pageIndex = page.HasValue ? page.Value : 1;

            Expression<Func<Contact, bool>> sqlWhere;
            if (!String.IsNullOrEmpty(Keyword))
            {
                ViewData["Keyword"] = Keyword;
                sqlWhere = item => (item.ServiceId.HasValue &&
                (item.Fullname.Contains(Keyword) || item.Address.Contains(Keyword) || item.Mobile.Contains(Keyword) || item.Email.Contains(Keyword) || item.Description.Contains(Keyword))
                );
            }
            else
            {
                ViewData["Keyword"] = "";
                sqlWhere = item => (item.ServiceId.HasValue);
            }
            Func<Contact, object> sqlOrder = s => s.Id;

            return View(await _unitOfWork.contactRepository.GetListByPage(sqlWhere, sqlOrder, true, pageIndex, pageSize));
        }

        public async Task<IActionResult> RegisterDetail(long Id)
        {
            return View("/Views/Services/RegisterDetail.cshtml", await _unitOfWork.contactRepository.GetByIdAsync(Id));
        }

        // GET: Services/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return View(await _service.GetByIdAsync(id.Value));
            //var service = await _context.Services
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (service == null)
            //{
            //    return NotFound();
            //}

            //return View(service);
        }

        // GET: Services/Create
        public IActionResult Create()
        {
            ViewData["GroupIdList"] = Paygate.OnePay.Tools.GroupServiceList();
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Service service)
        {
            var a = HttpContext.Request.Form["GroupIdList"];
            service.GroupIdList = a;
            ViewData["GroupIdList"] = Paygate.OnePay.Tools.GroupServiceList(a);
            if (ModelState.IsValid)
            {
                if (service.Description != null) service.Description = service.Description.Replace(@"../../Upload/", Util.UrlHostUpload(Request));
                if (service.Summary != null) service.Summary = service.Summary.Replace(@"../../Upload/", Util.UrlHostUpload(Request));
                await _service.AddAsync(service);
                //_context.Add(service);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(service);
        }

        // GET: Services/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var service = await _service.GetByIdAsync(id.Value);
            //var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            ViewData["GroupIdList"] = Paygate.OnePay.Tools.GroupServiceList(service.GroupIdList);
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Service service)
        {
            var a = HttpContext.Request.Form["GroupIdList"];
            service.GroupIdList = a;
            ViewData["GroupIdList"] = Paygate.OnePay.Tools.GroupServiceList(a);
            if (id != service.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (service.Description != null) service.Description = service.Description.Replace(@"../../Upload/", Util.UrlHostUpload(Request));
                    if (service.Summary != null) service.Summary = service.Summary.Replace(@"../../Upload/", Util.UrlHostUpload(Request));
                    await _service.UpdateAsync(service);
                    //_context.Update(service);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ServiceExists(service.Id))
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
            return View(service);
        }

        // GET: Services/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var service = await _service.GetByIdAsync(id.Value);
            //var service = await _context.Services
            //    .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            //var service = await _context.Services.FindAsync(id);
            //_context.Services.Remove(service);
            //await _context.SaveChangesAsync();
            await _service.DeleteByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ServiceExists(long id)
        {
            return (await _service.GetAllAsync()).Any(e => e.Id == id);
        }
    }
}
