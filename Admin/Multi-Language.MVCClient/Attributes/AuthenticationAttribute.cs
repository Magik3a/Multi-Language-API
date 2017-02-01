using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Multi_language.ApiHelper;
using Multi_Language.MVCClient.ApiInfrastructure;

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
                filterContext.HttpContext.Response.RedirectToRoute(RouteConfig.LoginRouteName);
            }
        }
    }
}