using System.ComponentModel.DataAnnotations;

namespace SSO.Models.AccountViewModels
{
    public class LoginWithRecoveryCodeViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "RecoveryCode", ResourceType = typeof(LanguageAll.Language))]
        public string RecoveryCode { get; set; }
    }
}
