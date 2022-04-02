using EntityFramework.Web.DBContext;
using EntityFramework.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace WebAdmin.Services.Interfaces
{
    public interface IProductServices
    {
        Task<bool> AddAsync(Product categories);
        Task<bool> UpdateAsync(Product categories);
        Task<bool> DeleteByIdAsync(long Id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<IEnumerable<Categories>> CateGetAllAsync();
        Task<Product> GetByIdAsync(long Id);
        Task<IPagedList<Product>> GetListAsync(
            Expression<Func<Product, bool>> expression, Func<Product, object> sort, bool desc = false,
            int pageIndex = 1, int pageSize = Constants.PageSize);
    }
}
