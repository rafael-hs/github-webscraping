using github_webscraping.Models;
using System.Collections.Generic;
using System.Linq;

namespace github_webscraping.Business
{
    public interface IGitHubRepoBusiness
    {
        IList<(string extension, int lines, float bytes, IGrouping<string, GitHubFile> files)> RepositoryMapping(string baseUrl);
    }
}
