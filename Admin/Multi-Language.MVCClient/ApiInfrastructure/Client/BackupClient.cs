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
    public class BackupClient : ClientBase, IBackupClient
    {
        private const string BackupUri = "backup";
        public BackupClient(IApiClient apiClient) : base(apiClient)
        {
        }

        public async Task<CreateBackupResponse> CreateBackup(string suffix)
        {
            var apiModel = new BackupApiModel()
            {
                FileName = suffix
            };
            var createBackupResponse = await PostEncodedContentWithSimpleResponse<CreateBackupResponse, BackupApiModel>(BackupUri, apiModel);
            return createBackupResponse;
        }

        public async Task<BackupsResponse> GetBackups()
        {
            return await GetJsonDecodedContent<BackupsResponse, List<BackupApiModel>>(BackupUri + "/getall");
        }
    }
}