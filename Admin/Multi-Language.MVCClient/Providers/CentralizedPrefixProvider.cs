using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Web.Mvc.Routing;
using Multi_language.Common.Helpers;
using Multi_Language.MVCClient.Enums;

namespace Multi_Language.MVCClient.Providers
{
    public class CentralizedPrefixProvider : DefaultDirectRouteProvider
    {
        private readonly string _centralizedPrefix;

        public CentralizedPrefixProvider(string centralizedPrefix)
        {
            _centralizedPrefix = centralizedPrefix;
        }

        protected override string GetRoutePrefix(ControllerDescriptor controllerDescriptor)
        {
            if (controllerDescriptor.ControllerName == EControllersPrefix.DocMultilanguage.ToString())
            {
                return
                    $"{_centralizedPrefix?? _centralizedPrefix+"/"}{EnumHelper<EControllersPrefix>.GetDisplayValue(EControllersPrefix.DocMultilanguage)}";
            }

            var existingPrefix = base.GetRoutePrefix(controllerDescriptor);
            if (existingPrefix == null) return _centralizedPrefix;

            return $"{_centralizedPrefix ?? _centralizedPrefix + "/"}{existingPrefix}";
        }
    }
}