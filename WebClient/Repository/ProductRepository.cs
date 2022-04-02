using EntityFramework.Web.DBContext;
using EntityFramework.Web.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebClient.Repository.Interfaces;

namespace WebClient.Repository
{
    public class ProductRepository : GenericRepository<Product, long>, IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Product> _dbSet;
        public ProductRepository(AppDbContext dbContext, IHttpContextAccessor contextAccessor)
              : base(dbContext, contextAccessor)
        {
            _context = dbContext;
            _dbSet = dbContext.Set<Product>();
        }
        public async Task<IEnumerable<Categories>> CateGetAllAsync()
        {
            return await _context.Categoriess.Include(a => a.Parent).Where(a => a.IsDeleted == false).ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetTopAsync(Expression<Func<Product, bool>> expression, int top)
        {
            return (await _context.Products
                            .Include(a => a.MainCategories)
                            .Include(a => a.ReferCategories)
                            .Where(a => a.IsDeleted == false && a.CategoryMain.HasValue)
                            .Where(a => a.Status)
                            .Where(a => a.Quota > 0)
                            .Where(expression).ToListAsync()
                          ).OrderByDescending(u => u.Id).Take(top);
        }
        public override async Task<Product> GetByIdAsync(long id)
        {
            return await _context.Products
                .Include(a => a.MainCategories)
                .Include(a => a.ReferCategories)
                .Where(a => a.Status)
                .Where(a => a.Quota > 0)
                .FirstOrDefaultAsync(m => m.Id == id && m.IsDeleted == false && m.CategoryMain.HasValue);
        }

        public async Task<Product> GetByCodeAsync(string Code)
        {
            return await _context.Products
                .Include(a => a.MainCategories)
                .Include(a => a.ReferCategories)
                .Where(a => a.Status)
                .Where(a => a.Quota > 0)
                .FirstOrDefaultAsync(m => m.Code == Code && m.IsDeleted == false && m.CategoryMain.HasValue);
        }

        public override async Task<BaseEntityList<Product>> GetListAsync(
            Expression<Func<Product, bool>> expression,
            Func<Product, object> sort, bool desc,
            int page, int pageSize)
        {
            var a = new BaseEntityList<Product>();
            a.totalRecords = await CountAsync1(expression);
            int skipRows = (page - 1) * pageSize;
            if (desc)
                a.list = (await _context.Products
                            .Include(a => a.MainCategories)
                            .Include(a => a.ReferCategories)
                            .Where(a => a.IsDeleted == false && a.CategoryMain.HasValue)
                            .Where(a => a.Status)
                            .Where(a => a.Quota > 0)
                            .Where(expression).ToListAsync()
                          ).OrderByDescending(sort).Skip(skipRows).Take(pageSize);
            else
                a.list = (await _context.Products
                            .Include(a => a.MainCategories)
                            .Include(a => a.ReferCategories)
                            .Where(a => a.IsDeleted == false && a.CategoryMain.HasValue)
                            .Where(a => a.Status)
                            .Where(a => a.Quota > 0)
                            .Where(expression).ToListAsync()
                          ).OrderBy(sort).Skip(skipRows).Take(pageSize);
            return a;
        }

        public async Task<int> CountAsync1(Expression<Func<Product, bool>> expression)
        {
            return await _dbSet.AsNoTracking()
                            .Where(a => a.IsDeleted == false && a.CategoryMain.HasValue)
                            .Where(a => a.Status)
                            .Where(a => a.Quota > 0)
                            .CountAsync(expression);
        }
    }
}
