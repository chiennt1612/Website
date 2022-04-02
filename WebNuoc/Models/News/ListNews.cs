using EntityFramework.Web.Entities;
using System.Collections.Generic;

namespace WebNuoc.Models.News
{
    public class ListNews
    {
        public string Keyword { get; set; }
        public long CategoryId { get; set; }
        public BaseEntityList<Article> listArticle { get; set; }
        public IList<Article> articles { get; set; }
        public IEnumerable<Adv> rightAdvs { get; set; }
    }
}
