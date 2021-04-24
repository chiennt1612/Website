using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SSO.Models.AccountViewModels
{
    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Nhập mã OTP")]
        public string Code { get; set; }

        public string ReturnUrl { get; set; }

        [Display(Name = "Ghi nhớ OTP?")]
        public bool RememberBrowser { get; set; }

        [Display(Name = "Ghi nhớ tài khoản?")]
        public bool RememberMe { get; set; }
    }
}
