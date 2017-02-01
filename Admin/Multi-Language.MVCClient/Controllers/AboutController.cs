using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Multi_Language.MVCClient.Attributes;
using Multi_Language.MVCClient.Models;

namespace Multi_Language.MVCClient.Controllers
{
    public class AboutController : BaseController
    {
        // GET: About
        [AllowAnonymous]
        public ActionResult Index()
        {
            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "About our project", "Send us some good news!");
            if (Request.IsAjaxRequest())
                return PartialView();

            return View();
        }

        [AllowAnonymous]
        public ActionResult SendEmail(SendEmailViewModel model, string id = "")
        {
            if (!ModelState.IsValid)
            {
                return PartialView("SendEmailPartial", model);
            }
            if (string.IsNullOrWhiteSpace(id))
            {
                ViewBag.IsSuccess = true;
                return PartialView("SendEmailPartial");
            }
            else
            {
                ViewBag.IsSuccess = "Something really bad happened here";
                return PartialView("SendEmailPartial");
            }
        }
    }
}