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
    }
}