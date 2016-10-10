using System;
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

namespace Multi_Language.MVCClient.Controllers
{
    public class ContextsController : BaseController
    {
        private IPhrasesContextServices contextServices;
        public ContextsController(IPhrasesContextServices contextServices)
        {
            this.contextServices = contextServices;
        }
        // GET: Contexts
        public ActionResult Index()
        {
            var model = contextServices.GetAll().ProjectTo<PhrasesContextViewModel>();

            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "All added contexts", "Add context and then you can add translations.");
            if (Request.IsAjaxRequest())
                return PartialView(model);

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Add new context", "Short description");
            if (Request.IsAjaxRequest())
                return PartialView();

            return View();
        }

        [HttpPost]
        public ActionResult Create(PhrasesContextViewModel model)
        {
            if (!ModelState.IsValid)
            {
                SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Add new context", "You have some validation errors.");
                if (Request.IsAjaxRequest())
                    return PartialView(model);

                return View(model);
            }
            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "All added context", "New context is created successfully.");

            model.DateChanged = DateTime.Now;
            model.DateCreated = DateTime.Now;
            model.UserId = User.Identity.GetUserId();
            contextServices.Add(Mapper.Map<PhrasesContext>(model));

            if (Request.IsAjaxRequest())
                return PartialView("Index", contextServices.GetAll().ProjectTo<PhrasesContextViewModel>());

            return View("Index", contextServices.GetAll().ProjectTo<PhrasesContextViewModel>());
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Edit context", "Error. Go back to list and choose context.");
                return View(new PhrasesContextViewModel());
            }
            var idLang = id ?? 0;
            var model = Mapper.Map<PhrasesContextViewModel>(contextServices.GetById(idLang));

            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Edit context", " ");
            if (Request.IsAjaxRequest())
                return PartialView(model);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(PhrasesContextViewModel model)
        {
            if (!ModelState.IsValid)
            {
                SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Edit", "You have some validation errors.");
                if (Request.IsAjaxRequest())
                    return PartialView(model);

                return View(model);
            }
            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "All added context", "Context is edited successfully.");

            model.DateChanged = DateTime.Now;
            model.UserId = User.Identity.GetUserId();

            contextServices.Update(Mapper.Map<PhrasesContext>(model));

            if (Request.IsAjaxRequest())
                return PartialView("Index", contextServices.GetAll().ProjectTo<PhrasesContextViewModel>());

            return View("Index", contextServices.GetAll().ProjectTo<PhrasesContextViewModel>());
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Details for context", "Error. Go back to list and choose context.");
                return View(new PhrasesContextViewModel());
            }
            var idLang = id ?? 0;
            var model = Mapper.Map<PhrasesContextViewModel>(contextServices.GetById(idLang));

            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Details for context", " ");
            if (Request.IsAjaxRequest())
                return PartialView(model);

            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Delete context", "Error. Go back to list and choose context.");
                return View(new PhrasesContextViewModel());
            }
            var idLang = id ?? 0;
            var model = Mapper.Map<PhrasesContextViewModel>(contextServices.GetById(idLang));

            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Delete context", "Alert! All contexts and resources associate with the context will be deleted to!");
            if (Request.IsAjaxRequest())
                return PartialView(model);

            return View(model);
        }

        // POST: Contexts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            contextServices.Delete(id);
            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "All added context", "Context is deleted successfully.");

            if (Request.IsAjaxRequest())
                return PartialView("Index", contextServices.GetAll().ProjectTo<PhrasesContextViewModel>());

            return View("Index", contextServices.GetAll().ProjectTo<PhrasesContextViewModel>());
        }

    }
}
