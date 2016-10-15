using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Multi_Language.MVCClient.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

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