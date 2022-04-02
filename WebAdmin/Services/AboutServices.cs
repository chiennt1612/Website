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
    public class AboutServices : IAboutServices
    {
        private IUnitOfWork unitOfWork;
        private ILogger<AboutServices> ilogger;
        public AboutServices(IUnitOfWork unitOfWork, ILogger<AboutServices> ilogger)
        {
            this.unitOfWork = unitOfWork;
            this.ilogger = ilogger;
        }

        public async Task<bool> AddAsync(About about)
        {
            try
            {
                await unitOfWork.aboutRepository.AddAsync(about);
                await unitOfWork.SaveAsync();
                ilogger.LogInformation($"Save object  Is OK");//{JsonConvert.SerializeObject(article)}
                return true;
            }
            catch (Exception ex)
            {
                ilogger.LogError($"Save object  Is Fail {ex.Message}");//{JsonConvert.SerializeObject(article)}
                return false;
            }
        }

        public async Task<bool> DeleteByIdAsync(long Id)
        {
            try
            {
                await unitOfWork.aboutRepository.DeleteAsync(Id);
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

        public async Task<IEnumerable<About>> GetAllAsync()
        {
            ilogger.LogInformation($"GetAllAsync");
            return await unitOfWork.aboutRepository.GetAllAsync();
        }

        public async Task<About> GetByIdAsync(long Id)
        {
            try
            {
                var a = await unitOfWork.aboutRepository.GetByIdAsync(Id);
                try
                {
                    ilogger.LogInformation($"Get by id {Id.ToString()} Is {JsonConvert.SerializeObject(a)}");
                }
                catch (Exception ex)
                {
                    ilogger.LogInformation($"Get by id {Id.ToString()} Is {ex.Message}");
                }
                return a;
            }
            catch (Exception ex)
            {
                ilogger.LogError($"Get by id {Id.ToString()} Is Fail {ex.Message}");
                return default;
            }
        }

        public async Task<IPagedList<About>> GetListAsync(Expression<Func<About, bool>> expression, Func<About, object> sort, bool desc = false, int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                var a = await unitOfWork.aboutRepository.GetListByPage(expression, sort, desc, pageIndex, pageSize);
                ilogger.LogInformation($"GetListAsync expression, sort {desc} {pageIndex} {pageSize}");
                return a;
            }
            catch (Exception ex)
            {
                ilogger.LogError($"GetListAsync expression, sort {desc} {pageIndex} {pageSize} Is Fail {ex.Message}");
                return default;
            }
        }

        public async Task<bool> UpdateAsync(About article)
        {
            try
            {
                unitOfWork.aboutRepository.Update(article);
                await unitOfWork.SaveAsync();
                ilogger.LogInformation($"Update object  Is OK");//{JsonConvert.SerializeObject(article)}
                return true;
            }
            catch (Exception ex)
            {
                ilogger.LogError($"Update object  Is Fail {ex.Message}");//{JsonConvert.SerializeObject(article)}
                return false;
            }
        }
    }
}
