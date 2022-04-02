using EntityFramework.Web.DBContext;
using EntityFramework.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace WebAdmin.Services.Interfaces
{
    public interface IMenuSubFooterServices
    {
        Task<bool> AddAsync(MenuSubFooter menuSubFooter);
        Task<bool> UpdateAsync(MenuSubFooter menuSubFooter);
        Task<bool> DeleteByIdAsync(long Id);
        Task<IEnumerable<MenuSubFooter>> GetAllAsync();
        Task<MenuSubFooter> GetByIdAsync(long Id);
        Task<IPagedList<MenuSubFooter>> GetListAsync(
            Expression<Func<MenuSubFooter, bool>> expression, Func<MenuSubFooter, object> sort, bool desc = false,
            int pageIndex = 1, int pageSize = Constants.PageSize);
    }
}
