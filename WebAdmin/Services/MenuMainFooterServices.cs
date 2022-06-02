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
    public class MenuMainFooterServices : IMenuMainFooterServices
    {
        private IUnitOfWork unitOfWork;
        private ILogger<MenuMainFooterServices> ilogger;
        public MenuMainFooterServices(IUnitOfWork unitOfWork, ILogger<MenuMainFooterServices> ilogger)
        {
            this.unitOfWork = unitOfWork;
            this.ilogger = ilogger;
        }

        public async Task<bool> AddAsync(MenuMainFooter menuMainFooter)
        {
            try
            {
                await unitOfWork.menuMainFooterRepository.AddAsync(menuMainFooter);
                await unitOfWork.SaveAsync();
                ilogger.LogInformation($"Save object {menuMainFooter.UrlText} Is OK");
                return true;
            }
            catch (Exception ex)
            {
                ilogger.LogError($"Save object {menuMainFooter.UrlText} Is Fail {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteByIdAsync(long Id)
        {
            try
            {
                await unitOfWork.menuMainFooterRepository.DeleteAsync(Id);
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
        public async Task<IEnumerable<MenuMainFooter>> GetAllAsync()
        {
            ilogger.LogInformation($"GetAllAsync");
            return await unitOfWork.menuMainFooterRepository.GetAllAsync();
        }
        public async Task<MenuMainFooter> GetByIdAsync(long Id)
        {
            try
            {
                var a = await unitOfWork.menuMainFooterRepository.GetByIdAsync(Id);
                ilogger.LogInformation($"Get by id {Id.ToString()} Is {a.UrlText}");
                return a;
            }
            catch (Exception ex)
            {
                ilogger.LogError($"Get by id {Id.ToString()} Is Fail {ex.Message}");
                return default;
            }
        }

        public async Task<IPagedList<MenuMainFooter>> GetListAsync(
            Expression<Func<MenuMainFooter, bool>> expression, Func<MenuMainFooter, object> sort, bool desc = false,
            int pageIndex = 1, int pageSize = Constants.PageSize)
        {
            try
            {
                var a = await unitOfWork.menuMainFooterRepository.GetListByPage(expression, sort, desc, pageIndex, pageSize);
                ilogger.LogInformation($"GetListAsync expression, sort {desc} {pageIndex} {pageSize}");
                return a;
            }
            catch (Exception ex)
            {
                ilogger.LogError($"GetListAsync expression, sort {desc} {pageIndex} {pageSize} Is Fail {ex.Message}");
                return default;
            }
        }

        public async Task<bool> UpdateAsync(MenuMainFooter menuMainFooter)
        {
            try
            {
                unitOfWork.menuMainFooterRepository.Update(menuMainFooter);
                await unitOfWork.SaveAsync();
                ilogger.LogInformation($"Update object {menuMainFooter.UrlText} Is OK");
                return true;
            }
            catch (Exception ex)
            {
                ilogger.LogError($"Update object {menuMainFooter.UrlText} Is Fail {ex.Message}");
                return false;
            }
        }
    }
}
