using GenericCore.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFS.BE.Data
{
    public class DataResult
    {
        public bool FileDownloaded { get; private set; }
        public string LocalPath { get; private set; }

        public DataResult(string localPath, bool fileDownloaded)
        {
            localPath.AssertNotNull("localPath");
            
            LocalPath = localPath;
            FileDownloaded = fileDownloaded;
        }
    }
}
