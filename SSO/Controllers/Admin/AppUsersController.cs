using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SSO.DBContext;
using SSO.Entities;
using SSO.Helpers;
using SSO.Models.AccountViewModels;
using SSO.Services.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace SSO.Controllers.Admin
{
    [SecurityHeaders]
    [CustomizeAuthorize(Roles = "Admin")]
    public class AppUsersController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger _logger;
        private readonly IEmailSender _emailSender;

        public AppUsersController(UserManager<AppUser> userManager, AppDbContext context, IEmailSender emailSender, ILogger<AppUsersController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        // GET: AppUsers
        public async Task<IActionResult> Index(string Keyword, string UserType, int? page)
        {
            int pageSize = 10;
            int pageIndex = 1;
            pageIndex = page.HasValue ? page.Value : 1;
            IQueryable<AppUser> r;

            if (String.IsNullOrEmpty(UserType))
            {
                UserType = "1";
            }
            ViewData["UserType"] = UserType;

            if (!String.IsNullOrEmpty(Keyword))
            {
                ViewData["Keyword"] = Keyword;
                r = _context.Users
                .Where(a => (a.IsUserAdmin && UserType == "1" || UserType != "1" && !a.IsUserAdmin) && (
                    a.Email.Contains(Keyword) || a.PhoneNumber.Contains(Keyword) || a.UserName.Contains(Keyword)
                    )).OrderBy(u => u.UserName);
            }
            else
            {
                ViewData["Keyword"] = "";
                r = _context.Users
                .Where(a => (a.IsUserAdmin && UserType == "1" || UserType != "1" && !a.IsUserAdmin)).OrderBy(u => u.UserName);
            }

            IPagedList<AppUser> a = await r.ToPagedListAsync(pageIndex, pageSize);
            return View("/Views/Admin/AppUsers/Index.cshtml", a);
        }

        // GET: AppUsers/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appUser == null)
            {
                return NotFound();
            }

            return View("/Views/Admin/AppUsers/Details.cshtml", appUser);
        }

        // GET: AppUsers/Create
        public IActionResult Create()
        {
            return View("/Views/Admin/AppUsers/Create.cshtml");
        }

        // POST: AppUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterUser model)
        {
            _logger.LogInformation($"User created a new account with {JsonConvert.SerializeObject(model)} is {ModelState.IsValid}.");
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser()
                {
                    IsUserAdmin = true,
                    UserName = (!String.IsNullOrEmpty(model.UserName) ? model.UserName : "Re_" + model.Email.Substring(0, model.Email.ToString().IndexOf("@")).Replace(".", "")),
                    Email = model.Email,
                    OldId = "0",
                    TwoFactorEnabled = true,
                    EmailConfirmed = true
                };
                var result = await _userManager.CreateAsync(appUser, model.Password);
                if (result.Succeeded)
                {
                    //_logger.LogInformation("User created a new account with password.");

                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
                    //var callbackUrl = Url.EmailConfirmationLink(appUser.Id.ToString(), code, Request.Scheme);
                    //await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                    _logger.LogInformation("User created a new account with password.");
                }
                //_context.Add(appUser);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("/Views/Admin/AppUsers/Create.cshtml", model);
        }

        [CustomizeAuthorize(Roles = "AYs")]
        // GET: AppUsers/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _context.Users.FindAsync(id);
            if (appUser == null)
            {
                return NotFound();
            }
            return View("/Views/Admin/AppUsers/Edit.cshtml", appUser);
        }

        // POST: AppUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [CustomizeAuthorize(Roles = "AYs")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, AppUser appUser1)
        {
            if (id != appUser1.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var appUser = await _context.Users.FindAsync(id);
                    appUser.OldId = appUser1.OldId;
                    _context.Update(appUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppUserExists(appUser1.Id))
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
            return View("/Views/Admin/AppUsers/Edit.cshtml", appUser1);
        }

        // GET: AppUsers/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appUser == null)
            {
                return NotFound();
            }

            return View("/Views/Admin/AppUsers/Delete.cshtml", appUser);
        }

        // POST: AppUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var appUser = await _context.Users.FindAsync(id);
            _context.Users.Remove(appUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppUserExists(long id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
