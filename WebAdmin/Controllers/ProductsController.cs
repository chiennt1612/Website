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
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IStringLocalizer<ProductsController> _localizer;
        private readonly IProductServices _service;

        public ProductsController(IProductServices _service, ILogger<ProductsController> logger, IStringLocalizer<ProductsController> localizer)
        {
            _logger = logger;
            _localizer = localizer;
            this._service = _service;
        }

        // GET: Products
        [Authorize(Roles = "Admin,Mod")]
        public async Task<IActionResult> Index(int? page, string Keyword, bool? IsNotCategory)
        {
            int pageSize = Constants.PageSize;
            int pageIndex = 1;
            pageIndex = page.HasValue ? page.Value : 1;

            bool INC = (IsNotCategory.HasValue ? IsNotCategory.Value : false);
            ViewData["IsNotCategory"] = INC;

            Expression<Func<Product, bool>> sqlWhere;
            if (!String.IsNullOrEmpty(Keyword))
            {
                ViewData["Keyword"] = Keyword;
                sqlWhere = item => (
                (INC && !item.CategoryMain.HasValue || !INC && item.CategoryMain.HasValue) &&
                item.IsDeleted == false &&
                (item.Code.Contains(Keyword) || item.Name.Contains(Keyword) || item.Summary.Contains(Keyword) || item.Description.Contains(Keyword))
                );
            }
            else
            {
                ViewData["Keyword"] = "";
                sqlWhere = item => ((INC && !item.CategoryMain.HasValue || !INC && item.CategoryMain.HasValue) && item.IsDeleted == false);
            }
            Func<Product, object> sqlOrder = s => s.Id;

            return View(await _service.GetListAsync(sqlWhere, sqlOrder, true, pageIndex, pageSize));
        }

        // GET: Products/Details/5
        [Authorize(Roles = "Admin,Mod")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var product = await _context.Products
            //    .Include(p => p.Categories)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (product == null)
            //{
            //    return NotFound();
            //}

            return View(await _service.GetByIdAsync(id.Value));
        }

        // GET: Products/Create
        [Authorize(Roles = "Admin,Mod")]
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryMain"] = new SelectList(await _service.CateGetAllAsync(), "Id", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                // Check category
                if (!product.CategoryMain.HasValue)
                {
                    ModelState.AddModelError("CategoryMain", _localizer.GetString("Trường bắt buộc, không để trống"));
                }
                else
                {
                    //_context.Add(product);
                    //await _context.SaveChangesAsync();
                    await _service.AddAsync(product);
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["CategoryMain"] = new SelectList(await _service.CateGetAllAsync(), "Id", "Name", product.CategoryMain);
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Admin,Mod")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _service.GetByIdAsync(id.Value);
            //await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryMain"] = new SelectList(await _service.CateGetAllAsync(), "Id", "Name", product.CategoryMain);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Mod")]
        public async Task<IActionResult> Edit(long id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Check category
                    if (!product.CategoryMain.HasValue)
                    {
                        ModelState.AddModelError("CategoryMain", _localizer.GetString("Trường bắt buộc, không để trống"));
                        ViewData["CategoryMain"] = new SelectList(await _service.CateGetAllAsync(), "Id", "Name", product.CategoryMain);
                        return View(product);
                    }
                    else
                    {
                        //_context.Update(product);
                        //await _context.SaveChangesAsync();
                        await _service.UpdateAsync(product);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ProductExists(product.Id))
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
            ViewData["CategoryMain"] = new SelectList(await _service.CateGetAllAsync(), "Id", "Name", product.CategoryMain);
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _service.GetByIdAsync(id.Value);
            //await _context.Products
            //.Include(p => p.Categories)
            //.FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            //var product = await _context.Products.FindAsync(id);
            //_context.Products.Remove(product);
            //await _context.SaveChangesAsync();
            await _service.DeleteByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProductExists(long id)
        {
            return (await _service.GetAllAsync()).Any(e => e.Id == id);
        }
    }
}
