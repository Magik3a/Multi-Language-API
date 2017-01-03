using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Multi_Language.MVCClient.Controllers.Documentation
{
    [RoutePrefix("Documentation/Multilanguage")]
    public class DocMultilanguageController : BaseController
    {
        // GET: DocMultilanguage
        [Route("GettingStarted")]

        public ActionResult GettingStarted()
        {
            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Documentation", "Multi-Language - Getting Started");
            if (Request.IsAjaxRequest())
                return PartialView();

            return View();
        }
    }
}