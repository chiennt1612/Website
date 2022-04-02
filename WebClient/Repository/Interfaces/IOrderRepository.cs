using EntityFramework.Web.Entities.Ordering;
using System.Collections.Generic;
using System.Threading.Tasks;
//using X.PagedList;

namespace WebClient.Repository.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order, long>
    {
        //Task<Order> GetByIdAsync(long id);
        // Get an entity using delegate
        //Task<BaseEntityList<Order>> GetListAsync(Expression<Func<Order, bool>> expression, Func<Order, object> sort, bool desc, int page, int pageSize);
    }

    public interface IOrderItemRepository : IGenericRepository<OrderItem, long>
    {
        //Task<Order> GetByIdAsync(long id);
        // Get an entity using delegate
        //Task<BaseEntityList<Order>> GetListAsync(Expression<Func<Order, bool>> expression, Func<Order, object> sort, bool desc, int page, int pageSize);
        Task<List<OrderItem>> GetByOrderIdAsync(long id);
    }
}
