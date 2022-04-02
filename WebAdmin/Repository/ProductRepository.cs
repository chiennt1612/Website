using EntityFramework.Web.DBContext;
using EntityFramework.Web.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebAdmin.Repository.Interfaces;
using X.PagedList;

namespace WebAdmin.Repository
{
    public class ProductRepository : GenericRepository<Product, long>, IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext dbContext, IHttpContextAccessor contextAccessor)
              : base(dbContext, contextAccessor)
        {
            _context = dbContext;
        }
        public async Task<IEnumerable<Categories>> CateGetAllAsync()
        {
            return await _context.Categoriess.Include(a => a.Parent).Where(a => a.IsDeleted == false).ToListAsync();
        }
        public override async Task<Product> GetByIdAsync(long id)
        {
            return await _context.Products
                .Include(a => a.MainCategories)
                .Include(a => a.ReferCategories)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public override async Task<IPagedList<Product>> GetListByPage(
            Expression<Func<Product, bool>> expression, Func<Product, object> sort, bool desc = false,
            int pageIndex = 1, int pageSize = Constants.PageSize)
        {
            //var a = _context.Products.Where (u => u.Id == 4).FirstOrDefault();
            //var b = _context.Products.Where(u => !u.CategoryMain.HasValue).Count();
            IQueryable<Product> r = _context.Products
                .Include(a => a.MainCategories)
                .Include(a => a.ReferCategories)
                .Where(a => a.IsDeleted == false)
                .Where(expression);
            IOrderedEnumerable<Product> r1;
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
