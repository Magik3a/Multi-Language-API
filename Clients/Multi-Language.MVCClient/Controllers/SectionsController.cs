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
                return PartialView("ResourcesFirstRowSection");
            }
            return PartialView("Default");
        }
    }
}