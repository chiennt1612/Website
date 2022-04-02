using EntityFramework.Web.DBContext;
using EntityFramework.Web.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebAdmin.Repository.Interfaces;
using X.PagedList;

namespace WebAdmin.Repository
{
    public class ContactRepository : GenericRepository<Contact, long>, IContactRepository
    {
        private readonly HttpContext _httpContext;
        private readonly AppDbContext _dbContext;
        private readonly DbSet<Contact> _dbSet;
        public ContactRepository(AppDbContext dbContext, IHttpContextAccessor contextAccessor)
            : base(dbContext, contextAccessor)
        {
            _dbContext = dbContext;
            _httpContext = contextAccessor?.HttpContext;
            _dbSet = _dbContext.Set<Contact>();
        }

        public override async Task<Contact> GetByIdAsync(long id)
        {
            return await _dbContext.Contacts.Include(u => u.OrderStatus).Include(u => u.Services).FirstOrDefaultAsync(m => m.Id == id);
        }

        public override async Task<IPagedList<Contact>> GetListByPage(Expression<Func<Contact, bool>> expression, Func<Contact, object> sort, bool desc = false, int pageIndex = 1, int pageSize = 10)
        {
            IQueryable<Contact> r = _dbContext.Contacts.Include(u => u.OrderStatus).Include(u => u.Services).Where(expression);
            IOrderedEnumerable<Contact> r1;
            if (!desc)
            {
                r1 = r.OrderBy(sort);
            }
            else
            {
                r1 = r.OrderByDescending(sort);
            }
            return await r1.ToPagedListAsync(pageIndex, pageSize);
        }
    }
}
