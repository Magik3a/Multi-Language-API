using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Multi_Language.DataApi.Hubs
{
    [HubName("InternalHub")]
    public class InternalHub : Hub
    {
        public void SendCpuInfo(string machineName, double processor, int memUsage, int totalMemory)
        {
            this.Clients.All.cpuInfoMessage(machineName, processor, memUsage, totalMemory);
        }

        public string Send(string name, string message)
        {
            Clients.All.broadcastMessage(name, message);
            return null;
        }
    }
}