using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Paygate.OnePay;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Threading.Tasks;
using WebNuoc.Helpers;
using WebNuoc.Models;
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
        private readonly IConfiguration _configuration;
        private JwtModel _jwtModel { get; set; }

        public AccountController(IDistributedCache _cache, ILogger<AccountController> _logger,
            IAllService _Service, IStringLocalizer<AccountController> _localizer, IInvoiceServices _iInvoiceServices, IConfiguration configuration)
        {
            this._logger = _logger;
            this._Service = _Service;
            this._localizer = _localizer;
            this._cache = _cache;
            this._iInvoiceServices = _iInvoiceServices;
            _configuration = configuration;
            _jwtModel = this._configuration.GetSection(nameof(JwtModel)).Get<JwtModel>();
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
            InvoiceFindModel invM = new InvoiceFindModel()
            {
                FromDate = null,
                ToDate = DateTime.Now,
                PaymentStatus = null
            };
            //string CustomerCode = User.Claims.Where(u => u.Type == Tools.ClaimCustomerCode).FirstOrDefault().Value;
            //var a = await GetInvoice(CustomerCode, _Page);
            ResponseOK a = await webRequest<ResponseOK, InvoiceFindModel>($"https://{_jwtModel.Issuer}/api/v1.0/Invoice/FindInvoice/{_Page}", HttpContext.Session.GetString("JWToken"), invM);
            ItemsDataAll c1 = new ItemsDataAll();
            c1.InvList = new List<InvListAll>();
            foreach(var item in a.data.itemsList)
            {
                foreach (var item1 in item.itemsData)
                {
                    c1.Address = item1.address;
                    c1.CustomerCode = item1.customerCode;
                    c1.CustomerName = item1.customerName;
                    c1.Email = "";
                    c1.Mobile = "";
                    c1.WaterIndexCode = item1.waterIndexCode;
                    
                    InvListAll c2 = new InvListAll()
                    {
                        InvAmount = item1.invAmount,
                        InvAmountWithoutTax = item1.invAmountWithoutTax,
                        InvCode = item1.invCode,
                        InvDate = item1.invDate,
                        InvNumber = item1.invNumber,
                        InvSerial = item1.invSerial,
                        InvRemarks = item1.invRemarks,
                        MaSoBiMat = item1.maSoBiMat,
                        PaymentStatus = item1.paymentStatus,
                        TaxPer = item1.taxPer,
                        Link = item1.link
                    };
                    c1.InvList.Add(c2);
                }
            }
            InvoiceAllResult b = new InvoiceAllResult()
            {
                DataStatus = "00",
                IsAgree = true,
                ItemsData = c1,
                Rowcount = a.data.itemsCount,
                Message = "OK",
                ResponseStatus = 200,
                Keyword = ""
            };
            return View(b);
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
            //return SignOut(CookieAuthenticationDefaults.AuthenticationScheme, "oidc");
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [SecurityHeaders]
        public async Task<IActionResult> SetCustomerCode()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect($"https://id.bacngoctuan.com/Manage/Index?returnUrl={((HttpContext.MerchantUrl() + "/Account/Login").UrlEncode())}");
        }


        #region login/register
        [AllowAnonymous]
        [MyAuthorize]
        [Route("/[controller]/[action]")]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [SecurityHeaders]
        [MyAuthorize]
        [Route("/[controller]/[action]")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                ResponseOK a = await webRequest<ResponseOK, LoginModel>($"https://{_jwtModel.Issuer}/api/v1.0/Authenticate/Login", "", model);
                if (a.Code != 200)
                {
                    return View();
                }
                else
                {
                    return View("OTP");
                }
            }
            return View();
        }

        [AllowAnonymous]
        [MyAuthorize]
        [SecurityHeaders]
        [Route("/[controller]/[action]")]
        [HttpPost]
        public async Task<IActionResult> Verify(OTPModel model)
        {
            if (ModelState.IsValid)
            {
                ResponseOK a = await webRequest<ResponseOK, OTPModel>($"https://{_jwtModel.Issuer}/api/v1.0/Authenticate/VerifyPhoneNumber", "", model);
                if (a.Code != 200)
                {
                    ModelState.AddModelError("OTP", _localizer.GetString("OTP không đúng"));
                    return View("OTP");
                }
                else
                {
                    // store token in a session
                    string token = a.data.token;
                    HttpContext.Session.SetString("JWToken", token);
                    //return View("Success");
                    return RedirectToAction(nameof(Profile));
                }
            }
            return View("OTP");
        }
        #endregion
        #region Web request
        private async Task<T> webRequest<T, T1>(string apiUrl, string apiToken, T1 pzData)
        {
            string result = string.Empty;
            HttpWebRequest httpWebRequest;
            httpWebRequest = (HttpWebRequest)WebRequest.Create(apiUrl);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Accept = "application/json";
            httpWebRequest.Method = "POST";
            if(!String.IsNullOrEmpty(apiToken)) httpWebRequest.Headers.Add("Authorization", "Bearer " + apiToken);

            if (pzData != null)
            {
                using (var streamWriter = new StreamWriter(await httpWebRequest.GetRequestStreamAsync()))
                {
                    string json = JsonConvert.SerializeObject(pzData);
                    await streamWriter.WriteAsync(json);
                    await streamWriter.FlushAsync();
                    streamWriter.Close();
                }
            }
            InitiateSSLTrust();//bypass SSL
            var httpResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = await streamReader.ReadToEndAsync();
            }
            return JsonConvert.DeserializeObject<T>(result);
        }
        private async Task<T> webRequest<T>(string apiUrl, string apiToken)
        {
            string result = string.Empty;
            HttpWebRequest httpWebRequest;
            httpWebRequest = (HttpWebRequest)WebRequest.Create(apiUrl);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Accept = "application/json";
            httpWebRequest.Method = "GET";
            if (!String.IsNullOrEmpty(apiToken)) httpWebRequest.Headers.Add("Authorization", "Bearer " + apiToken);

            InitiateSSLTrust();//bypass SSL
            var httpResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = await streamReader.ReadToEndAsync();
            }
            return JsonConvert.DeserializeObject<T>(result);
        }
        private void InitiateSSLTrust()
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback =
                   new RemoteCertificateValidationCallback(
                        delegate
                        { return true; }
                    );
            }
            catch
            {

            }
        }
        #endregion        
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
