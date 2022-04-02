using EntityFramework.Web.Entities;

namespace WebNuoc.Models
{
    public class ServiceRegister
    {
        public ServiceRegister()
        {
            service = new Service();
            contact = new Contact();
        }
        public Service service { get; set; }
        public Contact contact { get; set; }
    }
}
