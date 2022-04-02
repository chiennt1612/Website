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
    public class CategoriesRepository : GenericRepository<Categories, long>, ICategoriesRepository
    {
        private readonly AppDbContext _context;
        public CategoriesRepository(AppDbContext dbContext, IHttpContextAccessor contextAccessor)
              : base(dbContext, contextAccessor)
        {
            _context = dbContext;
        }

        public override async Task<IEnumerable<Categories>> GetAllAsync()
        {
            return await _context.Categoriess.Include(a => a.Parent).Where(a => a.IsDeleted == false).ToListAsync();
        }
        public override async Task<Categories> GetByIdAsync(long id)
        {
            return await _context.Categoriess.Include(a => a.Parent).FirstOrDefaultAsync(m => m.Id == id);
        }

        public override async Task<IPagedList<Categories>> GetListByPage(
            Expression<Func<Categories, bool>> expression, Func<Categories, object> sort, bool desc = false,
            int pageIndex = 1, int pageSize = Constants.PageSize)
        {
            IQueryable<Categories> r = _context.Categoriess.Include(a => a.Parent)
                .Where(a => a.IsDeleted == false)
                .Where(expression);
            IOrderedEnumerable<Categories> r1;
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
