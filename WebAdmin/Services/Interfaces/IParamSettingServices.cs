using EntityFramework.Web.DBContext;
using EntityFramework.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace WebAdmin.Services.Interfaces
{
    public interface IParamSettingServices
    {
        Task<bool> AddAsync(ParamSetting paramSetting);
        Task<bool> UpdateAsync(ParamSetting paramSetting);
        Task<bool> DeleteByIdAsync(long Id);
        Task<IEnumerable<ParamSetting>> GetAllAsync();
        Task<ParamSetting> GetByIdAsync(long Id);
        Task<IPagedList<ParamSetting>> GetListAsync(
            Expression<Func<ParamSetting, bool>> expression, Func<ParamSetting, object> sort, bool desc = false,
            int pageIndex = 1, int pageSize = Constants.PageSize);
    }
}
