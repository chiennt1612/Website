using System.ComponentModel.DataAnnotations;

namespace SSO.Models.ManageViewModels
{
    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessageResourceName = "PasswordLength", ErrorMessageResourceType = typeof(LanguageAll.Language), MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(LanguageAll.Language))]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(LanguageAll.Language))]
        [Compare("NewPassword", ErrorMessageResourceName = "confirmationnotmatch", ErrorMessageResourceType = typeof(LanguageAll.Language))]
        public string ConfirmPassword { get; set; }

        public string StatusMessage { get; set; }
    }
}
