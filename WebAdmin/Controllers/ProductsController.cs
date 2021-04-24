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
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = Constants.PageSize;
            int pageIndex = 1;
            pageIndex = page.HasValue ? page.Value : 1;

            Expression<Func<Product, bool>> sqlWhere = item => (item.IsDeleted == false);
            Func<Product, string> sqlOrder = s => "DateCreator";

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
        public async Task<IActionResult> Create([Bind("Code,Name,CategoryMain,CategoryReference,Img,ImgSlide1,ImgSlide2,ImgSlide3,ImgSlide4,ImgSlide5,Summary,Description,IsNews,Status,Discount,Quota,IsSale,IsHot,MetaTitle,MetaDescription,MetaKeyword,MetaBox,MetaRobotTag,Id,UserCreator,DateCreator,UserModify,DateModify,IsDeleted,UserDeleted,DateDeleted")] Product product)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(product);
                //await _context.SaveChangesAsync();
                await _service.AddAsync(product);
                return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Edit(long id, [Bind("Code,Name,CategoryMain,CategoryReference,Img,ImgSlide1,ImgSlide2,ImgSlide3,ImgSlide4,ImgSlide5,Summary,Description,IsNews,Status,Discount,Quota,IsSale,IsHot,MetaTitle,MetaDescription,MetaKeyword,MetaBox,MetaRobotTag,Id,UserCreator,DateCreator,UserModify,DateModify,IsDeleted,UserDeleted,DateDeleted")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(product);
                    //await _context.SaveChangesAsync();
                    await _service.UpdateAsync(product);
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
