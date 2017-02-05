using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Multi_language.ApiHelper;
using Multi_Language.MVCClient.ApiInfrastructure;
using Ninject.Activation;

namespace Multi_Language.MVCClient.Attributes
{
    public class AuthenticationAttribute : ActionFilterAttribute
    {
        private readonly ITokenContainer tokenContainer;

        private bool IsAjax(ResultExecutedContext filterContext)
        {
            return filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }

        public AuthenticationAttribute()
        {
            tokenContainer = new TokenContainer();
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (tokenContainer.ApiToken == null)
            {
                HttpCookie authCookie = filterContext.HttpContext.Request.Cookies[".AspNet.ApplicationCookie"];
                // filterContext.HttpContext.Response.RedirectToRoute(RouteConfig.LoginRouteName);
            }
        }
    }
}