using FFS.DAL;
using GenericCore.Support;
using GenericCore.Support.Web;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FFS.DAL.FantaGazzetta
{
    public class FantaGazzettaDataRetriever : AbstractDataRetriever
    {
        const string FantaGazzettaUrl = "https://www.fantagazzetta.com/statistiche-serie-a";
        const string htmlMatchToken = "$('#toexcel').unbind('click')";

        protected override string GetUrlToDownloadFile()
        {
            HtmlNode node = GetNodeContainingUrl();
            string url = GetUrlFromNode(node);
            return url;
        }

        protected override string GetLocalPath(string localFolder)
        {
            localFolder =
                localFolder.IsNullOrEmpty()
                ? Path.GetTempFileName().Replace("tmp", "xlsx")
                : Path.Combine(localFolder, $"data_sheet_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx");

            string directoryName = Path.GetDirectoryName(localFolder);

            if (!Directory.Exists(directoryName))
            {
                throw new DirectoryNotFoundException($"The directory '{directoryName}' has not been found");
            }

            return localFolder;
        }

        private HtmlNode GetNodeContainingUrl()
        {
            string html = WebPageDataRetriever.RetrievePage(FantaGazzettaUrl);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            HtmlNode node =
                doc
                    .DocumentNode
                    .SelectNodes("//script")
                    .Where(x => x.OuterHtml.Contains(htmlMatchToken))
                    .FirstOrDefault();

            return node;
        }

        private string GetUrlFromNode(HtmlNode node)
        {
            node.AssertNotNull("node");

            Match match = Regex.Match(node.InnerHtml, @"\$\('#toexcel'\).+?\{.+?href\s=\s""(.+?)"".+?\}", RegexOptions.Singleline | RegexOptions.IgnoreCase);

            if (match.Groups.Count < 2)
            {
                throw new ArgumentException("Could not retrieve url");
            }

            string url = match.Groups[1].Value;

            return "https:" + url;
        }
    }
}
