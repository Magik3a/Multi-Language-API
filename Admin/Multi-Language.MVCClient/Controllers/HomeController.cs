using Microsoft.AspNet.Identity;
using Multi_language.Services;
using Multi_Language.MVCClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Boilerplate.Web.Mvc.Filters;
using Multi_language.ApiHelper;
using Multi_language.Common.Infrastructure.Manifest;
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
        private readonly IManifestService manifestService;
        public HomeController(
            IProjectsServices projectServices,
            ILanguagesService langService,
            IPhrasesContextServices phrsContService,
            ITokenContainer tokenContainer,
            ISystemStabilityLoggsService systemStabilityLoggsService,
            IManifestService manifestService)
        {
            this.projectServices = projectServices;
            this.langService = langService;
            this.phrsContService = phrsContService;
            this.tokenContainer = tokenContainer;
            this.systemStabilityLoggsService = systemStabilityLoggsService;
            this.manifestService = manifestService;
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
            if (!systemStabilityLoggs.Any())
            {
                return new SystemStabilityBoxViewModel();
            }
            return new SystemStabilityBoxViewModel()
            {
                ForThePastHours = hoursBefore,
                ProcessorValues = systemStabilityLoggs.Select(s => s.CpuPercent).ToList(),
                MemoryValues = systemStabilityLoggs.Select(s => s.MemoryAvailablePercent).ToList(),
                LoggetHours = systemStabilityLoggs.Select(s => s.DateCreated?.Hour.ToString() + ":" + s.DateCreated?.Minute.ToString()).ToList(),
                MachineName = systemStabilityLoggs.Last()?.MachineName,
                MemoryAvailable = systemStabilityLoggs.Last()?.MemoryAvailable,
                MemoryTotal = systemStabilityLoggs.Last()?.MemoryTotal,
                MemoryAvailablePercent = systemStabilityLoggs.Last()?.MemoryAvailablePercent,
                CpuPercent = systemStabilityLoggs.Last()?.CpuPercent
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
        /// <summary>
        /// Gets the browserconfig XML for the current site. This allows you to customize the tile, when a user pins
        /// the site to their Windows 8/10 start screen. See http://www.buildmypinnedsite.com and
        /// https://msdn.microsoft.com/en-us/library/dn320426%28v=vs.85%29.aspx
        /// </summary>
        /// <returns>The browserconfig XML for the current site.</returns>
        [NoTrailingSlash]
        [OutputCache(Duration = 8200)]
        [Route("browserconfig.xml", Name = "browserconfig")]
        public ContentResult BrowserConfigXml()
        {
            //string content = this.browserConfigService.GetBrowserConfigXml();
            string content = "";
            return this.Content(content, Boilerplate.Web.Mvc.ContentType.Xml, Encoding.UTF8);
        }

        /// <summary>
        /// Gets the manifest JSON for the current site. This allows you to customize the icon and other browser
        /// settings for Chrome/Android and FireFox (FireFox support is coming). See https://w3c.github.io/manifest/
        /// for the official W3C specification. See http://html5doctor.com/web-manifest-specification/ for more
        /// information. See https://developer.chrome.com/multidevice/android/installtohomescreen for Chrome's
        /// implementation.
        /// </summary>
        /// <returns>The manifest JSON for the current site.</returns>
        [NoTrailingSlash]
        [OutputCache(Duration = 8200)]
        [Route("manifest.json", Name = "manifest")]
        [AllowAnonymous]
        public ContentResult ManifestJson()
        {
            string content = manifestService.GetManifestJson("MultiAdminAPI", "Multi Admin API", new Uri(System.Web.HttpContext.Current.Request.Url.Authority) + "/Images/Socials/icons").TrimEnd('/');
            return this.Content(content, Boilerplate.Web.Mvc.ContentType.Json, Encoding.UTF8);
        }

        /// <summary>
        /// Gets the Open Search XML for the current site. You can customize the contents of this XML here. The open
        /// search action is cached for one day, adjust this time to whatever you require. See
        /// http://www.hanselman.com/blog/CommentView.aspx?guid=50cc95b1-c043-451f-9bc2-696dc564766d
        /// http://www.opensearch.org
        /// </summary>
        /// <returns>The Open Search XML for the current site.</returns>
        [NoTrailingSlash]
        [OutputCache(Duration = 8200)]
        [Route("opensearch.xml", Name = "opensearch")]
        public ActionResult OpenSearchXml()
        {
            //string content = this.openSearchService.GetOpenSearchXml();
            string content = "";
            return this.Content(content, Boilerplate.Web.Mvc.ContentType.Xml, Encoding.UTF8);
        }

        /// <summary>
        /// Tells search engines (or robots) how to index your site.
        /// The reason for dynamically generating this code is to enable generation of the full absolute sitemap URL
        /// and also to give you added flexibility in case you want to disallow search engines from certain paths. The
        /// sitemap is cached for one day, adjust this time to whatever you require. See
        /// http://rehansaeed.com/dynamically-generating-robots-txt-using-asp-net-mvc/
        /// </summary>
        /// <returns>The robots text for the current site.</returns>
        [NoTrailingSlash]
        [OutputCache(Duration = 8200)]
        [Route("robots.txt", Name = "robots")]
        public ActionResult RobotsText()
        {
            //string content = this.robotsService.GetRobotsText();
            string content = "";
            return this.Content(content, Boilerplate.Web.Mvc.ContentType.Text, Encoding.UTF8);
        }

        /// <summary>
        /// Gets the sitemap XML for the current site. You can customize the contents of this XML from the
        /// <see cref="SitemapService"/>. The sitemap is cached for one day, adjust this time to whatever you require.
        /// http://www.sitemaps.org/protocol.html
        /// </summary>
        /// <param name="index">The index of the sitemap to retrieve. <c>null</c> if you want to retrieve the root
        /// sitemap file, which may be a sitemap index file.</param>
        /// <returns>The sitemap XML for the current site.</returns>
        [NoTrailingSlash]
        [Route("sitemap.xml", Name = "sitemap")]
        public async Task<ActionResult> SitemapXml(int? index = null)
        {
            //string content = await this.sitemapService.GetSitemapXml(index);
            string content = "";
            if (content == null)
            {
                return Content("Sitemap index is out of range.");
            }

            return this.Content(content, Boilerplate.Web.Mvc.ContentType.Xml, Encoding.UTF8);
        }
    }
}