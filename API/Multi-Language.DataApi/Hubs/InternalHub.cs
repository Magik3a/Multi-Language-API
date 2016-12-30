using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Multi_Language.DataApi.Hubs
{
    [HubName("InternalHub")]
    public class InternalHub : Hub
    {
        [Authorize]
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}