using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Multi_Language.MVCClient.Models.SectionsViewModels
{
    public class ContextsFirstRowSectionViewModel
    {
        public LanguagesInfoBoxViewModel Languages;

        public ContextsFirstRowSectionViewModel()
        {
            Languages = new LanguagesInfoBoxViewModel();
        }
    }
}