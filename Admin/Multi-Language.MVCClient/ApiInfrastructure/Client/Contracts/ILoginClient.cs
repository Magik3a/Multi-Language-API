namespace Multi_Language.MVCClient.ApiInfrastructure.Client
{
    using System.Threading.Tasks;
    using Models;
    using Responses;

    public interface ILoginClient
    {
        Task<TokenResponse> Login(string email, string password);

        Task<TokenResponse> GrandResourceOwnerAccess(string email, string password);

        Task<RegisterResponse> Register(RegisterViewModel viewModel);

        Task<RegisterResponse> RegisterExternal(RegisterExternalViewModel viewModel);
    }
}