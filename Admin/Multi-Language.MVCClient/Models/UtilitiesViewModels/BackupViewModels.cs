using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Multi_language.Common.Infrastructure.Mapping;
using Multi_Language.MVCClient.ApiInfrastructure.ApiModels;

namespace Multi_Language.MVCClient.Models.UtilitiesViewModels
{
    public class BackupViewModel : IMapFrom<BackupApiModel>
    {
        public string FileName { get; set; }

        public string FileSize { get; set; }

    }


}