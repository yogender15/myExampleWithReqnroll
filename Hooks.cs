using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Config;
using OpenQA.Selenium;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.IO;
using Reqnroll ;
using Log = Serilog.Log;
using System.Threading;

namespace POMSeleniumFrameworkPoc1.Helpers
{
    [Binding]
    public class Hooks
    {
        public static ExtentReports extent = new ExtentReports();
        private static ThreadLocal<ExtentTest> featureTest = new ThreadLocal<ExtentTest>();
        private static ThreadLocal<ExtentTest> scenarioTest = new ThreadLocal<ExtentTest>();
        public static string testResultsRootPath;
       
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;
        
        public Hooks(ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            // Logging setup
            string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + Config.LogFilePath);
            LoggingLevelSwitch levelSwitch = new LoggingLevelSwitch(LogEventLevel.Information);
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.File(logFilePath)
                .CreateLogger();

            //Extent Report code
            //testResultsRootPath = AppDomain.CurrentDomain.BaseDirectory + @"..\..\TestResults";
            testResultsRootPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\TestResults");
            var sparkReporter = new ExtentSparkReporter(Path.Combine(testResultsRootPath, "Report.html"));
            sparkReporter.Config.ReportName = "Automation Status Report";
            sparkReporter.Config.DocumentTitle = "Automation Status Report";
            sparkReporter.Config.Theme = Theme.Dark;
            extent.AttachReporter(sparkReporter);

            // Set base URL only once if environment doesn't change per scenario
            string propertiesFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Config.EnvironmentValFilePath);
            PropertiesReader _propertiesReader = new PropertiesReader(propertiesFilePath);
            Config.BaseUrl = _propertiesReader.Get(Config.EnvironmentVal);
        }

        [BeforeScenario("UI")]
        public void BeforeScenario()
        {

            DriverHelper.DriverInitiation();
            //SetbaseURL();

            if (featureTest.Value == null)
            {
                featureTest.Value = extent.CreateTest<Feature>(_featureContext.FeatureInfo.Title);
            }
            //Create Scenario under Feature
            scenarioTest.Value = featureTest.Value.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);
        } 

        [AfterScenario("UI")]
        public void AfterScenario()
        {
            if (_scenarioContext.TestError != null)
            {
                var screenshotPath = DriverHelper.TakeScreenShot();
                scenarioTest.Value.AddScreenCaptureFromPath(screenshotPath);
                Log.Error("Test failed! Screenshot captured: {ScreenShotPath}", screenshotPath);
            }
            //extent.Flush();
            DriverHelper.DriverDispose();
            Log.Information("The driver has been disposed");

        }

        [AfterStep()]
        public void TakeScreenShotAfterEachStep()
        {
            if (DriverHelper.Driver is ITakesScreenshot screenshottaker)
            {
                string userDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string screenShotFolder = Path.Combine(userDirectory, @"VOA_Automation\CTAzureAutomationRepo\BSTVOAQAAutomation\BSTVOAQAAutomation\ScreenShots");

                string screenshotFilePath = Path.Combine(screenShotFolder, DateTime.Now.ToString("ssfff") + $"{_scenarioContext.ScenarioInfo.Title.Replace(" ", "_")}_screenshot.png");
                string filename = Path.ChangeExtension(Path.GetRandomFileName(), "png");
                //  screenshottaker.GetScreenshot().SaveAsFile(filename);
                //  _specFlowOutputHelper.AddAttachment(filename);
            }

        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            extent.Flush();
            Log.CloseAndFlush();
        }

        private void SetbaseURL()
        {

            string propertiesFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + Config.EnvironmentValFilePath);
            PropertiesReader _propertiesReader = new PropertiesReader(propertiesFilePath);

          Config.BaseUrl = _propertiesReader.Get(Config.EnvironmentVal);
        }


        [AfterStep]
        public void AfterStep()
        {
            var scenarioInfo = _scenarioContext;
            var stepInfo = ScenarioStepContext.Current.StepInfo;
            var stepType = stepInfo.StepDefinitionType.ToString();
            var stepText = stepInfo.Text;
            if (scenarioInfo.TestError == null)
            {
                switch (stepType)
                {
                    case "Given":
                        scenarioTest.Value.CreateNode<Given>(stepText);
                        break;
                    case "When":
                        scenarioTest.Value.CreateNode<When>(stepText);
                        break;
                    case "Then":
                        scenarioTest.Value.CreateNode<Then>(stepText);
                        break;
                    case "And":
                        scenarioTest.Value.CreateNode<And>(stepText);
                        break;
                    default:
                        break;
                }
            }


            else
            {
                var screenshotPath = DriverHelper.TakeScreenShot();
                switch (stepType)
                {
                    case "Given":
                        scenarioTest.Value.CreateNode<Given>(stepText)
                            .Fail(scenarioInfo.TestError.Message, MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
                        break;
                    case "When":
                        scenarioTest.Value.CreateNode<When>(stepText)
                            .Fail(scenarioInfo.TestError.Message, MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
                        break;
                    case "Then":
                        scenarioTest.Value.CreateNode<Then>(stepText)
                            .Fail(scenarioInfo.TestError.Message, MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
                        break;
                    case "And":
                        scenarioTest.Value.CreateNode<And>(stepText)
                            .Fail(scenarioInfo.TestError.Message, MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
                        break;
                    default:
                        break;
                }

            }
        }
    }
}
