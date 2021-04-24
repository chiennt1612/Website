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
    public class ArticleRepository: GenericRepository<Article, long>, IArticleRepository
    {
        private readonly AppDbContext _context;
        public ArticleRepository(AppDbContext dbContext, IHttpContextAccessor contextAccessor)
            : base(dbContext, contextAccessor)
        {
            _context = dbContext;
        }
        public async Task<IEnumerable<NewsCategories>> CateGetAllAsync()
        {
            return await _context.NewsCategoriess.Include(a => a.Parent).Where(a => a.IsDeleted == false).ToListAsync();
        }
        public override async Task<Article> GetByIdAsync(long id)
        {
            return await _context.Articles.Include(a => a.NewsCategories).FirstOrDefaultAsync(m => m.Id == id);
        }

        public override async Task<IPagedList<Article>> GetListByPage(
            Expression<Func<Article, bool>> expression, Func<Article, string> sort, bool desc = false,
            int pageIndex = 1, int pageSize = Constants.PageSize)
        {
            IQueryable<Article> r = _context.Articles.Include(a => a.NewsCategories)
                .Where(a => a.IsDeleted == false)
                .Where(expression);
            IOrderedEnumerable<Article> r1;
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
