using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Multi_Language.MVCClient.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Index page", "Index description page");
            if (Request.IsAjaxRequest())
                return PartialView();

            return View();
        }

        public ActionResult About()
        {
            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "About page", "About description page");
            if (Request.IsAjaxRequest())
                return PartialView();

            return View();
        }


        public ActionResult Contact()
        {
            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Contact page", "Contact description page");
            if (Request.IsAjaxRequest())
                return PartialView();

            return View();
        }
    }
}