using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Multi_Language.DataApi.Models
{
    public class SystemInfoApiModel
    {
        public string UpTime { get; set; }

        public string DiskSpaceUsed { get; set; }

        public string DataApiVersion { get; set; }

        public string Ip { get; set; }

        public string ReservedDiskSpacePercent { get; set; }

        public string FreeDiskSpacePercent { get; set; }
    }
}