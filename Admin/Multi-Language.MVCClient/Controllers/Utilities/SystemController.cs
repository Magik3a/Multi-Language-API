using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Multi_language.ApiHelper;
using Multi_Language.MVCClient.ApiInfrastructure.Client;
using Multi_Language.MVCClient.Attributes;
using Multi_Language.MVCClient.Models.UtilitiesViewModels;

namespace Multi_Language.MVCClient.Controllers.Utilities
{
    [Authorize]
    [Authentication]
    public class SystemController : BaseController
    {

        private readonly ITokenContainer tokenContainer;
        private readonly ISystemInfoClient systemInfoClient;
        public SystemController(ITokenContainer tokenContainer, ISystemInfoClient systemInfoClient)
        {
            this.tokenContainer = tokenContainer;
            this.systemInfoClient = systemInfoClient;
        }
        // GET: System
        public async Task<ActionResult> Index()
        {
            var systemInfo = await systemInfoClient.GetSystemInfo();
            var model = new SystemViewModels();

            if (systemInfo.StatusIsSuccessful)
                model = Mapper.Map<SystemViewModels>(systemInfo.Data);

            model.BearerToken = tokenContainer.ApiToken?.ToString();

            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "System info page", "This is server info where Data API is.");
            if (Request.IsAjaxRequest())
                return PartialView(model);

            return View(model);
        }
    }
}