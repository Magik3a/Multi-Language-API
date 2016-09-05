﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Multi_language.Client.Controllers
{
    public class LanguageController : Controller
    {
        // GET: Language
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ShowLanguages()
        {
            return PartialView();
        }


        public ActionResult AddNewLanguage()
        {
            return PartialView();
        }


        public ActionResult EditLanguage()
        {
            return PartialView();
        }

        public ActionResult DeleteLanguage()
        {
            return PartialView();
        }
    }
}