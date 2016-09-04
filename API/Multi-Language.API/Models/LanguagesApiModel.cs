using Multi_language.Common.Infrastructure.Mapping;
using Multi_language.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Multi_Language.API.Models
{
    public class LanguagesApiModel : IMapFrom<Languages>
    {
        public int IdLanguage { get; set; }
        
        public string Name { get; set; }
        
        public string Initials { get; set; }
        
        public string Culture { get; set; }
        
        public byte[] Picture { get; set; }

        public bool IsActive { get; set; }
        
        public string UserName { get; set; }

        public DateTime? Datechanged { get; set; }

        public DateTime? DateCreated { get; set; }
    }
}