using Multi_language.Common.Infrastructure.Mapping;
using Multi_language.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Multi_Language.API.Models
{
    public class PhrasesContextApiModel : IMapFrom<PhrasesContext>
    {
        public string UserId { get; set; }

        public string Context { get; set; }

        public DateTime? DateChanged { get; set; }

        public DateTime? DateCreated { get; set; }
    }
}