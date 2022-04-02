using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;

namespace AYsLogin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IConfiguration configuration;
        private const string KeyState = "State_AYsLogin";
        private readonly string _Id;
        private readonly string UrlAYs;
        public HomeController(IConfiguration configuration, ILogger<HomeController> logger)
        {
            _Id = Guid.NewGuid().ToString();
            _logger = logger;
            this.configuration = configuration;
            UrlAYs = this.configuration.GetSection("UrlAYs").Value.ToString();
        }
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var a = new Tools.DecryptorProvider();
                var username = User.Claims.FirstOrDefault(u => u.Type == "oldid").Value;
                LoginInfo b = new LoginInfo()
                {
                    Email = User.Claims.FirstOrDefault(u => u.Type == "email").Value,
                    ID = User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value,
                    SID = User.Claims.FirstOrDefault(u => u.Type == "sid").Value,
                    OLDID = User.Claims.FirstOrDefault(u => u.Type == "oldid").Value,
                    NAME = User.Claims.FirstOrDefault(u => u.Type == "name").Value
                };

                var c = ReadCookie(HttpContext, KeyState);
                if (String.IsNullOrEmpty(c)) return Content("No State Request");

                var url = UrlAYs + "/authorityWithSSO.aspx" +
                    "?Token=" + System.Net.WebUtility.UrlEncode(a.Encrypt(Newtonsoft.Json.JsonConvert.SerializeObject(b))) +
                    "&State=" + System.Net.WebUtility.UrlEncode(c) +
                    "&oldid=" + System.Net.WebUtility.UrlEncode(a.Encrypt(username));
                _logger.LogInformation($"Login  {_Id} {url}");
                return Redirect(url);
            }
            return Content("No login");
        }

        [Route("/[action]")]
        public IActionResult Logout()
        {
            _logger.LogInformation($"Logout  {_Id}");
            return SignOut(CookieAuthenticationDefaults.AuthenticationScheme, "oidc");
        }

        [AllowAnonymous]
        [Route("/[action]")]
        public IActionResult Login(string State)
        {
            _logger.LogInformation($"Login  {_Id}");
            ViewData["State"] = State;
            WriteCookie(HttpContext, KeyState, State);
            return View("Login", State);
        }

        #region Cookie
        private string ReadCookie(HttpContext context, string key)
        {
            var a = context.Request.Cookies[key];
            _logger.LogInformation($"Read cookie {_Id}: {a}");
            return a;
        }

        private void WriteCookie(HttpContext context, string key, string obj, string _Path = "/", long _time = 10)
        {
            _logger.LogInformation($"Write cookie {_Id}: {obj}");
            string h = "";
            int i = context.Request.Host.Value.IndexOf(":");
            if (i > 1)
            {
                h = context.Request.Host.Value.Substring(0, i);
            }
            else
            {
                h = context.Request.Host.Value;
            }
            context
                .Response
                .Cookies
                .Append(key, obj, new CookieOptions()
                {
                    Expires = DateTime.Now.AddMinutes(_time),
                    SameSite = SameSiteMode.Strict,
                    Secure = true,
                    HttpOnly = true,
                    Domain = h,
                    Path = _Path
                });
        }
        #endregion
    }

    public class LoginInfo
    {
        public string Email { get; set; }
        public string ID { get; set; }
        public string NAME { get; set; }
        public string SID { get; set; }
        public string OLDID { get; set; }
    }
}
