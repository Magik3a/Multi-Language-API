using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Multi_language.ApiHelper.Model;

namespace Multi_Language.MVCClient.ApiInfrastructure.ApiModels
{
    public class SystemInfoApiModel : ApiModel
    {
        public string UpTime { get; set; }

        public string DiskSpaceUsed { get; set; }

        public string DataApiVersion { get; set; }

        public string Ip { get; set; }

        public string ReservedDiskSpacePercent { get; set; }

        public string FreeDiskSpacePercent { get; set; }
    }
}