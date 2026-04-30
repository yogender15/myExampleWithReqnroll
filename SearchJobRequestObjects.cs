using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POMSeleniumFrameworkPoc1.APIObjects
{
   public class SearchJobRequestObjects
    {
        public string Search { get; set; }
        public int Top { get; set; }
        public bool UseFuzzy { get; set; }
        public string SuggestType { get; set; }
    }
}
