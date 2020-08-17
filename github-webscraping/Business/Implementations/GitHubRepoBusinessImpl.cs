using github_webscraping.Models;
using github_webscraping.Repository;
using System.Collections.Generic;
using System.Linq;

namespace github_webscraping.Business.Implementations
{
    public class GitHubRepoBusinessImpl : IGitHubRepoBusiness
    {
        private IGitHubMappingRepository _gitHubMappingRepository;

        public GitHubRepoBusinessImpl(
            IGitHubMappingRepository gitHubRepoRepository)
        {
            _gitHubMappingRepository = gitHubRepoRepository;
        }

        public IList<(string extension, int lines, float bytes, IGrouping<string, GitHubFile> files)> RepositoryMapping(string baseUrl)
        {
            var files =  _gitHubMappingRepository.RepositoryMapping(baseUrl);
            var Grouped = MapData(files);
            return Grouped;
        }

        private IList<(string extension, int lines, float bytes, IGrouping<string, GitHubFile> files)> MapData(IList<GitHubFile> gitHubFiles)
        {
            var filesGrouped = gitHubFiles.GroupBy(f => f.Extension);
            var Grouped = new List<(string extension, int lines, float bytes, IGrouping<string, GitHubFile> files)>();

            foreach (var fileGrouped in filesGrouped)
            {
                int lines = 0;
                float bytes = 0;
                fileGrouped.ToList().ForEach(f => lines += f.FileLines);
                fileGrouped.ToList().ForEach(f => bytes += f.FileBytes);
                Grouped.Add((extension: fileGrouped.Key, lines, bytes, files: fileGrouped));
            }

            return Grouped;
        }
    }
}
