using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFramework.Web.Entities
{
    public class ParamSetting : BaseEntity
    {
        // For Company
        [StringLength(200, ErrorMessageResourceName = "StringLengthTooLong", ErrorMessageResourceType = typeof(Resources.EntityValidation))]
        [Display(Name = "CompanyLogo", ResourceType = typeof(Resources.EntityValidation))]
        public string Logo { get; set; }

        [Display(Name = "CompanySince", ResourceType = typeof(Resources.EntityValidation))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.EntityValidation))]
        [StringLength(100, ErrorMessageResourceName = "StringLengthTooLong", ErrorMessageResourceType = typeof(Resources.EntityValidation))]
        public string Since { get; set; }

        [Display(Name = "CompanyName", ResourceType = typeof(Resources.EntityValidation))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.EntityValidation))]
        [StringLength(200, ErrorMessageResourceName = "StringLengthTooLong", ErrorMessageResourceType = typeof(Resources.EntityValidation))]
        public string Name { get; set; }

        [Display(Name = "Address", ResourceType = typeof(Resources.EntityValidation))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.EntityValidation))]
        [StringLength(200, ErrorMessageResourceName = "StringLengthTooLong", ErrorMessageResourceType = typeof(Resources.EntityValidation))]
        public string Address { get; set; }

        [Display(Name = "Hotline", ResourceType = typeof(Resources.EntityValidation))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.EntityValidation))]
        [StringLength(50, ErrorMessageResourceName = "StringLengthTooLong", ErrorMessageResourceType = typeof(Resources.EntityValidation))]
        public string Hotline { get; set; }

        [Display(Name = "Phone", ResourceType = typeof(Resources.EntityValidation))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.EntityValidation))]
        [StringLength(50, ErrorMessageResourceName = "StringLengthTooLong", ErrorMessageResourceType = typeof(Resources.EntityValidation))]
        public string Phone { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Resources.EntityValidation))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.EntityValidation))]
        [StringLength(200, ErrorMessageResourceName = "StringLengthTooLong", ErrorMessageResourceType = typeof(Resources.EntityValidation))]
        public string Email { get; set; }

    }
}
