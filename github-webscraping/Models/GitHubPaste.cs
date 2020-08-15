using System.Collections.Generic;

namespace github_webscraping.Models
{
    public class GitHubPaste
    {
        public GitHubPaste(string url)
        {
            this.Url = url;
        }

        public string Url { get; protected set; }
        public IList<GitHubPaste> gitHubPastes { get; protected set; }
        public IList<GitHubFile> GitHubFiles { get; protected set; }

        public void AddPaste(GitHubPaste paste)
        {
            gitHubPastes.Add(paste);
        }

        public void AddFile(GitHubFile gitHubFile)
        {
            GitHubFiles.Add(gitHubFile);
        }
    }
}
