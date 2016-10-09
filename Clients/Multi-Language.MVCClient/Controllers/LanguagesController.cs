using AutoMapper;
using AutoMapper.QueryableExtensions;
using Multi_language.Services;
using Multi_Language.MVCClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Multi_Language.MVCClient.Controllers
{
    public class LanguagesController : BaseController
    {
        private ILanguagesService languagesService;

        public LanguagesController(ILanguagesService languagesService)
        {
            this.languagesService = languagesService;
        }

        public ActionResult Index()
        {
            var model = languagesService.GetAll().ProjectTo<LanguagesViewModels>();

            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "All added languages", "You can choose wich language to be active or add new one.");
            if (Request.IsAjaxRequest())
                return PartialView(model);

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Add new language", " ");
            if (Request.IsAjaxRequest())
                return PartialView();

            return View();
        }

        [HttpPost]
        public ActionResult Create(LanguagesViewModels model)
        {
            if (!ModelState.IsValid)
            {
                SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Add new language", "You have some validation errors.");
                if (Request.IsAjaxRequest())
                    return PartialView(model);

                return View(model);
            }
            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "All added languages", "New Language is created successfully.");

            // TODO Make some data thing here
            if (Request.IsAjaxRequest())
                return PartialView("Index", languagesService.GetAll().ProjectTo<LanguagesViewModels>());

            return View("Index", languagesService.GetAll().ProjectTo<LanguagesViewModels>());
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Edit language", "Error. Go back to list and choose language.");
                return View(new LanguagesViewModels());
            }
            var idLang = id ?? 0;
            var model = Mapper.Map<LanguagesViewModels>(languagesService.GetById(idLang).FirstOrDefault());

            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Edit language", " ");
            if (Request.IsAjaxRequest())
                return PartialView(model);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(LanguagesViewModels model)
        {
            if (!ModelState.IsValid)
            {
                SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Edit", "You have some validation errors.");
                if (Request.IsAjaxRequest())
                    return PartialView(model);

                return View(model);
            }
            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "All added languages", "Language is edited successfully.");

            // TODO Make some data thing here
            if (Request.IsAjaxRequest())
                return PartialView("Index", languagesService.GetAll().ProjectTo<LanguagesViewModels>());

            return View("Index", languagesService.GetAll().ProjectTo<LanguagesViewModels>());
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Details for language", "Error. Go back to list and choose language.");
                return View(new LanguagesViewModels());
            }
            var idLang = id ?? 0;
            var model = Mapper.Map<LanguagesViewModels>(languagesService.GetById(idLang).FirstOrDefault());

            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Details for language", " ");
            if (Request.IsAjaxRequest())
                return PartialView(model);

            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Delete language", "Error. Go back to list and choose language.");
                return View(new LanguagesViewModels());
            }
            var idLang = id ?? 0;
            var model = Mapper.Map<LanguagesViewModels>(languagesService.GetById(idLang).FirstOrDefault());

            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Delete language", "Alert! All contexts and resources associate with the language will be deleted to!");
            if (Request.IsAjaxRequest())
                return PartialView(model);

            return View(model);
        }

    }
}