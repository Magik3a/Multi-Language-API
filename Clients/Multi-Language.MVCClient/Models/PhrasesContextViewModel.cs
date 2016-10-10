using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Multi_Language.MVCClient.Models
{
    public class PhrasesContextViewModel
    {
        public int IdPhraseContext { get; set; }

        public string UserId { get; set; }

        [Required]
        public string Context { get; set; }

        public DateTime? DateChanged { get; set; }

        public DateTime? DateCreated { get; set; }
    }
}