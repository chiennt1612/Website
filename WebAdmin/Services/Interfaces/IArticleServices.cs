using EntityFramework.Web.DBContext;
using EntityFramework.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace WebAdmin.Services.Interfaces
{
    public interface IArticleServices
    {
        Task<bool> AddAsync(Article article);
        Task<bool> UpdateAsync(Article article);
        Task<bool> DeleteByIdAsync(long Id);
        Task<IEnumerable<NewsCategories>> CateGetAllAsync();
        Task<IEnumerable<Article>> GetAllAsync();
        Task<Article> GetByIdAsync(long Id);
        Task<IPagedList<Article>> GetListAsync(
            Expression<Func<Article, bool>> expression, Func<Article, object> sort, bool desc = false,
            int pageIndex = 1, int pageSize = Constants.PageSize);
    }
}
