using EntityFramework.Web.DBContext;
using EntityFramework.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace WebAdmin.Services.Interfaces
{
    public interface IFAQServices
    {
        Task<bool> AddAsync(FAQ article);
        Task<bool> UpdateAsync(FAQ article);
        Task<bool> DeleteByIdAsync(long Id);
        Task<IEnumerable<FAQ>> GetAllAsync();
        Task<FAQ> GetByIdAsync(long Id);
        Task<IPagedList<FAQ>> GetListAsync(
            Expression<Func<FAQ, bool>> expression, Func<FAQ, object> sort, bool desc = false,
            int pageIndex = 1, int pageSize = Constants.PageSize);
    }
}
