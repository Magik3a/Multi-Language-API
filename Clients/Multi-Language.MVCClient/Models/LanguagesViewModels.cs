using Multi_language.Common.Consants;
using Multi_language.Common.Infrastructure.Mapping;
using Multi_language.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Multi_Language.MVCClient.Models
{
    public class LanguagesViewModels : IMapFrom<Languages>
    {
        public int IdLanguage { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(10)]
        public string Initials { get; set; }

        [Required]
        [StringLength(20)]
       // [RegularExpression("^([a-zA-Z0-9 .&'-]+)$")]
        public string Culture { get; set; }

        [Column(TypeName = "image")]
        public byte[] Picture { get; set; }

        public bool IsActive { get; set; }

        [StringLength(ValidationConstants.UserEmail)]
        public string UserName { get; set; }

        public DateTime? DateChanged { get; set; }

        public DateTime? DateCreated { get; set; }
    }
}