using Multi_language.Common.Infrastructure.Mapping;
using Multi_language.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Multi_Language.MVCClient.Models
{
    public class ProjectsViewModel : IMapFrom<Projects>
    {
        [Display(Name = "Id")]
        public int IdProject { get; set; }

        [Required]
        [StringLength(100)]
        public string ProjectName { get; set; }

        public string UserId { get; set; }

        [Display(Name = "Date Changed")]
        public DateTime? DateChanged { get; set; }

        [Display(Name = "Date Created")]
        public DateTime? DateCreated { get; set; }

        public virtual AppUser User { get; set; }
    }
}