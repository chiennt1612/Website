using EntityFramework.Web.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Paygate.OnePay;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebNuoc.Helpers;
using WebNuoc.Services.Interfaces;

namespace WebNuoc.Controllers
{
    [Route("/[controller]")]
    public class InvoiceController : Controller
    {
        #region Properties
        private readonly IDistributedCache _cache;
        private readonly ILogger<InvoiceController> _logger;
        private readonly IStringLocalizer<InvoiceController> _localizer;
        private readonly IAllService _Service;
        private PaygateInfo paygateInfo;
        private IConfiguration configuration;
        private readonly IEmailSender _emailSender;
        private readonly IInvoiceServices _iInvoiceServices;
        //private const string ClaimCustomerCode = "oldid";
        #endregion

        #region private method
        private async Task<InvoiceResult> GetInvoice(string CustomerCode)
        {
            //string CustomerCode = User.Claims.Where(u => u.Type == Tools.ClaimCustomerCode).FirstOrDefault().Value;
            InvoiceResult a = null;
            if (!String.IsNullOrEmpty(CustomerCode))
            {
                //a = await _cache.GetAsync<InvoiceResult>($"CustomerCode_{CustomerCode}");
                if (a == null)
                {
                    var inv = new InvoiceInput()
                    {
                        CustomerCode = CustomerCode
                    };
                    a = await _iInvoiceServices.GetInvoice(inv);
                    //await _cache.SetAsync<InvoiceResult>($"CustomerCode_{CustomerCode}", a);
                }
            }
            if (a == null)
            {
                a = new InvoiceResult()
                {
                    Keyword = ""
                };
            }
            return a;
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
        #endregion

        public InvoiceController(IConfiguration configuration, IEmailSender _emailSender, IDistributedCache _cache,
            ILogger<InvoiceController> _logger, IStringLocalizer<InvoiceController> _localizer, IInvoiceServices _iInvoiceServices, IAllService _Service)
        {
            this._logger = _logger;
            this.configuration = configuration;
            this._localizer = _localizer;
            this._cache = _cache;
            this._emailSender = _emailSender;
            this._iInvoiceServices = _iInvoiceServices;
            this._Service = _Service;
            paygateInfo = this.configuration.GetSection(nameof(PaygateInfo)).Get<PaygateInfo>();
        }

        #region InvoiceHistoty
        [Route("/[controller]/[action]")]
        //[Authorize]
        [SecurityHeaders]
        [HttpGet]
        public async Task<IActionResult> InvoiceHist(int? Page)
        {
            var _Page = (Page.HasValue ? Page.Value : 1);
            string CustomerCode = "";
            if (User.Identity.IsAuthenticated) CustomerCode = User.Claims.Where(u => u.Type == Tools.ClaimCustomerCode).FirstOrDefault().Value;
            var a = await GetInvoice(CustomerCode, _Page);
            return View(a);
        }

        [Route("/[controller]/[action]")]
        [SecurityHeaders]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize]
        public async Task<IActionResult> InvoiceHist(WebNuoc.Models.Home.SearchInput search, int? Page)
        {
            var _Page = (Page.HasValue ? Page.Value : 1);
            string CustomerCode = search.Keyword;
            if (User.Identity.IsAuthenticated && String.IsNullOrEmpty(CustomerCode))
                CustomerCode = User.Claims.Where(u => u.Type == Tools.ClaimCustomerCode).FirstOrDefault().Value;
            var a = await GetInvoice(CustomerCode, _Page);
            return View(a);
        }
        #endregion

        #region SearchInvoice
        [SecurityHeaders]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize]
        public async Task<IActionResult> Index(WebNuoc.Models.Home.SearchInput search)
        {
            string CustomerCode = search.Keyword;
            if (User.Identity.IsAuthenticated && String.IsNullOrEmpty(CustomerCode))
                CustomerCode = User.Claims.Where(u => u.Type == Tools.ClaimCustomerCode).FirstOrDefault().Value;
            var a = await GetInvoice(CustomerCode);
            return View(a);
        }

