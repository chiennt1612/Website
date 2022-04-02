using EntityFramework.Web.DBContext;
using EntityFramework.Web.Entities;
using EntityFramework.Web.Entities.Ordering;
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
    public class OrderRepository : GenericRepository<Order, long>, IOrderRepository
    {
        private readonly AppDbContext _context;
        private readonly HttpContext _httpContext;
        private readonly DbSet<Order> _dbSet;
        public OrderRepository(AppDbContext dbContext, IHttpContextAccessor contextAccessor)
            : base(dbContext, contextAccessor)
        {
            _context = dbContext;
            _httpContext = contextAccessor.HttpContext;
            _dbSet = _context.Set<Order>();
        }
        public override async Task<Order> GetByIdAsync(long id)
        {
            var a = await _context.Orders.Include(a => a.OrderStatus).FirstOrDefaultAsync(m => m.Id == id);
            a.OrderItems = await _context.OrderItems.Include(u => u.Product).Where(u => u.OrderId == id).ToListAsync();
            return a;
        }

        public override async Task<IPagedList<Order>> GetListByPage(
            Expression<Func<Order, bool>> expression, Func<Order, object> sort, bool desc = false,
            int pageIndex = 1, int pageSize = Constants.PageSize)
        {
            IQueryable<Order> r = _context.Orders.Include(a => a.OrderStatus)
                .Where(a => a.StatusId > 0 && a.StatusId < 6)
                .Where(expression);
            IOrderedEnumerable<Order> r1;
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

        public override async Task<BaseEntityList<Order>> GetListAsync(
            Expression<Func<Order, bool>> expression,
            Func<Order, object> sort, bool desc,
            int page, int pageSize)
        {
            var a = new BaseEntityList<Order>();
            a.totalRecords = await CountAsync(expression);
            int skipRows = (page - 1) * pageSize;
            if (desc)
                a.list = (await _dbSet.AsNoTracking().Where(expression).ToListAsync()).OrderByDescending(sort).Skip(skipRows).Take(pageSize).ToList();
            else
                a.list = (await _dbSet.AsNoTracking().Where(expression).ToListAsync()).OrderBy(sort).Skip(skipRows).Take(pageSize).ToList();
            return a;
        }
        //public async Task<int> CountAsync(Expression<Func<Order, bool>> expression)
        //{
        //    return await _dbSet.AsNoTracking().CountAsync(expression);
        //}

    }
}
