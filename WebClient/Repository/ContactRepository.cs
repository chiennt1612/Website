using EntityFramework.Web.DBContext;
using EntityFramework.Web.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebClient.Repository.Interfaces;

namespace WebClient.Repository
{
    public class ContactRepository : GenericRepository<Contact, long>, IContactRepository
    {
        private readonly AppDbContext _context;
        public ContactRepository(AppDbContext dbContext, IHttpContextAccessor contextAccessor)
            : base(dbContext, contextAccessor)
        {
            _context = dbContext;
        }

        public override async Task<Contact> GetByIdAsync(long id)
        {
            return await _context.Contacts
                .Include(a => a.OrderStatus)
                .Include(a => a.Services)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
