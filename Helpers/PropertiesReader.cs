using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POMSeleniumFrameworkPoc1.Helpers
{
    class PropertiesReader
    {
        private readonly Dictionary<string, string> _properties;

        public PropertiesReader(string filePath)
        {

            _properties = new Dictionary<string, string>();

            foreach (var line in File.ReadAllLines(filePath))
            {
                if (line.Trim().Length == 0 || line.StartsWith("#"))
                {
                    continue;
                }

                var separatorIndex = line.IndexOf('=');
                if (separatorIndex > 0)
                {
                    var key = line.Substring(0, separatorIndex).Trim();
                    var value = line.Substring(separatorIndex + 1).Trim();
                    _properties[key] = value;
                }

            }
            //   _properties = new Dictionary<string, string>();
            //   ReadProperties(filePath);
        }

        // ***********************************
        //private void ReadProperties(string filePath) 
        //{
        //    var lines = File.ReadAllLines(filePath);
        //    StringBuilder currentValue = new StringBuilder();
        //    string currentKey = null;

        //    foreach(var line in lines) 
        //    {
        //        if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#")) 
        //        {
        //            continue;
        //        }
        //        if (line.Contains("="))
        //        {
        //            if (currentKey != null)
        //            {
        //                _properties[currentKey] = currentValue.ToString().Trim();
        //                currentValue.Clear();
        //            }

        //            var separatorIndex = line.IndexOf('=');
        //            currentKey = line.Substring(0, separatorIndex).Trim();
        //            currentValue.Append(line.Substring(separatorIndex + 1).Trim());
        //        }
        //        else if (currentKey != null) 
        //        {
        //            currentValue.Append(" " + line.Trim());
        //        }
        //    }
        //    if (currentKey != null) 
        //    {
        //        _properties[currentKey] = currentValue.ToString().Trim();
        //    }
        //}




        public string Get(string key)
        {
            return _properties.ContainsKey(key) ? _properties[key] : null;
        }
    }
}
