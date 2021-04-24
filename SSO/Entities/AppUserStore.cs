using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SSO.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
