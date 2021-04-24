using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SSO.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
