using Multi_language.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Multi_Language.MVCClient.Models
{
    public class PhrasesContextViewModel
    {
        [Display(Name = "Id")]
        public int IdPhraseContext { get; set; }

        public string UserId { get; set; }

        [Display(Name = "Id")]
        public int IdProject { get; set; }

        [Required]
        public string Context { get; set; }

        [Display(Name = "Date Changed")]
        public DateTime? DateChanged { get; set; }

        [Display(Name = "Date Created")]
        public DateTime? DateCreated { get; set; }

        public virtual AppUser User { get; set; }
    }
}