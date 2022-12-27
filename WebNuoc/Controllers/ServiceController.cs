using EntityFramework.Web.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Paygate.OnePay;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using WebNuoc.Models;
using WebNuoc.Repository.Interfaces;
using WebNuoc.Services.Interfaces;

namespace WebNuoc.Controllers
{
    //[SecurityHeaders]
    public class ServiceController : Controller
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger<ServiceController> _logger;
        private readonly IAllService _Service;
        private readonly IStringLocalizer<ServiceController> _localizer;
        private readonly IEmailSender _emailSender;
        //private readonly IVPCRequest _vPCRequest;
        private PaygateInfo paygateInfo;
        private IConfiguration configuration;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IInvoiceServices _iInvoiceServices;

        public ServiceController(IConfiguration configuration, IUnitOfWork _unitOfWork, IEmailSender _emailSender,
            IDistributedCache _cache, ILogger<ServiceController> _logger, IAllService _Service,
            IStringLocalizer<ServiceController> _localizer, IInvoiceServices _iInvoiceServices)
        {
            this._logger = _logger;
            this._Service = _Service;
            this._localizer = _localizer;
            this._cache = _cache;
            this._emailSender = _emailSender;
            //this.paygateInfo = paygateInfo;
            this.configuration = configuration;
            paygateInfo = this.configuration.GetSection(nameof(PaygateInfo)).Get<PaygateInfo>();
            this._unitOfWork = _unitOfWork;

            this._iInvoiceServices = _iInvoiceServices;
        }

        public async Task<IActionResult> Index()
        {
            var b = (await _Service.serviceServices.GetAllAsync());

            return View(b);
        }

