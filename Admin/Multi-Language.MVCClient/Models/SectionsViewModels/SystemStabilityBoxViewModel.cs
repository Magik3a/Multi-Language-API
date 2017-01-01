using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Multi_Language.MVCClient.Models.SectionsViewModels
{
    public class SystemStabilityBoxViewModel
    {
        public List<string> ProcessorValues;

        public List<string> MemoryValues;

        public List<string> LoggetHours;

        public SystemStabilityBoxViewModel()
        {
            ProcessorValues = new List<string>();
            MemoryValues = new List<string>();
            LoggetHours = new List<string>();
        }

        public string MachineName { get; set; }

        public string MemoryAvailable { get; set; }

        public string MemoryTotal { get; set; }

        public string CpuPercent { get; set; }

        public string MemoryAvailablePercent { get; set; }


        public DateTime? DateCreated { get; set; }
    }
}