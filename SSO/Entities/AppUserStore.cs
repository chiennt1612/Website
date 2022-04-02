using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SSO.DBContext;

namespace SSO.Entities
{
    public class AppUserStore : UserStore<AppUser, AppRole, AppDbContext, long>
    {
        public AppUserStore(AppDbContext context, IdentityErrorDescriber describer = null)
            : base(context, describer)
        {
        }
    }
}
