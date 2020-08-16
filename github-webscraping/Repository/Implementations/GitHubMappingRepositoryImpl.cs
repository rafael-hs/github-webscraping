using github_webscraping.Models;
using github_webscraping.Shared;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace github_webscraping.Repository.Implementations
{
    public class GitHubMappingRepositoryImpl : IGitHubMappingRepository
    {

        public GitHubMappingRepositoryImpl()
        {
            this.DataReturnRepository = new DataReturnRepository();
            this.web = new HtmlWeb();
        }

        public DataReturnRepository DataReturnRepository;
        public HtmlWeb web;
        public IList<string> pastesUrls;

        public DataReturnRepository RepositoryMapping(string baseUrl)
        {
            var htmlDoc = web.Load(baseUrl);
            pastesUrls = new List<string>();

            var linkRepoOrFile = htmlDoc.DocumentNode.Descendants(0)
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
                    var url = $"{Constants.GitUrlBase}{node.Attributes["href"].Value}";
                    DataReturnRepository.AddFile(FarmFile(url));
                }
            }

            if (pastesUrls.Count > 0)
            {
                foreach (var paste in pastesUrls)
                {
                    RepositoryMapping(paste);
                }
            }

            return DataReturnRepository;
        }

        private GitHubFile FarmFile(string url)
        {
            var fileDoc = web.Load(url).DocumentNode;
            var archiveName = fileDoc.Descendants(0).Where(n => n.HasClass($"{Constants.ClassPathFile}")).First().InnerText;
            var extension = Path.GetExtension(archiveName);
            var linesAndBytes = fileDoc.Descendants(0)
                .Where(n => n.HasClass($"{Constants.ClassInfoArchiveOne}") &&
                       n.HasClass($"{Constants.ClassInfoArchiveTwo}") &&
                       n.HasClass($"{Constants.ClassInfoArchiveThree}") &&
                       n.HasClass($"{Constants.ClassInfoArchiveFour}") &&
                       n.HasClass($"{Constants.ClassInfoArchiveFive}"))
                .First().InnerText;

            var lines = ReturnNumberLines(Regex.Match(linesAndBytes, @"(\d+) lines").Value);
            var size = ReturnSizeFileInBytes(linesAndBytes);

            return new GitHubFile(url, archiveName, extension, size, lines);
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
                bytes = Regex.Match(bytes, @"(\d+) KB").Value;
                bytes = Regex.Match(bytes, @"(\d+)").Value;
                bytesFloat = float.Parse(bytes) * 1000;

            }
            else if (bytes.Contains("MB"))
            {
                bytes = Regex.Match(bytes, @"(\d+) MB").Value;
                bytes = Regex.Match(bytes, @"(\d+)").Value;
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
