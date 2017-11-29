using FFS.BE.Data;
using GenericCore.Support.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFS.DAL
{
    public abstract class AbstractDataRetriever
    {
        public DataResult RetrieveData(string localFolder)
        {
            string localPath = GetLocalPath(localFolder);
            string url = GetUrlToDownloadFile();
            bool fileDownloaded = WebPageDataRetriever.DownloadFile(url, localPath);
            return new DataResult(localPath, fileDownloaded);
        }

        protected abstract string GetUrlToDownloadFile();
        protected abstract string GetLocalPath(string localFolder);
    }
}
