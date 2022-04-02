using EntityFramework.Web.Entities;
using System.Collections.Generic;

namespace WebNuoc.Models
{
    public class ServiceDetail
    {
        public Service _Detail { get; set; }
        public IEnumerable<Service> _Related { get; set; }
    }
}
