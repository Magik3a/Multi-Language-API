using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Multi_language.Common.Consants;

namespace Multi_language.Models
{
    public class Languages
    {
        [Key]
        public int IdLanguage { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(10)]
        public string Initials { get; set; }

        [Required]
        [StringLength(20)]
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
