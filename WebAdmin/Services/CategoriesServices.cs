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
    public class CategoriesServices : ICategoriesServices
    {
        private IUnitOfWork unitOfWork;
        private ILogger<CategoriesServices> ilogger;
        public CategoriesServices(IUnitOfWork unitOfWork, ILogger<CategoriesServices> ilogger)
        {
            this.unitOfWork = unitOfWork;
            this.ilogger = ilogger;
        }

        public async Task<bool> AddAsync(Categories categories)
        {
            try
            {
                await unitOfWork.categoriesRepository.AddAsync(categories);
                await unitOfWork.SaveAsync();
                ilogger.LogInformation($"Save object {JsonConvert.SerializeObject(categories)} Is OK");
                return true;
            }
            catch (Exception ex)
            {
                ilogger.LogError($"Save object {JsonConvert.SerializeObject(categories)} Is Fail {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteByIdAsync(long Id)
        {
            try
            {
                await unitOfWork.categoriesRepository.DeleteAsync(Id);
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

        public async Task<IEnumerable<Categories>> GetAllAsync()
        {
            ilogger.LogInformation($"GetAllAsync");
            return await unitOfWork.categoriesRepository.GetAllAsync();
        }

        public async Task<Categories> GetByIdAsync(long Id)
        {
            try
            {
                var a = await unitOfWork.categoriesRepository.GetByIdAsync(Id);
                ilogger.LogInformation($"Get by id {Id.ToString()} Is {JsonConvert.SerializeObject(a)}");
                return a;
            }
            catch (Exception ex)
            {
                ilogger.LogError($"Get by id {Id.ToString()} Is Fail {ex.Message}");
                return default;
            }
        }

        public async Task<IPagedList<Categories>> GetListAsync(
            Expression<Func<Categories, bool>> expression, Func<Categories, object> sort, bool desc = false,
            int pageIndex = 1, int pageSize = Constants.PageSize)
        {
            try
            {
                var a = await unitOfWork.categoriesRepository.GetListByPage(expression, sort, desc, pageIndex, pageSize);
                ilogger.LogInformation($"GetListAsync expression, sort {desc} {pageIndex} {pageSize}");
                return a;
            }
            catch (Exception ex)
            {
                ilogger.LogError($"GetListAsync expression, sort {desc} {pageIndex} {pageSize} Is Fail {ex.Message}");
                return default;
            }
        }

        public async Task<bool> UpdateAsync(Categories categories)
        {
            try
            {
                unitOfWork.categoriesRepository.Update(categories);
                await unitOfWork.SaveAsync();
                ilogger.LogInformation($"Update object {JsonConvert.SerializeObject(categories)} Is OK");
                return true;
            }
            catch (Exception ex)
            {
                ilogger.LogError($"Update object {JsonConvert.SerializeObject(categories)} Is Fail {ex.Message}");
                return false;
            }
        }
    }
}
