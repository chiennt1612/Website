using EntityFramework.Web.DBContext;
using EntityFramework.Web.Entities;
using Microsoft.AspNetCore.Http;
using WebClient.Repository.Interfaces;

namespace WebClient.Repository
{
    public class MenuSubFooterRepository : GenericRepository<MenuSubFooter, long>, IMenuSubFooterRepository
    {
        public MenuSubFooterRepository(AppDbContext dbContext, IHttpContextAccessor contextAccessor)
              : base(dbContext, contextAccessor)
        {
        }
    }
}
