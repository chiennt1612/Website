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
    public class OrdersController : Controller
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IStringLocalizer<OrdersController> _localizer;
        private readonly IUnitOfWork _service;

        public OrdersController(IUnitOfWork _service, ILogger<OrdersController> logger, IStringLocalizer<OrdersController> localizer)
        {
            _logger = logger;
            _localizer = localizer;
            this._service = _service;
        }

        // GET: Orders
        public async Task<IActionResult> Index(string Keyword, int? page)
        {
            int pageSize = Constants.PageSize;
            int pageIndex = 1;
            pageIndex = page.HasValue ? page.Value : 1;

            Expression<Func<Order, bool>> sqlWhere;
            if (!String.IsNullOrEmpty(Keyword))
            {
                ViewData["Keyword"] = Keyword;
                sqlWhere = item => (
                item.StatusId != 6 &&
                (item.Fullname.Contains(Keyword) || item.Address.Contains(Keyword) || item.Email.Contains(Keyword) ||
                item.Mobile.Contains(Keyword))
                );
            }
            else
            {
                ViewData["Keyword"] = "";
                sqlWhere = item => (item.StatusId != 6);
            }
            Func<Order, object> sqlOrder = s => s.OrderDate;

            return View(await _service.orderRepository.GetListByPage(sqlWhere, sqlOrder, true, pageIndex, pageSize));
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return View(await _service.orderRepository.GetByIdAsync(id.Value));
            //var order = await _context.Orders
            //    .Include(o => o.Address)
            //    .Include(o => o.OrderStatus)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (order == null)
            //{
            //    return NotFound();
            //}

            //return View(order);
        }

        //// GET: Orders/Create
        //public IActionResult Create()
        //{
        //    ViewData["AddressId"] = new SelectList(_context.Addresss, "Id", "Id");
        //    ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Id");
        //    return View();
        //}

        //// POST: Orders/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(Order order)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(order);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["AddressId"] = new SelectList(_context.Addresss, "Id", "Id", order.AddressId);
        //    ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Id", order.StatusId);
        //    return View(order);
        //}

        //// GET: Orders/Edit/5
        //public async Task<IActionResult> Edit(long? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var order = await _context.Orders.FindAsync(id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["AddressId"] = new SelectList(_context.Addresss, "Id", "Id", order.AddressId);
        //    ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Id", order.StatusId);
        //    return View(order);
        //}

        //// POST: Orders/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(long id, Order order)
        //{
        //    if (id != order.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(order);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!OrderExists(order.Id))
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
        //    ViewData["AddressId"] = new SelectList(_context.Addresss, "Id", "Id", order.AddressId);
        //    ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Id", order.StatusId);
        //    return View(order);
        //}

        //// GET: Orders/Delete/5
        //public async Task<IActionResult> Delete(long? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var order = await _context.Orders
        //        .Include(o => o.Address)
        //        .Include(o => o.OrderStatus)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(order);
        //}

        //// POST: Orders/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(long id)
        //{
        //    var order = await _context.Orders.FindAsync(id);
        //    _context.Orders.Remove(order);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool OrderExists(long id)
        //{
        //    return _context.Orders.Any(e => e.Id == id);
        //}
    }
}
