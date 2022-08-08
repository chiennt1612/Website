using EntityFramework.Web.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebNuoc.Repository.Interfaces;
using WebNuoc.Services.Interfaces;

namespace WebNuoc.Services
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

        public async Task<BaseEntityList<MenuMainFooter>> GetListAsync(
            Expression<Func<MenuMainFooter, bool>> expression,
            Func<MenuMainFooter, object> sort, bool desc,
            int page, int pageSize)
        {
            try
            {
                var a = await unitOfWork.menuMainFooterRepository.GetListAsync(expression, sort, desc, page, pageSize);
                ilogger.LogInformation($"GetListAsync expression, sort {desc} {page} {pageSize}");
                return a;
            }
            catch (Exception ex)
            {
                ilogger.LogError($"GetListAsync expression, sort {desc} {page} {pageSize} Is Fail {ex.Message}");
                return default;
            }
        }

    }
}
