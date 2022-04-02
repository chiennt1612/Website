using EntityFramework.Web.DBContext;
using EntityFramework.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace WebAdmin.Services.Interfaces
{
    public interface IAdvServices
    {
        Task<bool> AddAsync(Adv article);
        Task<bool> UpdateAsync(Adv article);
        Task<bool> DeleteByIdAsync(long Id);
        Task<IEnumerable<AdvPosition>> PositionGetAllAsync();
        Task<IEnumerable<Adv>> GetAllAsync();
        Task<Adv> GetByIdAsync(long Id);
        Task<IPagedList<Adv>> GetListAsync(
            Expression<Func<Adv, bool>> expression, Func<Adv, object> sort, bool desc = false,
            int pageIndex = 1, int pageSize = Constants.PageSize);
    }
}
