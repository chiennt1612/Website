using System;
using System.Collections.Generic;
//using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFramework.Web.Entities
{
    public class Article : MetaEntity
    {
        [ForeignKey("NewsCategories")]
        [Display(Name = "CateMain", ResourceType = typeof(Resources.EntityValidation))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.EntityValidation))]
        public long CategoryMain { get; set; }
        [Display(Name = "NewsCategories", ResourceType = typeof(Resources.EntityValidation))]
        public NewsCategories NewsCategories { get; set; }

        [StringLength(1000, ErrorMessageResourceName = "StringLengthTooLong", ErrorMessageResourceType = typeof(Resources.EntityValidation))]
        [Display(Name = "CateRefer", ResourceType = typeof(Resources.EntityValidation))]
        public string CategoryReference { get; set; }
        // For Company
        [StringLength(500, ErrorMessageResourceName = "StringLengthTooLong", ErrorMessageResourceType = typeof(Resources.EntityValidation))]
        [Display(Name = "TagsRefer", ResourceType = typeof(Resources.EntityValidation))]
        public string TagsReference { get; set; }

        // For Company
        [StringLength(500, ErrorMessageResourceName = "StringLengthTooLong", ErrorMessageResourceType = typeof(Resources.EntityValidation))]
        [Display(Name = "Title", ResourceType = typeof(Resources.EntityValidation))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.EntityValidation))]
        public string Title { get; set; }

        [StringLength(200, ErrorMessageResourceName = "StringLengthTooLong", ErrorMessageResourceType = typeof(Resources.EntityValidation))]
        [Display(Name = "Img", ResourceType = typeof(Resources.EntityValidation))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.EntityValidation))]
        public string Img { get; set; }

        [StringLength(200, ErrorMessageResourceName = "StringLengthTooLong", ErrorMessageResourceType = typeof(Resources.EntityValidation))]
        [Display(Name = "ImgBanner", ResourceType = typeof(Resources.EntityValidation))]
        public string ImgBanner { get; set; }

        [StringLength(2000, ErrorMessageResourceName = "StringLengthTooLong", ErrorMessageResourceType = typeof(Resources.EntityValidation))]
        [Display(Name = "Summary", ResourceType = typeof(Resources.EntityValidation))]
        public string Summary { get; set; }


        [DataType(DataType.MultilineText)]
        [Display(Name = "Description", ResourceType = typeof(Resources.EntityValidation))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.EntityValidation))]
        public string Description { get; set; }

        [Display(Name = "IsNews", ResourceType = typeof(Resources.EntityValidation))]
        public bool IsNews { get; set; }

        [Display(Name = "Status", ResourceType = typeof(Resources.EntityValidation))]
        public bool Status { get; set; }

        [Display(Name = "Publisher", ResourceType = typeof(Resources.EntityValidation))]
        [StringLength(200, ErrorMessageResourceName = "StringLengthTooLong", ErrorMessageResourceType = typeof(Resources.EntityValidation))]
        public string Publisher { get; set; }
    }
}
