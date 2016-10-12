using Multi_Language.MVCClient.Models.SectionsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Multi_Language.MVCClient.Controllers
{
    public class SectionsController : BaseController
    {
        // GET: Sections
        public async Task<ActionResult> FirstRow(string id)
        {
            if (id == "Resources")
            {
                var model = new ResourcesFirstRowSectionViewModel();
                model.Languages.CurrentCount = 9;
                model.Languages.ActiveCount = 3;

                model.Contexts.CurrentCount = 40;
                model.Contexts.Translated = 25;
                return PartialView("ResourcesFirstRowSection", model);
            }

            if (id == "Contexts")
            {
                var model = new ContextsFirstRowSectionViewModel();
                model.Languages.CurrentCount = 9;
                model.Languages.ActiveCount = 3;

                return PartialView("ContextsFirstRowSection", model);

            }
            return PartialView("Default");
        }
    }
}