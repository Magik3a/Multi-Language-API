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
    public class ResourcesController : BaseController
    {
        private readonly IPhrasesService phrasesService;
        private readonly IPhrasesContextServices contextServices;
        private readonly ILanguagesService languagesService;


        public ResourcesController(IPhrasesService phrasesService, IPhrasesContextServices contextServices, ILanguagesService languagesService)
        {
            this.phrasesService = phrasesService;
            this.contextServices = contextServices;
            this.languagesService = languagesService;
        }
        // GET: Resources
        public ActionResult Index()
        {
            var model = phrasesService.GetAll().ProjectTo<ResourcesViewModels>();

            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "All added resources", " ");
            if (Request.IsAjaxRequest())
                return PartialView(model);

            return View(model);
        }

        // GET: Resources/Details/5
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Details for resource", "Error. Go back to list and choose resource.");
                return View(new ResourcesViewModels());
            }
            var model = Mapper.Map<ResourcesViewModels>(phrasesService.GetById(id??0));

            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Details for resource", " ");
            if (Request.IsAjaxRequest())
                return PartialView(model);

            return View(model);
        }

        // GET: Resources/Create
        public ActionResult Create()
        {
            ViewBag.IdLanguage = new SelectList(languagesService.GetAll(), "IdLanguage", "Name");
            ViewBag.IdPhraseContext = new SelectList(contextServices.GetAll(), "IdPhraseContext", "Context");

            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Add new resource", " ");
            if (Request.IsAjaxRequest())
                return PartialView();

            return View();
        }

        // POST: Resources/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPhrase,IdPhraseContext,IdLanguage,PhraseText")] ResourcesViewModels model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.IdLanguage = new SelectList(languagesService.GetAll(), "IdLanguage", "Name", model.IdLanguage);
                ViewBag.IdPhraseContext = new SelectList(contextServices.GetAll(), "IdPhraseContext", "Context", model.IdPhraseContext);

                SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Add new resource", "You have some validation errors.");
                if (Request.IsAjaxRequest())
                    return PartialView(model);

                return View(model);
            }

            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "All added resources", "New resource is created successfully.");

            model.DateChanged = DateTime.Now;
            model.DateCreated = DateTime.Now;
            model.UserId = User.Identity.GetUserId();

            phrasesService.Add(Mapper.Map<Phrases>(model));

            if (Request.IsAjaxRequest())
                return PartialView("Index", phrasesService.GetAll().ProjectTo<ResourcesViewModels>());

            return View("Index", phrasesService.GetAll().ProjectTo<ResourcesViewModels>());
        }

        // GET: Resources/Edit/5
        public ActionResult Edit(int? id)
        {

            if (!id.HasValue)
            {
                SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Edit resource", "Error. Go back to list and choose resource.");
                ViewBag.IdLanguage = new SelectList(languagesService.GetAll(), "IdLanguage", "Name");
                ViewBag.IdPhraseContext = new SelectList(contextServices.GetAll(), "IdPhraseContext", "Context");

                return View(new ResourcesViewModels());
            }
            var model = Mapper.Map<ResourcesViewModels>(phrasesService.GetById(id??0));
            ViewBag.IdLanguage = new SelectList(languagesService.GetAll(), "IdLanguage", "Name", model.IdLanguage);
            ViewBag.IdPhraseContext = new SelectList(contextServices.GetAll(), "IdPhraseContext", "Context", model.IdPhraseContext);

            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Edit resource", " ");
            if (Request.IsAjaxRequest())
                return PartialView(model);

            return View(model);
        }

        // POST: Resources/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPhrase,IdPhraseContext,IdLanguage,PhraseText,DateCreated")] ResourcesViewModels model)
        {
            ViewBag.IdLanguage = new SelectList(languagesService.GetAll(), "IdLanguage", "Name");
            ViewBag.IdPhraseContext = new SelectList(contextServices.GetAll(), "IdPhraseContext", "Context");

            if (!ModelState.IsValid)
            {
                SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Edit", "You have some validation errors.");
                if (Request.IsAjaxRequest())
                    return PartialView(model);

                return View(model);
            }
            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "All added resources", "Resource is edited successfully.");

            model.DateChanged = DateTime.Now;
            model.UserId = User.Identity.GetUserId();

            phrasesService.Update(Mapper.Map<Phrases>(model));

            if (Request.IsAjaxRequest())
                return PartialView("Index", phrasesService.GetAll().ProjectTo<ResourcesViewModels>());

            return View("Index", phrasesService.GetAll().ProjectTo<ResourcesViewModels>());
        }

        // GET: Resources/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Delete resource", "Error. Go back to list and choose resource.");
                return View(new ResourcesViewModels());
            }
            var model = Mapper.Map<ResourcesViewModels>(phrasesService.GetById(id??0));

            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Delete resource", " ");
            if (Request.IsAjaxRequest())
                return PartialView(model);

            return View(model);
        }

        // POST: Resources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            phrasesService.Delete(id);
            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "All added resources", "Resource is deleted successfully.");

            if (Request.IsAjaxRequest())
                return PartialView("Index", phrasesService.GetAll().ProjectTo<ResourcesViewModels>());

            return View("Index", phrasesService.GetAll().ProjectTo<ResourcesViewModels>());
        }

    }
}
