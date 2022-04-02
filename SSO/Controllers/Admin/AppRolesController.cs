using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SSO.DBContext;
using SSO.Entities;
using SSO.Helpers;
using System.Linq;
using System.Threading.Tasks;

namespace SSO.Controllers.Admin
{
    [SecurityHeaders]
    [CustomizeAuthorize(Roles = "Admin")]
    public class AppRolesController : Controller
    {
        private readonly AppDbContext _context;

        public AppRolesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: AppRoles
        public async Task<IActionResult> Index()
        {
            return View("/Views/Admin/AppRoles/Index.cshtml", await _context.Roles.Where(u => u.Id > 0).ToListAsync());
        }

        // GET: AppRoles/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appRole = await _context.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appRole == null)
            {
                return NotFound();
            }

            return View("/Views/Admin/AppRoles/Details.cshtml", appRole);
        }

        // GET: AppRoles/Create
        public IActionResult Create()
        {
            return View("/Views/Admin/AppRoles/Create.cshtml");
        }

        // POST: AppRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppRole appRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("/Views/Admin/AppRoles/Create.cshtml", appRole);
        }

        // GET: AppRoles/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appRole = await _context.Roles.FindAsync(id);
            if (appRole == null)
            {
                return NotFound();
            }
            return View("/Views/Admin/AppRoles/Edit.cshtml", appRole);
        }

        // POST: AppRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, AppRole appRole)
        {
            if (id != appRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppRoleExists(appRole.Id))
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
            return View("/Views/Admin/AppRoles/Edit.cshtml", appRole);
        }

        // GET: AppRoles/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appRole = await _context.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appRole == null)
            {
                return NotFound();
            }

            return View("/Views/Admin/AppRoles/Delete.cshtml", appRole);
        }

        // POST: AppRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var appRole = await _context.Roles.FindAsync(id);
            _context.Roles.Remove(appRole);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppRoleExists(long id)
        {
            return _context.Roles.Any(e => e.Id == id);
        }
    }
}
