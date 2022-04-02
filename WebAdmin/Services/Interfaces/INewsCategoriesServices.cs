using EntityFramework.Web.DBContext;
using EntityFramework.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace WebAdmin.Services.Interfaces
{
    public interface INewsCategoriesServices
    {
        Task<bool> AddAsync(NewsCategories categories);
        Task<bool> UpdateAsync(NewsCategories categories);
        Task<bool> DeleteByIdAsync(long Id);
        Task<IEnumerable<NewsCategories>> GetAllAsync();
        Task<NewsCategories> GetByIdAsync(long Id);
        Task<IPagedList<NewsCategories>> GetListAsync(
            Expression<Func<NewsCategories, bool>> expression, Func<NewsCategories, object> sort, bool desc = false,
            int pageIndex = 1, int pageSize = Constants.PageSize);
    }
}
