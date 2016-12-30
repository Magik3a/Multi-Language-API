using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Multi_Language.DataApi.Models
{
    public class CpuInfoApiModel
    {
        public string MachineName { get; set; }

        public double Processor { get; set; }

        public ulong MemUsage { get; set; }

        public ulong TotalMemory { get; set; }
    }
}