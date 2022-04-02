using EntityFramework.Web.DBContext;
using EntityFramework.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace WebAdmin.Services.Interfaces
{
    public interface ICategoriesServices
    {
        Task<bool> AddAsync(Categories categories);
        Task<bool> UpdateAsync(Categories categories);
        Task<bool> DeleteByIdAsync(long Id);
        Task<IEnumerable<Categories>> GetAllAsync();
        Task<Categories> GetByIdAsync(long Id);
        Task<IPagedList<Categories>> GetListAsync(
            Expression<Func<Categories, bool>> expression, Func<Categories, object> sort, bool desc = false,
            int pageIndex = 1, int pageSize = Constants.PageSize);
    }
}
