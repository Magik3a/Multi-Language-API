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

            if (Request.IsAjaxRequest())
                return PartialView("Index", languagesService.GetAll().ProjectTo<LanguagesViewModels>());

            return View("Index", languagesService.GetAll().ProjectTo<LanguagesViewModels>());
        }
    }
}