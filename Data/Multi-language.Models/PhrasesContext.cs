using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Multi_language.Common.Consants;
using System.Collections;
using System.Collections.Generic;

namespace Multi_language.Models
{
    public class PhrasesContext
    {
        public PhrasesContext()
        {
            Phrases = new HashSet<Phrases>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPhraseContext { get; set; }


        public string UserId { get; set; }

        public int IdProject { get; set; }


        [Required]
        [StringLength(1000)]
        public string Context { get; set; }

        public DateTime? DateChanged { get; set; }

        public DateTime? DateCreated { get; set; }

        public virtual AppUser User { get; set; }


        public virtual ICollection<Phrases> Phrases { get; set; }

    }
}
