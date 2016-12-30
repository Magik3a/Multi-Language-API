using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.SignalR;
using Multi_Language.DataApi.Hubs;
using Multi_Language.DataApi.Models;

namespace Multi_Language.DataApi.Controllers
{
    [RoutePrefix("cpu")]
    public class CpuInfoController : ApiController
    {
        public void Post(CpuInfoApiModel cpuInfo)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<InternalHub>();
            context.Clients.All.cpuInfoMessage(cpuInfo.MachineName, cpuInfo.Processor, cpuInfo.MemUsage, cpuInfo.TotalMemory);
        }
    }
}
