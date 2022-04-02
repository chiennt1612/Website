using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;

namespace WebAdmin.Helpers
{
    public class RolesAttribute : AuthorizeAttribute
    {
        public RolesAttribute(params string[] roles)
        {
            Roles = String.Join(",", roles);
        }
    }

    public class CustomizeAuthorize : AuthorizeAttribute, IAuthorizationFilter //AuthorizeAttribute 
    {
        private readonly string _someFilterParameter;

        public CustomizeAuthorize(params string[] roles)
        {
            _someFilterParameter = "," + String.Join(",", roles) + ",";
        }

        private RedirectToRouteResult redirectToRoute(AuthorizationFilterContext context)
        {
            string path = context.HttpContext.Request.Path.ToString().Replace("?", "");
            string query = context.HttpContext.Request.QueryString.ToString().Replace("?", "");
            if (query != "") path = path + "?" + query;
            return new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                            { "action", "Index" },
                            { "controller", "Home" },
                            { "ReturnUrl", path }
                    });
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                // it isn't needed to set unauthorized result 
                // as the base class already requires the user to be authenticated
                // this also makes redirect to a login page work properly
                // context.Result = new UnauthorizedResult();
                context.Result = redirectToRoute(context);
                return;
            }
            else
            {
                var a = context.HttpContext.User.Claims.Where(u => u.Type == "role" && _someFilterParameter.IndexOf("," + u.Value + ",") > -1).ToList();
                if (a.Count < 1)
                {
                    context.Result = redirectToRoute(context);
                    return;
                }
            }

            //// you can also use registered services
            //var someService = context.HttpContext.RequestServices.GetService<ISomeService>();

            //var isAuthorized = someService.IsUserAuthorized(user.Identity.Name, _someFilterParameter);
            //if (!isAuthorized)
            //{
            //    context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
            //    return;
            //}
        }
    }
}
