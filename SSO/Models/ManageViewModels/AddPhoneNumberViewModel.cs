using System.ComponentModel.DataAnnotations;

namespace SSO.Models.ManageViewModels
{
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Mobile")]
        public string PhoneNumber { get; set; }
    }
}
