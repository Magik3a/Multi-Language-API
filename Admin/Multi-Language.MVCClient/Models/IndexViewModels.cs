using Multi_Language.MVCClient.Models.SectionsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Multi_Language.MVCClient.Models
{
    public class IndexViewModels
    {
        public LanguagesInfoBoxViewModel Languages;
        public ContextsInfoBoxViewModel Contexts;
        public CurrentProjectInfoBoxViewModel Projects;
        public string BearerToken { get; set; }
        public IndexViewModels()
        {
            Languages = new LanguagesInfoBoxViewModel();
            Contexts = new ContextsInfoBoxViewModel();
            Projects = new CurrentProjectInfoBoxViewModel();
        }
    }
}