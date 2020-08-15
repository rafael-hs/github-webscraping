using github_webscraping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace github_webscraping.Business
{
    public interface IGitHubRepoBusiness
    {
        GitHubBaseRepo RepositoryMapping(string baseUrl);
    }
}
