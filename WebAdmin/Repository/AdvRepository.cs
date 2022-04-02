using EntityFramework.Web.DBContext;
using EntityFramework.Web.Entities;
using Microsoft.AspNetCore.Http;
using WebAdmin.Repository.Interfaces;

namespace WebAdmin.Repository
{
    public class AdvRepository : GenericRepository<Adv, long>, IAdvRepository
    {
        private readonly AppDbContext _context;
        public AdvRepository(AppDbContext dbContext, IHttpContextAccessor contextAccessor)
            : base(dbContext, contextAccessor)
        {
            _context = dbContext;
        }
    }

    public class AdvPositionRepository : GenericRepository<AdvPosition, long>, IAdvPositionRepository
    {
        private readonly AppDbContext _context;
        public AdvPositionRepository(AppDbContext dbContext, IHttpContextAccessor contextAccessor)
            : base(dbContext, contextAccessor)
        {
            _context = dbContext;
        }
    }
}
