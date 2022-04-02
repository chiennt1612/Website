using System.ComponentModel.DataAnnotations;

namespace SSO.Models.AccountViewModels
{
    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "OTPCode", ResourceType = typeof(LanguageAll.Language))]
        public string Code { get; set; }

        public string ReturnUrl { get; set; }

        [Display(Name = "RememberOTP", ResourceType = typeof(LanguageAll.Language))]
        public bool RememberBrowser { get; set; }

        [Display(Name = "Remember", ResourceType = typeof(LanguageAll.Language))]
        public bool RememberMe { get; set; }
    }
}
