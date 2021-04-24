using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityFramework.Web.DBContext;
using EntityFramework.Web.Entities;
using WebAdmin.Repository.Interfaces;

namespace WebAdmin.Repository
{
    public class MenuSubFooterRepository : GenericRepository<MenuSubFooter, long>, IMenuSubFooterRepository
    {
        public MenuSubFooterRepository(AppDbContext dbContext, IHttpContextAccessor contextAccessor)
              : base(dbContext, contextAccessor)
        {
        }
    }
}
