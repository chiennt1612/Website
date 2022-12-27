using EntityFramework.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebNuoc.Services.Interfaces
{
    public interface IAboutServices
    {
        Task<IEnumerable<About>> GetAllAsync();
        Task<IEnumerable<About>> GetListAsync(IEnumerable<long> Ids);
        Task<About> GetByIdAsync(long Id);
        Task<BaseEntityList<About>> GetListAsync(
            Expression<Func<About, bool>> expression,
            Func<About, object> sort, bool desc,
            int page, int pageSize);
    }
}
