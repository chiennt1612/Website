using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Paygate.OnePay;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebNuoc.Helpers;
using WebNuoc.Services.Interfaces;

namespace WebNuoc.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IDistributedCache _cache;
        private readonly IInvoiceServices _iInvoiceServices;
        private readonly ILogger<AccountController> _logger;
        private readonly IAllService _Service;
        private readonly IStringLocalizer<AccountController> _localizer;

        public AccountController(IDistributedCache _cache, ILogger<AccountController> _logger,
            IAllService _Service, IStringLocalizer<AccountController> _localizer, IInvoiceServices _iInvoiceServices)
        {
            this._logger = _logger;
            this._Service = _Service;
            this._localizer = _localizer;
            this._cache = _cache;
            this._iInvoiceServices = _iInvoiceServices;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> AccessDenied()
        {
            await Task.Delay(0);
            //PathOfPage path = new PathOfPage() { IsProduct = 3, Total = 0, TotalPage = 0, Page = 1, PathName = { _localizer["Không có quyền truy cập"] } };
            //await ViewDataParam.SetViewData(this._logger, ViewData, this._Service, path);
            return View();
        }

        [SecurityHeaders]
        public async Task<IActionResult> Profile(int? Page)
        {
            var _Page = (Page.HasValue ? Page.Value : 1);
            string CustomerCode = User.Claims.Where(u => u.Type == Tools.ClaimCustomerCode).FirstOrDefault().Value;
            var a = await GetInvoice(CustomerCode, _Page);
            return View(a);
        }

        [AllowAnonymous]
        public IActionResult InvoiceView(string reservationCode, string supplierTaxCode, string invoiceNo, string invoiceType)
        {
            ViewData["invoiceType"] = invoiceType;
            ViewData["invoiceNo"] = invoiceNo;
            ViewData["supplierTaxCode"] = supplierTaxCode;
            ViewData["reservationCode"] = reservationCode;
            return View();
        }

        [SecurityHeaders]
        public IActionResult Logout()
        {
            //SignOut(CookieAuthenticationDefaults.AuthenticationScheme);
            return SignOut(CookieAuthenticationDefaults.AuthenticationScheme, "oidc");
        }

        [SecurityHeaders]
        public async Task<IActionResult> SetCustomerCode()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect($"https://id.bacngoctuan.com/Manage/Index?returnUrl={((HttpContext.MerchantUrl() + "/Account/Login").UrlEncode())}");
        }

        [SecurityHeaders]
        [Route("/[controller]/[action]")]
        public IActionResult Login()
        {
            return RedirectToAction(nameof(Profile));
            //if (String.IsNullOrEmpty(UrlBack))
            //{
            //    return Redirect("/Home/Index");
            //}
            //else
            //{
            //    return Redirect(UrlBack.UrlDecode());
            //}            
        }

        private async Task<InvoiceAllResult> GetInvoice(string CustomerCode, int Page)
        {
            InvoiceAllResult a = null;
            if (!String.IsNullOrEmpty(CustomerCode))
            {
                a = await _cache.GetAsync<InvoiceAllResult>($"InvoiceHistory_{CustomerCode}");
                if (a == null)
                {
                    var inv = new InvoiceAllInput()
                    {
                        CustomerCode = CustomerCode,
                        Page = Page
                    };
                    a = await _iInvoiceServices.GetInvoiceAll(inv);
                    await _cache.SetAsync<InvoiceAllResult>($"InvoiceHistory_{CustomerCode}", a);
                }
            }
            if (a == null)
            {
                a = new InvoiceAllResult();
            }
            return a;
        }
    }
}
