using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Paygate.OnePay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Models.Category;
using WebClient.Models.Home;
using WebClient.Models.Order;
using WebClient.Services.Interfaces;

namespace WebClient.Helpers
{
    public static class Util
    {
        #region Properties
        public const string ProductViewKey = "_BacNgocTuan_ProductView";
        public const string SearchInputKey = "_BacNgocTuan_SearchInput";
        public const string OrderKey = "_BacNgocTuan_OrderKey";
        public const int MaxQtyProductViewKey = 5;
        public const bool IsLogger = true;
        #endregion

        #region Init
        public static async Task<ViewDataDictionary> InitialAsync(this ViewDataDictionary viewData, IAllService _Service)
        {
            viewData.SetCategory(await _Service.categoriesServices.GetAllAsync());
            viewData.SetSetting(await _Service.paramSettingServices.GetAllAsync());
            return viewData;
        }
        #endregion

        #region Httpcontext
        public static void SetSearchInput(this ViewDataDictionary viewData, SearchInput b)
        {
            viewData.SetData(SearchInputKey, b);
        }

        public static SearchInput GetSearchInput(this ViewDataDictionary viewData)
        {
            return viewData.GetData<SearchInput>(SearchInputKey);
        }

        public static IEnumerable<ProductView> ChangeProductView(this HttpContext context, ProductView obj)
        {
            var a = context.ReadCookie<IEnumerable<ProductView>>(ProductViewKey);
            IList<ProductView> a1;
            if (a == null)
            {
                a1 = new List<ProductView>();
            }
            else
            {
                a1 = a.ToList();
            }
            if (!a1.Any(u => u.Id == obj.Id))
            {
                a1.Add(obj);
                var b = a1.OrderByDescending(u => u.TimeAdd).Take(MaxQtyProductViewKey);
                context.WriteCookie<IEnumerable<ProductView>>(ProductViewKey, b);
            }

            return a1;
        }
        public static void SetPage(this ViewDataDictionary viewData, PathOfPage obj)
        {
            viewData.SetData(Tools.PageOfPage, obj);
        }
        public static PathOfPage GetPage(this ViewDataDictionary viewData)
        {
            return viewData.GetData<PathOfPage>(Tools.PageOfPage);
        }
        #endregion

        #region Order
        public static CreateOrder OrderAdd(this CreateOrder a, ILogger logger, long ProductId, string ProductCode, string ProductName, string ProductImg, double Price, double Discount = 0, int Units = 1)
        {
            if (a == null)
            {
                if (IsLogger) logger.LogInformation($"Reading OrderKey cookie is null value");
                var StatusId = 0;
                var Total = (Price - Discount) * Units;
                var OrderItems = new List<OrderItem>();
                OrderItems.Add(new OrderItem()
                {
                    ProductId = ProductId,
                    ProductCode = ProductCode,
                    ProductName = ProductName,
                    ProductImg = ProductImg,
                    Price = Price,
                    Discount = Discount,
                    Units = Units,
                    Total = Total
                });
                a = new CreateOrder()
                {
                    OrderDate = DateTime.Now,
                    StatusId = StatusId,
                    OrderItems = OrderItems,
                    Total = Total
                };
            }
            else
            {
                if (IsLogger) logger.LogInformation($"Reading OrderKey cookie is {JsonConvert.SerializeObject(a)} value");
                var b = a.OrderItems.FirstOrDefault(u => u.ProductId == ProductId);
                if (b == null)
                {
                    var Total = (Price - Discount) * Units;
                    a.OrderItems.Add(new OrderItem()
                    {
                        ProductId = ProductId,
                        ProductCode = ProductCode,
                        ProductName = ProductName,
                        ProductImg = ProductImg,
                        Price = Price,
                        Discount = Discount,
                        Units = Units,
                        Total = Total
                    });
                    a.Total += Total;
                }
                else
                {
                    var i = a.OrderItems.IndexOf(b);
                    var Total = (a.OrderItems[i].Price - a.OrderItems[i].Discount) * Units;
                    var OldTotal = a.OrderItems[i].Total;
                    a.OrderItems[i].Units = Units;
                    a.OrderItems[i].Total += (Total - OldTotal);
                    a.Total += (Total - OldTotal);
                }
            }
            if (IsLogger) logger.LogInformation($"Order is {JsonConvert.SerializeObject(a)} value");
            return a;
        }

