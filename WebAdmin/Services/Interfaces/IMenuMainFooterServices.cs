using EntityFramework.Web.DBContext;
using EntityFramework.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace WebAdmin.Services.Interfaces
{
    public interface IMenuMainFooterServices
    {
        Task<bool> AddAsync(MenuMainFooter menuMainFooter);
        Task<bool> UpdateAsync(MenuMainFooter menuMainFooter);
        Task<bool> DeleteByIdAsync(long Id);
        Task<IEnumerable<MenuMainFooter>> GetAllAsync();
        Task<MenuMainFooter> GetByIdAsync(long Id);
        Task<IPagedList<MenuMainFooter>> GetListAsync(
            Expression<Func<MenuMainFooter, bool>> expression, Func<MenuMainFooter, object> sort, bool desc = false,
            int pageIndex = 1, int pageSize = Constants.PageSize);
    }
}
