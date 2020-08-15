using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace github_webscraping.Models
{
    public class GitHubFile
    {
        public GitHubFile(string fileUrl)
        {
            this.FileUrl = fileUrl;
        }

        public string FileName { get; protected set; }
        public string FileUrl { get; protected set; }
        //public string FileLocationPath { get; protected set; }
        public string FileExtension { get; protected set; }

        public byte FileBytes { get; protected set; }
        public int FileLines { get; protected set; }


        private void SetFileByte(byte fileByte)
        {
            this.FileBytes = fileByte;
        }

        private void SetFileLines(int fileLines)
        {
            this.FileLines = fileLines;
        }
    }
}
