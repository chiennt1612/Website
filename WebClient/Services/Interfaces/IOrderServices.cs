using EntityFramework.Web.Entities;
using EntityFramework.Web.Entities.Ordering;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebClient.Services.Interfaces
{
    public interface IOrderServices
    {
        Task<Order> Add(Order order);
        Task<Order> Update(Order order);
        Task<Order> GetByIdAsync(long Id);
        Task<BaseEntityList<Order>> GetListAsync(
            Expression<Func<Order, bool>> expression,
            Func<Order, object> sort, bool desc,
            int page, int pageSize);
    }
}
