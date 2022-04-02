﻿using EntityFramework.Web.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebClient.Repository.Interfaces;
using WebClient.Services.Interfaces;

namespace WebClient.Services
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

        public async Task<Product> GetByIdAsync(long Id)
        {
            try
            {
                var a = await unitOfWork.productRepository.GetByIdAsync(Id);
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

        public async Task<Product> GetByCodeAsync(string Code)
        {
            try
            {
                var a = await unitOfWork.productRepository.GetByCodeAsync(Code);
                try
                {
                    ilogger.LogInformation($"Get by Code {Code} Is {JsonConvert.SerializeObject(a)}");
                }
                catch (Exception ex)
                {
                    ilogger.LogInformation($"Get by Code {Code} Is {ex.Message}");
                }
                return a;
            }
            catch (Exception ex)
            {
                ilogger.LogError($"Get by Code {Code} Is Fail {ex.Message}");
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
        public async Task<BaseEntityList<Product>> GetListAsync(
            Expression<Func<Product, bool>> expression,
            Func<Product, object> sort, bool desc,
            int page, int pageSize)
        {
            try
            {
                var a = await unitOfWork.productRepository.GetListAsync(expression, sort, desc, page, pageSize);
                ilogger.LogInformation($"GetListAsync expression, sort {desc} {page} {pageSize}");
                return a;
            }
            catch (Exception ex)
            {
                ilogger.LogError($"GetListAsync expression, sort {desc} {page} {pageSize} Is Fail {ex.Message}");
                return default;
            }
        }
        public async Task<IEnumerable<Product>> GetTopAsync(Expression<Func<Product, bool>> expression, int top)
        {
            return await unitOfWork.productRepository.GetTopAsync(expression, top);
        }
    }
}
