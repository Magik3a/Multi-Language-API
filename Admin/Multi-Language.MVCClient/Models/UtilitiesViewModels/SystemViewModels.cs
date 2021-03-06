﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Multi_Language.MVCClient.Models.UtilitiesViewModels
{
    public class SystemViewModels
    {
        public string UpTime { get; set; }

        public string DiskSpaceUsed { get; set; }

        public string ClientVersion { get; set; }

        public string DataApiVersion { get; set; }

        public string Ip { get; set; }

        public string BearerToken { get; set; }
    }
}