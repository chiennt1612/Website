using System.ComponentModel.DataAnnotations;
using WebNuoc.Models;

namespace WebNuoc.Models
{
    public class LoginModel
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        [Display(Name = "Username", ResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        public string Username { get; set; }
    }

    public class LoginEVNModel
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        [Display(Name = "EVNCode", ResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        public string EVNCode { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        [Display(Name = "Fullname", ResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        public string Fullname { get; set; }
        [Display(Name = "Email", ResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        [Display(Name = "Username", ResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        public string Username { get; set; }
        [Display(Name = "Address", ResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        public string Address { get; set; }
    }

    public class OTPModel
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        [Display(Name = "OTP", ResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        public string Code { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        [Display(Name = "Username", ResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        public string Username { get; set; }
        public string? DeviceId { get; set; }
    }

    public class DeviceModel : DeviceTokenModel
    {
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        public string DeviceId { get; set; }

        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(EntityFramework.Web.Resources.EntityValidation))]
        public OSType OS { get; set; }

        public bool IsGetNotice { get; set; }
    }

    public class DeviceTokenModel
    {
        public string Token { get; set; }
    }

    public enum OSType
    {
        IOs = 2,
        Android = 1,
        PC = 0
    }
}
