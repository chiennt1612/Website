using EntityFramework.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebNuoc.Services.Interfaces
{
    public interface IMenuSubFooterServices
    {
        Task<IEnumerable<MenuSubFooter>> GetAllAsync();
        Task<MenuSubFooter> GetByIdAsync(long Id);
        Task<BaseEntityList<MenuSubFooter>> GetListAsync(
            Expression<Func<MenuSubFooter, bool>> expression,
            Func<MenuSubFooter, object> sort, bool desc,
            int page, int pageSize);
    }
}
