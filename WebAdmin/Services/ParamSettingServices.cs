using EntityFramework.Web.DBContext;
using EntityFramework.Web.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebAdmin.Repository.Interfaces;
using WebAdmin.Services.Interfaces;
using X.PagedList;

namespace WebAdmin.Services
{
    public class ParamSettingServices : IParamSettingServices
    {
        private IUnitOfWork unitOfWork;
        private ILogger<ParamSettingServices> ilogger;
        public ParamSettingServices(IUnitOfWork unitOfWork, ILogger<ParamSettingServices> ilogger)
        {
            this.unitOfWork = unitOfWork;
            this.ilogger = ilogger;
        }

        public async Task<bool> AddAsync(ParamSetting paramSetting)
        {
            try
            {
                await unitOfWork.paramSettingRepository.AddAsync(paramSetting);
                await unitOfWork.SaveAsync();
                ilogger.LogInformation($"Save object {paramSetting.ParamKey} Is OK");
                return true;
            }
            catch (Exception ex)
            {
                ilogger.LogError($"Save object {paramSetting.ParamKey} Is Fail {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteByIdAsync(long Id)
        {
            try
            {
                await unitOfWork.paramSettingRepository.DeleteAsync(Id);
                await unitOfWork.SaveAsync();
                ilogger.LogInformation($"Delete by id {Id.ToString()} Is OK");
                return true;
            }
            catch (Exception ex)
            {
                ilogger.LogError($"Delete by id {Id.ToString()} Is Fail {ex.Message}");
                return false;
            }
        }
        public async Task<IEnumerable<ParamSetting>> GetAllAsync()
        {
            ilogger.LogInformation($"GetAllAsync");
            return await unitOfWork.paramSettingRepository.GetAllAsync();
        }
        public async Task<ParamSetting> GetByIdAsync(long Id)
        {
            try
            {
                var a = await unitOfWork.paramSettingRepository.GetByIdAsync(Id);
                ilogger.LogInformation($"Get by id {Id.ToString()} Is {a.ParamKey}");
                return a;
            }
            catch (Exception ex)
            {
                ilogger.LogError($"Get by id {Id.ToString()} Is Fail {ex.Message}");
                return default;
            }
        }

        public async Task<IPagedList<ParamSetting>> GetListAsync(
            Expression<Func<ParamSetting, bool>> expression, Func<ParamSetting, object> sort, bool desc = false,
            int pageIndex = 1, int pageSize = Constants.PageSize)
        {
            try
            {
                var a = await unitOfWork.paramSettingRepository.GetListByPage(expression, sort, desc, pageIndex, pageSize);
                ilogger.LogInformation($"GetListAsync expression, sort {desc} {pageIndex} {pageSize}");
                return a;
            }
            catch (Exception ex)
            {
                ilogger.LogError($"GetListAsync expression, sort {desc} {pageIndex} {pageSize} Is Fail {ex.Message}");
                return default;
            }
        }

        public async Task<bool> UpdateAsync(ParamSetting paramSetting)
        {
            try
            {
                unitOfWork.paramSettingRepository.Update(paramSetting);
                await unitOfWork.SaveAsync();
                ilogger.LogInformation($"Update object {paramSetting.ParamKey} Is OK");
                return true;
            }
            catch (Exception ex)
            {
                ilogger.LogError($"Update object {paramSetting.ParamKey} Is Fail {ex.Message}");
                return false;
            }
        }
    }
}
