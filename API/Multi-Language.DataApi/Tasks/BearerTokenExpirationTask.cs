using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using Newtonsoft.Json;

namespace Multi_Language.DataApi.Tasks
{
    public class BearerTokenExpirationTask : IBearerTokenExpirationTask
    {
        public bool BearerTokenExpired(string token)
        {
            //TODO add bearer token to web api call for expiration date
            var postData = new
            {
                Bearer = token
            };

            try
            {
                var json = JsonConvert.SerializeObject(postData);
                // Post the data to the server
                var serverUrl = new Uri(ConfigurationManager.AppSettings["ServerUrl"] + "/api/RefreshTokens/Expires?tokenId=" + token);

                var client = new WebClient();
                client.Headers.Add("Content-Type", "application/json");
                client.UploadString(serverUrl, json);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}