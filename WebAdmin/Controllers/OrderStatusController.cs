using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebAdmin.Helpers;
using WebAdmin.Repository.Interfaces;

namespace WebApplication4.Controllers
{
    [SecurityHeaders]
    [Authorize(Roles = "Admin,Mod")]
    public class OrderStatusController : Controller
    {
        private readonly ILogger<OrderStatusController> _logger;
        private readonly IStringLocalizer<OrderStatusController> _localizer;
        private readonly IUnitOfWork _service;

        public OrderStatusController(IUnitOfWork _service, ILogger<OrderStatusController> logger, IStringLocalizer<OrderStatusController> localizer)
        {
            _logger = logger;
            _localizer = localizer;
            this._service = _service;
        }

        // GET: OrderStatus
        public async Task<IActionResult> Index()
        {
            return View(await _service.orderStatusRepository.GetAllAsync());
        }

        // GET: OrderStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return View(await _service.orderStatusRepository.GetByIdAsync(id.Value));
            //var orderStatus = await _context.Status
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (orderStatus == null)
            //{
            //    return NotFound();
            //}

            //return View(orderStatus);
        }

        //// GET: OrderStatus/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: OrderStatus/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(OrderStatus orderStatus)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(orderStatus);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(orderStatus);
        //}

        //// GET: OrderStatus/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var orderStatus = await _context.Status.FindAsync(id);
        //    if (orderStatus == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(orderStatus);
        //}

        //// POST: OrderStatus/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, OrderStatus orderStatus)
        //{
        //    if (id != orderStatus.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(orderStatus);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!OrderStatusExists(orderStatus.Id))
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
        //    return View(orderStatus);
        //}

        //// GET: OrderStatus/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var orderStatus = await _context.Status
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (orderStatus == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(orderStatus);
        //}

        //// POST: OrderStatus/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var orderStatus = await _context.Status.FindAsync(id);
        //    _context.Status.Remove(orderStatus);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool OrderStatusExists(int id)
        //{
        //    return _context.Status.Any(e => e.Id == id);
        //}
    }
}
