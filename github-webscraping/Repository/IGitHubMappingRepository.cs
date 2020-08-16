using github_webscraping.Models;

namespace github_webscraping.Repository
{
    public interface IGitHubMappingRepository
    {
        DataReturnRepository RepositoryMapping(string baseUrl);
    }
}
