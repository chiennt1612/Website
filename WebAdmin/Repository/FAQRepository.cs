using EntityFramework.Web.DBContext;
using EntityFramework.Web.Entities;
using Microsoft.AspNetCore.Http;
using WebAdmin.Repository.Interfaces;

namespace WebAdmin.Repository
{
    public class FAQRepository : GenericRepository<FAQ, long>, IFAQRepository
    {
        private readonly AppDbContext _context;
        public FAQRepository(AppDbContext dbContext, IHttpContextAccessor contextAccessor)
            : base(dbContext, contextAccessor)
        {
            _context = dbContext;
        }
    }
}
