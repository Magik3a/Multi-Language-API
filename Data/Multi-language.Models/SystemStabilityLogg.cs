using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Multi_language.Models
{
    public class SystemStabilityLogg
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string MachineName { get; set; }

        public string MemoryAvailable { get; set; }

        public string MemoryTotal { get; set; }

        public string CpuPercent { get; set; }

        public string MemoryAvailablePercent { get; set; }


        public DateTime? DateCreated { get; set; }

    }
}
