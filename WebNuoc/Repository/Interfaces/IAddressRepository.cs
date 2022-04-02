using EntityFramework.Web.Entities.Ordering;
//using X.PagedList;

namespace WebNuoc.Repository.Interfaces
{
    public interface IAddressRepository : IGenericRepository<Address, int>
    {
        //Task<BaseEntityList<Address>> GetListAsync(Expression<Func<Address, bool>> expression, Func<Address, object> sort, bool desc, int page, int pageSize);
        //Task<Address> GetByIdAsync(int id);
    }
}
