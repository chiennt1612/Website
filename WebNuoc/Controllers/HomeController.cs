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
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebNuoc.Models;
using WebNuoc.Models.Home;
using WebNuoc.Services.Interfaces;

namespace WebNuoc.Controllers
{
    //[SecurityHeaders]
    public class HomeController : Controller
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger<HomeController> _logger;
        private readonly IAllService _Service;
        private readonly IStringLocalizer<HomeController> _localizer;
        private readonly IEmailSender _emailSender;
        private const int PageSize = 20;

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
            await Task.Delay(0);
            //PathOfPage path = new PathOfPage();
            //await ViewDataParam.SetViewData(this._logger, ViewData, this._Service, path);
            return View();
        }

        [Route("/[action]/{Id?}")]
        public async Task<IActionResult> About(long? Id)
        {
            long _Id = (Id.HasValue ? Id.Value : 0);
            var a = await _Service.aboutServices.GetByIdAsync(_Id);
            PathOfPage path = new PathOfPage() { IsProduct = 3, Total = 0, TotalPage = 0, Page = 1, PathName = { a.Title } };
            //await ViewDataParam.SetViewData(this._logger, ViewData, this._Service, path);

            return View(a);
        }

        [Route("/[action]/{Id?}")]
        public async Task<IActionResult> FAQDetails(long? Id)
        {

            long _Id = (Id.HasValue ? Id.Value : 0);
            var a = await _Service.fAQServices.GetByIdAsync(_Id);
            Expression<Func<FAQ, bool>> sqlWhere = u => (u.Id < _Id);
            var b = new FAQDetail()
            {
                fAQ = a,
                fAQs = await _Service.fAQServices.GetTopAsync(sqlWhere, 10)
            };
            return View(b);
        }

        [Route("/[action]/{Page?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FAQ(int? Page, SearchInput contact)
        {
            int _Page = (Page.HasValue ? Page.Value : 1);
            Func<FAQ, object> sqlOrder = s => s.Id;
            Expression<Func<FAQ, bool>> sqlWhere;
            if (contact.Keyword != "")
            {
                sqlWhere = u => (u.Title.Contains(contact.Keyword) ||
                    u.Summary.Contains(contact.Keyword));
            }
            else
            {
                sqlWhere = u => (true);
            }
            var a = await _Service.fAQServices.GetListAsync(sqlWhere, sqlOrder, true, _Page, PageSize);
            var b = new ListFAQs() { Keyword = "", listFAQ = a };

            return View(b);
        }

        [Route("/[action]/{Page?}")]
        public async Task<IActionResult> FAQ(int? Page)
        {
            int _Page = (Page.HasValue ? Page.Value : 1);
            Func<FAQ, object> sqlOrder = s => s.Id;
            Expression<Func<FAQ, bool>> sqlWhere = u => (true);
            var a = await _Service.fAQServices.GetListAsync(sqlWhere, sqlOrder, true, _Page, PageSize);
            var b = new ListFAQs() { Keyword = "", listFAQ = a };

            return View(b);
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
            await Task.Delay(0);
            //PathOfPage path = new PathOfPage() { IsProduct = 3, Total = 0, TotalPage = 0, Page = 1, PathName = { _localizer.GetString("Liên hệ") } };
            //await ViewDataParam.SetViewData(this._logger, ViewData, this._Service, path);
            return View("LienHe", a);
        }

        [Route("/[action]")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(Contact contact)
        {
            ViewData.SetNotification(_localizer.GetString("Kiểm tra lại dữ liệu!"));
            ViewData["Re-Contact"] = true;
            PathOfPage path = new PathOfPage() { IsProduct = 3, Total = 0, TotalPage = 0, Page = 1, PathName = { _localizer.GetString("Liên hệ") } };
            if (ModelState.IsValid)
            {
                var r = await _Service.contactServices.AddAsync(contact);
                if (r != null)
                {
                    ViewData.SetNotification(_localizer.GetString("Gửi thành công"));
                    _logger.LogInformation($"Send contact is success: {JsonConvert.SerializeObject(contact)}");
                    ViewData["Re-Contact"] = false;
                    string msg = $"<ul><li><b>{_localizer["Tên đầy đủ"]}:</b> {contact.Fullname}</li><li><b>Email:</b> {contact.Email}</li><li><b>Mobile:</b> {contact.Mobile}</li><li>{contact.Description}</li></ul>";
                    await _emailSender.SendEmailAsync(contact.Email, _localizer.GetString("v.v Liên hệ Ngọc Tuấn"), msg);
                }
                else
                {
                    //await ViewDataParam.SetViewData(this._logger, ViewData, this._Service, path, _localizer.GetString("Gửi thất bại"));
                    _logger.LogInformation($"Send contact is fail: {JsonConvert.SerializeObject(contact)}");
                }

            }
            //else
            //{
            //    await ViewDataParam.SetViewData(this._logger, ViewData, this._Service, path);
            //}

            return View("LienHe", contact);
        }

        [Authorize(Roles = "Customer")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error404()
        {
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
