using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Multi_Language.MVCClient.Controllers
{
    public class ContextsController : BaseController
    {
        // GET: Contexts
        public ActionResult Index()
        {
            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "All key contexts", "Add new short context and then you can add translations to him.");
            if (Request.IsAjaxRequest())
                return PartialView();

            return View();
        }
    }
}