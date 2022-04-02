using EntityFramework.Web.DBContext;
using EntityFramework.Web.Entities;
using Microsoft.AspNetCore.Http;
using WebNuoc.Repository.Interfaces;

namespace WebNuoc.Repository
{
    public class MenuSubFooterRepository : GenericRepository<MenuSubFooter, long>, IMenuSubFooterRepository
    {
        public MenuSubFooterRepository(AppDbContext dbContext, IHttpContextAccessor contextAccessor)
              : base(dbContext, contextAccessor)
        {
        }
    }
}
