using EntityFramework.Web.Entities.Ordering;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Paygate.OnePay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using WebClient.Helpers;
using WebClient.Models.Category;
using WebClient.Models.Order;
using WebClient.Repository.Interfaces;
using WebClient.Services.Interfaces;

namespace WebClient.Controllers
{
    //[SecurityHeaders]
    public class OrderController : Controller
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger<OrderController> _logger;
        private readonly IAllService _Service;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<OrderController> _localizer;
        private readonly IEmailSender _emailSender;
        //private readonly IVPCRequest _vPCRequest;
        private PaygateInfo paygateInfo;
        private IConfiguration configuration;
        public OrderController(IConfiguration configuration, IUnitOfWork _unitOfWork, IEmailSender _emailSender, IDistributedCache _cache, ILogger<OrderController> _logger, IAllService _Service, IStringLocalizer<OrderController> _localizer)
        {
            this._logger = _logger;
            this._Service = _Service;
            this._localizer = _localizer;
            this._cache = _cache;
            this._emailSender = _emailSender;
            this._unitOfWork = _unitOfWork;
            //this._vPCRequest = _vPCRequest;
            this.configuration = configuration;
            paygateInfo = this.configuration.GetSection(nameof(PaygateInfo)).Get<PaygateInfo>();
        }

