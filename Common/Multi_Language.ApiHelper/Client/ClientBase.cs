using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Helpers;
using Multi_language.ApiHelper.Model;
using Multi_language.ApiHelper.Response;

namespace Multi_language.ApiHelper.Client
{
    public abstract class ClientBase
    {
        protected readonly IApiClient ApiClient;

        protected ClientBase(IApiClient apiClient)
        {
            ApiClient = apiClient;
        }

        protected async Task<TResponse> GetJsonDecodedContent<TResponse, TContentResponse>(string uri, params KeyValuePair<string, string>[] requestParameters) 
            where TResponse : ApiResponse<TContentResponse>, new()
        {
            using (var apiResponse = await ApiClient.GetFormEncodedContent(uri, requestParameters))
            {
                return await DecodeJsonResponse<TResponse, TContentResponse>(apiResponse);
            }
        }

        protected async Task<TResponse> PostEncodedContentWithSimpleResponse<TResponse, TModel>(string url, TModel model)
            where TModel : ApiModel
            where TResponse : ApiResponse<string>, new()
        {
            using (var apiResponse = await ApiClient.PostJsonEncodedContent(url, model))
            {
                return await DecodeJsonResponse<TResponse, string>(apiResponse);
            }
        }

        protected static async Task<TResponse> CreateJsonResponse<TResponse>(HttpResponseMessage response) where TResponse : ApiResponse, new()
        {
            var clientResponse = new TResponse
            {
                StatusIsSuccessful = response.IsSuccessStatusCode,
                ErrorState = response.IsSuccessStatusCode ? null : await DecodeContent<ErrorStateResponse>(response),
                ResponseCode = response.StatusCode
            };
            if (response.Content != null)
            {
                clientResponse.ResponseResult = await response.Content.ReadAsStringAsync();
            }

            return clientResponse;
        }

        protected static async Task<TContentResponse> DecodeContent<TContentResponse>(HttpResponseMessage response)
        {
            var result = await response.Content.ReadAsStringAsync();
            return Json.Decode<TContentResponse>(result);
        }

        private static async Task<TResponse> DecodeJsonResponse<TResponse, TDecode>(HttpResponseMessage apiResponse) where TResponse : ApiResponse<TDecode>, new()
        {
            var response = await CreateJsonResponse<TResponse>(apiResponse);
            response.Data = Json.Decode<TDecode>(response.ResponseResult);
            return response;
        }
    }
}