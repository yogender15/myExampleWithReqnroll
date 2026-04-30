using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace POMSeleniumFrameworkPoc1.Helpers
{
    class userConfigHelper
    {
        public Dictionary<string, Dictionary<string,string>> getAllUserDeatils() {
            Dictionary<string, Dictionary<string, string>> user = new Dictionary<string, Dictionary<string, string>>();
            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + Config.userFilePath); ;

            // Validate JSON file existence
            if (!File.Exists(jsonFilePath))
            throw new Exception("Error: JSON file not found.");

            try
            {
                // Read and parse JSON
                var users = JsonConvert.DeserializeObject<List<UserConfig>>(File.ReadAllText(jsonFilePath));

                if (users == null || users.Count == 0)
                    throw new Exception("No user data found in JSON.");



                foreach (var userData in users)
                {
                    Dictionary<string, string> userDetails = new Dictionary<string, string>();
                    userDetails.Add(userData.PID, userData.password);
                    user.Add(userData.userRole, userDetails);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Unable to fetch data from users json file.");

            }
            return user;
            }

        public Dictionary<string, string> getUserData(string userRole)
        {
            Dictionary<string, Dictionary<string, string>> allusersData = getAllUserDeatils();
            Dictionary<string, string> userData = new Dictionary<string, string>();
            foreach (KeyValuePair<string, Dictionary<string, string>> entry in allusersData)
            {
                if (entry.Key.Equals(userRole))
                {
                    userData = entry.Value;
                    break;
                }
            }
            return userData;
        }
    }
}
