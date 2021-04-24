using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EntityFramework.Web.Entities;
using WebAdmin.Helpers;
using X.PagedList;
using EntityFramework.Web.DBContext;

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
            Expression<Func<Article, bool>> expression, Func<Article, string> sort, bool desc = false,
            int pageIndex = 1, int pageSize = Constants.PageSize);
    }
}
