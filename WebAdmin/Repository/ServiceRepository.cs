using EntityFramework.Web.DBContext;
using EntityFramework.Web.Entities;
using Microsoft.AspNetCore.Http;
using WebAdmin.Repository.Interfaces;

namespace WebAdmin.Repository
{
    public class ServiceRepository : GenericRepository<Service, long>, IServiceRepository
    {
        private readonly AppDbContext _context;
        public ServiceRepository(AppDbContext dbContext, IHttpContextAccessor contextAccessor)
            : base(dbContext, contextAccessor)
        {
            _context = dbContext;
        }
    }
}
