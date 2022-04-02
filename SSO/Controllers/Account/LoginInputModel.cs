// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.ComponentModel.DataAnnotations;

namespace SSO.Controllers
{
    public class LoginInputModel
    {
        [Required]
        [Display(Name = "UserName", ResourceType = typeof(LanguageAll.Language))]
        public string Username { get; set; }
        [Required]
        [Display(Name = "Password", ResourceType = typeof(LanguageAll.Language))]
        public string Password { get; set; }
        [Display(Name = "RememberLogin", ResourceType = typeof(LanguageAll.Language))]
        public bool RememberLogin { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember", ResourceType = typeof(LanguageAll.Language))]
        public bool RememberMe { get; set; }
    }
}