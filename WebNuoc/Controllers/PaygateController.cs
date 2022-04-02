using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Paygate.OnePay;
using WebNuoc.Helpers;
using WebNuoc.Services.Interfaces;

namespace WebNuoc.Controllers
{
    [SecurityHeaders]
    public class PaygateController : Controller
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger<PaygateController> _logger;
        private readonly IAllService _Service;
        private readonly IStringLocalizer<PaygateController> _localizer;
        private readonly IEmailSender _emailSender;
        private readonly IVPCRequest conn;
        private readonly IPaygateInfo paygateInfo;

        //public VPCRequest conn
        //{
        //    get
        //    {
        //        return _conn = _conn ?? new VPCRequest(Tools._PaygateURL);
        //    }
        //}
        //private VPCRequest _conn;

        public PaygateController(IVPCRequest conn, IPaygateInfo paygateInfo, IEmailSender _emailSender, IDistributedCache _cache, ILogger<PaygateController> _logger, IAllService _Service, IStringLocalizer<PaygateController> _localizer)
        {
            this._logger = _logger;
            this._Service = _Service;
            this._localizer = _localizer;
            this._cache = _cache;
            this._emailSender = _emailSender;
            this.paygateInfo = paygateInfo;
            this.conn = conn;
        }

        //[HttpGet]
        //public IActionResult Index()
        //{
        //    PaymentIn t = new PaymentIn();
        //    t.SetValue(HttpContext);
        //    var url = conn.CreatePay(HttpContext, paygateInfo, t);
        //    return Redirect(url);
        //}

        public IActionResult PayResponse()
        {
            return View();
        }

        public IActionResult IPN()
        {
            return View();
        }
    }
}
