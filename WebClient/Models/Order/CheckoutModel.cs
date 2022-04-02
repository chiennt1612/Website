using System.ComponentModel.DataAnnotations;
using WebClient.Models.Category;

namespace WebClient.Models.Order
{
    public class CheckoutInput
    {
        /// <Order contact info >
        [StringLength(128, ErrorMessageResourceName = "StringLengthTooLong", ErrorMessageResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        [Display(Name = "Fullname", ResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        public string Fullname { get; set; }

        [StringLength(128, ErrorMessageResourceName = "StringLengthTooLong", ErrorMessageResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        [Display(Name = "Address", ResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        public string Address { get; set; }

        [StringLength(128, ErrorMessageResourceName = "StringLengthTooLong", ErrorMessageResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        public string Mobile { get; set; }

        [StringLength(250, ErrorMessageResourceName = "StringLengthTooLong", ErrorMessageResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        //[Display(Name = "Email", ResourceType = typeof(Resources.EntityValidation))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        [EmailAddress(ErrorMessageResourceName = "EmailIsNotValid", ErrorMessageResourceType = typeof(LanguageAll.Language))]
        public string Email { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Description", ResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        public string Description { get; set; }

        public bool IsAgree { get; set; }
        /// </Order contact info>
    }
    public class CheckoutModel
    {
        public CreateOrder order { get; set; }
        public ProductView product { get; set; }

        /// <Order contact info >
        [StringLength(128, ErrorMessageResourceName = "StringLengthTooLong", ErrorMessageResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        [Display(Name = "Fullname", ResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        public string Fullname { get; set; }

        [StringLength(128, ErrorMessageResourceName = "StringLengthTooLong", ErrorMessageResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        [Display(Name = "Address", ResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        public string Address { get; set; }

        [StringLength(128, ErrorMessageResourceName = "StringLengthTooLong", ErrorMessageResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        public string Mobile { get; set; }

        [StringLength(250, ErrorMessageResourceName = "StringLengthTooLong", ErrorMessageResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        //[Display(Name = "Email", ResourceType = typeof(Resources.EntityValidation))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        [EmailAddress(ErrorMessageResourceName = "EmailIsNotValid", ErrorMessageResourceType = typeof(LanguageAll.Language))]
        public string Email { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Description", ResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        //[Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        public string Description { get; set; }
        /// </Order contact info>

        public bool IsAgree { get; set; } = true;
    }
}
