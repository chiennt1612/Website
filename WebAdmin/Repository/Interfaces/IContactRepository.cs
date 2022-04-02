using EntityFramework.Web.Entities;

namespace WebAdmin.Repository.Interfaces
{
    public interface IContactRepository : IGenericRepository<Contact, long>
    {
        //Task<BaseEntityList<Contact>> GetListAsync(Expression<Func<Contact, bool>> expression, Func<Contact, object> sort, bool desc, int page, int pageSize);
        //Task<IPagedList<Contact>> GetListByPage(
        //    Expression<Func<Contact, bool>> expression, Func<Contact, object> sort, bool desc = false,
        //    int pageIndex = 1, int pageSize = Constants.PageSize);
        //Task<Contact> GetByIdAsync(long id);
    }
}
