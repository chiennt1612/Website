using EntityFramework.Web.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Paygate.OnePay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebNuoc.Helpers;
using WebNuoc.Services.Interfaces;

namespace WebNuoc.ViewComponents
{
    public class PartialViewComponent : ViewComponent
    {
        private readonly ILogger<PartialViewComponent> _logger;
        private readonly IAllService _Service;
        private readonly IStringLocalizer<PartialViewComponent> _localizer;
        private readonly string requestCulture;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDistributedCache _cache;
        private IConfiguration _configuration;
        private AboutPage _guide;

        public PartialViewComponent(
            IHttpContextAccessor _httpContextAccessor,
            ILogger<PartialViewComponent> _logger,
            IAllService _Service,
            IStringLocalizer<PartialViewComponent> _localizer,
            IConfiguration _configuration,
            IDistributedCache _cache)
        {
            this._httpContextAccessor = _httpContextAccessor;
            this._logger = _logger;
            this._Service = _Service;
            this._localizer = _localizer;
            this._configuration = _configuration;
            this._cache = _cache;
            this._logger.LogInformation($"Start PartialViewComponent at: {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}");
            requestCulture = this._httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;

            var a = _cache.GetAsync<AboutPage>($"_guide_Data").GetAwaiter().GetResult();
            if (a == null)
            {
                a = this._configuration.GetSection(nameof(AboutPage)).Get<AboutPage>();
                _cache.SetAsync<AboutPage>($"_guide_Data", a).GetAwaiter().GetResult();
            }
            _guide = a;
        }

