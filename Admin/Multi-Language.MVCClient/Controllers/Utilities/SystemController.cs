using System.Web.Mvc;
using Multi_Language.MVCClient.Attributes;
using Multi_Language.MVCClient.Models.UtilitiesViewModels;

namespace Multi_Language.MVCClient.Controllers.Utilities
{
    [Authentication]
    public class SystemController : BaseController
    {
        // GET: System
        public ActionResult Index()
        {
            var  model = new SystemViewModels();

            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "System info page", "This is server info where Data API is.");
            if (Request.IsAjaxRequest())
                return PartialView(model);

            return View(model);
        }
    }
}