using EntityFramework.Web.DBContext;
using EntityFramework.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace WebAdmin.Services.Interfaces
{
    public interface IServiceServices
    {
        Task<bool> AddAsync(Service article);
        Task<bool> UpdateAsync(Service article);
        Task<bool> DeleteByIdAsync(long Id);
        Task<IEnumerable<Service>> GetAllAsync();
        Task<Service> GetByIdAsync(long Id);
        Task<IPagedList<Service>> GetListAsync(
            Expression<Func<Service, bool>> expression, Func<Service, object> sort, bool desc = false,
            int pageIndex = 1, int pageSize = Constants.PageSize);
    }
}
