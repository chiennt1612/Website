using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SSO.Models.ManageViewModels
{
    public class IndexViewModel
    {
        [Display(Name = "Tên tài khoản")]
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public bool IsPhoneNumberConfirmed { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Địa chỉ Email")]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Mobile")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }
    }
}
