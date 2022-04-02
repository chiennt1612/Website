using EntityFramework.Web.Entities.Ordering;

namespace WebAdmin.Repository.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order, long>
    {
        //Task<Order> GetByIdAsync(long id);
        //// Get an entity using delegate
        //Task<BaseEntityList<Order>> GetListAsync(Expression<Func<Order, bool>> expression, Func<Order, object> sort, bool desc, int page, int pageSize);
        //Task<IPagedList<Order>> GetListByPage(
        //    Expression<Func<Order, bool>> expression, Func<Order, object> sort, bool desc = false,
        //    int pageIndex = 1, int pageSize = Constants.PageSize);
    }
}
