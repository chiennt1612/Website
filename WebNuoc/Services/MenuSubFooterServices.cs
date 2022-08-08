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
    public class MenuSubFooterServices : IMenuSubFooterServices
    {
        private IUnitOfWork unitOfWork;
        private ILogger<MenuSubFooterServices> ilogger;
        public MenuSubFooterServices(IUnitOfWork unitOfWork, ILogger<MenuSubFooterServices> ilogger)
        {
            this.unitOfWork = unitOfWork;
            this.ilogger = ilogger;
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
                ilogger.LogInformation($"Get by id {Id.ToString()} Is {a.UrlText}");
                return a;
            }
            catch (Exception ex)
            {
                ilogger.LogError($"Get by id {Id.ToString()} Is Fail {ex.Message}");
                return default;
            }
        }

        public async Task<BaseEntityList<MenuSubFooter>> GetListAsync(
            Expression<Func<MenuSubFooter, bool>> expression,
            Func<MenuSubFooter, object> sort, bool desc,
            int page, int pageSize)
        {
            try
            {
                var a = await unitOfWork.menuSubFooterRepository.GetListAsync(expression, sort, desc, page, pageSize);
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
