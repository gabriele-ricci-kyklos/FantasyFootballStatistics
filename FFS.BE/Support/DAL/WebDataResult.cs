using GenericCore.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFS.BE.Support.DAL
{
    public class WebDataResult
    {
        public string RemoteUrl { get; private set; }
        public bool FileDownloaded { get; private set; }
        public string LocalPath { get; private set; }

        public WebDataResult(string remoteUrl, string localPath, bool fileDownloaded)
        {
            remoteUrl.AssertNotNull("remoteUrl");
            localPath.AssertNotNull("localPath");

            RemoteUrl = remoteUrl;
            LocalPath = localPath;
            FileDownloaded = fileDownloaded;
        }
    }
}
