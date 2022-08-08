using EntityFramework.Web.DBContext;
using EntityFramework.Web.Entities;
using Microsoft.AspNetCore.Http;
using WebNuoc.Repository.Interfaces;

namespace WebNuoc.Repository
{
    public class InvoiceSaveRepository : GenericRepository<InvoiceSave, long>, IInvoiceSaveRepository
    {
        private readonly AppDbContext _context;
        public InvoiceSaveRepository(AppDbContext dbContext, IHttpContextAccessor contextAccessor)
            : base(dbContext, contextAccessor)
        {
            _context = dbContext;
        }
    }
}
