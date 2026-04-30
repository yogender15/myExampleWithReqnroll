using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

[assembly: Parallelizable(ParallelScope.Fixtures)]
[assembly: LevelOfParallelism(2)]

namespace POMSeleniumFrameworkPoc1.Helpers
{
    public static class Config
    {
        static Config()
        {
            SITBaseUrl = ConfigurationManager.AppSettings["SITBaseUrl"];
            UATBaseUrl = ConfigurationManager.AppSettings["UATBaseUrl"];
            PPEBaseUrl = ConfigurationManager.AppSettings["PPEBaseUrl"];
            LAPortalBaseUrl = ConfigurationManager.AppSettings["LAPortalBaseUrl"];
            DefaultDriverTimeOut = double.Parse(ConfigurationManager.AppSettings["DefaultDriverTimeOut"].ToString());
            ReqClientId = ConfigurationManager.AppSettings["ReqClientId"].ToString();
            OrgId = ConfigurationManager.AppSettings["OrgId"].ToString();
            CrmOwinAuth = ConfigurationManager.AppSettings["CrmOwinAuth"].ToString();
            BaseApiUrl = ConfigurationManager.AppSettings["BaseApiUrl"].ToString();
            SearchJobRelativeUrl = ConfigurationManager.AppSettings["SearchJobRelativeUrl"].ToString();
            Username = ConfigurationManager.AppSettings["Username"];
            Password = ConfigurationManager.AppSettings["Password"];
            LAPortalUsername = ConfigurationManager.AppSettings["LAPortalUsername"];
            LAPortalPassword = ConfigurationManager.AppSettings["LAPortalPassword"];
            TestDataExcelFilePath = ConfigurationManager.AppSettings["TestDataExcelFilePath"];
            EnvironmentValue = Environment.GetEnvironmentVariable("TestEnvironment", EnvironmentVariableTarget.User);
            TestOutPutExcelPath = ConfigurationManager.AppSettings["TestOutPutExcelPath"];
            SQLQueryPropertiesFilePath = ConfigurationManager.AppSettings["SQLQueryPropertiesFilePath"];
            LogFilePath = ConfigurationManager.AppSettings["LogFilePath"];
            EnvironmentVal = ConfigurationManager.AppSettings["EnvironmentVal"];
            EnvironmentValFilePath = ConfigurationManager.AppSettings["EnvironmentValFilePath"];
            userFilePath = ConfigurationManager.AppSettings["userFilePath"];

            BrowserType = ConfigurationManager.AppSettings["BrowserType"];
            ScreenshotFolder = ConfigurationManager.AppSettings["ScreenshotFolder"];
            BaseProposedEffectiveDate = ConfigurationManager.AppSettings["BaseProposedEffectiveDate"];
        }
        public static string BaseUrl { get; set; }

        public static string SITBaseUrl { get; set; }
        public static string UATBaseUrl { get; set; }

        public static string PPEBaseUrl { get; set; }


        public static string EnvironmentValue { get; set; }
        public static string LAPortalBaseUrl { get; set; }
        public static double DefaultDriverTimeOut { get; set; }
        public static string ReqClientId { get; set; }
        public static string OrgId { get; set; }
        public static string CrmOwinAuth { get; set; }
        public static string BaseApiUrl { get; set; }
        public static string SearchJobRelativeUrl { get; set; }
        public static string Username { get; set; }
        public static string Password { get; set; }
        public static string LAPortalUsername { get; set; }
        public static string LAPortalPassword { get; set; }
        public static string TestDataExcelFilePath { get; set; }
        public static string TestOutPutExcelPath { get; set; }
        public static string SQLQueryPropertiesFilePath { get; set; }
        public static string LogFilePath { get; set; }
        public static string EnvironmentVal { get; set; }
        public static string EnvironmentValFilePath { get; set; }

        public static string userFilePath { get; set; }

        public static string BrowserType { get; set; }

        public static string ScreenshotFolder { get; set; }

        public static string BaseProposedEffectiveDate { get; set; }



    }


}
