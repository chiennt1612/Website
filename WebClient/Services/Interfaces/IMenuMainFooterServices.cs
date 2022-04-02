using EntityFramework.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebClient.Services.Interfaces
{
    public interface IMenuMainFooterServices
    {
        Task<IEnumerable<MenuMainFooter>> GetAllAsync();
        Task<MenuMainFooter> GetByIdAsync(long Id);
        Task<BaseEntityList<MenuMainFooter>> GetListAsync(
            Expression<Func<MenuMainFooter, bool>> expression,
            Func<MenuMainFooter, object> sort, bool desc,
            int page, int pageSize);
    }
}
