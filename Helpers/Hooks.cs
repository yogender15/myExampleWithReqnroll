using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Model;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Config;
using Serilog;
using System;
using System.IO;
using Reqnroll;
using Reqnroll.Infrastructure;
using Log = Serilog.Log;


namespace POMSeleniumFrameworkPoc1.Helpers
{
    [Binding]
    public class Hooks
    {
        public static ExtentReports extent = new ExtentReports();
        private static ExtentTest extentTest;
        private static ExtentTest scenarioTest;
        public static string testResultsRootPath;

        private readonly IReqnrollOutputHelper _specFlowOutputHelper;
        public ScenarioContext _scenarioContext;
        public FeatureContext _featureContext;

        public Hooks(IReqnrollOutputHelper specFlowOutputHelper, ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            _specFlowOutputHelper = specFlowOutputHelper;
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();

            testResultsRootPath = AppDomain.CurrentDomain.BaseDirectory + @"..\..\TestResults";
            Directory.CreateDirectory(testResultsRootPath);

            var sparkReporter = new ExtentSparkReporter(Path.Combine(testResultsRootPath, "Report.html"));
            sparkReporter.Config.ReportName = "Automation Status Report";
            sparkReporter.Config.DocumentTitle = "Automation Status Report";
            sparkReporter.Config.Theme = Theme.Dark;
            extent.AttachReporter(sparkReporter);

            SetBaseUrl();
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext context)
        {
            Log.Information("Selecting feature File {0} to run", context.FeatureInfo.Title);
            extentTest = extent.CreateTest<Feature>(context.FeatureInfo.Title.ToString());
        }

        [BeforeScenario("UI")]
        public void BeforeScenario()
        {
            DriverHelper.Driver = DriverHelper.DriverInitiation();
            scenarioTest = extentTest.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);
        }

        [AfterScenario("UI")]
        public void AfterScenario()
        {
            DriverHelper.DriverDispose();
            Log.Information("The driver has been disposed");
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            extent.Flush();
            Log.CloseAndFlush();
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
                }
            }
            else
            {
                var screenshotPath = DriverHelper.TakeScreenShot();
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
                }
            }
        }

        private static void SetBaseUrl()
        {
            string propertiesFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + Config.EnvironmentValFilePath);
            PropertiesReader propertiesReader = new PropertiesReader(propertiesFilePath);
            Config.BaseUrl = propertiesReader.Get(Config.EnvironmentVal);
        }
    }
}
