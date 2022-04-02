using EntityFramework.Web.DBContext;
using EntityFramework.Web.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebAdmin.Helpers;
using WebAdmin.Repository.Interfaces;

namespace WebApplication4.Controllers
{
    [SecurityHeaders]
    [Authorize(Roles = "Admin,Mod,User")]
    public class ContactsController : Controller
    {
        private readonly ILogger<ContactsController> _logger;
        private readonly IStringLocalizer<ContactsController> _localizer;
        private readonly IUnitOfWork _service;

        public ContactsController(IUnitOfWork _service, ILogger<ContactsController> logger, IStringLocalizer<ContactsController> localizer)
        {
            _logger = logger;
            _localizer = localizer;
            this._service = _service;
        }

        // GET: Contacts
        public async Task<IActionResult> Index(int? page, string Keyword, string StatusType)
        {
            List<int> StatusNotPay = new List<int>() { 0, 1, 2, 6 };
            List<int> StatusPay = new List<int>() { 3, 4, 5 };
            int pageSize = Constants.PageSize;
            int pageIndex = 1;
            pageIndex = page.HasValue ? page.Value : 1;

            if (String.IsNullOrEmpty(StatusType)) StatusType = "1";
            ViewData["StatusType"] = StatusType;

            Expression<Func<Contact, bool>> sqlWhere;
            if (!String.IsNullOrEmpty(Keyword))
            {
                ViewData["Keyword"] = Keyword;
                sqlWhere = item => (!item.ServiceId.HasValue &&
                (item.Fullname.Contains(Keyword) || item.Address.Contains(Keyword) || item.Mobile.Contains(Keyword) || item.Email.Contains(Keyword) || item.Description.Contains(Keyword))
                ) && (StatusType == "0" && StatusNotPay.Contains(item.StatusId.Value) || (StatusType != "0" && (!item.StatusId.HasValue || StatusPay.Contains(item.StatusId.Value))));
            }
            else
            {
                ViewData["Keyword"] = "";
                sqlWhere = item => (!item.ServiceId.HasValue) && (StatusType == "0" && StatusNotPay.Contains(item.StatusId.Value) || (StatusType != "0" && (!item.StatusId.HasValue || StatusPay.Contains(item.StatusId.Value))));
            }
            Func<Contact, object> sqlOrder = s => s.Id;

            return View(await _service.contactRepository.GetListByPage(sqlWhere, sqlOrder, true, pageIndex, pageSize));
        }

        // GET: Contacts/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return View(await _service.contactRepository.GetByIdAsync(id.Value));
            //var contact = await _context.Contacts
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (contact == null)
            //{
            //    return NotFound();
            //}

            //return View(contact);
        }

        // GET: Contacts/Create
        public IActionResult Create()
        {
            return View();
        }

        //// POST: Contacts/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(Contact contact)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(contact);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(contact);
        //}

        //// GET: Contacts/Edit/5
        //public async Task<IActionResult> Edit(long? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var contact = await _context.Contacts.FindAsync(id);
        //    if (contact == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(contact);
        //}

        //// POST: Contacts/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(long id, Contact contact)
        //{
        //    if (id != contact.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(contact);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ContactExists(contact.Id))
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
        //    return View(contact);
        //}

        //// GET: Contacts/Delete/5
        //public async Task<IActionResult> Delete(long? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var contact = await _context.Contacts
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (contact == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(contact);
        //}

        //// POST: Contacts/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(long id)
        //{
        //    var contact = await _context.Contacts.FindAsync(id);
        //    _context.Contacts.Remove(contact);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool ContactExists(long id)
        //{
        //    return _context.Contacts.Any(e => e.Id == id);
        //}
    }
}
