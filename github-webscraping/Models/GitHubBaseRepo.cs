using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace github_webscraping.Models
{
    public class GitHubBaseRepo
    {
        public GitHubBaseRepo()
        {
            PastesUrl = new List<GitHubPaste>();
            GitHubFiles = new List<GitHubFile>();
        }

        public string BaseUrl { get; protected set; }
        public IList<GitHubPaste> PastesUrl { get; protected set; }
        public IList<GitHubFile> GitHubFiles { get; protected set; }


        public void AddPaste(GitHubPaste paste)
        {
            PastesUrl.Add(paste);
        }

        public void AddFile(GitHubFile gitHubFile)
        {
            GitHubFiles.Add(gitHubFile);
        }

        public void SetBaseUrl(string baseUrl)
        {
            this.BaseUrl = baseUrl;
        }
        public void RemoveFile(GitHubFile gitHubFile)
        {
            for (var i = 0; i < GitHubFiles.Count; i++)
            {
                if (GitHubFiles.ElementAt(i).FileName == gitHubFile.FileName)
                {
                    GitHubFiles.RemoveAt(i);
                }
            }
        }
    }
}
