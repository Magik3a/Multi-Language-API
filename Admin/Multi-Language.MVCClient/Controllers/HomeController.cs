using Microsoft.AspNet.Identity;
using Multi_language.Services;
using Multi_Language.MVCClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Multi_language.ApiHelper;
using Multi_Language.MVCClient.Attributes;

namespace Multi_Language.MVCClient.Controllers
{
    [Authorize]
    [Authentication]
    public class HomeController : BaseController
    {
        private IProjectsServices projectServices;
        private ILanguagesService langService;
        private IPhrasesContextServices phrsContService;
        private readonly ITokenContainer tokenContainer;

        public HomeController(IProjectsServices projectServices, ILanguagesService langService, IPhrasesContextServices phrsContService, ITokenContainer tokenContainer)
        {
            this.projectServices = projectServices;
            this.langService = langService;
            this.phrsContService = phrsContService;
            this.tokenContainer = tokenContainer;
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
            model.BearerToken = tokenContainer.ApiToken?.ToString();
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