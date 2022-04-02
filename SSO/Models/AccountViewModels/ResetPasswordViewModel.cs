﻿using System.ComponentModel.DataAnnotations;

namespace SSO.Models.AccountViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress(ErrorMessageResourceName = "EmailIsNotValid", ErrorMessageResourceType = typeof(LanguageAll.Language))]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceName = "PasswordLength", ErrorMessageResourceType = typeof(LanguageAll.Language), MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(LanguageAll.Language))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(LanguageAll.Language))]
        [Compare("Password", ErrorMessageResourceName = "confirmationnotmatch", ErrorMessageResourceType = typeof(LanguageAll.Language))]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }

        public string OnError { get; set; }
    }
}
