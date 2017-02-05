using System.Web;

namespace Multi_Language.MVCClient.ApiInfrastructure.Client
{
    using System.Threading.Tasks;
    using Models;
    using Responses;

    public interface ILoginClient
    {
        Task<TokenResponse> Login(string email, string password);

        Task<LoginResponse> LoginExternal(string provider, HttpCookie authCookie);

        Task<TokenResponse> ObtaionLocalAccesstoken(string provider, string externalAccessToken);

        Task<TokenResponse> GrandResourceOwnerAccess(string email, string password);

        Task<RegisterResponse> Register(RegisterViewModel viewModel);

        Task<RegisterResponse> RegisterExternal(RegisterExternalViewModel viewModel);
    }
}