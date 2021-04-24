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
    public interface IMenuMainFooterServices
    {
        Task<bool> AddAsync(MenuMainFooter menuMainFooter);
        Task<bool> UpdateAsync(MenuMainFooter menuMainFooter);
        Task<bool> DeleteByIdAsync(long Id);
        Task<IEnumerable<MenuMainFooter>> GetAllAsync();
        Task<MenuMainFooter> GetByIdAsync(long Id);
        Task<IPagedList<MenuMainFooter>> GetListAsync(
            Expression<Func<MenuMainFooter, bool>> expression, Func<MenuMainFooter, string> sort, bool desc = false,
            int pageIndex = 1, int pageSize = Constants.PageSize);
    }
}
