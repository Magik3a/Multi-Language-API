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
        [Display(Name = "Id")]
        public int IdLanguage { get; set; }

        [Display(Name = "Id")]
        public int IdProject { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Init.")]
        public string Initials { get; set; }

        [Required]
        [StringLength(20)]
       [RegularExpression("^[a-z]{2,3}(?:-[A-Z]{2,3}(?:-[a-zA-Z]{4})?)?$")]
        public string Culture { get; set; }

        [Column(TypeName = "image")]
        public byte[] Picture { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [StringLength(ValidationConstants.UserEmail)]
        public string UserName { get; set; }
        [Display(Name = "Date Changed")]
        public DateTime? DateChanged { get; set; }

        [Display(Name = "Date Created")]
        public DateTime? DateCreated { get; set; }
    }
}