        public async Task<IViewComponentResult> InvokeAsync(string _PartialName, long? Id, int? page, int? pageSize)
        {
            long _id = (Id.HasValue ? Id.Value : 0);
            int _page = (page.HasValue ? page.Value : 1);
            int _pageSize = (pageSize.HasValue ? pageSize.Value : 7);
            switch (_PartialName)
            {
                #region Partial Layout
                case "_HomepageAbout":
                    var about = await _cache.GetAsync<About>($"{_PartialName}_DATA");
                    if (about == null)
                    {
                        about = await PartialHomepageAbout();
                        await _cache.SetAsync<About>($"{_PartialName}_DATA", about, 43200);
                    }
                    _logger.LogInformation($"Partial {_PartialName} model {about}");
                    return View(_PartialName, about);
                case "_HeaderTop":
                    var a = await _cache.GetAsync<IEnumerable<ParamSetting>>($"{_PartialName}_DATA");
                    if (a == null)
                    {
                        a = await PartialHeaderTop();
                        await _cache.SetAsync<IEnumerable<ParamSetting>>($"{_PartialName}_DATA", a, 518400);
                    }
                    _logger.LogInformation($"Partial {_PartialName} model {a}");
                    return View(_PartialName, a);
                case "_FooterBottom":
                    var b = await _cache.GetAsync<IEnumerable<ParamSetting>>($"{_PartialName}_DATA");
                    if (b == null)
                    {
                        b = await PartialHeaderTop();
                        await _cache.SetAsync<IEnumerable<ParamSetting>>($"{_PartialName}_DATA", b, 518400);
                    }
                    _logger.LogInformation($"Partial {_PartialName} model {b}");
                    return View(_PartialName, b);
                case "_ListServicesMenu":
                    var c = await _cache.GetAsync<IEnumerable<Service>>($"{_PartialName}_DATA");
                    if (c == null)
                    {
                        c = await PartialListServicesMenu();
                        await _cache.SetAsync<IEnumerable<Service>>($"{_PartialName}_DATA", c, 43200);
                    }
                    _logger.LogInformation($"Partial {_PartialName} model {c}");
                    return View(_PartialName, c);
                case "_ListGuidesMenu":
                    var c2 = await _cache.GetAsync<IEnumerable<About>>($"{_PartialName}_DATA");
                    if (c2 == null)
                    {
                        c2 = await PartialListGuidesMenu();
                        await _cache.SetAsync<IEnumerable<About>>($"{_PartialName}_DATA", c2, 518400);
                    }
                    _logger.LogInformation($"Partial {_PartialName} model {c2}");
                    return View(_PartialName, c2);
                #endregion
                // Home page -------------------------------------------------
                #region Partial Home
                case "_ContactInfo":
                    var contact = await _cache.GetAsync<IEnumerable<ParamSetting>>($"{_PartialName}_DATA");
                    if (contact == null)
                    {
                        contact = await PartialHeaderTop();
                        await _cache.SetAsync<IEnumerable<ParamSetting>>($"{_PartialName}_DATA", contact, 518400);
                    }
                    _logger.LogInformation($"Partial {_PartialName} model {contact.Count()}");
                    return View(_PartialName, contact);
                case "_HomepageBanner":
                    var h1 = await _cache.GetAsync<IEnumerable<Adv>>($"{_PartialName}_DATA");
                    if (h1 == null)
                    {
                        h1 = await PartialBannerHomeInfo();
                        await _cache.SetAsync<IEnumerable<Adv>>($"{_PartialName}_DATA", h1, 1440);
                    }
                    _logger.LogInformation($"Partial {_PartialName} model {h1.Count()}");
                    return View(_PartialName, h1);
                case "_HomepageProject":
                    var h2 = await _cache.GetAsync<IEnumerable<Adv>>($"{_PartialName}_DATA");
                    if (h2 == null)
                    {
                        h2 = await PartialHomepageProject();
                        await _cache.SetAsync<IEnumerable<Adv>>($"{_PartialName}_DATA", h2, 1440);
                    }
                    _logger.LogInformation($"Partial {_PartialName} model {h2.Count()}");
                    return View(_PartialName, h2);
                case "_HomepageCAForWater":
                    var h5 = await _cache.GetAsync<IEnumerable<Adv>>($"{_PartialName}_DATA");
                    if (h5 == null)
                    {
                        h5 = await PartialHomepageCAForWater();
                        await _cache.SetAsync<IEnumerable<Adv>>($"{_PartialName}_DATA", h5, 1440);
                    }
                    _logger.LogInformation($"Partial {_PartialName} model {h5.Count()}");
                    return View(_PartialName, h5);
                case "_HomepageServiceShortList":
                    var h4 = await _cache.GetAsync<IEnumerable<Service>>($"{_PartialName}_DATA");
                    if (h4 == null)
                    {
                        h4 = await PartialHomepageServiceShortList();
                        await _cache.SetAsync<IEnumerable<Service>>($"{_PartialName}_DATA", h4, 1440);
                    }
                    _logger.LogInformation($"Partial {_PartialName} model {h4.Count()}");
                    return View(_PartialName, h4);
                case "_HomepageVideoYoutube":
                    var h6 = await _cache.GetAsync<IEnumerable<Adv>>($"{_PartialName}_DATA");
                    if (h6 == null)
                    {
                        h6 = await PartialHomepageVideoYoutube();
                        await _cache.SetAsync<IEnumerable<Adv>>($"{_PartialName}_DATA", h6, 1440);
                    }
                    _logger.LogInformation($"Partial {_PartialName} model {h6.Count()}");
                    return View(_PartialName, h6);
                case "_PartialNewsHome":
                    var h3 = await PartialNewHomeInfo();
                    _logger.LogInformation($"Partial {_PartialName} model {h3.Count()}");
                    return View(_PartialName, h3);
                case "_PartialFAQHome":
                    var h7 = await _cache.GetAsync<IList<FAQ>>($"{_PartialName}_DATA");
                    if (h7 == null)
                    {
                        h7 = await PartialFAQsHomeInfo();
                        await _cache.SetAsync<IList<FAQ>>($"{_PartialName}_DATA", h7, 43200);
                    }
                    _logger.LogInformation($"Partial {_PartialName} model {h7.Count()}");
                    return View(_PartialName, h7);
                // End Home page -------------------------------------------------
                #endregion

                // New page -------------------------------------------------
                #region Partial News
                case "NewHomeLastest":
                    var n1 = await PartialNewHomeInfo();
                    _logger.LogInformation($"Partial {_PartialName} model {n1.Count()}");
                    return View(_PartialName, n1);

                case "NewByCate":
                    var n2 = await PartialNewByCateInfo();
                    _logger.LogInformation($"Partial {_PartialName} model {n2.Count()}");
                    return View(_PartialName, n2);
                case "NewList":
                    var n3 = await PartialNewListInfo(_id);
                    _logger.LogInformation($"Partial {_PartialName} model {n3.Count()}");
                    return View(_PartialName, n3);
                case "NewListByPage":
                    var n4 = await PartialNewListByPageInfo(_id, _page, _pageSize);
                    _logger.LogInformation($"Partial {_PartialName} ");
                    return View(_PartialName, n4);
                    // End New page -------------------------------------------------
                    #endregion
            }
            _logger.LogInformation($"Partial {_PartialName} is not found model Non");
            return View("Index", _PartialName);
        }

