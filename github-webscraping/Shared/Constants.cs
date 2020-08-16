using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;

namespace github_webscraping.Shared
{
    public static class Constants
    {
        //string for use unique one instance in scope
        public const string RepositoryLinkClassInit = "js-navigation-open";
        public const string RepositoryLinkClassEnd = "link-gray-dark"; 
        public const string GitUrlBase = "https://github.com";
        public const string ClassPathFile = "final-path";
        public const string ClassInfoArchiveOne = "text-mono";
        public const string ClassInfoArchiveTwo = "f6";
        public const string ClassInfoArchiveThree = "flex-auto";
        public const string ClassInfoArchiveFour = "pr-3";
        public const string ClassInfoArchiveFive = "flex-order-2";
    }
}
