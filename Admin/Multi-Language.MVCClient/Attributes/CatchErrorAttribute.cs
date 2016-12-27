using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Multi_Language.MVCClient.Controllers;

namespace Multi_Language.MVCClient.Attributes
{
    public class CatchErrorAttribute : HandleErrorAttribute
    {
        private bool IsAjax(ExceptionContext filterContext)
        {
            return filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }
        public override void OnException(ExceptionContext filterContext)
        {
            Exception ex = filterContext.Exception;
            filterContext.ExceptionHandled = true;
            // if the request is AJAX return JSON else view.


            var currentController = (string)filterContext.RouteData.Values["controller"];
            var currentActionName = (string)filterContext.RouteData.Values["action"];
            if (IsAjax(filterContext))
            {
                //Because its a exception raised after ajax invocation
                //Lets return Json
                filterContext.Result = new JsonResult()
                {
                    Data = filterContext.Exception.Message,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };

                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.Clear();
            }
            else
            {
                var model = new HandleErrorInfo(filterContext.Exception, currentController, currentActionName);
                filterContext.Result = new ViewResult()
                {
                    ViewName = "~/Views/Error/Index.cshtml",
                    ViewData = new ViewDataDictionary(model)
                };
            }

            // Write error logging code here if you wish.
        }
    }
}