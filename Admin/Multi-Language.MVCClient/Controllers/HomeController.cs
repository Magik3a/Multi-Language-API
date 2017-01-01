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
using WebGrease.Css.Extensions;

namespace Multi_Language.MVCClient.Controllers
{
    [Authorize]
    [Authentication]
    public class HomeController : BaseController
    {
        private readonly IProjectsServices projectServices;
        private readonly ILanguagesService langService;
        private readonly IPhrasesContextServices phrsContService;
        private readonly ITokenContainer tokenContainer;
        private readonly ISystemStabilityLoggsService systemStabilityLoggsService;
        public HomeController(
            IProjectsServices projectServices,
            ILanguagesService langService,
            IPhrasesContextServices phrsContService,
            ITokenContainer tokenContainer,
            ISystemStabilityLoggsService systemStabilityLoggsService)
        {
            this.projectServices = projectServices;
            this.langService = langService;
            this.phrsContService = phrsContService;
            this.tokenContainer = tokenContainer;
            this.systemStabilityLoggsService = systemStabilityLoggsService;
        }

        public ActionResult Index()
        {
            var loggsBefore = -24;
            var systemStabilityLogs = systemStabilityLoggsService.GetAllBeforeHours(loggsBefore).ToList().GroupBy(x => x.DateCreated.Value.Hour)
            .Select(grp => grp.First());
            var model = new IndexViewModels
            {
                Languages =
                {
                    CurrentCount = langService.GetByActiveProject(UserActiveProject).Count(),
                    ActiveCount = langService.GetActiveByActiveProject(UserActiveProject).Count()
                },
                Contexts =
                {
                    CurrentCount =
                        phrsContService.GetAllByIdProject(UserActiveProject, User.Identity.GetUserId()).Count(),
                    Translated =
                        phrsContService.GetTranslatedByIdProject(UserActiveProject, User.Identity.GetUserId()).Count()
                },
                Projects = {ProjectCount = projectServices.GetForUser(User.Identity.GetUserId()).Count()},
                SystemStabilityBox =
                {
                    ProcessorValues = systemStabilityLogs.Select(s => s.CpuPercent).ToList(),
                    MemoryValues = systemStabilityLogs.Select(s => s.MemoryAvailablePercent).ToList(),
                    LoggetHours = systemStabilityLogs.Select(s => s.DateCreated.Value.Hour.ToString()).ToList(),
                    MachineName = systemStabilityLogs.Last().MachineName,
                    MemoryAvailable = systemStabilityLogs.Last().MemoryAvailable,
                    MemoryTotal = systemStabilityLogs.Last().MemoryTotal,
                    MemoryAvailablePercent = systemStabilityLogs.Last().MemoryAvailablePercent,
                    CpuPercent = systemStabilityLogs.Last().CpuPercent,
                 },
                BearerToken = tokenContainer.ApiToken?.ToString()
            };



            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Index page", "Index description page");
            if (Request.IsAjaxRequest())
                return PartialView(model);

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