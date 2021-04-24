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
    public interface ICategoriesServices
    {
        Task<bool> AddAsync(Categories categories);
        Task<bool> UpdateAsync(Categories categories);
        Task<bool> DeleteByIdAsync(long Id);
        Task<IEnumerable<Categories>> GetAllAsync();
        Task<Categories> GetByIdAsync(long Id);
        Task<IPagedList<Categories>> GetListAsync(
            Expression<Func<Categories, bool>> expression, Func<Categories, string> sort, bool desc = false,
            int pageIndex = 1, int pageSize = Constants.PageSize);
    }
}
