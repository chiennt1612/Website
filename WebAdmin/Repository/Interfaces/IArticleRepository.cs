using EntityFramework.Web.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAdmin.Repository.Interfaces
{
    public interface IArticleRepository : IGenericRepository<Article, long>
    {
        Task<IEnumerable<NewsCategories>> CateGetAllAsync();
    }
}
