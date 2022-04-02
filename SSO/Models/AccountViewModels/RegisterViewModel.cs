using System.ComponentModel.DataAnnotations;

namespace SSO.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        public string OnError { get; set; }
        [Required]
        [EmailAddress(ErrorMessageResourceName = "EmailIsNotValid", ErrorMessageResourceType = typeof(LanguageAll.Language))]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "UserName", ResourceType = typeof(LanguageAll.Language))]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceName = "PasswordLength", ErrorMessageResourceType = typeof(LanguageAll.Language), MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(LanguageAll.Language))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(LanguageAll.Language))]
        [Compare("Password", ErrorMessageResourceName = "confirmationnotmatch", ErrorMessageResourceType = typeof(LanguageAll.Language))]
        public string ConfirmPassword { get; set; }

        //[Display(Name = "Tôi đã đọc và Đồng ý với <u><a href=\"#\">Điều khoản</a></u> dịch vụ!")]
        public bool IsAgree { get; set; } = true;
    }

    public class RegisterUser
    {
        [Required]
        [EmailAddress(ErrorMessageResourceName = "EmailIsNotValid", ErrorMessageResourceType = typeof(LanguageAll.Language))]
        [Display(Name = "Email")]
        public string Email { get; set; }

        //[Display(Name = "AYs UserName")]
        //[Required]
        public string OldId { get; set; }

        [Display(Name = "UserName")]
        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceName = "PasswordLength", ErrorMessageResourceType = typeof(LanguageAll.Language), MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessageResourceName = "confirmationnotmatch", ErrorMessageResourceType = typeof(LanguageAll.Language))]
        public string ConfirmPassword { get; set; }
    }
}
