using System.Collections.Generic;

namespace github_webscraping.Models
{
    public class DataReturnRepository
    {
        public DataReturnRepository()
        {
            this.GitHubFiles = new List<GitHubFile>();
        }

        public IList<GitHubFile> GitHubFiles { get; protected set; }

        public void AddFile(GitHubFile gitHubFile) => GitHubFiles.Add(gitHubFile);

    }
}
