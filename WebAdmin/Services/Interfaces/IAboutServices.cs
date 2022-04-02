using EntityFramework.Web.DBContext;
using EntityFramework.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace WebAdmin.Services.Interfaces
{
    public interface IAboutServices
    {
        Task<bool> AddAsync(About article);
        Task<bool> UpdateAsync(About article);
        Task<bool> DeleteByIdAsync(long Id);
        Task<IEnumerable<About>> GetAllAsync();
        Task<About> GetByIdAsync(long Id);
        Task<IPagedList<About>> GetListAsync(
            Expression<Func<About, bool>> expression, Func<About, object> sort, bool desc = false,
            int pageIndex = 1, int pageSize = Constants.PageSize);
    }
}
