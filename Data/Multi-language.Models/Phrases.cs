using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Multi_language.Common.Consants;
using System.Collections;
using System.Collections.Generic;

namespace Multi_language.Models
{
    public class Phrases
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPhrase { get; set; }

        public int IdPhraseContext { get; set; }

        public string UserId { get; set; }

        public int IdLanguage { get; set; }

        public string PhraseText { get; set; }

        public DateTime? DateChanged { get; set; }

        public DateTime? DateCreated { get; set; }

        public virtual AppUser User { get; set; }

        public virtual Languages Language { get; set; }

        public virtual PhrasesContext PhraseContext { get; set; }
    }
}
