using EntityFramework.Web.Entities;
using System.Collections.Generic;

namespace WebClient.Models.Category
{
    public class ProductDetails
    {
        public IEnumerable<ProductView> productView { get; set; }
        public IEnumerable<Product> productRelated { get; set; }
        public Product product { get; set; }
        public IEnumerable<ParamSetting> paramSetting { get; set; }
    }
}