        [Route("/[controller]/[action]/{ServiceId?}")]
        [HttpGet]
        public async Task<IActionResult> Register(long? ServiceId)
        {
            var _Id = (ServiceId.HasValue ? ServiceId.Value : 0);
            ViewData["ServiceList"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(await _Service.serviceServices.GetAllAsync(), "Id", "Title");
            ViewData["ServiceDetail"] = await _Service.serviceServices.GetByIdAsync(_Id);
            var b = new ServiceInput();
            if (User.Identity.IsAuthenticated)
            {
                b.Fullname = User.Claims.GetClaimValue("name");
                b.Email = User.Claims.GetClaimValue("email");
                b.Mobile = User.Claims.GetClaimValue("phone_number");
                b.Address = User.Claims.GetClaimValue("address");
            }

            return View("LienHe", b);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/[controller]/[action]/{ServiceId?}")]
        public async Task<IActionResult> Register(long? ServiceId, ServiceInput contact)
        {
            var _id = (ServiceId.HasValue ? ServiceId.Value : 0);
            //ServiceInput contact = new ServiceInput();
            ViewData["ServiceList"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(await _Service.serviceServices.GetAllAsync(), "Id", "Title");
            var _service = await _Service.serviceServices.GetByIdAsync(_id);
            ViewData["ServiceDetail"] = _service;
            if (ModelState.IsValid)
            {
                if (contact.IsAgree)
                {
                    var _contact = new Contact()
                    {
                        Address = contact.Address,
                        CompanyName = contact.CompanyName,
                        ContactDate = DateTime.Now,
                        Description = contact.Description,
                        Email = contact.Email,
                        Fullname = contact.Fullname,
                        IsCompany = contact.IsCompany,
                        Mobile = contact.Mobile,
                        Price = (contact.IsCompany ? _service.Price1 : _service.Price),
                        ServiceId = contact.ServiceId,
                        StatusId = 0,
                        UserId = -1,
                        PaymentMethod = contact.PaymentMethod,
                        IsAgree = contact.IsAgree
                    };
                    if (User.Identity.IsAuthenticated)
                    {
                        _contact.Fullname = User.Claims.GetClaimValue("name");
                        _contact.Email = User.Claims.GetClaimValue("email");
                        _contact.Mobile = User.Claims.GetClaimValue("phone_number");
                        _contact.Address = User.Claims.GetClaimValue("address");
                        _contact.UserId = long.Parse(User.Claims.GetClaimValue(ClaimTypes.NameIdentifier));
                    }
                    if (!string.IsNullOrEmpty(contact.Address)) _contact.Address = contact.Address;
                    if (!string.IsNullOrEmpty(contact.Description)) _contact.Description = contact.Description;
                    if (!string.IsNullOrEmpty(contact.Email)) _contact.Email = contact.Email;
                    if (!string.IsNullOrEmpty(contact.Fullname)) _contact.Fullname = contact.Fullname;
                    if (!string.IsNullOrEmpty(contact.Mobile)) _contact.Mobile = contact.Mobile;

                    var r = await _Service.contactServices.AddAsync(_contact);
                    if (r != null)
                    {
                        _logger.LogInformation($"Send contact is success: {_contact.Id}");
                        ViewData["SaveContact"] = _localizer.GetString("Gửi thành công");
                        string msg = $"<ul><li><b>{_localizer["Tên đầy đủ"]}:</b> {contact.Fullname}</li><li><b>Email:</b> {contact.Email}</li><li><b>Mobile:</b> {contact.Mobile}</li><li><b>Trạng thái:</b> Chưa thanh toán</li><li>{contact.Description}</li></ul>";

                        switch (contact.PaymentMethod)
                        {
                            case 3:
                                PaymentIn t = new PaymentIn()
                                {
                                    vpc_Amount = (_service.Price).ToString(),
                                    vpc_Customer_Email = contact.Email,
                                    vpc_Customer_Id = (_contact.UserId.HasValue ? _contact.UserId.Value : 0).ToString(),
                                    vpc_Customer_Phone = "",
                                    vpc_MerchTxnRef = (r.Id + Tools.StartIdOrder).ToString(),
                                    vpc_OrderInfo = $"1_{r.Id}_{_service.Price}",
                                    vpc_SHIP_City = "Han",
                                    vpc_SHIP_Country = "VN",
                                    vpc_SHIP_Provice = "Han",
                                    vpc_SHIP_Street01 = _contact.Address
                                };
                                VPCRequest conn = new VPCRequest(paygateInfo, _logger);
                                var url = conn.CreatePay(HttpContext, paygateInfo, t);
                                _logger.LogInformation(url);
                                return Redirect(url);
                            default:
                                await _emailSender.SendEmailAsync(contact.Email, _localizer.GetString("v.v Liên hệ Ngọc Tuấn"), msg);
                                return RedirectToAction(nameof(Complete), new { OrderId = r.Id });
                        }
                    }
                    else
                    {
                        _logger.LogInformation($"Send contact is fail: {_contact.Id}");
                        ViewData["SaveContact"] = _localizer.GetString("Gửi thất bại");
                    }
                }
                else
                {
                    ModelState.AddModelError("IsAgree", _localizer.GetString("Bạn chưa chọn đồng ý điều khoản thanh toán!"));
                }
            }
            //else
            //{
            //    //await ViewDataParam.SetViewData(this._logger, ViewData, this._Service, path);
            //}

            return View("LienHe", contact);
        }

        [Route("/[controller]/[action]/{Id?}")]
        public async Task<IActionResult> Details(long? Id)
        {
            long _Id = (Id.HasValue ? Id.Value : 0);
            var a = await _Service.serviceServices.GetByIdAsync(_Id);
            var b = new ServiceDetail()
            {
                _Detail = a,
                _Related = (await _Service.serviceServices.GetAllAsync()).Where(u => u.Id != _Id)
            };
            return View(b);
        }

        #region Paygate
        [Route("/[controller]/[action]")]
        public async Task<IActionResult> IPN()
        {
            var vpc_MerchTxnRef = HttpContext.GetQueryString("vpc_MerchTxnRef", false);
            var vpc_TxnResponseCode = HttpContext.GetQueryString("vpc_TxnResponseCode", false);
            var vpc_TransactionNo = HttpContext.GetQueryString("vpc_TransactionNo", false);
            var vpc_Merchant = HttpContext.GetQueryString("vpc_Merchant");
            var vpc_OrderInfo = HttpContext.GetQueryString("vpc_OrderInfo");
            var vpc_Amount = HttpContext.GetQueryString("vpc_Amount");
            try
            {
                VPCRequest conn = new VPCRequest(paygateInfo, _logger);
                conn.SetSecureSecret(paygateInfo._SECURE_SECRET);
                string hashvalidateResult = conn.Process3PartyResponse(HttpUtility.ParseQueryString(HttpContext.Request.QueryString.Value));

                var _vpc_Amount = int.Parse(vpc_Amount.Left(vpc_Amount.Length - 2));
                string[] _orderInfo = vpc_OrderInfo.Split(new string[] { "_" }, StringSplitOptions.None);
                var _orderId = long.Parse(vpc_MerchTxnRef) - Tools.StartIdOrder;
                var _order = await _Service.contactServices.GetByIdAsync(_orderId);

                if (hashvalidateResult == "CORRECTED" && vpc_TxnResponseCode.Trim() == "0" && vpc_Merchant == paygateInfo._MerchantID)
                {
                    if (_orderInfo[0] == "1") // Order đăng ký dịch vụ
                    {
                        if (_order.StatusId != 4 && _order.StatusId != 6)
                        {
                            _order.StatusId = 4;
                            _order.CookieID = vpc_TransactionNo;
                            _order.OrderStatus = await _unitOfWork.orderStatusRepository.GetByIdAsync(4);
                            var _orderU = await _Service.contactServices.Update(_order);
                            string msg = $"<ul><li><b>{_localizer["Tên đầy đủ"]}:</b> {_order.Fullname}</li><li><b>Email:</b> {_order.Email}</li><li><b>Mobile:</b> {_order.Mobile}</li><li><b>Trạng thái:</b> Đã thanh toán</li></li><li>{_order.Description}</li></ul>";
                            await _emailSender.SendEmailAsync(_order.Email, _localizer.GetString("v.v Liên hệ Ngọc Tuấn"), msg);
                            _logger.LogInformation($"Thanh toan thanh cong {_orderId} {vpc_TxnResponseCode} {vpc_TransactionNo}");
                        }
                        else
                        {
                            _logger.LogInformation($"Don hang da xu ly {_orderId} {vpc_TxnResponseCode} {vpc_TransactionNo}");
                        }
                    }
                    else if (_orderInfo[0] == "2") // Order thanh toán hóa đơn nước
                    {
                        //vpc_OrderInfo = $"2_{inv.CustomerCode}_{inv.InvoiceNo}_{inv.InvoiceAmount}",
                        var InvoiceAmount = int.Parse(_orderInfo[2]);
                        if (_orderInfo.Length == 4 && _vpc_Amount == InvoiceAmount && _order.Mobile == _orderInfo[2])
                        {
                            if (_order.StatusId != 4 && _order.StatusId != 6)
                            {
                                _order.StatusId = 4;
                                _order.CookieID = vpc_TransactionNo;
                                _order.OrderStatus = await _unitOfWork.orderStatusRepository.GetByIdAsync(4);
                                var _orderU = await _Service.contactServices.Update(_order);
                                _logger.LogInformation($"Thanh toan thanh cong {_orderId} {vpc_TxnResponseCode} {vpc_TransactionNo}");
                            }
                            else
                            {
                                _logger.LogInformation($"Don hang da xu ly {_orderId} {vpc_TxnResponseCode} {vpc_TransactionNo}");
                            }

                            PayInput inv1 = new PayInput()
                            {
                                CustomerCode = _orderInfo[1],
                                InvoiceNo = _orderInfo[2],
                                InvoiceAmount = InvoiceAmount,
                                OnePayID = vpc_TransactionNo
                            };
                            PayResult payResult = await _iInvoiceServices.PayInvoice(inv1);

                            _logger.LogInformation($"hashvalidateResult: {hashvalidateResult}; vpc_OrderInfo: {vpc_OrderInfo}; vpc_TransactionNo: {vpc_TransactionNo}; vpc_Amount: {vpc_Amount}; Result: {payResult.PayStatus}");
                        }
                        else
                        {
                            _logger.LogInformation($"Error: hashvalidateResult: {hashvalidateResult}; vpc_OrderInfo: {vpc_OrderInfo}; vpc_TransactionNo: {vpc_TransactionNo}; vpc_Amount: {vpc_Amount}");
                        }
                    }

                }
                else if (hashvalidateResult == "INVALIDATED" && vpc_TxnResponseCode.Trim() == "0")
                {
                    _logger.LogInformation($"Don hang dang xu ly {_orderId} {vpc_TxnResponseCode} {vpc_TransactionNo}");
                }
                else
                {
                    if (_orderInfo[0] == "1") // Order đăng ký dịch vụ
                    {
                        if (_order.StatusId != 4 && _order.StatusId != 6)
                        {
                            _order.StatusId = 6;
                            _order.OrderStatus = await _unitOfWork.orderStatusRepository.GetByIdAsync(6);
                            var _orderU = await _Service.contactServices.Update(_order);
                            _logger.LogInformation($"Huy thanh cong {_orderId} {vpc_TxnResponseCode} {vpc_TransactionNo}");
                            string msg = $"<ul><li><b>{_localizer["Tên đầy đủ"]}:</b> {_order.Fullname}</li><li><b>Email:</b> {_order.Email}</li><li><b>Mobile:</b> {_order.Mobile}</li><li><b>Trạng thái:</b>Đã hủy thanh toán</li></li><li>{_order.Description}</li></ul>";
                            await _emailSender.SendEmailAsync(_order.Email, _localizer.GetString("v.v Liên hệ Ngọc Tuấn"), msg);
                        }
                        else
                        {
                            _logger.LogInformation($"Don hang da xu ly {_orderId} {vpc_TxnResponseCode} {vpc_TransactionNo}");
                        }
                    }
                    else if (_orderInfo[0] == "2") // Hoa don nuoc
                    {
                        if (_order.StatusId != 4 && _order.StatusId != 6)
                        {
                            _order.StatusId = 6;
                            _order.OrderStatus = await _unitOfWork.orderStatusRepository.GetByIdAsync(6);
                            var _orderU = await _Service.contactServices.Update(_order);
                            _logger.LogInformation($"Huy thanh cong {_orderId} {vpc_TxnResponseCode} {vpc_TransactionNo}");
                            string msg = $"<ul><li><b>{_localizer["Tên đầy đủ"]}:</b> {_order.Fullname}</li><li><b>Email:</b> {_order.Email}</li><li><b>Mobile:</b> {_order.Mobile}</li><li><b>Trạng thái:</b>Đã hủy thanh toán</li></li><li>{_order.Description}</li></ul>";
                        }
                        else
                        {
                            _logger.LogInformation($"Don hang da xu ly {_orderId} {vpc_TxnResponseCode} {vpc_TransactionNo}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Co loi {vpc_MerchTxnRef} {vpc_TxnResponseCode} {vpc_TransactionNo} {ex.Message}");
            }

            return Content("responsecode=1&desc=confirm-success");
        }

        [Route("/[controller]/[action]")]
        public async Task<IActionResult> PayResponse()
        {
            VPCRequest conn = new VPCRequest(paygateInfo, _logger);
            conn.SetSecureSecret(paygateInfo._SECURE_SECRET);
            string hashvalidateResult = conn.Process3PartyResponse(HttpUtility.ParseQueryString(HttpContext.Request.QueryString.Value));

            var vpc_TxnResponseCode = HttpContext.GetQueryString("vpc_TxnResponseCode", false);
            //var vpc_Command = HttpContext.GetQueryString("vpc_Command");
            //var vpc_Locale = HttpContext.GetQueryString("vpc_Locale");
            //var vpc_CurrencyCode = HttpContext.GetQueryString("vpc_CurrencyCode");
            var vpc_MerchTxnRef = HttpContext.GetQueryString("vpc_MerchTxnRef", false);
            var vpc_Merchant = HttpContext.GetQueryString("vpc_Merchant");
            var vpc_OrderInfo = HttpContext.GetQueryString("vpc_OrderInfo");
            var vpc_Amount = HttpContext.GetQueryString("vpc_Amount");
            var vpc_TransactionNo = HttpContext.GetQueryString("vpc_TransactionNo", false);
            var vpc_Message = HttpContext.GetQueryString("vpc_Message");
            //var vpc_Card = HttpContext.GetQueryString("vpc_Card");
            //var vpc_PayChannel = HttpContext.GetQueryString("vpc_PayChannel");
            //var vpc_CardUid = HttpContext.GetQueryString("vpc_CardUid");
            //var vpc_CardHolderName = HttpContext.GetQueryString("vpc_CardHolderName");
            //var vpc_ItaBank = HttpContext.GetQueryString("vpc_ItaBank");
            //var vpc_ItaFeeAmount = HttpContext.GetQueryString("vpc_ItaFeeAmount");
            //var vpc_ItaTime = HttpContext.GetQueryString("vpc_ItaTime");
            //var vpc_OrderAmount = HttpContext.GetQueryString("vpc_OrderAmount");
            //var vpc_ItaMobile = HttpContext.GetQueryString("vpc_ItaMobile");
            //var vpc_ItaEmail = HttpContext.GetQueryString("vpc_ItaEmail");
            //var vpc_SecureHash = HttpContext.GetQueryString("vpc_SecureHash");

            var _vpc_Amount = int.Parse(vpc_Amount.Left(vpc_Amount.Length - 2));
            string[] _orderInfo = vpc_OrderInfo.Split(new string[] { "_" }, StringSplitOptions.None);
            var _orderId = long.Parse(vpc_MerchTxnRef) - Tools.StartIdOrder;
            var _order = await _Service.contactServices.GetByIdAsync(_orderId);

            InvoiceResultModel resultModel = new InvoiceResultModel()
            {
                vpc_TxnResponseCode = vpc_TxnResponseCode,
                vpc_Message = vpc_Message,
                vpc_OrderInfo = vpc_OrderInfo,
                onePayID = vpc_TransactionNo,
                invoiceNo = vpc_MerchTxnRef,
                invoiceAmount = _vpc_Amount,
                order = _order,
                IsInvoice = (_orderInfo[0] == "2")
            };

            if (hashvalidateResult == "CORRECTED" && vpc_TxnResponseCode.Trim() == "0" && vpc_Merchant == paygateInfo._MerchantID)
            {
                ViewData.SetNotification(_localizer.GetString("/ <font color=\"green\">Thanh toán thành công</font>"));
                if (_orderInfo[0] == "1") // Order đăng ký dịch vụ
                {
                    _order.StatusId = 4;
                    _order.CookieID = vpc_TransactionNo;
                    _order.OrderStatus = await _unitOfWork.orderStatusRepository.GetByIdAsync(4);
                    var _orderU = await _Service.contactServices.Update(_order);
                    string msg = $"<ul><li><b>{_localizer["Tên đầy đủ"]}:</b> {_order.Fullname}</li><li><b>Email:</b> {_order.Email}</li><li><b>Mobile:</b> {_order.Mobile}</li><li><b>Trạng thái:</b> Đã thanh toán</li></li><li>{_order.Description}</li></ul>";
                    await _emailSender.SendEmailAsync(_order.Email, _localizer.GetString("v.v Liên hệ Ngọc Tuấn"), msg);
                }
                else if (_orderInfo[0] == "2") // Order thanh toán hóa đơn nước
                {
                    //vpc_OrderInfo = $"2_{inv.CustomerCode}_{inv.InvoiceNo}_{inv.InvoiceAmount}",
                    var InvoiceAmount = int.Parse(_orderInfo[3]);
                    if (_orderInfo.Length == 4 && _vpc_Amount == InvoiceAmount && _order.Description == vpc_OrderInfo)
                    {
                        if (_order.StatusId != 4 && _order.StatusId != 6)
                        {
                            var orderSave = await _unitOfWork.invoiceSaveRepository.GetByIdAsync(_order.Id);
                            if (orderSave != null)
                            {
                                orderSave.PaymentStatus = 1;
                                _unitOfWork.invoiceSaveRepository.Update(orderSave);
                            }
                            _order.StatusId = 4;
                            _order.CookieID = vpc_TransactionNo;
                            _order.OrderStatus = await _unitOfWork.orderStatusRepository.GetByIdAsync(4);
                            var _orderU = await _Service.contactServices.Update(_order);
                            _logger.LogInformation($"Thanh toan thanh cong {_orderId} {vpc_TxnResponseCode} {vpc_TransactionNo}");
                        }
                        else
                        {
                            _logger.LogInformation($"Don hang da xu ly {_orderId} {vpc_TxnResponseCode} {vpc_TransactionNo}");
                        }
                        PayInput inv1 = new PayInput()
                        {
                            CustomerCode = _orderInfo[1],
                            InvoiceNo = _orderInfo[2],
                            InvoiceAmount = InvoiceAmount,
                            OnePayID = vpc_TransactionNo
                        };
                        PayResult payResult = await _iInvoiceServices.PayInvoice(inv1);
                        resultModel.customerCode = _orderInfo[1];
                        resultModel.invoiceAmount = InvoiceAmount;
                        resultModel.invoiceResult = await _cache.GetAsync<InvoiceResult>($"CustomerCode_{_orderInfo[1]}");
                        resultModel.payResult = payResult;
                        resultModel.IsInvoice = true;
                        resultModel.invoiceNo = _orderInfo[2];

                        _logger.LogInformation($"hashvalidateResult: {hashvalidateResult}; vpc_OrderInfo: {vpc_OrderInfo}; vpc_TransactionNo: {vpc_TransactionNo}; vpc_Amount: {vpc_Amount}; Result: {payResult.PayStatus}");
                    }
                    else
                    {
                        _logger.LogInformation($"Error: hashvalidateResult: {hashvalidateResult}; vpc_OrderInfo: {vpc_OrderInfo}; vpc_TransactionNo: {vpc_TransactionNo}; vpc_Amount: {vpc_Amount}");
                    }
                }

            }
            else if (hashvalidateResult == "INVALIDATED" && vpc_TxnResponseCode.Trim() == "0")
            {
                ViewData.SetNotification(_localizer.GetString("<font color=\"yellow\">Đang chờ thanh toán</font>"));
            }
            else
            {
                ViewData.SetNotification(_localizer.GetString("/ <font color=\"red\">Thanh toán thất bại</font>"));
                if (_orderInfo[0] == "1") // Order đăng ký dịch vụ
                {
                    _order.StatusId = 6;
                    _order.OrderStatus = await _unitOfWork.orderStatusRepository.GetByIdAsync(6);
                    var _orderU = await _Service.contactServices.Update(_order);
                    string msg = $"<ul><li><b>{_localizer["Tên đầy đủ"]}:</b> {_order.Fullname}</li><li><b>Email:</b> {_order.Email}</li><li><b>Mobile:</b> {_order.Mobile}</li><li><b>Trạng thái:</b> Đã hủy thanh toán</li></li><li>{_order.Description}</li></ul>";
                    await _emailSender.SendEmailAsync(_order.Email, _localizer.GetString("v.v Liên hệ Ngọc Tuấn"), msg);
                }
                else if (_orderInfo[0] == "2") // Hóa đơn nước
                {
                    await _unitOfWork.invoiceSaveRepository.DeleteAsync(_order.Id);
                    var InvoiceAmount = int.Parse(_orderInfo[3]);
                    _order.StatusId = 6;
                    _order.OrderStatus = await _unitOfWork.orderStatusRepository.GetByIdAsync(6);
                    var _orderU = await _Service.contactServices.Update(_order);
                    _logger.LogInformation($"<ul><li><b>{_localizer["Tên đầy đủ"]}:</b> {_order.Fullname}</li><li><b>Email:</b> {_order.Email}</li><li><b>Mobile:</b> {_order.Mobile}</li><li><b>Trạng thái:</b> Đã hủy thanh toán</li></li><li>{_order.Description}</li></ul>");
                    resultModel.customerCode = _orderInfo[1];
                    resultModel.invoiceAmount = InvoiceAmount;
                    resultModel.invoiceResult = await _cache.GetAsync<InvoiceResult>($"CustomerCode_{_orderInfo[1]}");
                    //resultModel.payResult = payResult;
                    resultModel.IsInvoice = true;
                    resultModel.invoiceNo = _orderInfo[2];
                }
            }
            if (_orderInfo[0] == "1") // Order đăng ký dịch vụ
            {
                ViewData["ServiceList"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(await _Service.serviceServices.GetAllAsync(), "Id", "Title");
                ViewData["ServiceDetail"] = await _Service.serviceServices.GetByIdAsync(_order.ServiceId.Value);
            }
            return View("Complete", resultModel);
        }

        [Route("/[controller]/[action]")]
        public async Task<IActionResult> IQuery()
        {
            VPCRequest conn = new VPCRequest(paygateInfo, _logger);
            conn.SetSecureSecret(paygateInfo._SECURE_SECRET);
            var d1 = DateTime.Now.AddMinutes(-60);
            var d2 = DateTime.Now.AddMinutes(-10);
            Expression<Func<Contact, bool>> sqlWhere = u => (u.StatusId == 0 && u.ContactDate >= d1 && u.ContactDate <= d2);
            var a = await _Service.contactServices.GetManyAsync(sqlWhere);

            foreach(Contact a1 in a)
            {
                string result = await conn.CreateIQuery(_logger, paygateInfo, (a1.Id + Tools.StartIdOrder).ToString());
                string[] r = result.Split(new string[] { "&" }, StringSplitOptions.None);
                string vpc_DRExists = "";
                string vpc_Amount = "";
                string vpc_MerchTxnRef = "";
                string vpc_OrderInfo = "";
                string vpc_TransactionNo = "";
                string vpc_TxnResponseCode = "";
                string vpc_Message = "";
                string vpc_Merchant = "";
                for (int i = 0; i < r.Length; i++)
                {
                    if (r[i].StartsWith("vpc_DRExists="))
                        vpc_DRExists = r[i].MySubString("vpc_DRExists=");
                    if (r[i].StartsWith("vpc_Merchant="))
                        vpc_Merchant = r[i].MySubString("vpc_Merchant=");
                    else if (r[i].StartsWith("vpc_Amount="))
                        vpc_Amount = r[i].MySubString("vpc_Amount=");
                    else if (r[i].StartsWith("vpc_MerchTxnRef="))
                        vpc_MerchTxnRef = r[i].MySubString("vpc_MerchTxnRef=");
                    else if (r[i].StartsWith("vpc_OrderInfo="))
                        vpc_OrderInfo = r[i].MySubString("vpc_OrderInfo=");
                    else if (r[i].StartsWith("vpc_TransactionNo="))
                        vpc_TransactionNo = r[i].MySubString("vpc_TransactionNo=");
                    else if (r[i].StartsWith("vpc_TxnResponseCode="))
                        vpc_TxnResponseCode = r[i].MySubString("vpc_TxnResponseCode=");
                    else if (r[i].StartsWith("vpc_Message="))
                        vpc_Message = r[i].MySubString("vpc_Message=");
                }
                _logger.LogInformation($"CreateIQuery/ {(a1.Id + Tools.StartIdOrder).ToString()}: {result}");
                string hashvalidateResult = conn.Process3PartyResponse(HttpUtility.ParseQueryString(result));
                _logger.LogInformation($"hashvalidateResult: {hashvalidateResult}");
                if (hashvalidateResult == "CORRECTED" && vpc_Merchant == paygateInfo._MerchantID)//CORRECTED
                {                    
                    _logger.LogInformation($"vpc_DRExists: {vpc_DRExists}/ vpc_TxnResponseCode: {vpc_TxnResponseCode}/ vpc_Message: {vpc_Message}");
                    if (vpc_DRExists == "Y" && vpc_TxnResponseCode == "0")
                    {
                        var _vpc_Amount = int.Parse(vpc_Amount.Left(vpc_Amount.Length - 2));
                        string[] _orderInfo = vpc_OrderInfo.Split(new string[] { "_" }, StringSplitOptions.None);
                        var _orderId = long.Parse(vpc_MerchTxnRef) - Tools.StartIdOrder;
                        var _order = await _Service.contactServices.GetByIdAsync(_orderId);

                        if (_orderInfo[0] == "1") // Order đăng ký dịch vụ
                        {
                            _order.StatusId = 4;
                            _order.CookieID = vpc_TransactionNo;
                            _order.OrderStatus = await _unitOfWork.orderStatusRepository.GetByIdAsync(4);
                            var _orderU = await _Service.contactServices.Update(_order);
                            string msg = $"<ul><li><b>{_localizer["Tên đầy đủ"]}:</b> {_order.Fullname}</li><li><b>Email:</b> {_order.Email}</li><li><b>Mobile:</b> {_order.Mobile}</li><li><b>Trạng thái:</b> Đã thanh toán</li></li><li>{_order.Description}</li></ul>";
                            await _emailSender.SendEmailAsync(_order.Email, _localizer.GetString("v.v Liên hệ Ngọc Tuấn"), msg);
                        }
                        else if (_orderInfo[0] == "2") // Order thanh toán hóa đơn nước
                        {
                            //vpc_OrderInfo = $"2_{inv.CustomerCode}_{inv.InvoiceNo}_{inv.InvoiceAmount}",
                            var InvoiceAmount = int.Parse(_orderInfo[3]);
                            if (_orderInfo.Length == 4 && _vpc_Amount == InvoiceAmount && _order.Description == vpc_OrderInfo)
                            {
                                if (_order.StatusId != 4 && _order.StatusId != 6)
                                {
                                    var orderSave = await _unitOfWork.invoiceSaveRepository.GetByIdAsync(_order.Id);
                                    if (orderSave != null)
                                    {
                                        orderSave.PaymentStatus = 1;
                                        _unitOfWork.invoiceSaveRepository.Update(orderSave);
                                    }
                                    _order.StatusId = 4;
                                    _order.CookieID = vpc_TransactionNo;
                                    _order.OrderStatus = await _unitOfWork.orderStatusRepository.GetByIdAsync(4);
                                    var _orderU = await _Service.contactServices.Update(_order);
                                    _logger.LogInformation($"Thanh toan thanh cong {_orderId} {vpc_TxnResponseCode} {vpc_TransactionNo}");
                                }
                                else
                                {
                                    _logger.LogInformation($"Don hang da xu ly {_orderId} {vpc_TxnResponseCode} {vpc_TransactionNo}");
                                }
                                PayInput inv1 = new PayInput()
                                {
                                    CustomerCode = _orderInfo[1],
                                    InvoiceNo = _orderInfo[2],
                                    InvoiceAmount = InvoiceAmount,
                                    OnePayID = vpc_TransactionNo
                                };
                                PayResult payResult = await _iInvoiceServices.PayInvoice(inv1);
                                _logger.LogInformation($"hashvalidateResult: {hashvalidateResult}; vpc_OrderInfo: {vpc_OrderInfo}; vpc_TransactionNo: {vpc_TransactionNo}; vpc_Amount: {vpc_Amount}; Result: {payResult.PayStatus}");
                            }
                            else
                            {
                                _logger.LogInformation($"Error: hashvalidateResult: {hashvalidateResult}; vpc_OrderInfo: {vpc_OrderInfo}; vpc_TransactionNo: {vpc_TransactionNo}; vpc_Amount: {vpc_Amount}");
                            }
                        }
                    }
                }
            }
            return Ok();
        }

        [Route("/[controller]/[action]/{OrderId?}")]
        public async Task<IActionResult> Complete(long? OrderId)
        {
            var _order = new InvoiceResultModel()
            {
                order = await _Service.contactServices.GetByIdAsync((OrderId.HasValue ? OrderId.Value : 0)),
                IsInvoice = false
            };
            _order.order.Services = await _Service.serviceServices.GetByIdAsync(_order.order.ServiceId.Value);
            _order.order.OrderStatus = await _unitOfWork.orderStatusRepository.GetByIdAsync((_order.order.StatusId.HasValue ? _order.order.StatusId.Value : 0));
            ViewData["ServiceList"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(await _Service.serviceServices.GetAllAsync(), "Id", "Title");
            ViewData["ServiceDetail"] = _order.order.Services;

            return View("Complete", _order);
        }
        #endregion
    }
}