        //[Authorize]
        [SecurityHeaders]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string CustomerCode = "";
            if (User.Identity.IsAuthenticated) CustomerCode = User.Claims.Where(u => u.Type == Tools.ClaimCustomerCode).FirstOrDefault().Value;
            var a = await GetInvoice(CustomerCode);
            return View(a);
        }
        #endregion

        #region PayGateway
        [Route("/[controller]/[action]")]
        public IActionResult Payoo()
        {
            return View();
        }

        //[Authorize]
        [SecurityHeaders]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/[controller]/[action]")]
        public async Task<IActionResult> Pay(PayInput inv)
        {
            if (ModelState.IsValid)
            {
                if (inv.IsAgree)
                {
                    var a = await GetInvoice(inv.CustomerCode);

                    if (a != null)
                    {
                        var invq = a.ItemsData.InvList.Where(u => u.InvCode == inv.InvoiceNo).FirstOrDefault();
                        if (invq != null)
                        {
                            if (invq.InvAmount == inv.InvoiceAmount)
                            {
                                var _contact = new Contact()
                                {
                                    Address = inv.CustomerCode,
                                    CompanyName = inv.CustomerCode,
                                    ContactDate = DateTime.Now,
                                    Description = $"2_{inv.CustomerCode}_{inv.InvoiceNo}_{inv.InvoiceAmount}",
                                    Email = inv.CustomerCode,
                                    Fullname = inv.CustomerCode,
                                    IsCompany = false,
                                    Mobile = inv.InvoiceNo,
                                    Price = inv.InvoiceAmount,
                                    ServiceId = null,
                                    StatusId = 0,
                                    UserId = -1,
                                    PaymentMethod = 3,
                                    IsAgree = true
                                };

                                var r = await _Service.contactServices.AddAsync(_contact);

                                if (r != null)
                                {
                                    _logger.LogInformation($"Send payment-invoice is success: {_contact.Id}");
                                    PaymentIn t = new PaymentIn()
                                    {
                                        vpc_Amount = _contact.Price.ToString(),
                                        vpc_Customer_Email = "",
                                        vpc_Customer_Id = User.Claims.GetClaimValue(ClaimTypes.NameIdentifier),
                                        vpc_Customer_Phone = "",
                                        vpc_MerchTxnRef = $"{(r.Id + Tools.StartIdOrder).ToString()}",//.{inv.InvoiceNo}
                                        vpc_OrderInfo = $"2_{inv.CustomerCode}_{inv.InvoiceNo}_{inv.InvoiceAmount}",
                                        vpc_SHIP_City = "Han",
                                        vpc_SHIP_Country = "VN",
                                        vpc_SHIP_Provice = "Han",
                                        vpc_SHIP_Street01 = ""
                                    };
                                    VPCRequest conn = new VPCRequest(paygateInfo, _logger);
                                    var url = conn.CreatePay(HttpContext, paygateInfo, t);
                                    _logger.LogInformation(url);
                                    return Redirect(url);
                                }

                            }
                        }
                    }
                }
                else
                {
                    //TempData["IsAgree"] = _localizer.GetString("Bạn chưa chọn đồng ý điều khoản thanh toán!");
                    ModelState.AddModelError("IsAgree", _localizer.GetString("Bạn chưa chọn đồng ý điều khoản thanh toán!"));
                    string CustomerCode = inv.CustomerCode;
                    //if (User.Identity.IsAuthenticated && String.IsNullOrEmpty(CustomerCode))
                    //    CustomerCode = User.Claims.Where(u => u.Type == Tools.ClaimCustomerCode).FirstOrDefault().Value;
                    var a = await GetInvoice(CustomerCode);
                    return View("Index", a);
                }
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion
        //#region Paygate
        //[Route("/[controller]/[action]")]
        //public async Task<IActionResult> IPN()
        //{
        //    VPCRequest conn = new VPCRequest(paygateInfo);
        //    conn.SetSecureSecret(paygateInfo._SECURE_SECRET);
        //    string hashvalidateResult = conn.Process3PartyResponse(HttpUtility.ParseQueryString(HttpContext.Request.QueryString.Value));

        //    var vpc_TxnResponseCode = HttpContext.GetQueryString("vpc_TxnResponseCode", false);

        //    //Mã giao dịch gửi sang cổng
        //    var vpc_MerchTxnRef = HttpContext.GetQueryString("vpc_MerchTxnRef", false);
        //    //Mã giao dịch gửi sang cổng (OrderID)
        //    var vpc_Merchant = HttpContext.GetQueryString("vpc_Merchant");
        //    //Mã đơn hàng gửi sang cổng (OrderInfo)
        //    var vpc_OrderInfo = HttpContext.GetQueryString("vpc_OrderInfo");
        //    var vpc_Amount = HttpContext.GetQueryString("vpc_Amount");
        //    var vpc_TransactionNo = HttpContext.GetQueryString("vpc_TransactionNo", false);
        //    var vpc_Message = HttpContext.GetQueryString("vpc_Message");

        //    var _vpc_Amount = int.Parse(vpc_Amount.Left(vpc_Amount.Length - 2));

        //    CheckPayInput inv = new CheckPayInput() { OnePayID = vpc_TransactionNo };
        //    PayResult payCheckResult = await _iInvoiceServices.CheckPayInvoice(inv);
        //    if (hashvalidateResult == "CORRECTED" && vpc_TxnResponseCode.Trim() == "0" && vpc_Merchant == paygateInfo._MerchantID)
        //    {
        //        ViewData.SetNotification(_localizer.GetString("/ <font color=\"green\">Thanh toán thành công</font>"));
        //        if (payCheckResult.PayStatus != "00")
        //        {
        //            string[] _orderInfo = vpc_OrderInfo.Split(new string[] { "_" }, StringSplitOptions.None);
        //            var InvoiceAmount = int.Parse(_orderInfo[2]);
        //            if (_orderInfo.Length == 3 && _vpc_Amount == InvoiceAmount && vpc_MerchTxnRef == _orderInfo[1])
        //            {
        //                PayInput inv1 = new PayInput()
        //                {
        //                    CustomerCode = _orderInfo[0],
        //                    InvoiceNo = _orderInfo[1],
        //                    InvoiceAmount = InvoiceAmount,
        //                    OnePayID = vpc_TransactionNo
        //                };
        //                PayResult payResult = await _iInvoiceServices.PayInvoice(inv1);

        //                _logger.LogInformation($"hashvalidateResult: {hashvalidateResult}; vpc_OrderInfo: {vpc_OrderInfo}; vpc_TransactionNo: {vpc_TransactionNo}; vpc_Amount: {vpc_Amount}; Result: {JsonConvert.SerializeObject(payResult)}");
        //            }
        //            else
        //            {
        //                _logger.LogInformation($"Error: hashvalidateResult: {hashvalidateResult}; vpc_OrderInfo: {vpc_OrderInfo}; vpc_TransactionNo: {vpc_TransactionNo}; vpc_Amount: {vpc_Amount}");
        //            }
        //        }
        //    }
        //    else if (hashvalidateResult == "INVALIDATED" && vpc_TxnResponseCode.Trim() == "0")
        //    {
        //        ViewData.SetNotification(_localizer.GetString("<font color=\"yellow\">Đang chờ thanh toán</font>"));
        //        _logger.LogInformation($"Error: hashvalidateResult: {hashvalidateResult}; vpc_OrderInfo: {vpc_OrderInfo}; vpc_TransactionNo: {vpc_TransactionNo}; vpc_Amount: {vpc_Amount}");
        //    }
        //    else
        //    {
        //        ViewData.SetNotification(_localizer.GetString("/ <font color=\"red\">Thanh toán thất bại</font>"));
        //        _logger.LogInformation($"Error: hashvalidateResult: {hashvalidateResult}; vpc_OrderInfo: {vpc_OrderInfo}; vpc_TransactionNo: {vpc_TransactionNo}; vpc_Amount: {vpc_Amount}");
        //    }

        //    return Content("responsecode=1&desc=confirm-success");
        //}

        //[Route("/[controller]/[action]")]
        //public async Task<IActionResult> PayResponse()
        //{
        //    VPCRequest conn = new VPCRequest(paygateInfo);
        //    conn.SetSecureSecret(paygateInfo._SECURE_SECRET);
        //    string hashvalidateResult = conn.Process3PartyResponse(HttpUtility.ParseQueryString(HttpContext.Request.QueryString.Value));

        //    var vpc_TxnResponseCode = HttpContext.GetQueryString("vpc_TxnResponseCode", false);

        //    //Mã giao dịch gửi sang cổng
        //    var vpc_MerchTxnRef = HttpContext.GetQueryString("vpc_MerchTxnRef", false);
        //    //Mã giao dịch gửi sang cổng (OrderID)
        //    var vpc_Merchant = HttpContext.GetQueryString("vpc_Merchant");
        //    //Mã đơn hàng gửi sang cổng (OrderInfo)
        //    var vpc_OrderInfo = HttpContext.GetQueryString("vpc_OrderInfo");
        //    var vpc_Amount = HttpContext.GetQueryString("vpc_Amount");
        //    var vpc_TransactionNo = HttpContext.GetQueryString("vpc_TransactionNo", false);
        //    var vpc_Message = HttpContext.GetQueryString("vpc_Message");

        //    var _vpc_Amount = int.Parse(vpc_Amount.Left(vpc_Amount.Length - 2));
        //    InvoiceResultModel resultModel = new InvoiceResultModel()
        //    {
        //        vpc_TxnResponseCode = vpc_TxnResponseCode,
        //        vpc_Message = vpc_Message,
        //        vpc_OrderInfo = vpc_OrderInfo,
        //        onePayID = vpc_TxnResponseCode,
        //        invoiceNo = vpc_MerchTxnRef,
        //        invoiceAmount = _vpc_Amount
        //    };

        //    CheckPayInput inv = new CheckPayInput() { OnePayID = vpc_TransactionNo };
        //    PayResult payCheckResult = await _iInvoiceServices.CheckPayInvoice(inv);
        //    if (hashvalidateResult == "CORRECTED" && vpc_TxnResponseCode.Trim() == "0" && vpc_Merchant == paygateInfo._MerchantID)
        //    {
        //        ViewData.SetNotification(_localizer.GetString("/ <font color=\"green\">Thanh toán thành công</font>"));
        //        if(payCheckResult.PayStatus != "00")
        //        {
        //            string [] _orderInfo = vpc_OrderInfo.Split(new string[] { "_" }, StringSplitOptions.None);
        //            var InvoiceAmount = int.Parse(_orderInfo[2]);
        //            if (_orderInfo.Length == 4 && _vpc_Amount == InvoiceAmount && vpc_MerchTxnRef == _orderInfo[2])
        //            {
        //                PayInput inv1 = new PayInput()
        //                {
        //                    CustomerCode = _orderInfo[2],
        //                    InvoiceNo = _orderInfo[3],
        //                    InvoiceAmount = InvoiceAmount,
        //                    OnePayID = vpc_TransactionNo
        //                };
        //                PayResult payResult = await _iInvoiceServices.PayInvoice(inv1);
        //                resultModel.customerCode = _orderInfo[1];
        //                resultModel.invoiceAmount = InvoiceAmount;
        //                resultModel.invoiceResult = await _cache.GetAsync<InvoiceResult>($"CustomerCode_{_orderInfo[1]}");
        //                resultModel.payResult = payResult;

        //                _logger.LogInformation($"hashvalidateResult: {hashvalidateResult}; vpc_OrderInfo: {vpc_OrderInfo}; vpc_TransactionNo: {vpc_TransactionNo}; vpc_Amount: {vpc_Amount}; Result: {JsonConvert.SerializeObject(payResult)}");
        //            }
        //            else
        //            {
        //                _logger.LogInformation($"Error: hashvalidateResult: {hashvalidateResult}; vpc_OrderInfo: {vpc_OrderInfo}; vpc_TransactionNo: {vpc_TransactionNo}; vpc_Amount: {vpc_Amount}");
        //            }
        //        }
        //    }
        //    else if (hashvalidateResult == "INVALIDATED" && vpc_TxnResponseCode.Trim() == "0")
        //    {
        //        ViewData.SetNotification(_localizer.GetString("<font color=\"yellow\">Đang chờ thanh toán</font>"));
        //        _logger.LogInformation($"Error: hashvalidateResult: {hashvalidateResult}; vpc_OrderInfo: {vpc_OrderInfo}; vpc_TransactionNo: {vpc_TransactionNo}; vpc_Amount: {vpc_Amount}");
        //    }
        //    else
        //    {
        //        ViewData.SetNotification(_localizer.GetString("/ <font color=\"red\">Thanh toán thất bại</font>"));
        //        _logger.LogInformation($"Error: hashvalidateResult: {hashvalidateResult}; vpc_OrderInfo: {vpc_OrderInfo}; vpc_TransactionNo: {vpc_TransactionNo}; vpc_Amount: {vpc_Amount}");
        //    }

        //    return View("Complete", resultModel);
        //}
        //#endregion
    }
}
