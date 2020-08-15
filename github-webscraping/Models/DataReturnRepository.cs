using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace github_webscraping.Models
{
    public class DataReturnRepository
    {
        public DataReturnRepository()
        {

        }

        public string Extention { get; set; }
        public int TotalLines { get; set; }
        public float TotalBytes { get; set; }
    }
}
