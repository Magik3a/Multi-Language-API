using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Multi_Language.DataApi.Hubs
{
    [HubName("InternalHub")]
    public class InternalHub : Hub
    {
        public string Send(string name, string message)
        {
            Clients.All.broadcastMessage(name, message);
            return null;
        }
    }
}