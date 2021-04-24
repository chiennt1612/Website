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
    public class ProductServices : IProductServices
    {
        private IUnitOfWork unitOfWork;
        private ILogger<ProductServices> ilogger;
        public ProductServices(IUnitOfWork unitOfWork, ILogger<ProductServices> ilogger)
        {
            this.unitOfWork = unitOfWork;
            this.ilogger = ilogger;
        }

        public async Task<bool> AddAsync(Product _Product)
        {
            try
            {
                await unitOfWork.productRepository.AddAsync(_Product);
                await unitOfWork.SaveAsync();
                ilogger.LogInformation($"Save object {JsonConvert.SerializeObject(_Product)} Is OK");
                return true;
            }
            catch (Exception ex)
            {
                ilogger.LogError($"Save object {JsonConvert.SerializeObject(_Product)} Is Fail {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteByIdAsync(long Id)
        {
            try
            {
                await unitOfWork.productRepository.DeleteAsync(Id);
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

        public async Task<Product> GetByIdAsync(long Id)
        {
            try
            {
                var a = await unitOfWork.productRepository.GetByIdAsync(Id);
                ilogger.LogInformation($"Get by id {Id.ToString()} Is {JsonConvert.SerializeObject(a)}");
                return a;
            }
            catch (Exception ex)
            {
                ilogger.LogError($"Get by id {Id.ToString()} Is Fail {ex.Message}");
                return default;
            }
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            ilogger.LogInformation($"GetAllAsync");
            return await unitOfWork.productRepository.GetAllAsync();
        }
        public async Task<IEnumerable<Categories>> CateGetAllAsync()
        {
            ilogger.LogInformation($"GetAllAsync");
            return await unitOfWork.productRepository.CateGetAllAsync();
        }
        public async Task<IPagedList<Product>> GetListAsync(
            Expression<Func<Product, bool>> expression, Func<Product, string> sort, bool desc = false,
            int pageIndex = 1, int pageSize = Constants.PageSize)
        {
            try
            {
                var a = await unitOfWork.productRepository.GetListByPage(expression, sort, desc, pageIndex, pageSize);
                ilogger.LogInformation($"GetListAsync expression, sort {desc} {pageIndex} {pageSize}");
                return a;
            }
            catch (Exception ex)
            {
                ilogger.LogError($"GetListAsync expression, sort {desc} {pageIndex} {pageSize} Is Fail {ex.Message}");
                return default;
            }
        }

        public async Task<bool> UpdateAsync(Product _Product)
        {
            try
            {
                unitOfWork.productRepository.Update(_Product);
                await unitOfWork.SaveAsync();
                ilogger.LogInformation($"Update object {JsonConvert.SerializeObject(_Product)} Is OK");
                return true;
            }
            catch (Exception ex)
            {
                ilogger.LogError($"Update object {JsonConvert.SerializeObject(_Product)} Is Fail {ex.Message}");
                return false;
            }
        }
    }
}
