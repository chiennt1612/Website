﻿using EntityFramework.Web.DBContext;
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
    public class ArticleServices : IArticleServices
    {
        private IUnitOfWork unitOfWork;
        private ILogger<ArticleServices> ilogger;
        public ArticleServices(IUnitOfWork unitOfWork, ILogger<ArticleServices> ilogger)
        {
            this.unitOfWork = unitOfWork;
            this.ilogger = ilogger;
        }

        public async Task<bool> AddAsync(Article article)
        {
            try
            {
                await unitOfWork.articleRepository.AddAsync(article);
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
                await unitOfWork.articleRepository.DeleteAsync(Id);
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
        public async Task<IEnumerable<NewsCategories>> CateGetAllAsync()
        {
            ilogger.LogInformation($"GetAllAsync");
            return await unitOfWork.articleRepository.CateGetAllAsync();
        }
        public async Task<IEnumerable<Article>> GetAllAsync()
        {
            ilogger.LogInformation($"GetAllAsync");
            return await unitOfWork.articleRepository.GetAllAsync();
        }
        public async Task<Article> GetByIdAsync(long Id)
        {
            try
            {
                var a = await unitOfWork.articleRepository.GetByIdAsync(Id);
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

        public async Task<IPagedList<Article>> GetListAsync(
            Expression<Func<Article, bool>> expression, Func<Article, object> sort, bool desc = false,
            int pageIndex = 1, int pageSize = Constants.PageSize)
        {
            try
            {
                var a = await unitOfWork.articleRepository.GetListByPage(expression, sort, desc, pageIndex, pageSize);
                ilogger.LogInformation($"GetListAsync expression, sort {desc} {pageIndex} {pageSize}");
                return a;
            }
            catch (Exception ex)
            {
                ilogger.LogError($"GetListAsync expression, sort {desc} {pageIndex} {pageSize} Is Fail {ex.Message}");
                return default;
            }
        }

        public async Task<bool> UpdateAsync(Article article)
        {
            try
            {
                unitOfWork.articleRepository.Update(article);
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
