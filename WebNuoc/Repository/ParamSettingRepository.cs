using EntityFramework.Web.DBContext;
using EntityFramework.Web.Entities;
using Microsoft.AspNetCore.Http;
using WebNuoc.Repository.Interfaces;

namespace WebNuoc.Repository
{
    public class ParamSettingRepository : GenericRepository<ParamSetting, long>, IParamSettingRepository
    {
        public ParamSettingRepository(AppDbContext dbContext, IHttpContextAccessor contextAccessor)
              : base(dbContext, contextAccessor)
        {
        }
    }
}
