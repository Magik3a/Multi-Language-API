using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Multi_Language.MVCClient.ApiInfrastructure;
using Multi_Language.MVCClient.Models.UtilitiesViewModels;
using Multi_language.ApiHelper;
using Multi_language.ApiHelper.Client;
using Multi_Language.MVCClient.ApiInfrastructure.Client;
using Newtonsoft.Json;
using AutoMapper;
namespace Multi_Language.MVCClient.Controllers.Utilities
{
    public class BackupsController : BaseController
    {
        private readonly ITokenContainer tokenContainer;
        private readonly IBackupClient backupClient;
        public BackupsController(ITokenContainer tokenContainer, IBackupClient backupClient)
        {
            this.tokenContainer = tokenContainer;
            var apiClient = new ApiClient(HttpClientInstance.Instance, tokenContainer);
            this.backupClient = new BackupClient(apiClient);
        }
        public async Task<ActionResult> Index()
        {
            var backups = await backupClient.GetBackups();
            var model = new List<BackupViewModel>();
            foreach (var backupApiModel in backups.Data)
            {
                model.Add(Mapper.Map<BackupViewModel>(backupApiModel));
            }

            //TODO remove viewbag with token. Use Mvc controllers
            ViewBag.BearerToken = tokenContainer.ApiToken.ToString();

            SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Backups page", "Create, upload and restore the DataBase");
            if (Request.IsAjaxRequest())
                return PartialView(model);

            return View(model);
        }
        //// GET: Backup
        //public ActionResult Index()
        //{
        //    var model = new List<BackupViewModels>();
        //    try
        //    {
        //        using (HttpClient client = new HttpClient())
        //        {
        //            var bearerToken = tokenContainer.ApiToken.ToString();
        //            // TODO Add validation for bearer token

        //            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
        //            ViewBag.BearerToken = bearerToken;
        //            var response = client.GetAsync(ConfigurationManager.AppSettings["MultiLanguageApiUrl"] + "/backup/getall").GetAwaiter().GetResult();
        //            if (response.IsSuccessStatusCode)
        //            {
        //                var result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        //                model = JsonConvert.DeserializeObject<List<BackupViewModels>>(result);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //TODO send to admin or do something with this
        //    }

        //    SetViewBagsAndHeaders(Request.IsAjaxRequest(), "Backups page", "Create, upload and restore the DataBase");
        //    if (Request.IsAjaxRequest())
        //        return PartialView(model);

        //    return View(model);
        //}

    }
}