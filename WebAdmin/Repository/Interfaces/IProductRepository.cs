using EntityFramework.Web.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAdmin.Repository.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product, long>
    {
        Task<IEnumerable<Categories>> CateGetAllAsync();
    }
}