        public async Task<IActionResult> Index()
        {
            ProductView a = null;

            var b = HttpContext.OrderGet();
            ViewCart c = new ViewCart()
            {
                order = b,
                product = a,
                paramSetting = await _Service.paramSettingServices.GetAllAsync()
            };

            return View("Cart", c);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/[controller]/[action]/{Id?}")]
        public async Task<IActionResult> Add(long? Id)
        {
            long _Id = (Id.HasValue ? Id.Value : 0);
            int Qty = 0;
            if (int.TryParse(HttpContext.Request.Form["quantity"].ToString(), out Qty)) await AddOrUpdateOrder(_Id, Qty);
            return RedirectToAction("Cart", "Order", new { Id = _Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/[controller]/[action]")]
        public async Task<IActionResult> Update()
        {
            try
            {
                CreateOrder a = HttpContext.ReadCookie<CreateOrder>(Util.OrderKey);
                var cnt = int.Parse(HttpContext.GetFormValue("Cnt"));
                for (var i = 0; i < cnt; i++)
                {
                    long ProductId = 0; int Qty = 0;
                    string _productId = HttpContext.GetFormValue($"ProductId{i}");
                    string _qty = HttpContext.GetFormValue($"qty{i}");
                    if (long.TryParse(_productId, out ProductId) && int.TryParse(_qty, out Qty))
                    {
                        await AddOrUpdateOrder(a, ProductId, Qty);
                    }
                    else
                    {
                        _logger.LogError($"Cập nhật đơn hàng lỗi ProductId {_productId} Qty {_qty}");
                    }
                }
                if (Util.IsLogger) _logger.LogInformation($"Order is {JsonConvert.SerializeObject(a)} value");
                HttpContext.WriteCookie<CreateOrder>(Util.OrderKey, a);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Cập nhật đơn hàng lỗi {ex.Message}");
            }

            return RedirectToAction("Cart", "Order");
        }

        [Route("/[controller]/[action]/{Id?}")]
        public IActionResult Remove(long? Id)
        {
            long _Id = (Id.HasValue ? Id.Value : 0);
            if (Util.IsLogger) _logger.LogInformation($"Remove order productid = {_Id}");
            HttpContext.OrderRemove(_logger, _Id);
            //var a = await _Service.productServices.GetByIdAsync(_Id);
            //return RedirectToAction("Details", "Categories", new { Id = _Id });
            //return RedirectToAction("Index", "Categories", new { Id = a.CategoryMain });
            return RedirectToAction("Cart", "Order");
        }

        [Route("/[controller]/[action]/{Id?}")]
        public async Task<IActionResult> Cart(long? Id)
        {
            long _Id = (Id.HasValue ? Id.Value : 0);
            ProductView a = null;
            if (_Id > 0)
            {
                var b1 = await _Service.productServices.GetByIdAsync(_Id);
                a = new ProductView()
                {
                    CategoryName = b1.MainCategories.Name,
                    Code = b1.Code,
                    Name = b1.Name,
                    Id = b1.Id,
                    Discount = b1.Discount,
                    Img = b1.Img,
                    Price = b1.Price,
                    Quota = b1.Quota,
                    CategoryId = b1.CategoryMain.Value
                };
            }

            var b = HttpContext.OrderGet();
            ViewCart c = new ViewCart()
            {
                order = b,
                product = a,
                paramSetting = await _Service.paramSettingServices.GetAllAsync()
            };
            if (User.Identity.IsAuthenticated)
            {
                c.Fullname = User.Claims.GetClaimValue("name");
                c.Email = User.Claims.GetClaimValue("email");
                c.Mobile = User.Claims.GetClaimValue("phone_number");
                c.Address = User.Claims.GetClaimValue("address");
            }
            return View(c);
        }

        [Route("/[controller]/[action]")]
        public async Task<IActionResult> Checkout()
        {
            ProductView a = null;

            var b = HttpContext.OrderGet();
            if (b == null)
            {
                return RedirectToAction(nameof(Cart));
            }
            CheckoutModel c = new CheckoutModel()
            {
                order = b,
                product = a
            };
            if (User.Identity.IsAuthenticated)
            {
                c.Fullname = User.Claims.GetClaimValue("name");
                c.Email = User.Claims.GetClaimValue("email");
                c.Mobile = User.Claims.GetClaimValue("phone_number");
                c.Address = User.Claims.GetClaimValue("address");
            }

            return View(c);
        }

        [Route("/[controller]/[action]")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Complete(CheckoutInput input)
        {
            ProductView a = null;

            var b = HttpContext.OrderGet();
            ViewCart c = new ViewCart()
            {
                order = b,
                product = a,
                paramSetting = await _Service.paramSettingServices.GetAllAsync()
            };

            if (User.Identity.IsAuthenticated)
            {
                c.Fullname = User.Claims.GetClaimValue("name");
                c.Email = User.Claims.GetClaimValue("email");
                c.Mobile = User.Claims.GetClaimValue("phone_number");
                c.Address = User.Claims.GetClaimValue("address");
                b.UserId = long.Parse(User.Claims.GetClaimValue(ClaimTypes.NameIdentifier));
            }
            if (!string.IsNullOrEmpty(input.Address)) c.Address = input.Address;
            if (!string.IsNullOrEmpty(input.Description)) c.Description = input.Description;
            if (!string.IsNullOrEmpty(input.Email)) c.Email = input.Email;
            if (!string.IsNullOrEmpty(input.Fullname)) c.Fullname = input.Fullname;
            if (!string.IsNullOrEmpty(input.Mobile)) c.Mobile = input.Mobile;

            if (ModelState.IsValid)
            {
                if (input.IsAgree)
                {
                    var payment_method = int.Parse(HttpContext.GetFormValue("payment_method"));
                    double Total = 0;
                    List<EntityFramework.Web.Entities.Ordering.OrderItem> OrderItems = new List<EntityFramework.Web.Entities.Ordering.OrderItem>();
                    foreach(var item in b.OrderItems)
                    {
                        var b1 = await _Service.productServices.GetByIdAsync(item.ProductId);
                        var Total1 = (b1.Price - b1.Discount) * item.Units;
                        var u = new EntityFramework.Web.Entities.Ordering.OrderItem()
                        {
                            Discount = b1.Discount,
                            Price = b1.Price,
                            ProductId = item.ProductId,
                            Total = Total1,
                            Units = item.Units
                        };
                        Total = Total + Total1;
                        OrderItems.Add(u);
                    }

                    var FreeShip = double.Parse(c.paramSetting.Where(u => u.ParamKey == "SHIPFREE" && u.Language == "all").FirstOrDefault().ParamValue);//ViewData.GetSettingByKey("all", "SHIPFREE")
                    var FeeShip = (FreeShip <= Total ? 0 : double.Parse(c.paramSetting.Where(u => u.ParamKey == "SHIPFEE" && u.Language == "all").FirstOrDefault().ParamValue)); //ViewData.GetSettingByKey("all", "SHIPFEE")

                    Order order = new Order()
                    {
                        OrderDate = DateTime.Now,
                        Address = input.Address,
                        Description = input.Description,
                        Email = input.Email,
                        FeeShip = FeeShip,
                        Fullname = input.Fullname,
                        Mobile = input.Mobile,
                        OrderItems = OrderItems,
                        Total = Total + FeeShip,
                        StatusId = 0,
                        UserName = b.UserName,
                        UserId = (b.UserId.HasValue ? b.UserId.Value : -1),
                        PaymentMethod = payment_method,
                        IsAgree = input.IsAgree
                    };

                    var _order = await _Service.orderServices.Add(order);

                    if (_order != null)
                    {
                        // xóa cookie đơn hàng tạm
                        HttpContext.OrderClear(_logger);

                        //Parallel.For(0, _order.OrderItems.Count(), async delegate (int i)
                        //{
                        //    _order.OrderItems[i].Product = await _Service.productServices.GetByIdAsync(_order.OrderItems[i].ProductId);
                        //});
                        //double Total1 = 0;
                        for (var i = 0; i < _order.OrderItems.Count(); i++)
                        {
                            _order.OrderItems[i].Product = await _Service.productServices.GetByIdAsync(_order.OrderItems[i].ProductId);
                            //Total1 += (_order.OrderItems[i].Product.Price - _order.OrderItems[i].Product.Discount) * _order.OrderItems[i].Units;
                        }
                        //_order.Total = Total1;
                        //await _emailSender.SendEmailAsync(order.Email, $"Cảm ơn {order.Fullname}. Đơn hàng của bạn đã được nhận!", $"Mã đơn hàng {_order.Id}; Phí Ship {_order.FeeShip}; Tổng tiền {(_order.Total + _order.FeeShip)}");

                        _order.OrderStatus = await _unitOfWork.orderStatusRepository.GetByIdAsync(_order.StatusId);

                        if (payment_method == 3) payment_method = 1;
                        switch (payment_method)
                        {
                            case 3:
                                PaymentIn t = new PaymentIn()
                                {
                                    vpc_Amount = (_order.Total + _order.FeeShip).ToString(),
                                    vpc_Customer_Email = _order.Email,
                                    vpc_Customer_Id = (_order.UserId.HasValue ? _order.UserId.Value : 0).ToString(),
                                    vpc_Customer_Phone = "",
                                    vpc_MerchTxnRef = (_order.Id + Tools.StartIdOrder).ToString(),
                                    vpc_OrderInfo = $"OrderId{_order.Id}_{_order.Total + _order.FeeShip}",
                                    vpc_SHIP_City = "Han",
                                    vpc_SHIP_Country = "VN",
                                    vpc_SHIP_Provice = "Han",
                                    vpc_SHIP_Street01 = _order.Address
                                };
                                VPCRequest conn = new VPCRequest(paygateInfo, _logger);
                                var url = conn.CreatePay(HttpContext, paygateInfo, t);
                                _logger.LogInformation(url);
                                return Redirect(url);
                            default:
                                await _emailSender.SendEmailOrderAsync(_order, configuration, _Service,
                                    HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name, _localizer, HttpContext);
                                return View(_order);
                        }
                    }
                    else
                    {
                        ViewData.SetNotification(_localizer.GetString("Thêm đơn hàng thất bại"));
                    }
                }
                else
                {
                    ModelState.AddModelError("IsAgree", _localizer.GetString("Bạn chưa chọn đồng ý điều khoản thanh toán!"));
                }
            }
            return View("Cart", c);
        }

        [Route("/[controller]/[action]")]
        public async Task<IActionResult> IPN()
        {
            var vpc_MerchTxnRef = HttpContext.GetQueryString("vpc_MerchTxnRef", false);
            var vpc_TxnResponseCode = HttpContext.GetQueryString("vpc_TxnResponseCode", false);
            var vpc_TransactionNo = HttpContext.GetQueryString("vpc_TransactionNo", false);

            try
            {
                VPCRequest conn = new VPCRequest(paygateInfo, _logger);
                conn.SetSecureSecret(paygateInfo._SECURE_SECRET);
                string hashvalidateResult = conn.Process3PartyResponse(HttpUtility.ParseQueryString(HttpContext.Request.QueryString.Value));
                var _orderId = long.Parse(vpc_MerchTxnRef) - Tools.StartIdOrder;
                var _order = await _Service.orderServices.GetByIdAsync(_orderId);
                _order.OrderItems = await _unitOfWork.orderItemRepository.GetByOrderIdAsync(_orderId);

                if (hashvalidateResult == "CORRECTED" && vpc_TxnResponseCode.Trim() == "0")
                {
                    if (_order.StatusId != 4 && _order.StatusId != 6)
                    {
                        _order.StatusId = 4;
                        _order.CookieID = vpc_TransactionNo;
                        _order.OrderStatus = await _unitOfWork.orderStatusRepository.GetByIdAsync(4);
                        var _orderU = await _Service.orderServices.Update(_order);
                        _logger.LogInformation($"Thanh toan thanh cong {_orderId} {vpc_TxnResponseCode} {vpc_TransactionNo}");
                    }
                    else
                    {
                        _logger.LogInformation($"Don hang da xu ly {_orderId} {vpc_TxnResponseCode} {vpc_TransactionNo}");
                    }
                }
                else if (hashvalidateResult == "INVALIDATED" && vpc_TxnResponseCode.Trim() == "0")
                {
                    _logger.LogInformation($"Don hang dang xu ly {_orderId} {vpc_TxnResponseCode} {vpc_TransactionNo}");
                }
                else
                {
                    if (_order.StatusId != 4 && _order.StatusId != 6)
                    {
                        _order.StatusId = 6;
                        _order.OrderStatus = await _unitOfWork.orderStatusRepository.GetByIdAsync(6);
                        var _orderU = await _Service.orderServices.Update(_order);
                        _logger.LogInformation($"Huy thanh cong {_orderId} {vpc_TxnResponseCode} {vpc_TransactionNo}");
                        await _emailSender.SendEmailOrderAsync(_order, configuration, _Service,
                            HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name, _localizer, HttpContext);
                    }
                    else
                    {
                        _logger.LogInformation($"Don hang da xu ly {_orderId} {vpc_TxnResponseCode} {vpc_TransactionNo}");
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
            //var vpc_Merchant = HttpContext.GetQueryString("vpc_Merchant");
            //var vpc_OrderInfo = HttpContext.GetQueryString("vpc_OrderInfo");
            //var vpc_Amount = HttpContext.GetQueryString("vpc_Amount");
            var vpc_TransactionNo = HttpContext.GetQueryString("vpc_TransactionNo", false);
            //var vpc_Message = HttpContext.GetQueryString("vpc_Message");
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

            var _orderId = long.Parse(vpc_MerchTxnRef) - Tools.StartIdOrder;
            var _order = await _Service.orderServices.GetByIdAsync(_orderId);
            _order.OrderItems = await _unitOfWork.orderItemRepository.GetByOrderIdAsync(_orderId);

            //for (var i = 0; i < _order.OrderItems.Count(); i++)
            //{
            //    _order.OrderItems[i].Product = await _Service.productServices.GetByIdAsync(_order.OrderItems[i].ProductId);
            //}

            if (hashvalidateResult == "CORRECTED" && vpc_TxnResponseCode.Trim() == "0")
            {
                ViewData.SetNotification(_localizer.GetString("/ <font color=\"green\">Thanh toán thành công</font>"));
                _order.StatusId = 4;
                _order.CookieID = vpc_TransactionNo;
                _order.OrderStatus = await _unitOfWork.orderStatusRepository.GetByIdAsync(4);
                var _orderU = await _Service.orderServices.Update(_order);
                await _emailSender.SendEmailOrderAsync(_order, configuration, _Service,
                            HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name, _localizer, HttpContext);
            }
            else if (hashvalidateResult == "INVALIDATED" && vpc_TxnResponseCode.Trim() == "0")
            {
                ViewData.SetNotification(_localizer.GetString("<font color=\"yellow\">Đang chờ thanh toán</font>"));
            }
            else if (hashvalidateResult == "CORRECTED" && vpc_TxnResponseCode.Trim() == "99")
            {
                ViewData.SetNotification(_localizer.GetString("/ <font color=\"red\">Thanh toán thất bại</font>"));
                _order.StatusId = 6;
                _order.OrderStatus = await _unitOfWork.orderStatusRepository.GetByIdAsync(6);
                var _orderU = await _Service.orderServices.Update(_order);
                await _emailSender.SendEmailOrderAsync(_order, configuration, _Service,
                            HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name, _localizer, HttpContext);
            }
            else 
            {
                ViewData.SetNotification(_localizer.GetString("<font color=\"yellow\">Invalid</font>"));
            }

            return View("Complete", _order);

        }

        private async Task<bool> AddOrUpdateOrder(long ProductId, int Qty)
        {
            try
            {
                if (Util.IsLogger) _logger.LogInformation($"AddOrUpdateOrder productid = {ProductId}, Qty {Qty}");
                var a = await _Service.productServices.GetByIdAsync(ProductId);
                HttpContext.OrderAdd(_logger, a.Id, a.Code, a.Name, a.Img, a.Price, a.Discount, Qty);
                return true;
            }
            catch (Exception ex)
            {
                if (Util.IsLogger) _logger.LogInformation($"AddOrUpdateOrder productid = {ProductId}, Qty {Qty} Error: {ex.Message}");
                return false;
            }
        }
        private async Task<bool> AddOrUpdateOrder(CreateOrder order, long ProductId, int Qty)
        {
            try
            {
                if (Util.IsLogger) _logger.LogInformation($"AddOrUpdateOrder productid = {ProductId}, Qty {Qty}");
                var a = await _Service.productServices.GetByIdAsync(ProductId);
                order.OrderAdd(_logger, a.Id, a.Code, a.Name, a.Img, a.Price, a.Discount, Qty);
                return true;
            }
            catch (Exception ex)
            {
                if (Util.IsLogger) _logger.LogInformation($"AddOrUpdateOrder productid = {ProductId}, Qty {Qty} Error: {ex.Message}");
                return false;
            }
        }
    }
}