        #region Partial Layout
        private async Task<IEnumerable<ParamSetting>> PartialHeaderTop()
        {
            return (await _Service.paramSettingServices.GetAllAsync());
        }

        private async Task<IEnumerable<Service>> PartialListServicesMenu()
        {
            return (await _Service.serviceServices.GetAllAsync()).Where(u => u.Id > 10);
        }
        private async Task<IEnumerable<About>> PartialListGuidesMenu()
        {
            //return (await _Service.aboutServices.GetAllAsync()).Where(u => _guide.GuideID.Contains(u.Id)).OrderBy(u => u.Title);
            return await _Service.aboutServices.GetListAsync(_guide.GuideID);
        }
        #endregion

        #region Partial Function Newpage
        private async Task<IList<Article>> PartialNewHomeInfo()
        {
            Expression<Func<Article, bool>> sqlWhere = u => (true);
            var b = (await _Service.articleServices.GetTopAsync(sqlWhere, 5));

            return b;
        }

        private async Task<IEnumerable<NewsCategories>> PartialNewByCateInfo()
        {
            var b = await _Service.articleServices.CateGetAllAsync();

            return b;
        }

        private async Task<IEnumerable<Article>> PartialNewListInfo(long _id)
        {
            Expression<Func<Article, bool>> sqlWhere = u => (u.CategoryMain == _id || _id == 0);
            var b = (await _Service.articleServices.GetTopAsync(sqlWhere, 3));

            return b;
        }

        private async Task<BaseEntityList<Article>> PartialNewListByPageInfo(long _id, int page, int pageSize)
        {
            Func<Article, object> sqlOrder = s => s.Id;
            Expression<Func<Article, bool>> sqlWhere = u => (u.CategoryMain == _id || _id == 0);
            var a = await _Service.articleServices.GetListAsync(sqlWhere, sqlOrder, true, page, pageSize);
            a.page = page;
            a.pageSize = pageSize;
            a.CategoryId = _id;
            return a;
        }
        #endregion

        #region Partial Function Homepage
        private async Task<IEnumerable<Adv>> PartialBannerHomeInfo()
        {
            var b = (await _Service.advServices.GetAllAsync()).Where(u => u.Pos == 91).OrderByDescending(u => u.StartDate);

            return b;
        }

        private async Task<About> PartialHomepageAbout()
        {
            var b = await _Service.aboutServices.GetByIdAsync(21);

            return b;
        }

        private async Task<IEnumerable<Adv>> PartialHomepageCAForWater()
        {
            var b = (await _Service.advServices.GetAllAsync()).Where(u => u.Pos == 11).OrderByDescending(u => u.StartDate);

            return b;
        }

        private async Task<IEnumerable<Service>> PartialHomepageServiceShortList()
        {
            var b = (await _Service.serviceServices.GetAllAsync());

            return b;
        }

        private async Task<IEnumerable<Adv>> PartialHomepageVideoYoutube()
        {
            var b = (await _Service.advServices.GetAllAsync()).Where(u => u.Pos == 13).OrderByDescending(u => u.StartDate);

            return b;
        }

        private async Task<IEnumerable<Adv>> PartialHomepageProject()
        {
            var b = (await _Service.advServices.GetAllAsync()).Where(u => u.Pos == 12).OrderByDescending(u => u.StartDate);

            return b;
        }

        private async Task<IList<FAQ>> PartialFAQsHomeInfo()
        {
            Expression<Func<FAQ, bool>> sqlWhere = u => (true);
            var b = (await _Service.fAQServices.GetTopAsync(sqlWhere, 5));

            return b;
        }
        #endregion
    }
}
