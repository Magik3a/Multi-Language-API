using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Multi_Language.MVCClient.Controllers
{
    public class BaseController : Controller
    {
        public void SetViewBagsAndHeaders(bool isAjax, string ContentHeader, string ContentDescription)
        {
            if (isAjax)
            {
                Response.Headers["ContentHeader"] = ContentHeader;
                Response.Headers["ContentDescription"] = ContentDescription;
            }

            ViewBag.ContentHeader = ContentHeader;
            ViewBag.ContentDescription = ContentDescription;
        }

    }
}