using EntityFramework.Web.Entities;

namespace WebNuoc.Models.Home
{
    public class ListFAQs
    {
        public string Keyword { get; set; }
        public BaseEntityList<FAQ> listFAQ { get; set; }
    }
}
