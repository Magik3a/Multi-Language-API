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
        private const string Suffix = "suffix";
        public BackupClient(IApiClient apiClient) : base(apiClient)
        {
        }

        public async Task<CreateBackupResponse> CreateBackup(string suffix)
        {
            var model = new CreateBackupApiModel()
            {
                suffix = suffix
            };
            return await PostEncodedContentWithSimpleResponse<CreateBackupResponse, CreateBackupApiModel>(BackupUri + "/create/" + model.suffix + "/", model);

        }
        public async Task<CreateBackupResponse> DeleteBackup(string fileName)
        {
            var model = new DeleteBackupApiModel()
            {
               filename = fileName
            };
            return await PostEncodedContentWithSimpleResponse<CreateBackupResponse, DeleteBackupApiModel>(BackupUri + "/delete/" + fileName + "/", model);

        }
        public async Task<BackupsResponse> GetBackups()
        {
            return await GetJsonDecodedContent<BackupsResponse, List<BackupApiModel>>(BackupUri + "/getall");
        }
    }
}