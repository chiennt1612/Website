using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SSO.DBContext;
using SSO.Entities;
using SSO.Helpers;
using SSO.Models.UserRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace SSO.Controllers
{
    [SecurityHeaders]
    [CustomizeAuthorize(Roles = "Admin")]
    public class UserRolesController : Controller
    {
        private readonly AppDbContext _context;

        public UserRolesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UserRoles
        public async Task<IActionResult> Index(string Keyword, int? page)
        {
            //var a = IEnumerable<UserRoleList>
            int pageSize = 10;
            int pageIndex = 1;
            pageIndex = page.HasValue ? page.Value : 1;
            IEnumerable<UserRoleList> r;

            if (!String.IsNullOrEmpty(Keyword))
            {
                ViewData["Keyword"] = Keyword;
                r = (await _context.UserRoles.ToListAsync()).Join(
                    (await _context.Users.Where(u => u.IsUserAdmin && (
                        u.Email.Contains(Keyword) || u.PhoneNumber.Contains(Keyword) || u.UserName.Contains(Keyword)
                    )).ToListAsync()),
                    userroles => userroles.UserId,
                    users => users.Id,
                    (userroles, users) => new
                    {
                        UserId = userroles.UserId,
                        UserName = users.UserName,
                        Email = users.Email,
                        RoleId = userroles.RoleId
                    }).Join(
                    (await _context.Roles.ToListAsync()),
                    userroles => userroles.RoleId,
                    roles => roles.Id,
                    (userroles, roles) => new
                    {
                        UserId = userroles.UserId,
                        UserName = userroles.UserName,
                        Email = userroles.Email,
                        RoleId = userroles.RoleId,
                        RoleName = roles.Name
                    }).Select(u => new UserRoleList()
                    {
                        UserId = u.UserId,
                        UserName = u.UserName,
                        Email = u.Email,
                        RoleId = u.RoleId,
                        RoleName = u.RoleName
                    });
            }
            else
            {
                ViewData["Keyword"] = "";
                r = (await _context.UserRoles.ToListAsync()).Join(
                    (await _context.Users.Where(u => u.IsUserAdmin).ToListAsync()),
                    userroles => userroles.UserId,
                    users => users.Id,
                    (userroles, users) => new
                    {
                        UserId = userroles.UserId,
                        UserName = users.UserName,
                        Email = users.Email,
                        RoleId = userroles.RoleId
                    }).Join(
                    (await _context.Roles.ToListAsync()),
                    userroles => userroles.RoleId,
                    roles => roles.Id,
                    (userroles, roles) => new
                    {
                        UserId = userroles.UserId,
                        UserName = userroles.UserName,
                        Email = userroles.Email,
                        RoleId = userroles.RoleId,
                        RoleName = roles.Name
                    }).Select(u => new UserRoleList()
                    {
                        UserId = u.UserId,
                        UserName = u.UserName,
                        Email = u.Email,
                        RoleId = u.RoleId,
                        RoleName = u.RoleName
                    });
            }

            IPagedList<UserRoleList> a = await r.ToPagedListAsync(pageIndex, pageSize);

            return View("/Views/Admin/UserRoles/Index.cshtml", a);
        }

        // GET: UserRoles/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRole = await _context.UserRoles
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userRole == null)
            {
                return NotFound();
            }

            return View("/Views/Admin/UserRoles/Details.cshtml", userRole);
        }

        // GET: UserRoles/Create
        public async Task<IActionResult> Create()
        {
            ViewData["UserId"] = new SelectList((await _context.Users.Where(u => u.IsUserAdmin).ToListAsync()), "Id", "UserName");
            ViewData["RoleId"] = new SelectList((await _context.Roles.Where(u => u.Id > 0).ToListAsync()), "Id", "Name");
            return View("/Views/Admin/UserRoles/Create.cshtml");
        }

        // POST: UserRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserRole userRole)
        {
            if (ModelState.IsValid)
            {
                //ViewData["UserId"] = (await _context.Users.Where(u => u.IsUserAdmin).ToListAsync());
                //ViewData["RoleId"] = (await _context.Roles.ToListAsync());
                _context.Add(userRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("/Views/Admin/UserRoles/Create.cshtml", userRole);
        }

        // GET: UserRoles/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList((await _context.Users.Where(u => u.IsUserAdmin).ToListAsync()), "Id", "UserName");
            ViewData["RoleId"] = new SelectList((await _context.Roles.Where(u => u.Id > 0).ToListAsync()), "Id", "Name");
            var userRole = await _context.UserRoles.FindAsync(id);
            if (userRole == null)
            {
                return NotFound();
            }
            return View("/Views/Admin/UserRoles/Edit.cshtml", userRole);
        }

        // POST: UserRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, UserRole userRole)
        {
            if (id != userRole.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //ViewData["UserId"] = (await _context.Users.Where(u => u.IsUserAdmin).ToListAsync());
                    //ViewData["RoleId"] = (await _context.Roles.ToListAsync());
                    _context.Update(userRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserRoleExists(userRole.UserId))
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
            return View("/Views/Admin/UserRoles/Edit.cshtml", userRole);
        }

        // GET: UserRoles/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRole = await _context.UserRoles
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userRole == null)
            {
                return NotFound();
            }

            return View("/Views/Admin/UserRoles/Delete.cshtml", userRole);
        }

        // POST: UserRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var userRole = await _context.UserRoles.FindAsync(id);
            _context.UserRoles.Remove(userRole);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserRoleExists(long id)
        {
            return _context.UserRoles.Any(e => e.UserId == id);
        }
    }
}
