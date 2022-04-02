using EntityFramework.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebClient.Repository.Interfaces
{
    public interface IArticleRepository : IGenericRepository<Article, long>
    {
        Task<IEnumerable<NewsCategories>> CateGetAllAsync();
        Task<IEnumerable<Article>> GetTopAsync(Expression<Func<Article, bool>> expression, int top);
    }
}
