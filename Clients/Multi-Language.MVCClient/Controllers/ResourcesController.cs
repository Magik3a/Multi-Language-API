using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Multi_Language.MVCClient.Controllers
{
    public class ResourcesController : BaseController
    {
        // GET: Resources
        public ActionResult Index()
        {
            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "All translated resources", "Choose context and add translation.");
            if (Request.IsAjaxRequest())
                return PartialView();

            return View();
        }
    }
}