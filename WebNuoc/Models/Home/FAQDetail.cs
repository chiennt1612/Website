using EntityFramework.Web.Entities;
using System.Collections.Generic;

namespace WebNuoc.Models.Home
{
    public class FAQDetail
    {
        public FAQ fAQ { get; set; }
        public IEnumerable<FAQ> fAQs { get; set; }
    }
}
