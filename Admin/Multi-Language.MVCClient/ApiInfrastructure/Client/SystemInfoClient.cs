using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Multi_language.ApiHelper.Client;
using Multi_Language.MVCClient.ApiInfrastructure.ApiModels;
using Multi_Language.MVCClient.ApiInfrastructure.Responses;

namespace Multi_Language.MVCClient.ApiInfrastructure.Client
{
    public class SystemInfoClient : ClientBase, ISystemInfoClient
    {
        private const string SystemInfoUri = "system";
        public SystemInfoClient(IApiClient apiClient) : base(apiClient)
        {
        }

        public async Task<SystemInfoResponse> GetSystemInfo()
        {
            return await GetJsonDecodedContent<SystemInfoResponse, SystemInfoApiModel>(SystemInfoUri + "/getsysteminfo");
        }
    }
}