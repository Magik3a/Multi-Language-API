using Microsoft.AspNet.Identity;
using Multi_language.Services;
using Multi_Language.MVCClient.Models.SectionsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Multi_Language.MVCClient.Controllers
{
    public class SectionsController : BaseController
    {
        private IProjectsServices projectServices;
        public SectionsController(IProjectsServices projectServices)
        {
            this.projectServices = projectServices;
        }

        public async Task<ActionResult> ChangeActiveProject(int id)
        {
            var model = new ProjectBoxViewModel();
            var activeProject = int.Parse(User.Identity.GetActiveProject());

            ViewBag.AllProjects = true;
            model.ProjectName = projectServices.GetById(id).ProjectName;

            var user = UserManager.FindById(User.Identity.GetUserId());

            user.ActiveProject = id;

            IdentityResult result = UserManager.Update(user);

            Response.Headers["ProjectIsChanged"] = id.ToString();

            return PartialView("LayoutPartials/ProjectSmallBox", model);
        }

        public async Task<ActionResult> ProjectBoxDropDowns(int? id)
        {
           var  allProjects = new SelectList(projectServices.GetAll(), "IdProject", "ProjectName", User.Identity.GetActiveProject());

            return PartialView("LayoutPartials/ProjectBoxDropDowns", new ProjectsBoxDropDownsViewModels() {
                ProjectsDropDowns = allProjects
            });
        }
        public async Task<ActionResult> ProjectBox(int? id)
        {
            var model = new ProjectBoxViewModel();
            var activeProject = int.Parse(User.Identity.GetActiveProject());
            var userId = User.Identity.GetUserId();
            if(projectServices.GetAll().Where(p => p.UserId == userId).Count() == 0)
            {
                return PartialView("LayoutPartials/ProjectSmallBox", model);
            }

            if (id.HasValue && id != 0)
            {
                ViewBag.AllProjects = new SelectList(projectServices.GetAll(), "IdProject", "ProjectName", id??0);
                model.ProjectName = projectServices.GetById(id??0).ProjectName;
                return PartialView("LayoutPartials/ProjectSmallBox", model);

            }
            if (id == 0)
            {
                ViewBag.AllProjects = new SelectList(projectServices.GetAll(), "IdProject", "ProjectName");
                return PartialView("LayoutPartials/ProjectSmallBox", model);
            }
            if (activeProject != 0)
            {
                ViewBag.AllProjects = new SelectList(projectServices.GetAll(), "IdProject", "ProjectName", activeProject);
                model.ProjectName = projectServices.GetById(activeProject).ProjectName;
                return PartialView("LayoutPartials/ProjectSmallBox", model);

            }
            ViewBag.AllProjects = new SelectList(projectServices.GetAll(), "IdProject", "ProjectName");

            return PartialView("LayoutPartials/ProjectSmallBox", model);

        }
        // GET: Sections
        public async Task<ActionResult> FirstRow(string id)
        {
            if (id == "Resources")
            {
                var model = new ResourcesFirstRowSectionViewModel();
                model.Languages.CurrentCount = 9;
                model.Languages.ActiveCount = 3;

                model.Contexts.CurrentCount = 40;
                model.Contexts.Translated = 25;
                return PartialView("ResourcesFirstRowSection", model);
            }

            if (id == "Contexts")
            {
                var model = new ContextsFirstRowSectionViewModel();
                model.Languages.CurrentCount = 9;
                model.Languages.ActiveCount = 3;

                return PartialView("ContextsFirstRowSection", model);

            }
            return PartialView("Default");
        }
    }
}