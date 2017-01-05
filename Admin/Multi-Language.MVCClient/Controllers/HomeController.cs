using Microsoft.AspNet.Identity;
using Multi_language.Services;
using Multi_Language.MVCClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Multi_language.ApiHelper;
using Multi_language.Models;
using Multi_Language.MVCClient.Attributes;
using Multi_Language.MVCClient.Models.SectionsViewModels;
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
                SystemStabilityBox = GetSystemStabilityLoggsViewModel(),
                BearerToken = tokenContainer.ApiToken?.ToString()
            };



            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Index page", "Index description page");
            if (Request.IsAjaxRequest())
                return PartialView(model);

            return View(model);
        }

        public SystemStabilityBoxViewModel GetSystemStabilityLoggsViewModel(int hoursBefore = 24)
        {
            var loggsBefore = -hoursBefore;

            // Default 24 hours
            TimeSpan interval = new TimeSpan(1, 0, 0);

            if (hoursBefore == 6)
               interval = new TimeSpan(0, 10, 0);

            if (hoursBefore == 12)
                interval = new TimeSpan(0, 30, 0);

            if (hoursBefore == 24)
                interval = new TimeSpan(1, 0, 0);

            var systemStabilityLogs = systemStabilityLoggsService.GetAllBeforeHours(loggsBefore).ToList().GroupBy(x => x.DateCreated?.Ticks / interval.Ticks)
            .Select(grp => grp.First());

            var systemStabilityLoggs = systemStabilityLogs as IList<SystemStabilityLogg> ?? systemStabilityLogs.ToList();

            return new SystemStabilityBoxViewModel()
            {
                ForThePastHours = hoursBefore,
                ProcessorValues = systemStabilityLoggs.Select(s => s.CpuPercent).ToList(),
                MemoryValues = systemStabilityLoggs.Select(s => s.MemoryAvailablePercent).ToList(),
                LoggetHours = systemStabilityLoggs.Select(s => s.DateCreated?.Hour.ToString() + ":" + s.DateCreated?.Minute.ToString()).ToList(),
                MachineName = systemStabilityLoggs.Last().MachineName,
                MemoryAvailable = systemStabilityLoggs.Last().MemoryAvailable,
                MemoryTotal = systemStabilityLoggs.Last().MemoryTotal,
                MemoryAvailablePercent = systemStabilityLoggs.Last().MemoryAvailablePercent,
                CpuPercent = systemStabilityLoggs.Last().CpuPercent
            };
        }

        public ActionResult GetSystemStabilityBox(int? hoursBefore)
        {
            return PartialView("InnerPartials/SystemStabilityBox", GetSystemStabilityLoggsViewModel(hoursBefore??24));
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