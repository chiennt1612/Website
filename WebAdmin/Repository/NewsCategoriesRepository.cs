using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EntityFramework.Web.DBContext;
using EntityFramework.Web.Entities;
using WebAdmin.Helpers;
using WebAdmin.Repository.Interfaces;
using X.PagedList;

namespace WebAdmin.Repository
{
    public class NewsCategoriesRepository : GenericRepository<NewsCategories, long>, INewsCategoriesRepository
    {
        private readonly AppDbContext _context;
        public NewsCategoriesRepository(AppDbContext dbContext, IHttpContextAccessor contextAccessor)
              : base(dbContext, contextAccessor)
        {
            _context = dbContext;
        }

        public override async Task<IEnumerable<NewsCategories>> GetAllAsync()
        {
            return await _context.NewsCategoriess.Include(a => a.Parent).Where(a => a.IsDeleted == false).ToListAsync();
        }
        public override async Task<NewsCategories> GetByIdAsync(long id)
        {
            return await _context.NewsCategoriess.Include(a => a.Parent).FirstOrDefaultAsync(m => m.Id == id);
        }

        public override async Task<IPagedList<NewsCategories>> GetListByPage(
            Expression<Func<NewsCategories, bool>> expression, Func<NewsCategories, string> sort, bool desc = false,
            int pageIndex = 1, int pageSize = Constants.PageSize)
        {
            IQueryable<NewsCategories> r = _context.NewsCategoriess.Include(a => a.Parent)
                .Where(a => a.IsDeleted == false)
                .Where(expression);
            IOrderedEnumerable<NewsCategories> r1;
            if (desc)
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
