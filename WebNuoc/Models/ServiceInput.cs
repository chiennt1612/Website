using System.ComponentModel.DataAnnotations;

namespace WebNuoc.Models
{
    public class ServiceInput
    {
        [Display(Name = "ServiceName", ResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        public long ServiceId { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        public bool IsCompany { get; set; }

        [Display(Name = "Price", ResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        public double Price { get; set; }

        [StringLength(128, ErrorMessageResourceName = "StringLengthTooLong", ErrorMessageResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        [Display(Name = "Fullname", ResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        public string Fullname { get; set; }

        [StringLength(300, ErrorMessageResourceName = "StringLengthTooLong", ErrorMessageResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        [Display(Name = "Address", ResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        public string Address { get; set; }

        [StringLength(20, ErrorMessageResourceName = "StringLengthTooLong", ErrorMessageResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        public string Mobile { get; set; }

        [StringLength(250, ErrorMessageResourceName = "StringLengthTooLong", ErrorMessageResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        [Display(Name = "Email", ResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        [EmailAddress(ErrorMessageResourceName = "EmailIsNotValid", ErrorMessageResourceType = typeof(LanguageAll.Language))]
        public string Email { get; set; }

        [StringLength(250, ErrorMessageResourceName = "StringLengthTooLong", ErrorMessageResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        public string CompanyName { get; set; } // CMND

        [StringLength(2000, ErrorMessageResourceName = "StringLengthTooLong", ErrorMessageResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        [Display(Name = "Description", ResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        public string Description { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        public int PaymentMethod { get; set; }

        public bool IsAgree { get; set; } = true;
    }
}
