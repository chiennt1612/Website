using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SSO.DBContext;

namespace SSO.Entities
{
    public class AppRoleStore : RoleStore<AppRole, AppDbContext, long>
    {
        public AppRoleStore(AppDbContext context, IdentityErrorDescriber describer = null)
            : base(context, describer)
        {
        }
    }
}
