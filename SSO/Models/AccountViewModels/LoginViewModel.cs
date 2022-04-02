using System.ComponentModel.DataAnnotations;

namespace SSO.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress(ErrorMessageResourceName = "EmailIsNotValid", ErrorMessageResourceType = typeof(LanguageAll.Language))]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(LanguageAll.Language))]
        public string Password { get; set; }

        [Display(Name = "Remember", ResourceType = typeof(LanguageAll.Language))]
        public bool RememberMe { get; set; }
    }
}
