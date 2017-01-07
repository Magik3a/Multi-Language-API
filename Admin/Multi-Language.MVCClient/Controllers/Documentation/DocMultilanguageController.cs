using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Multi_language.Common.Helpers;
using Multi_language.Services;
using Multi_Language.MVCClient.Enums;

namespace Multi_Language.MVCClient.Controllers.Documentation
{
    [RoutePrefix("")]
    public class DocMultilanguageController : BaseController
    {

        private readonly IPhrasesContextServices contextServices;
        private readonly ILanguagesService languagesService;

        //TODO Use variable for RoutePrefix
        private readonly string prefix = EnumHelper<EControllersPrefix>.GetDisplayValue(EControllersPrefix.DocMultilanguage);

        public DocMultilanguageController(IPhrasesContextServices contextServices, ILanguagesService languagesService)
        {
            this.contextServices = contextServices;
            this.languagesService = languagesService;
        }
        // GET: DocMultilanguage
        [Route("GettingStarted")]

        public ActionResult GettingStarted()
        {
            ViewBag.IdLanguage = new SelectList(
              languagesService.GetActiveByActiveProject(UserActiveProject), "IdLanguage", "Name");
            ViewBag.IdPhraseContext = new SelectList(
                contextServices.GetAllByIdProject(UserActiveProject, User.Identity.GetUserId()), "IdPhraseContext", "Context");

            SetViewBagsAndHeaders(Request.IsAjaxRequest(), prefix, "Multi-Language - Getting Started");
            if (Request.IsAjaxRequest())
                return PartialView();

            return View();
        }

    }
}