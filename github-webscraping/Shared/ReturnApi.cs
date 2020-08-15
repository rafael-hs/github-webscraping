using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace github_webscraping.Shared
{
    public static class ReturnApi
    {
        public static string Empty { get => "Sorry, but input is Empty"; }
        public static string NotFound { get => "Sorry, this repository not found in GitHub"; }
        public static string InternalError { get => "Sorry, an internal error has occurred"; }
       
    }
}
