using EntityFramework.Web.Entities.Ordering;
using System.Threading.Tasks;

namespace WebAdmin.Repository.Interfaces
{
    public interface IAddressRepository : IGenericRepository<Address, int>
    {
        Task<Address> GetByUserIdAsync(long UserId);
        //Task<BaseEntityList<Address>> GetListAsync(Expression<Func<Address, bool>> expression, Func<Address, object> sort, bool desc, int page, int pageSize);
        //Task<IPagedList<Address>> GetListByPage(
        //    Expression<Func<Address, bool>> expression, Func<Address, object> sort, bool desc = false,
        //    int pageIndex = 1, int pageSize = Constants.PageSize);
        //Task<Address> GetByIdAsync(int id);
    }
}
