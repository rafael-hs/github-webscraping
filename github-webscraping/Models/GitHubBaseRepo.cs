using System.Collections.Generic;

namespace github_webscraping.Models
{
    public class GitHubBaseRepo
    {
        public GitHubBaseRepo()
        {
            this.PastesUrl = new List<string>();
        }

        public string BaseUrl { get; protected set; }
        public IList<string> PastesUrl { get; protected set; }

        public void AddPaste(string paste)
        {
            PastesUrl.Add(paste);
        }

        public void SetBaseUrl(string baseUrl)
        {
            this.BaseUrl = baseUrl;
        }

    }
}
