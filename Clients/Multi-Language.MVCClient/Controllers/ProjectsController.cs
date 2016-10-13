﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Multi_language.Data;
using Multi_language.Models;
using Multi_language.Services;
using AutoMapper.QueryableExtensions;
using Multi_Language.MVCClient.Models;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Security.Claims;

namespace Multi_Language.MVCClient.Controllers
{
    public class ProjectsController : BaseController
    {
        private IProjectsServices projectServices;

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

        public ProjectsController(IProjectsServices projectServices)
        {
            this.projectServices = projectServices;
        }
        // GET: Contexts
        public ActionResult Index()
        {
            var model = projectServices.GetAll().ProjectTo<ProjectsViewModel>();

            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "All projects", " ");
            if (Request.IsAjaxRequest())
                return PartialView(model);

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Add new project", " ");
            if (Request.IsAjaxRequest())
                return PartialView();

            return View();
        }

        [HttpPost]
        public ActionResult Create(ProjectsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Add new project", "You have some validation errors.");
                if (Request.IsAjaxRequest())
                    return PartialView(model);

                return View(model);
            }
            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "All added project", "New project is created successfully.");

            var userId = User.Identity.GetUserId();

            model.DateChanged = DateTime.Now;
            model.DateCreated = DateTime.Now;
            model.UserId = userId;
            projectServices.Add(Mapper.Map<Projects>(model));

            if(User.Identity.GetActiveProject() == "0")
            {
                var projectId = projectServices.GetAll().OrderByDescending(m => m.DateCreated).Where(p => p.UserId == userId).FirstOrDefault().IdProject;

                var user = UserManager.FindById(User.Identity.GetUserId());

                user.ActiveProject = projectId;

                IdentityResult result = UserManager.Update(user);
                Response.Headers["FirstProjectAdded"] = "yeaaaa";
            }

            if (Request.IsAjaxRequest())
                return PartialView("Index", projectServices.GetAll().ProjectTo<ProjectsViewModel>());

            return View("Index", projectServices.GetAll().ProjectTo<ProjectsViewModel>());
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Edit project", "Error. Go back to list and choose project.");
                return View(new ProjectsViewModel());
            }
            var model = Mapper.Map<ProjectsViewModel>(projectServices.GetById(id??0));

            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Edit project", " ");
            if (Request.IsAjaxRequest())
                return PartialView(model);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ProjectsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Edit", "You have some validation errors.");
                if (Request.IsAjaxRequest())
                    return PartialView(model);

                return View(model);
            }
            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "All added projects", "Project is edited successfully.");

            model.DateChanged = DateTime.Now;
            model.UserId = User.Identity.GetUserId();

            projectServices.Update(Mapper.Map<Projects>(model));

            if (Request.IsAjaxRequest())
                return PartialView("Index", projectServices.GetAll().ProjectTo<ProjectsViewModel>());

            return View("Index", projectServices.GetAll().ProjectTo<ProjectsViewModel>());
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Details for project", "Error. Go back to list and choose project.");
                return View(new ProjectsViewModel());
            }
            var model = Mapper.Map<ProjectsViewModel>(projectServices.GetById(id??0));

            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Details for project", " ");
            if (Request.IsAjaxRequest())
                return PartialView(model);

            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Delete project", "Error. Go back to list and choose project.");
                return View(new ProjectsViewModel());
            }
            var model = Mapper.Map<ProjectsViewModel>(projectServices.GetById(id??0));

            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Delete project", "Alert! All contexts and resources associate with the project will be deleted to!");
            if (Request.IsAjaxRequest())
                return PartialView(model);

            return View(model);
        }

        // POST: Contexts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            projectServices.Delete(id);
            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "All projects", "Project is deleted successfully.");

            if(User.Identity.GetActiveProject() == id.ToString())
            {
                var user = UserManager.FindById(User.Identity.GetUserId());

                user.ActiveProject = 0;

                IdentityResult result = UserManager.Update(user);
            }
            if (Request.IsAjaxRequest())
                return PartialView("Index", projectServices.GetAll().ProjectTo<ProjectsViewModel>());

            return View("Index", projectServices.GetAll().ProjectTo<ProjectsViewModel>());
        }

    }
}
