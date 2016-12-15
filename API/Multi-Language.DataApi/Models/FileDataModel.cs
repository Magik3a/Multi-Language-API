using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Multi_Language.DataApi.Models
{
    public class FileDataModel
    {
        public FileDataModel(string filename, string filesize)
        {
            FileName = filename;
            FileSize = filesize;
        }

        public string FileName { get; set; }
        public string FileSize { get; set; }
    }
}