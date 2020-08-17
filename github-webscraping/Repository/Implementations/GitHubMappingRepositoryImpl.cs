using github_webscraping.Models;
using github_webscraping.Shared;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace github_webscraping.Repository.Implementations
{
    public class GitHubMappingRepositoryImpl : IGitHubMappingRepository
    {

        public GitHubMappingRepositoryImpl()
        {
            this.doc = new HtmlDocument();
            this.gitHubFiles = new List<GitHubFile>();
            this.web = new HtmlWeb();
        }

        public IList<string> pastesUrls;
        public HtmlDocument doc;
        public IList<GitHubFile> gitHubFiles;
        public HtmlWeb web;

        public IList<GitHubFile> RepositoryMapping(string baseUrl)
        {
            var doc = web.Load(baseUrl);
            pastesUrls = new List<string>();

            var linkRepoOrFile = doc.DocumentNode.Descendants(0)
                .Where(n =>
                       n.HasClass($"{Constants.RepositoryLinkClassInit}") &&
                       n.HasClass($"{Constants.RepositoryLinkClassEnd}"));

            foreach (HtmlNode node in linkRepoOrFile)
            {
                if (IsPaste(node.Attributes["href"].Value))
                {
                    pastesUrls.Add($"{Constants.GitUrlBase}{node.Attributes["href"].Value}");
                }
                else
                {
                    gitHubFiles.Add(FarmFile($"{Constants.GitUrlBase}{node.Attributes["href"].Value}"));
                }
            }
            if (pastesUrls.Count > 0)
            {
                foreach (var paste in pastesUrls)
                {
                    RepositoryMapping(paste);
                }
            }
            return gitHubFiles;
        }

        public GitHubFile FarmFile(string url)
        {
            var html = LoadStringUrl(url);
            doc.LoadHtml(html);
            var archiveName = doc.DocumentNode.Descendants(0).Where(n => n.HasClass($"{Constants.ClassPathFile}")).First().InnerText;
            var extension = Path.GetExtension(archiveName);
            var linesAndBytes = doc.DocumentNode.Descendants(0)
                .Where(n => n.HasClass($"{Constants.ClassInfoArchiveOne}") && n.HasClass($"{Constants.ClassInfoArchiveTwo}"))
                .First().InnerText;

            var lines = ReturnNumberLines(Regex.Match(linesAndBytes, @"(\d+) lines").Value);
            var size = ReturnSizeFileInBytes(linesAndBytes);

            return new GitHubFile(url, archiveName, extension, size, lines);
        }

        private string LoadStringUrl(string url)
        {
            string siteContent = string.Empty;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream responseStream = response.GetResponseStream())         
            using (StreamReader streamReader = new StreamReader(responseStream))
            {
                siteContent = streamReader.ReadToEnd();
            }

            return siteContent;
        }

        private int ReturnNumberLines(string linesInput)
        {
            var number = Regex.Match(linesInput, @"(\d+)").Value;
            if (string.IsNullOrEmpty(number))
                return 0;

            return int.Parse(number);
        }

        private float ReturnSizeFileInBytes(string bytes)
        {
            float bytesFloat = 0;
            if (bytes.Contains("KB"))
            {
                bytes = Regex.Match(bytes, @"(\d+)?.?(\d+) KB").Value;
                bytes = Regex.Match(bytes, @"(\d+)?.?(\d+)").Value;
                bytesFloat = float.Parse(bytes) * 1000;

            }
            else if (bytes.Contains("MB"))
            {
                bytes = Regex.Match(bytes, @"(\d+).(\d+) MB").Value;
                bytes = Regex.Match(bytes, @"(\d+).(\d+)").Value;
                bytesFloat = float.Parse(bytes) * 1000000;
            }
            else
            {
                bytes = Regex.Match(bytes, @"(\d+) Bytes").Value;
                bytes = Regex.Match(bytes, @"(\d+)").Value;
                bytesFloat = float.Parse(bytes);
            }

            return bytesFloat;
        }

        private bool IsPaste(string url)
        {
            if (url.Contains("tree"))
                return true;

            return false;
        }
    }
}
