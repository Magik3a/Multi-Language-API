using Microsoft.AspNet.Identity;
using Multi_language.Services;
using Multi_Language.MVCClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Multi_Language.MVCClient.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private IProjectsServices projectServices;
        private ILanguagesService langService;
        private IPhrasesContextServices phrsContService;

        public HomeController(IProjectsServices projectServices, ILanguagesService langService, IPhrasesContextServices phrsContService)
        {
            this.projectServices = projectServices;
            this.langService = langService;
            this.phrsContService = phrsContService;
        }

        public ActionResult Index()
        {
            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Index page", "Index description page");
            if (Request.IsAjaxRequest())
                return PartialView();

            var model = new IndexViewModels();
            model.Languages.CurrentCount = langService.GetByActiveProject(UserActiveProject).Count();
            model.Languages.ActiveCount = langService.GetActiveByActiveProject(UserActiveProject).Count();

            model.Contexts.CurrentCount = phrsContService.GetAllByIdProject(UserActiveProject, User.Identity.GetUserId()).Count();
            model.Contexts.Translated = phrsContService.GetTranslatedByIdProject(UserActiveProject, User.Identity.GetUserId()).Count();

            model.Projects.ProjectCount = projectServices.GetForUser(User.Identity.GetUserId()).Count();

            return View(model);
        }

        public ActionResult About()
        {
            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "About page", "About description page");
            if (Request.IsAjaxRequest())
                return PartialView();

            return View();
        }


        public ActionResult Contact()
        {
            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Contact page", "Contact description page");
            if (Request.IsAjaxRequest())
                return PartialView();

            return View();
        }
    }
}