using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Multi_Language.MVCClient.Controllers
{
    public class ProjectsController : BaseController
    {
        // GET: Projects
        public ActionResult Index()
        {
            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "All projects", "Create project and start adding translated texts.");
            if (Request.IsAjaxRequest())
                return PartialView();

            return View();
        }
    }
}