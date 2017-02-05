using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Security;
using Ninject.Activation;

namespace Multi_Language.MVCClient.ApiInfrastructure.Client
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Multi_language.ApiHelper.Client;
    using Multi_language.ApiHelper.Response;
    using ApiModels;
    using Models;
    using Responses;

    public class LoginClient : ClientBase, ILoginClient
    {
        private const string RegisterUri = "api/account/register";
        private const string RegisterExternalUri = "api/account/registerexternal";
        private const string ExternalLogin = "api/account/externallogin";
        private const string ObtaionLocalAccessTokenUri = "api/Account/ObtainLocalAccessToken";


        private const string TokenUri = "token";
        private const string ResourceOwnerAccessUri = "token";
        public LoginClient(IApiClient apiClient) : base(apiClient)
        {
        }

        public async Task<TokenResponse> Login(string email, string password)
        {
            var response = await ApiClient.PostFormEncodedContent(TokenUri, "grant_type".AsPair("password"),
                "username".AsPair(email), "password".AsPair(password), "client_id".AsPair("ngAuthApp"));


            var tokenResponse = await CreateJsonResponse<TokenResponse>(response);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await DecodeContent<dynamic>(response);
                tokenResponse.ErrorState = new ErrorStateResponse
                {
                    ModelState = new Dictionary<string, string[]>
                    {
                        {errorContent["error"], new string[] {errorContent["error_description"]}}
                    }
                };
                return tokenResponse;
            }
            var grantResourceOwnerAccess = await ApiClient.PostFormEncodedContent(ResourceOwnerAccessUri, "grant_type".AsPair("password"),
                "username".AsPair(email), "password".AsPair(password), "client_id".AsPair("ngAuthApp"));

            var tokenData = await DecodeContent<dynamic>(response);
            tokenResponse.Data = tokenData["access_token"];
            return tokenResponse;
        }

        public async Task<TokenResponse> ObtaionLocalAccesstoken(string provider, string externalAccessToken)
        {
            var response = await ApiClient.GetFormEncodedContent(ObtaionLocalAccessTokenUri, "provider".AsPair(provider), "externalAccessToken".AsPair(externalAccessToken));
            var tokenResponse = await CreateJsonResponse<TokenResponse>(response);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await DecodeContent<dynamic>(response);
                tokenResponse.ErrorState = new ErrorStateResponse
                {
                    ModelState = new Dictionary<string, string[]>
                    {
                        {errorContent["error"], new string[] {errorContent["error_description"]}}
                    }
                };
                return tokenResponse;
            }


            var tokenData = await DecodeContent<dynamic>(response);
            tokenResponse.Data = tokenData["access_token"];
            return tokenResponse;
        }

        public async Task<TokenResponse> GrandResourceOwnerAccess(string email, string password)
        {
            var response = await ApiClient.PostFormEncodedContent(ResourceOwnerAccessUri, "grant_type".AsPair("password"),
               "username".AsPair(email), "password".AsPair(password), "client_id".AsPair("ngAuthApp"));
            var tokenResponse = await CreateJsonResponse<TokenResponse>(response);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await DecodeContent<dynamic>(response);
                tokenResponse.ErrorState = new ErrorStateResponse
                {
                    ModelState = new Dictionary<string, string[]>
                    {
                        {errorContent["error"], new string[] {errorContent["error_description"]}}
                    }
                };
                return tokenResponse;
            }


            var tokenData = await DecodeContent<dynamic>(response);
            tokenResponse.Data = tokenData["access_token"];
            return tokenResponse;
        }
        public async Task<LoginResponse> LoginExternal(string provider, HttpCookie authCookie)
        {
            var response = await ApiClient.GetFormEncodedContent(ExternalLogin,
                new KeyValuePair<string, string>("provider", provider),
                new KeyValuePair<string, string>("response_type", "token"),
                new KeyValuePair<string, string>("client_id", "ngAuthApp"),
                new KeyValuePair<string, string>("redirect_uri", "http://localhost:64959/"));
           response.Headers.Add("Set-Cookie", authCookie.Values.ToString());
            var registerResponse = await CreateJsonResponse<LoginResponse>(response);
            return registerResponse;
        }
        public async Task<RegisterResponse> Register(RegisterViewModel viewModel)
        {
            var apiModel = new RegisterApiModel
            {
                ConfirmPassword = viewModel.ConfirmPassword,
                Email = viewModel.Email,
                Password = viewModel.Password
            };
            var response = await ApiClient.PostJsonEncodedContent(RegisterUri, apiModel);
            var registerResponse = await CreateJsonResponse<RegisterResponse>(response);

            return registerResponse;
        }


        public async Task<RegisterResponse> RegisterExternal(RegisterExternalViewModel viewModel)
        {
            var apiModel = new RegisterExternalApiModel()
            {
                provider = viewModel.Provider,
                ExternalAccessToken = viewModel.ExternalAccessToken,
                userName = viewModel.UserName
            };
            var response = await ApiClient.PostJsonEncodedContent(RegisterExternalUri, apiModel);
            var registerResponse = await CreateJsonResponse<RegisterResponse>(response);
            return registerResponse;
        }
    }
}