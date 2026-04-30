using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace POMSeleniumFrameworkPoc1.Helpers
{
    class DBConnectionUtility
    {


        public static bool TestConnection()
        {
            using (NpgsqlConnection con = GetConnection())
            {
                con.Open();
                if (con.State == System.Data.ConnectionState.Open)
                {
                    Console.WriteLine("Connected");
                    return true;
                }
                else
                    return false;
            }
        }
        public static NpgsqlConnection GetConnection()
        {
            string userDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string[] UserDirectoryParts = userDirectory.Split(new string[] { "\\" }, StringSplitOptions.None);
            
            string userID = UserDirectoryParts[2];
            Console.Write("userDirectory : " + userDirectory);
            Console.Write("UserDirectoryParts : " + UserDirectoryParts);
            Console.Write("userID : "+ userID);
            string serverName = ConfigurationManager.AppSettings["DBServer"];
            string portName = ConfigurationManager.AppSettings["DBPort"];
            string DBName = ConfigurationManager.AppSettings["DBName"];
            // string password = ConfigurationManager.AppSettings["DBPassword"];
            string password = "";
            // ConfigurationManager.AppSettings["DBPassword"];
            //string userID = "RES-PVDB_DB_User_T1_Test-US";
            string accessToken = "";//GetAccessToken();
            string connectionString = "";


                if (Config.EnvironmentVal == "DEV" || Config.EnvironmentVal == "WAVE")
                {
                    userID = "RES-PVDB_DB_User_T1_Dev-US";
                    accessToken = GetAccessToken();
                    serverName = "bst-iaz-pgsql-dev-dal.postgres.database.azure.com";
                    connectionString = $"Server={serverName};Port={portName};User Id={userID};Password={accessToken};Database={DBName};";

                }
                else
                {

                    userID = "RES-PVDB_DB_User_T1_Test-US";
                    accessToken = GetAccessToken();
                    connectionString = $"Server={serverName};Port={portName};User Id={userID};Password={accessToken};Database={DBName};";
                }
                

            Console.WriteLine($"connectionString : {connectionString.Trim()}");

            return new NpgsqlConnection(@connectionString);
            //  return new NpgsqlConnection(@"Server={serverName};Port={portName};User Id=_8904256;Password={Password};Database={DBName};");

        }

        public static string GetAccessToken()
        {
            string access_Token = "";
            try
            {
                // Define the Azure CLI command to get the access token
                string command = "az account get-access-token --resource https://ossrdbms-aad.database.windows.net --query \"[accessToken]\" -o tsv";
                // Create a new process to run the command
                Process process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe", // Use "bash" if you're on Linux/Mac
                        Arguments = $"/c {command}",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                // Start the process
                process.Start();

                // Capture the output
                access_Token = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                // Wait for the process to exit
                process.WaitForExit();

                // Check for errors
                if (!string.IsNullOrEmpty(error))
                {
                    Console.WriteLine($"Error: {error}");
                }
                else
                {
                    Console.WriteLine($"Azure Access Token: {access_Token.Trim()}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
            return access_Token.Trim();
        }
      }
}

