using EntityFramework.Web.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Paygate.OnePay;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using WebClient.Helpers;
using WebClient.Models;
using WebClient.Models.Home;
using WebClient.Services.Interfaces;

namespace WebClient.Controllers
{
    //[SecurityHeaders]
    public class HomeController : Controller
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger<HomeController> _logger;
        private readonly IAllService _Service;
        private readonly IStringLocalizer<HomeController> _localizer;
        private readonly IEmailSender _emailSender;

        public HomeController(IEmailSender _emailSender, IDistributedCache _cache, ILogger<HomeController> _logger, IAllService _Service, IStringLocalizer<HomeController> _localizer)
        {
            this._logger = _logger;
            this._Service = _Service;
            this._localizer = _localizer;
            this._cache = _cache;
            this._emailSender = _emailSender;
        }

        public async Task<IActionResult> Index()
        {
            PathOfPage path = new PathOfPage();
            (await ViewData.InitialAsync(_Service)).SetPage(path);
            return View();
        }

        [Route("/[action]/{Id}")]
        public async Task<IActionResult> About(long? Id)
        {
            long _Id = (Id.HasValue ? Id.Value : 0);
            var a = await _Service.aboutServices.GetByIdAsync(_Id);

            PathOfPage path = new PathOfPage() { IsProduct = 3, Total = 0, TotalPage = 0, Page = 1, PathName = { a.Title } };
            (await ViewData.InitialAsync(_Service)).SetPage(path);

            return View(a);
        }

        [Route("/[action]")]
        public async Task<IActionResult> Contact()
        {
            var a = new Contact();
            if (User.Identity.IsAuthenticated)
            {
                a.Fullname = User.Claims.GetClaimValue("name");
                a.Email = User.Claims.GetClaimValue("email");
                a.Mobile = User.Claims.GetClaimValue("phone_number");
                a.Address = User.Claims.GetClaimValue("address");
            }

            ViewData["ParamSetting"] = await _Service.paramSettingServices.GetAllAsync();
            ViewData["About10"] = await _Service.aboutServices.GetByIdAsync(10);
            return View("LienHe", a);
        }

        [Route("/[action]")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(Contact contact)
        {
            ViewData.SetNotification(_localizer.GetString("Kiểm tra lại dữ liệu!"));
            ViewData["Re-Contact"] = true;
            if (ModelState.IsValid)
            {
                if (await _Service.contactServices.AddAsync(contact))
                {
                    ViewData.SetNotification(_localizer.GetString("Gửi thành công"));
                    _logger.LogInformation($"Send contact is success: {JsonConvert.SerializeObject(contact)}");
                    ViewData["Re-Contact"] = false;
                    string msg = $"<ul><li><b>{_localizer["Tên đầy đủ"]}:</b> {contact.Fullname}</li><li><b>Email:</b> {contact.Email}</li><li><b>Mobile:</b> {contact.Mobile}</li><li>{contact.Description}</li></ul>";
                    await _emailSender.SendEmailAsync(contact.Email, _localizer.GetString("v.v Liên hệ Ngọc Tuấn"), msg);
                }
                else
                {
                    ViewData.SetNotification(_localizer.GetString("Gửi thất bại"));
                    _logger.LogInformation($"Send contact is fail: {JsonConvert.SerializeObject(contact)}");
                }
            }

            ViewData["ParamSetting"] = await _Service.paramSettingServices.GetAllAsync();
            ViewData["About10"] = await _Service.aboutServices.GetByIdAsync(10);
            return View("LienHe", contact);
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Privacy()
        {
            PathOfPage path = new PathOfPage() { IsProduct = 3, Total = 0, TotalPage = 0, Page = 1, PathName = { _localizer.GetString("Chính sách") } };
            (await ViewData.InitialAsync(_Service)).SetPage(path);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            PathOfPage path = new PathOfPage() { IsProduct = 3, Total = 0, TotalPage = 0, Page = 1, PathName = { _localizer.GetString("Lỗi") } };
            (await ViewData.InitialAsync(_Service)).SetPage(path);
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error404()
        {
            PathOfPage path = new PathOfPage() { IsProduct = 3, Total = 0, TotalPage = 0, Page = 1, PathName = { _localizer.GetString("Lỗi") } };
            (await ViewData.InitialAsync(_Service)).SetPage(path);
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
    }
}
