using Multi_language.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Multi_Language.MVCClient.Models
{
    public class ResourcesViewModels
    {
        public int IdPhrase { get; set; }

        public int IdPhraseContext { get; set; }

        public string UserId { get; set; }

        public int IdLanguage { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string PhraseText { get; set; }

        public DateTime? DateChanged { get; set; }

        public DateTime? DateCreated { get; set; }

        public virtual AppUser User { get; set; }

        public virtual Languages Language { get; set; }

        public virtual PhrasesContext PhraseContext { get; set; }
    }
}