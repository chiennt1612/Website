using EntityFramework.Web.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Paygate.OnePay
{
    public static class Tools
    {
        public const string NumberSepr = ",";
        public const string Notification = "Notification";
        public const string PageOfPage = "PageOfPage";
        public const string SettingParam = "SettingParam";
        public const string Category = "Category";
        public const int StartIdOrder = 0;
        public const string ClaimCustomerCode = "username";

        public static string GetClaimValue(this IEnumerable<System.Security.Claims.Claim> Claims, string Key)
        {
            var a = Claims.Where(u => u.Type == Key).FirstOrDefault();
            if (a != null)
            {
                return a.Value;
            }
            else
            {
                return "";
            }
        }

        #region cache dist
        public static async Task<T> GetAsync<T>(this IDistributedCache _cache, string Key, CancellationToken token = default)
        {
            string r = await _cache.GetStringAsync(Key, token);
            if (String.IsNullOrEmpty(r))
                return default;
            else
                return DeserializeFromBase64String<T>(r);
        }
        public static async Task SetAsync<T>(this IDistributedCache _cache, string Key, T _obj, long _time = 1, CancellationToken token = default)
        {
            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions() { AbsoluteExpiration = DateTime.Now.AddMinutes(_time) };
            string r = SerializeToBase64String<T>(_obj);
            await _cache.SetStringAsync(Key, r, options, token);
        }
        #endregion

        #region Base64
        public static bool IsBase64String(this string s)
        {
            s = s.Trim();
            return (s.Length % 4 == 0) && Regex.IsMatch(s, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);

        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string SerializeToBase64String<T>(T obj)
        {
            var a = JsonConvert.SerializeObject(obj);
            return Base64Encode(a);
        }

        public static T DeserializeFromBase64String<T>(string content)
        {
            var obj = JsonConvert.DeserializeObject<T>(Base64Decode(content));
            return obj;
        }

        public static string Base64UrlEncode(byte[] s)
        {
            return Base64UrlTextEncoder.Encode(s);
        }
        public static byte[] Base64UrlDecode(string s)
        {
            return Base64UrlTextEncoder.Decode(s);
        }
        #endregion

        #region Zip
        public static string Zip(string s)
        {
            MemoryStream m = new MemoryStream();
            GZipStream gz = new GZipStream(m, CompressionMode.Compress);
            StreamWriter sw = new StreamWriter(gz);
            sw.Write(s); sw.Close();
            byte[] b = m.ToArray();
            string zip = Base64UrlEncode(b);
            return zip;
        }
        public static string UnZip(string s)
        {
            string r = "";
            try
            {
                byte[] r1;
                r1 = Base64UrlDecode(s);
                MemoryStream m = new MemoryStream(r1);
                GZipStream gz = new GZipStream(m, CompressionMode.Decompress);
                StreamReader sr = new StreamReader(gz);
                r = sr.ReadToEnd();
                sr.Close();
            }
            catch
            {
                r = "";
            }
            return r;
        }
        #endregion 

        #region Set-Get-ViewData
        public static void SetData<T>(this ViewDataDictionary viewData, string Key, T obj) where T : class
        {
            viewData[Key] = obj;
        }
        public static T GetData<T>(this ViewDataDictionary viewData, string Key) where T : class
        {
            return viewData[Key] as T;
        }
        public static void SetNotification(this ViewDataDictionary viewData, string obj)
        {
            viewData.SetData(Notification, obj);
        }
        public static string GetNotification(this ViewDataDictionary viewData)
        {
            return viewData.GetData<string>(Notification);
        }
        public static void SetSetting(this ViewDataDictionary viewData, IEnumerable<ParamSetting> obj)
        {
            viewData.SetData(SettingParam, obj);
        }
        public static IEnumerable<ParamSetting> GetSetting(this ViewDataDictionary viewData)
        {
            return viewData.GetData<IEnumerable<ParamSetting>>(SettingParam);
        }
        public static string GetSettingByKey(this ViewDataDictionary viewData, string lang, string Key)
        {
            var a = viewData.GetSetting().Where(u => u.ParamKey == Key && (u.Language == lang || u.Language == "all")).FirstOrDefault();
            if (a == null) return "0";
            return a.ParamValue;
        }
        public static void SetCategory(this ViewDataDictionary viewData, IEnumerable<Categories> obj)
        {
            viewData.SetData(Category, obj);
        }
        public static IEnumerable<Categories> GetCategory(this ViewDataDictionary viewData)
        {
            return viewData.GetData<IEnumerable<Categories>>(Category);
        }
        #endregion

        #region Number
        private static string insertSepr(string d0)
        {
            var d = "" + d0; // convert to string
            var i = 0;
            var d2 = "";
            var ic = 0;
            var ofs = d.Length - 1;
            var decimalpoint = d.IndexOf('.');
            if (decimalpoint >= 0) ofs = decimalpoint - 1;
            for (i = ofs; i >= 0; i--)
            {
                if (d[i].ToString() != NumberSepr)
                {
                    if (ic++ % 3 == 0 && i != ofs && d[i] != '-') d2 = NumberSepr + d2;
                    d2 = d[i] + d2;
                }
            }

            if (decimalpoint >= 0)
            {
                for (i = decimalpoint; i < d.Length; i++)
                    d2 += d[i];
            }
            return d2;
        }

        public static string RemNumSepr(string num)
        {
            return Regex.Replace(num.Replace(NumberSepr, ""), "[^0-9]", "");
        }

        public static string FormatNumber(this string num, int iRound = 0)
        {
            double a = 0;
            try
            {
                a = double.Parse(num);
                a = Math.Round(a, iRound);
            }
            catch
            {
                a = 0;
            }
            //if (a == 0) return "";
            return insertSepr(a.ToString());
        }

        public static string NumberTo00N(this int num, int round)
        {
            return ("0000000000" + num.ToString()).Right(round);
        }

        public static string Left(this string s, int n)
        {
            if (s.Length > n)
            {
                s = s.Substring(0, n);
            }
            return s;
        }

        public static string Right(this string s, int n, bool IsN = true)
        {
            if (IsN)
            {
                if (s.Length > n)
                {
                    s = s.Substring(s.Length - n, n);
                }
            }
            else
            {
                s = s.Substring(n);
            }
            return s;
        }
        
        public static string MySubString(this string s, string s1)
        {
            return s.Substring(s.IndexOf(s1) + s1.Length).Trim();
        }
        #endregion

        #region Cookie
        public static T ReadCookie<T>(this HttpContext context, string key)
        {
            var a = context.Request.Cookies[key];
            if (!String.IsNullOrEmpty(a))
            {
                if (IsBase64String(a))
                {
                    return DeserializeFromBase64String<T>(a);
                }
            }
            return default;
        }

        public static void WriteCookie<T>(this HttpContext context, string key, T obj, string _Path = "/", long _time = 31536000000)
        {
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
            var a = SerializeToBase64String(obj);
            context
                .Response
                .Cookies
                .Append(key, a, new CookieOptions()
                {
                    Expires = DateTime.Now.AddMilliseconds(_time),
                    SameSite = SameSiteMode.Strict,
                    Secure = true,
                    HttpOnly = true,
                    Domain = h,
                    Path = _Path
                });
        }

        public static string GetQueryString(this HttpContext httpContext, string Param, bool XSS = true)
        {
            string r = "";
            try
            {
                r = httpContext.Request.Query[Param];
            }
            catch
            {
            }
            if (String.IsNullOrEmpty(r)) r = "";

            if (XSS)
                return r.ReplaceXSS();
            else
                return r;
        }

        public static string GetFormValue(this HttpContext httpContext, string Param, bool XSS = true)
        {
            string r = "";
            try
            {
                r = httpContext.Request.Form[Param];
            }
            catch
            {
            }
            if (String.IsNullOrEmpty(r)) r = "";

            if (XSS)
                return r.ReplaceXSS();
            else
                return r;
        }
        #endregion

        #region WebUtility
        public static string ReplaceXSS(this string s)
        {
            s = s.UrlDecode().Trim();
            s = s.Replace("??", "?")
                    .Replace("'", "’")
                    .Replace("\"", "’’")
                    .Replace("%27", "’")
                    .Replace("%22", "")
                    .Replace("\\\\", "")
                    .Replace("\\", "")
                    .Replace("(", "")
                    .Replace("[", "")
                    .Replace("]", "")
                    .Replace("{", "")
                    .Replace("$", "")
                    .Replace("%24", "")
                    .Replace("%25", "")
                    .Replace("%23", "")
                    .Replace("javascript:", "")
                    .Replace("%", "")
                    .Replace("#", "")
                    .Replace("--", "-")
                    .Replace(")", "")
                    .Replace("<", "«")
                    .Replace(">", "»")
                    ;

            return s;
        }

        public static bool IsEncode(this string s, bool IsUrlEncode = true)
        {
            if (IsUrlEncode)
                return (System.Net.WebUtility.UrlDecode(s) != s);
            else
                return (System.Net.WebUtility.HtmlDecode(s) != s);
        }

        public static string UrlEncode(this string s)
        {
            var r = s;
            if (!IsEncode(r)) r = System.Net.WebUtility.UrlEncode(r);
            return r;
        }

        public static string UrlDecode(this string s)
        {
            var r = s;
            while (IsEncode(r)) r = System.Net.WebUtility.UrlDecode(r);
            return r;
        }

        public static string HtmlEncode(this string s)
        {
            var r = s;
            if (!IsEncode(r, false)) r = System.Net.WebUtility.HtmlEncode(r);
            return r;
        }

        public static string HtmlDecode(this string s)
        {
            var r = s;
            while (!IsEncode(r, false)) r = System.Net.WebUtility.HtmlDecode(r);
            return r;
        }
        #endregion

        #region Paygate

        public static string MerchantUrl(this HttpContext context, string MerchantPath)
        {
            return context.Request.Scheme + "://" + context.Request.Host + MerchantPath;
        }

        public static string MerchantUrl(this HttpContext context)
        {
            return context.Request.Scheme + "://" + context.Request.Host;
        }

        // vpc_ticket
        public static string GetRemoteIp(this HttpContext context)
        {
            return context.Connection.RemoteIpAddress.ToString();
        }

        public static string CreatePay(this VPCRequest conn, HttpContext context, PaygateInfo paygateInfo, PaymentIn t)
        {
            // Khoi tao lop thu vien va gan gia tri cac tham so gui sang cong thanh toan
            conn.SetSecureSecret(paygateInfo._SECURE_SECRET);
            // Add the Digital Order Fields for the functionality you wish to use
            // Core Transaction Fields
            conn.AddDigitalOrderField("AgainLink", context.MerchantUrl());
            conn.AddDigitalOrderField("Title", paygateInfo._Title);
            conn.AddDigitalOrderField("vpc_Locale", paygateInfo._PaygateLanguage);//Chon ngon ngu hien thi tren cong thanh toan (vn/en)
            conn.AddDigitalOrderField("vpc_Version", paygateInfo._PaygateVersion.ToString());
            conn.AddDigitalOrderField("vpc_Command", paygateInfo._CreatePayCommand);
            conn.AddDigitalOrderField("vpc_Merchant", paygateInfo._MerchantID);
            conn.AddDigitalOrderField("vpc_AccessCode", paygateInfo._MerchantAccessCode);
            conn.AddDigitalOrderField("vpc_MerchTxnRef", t.vpc_MerchTxnRef);//new Random().NextDouble().ToString()
            conn.AddDigitalOrderField("vpc_OrderInfo", t.vpc_OrderInfo);
            conn.AddDigitalOrderField("vpc_Amount", t.vpc_Amount + "00");
            conn.AddDigitalOrderField("vpc_ReturnURL", context.MerchantUrl(paygateInfo._MerchantUrl));

            //// Thong tin them ve khach hang. De trong neu khong co thong tin
            //conn.AddDigitalOrderField("vpc_SHIP_Street01", t.vpc_SHIP_Street01);
            //conn.AddDigitalOrderField("vpc_SHIP_Provice", t.vpc_SHIP_Provice);
            //conn.AddDigitalOrderField("vpc_SHIP_City", t.vpc_SHIP_City);
            //conn.AddDigitalOrderField("vpc_SHIP_Country", t.vpc_SHIP_Country);
            //conn.AddDigitalOrderField("vpc_Customer_Phone", t.vpc_Customer_Phone);
            //conn.AddDigitalOrderField("vpc_Customer_Email", t.vpc_Customer_Email);
            //conn.AddDigitalOrderField("vpc_Customer_Id", t.vpc_Customer_Id);
            // Dia chi IP cua khach hang
            conn.AddDigitalOrderField("vpc_TicketNo", context.GetRemoteIp());
            // Chuyen huong trinh duyet sang cong thanh toan
            var url = conn.Create3PartyQueryString();
            return url;
        }

        public static async Task<string> CreateIQuery(this VPCRequest conn, ILogger logger, PaygateInfo paygateInfo, string vpc_MerchTxnRef)
        {
            string queryString = $"vpc_AccessCode={paygateInfo._MerchantAccessCode}&vpc_Command=queryDR&vpc_MerchTxnRef={vpc_MerchTxnRef}&vpc_Merchant={paygateInfo._MerchantID}&vpc_Password={paygateInfo._Password}&vpc_User={paygateInfo._User}&vpc_Version={paygateInfo._PaygateVersion.ToString()}";
            string vpc_SecureHash = conn.CreateSHA256Signature(queryString);
            string url = $"{paygateInfo._IQueryURL}?{queryString}&vpc_SecureHash={vpc_SecureHash}";
            logger.LogInformation($"CreateIQuery/url:{url}");
            //Open URL check
            string result = string.Empty;
            HttpWebRequest httpWebRequest;
            httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "text/html; charset=utf-8";
            httpWebRequest.Method = "GET";

            try
            {
                var httpResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = await streamReader.ReadToEndAsync();
                }
            }
            catch (Exception ex)
            {
                result = "";
                logger.LogError($"CreateIQuery/{url}: {ex.Message}");
            }

            return result;
        }
        #endregion

        #region Object
        public static bool SetValue<T>(this T obj, HttpContext context) where T : class
        {
            try
            {
                foreach (var propertyInfo in obj.GetType()
                                .GetProperties(
                                        BindingFlags.Public
                                        | BindingFlags.Instance))
                {
                    propertyInfo.SetValue(obj, context.GetQueryString(propertyInfo.Name));
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool SetValue<T>(this T obj, string property, object value) where T : class
        {
            try
            {
                obj.GetType().GetProperty(property).SetValue(obj, value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static object GetValue<T>(this T obj, string property) where T : class
        {
            try
            {
                obj.GetType().GetProperty(property).GetValue(obj);
                return true;
            }
            catch
            {
                return default;
            }
        }
        #endregion

        #region string Vietnamese
        private static readonly string[] VietnameseSigns = new string[]
        {

            "aAeEoOuUiIdDyY",

            "áàạảãâấầậẩẫăắằặẳẵ",

            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

            "éèẹẻẽêếềệểễ",

            "ÉÈẸẺẼÊẾỀỆỂỄ",

            "óòọỏõôốồộổỗơớờợởỡ",

            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

            "úùụủũưứừựửữ",

            "ÚÙỤỦŨƯỨỪỰỬỮ",

            "íìịỉĩ",

            "ÍÌỊỈĨ",

            "đ",

            "Đ",

            "ýỳỵỷỹ",

            "ÝỲỴỶỸ"
        };
        public static string RemoveSign4VietnameseString(this string str)
        {
            if (String.IsNullOrEmpty(str)) return "";
            str = str
                .ToLower()
                .Trim()
                .Replace("\t", "-")
                .Replace("\n", "-")
                .Replace(" ", "-")
                .Replace("--", "-");

            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }
            str = Regex.Replace(str, "[^a-zA-Z0-9-]", String.Empty);
            return str;
        }
        #endregion

        #region Url
        public static string CateProductUrl(this HttpContext context, long Id, bool IsFullUrl = true)
        {
            if (IsFullUrl)
            {
                return context.Request.Scheme + "://" + context.Request.Host + "/Categories/" + Id.ToString();
            }
            else
            {
                return "/Categories/" + Id.ToString();
            }
        }

        public static string ProductUrl(this HttpContext context, long Id, bool IsFullUrl = true)
        {
            if (IsFullUrl)
            {
                return context.Request.Scheme + "://" + context.Request.Host + "/Categories/Details/" + Id.ToString();
            }
            else
            {
                return "/Categories/Details/" + Id.ToString();
            }
        }

        public static string ProductUrl(this HttpContext context, string Name, string Code, bool IsFullUrl = true)
        {
            if (IsFullUrl)
            {
                return $"{context.Request.Scheme}://{context.Request.Host}/{Name.RemoveSign4VietnameseString()}-{Code}";
                // context.Request.Scheme + "://" + context.Request.Host + "/" + Id.ToString();
            }
            else
            {
                return $"/{Name.RemoveSign4VietnameseString()}-{Code}";
                // "/Categories/Details/" + Id.ToString();
            }
        }

        public static string CateNewsUrl(this HttpContext context, long Id, bool IsFullUrl = true)
        {
            if (IsFullUrl)
            {
                return context.Request.Scheme + "://" + context.Request.Host + "/News/" + Id.ToString();
            }
            else
            {
                return "/News/" + Id.ToString();
            }
        }

        public static string NewsUrl(this HttpContext context, long Id, bool IsFullUrl = true)
        {
            if (IsFullUrl)
            {
                return $"{context.Request.Scheme}://{context.Request.Host}/News/Details/{Id.ToString()}";
            }
            else
            {
                return $"/News/Details/{Id.ToString()}";
            }
        }

        public static string NewsUrl(this HttpContext context, string Name, long Id, bool IsFullUrl = true)
        {
            if (IsFullUrl)
            {
                return $"{context.Request.Scheme}://{context.Request.Host}/{Name.RemoveSign4VietnameseString()}-{Id}";
                // context.Request.Scheme + "://" + context.Request.Host + "/News/Details/" + Id.ToString();
            }
            else
            {
                return $"/{Name.RemoveSign4VietnameseString()}-{Id}";
                //"/News/Details/" + Id.ToString();
            }
        }

        public static string FAQUrl(this HttpContext context, long Id, bool IsFullUrl = true)
        {
            if (IsFullUrl)
            {
                return context.Request.Scheme + "://" + context.Request.Host + "/FAQDetails/" + Id.ToString();
            }
            else
            {
                return "/FAQDetails/" + Id.ToString();
            }
        }

        public static string ServiceUrl(this HttpContext context, long Id, bool IsFullUrl = true)
        {
            if (IsFullUrl)
            {
                return context.Request.Scheme + "://" + context.Request.Host + "/News/Details/" + Id.ToString();
            }
            else
            {
                return "/News/Details/" + Id.ToString();
            }
        }

        #endregion

        #region Get list/path by cate
        public static List<string>
            GetPath(IEnumerable<NewsCategories> CateList, long CateId)
        {
            var a = new List<string>();
            if (CateId < 1)
            {
                return a;
            }
            else
            {
                var p1 = CateList.FirstOrDefault(u => u.Id == CateId);
                if (p1 != null)
                {
                    if (p1.ParentId.HasValue)
                    {
                        if (p1.ParentId.Value > 0)
                        {
                            var a1 = CateList.FirstOrDefault(u => u.Id == p1.ParentId);
                            if (a1 != null)
                            {
                                if (a1.ParentId.HasValue)
                                {
                                    if (a1.ParentId.Value > 0)
                                    {
                                        var a2 = CateList.FirstOrDefault(u => u.Id == a1.ParentId);
                                        if (a2 != null)
                                        {
                                            if (a2.ParentId.HasValue)
                                            {
                                                if (a2.ParentId.Value > 0)
                                                {
                                                    var a3 = CateList.FirstOrDefault(u => u.Id == a2.ParentId);
                                                    if (a3 != null)
                                                    {
                                                        if (a3.ParentId.HasValue)
                                                        {
                                                            if (a3.ParentId.Value > 0)
                                                            {
                                                                var a4 = CateList.FirstOrDefault(u => u.Id == a3.ParentId);
                                                                if (a4 != null)
                                                                {
                                                                    if (a4.ParentId.HasValue)
                                                                    {
                                                                        if (a4.ParentId.Value > 0)
                                                                        {
                                                                            var a5 = CateList.FirstOrDefault(u => u.Id == a4.ParentId);
                                                                            if (a5 != null)
                                                                            {
                                                                                a.Add(a5.Name);
                                                                            }
                                                                        }
                                                                    }
                                                                    a.Add(a4.Name);
                                                                }
                                                            }
                                                        }
                                                        a.Add(a3.Name);
                                                    }
                                                }
                                            }
                                            a.Add(a2.Name);
                                        }
                                    }
                                }
                                a.Add(a1.Name);
                            }
                        }
                    }
                    a.Add(p1.Name);
                }
            }
            return a;
        }

        public static List<string>
            GetPath(IEnumerable<Categories> CateList, long CateId)
        {
            var a = new List<string>();
            if (CateId < 1)
            {
                return a;
            }
            else
            {
                var p1 = CateList.FirstOrDefault(u => u.Id == CateId);
                if (p1 != null)
                {
                    if (p1.ParentId.HasValue)
                    {
                        if (p1.ParentId.Value > 0)
                        {
                            var a1 = CateList.FirstOrDefault(u => u.Id == p1.ParentId);
                            if (a1 != null)
                            {
                                if (a1.ParentId.HasValue)
                                {
                                    if (a1.ParentId.Value > 0)
                                    {
                                        var a2 = CateList.FirstOrDefault(u => u.Id == a1.ParentId);
                                        if (a2 != null)
                                        {
                                            if (a2.ParentId.HasValue)
                                            {
                                                if (a2.ParentId.Value > 0)
                                                {
                                                    var a3 = CateList.FirstOrDefault(u => u.Id == a2.ParentId);
                                                    if (a3 != null)
                                                    {
                                                        if (a3.ParentId.HasValue)
                                                        {
                                                            if (a3.ParentId.Value > 0)
                                                            {
                                                                var a4 = CateList.FirstOrDefault(u => u.Id == a3.ParentId);
                                                                if (a4 != null)
                                                                {
                                                                    if (a4.ParentId.HasValue)
                                                                    {
                                                                        if (a4.ParentId.Value > 0)
                                                                        {
                                                                            var a5 = CateList.FirstOrDefault(u => u.Id == a4.ParentId);
                                                                            if (a5 != null)
                                                                            {
                                                                                a.Add(a5.Name);
                                                                            }
                                                                        }
                                                                    }
                                                                    a.Add(a4.Name);
                                                                }
                                                            }
                                                        }
                                                        a.Add(a3.Name);
                                                    }
                                                }
                                            }
                                            a.Add(a2.Name);
                                        }
                                    }
                                }
                                a.Add(a1.Name);
                            }
                        }
                    }
                    a.Add(p1.Name);
                }
            }
            return a;
        }

        public static List<long> GetChildList(IEnumerable<NewsCategories> CateList, long CateId)
        {
            var a = new List<long>();
            if (CateId < 1)
            {
                a.AddRange(CateList.Select(u => u.Id));
            }
            else
            {
                a.Add(CateId);
                a.AddRange(CateList.Where(u => u.ParentId == CateId).Select(u => u.Id));
                foreach (var n1 in CateList.Where(u => u.ParentId == CateId).Select(u => u.Id)) // Level 1
                {
                    a.AddRange(CateList.Where(u => u.ParentId == n1).Select(u => u.Id));
                    foreach (var n2 in CateList.Where(u => u.ParentId == n1).Select(u => u.Id)) // Level 2
                    {
                        a.AddRange(CateList.Where(u => u.ParentId == n2).Select(u => u.Id));
                        foreach (var n3 in CateList.Where(u => u.ParentId == n2).Select(u => u.Id)) // Level 3
                        {
                            a.AddRange(CateList.Where(u => u.ParentId == n3).Select(u => u.Id));
                            foreach (var n4 in CateList.Where(u => u.ParentId == n3).Select(u => u.Id)) // Level 4
                            {
                                a.AddRange(CateList.Where(u => u.ParentId == n4).Select(u => u.Id));
                            }
                        }
                    }
                }
            }

            return a;
        }

        public static List<long> GetChildList(IEnumerable<Categories> CateList, long CateId)
        {
            var a = new List<long>();
            if (CateId < 1)
            {
                a.AddRange(CateList.Select(u => u.Id));
            }
            else
            {
                a.Add(CateId);
                a.AddRange(CateList.Where(u => u.ParentId == CateId).Select(u => u.Id));
                foreach (var n1 in CateList.Where(u => u.ParentId == CateId).Select(u => u.Id)) // Level 1
                {
                    a.AddRange(CateList.Where(u => u.ParentId == n1).Select(u => u.Id));
                    foreach (var n2 in CateList.Where(u => u.ParentId == n1).Select(u => u.Id)) // Level 2
                    {
                        a.AddRange(CateList.Where(u => u.ParentId == n2).Select(u => u.Id));
                        foreach (var n3 in CateList.Where(u => u.ParentId == n2).Select(u => u.Id)) // Level 3
                        {
                            a.AddRange(CateList.Where(u => u.ParentId == n3).Select(u => u.Id));
                            foreach (var n4 in CateList.Where(u => u.ParentId == n3).Select(u => u.Id)) // Level 4
                            {
                                a.AddRange(CateList.Where(u => u.ParentId == n4).Select(u => u.Id));
                            }
                        }
                    }
                }
            }

            return a;
        }

        public static Func<Product, object> GetFunc(string Orderby, out bool IsDesc)
        {
            Func<Product, object> a;
            IsDesc = false;
            switch (Orderby)
            {
                case "price":
                case "price-asc":
                    return a = (u => u.Price);
                case "name":
                    return a = (u => u.Code); //(u => u.Name);
                case "price-desc":
                    IsDesc = true;
                    return a = (u => u.Price);
                case "new":
                case "rating":
                case "popularity":
                default:
                    IsDesc = true;
                    return a = (u => u.Id);
            }
        }

        public static IEnumerable<SelectListItem> PaymentMethod()
        {
            var a = new List<SelectListItem>();
            a.Add(new SelectListItem() { Value = "1", Text = "Trả tiền mặt khi nhận hàng" });
            a.Add(new SelectListItem() { Value = "2", Text = "Chuyển khoản ngân hàng" });
            a.Add(new SelectListItem() { Value = "3", Text = "Thanh toán online" });
            return a;
        }
        #endregion

        #region Service Group
        private const string _v1 = "1";
        private const string _v2 = "2";
        private const string _v3 = "3";
        private const string _v4 = "4";
        private const string _v5 = "5";
        private const string _t1 = "Hộ gia đình, hộ kinh doanh cá thể";
        private const string _t2 = "Cơ quan, tổ chức, doanh nghiệp";
        private const string _t3 = "Hộ gia đình, hộ kinh doanh";
        private const string _t4 = "Cơ quan hành chính";
        private const string _t5 = "Doanh nghiệp, Cty sản xuất";

        public static IEnumerable<SelectListItem> GroupServiceList(string v = "")
        {
            var a = new List<SelectListItem>();
            v = $",{v},";
            a.Add(new SelectListItem()
            {
                Value = _v1,
                Text = _t1,
                Selected = (v.IndexOf($",{_v1},") > -1)
            });

            a.Add(new SelectListItem()
            {
                Value = _v2,
                Text = _t2,
                Selected = (v.IndexOf($",{_v2},") > -1)
            });

            a.Add(new SelectListItem()
            {
                Value = _v3,
                Text = _t3,
                Selected = (v.IndexOf($",{_v3},") > -1)
            });

            a.Add(new SelectListItem()
            {
                Value = _v4,
                Text = _t4,
                Selected = (v.IndexOf($",{_v4},") > -1)
            });

            a.Add(new SelectListItem()
            {
                Value = _v5,
                Text = _t5,
                Selected = (v.IndexOf($",{_v5},") > -1)
            });
            return a;
        }

        public static string GroupServiceList(this string v, string _Html = "li")
        {
            v = $",{v},";
            var r = new System.Text.StringBuilder();
            var r1 = "";
            var v1 = (v.IndexOf($",{_v1},") > -1 ? $"{_t1}" : "");
            var v2 = (v.IndexOf($",{_v2},") > -1 ? $"{_t2}" : "");
            var v3 = (v.IndexOf($",{_v3},") > -1 ? $"{_t3}" : "");
            var v4 = (v.IndexOf($",{_v4},") > -1 ? $"{_t4}" : "");
            var v5 = (v.IndexOf($",{_v5},") > -1 ? $"{_t5}" : "");

            switch (_Html)
            {
                case "li":
                    r.Append("<ul class=\"ul\">");
                    if (!String.IsNullOrEmpty(v1)) r.Append($"<li><i class=\"fa fa-check-circle\"></i> {v1}</li>");
                    if (!String.IsNullOrEmpty(v2)) r.Append($"<li><i class=\"fa fa-check-circle\"></i> {v2}</li>");
                    if (!String.IsNullOrEmpty(v3)) r.Append($"<li><i class=\"fa fa-check-circle\"></i> {v3}</li>");
                    if (!String.IsNullOrEmpty(v4)) r.Append($"<li><i class=\"fa fa-check-circle\"></i> {v4}</li>");
                    if (!String.IsNullOrEmpty(v5)) r.Append($"<li><i class=\"fa fa-check-circle\"></i> {v5}</li>");
                    r.Append("</ul>");
                    r1 = r.ToString();
                    r.Clear();
                    r = null;
                    return r1;
                case "br":
                    if (!String.IsNullOrEmpty(v1)) r.Append($"{v1}<br>");
                    if (!String.IsNullOrEmpty(v2)) r.Append($"{v2}<br>");
                    if (!String.IsNullOrEmpty(v3)) r.Append($"{v3}<br>");
                    if (!String.IsNullOrEmpty(v4)) r.Append($"{v4}<br>");
                    if (!String.IsNullOrEmpty(v5)) r.Append($"{v5}<br>");
                    r1 = r.ToString();
                    if (r1.Length > 4) r1.Left(r1.Length - 4);
                    r.Clear();
                    r = null;
                    return r1;
                default:
                    if (!String.IsNullOrEmpty(v1)) r.Append($"{v1};");
                    if (!String.IsNullOrEmpty(v2)) r.Append($"{v2};");
                    if (!String.IsNullOrEmpty(v3)) r.Append($"{v3};");
                    if (!String.IsNullOrEmpty(v4)) r.Append($"{v4};");
                    if (!String.IsNullOrEmpty(v5)) r.Append($"{v5};");
                    r1 = r.ToString();
                    if (r1.Length > 1) r1.Left(r1.Length - 1);
                    r.Clear();
                    r = null;
                    return r1;
            }
        }
        #endregion
    }
}
