using EntityFramework.Web.Entities;
using System.Collections.Generic;

namespace WebNuoc.Models.News
{
    public class NewsDetails
    {
        public Article article { get; set; }
        public IList<Article> articles { get; set; }
        public IEnumerable<Adv> rightAdvs { get; set; }
        public IList<Article> articleRelated { get; set; }
    }
}
