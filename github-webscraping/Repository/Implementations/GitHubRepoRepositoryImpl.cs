using github_webscraping.Models;
using github_webscraping.Shared;
using HtmlAgilityPack;
using System;
using System.Linq;

namespace github_webscraping.Repository.Implementations
{
    public class GitHubRepoRepositoryImpl : IGitHubRepoRepository
    {

        public GitHubRepoRepositoryImpl()
        {
            this.GlobalGitHubRepo = new GitHubBaseRepo();
        }

        public string RootBaseUrl { get; set; }
        public GitHubBaseRepo GlobalGitHubRepo;

        public GitHubBaseRepo RepositoryMapping(string baseUrl)
        {
            var baseRepo = new GitHubBaseRepo();
            baseRepo.SetBaseUrl(baseUrl);
            var web = new HtmlWeb();
            var htmlDoc = web.Load(baseRepo.BaseUrl);

            var linkRepoOrFile = htmlDoc.DocumentNode.Descendants(0).Where(n => n.HasClass($"js-navigation-open") && n.HasClass("link-gray-dark"));

            foreach (HtmlNode node in linkRepoOrFile)
            {
                if (IsPaste(node.Attributes["href"].Value))
                {
                    baseRepo.AddPaste(new GitHubPaste($"{Constants.GIT_URL_BASE}{node.Attributes["href"].Value}"));
                }
                else
                {
                    GlobalGitHubRepo.AddFile(new GitHubFile($"{Constants.GIT_URL_BASE}{node.Attributes["href"].Value}"));
                    baseRepo.AddFile(new GitHubFile($"{Constants.GIT_URL_BASE}{node.Attributes["href"].Value}"));
                }
            }
            if (baseRepo.PastesUrl.Count != 0)
            {
                foreach (var paste in baseRepo.PastesUrl)
                {
                    RepositoryMapping(paste.Url);
                }
            }
            return GlobalGitHubRepo;
        }

        private void RecursiveFactor(GitHubBaseRepo gitHubBaseRepo)
        {
            var pastes = gitHubBaseRepo.PastesUrl;
            var files = gitHubBaseRepo.GitHubFiles;
            foreach (var paste in pastes)
            {
                var a = RepositoryMapping(paste.Url);
            }

        }

        private bool IsPaste(string url)
        {
            if (url.Contains("tree"))
                return true;
            else
                return false;

        }

        public void GetInfoArchive()
        {
            throw new NotImplementedException();
        }
    }
}
