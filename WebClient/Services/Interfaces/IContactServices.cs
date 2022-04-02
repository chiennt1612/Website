using EntityFramework.Web.Entities;
using System.Threading.Tasks;

namespace WebClient.Services.Interfaces
{
    public interface IContactServices
    {
        Task<bool> AddAsync(Contact contact);
        Task<Contact> GetByIdAsync(long Id);
    }
}
