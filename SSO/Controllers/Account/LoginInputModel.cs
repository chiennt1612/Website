// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.ComponentModel.DataAnnotations;

namespace SSO.Controllers
{
    public class LoginInputModel
    {
        [Required]
        [Display(Name = "Tên đăng nhập")]
        public string Username { get; set; }
        [Required]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }
        [Display(Name = "Ghi nhớ đăng nhập")]
        public bool RememberLogin { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}