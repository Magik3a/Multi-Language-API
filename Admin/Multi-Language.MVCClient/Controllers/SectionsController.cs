using Microsoft.AspNet.Identity;
using Multi_language.Services;
using Multi_Language.MVCClient.Models.SectionsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Multi_language.ApiHelper;

namespace Multi_Language.MVCClient.Controllers
{
    public class SectionsController : BaseController
    {
        private IProjectsServices projectServices;
        private ILanguagesService langService;
        private IPhrasesContextServices phrsContService;
        private readonly ITokenContainer tokenContainer;

        public SectionsController(
            IProjectsServices projectServices,
            ILanguagesService langService,
            IPhrasesContextServices phrsContService,
            ITokenContainer tokenContainer)
        {
            this.projectServices = projectServices;
            this.langService = langService;
            this.phrsContService = phrsContService;
            this.tokenContainer = tokenContainer;
        }

        public async Task<ActionResult> ChangeActiveProject(int id)
        {
            var model = new ProjectBoxViewModel();
            var activeProject = int.Parse(User.Identity.GetActiveProject());

            ViewBag.AllProjects = true;
            model.ProjectName = projectServices.GetById(id).ProjectName;

            var user = UserManager.FindById(User.Identity.GetUserId());

            user.ActiveProject = id;

            IdentityResult result = await UserManager.UpdateAsync(user);

            var some = User.Identity.GetActiveProject();
            Response.Headers["ProjectIsChanged"] = id.ToString();
            model.IdProject = UserActiveProject;
            return PartialView("LayoutPartials/ProjectSmallBox", model);
        }

        public async Task<ActionResult> ProjectBoxDropDowns(int? id)
        {
            var allProjects = new SelectList(projectServices.GetForUser(User.Identity.GetUserId()), "IdProject", "ProjectName", User.Identity.GetActiveProject());

            return PartialView("LayoutPartials/ProjectBoxDropDowns", new ProjectsBoxDropDownsViewModels() {
                ProjectsDropDowns = allProjects
            });
        }
        public async Task<ActionResult> ProjectBox(int? id)
        {
            var model = new ProjectBoxViewModel();
            var activeProject = int.Parse(User.Identity.GetActiveProject());
            var userId = User.Identity.GetUserId();
            if(!projectServices.GetForUser(userId).Any())
            {
                return PartialView("LayoutPartials/ProjectSmallBox", model);
            }

            if (id.HasValue && id != 0)
            {
                ViewBag.AllProjects = true;
                model.ProjectName = projectServices.GetById(id??0).ProjectName;
                model.IdProject = UserActiveProject;

                return PartialView("LayoutPartials/ProjectSmallBox", model);

            }
            if (id == 0)
            {
                ViewBag.AllProjects = true;
                return PartialView("LayoutPartials/ProjectSmallBox", model);
            }
            if (activeProject != 0)
            {
                ViewBag.AllProjects = true;
                model.ProjectName = projectServices.GetById(activeProject).ProjectName;
                model.IdProject = UserActiveProject;

                return PartialView("LayoutPartials/ProjectSmallBox", model);

            }
            ViewBag.AllProjects = true;

            return PartialView("LayoutPartials/ProjectSmallBox", model);

        }
        // GET: Sections
        public async Task<ActionResult> FirstRow(string id)
        {
            if (id == "Home" || id == "")
            {
                var model = new HomeFirstRowSectionViewModel();
                model.BearerToken = tokenContainer?.ApiToken.ToString();
                return PartialView("HomeFirstRowSection", model);
            }
            if (id == "Resources")
            {
                var model = new ResourcesFirstRowSectionViewModel();
                model.Languages.CurrentCount = langService.GetByActiveProject(UserActiveProject).Count();
                model.Languages.ActiveCount = langService.GetActiveByActiveProject(UserActiveProject).Count();

                model.Contexts.CurrentCount = phrsContService.GetAllByIdProject(UserActiveProject, User.Identity.GetUserId()).Count();
                model.Contexts.Translated = phrsContService.GetTranslatedByIdProject(UserActiveProject, User.Identity.GetUserId()).Count();

                model.Projects.ProjectCount = projectServices.GetForUser(User.Identity.GetUserId()).Count();
                return PartialView("ResourcesFirstRowSection", model);
            }

            if (id == "Contexts")
            {
                var model = new ContextsFirstRowSectionViewModel();
                model.Languages.CurrentCount = langService.GetByActiveProject(UserActiveProject).Count();
                model.Languages.ActiveCount = langService.GetActiveByActiveProject(UserActiveProject).Count();

                model.Projects.ProjectCount = projectServices.GetForUser(User.Identity.GetUserId()).Count();

                return PartialView("ContextsFirstRowSection", model);

            }

            if (id == "Languages")
            {
                var model = new CurrentProjectInfoBoxViewModel();
                model.ProjectCount = projectServices.GetForUser(User.Identity.GetUserId()).Count();
                return PartialView("InnerPartials/CurrentProjectInfoBox", model);

            }
            return PartialView("Default");
        }
    }
}