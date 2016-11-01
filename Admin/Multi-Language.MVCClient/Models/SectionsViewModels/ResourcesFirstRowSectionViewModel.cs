using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Multi_Language.MVCClient.Models.SectionsViewModels
{
    public class ResourcesFirstRowSectionViewModel
    {
        public LanguagesInfoBoxViewModel Languages;
        public ContextsInfoBoxViewModel Contexts;
        public CurrentProjectInfoBoxViewModel Projects;

        public ResourcesFirstRowSectionViewModel()
        {
            Languages = new LanguagesInfoBoxViewModel();
            Contexts = new ContextsInfoBoxViewModel();
            Projects = new CurrentProjectInfoBoxViewModel();
        }
    }
}