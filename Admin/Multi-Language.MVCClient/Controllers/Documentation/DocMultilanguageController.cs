using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Multi_language.Common.Helpers;
using Multi_Language.MVCClient.Enums;

namespace Multi_Language.MVCClient.Controllers.Documentation
{
    [RoutePrefix("")]
    public class DocMultilanguageController : BaseController
    {
        //TODO Use variable for RoutePrefix
        private readonly string prefix = EnumHelper<EControllersPrefix>.GetDisplayValue(EControllersPrefix.DocMultilanguage);

        // GET: DocMultilanguage
        [Route("GettingStarted")]

        public ActionResult GettingStarted()
        {
            SetViewBagsAndHeaders(Request.IsAjaxRequest(), prefix, "Multi-Language - Getting Started");
            if (Request.IsAjaxRequest())
                return PartialView();

            return View();
        }
        
    }
}