using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityFramework.Web.DBContext;
using EntityFramework.Web.Entities;
using WebAdmin.Repository.Interfaces;

namespace WebAdmin.Repository
{
    public class ParamSettingRepository : GenericRepository<ParamSetting, long>, IParamSettingRepository
    {
        public ParamSettingRepository(AppDbContext dbContext, IHttpContextAccessor contextAccessor)
              : base(dbContext, contextAccessor)
        {
        }
    }
}
