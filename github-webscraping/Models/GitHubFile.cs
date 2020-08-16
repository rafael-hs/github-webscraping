namespace github_webscraping.Models
{
    public class GitHubFile
    {
        public GitHubFile(string url, string name, string extension, float bytes, int lines)
        {
            this.FileUrl = url;
            this.FileName = name;
            this.Extension = extension;
            this.FileBytes = bytes;
            this.FileLines = lines;
        }

        public string FileName { get; protected set; }
        public string FileUrl { get; protected set; }
        public string Extension { get; protected set; }

        public float FileBytes { get; protected set; }
        public int FileLines { get; protected set; }
    }
}
