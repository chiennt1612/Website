using System.ComponentModel.DataAnnotations;

namespace SSO.Models.ManageViewModels
{
    public class IndexViewModel
    {
        [Display(Name = "UserName", ResourceType = typeof(LanguageAll.Language))]
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public bool IsPhoneNumberConfirmed { get; set; }

        [Required]
        [EmailAddress(ErrorMessageResourceName = "EmailIsNotValid", ErrorMessageResourceType = typeof(LanguageAll.Language))]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Mobile")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }
    }
}
