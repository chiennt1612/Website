using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Logout()
        {
            //SignOut(CookieAuthenticationDefaults.AuthenticationScheme);
            return SignOut(CookieAuthenticationDefaults.AuthenticationScheme, "oidc");
        }
    }
}
