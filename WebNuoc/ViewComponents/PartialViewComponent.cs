using EntityFramework.Web.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
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

        public PartialViewComponent(IHttpContextAccessor _httpContextAccessor, ILogger<PartialViewComponent> _logger, IAllService _Service, IStringLocalizer<PartialViewComponent> _localizer)
        {
            this._httpContextAccessor = _httpContextAccessor;
            this._logger = _logger;
            this._Service = _Service;
            this._localizer = _localizer;
            this._logger.LogInformation($"Start PartialViewComponent at: {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}");
            requestCulture = this._httpContextAccessor.HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
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
                    var about = await PartialHomepageAbout();
                    _logger.LogInformation($"Partial {_PartialName} model {about}");
                    return View(_PartialName, about);
                case "_HeaderTop":
                    var a = await PartialHeaderTop();
                    _logger.LogInformation($"Partial {_PartialName} model {a}");
                    return View(_PartialName, a);
                case "_FooterBottom":
                    var b = await PartialHeaderTop();
                    _logger.LogInformation($"Partial {_PartialName} model {b}");
                    return View(_PartialName, b);
                case "_ListServicesMenu":
                    var c = await PartialListServicesMenu();
                    _logger.LogInformation($"Partial {_PartialName} model {c}");
                    return View(_PartialName, c);
                #endregion
                // Home page -------------------------------------------------
                #region Partial Home
                case "_ContactInfo":
                    var contact = await PartialHeaderTop();
                    _logger.LogInformation($"Partial {_PartialName} model {contact.Count()}");
                    return View(_PartialName, contact);
                case "_HomepageBanner":
                    var h1 = await PartialBannerHomeInfo();
                    _logger.LogInformation($"Partial {_PartialName} model {h1.Count()}");
                    return View(_PartialName, h1);
                case "_HomepageProject":
                    var h2 = await PartialHomepageProject();
                    _logger.LogInformation($"Partial {_PartialName} model {h2.Count()}");
                    return View(_PartialName, h2);
                case "_HomepageCAForWater":
                    var h5 = await PartialHomepageCAForWater();
                    _logger.LogInformation($"Partial {_PartialName} model {h5.Count()}");
                    return View(_PartialName, h5);
                case "_HomepageServiceShortList":
                    var h4 = await PartialHomepageServiceShortList();
                    _logger.LogInformation($"Partial {_PartialName} model {h4.Count()}");
                    return View(_PartialName, h4);
                case "_HomepageVideoYoutube":
                    var h6 = await PartialHomepageVideoYoutube();
                    _logger.LogInformation($"Partial {_PartialName} model {h6.Count()}");
                    return View(_PartialName, h6);
                case "_PartialNewsHome":
                    var h3 = await PartialNewHomeInfo();
                    _logger.LogInformation($"Partial {_PartialName} model {h3.Count()}");
                    return View(_PartialName, h3);
                case "_PartialFAQHome":
                    var h7 = await PartialFAQsHomeInfo();
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
