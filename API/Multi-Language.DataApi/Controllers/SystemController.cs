using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Multi_language.Common;
using Multi_language.Common.Enums;
using Multi_Language.DataApi.Models;

namespace Multi_Language.DataApi.Controllers
{
    [RoutePrefix("system")]
    //[AuthorizeEnum(ERoleLevels.AdminPermissions)]
    public class SystemController : ApiController
    {
        private string backupFolder = "~/DBBackups";

        [Route("getsysteminfo", Name = "GetSystemInfo")]
        [ResponseType(typeof(SystemInfoApiModel))]
        public IHttpActionResult GetSystemInfo()
        {
            var driveLetter = HttpContext.Current.Server.MapPath(backupFolder).Substring(0,1);

            var model = new SystemInfoApiModel();
            model.ReservedDiskSpacePercent = $"{Utils.GetDiskSpaceUsedPercentage(driveLetter):N1}";
            model.FreeDiskSpacePercent = $"{100 - Utils.GetDiskSpaceUsedPercentage(driveLetter):N1}";
            model.UpTime = Utils.GetSystemUpTime();
            model.DiskSpaceUsed = Utils.GetDiskSpaceUsedString(driveLetter);
            model.Ip = Utils.GetLocalIpV4().ToString();
            model.DataApiVersion = typeof(Multi_Language.DataApi.Startup).Assembly.GetName().Version.ToString();
            return Ok(model);
        }

    }
}
