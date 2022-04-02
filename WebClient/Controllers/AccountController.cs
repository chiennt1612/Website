using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebClient.Helpers;
using WebClient.Models.Home;
using WebClient.Services.Interfaces;

namespace WebClient.Controllers
{
    [SecurityHeaders]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger<AccountController> _logger;
        private readonly IAllService _Service;
        private readonly IStringLocalizer<AccountController> _localizer;

        public AccountController(IDistributedCache _cache, ILogger<AccountController> _logger, IAllService _Service, IStringLocalizer<AccountController> _localizer)
        {
            this._logger = _logger;
            this._Service = _Service;
            this._localizer = _localizer;
            this._cache = _cache;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/[controller]/[action]")]
        public async Task<IActionResult> AccessDenied()
        {
            PathOfPage path = new PathOfPage() { IsProduct = 3, Total = 0, TotalPage = 0, Page = 1, PathName = { _localizer["Không có quyền truy cập"] } };
            (await ViewData.InitialAsync(_Service)).SetPage(path);
            return View();
        }

        [Route("/[controller]/[action]")]
        public async Task<IActionResult> Profile()
        {
            return View();
        }

        [Route("/[controller]/[action]")]
        public IActionResult Logout()
        {
            return SignOut(CookieAuthenticationDefaults.AuthenticationScheme, "oidc");
        }

        [Route("/[controller]/[action]")]
        public IActionResult Login()
        {
            return RedirectToAction(nameof(Profile));
        }
    }
}
