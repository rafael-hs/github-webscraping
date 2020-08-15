using github_webscraping.Models;
using github_webscraping.Repository;

namespace github_webscraping.Business.Implementations
{
    public class GitHubRepoBusinessImpl : IGitHubRepoBusiness
    {
        private IGitHubRepoRepository _gitHubRepoRepository;

       public GitHubRepoBusinessImpl(IGitHubRepoRepository gitHubRepoRepository)
        {
            _gitHubRepoRepository = gitHubRepoRepository;
        }

        public GitHubBaseRepo RepositoryMapping(string baseUrl)
        {
            var result = _gitHubRepoRepository.RepositoryMapping(baseUrl);
            return result;
        }
    }
}
