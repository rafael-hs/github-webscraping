using github_webscraping.Models;
using System.Collections.Generic;

namespace github_webscraping.Repository
{
    public interface IGitHubRepoRepository
    {
        void GetInfoArchive();
        GitHubBaseRepo RepositoryMapping(string baseUrl);
    }
}
