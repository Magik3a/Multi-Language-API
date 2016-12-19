using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Multi_language.ApiHelper.Model;

namespace Multi_Language.MVCClient.ApiInfrastructure.ApiModels
{
    public class BackupApiModel : ApiModel
    {
        public string FileName { get; set; }

        public string FileSize { get; set; }
    }
}