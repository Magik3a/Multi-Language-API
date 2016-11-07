using Multi_Language.MVCClient.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Multi_Language.MVCClient.Areas.Utilities.Controllers
{
    public class BackupsController : BaseController
    {
        // GET: Utilities/Backups
        public ActionResult Index()
        {
            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Backups page", "");
            if (Request.IsAjaxRequest())
                return PartialView();

            return View();
        }
    }
}