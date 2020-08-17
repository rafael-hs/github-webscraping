using github_webscraping.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace github_webscraping.Repository
{
    public interface IGitHubMappingRepository
    {
        IList<GitHubFile> RepositoryMapping(string baseUrl);
    }
}
