using EntityFramework.Web.DBContext;
using EntityFramework.Web.Entities.Ordering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebAdmin.Helpers;
using WebAdmin.Repository.Interfaces;

namespace WebApplication4.Controllers
{
    [SecurityHeaders]
    [Authorize(Roles = "Admin,Mod")]
    public class AddressesController : Controller
    {
        private readonly ILogger<AddressesController> _logger;
        private readonly IStringLocalizer<AddressesController> _localizer;
        private readonly IUnitOfWork _service;

        public AddressesController(IUnitOfWork _service, ILogger<AddressesController> logger, IStringLocalizer<AddressesController> localizer)
        {
            _logger = logger;
            _localizer = localizer;
            this._service = _service;
        }

        // GET: Addresses
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = Constants.PageSize;
            int pageIndex = 1;
            pageIndex = page.HasValue ? page.Value : 1;

            Expression<Func<Address, bool>> sqlWhere = item => (true);
            Func<Address, object> sqlOrder = s => s.Id;

            return View(await _service.addressRepository.GetListByPage(sqlWhere, sqlOrder, true, pageIndex, pageSize));
        }

        // GET: Addresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return View(await _service.addressRepository.GetByIdAsync(id.Value));
            //var address = await _context.Addresss
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (address == null)
            //{
            //    return NotFound();
            //}

            //return View(address);
        }

        //// GET: Addresses/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Addresses/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(Address address)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(address);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(address);
        //}

        //// GET: Addresses/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var address = await _context.Addresss.FindAsync(id);
        //    if (address == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(address);
        //}

        //// POST: Addresses/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, Address address)
        //{
        //    if (id != address.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(address);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!AddressExists(address.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(address);
        //}

        //// GET: Addresses/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var address = await _context.Addresss
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (address == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(address);
        //}

        //// POST: Addresses/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var address = await _context.Addresss.FindAsync(id);
        //    _context.Addresss.Remove(address);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool AddressExists(int id)
        //{
        //    return _context.Addresss.Any(e => e.Id == id);
        //}
    }
}
