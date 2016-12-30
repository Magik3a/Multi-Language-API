using System.Threading.Tasks;
using Multi_Language.MVCClient.ApiInfrastructure.Responses;

namespace Multi_Language.MVCClient.ApiInfrastructure.Client
{
    public interface ISystemInfoClient
    {
        Task<SystemInfoResponse> GetSystemInfo();
    }
}