        public static CreateOrder OrderAdd(this HttpContext context, ILogger logger, long ProductId, string ProductCode, string ProductName, string ProductImg, double Price, double Discount = 0, int Units = 1)
        {
            CreateOrder a = context.ReadCookie<CreateOrder>(OrderKey);
            a = a.OrderAdd(logger, ProductId, ProductCode, ProductName, ProductImg, Price, Discount, Units);
            if (IsLogger) logger.LogInformation($"Reading OrderKey cookie is {JsonConvert.SerializeObject(a)} value");
            context.WriteCookie<CreateOrder>(OrderKey, a);
            return a;
        }

        public static void OrderClear(this HttpContext context, ILogger logger)
        {
            var a = context.ReadCookie<CreateOrder>(OrderKey);
            if (a == null)
            {
                if (IsLogger) logger.LogInformation($"Reading OrderKey cookie is null value");
                return;
            }
            else
            {
                if (IsLogger) logger.LogInformation($"Reading OrderKey cookie is {JsonConvert.SerializeObject(a)} value");
                a.OrderItems.Clear();
                a = null;
                context.WriteCookie<CreateOrder>(OrderKey, a);
                return;
            }
        }

        public static CreateOrder OrderRemove(this HttpContext context, ILogger logger, long ProductId)
        {
            var a = context.ReadCookie<CreateOrder>(OrderKey);
            if (a == null)
            {
                if (IsLogger) logger.LogInformation($"Reading OrderKey cookie is null value");
                return default;
            }
            else
            {
                if (IsLogger) logger.LogInformation($"Reading OrderKey cookie is {JsonConvert.SerializeObject(a)} value");
                var b = a.OrderItems.FirstOrDefault(u => u.ProductId == ProductId);
                if (b == null)
                {
                    if (IsLogger) logger.LogInformation($"Reading OrderKey cookie is not found {ProductId}");
                    return a;
                }
                else
                {
                    if (a.OrderItems.Count() <= 1)
                    {
                        if (IsLogger) logger.LogInformation($"Clear OrderKey cookie is found {ProductId}");
                        a.OrderItems.Clear();
                        a = null;
                        //context.DeleteCookie(OrderKey);
                        context.WriteCookie<CreateOrder>(OrderKey, a);
                        return default;
                    }
                    else
                    {
                        if (IsLogger) logger.LogInformation($"Remove OrderKey cookie is found {ProductId}");
                        a.OrderItems.Remove(b);
                        a.Total -= b.Total;
                    }
                }
            }
            if (IsLogger) logger.LogInformation($"Reading OrderKey cookie is {JsonConvert.SerializeObject(a)} value");
            context.WriteCookie<CreateOrder>(OrderKey, a);
            return a;
        }

        public static CreateOrder OrderDeduct(this HttpContext context, ILogger logger, long ProductId, int Units = 1)
        {
            var a = context.ReadCookie<CreateOrder>(OrderKey);
            if (a == null)
            {
                if (IsLogger) logger.LogInformation($"Reading OrderKey cookie is null value");
                return default;
            }
            else
            {
                if (IsLogger) logger.LogInformation($"Reading OrderKey cookie is {JsonConvert.SerializeObject(a)} value");
                var b = a.OrderItems.FirstOrDefault(u => u.ProductId == ProductId);
                if (b == null)
                {
                    if (IsLogger) logger.LogInformation($"Reading OrderKey cookie is not found {ProductId} value");
                    return a;
                }
                else
                {
                    if (IsLogger) logger.LogInformation($"Reading OrderKey cookie is {JsonConvert.SerializeObject(b)} value");
                    var i = a.OrderItems.IndexOf(b);
                    if (a.OrderItems[i].Units <= Units)
                    {
                        if (IsLogger) logger.LogInformation($"Reading OrderKey cookie is remove {JsonConvert.SerializeObject(b)} value");
                        if (a.OrderItems.Count() <= 1)
                        {
                            a.OrderItems.Clear();
                            a = null;
                        }
                        else
                        {
                            a.OrderItems.Remove(b);
                            a.Total -= b.Total;
                        }
                        if (IsLogger) logger.LogInformation($"Reading OrderKey cookie is count {JsonConvert.SerializeObject(a.OrderItems.Count())} value");
                    }
                    else
                    {
                        if (IsLogger) logger.LogInformation($"Reading OrderKey cookie is update {JsonConvert.SerializeObject(b)} value");
                        var Total = (a.OrderItems[i].Price - a.OrderItems[i].Discount) * Units;
                        a.OrderItems[i].Units -= Units;
                        a.OrderItems[i].Total -= Total;
                        a.Total -= Total;
                    }
                }
            }

            context.WriteCookie<CreateOrder>(OrderKey, a);
            return a;
        }

