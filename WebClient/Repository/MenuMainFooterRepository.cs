using EntityFramework.Web.DBContext;
using EntityFramework.Web.Entities;
using Microsoft.AspNetCore.Http;
using WebClient.Repository.Interfaces;

namespace WebClient.Repository
{
    public class MenuMainFooterRepository : GenericRepository<MenuMainFooter, long>, IMenuMainFooterRepository
    {
        public MenuMainFooterRepository(AppDbContext dbContext, IHttpContextAccessor contextAccessor)
              : base(dbContext, contextAccessor)
        {
        }
    }
}
