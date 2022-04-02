using System.ComponentModel.DataAnnotations;

namespace SSO.Models.AccountViewModels
{
    public class LoginWith2faViewModel
    {
        [Required]
        [StringLength(7, ErrorMessageResourceName = "PasswordLength", ErrorMessageResourceType = typeof(LanguageAll.Language), MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "AuthenticatorCode", ResourceType = typeof(LanguageAll.Language))]
        public string TwoFactorCode { get; set; }

        [Display(Name = "RememberThisMachine", ResourceType = typeof(LanguageAll.Language))]
        public bool RememberMachine { get; set; }

        public bool RememberMe { get; set; }
    }
}