        public static CreateOrder OrderGet(this HttpContext context)
        {
            var a = context.ReadCookie<CreateOrder>(OrderKey);
            return a;
        }

        public static async Task SendEmailOrderAsync(this IEmailSender sender, EntityFramework.Web.Entities.Ordering.Order _order,
            IConfiguration configuration, IAllService _Service, string Language, IStringLocalizer _localizer, HttpContext context)
        {
            EmailInfo emailInfo = configuration.GetSection(nameof(EmailInfo)).Get<EmailInfo>();
            string _body = "";
            if (Language == "vi")
                _body = (await _Service.aboutServices.GetByIdAsync(emailInfo.EmailTemplateId_vi)).Description; // 
            else
                _body = (await _Service.aboutServices.GetByIdAsync(emailInfo.EmailTemplateId_en)).Description; // 

            #region Parameter
            _body = _body.Replace("[ORDERCODE]", "#" + _order.Id.ToString())
                    .Replace("[CUSTOMERNAME]", _order.Fullname)
                    .Replace("[CUSTOMEREMAIL]", _order.Email)
                    .Replace("[CUSTOMERPHONE]", _order.Mobile)
                    .Replace("[CUSTOMERADDRESS]", _order.Address)

                    .Replace("[PAYMENTMETHOD]", GetPaymentMethod(_localizer, _order.PaymentMethod.Value) +
                            "/ " + _order.OrderStatus.Name)
                    .Replace("[SHIPFEE]", _order.FeeShip.ToString().FormatNumber())
                    .Replace("[PRODUCTALLTOTAL]", _order.Total.ToString().FormatNumber())
                    .Replace("[ORDERTOTAL]", (_order.FeeShip + _order.Total).ToString().FormatNumber())

                    .Replace("[ORDERVIEWURL]", context.MerchantUrl(emailInfo.ORDERVIEWURL.Replace("[ORDERCODE]", _order.Id.ToString())))
                    .Replace("[FAQURL]", context.MerchantUrl(emailInfo.FAQURL))
                    .Replace("[SUPPORTURL]", context.MerchantUrl(emailInfo.SUPPORTURL))
                    .Replace("[HOMEURL]", context.MerchantUrl(emailInfo.HOMEURL))
                    .Replace("[OFFICER]", emailInfo.OFFICER);
            #endregion

            #region ProductList
            string productTag = "";
            int i; int j; string _beginTag = "<!--PRODUCT-->"; string _endTag = "<!--/PRODUCT-->";
            i = _body.IndexOf(_beginTag);
            j = _body.IndexOf(_endTag);
            if (i > -1 && j > i)
            {
                productTag = _body.Substring(i + _beginTag.Length, j - i - _beginTag.Length);
                _body = _body.Substring(0, i) + "[PRODUCTLIST]" + _body.Substring(j + _endTag.Length);
            }
            string ProductList = "";
            if (productTag != "")
            {
                foreach (var item in _order.OrderItems)
                {
                    ProductList += productTag.Replace("[PRODUCTNAME]", item.Product.Name + " " + item.Product.Code)
                                    .Replace("[PRODUCTPRICE]", item.Price.ToString().FormatNumber())
                                    .Replace("[PRODUCTQTY]", item.Units.ToString().FormatNumber())
                                    .Replace("[PRODUCTDISCOUNT]", item.Discount.ToString().FormatNumber())
                                    .Replace("[PRODUCTTOTAL]", item.Total.ToString().FormatNumber());
                }
            }
            #endregion
            _body = _body.Replace("[PRODUCTLIST]", ProductList);
            await sender.SendEmailAsync(_order.Email, $"Cảm ơn {_order.Fullname}. Đơn hàng của bạn đã được nhận!", _body);
        }

        public static string GetPaymentMethod(IStringLocalizer _localizer, int Id)
        {
            switch (Id)
            {
                case 1:
                    return _localizer.GetString("Tiền mặt, giao hàng thanh toán");
                case 2:
                    return _localizer.GetString("Chuyển khoản ngân hàng");
                case 3:
                    return _localizer.GetString("Thanh toán online");
                default:
                    return _localizer.GetString("Thanh toán và nhận hàng tại cửa hàng");
            }
        }
        #endregion
    }

    public class EmailInfo
    {
        public string ORDERVIEWURL { get; set; }
        public string FAQURL { get; set; }
        public string SUPPORTURL { get; set; }
        public string HOMEURL { get; set; }
        public string OFFICER { get; set; }
        public long EmailTemplateId_vi { get; set; }
        public long EmailTemplateId_en { get; set; }
    }
}
