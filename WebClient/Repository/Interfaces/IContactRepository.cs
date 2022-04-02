using EntityFramework.Web.Entities;

namespace WebClient.Repository.Interfaces
{
    public interface IContactRepository : IGenericRepository<Contact, long>
    {
    }
}
