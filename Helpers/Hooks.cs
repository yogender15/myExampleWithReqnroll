using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Model;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Config;
using OpenQA.Selenium;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reqnroll ;
using Reqnroll .Infrastructure;
using Log = Serilog.Log;


namespace POMSeleniumFrameworkPoc1.Helpers
{
    [Binding]
    public class Hooks : DriverHelper
    {
        //Extent Report code
        public static ExtentReports extent = new ExtentReports();
        private static ExtentTest extentTest;
        private static ExtentTest scenarioTest;
        public static string testResultsRootPath;


        private readonly IReqnrollOutputHelper _specFlowOutputHelper;
        public  ScenarioContext _scenarioContext;
        public FeatureContext _featureContext;

        public Hooks(IReqnrollOutputHelper specFlowOutputHelper , ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            _specFlowOutputHelper = specFlowOutputHelper;
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + Config.LogFilePath);

            LoggingLevelSwitch levelSwitch = new LoggingLevelSwitch(LogEventLevel.Information);
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}").CreateLogger();
            //     .MinimumLevel.ControlledBy(levelSwitch)
            //  .WriteTo.File(logFilePath, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} | {Level:u3} | {Message} {NewLine}",
            //    rollingInterval: RollingInterval.Day).CreateLogger();

        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext context)
        {
            Log.Information("Selecting feature File {0} to run", context.FeatureInfo.Title);

        }

        [BeforeScenario("UI")]
        public void BeforeScenario()
        {
            switch (Config.BrowserType)
            {
                case "chrome":
                    CloseExistingChromeInstances();
                    break;
                case "edge":
                    CloseExistingEdgeInstances();
                    break;
                case "EdgeGrid":
                    CloseExistingChromeInstances();
                    break;
            }

            Driver = DriverInitiation();
            SetbaseURL();

            //Extent Report code
            testResultsRootPath = AppDomain.CurrentDomain.BaseDirectory + @"..\..\TestResults";
                //AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug", "TestResults");
            var sparkReporter = new ExtentSparkReporter(Path.Combine(testResultsRootPath, "Report.html"));
            sparkReporter.Config.ReportName = "Automation Status Report";
            sparkReporter.Config.DocumentTitle = "Automation Status Report";
            sparkReporter.Config.Theme = Theme.Dark;
            extent.AttachReporter(sparkReporter);

            scenarioTest = extentTest.CreateNode<Scenario>(ScenarioContext.Current.ScenarioInfo.Title);
        } 

        [AfterScenario("UI")]
        public void AfterScenario()
        {
            if (ScenarioContext.Current.TestError != null)
            {
                TakeScreenShot(); 
            }
            //extent.Flush();
            DriverDispose();
            Log.Information("The driver has been disposed");

        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            //string userDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            //string batchFile = @"VOA_Automation\CTAzureAutomationRepo\BSTVOAQAAutomation\BSTVOAQAAutomation\bin\Debug\CreateHTMLReport.bat";
            // string batchFilePath = Path.Combine(userDirectory,batchFile);
            //string userDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            //String baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            //string batchFile = @"VOA_Automation\CTAzureAutomationRepo\BSTVOAQAAutomation\BSTVOAQAAutomation\bin\Debug\CreateHTMLReport.bat";
            //string batchFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "CreateHTMLReport.bat");
           // Process.Start(batchFilePath);
        }

        [AfterStep()]
        public void TakeScreenShotAfterEachStep()
        {
            if (DriverHelper.Driver is ITakesScreenshot screenshottaker)
            {
                string userDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string screenShotFolder = Path.Combine(userDirectory, @"VOA_Automation\CTAzureAutomationRepo\BSTVOAQAAutomation\BSTVOAQAAutomation\ScreenShots");

                string screenshotFilePath = Path.Combine(screenShotFolder, DateTime.Now.ToString("ssfff") + $"{ScenarioContext.Current.ScenarioInfo.Title.Replace(" ", "_")}_screenshot.png");
                string filename = Path.ChangeExtension(Path.GetRandomFileName(), "png");
                //  screenshottaker.GetScreenshot().SaveAsFile(filename);
                //  _specFlowOutputHelper.AddAttachment(filename);
            }

        }
        private void CloseExistingChromeInstances()
        {

            var chromeProcesses = Process.GetProcessesByName("chrome");
            foreach (var process in chromeProcesses)
            {
                try
                {
                    process.Kill();
                    process.WaitForExit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error closing chrome process: {ex.Message}");
                }
            }
        }

        private void CloseExistingEdgeInstances()
        {

            var edgeProcesses = Process.GetProcessesByName("msedge");
            foreach (var process in edgeProcesses)
            {
                try
                {
                    process.Kill();
                    process.WaitForExit();
                    process.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error closing edge process: {ex.Message}");
                }
            }
        }

        private void SetbaseURL()
        {

            string propertiesFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + Config.EnvironmentValFilePath);
            PropertiesReader _propertiesReader = new PropertiesReader(propertiesFilePath);

          Config.BaseUrl = _propertiesReader.Get(Config.EnvironmentVal);
        }


        //Extent Report code

        [BeforeFeature]
        public static void BeforeFeature()
        {
            extentTest = extent.CreateTest<Feature>(FeatureContext.Current.FeatureInfo.Title.ToString());
        }

        [AfterStep]
        public static void AfterStep()
        {
            var scenarioInfo = ScenarioContext.Current;
            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
            if (scenarioInfo.TestError == null)
            {
                switch (stepType)
                {
                    case "Given":
                        scenarioTest.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
                        break;
                    case "When":
                        scenarioTest.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text);
                        break;
                    case "Then":
                        scenarioTest.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text);
                        break;
                    case "And":
                        scenarioTest.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text);
                        break;
                    default:
                        break;
                }
            }


            else
            {
                var screenshotPath = TakeScreenShot();
                switch (stepType)
                {
                    case "Given":
                        scenarioTest.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text)
                            .Fail(scenarioInfo.TestError.Message, MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
                        break;
                    case "When":
                        scenarioTest.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text)
                            .Fail(scenarioInfo.TestError.Message, MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
                        break;
                    case "Then":
                        scenarioTest.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text)
                            .Fail(scenarioInfo.TestError.Message, MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
                        break;
                    case "And":
                        scenarioTest.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text)
                            .Fail(scenarioInfo.TestError.Message, MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
                        break;
                    default:
                        break;
                }

            }
        }
    }
}
