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
    public class MenuSubFooterServices : IMenuSubFooterServices
    {
        private IUnitOfWork unitOfWork;
        private ILogger<MenuSubFooterServices> ilogger;
        public MenuSubFooterServices(IUnitOfWork unitOfWork, ILogger<MenuSubFooterServices> ilogger)
        {
            this.unitOfWork = unitOfWork;
            this.ilogger = ilogger;
        }

        public async Task<bool> AddAsync(MenuSubFooter menuSubFooter)
        {
            try
            {
                await unitOfWork.menuSubFooterRepository.AddAsync(menuSubFooter);
                await unitOfWork.SaveAsync();
                ilogger.LogInformation($"Save object {JsonConvert.SerializeObject(menuSubFooter)} Is OK");
                return true;
            }
            catch (Exception ex)
            {
                ilogger.LogError($"Save object {JsonConvert.SerializeObject(menuSubFooter)} Is Fail {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteByIdAsync(long Id)
        {
            try
            {
                await unitOfWork.menuSubFooterRepository.DeleteAsync(Id);
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
        public async Task<IEnumerable<MenuSubFooter>> GetAllAsync()
        {
            ilogger.LogInformation($"GetAllAsync");
            return await unitOfWork.menuSubFooterRepository.GetAllAsync();
        }
        public async Task<MenuSubFooter> GetByIdAsync(long Id)
        {
            try
            {
                var a = await unitOfWork.menuSubFooterRepository.GetByIdAsync(Id);
                ilogger.LogInformation($"Get by id {Id.ToString()} Is {JsonConvert.SerializeObject(a)}");
                return a;
            }
            catch (Exception ex)
            {
                ilogger.LogError($"Get by id {Id.ToString()} Is Fail {ex.Message}");
                return default;
            }
        }

        public async Task<IPagedList<MenuSubFooter>> GetListAsync(
            Expression<Func<MenuSubFooter, bool>> expression, Func<MenuSubFooter, object> sort, bool desc = false,
            int pageIndex = 1, int pageSize = Constants.PageSize)
        {
            try
            {
                var a = await unitOfWork.menuSubFooterRepository.GetListByPage(expression, sort, desc, pageIndex, pageSize);
                ilogger.LogInformation($"GetListAsync expression, sort {desc} {pageIndex} {pageSize}");
                return a;
            }
            catch (Exception ex)
            {
                ilogger.LogError($"GetListAsync expression, sort {desc} {pageIndex} {pageSize} Is Fail {ex.Message}");
                return default;
            }
        }

        public async Task<bool> UpdateAsync(MenuSubFooter menuSubFooter)
        {
            try
            {
                unitOfWork.menuSubFooterRepository.Update(menuSubFooter);
                await unitOfWork.SaveAsync();
                ilogger.LogInformation($"Update object {JsonConvert.SerializeObject(menuSubFooter)} Is OK");
                return true;
            }
            catch (Exception ex)
            {
                ilogger.LogError($"Update object {JsonConvert.SerializeObject(menuSubFooter)} Is Fail {ex.Message}");
                return false;
            }
        }
    }
}
