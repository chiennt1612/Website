using EntityFramework.Web.DBContext;
using EntityFramework.Web.Entities;
using EntityFramework.Web.Entities.Ordering;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebClient.Repository.Interfaces;
//using X.PagedList;

namespace WebClient.Repository
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
            return await _context.Orders.Include(a => a.OrderStatus).FirstOrDefaultAsync(m => m.Id == id);
        }
        //async Task<BaseEntityList<T>> GetListAsync(Expression<Func<T, bool>> expression, Func<T, object> sort, bool desc, int page, int pageSize)
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
    }
    public class OrderItemRepository : GenericRepository<OrderItem, long>, IOrderItemRepository
    {
        private readonly AppDbContext _context;
        private readonly HttpContext _httpContext;
        private readonly DbSet<OrderItem> _dbSet;
        public OrderItemRepository(AppDbContext dbContext, IHttpContextAccessor contextAccessor)
            : base(dbContext, contextAccessor)
        {
            _context = dbContext;
            _httpContext = contextAccessor.HttpContext;
            _dbSet = _context.Set<OrderItem>();
        }
        public override async Task<OrderItem> GetByIdAsync(long id)
        {
            return await _context.OrderItems.Include(a => a.Order).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<OrderItem>> GetByOrderIdAsync(long id)
        {
            return await _context.OrderItems.Include(a => a.Order).Include(a => a.Product).Where(m => m.OrderId == id).ToListAsync();
        }
        //async Task<BaseEntityList<T>> GetListAsync(Expression<Func<T, bool>> expression, Func<T, object> sort, bool desc, int page, int pageSize)
        public override async Task<BaseEntityList<OrderItem>> GetListAsync(
            Expression<Func<OrderItem, bool>> expression,
            Func<OrderItem, object> sort, bool desc,
            int page, int pageSize)
        {
            var a = new BaseEntityList<OrderItem>();
            a.totalRecords = await CountAsync(expression);
            int skipRows = (page - 1) * pageSize;
            if (desc)
                a.list = (await _dbSet.AsNoTracking().Where(expression).ToListAsync()).OrderByDescending(sort).Skip(skipRows).Take(pageSize).ToList();
            else
                a.list = (await _dbSet.AsNoTracking().Where(expression).ToListAsync()).OrderBy(sort).Skip(skipRows).Take(pageSize).ToList();
            return a;
        }
    }
}
