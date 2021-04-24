using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EntityFramework.Web.Entities;
using WebAdmin.Helpers;
using WebAdmin.Repository.Interfaces;
using WebAdmin.Services.Interfaces;
using X.PagedList;
using EntityFramework.Web.DBContext;

namespace WebAdmin.Services
{
    public class NewsCategoriesServices : INewsCategoriesServices
    {
        private IUnitOfWork unitOfWork;
        private ILogger<NewsCategoriesServices> ilogger;
        public NewsCategoriesServices(IUnitOfWork unitOfWork, ILogger<NewsCategoriesServices> ilogger)
        {
            this.unitOfWork = unitOfWork;
            this.ilogger = ilogger;
        }

        public async Task<bool> AddAsync(NewsCategories newsCategories)
        {
            try
            {
                await unitOfWork.newsCategoriesRepository.AddAsync(newsCategories);
                await unitOfWork.SaveAsync();
                ilogger.LogInformation($"Save object {JsonConvert.SerializeObject(newsCategories)} Is OK");
                return true;
            }
            catch (Exception ex)
            {
                ilogger.LogError($"Save object {JsonConvert.SerializeObject(newsCategories)} Is Fail {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteByIdAsync(long Id)
        {
            try
            {
                await unitOfWork.newsCategoriesRepository.DeleteAsync(Id);
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
        public async Task<IEnumerable<NewsCategories>> GetAllAsync()
        {
            ilogger.LogInformation($"GetAllAsync");
            return await unitOfWork.newsCategoriesRepository.GetAllAsync();
        }
        public async Task<NewsCategories> GetByIdAsync(long Id)
        {
            try
            {
                var a = await unitOfWork.newsCategoriesRepository.GetByIdAsync(Id);
                ilogger.LogInformation($"Get by id {Id.ToString()} Is {JsonConvert.SerializeObject(a)}");
                return a;
            }
            catch (Exception ex)
            {
                ilogger.LogError($"Get by id {Id.ToString()} Is Fail {ex.Message}");
                return default;
            }
        }

        //public async Task<BaseEntityList<NewsCategories>> GetListAsync(Expression<Func<NewsCategories, bool>> expression, Func<NewsCategories, string> sort, bool desc = false, int page = 1, int pageSize = 10)
        //{
        //    try
        //    {
        //        var a = await unitOfWork.newsCategoriesRepository.GetListAsync(expression, sort, desc, page, pageSize);
        //        ilogger.LogInformation($"GetListAsync {JsonConvert.SerializeObject(expression)}, {JsonConvert.SerializeObject(sort)} {desc} {page} {pageSize}");
        //        return a;
        //    }
        //    catch (Exception ex)
        //    {
        //        ilogger.LogError($"GetListAsync {JsonConvert.SerializeObject(expression)}, {JsonConvert.SerializeObject(sort)} {desc} {page} {pageSize} Is Fail {ex.Message}");
        //        return default;
        //    }
        //}

        public async Task<bool> UpdateAsync(NewsCategories newsCategories)
        {
            try
            {
                unitOfWork.newsCategoriesRepository.Update(newsCategories);
                await unitOfWork.SaveAsync();
                ilogger.LogInformation($"Update object {JsonConvert.SerializeObject(newsCategories)} Is OK");
                return true;
            }
            catch (Exception ex)
            {
                ilogger.LogError($"Update object {JsonConvert.SerializeObject(newsCategories)} Is Fail {ex.Message}");
                return false;
            }
        }

        public async Task<IPagedList<NewsCategories>> GetListAsync(
            Expression<Func<NewsCategories, bool>> expression, Func<NewsCategories, string> sort, bool desc = false,
            int pageIndex = 1, int pageSize = Constants.PageSize)
        {
            try
            {
                var a = await unitOfWork.newsCategoriesRepository.GetListByPage(expression, sort, desc, pageIndex, pageSize);
                ilogger.LogInformation($"GetListAsync expression, sort {desc} {pageIndex} {pageSize}");
                return a;
            }
            catch (Exception ex)
            {
                ilogger.LogError($"GetListAsync expression, sort {desc} {pageIndex} {pageSize} Is Fail {ex.Message}");
                return default;
            }
        }
    }
}
