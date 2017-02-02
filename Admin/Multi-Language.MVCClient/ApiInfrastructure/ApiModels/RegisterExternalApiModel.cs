using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Multi_language.ApiHelper.Model;

namespace Multi_Language.MVCClient.ApiInfrastructure.ApiModels
{
    public class RegisterExternalApiModel : ApiModel
    {
        public string userName { get; set; }

        public string provider { get; set; }

        public string ExternalAccessToken { get; set; }
    }
}