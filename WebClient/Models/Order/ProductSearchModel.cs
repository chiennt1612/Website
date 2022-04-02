using EntityFramework.Web.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebClient.Models.Order
{
    public class ProductSearchModel
    {
        public IEnumerable<Product> products { get; set; }
        public IEnumerable<ParamSetting> paramSetting { get; set; }
        public int? Page { get; set; }
        public int? TotalRow { get; set; }
        public int? TotalPage { get; set; }
        public long? CategoryId { get; set; }

        [StringLength(128, ErrorMessageResourceName = "StringLengthTooLong", ErrorMessageResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        [Display(Name = "Keyword", ResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        public string Keyword { get; set; }
        [Display(Name = "MinPrice", ResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        public double? min_price { get; set; }
        [Display(Name = "MaxPrice", ResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        public double? max_price { get; set; }
    }
}
