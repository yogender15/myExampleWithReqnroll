using Serilog;
using System;
using System.IO;
using System.Linq;
using Reqnroll;
using Reqnroll.Infrastructure;
using Log = Serilog.Log;


namespace POMSeleniumFrameworkPoc1.Helpers
{
    [Binding]
    public class Hooks
    {
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

            Directory.CreateDirectory(
                AppDomain.CurrentDomain.BaseDirectory + @"..\..\TestResults");

            SetBaseUrl();
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext context)
        {
            Log.Information("Selecting feature file: {Feature}", context.FeatureInfo.Title);
        }

        [BeforeScenario("UI")]
        public void BeforeScenario()
        {
            DriverHelper.Driver = DriverHelper.DriverInitiation();

            var title = _scenarioContext.ScenarioInfo.Title;
            var safeName = new string(title.Take(60).ToArray())
                .Replace(":", "").Replace("/", "").Replace("\\", "").Replace("\"", "");
            var pdf_Util = new PDF_Utility();
            pdf_Util.initializeScreenshotsFile(safeName);
        }

        [AfterScenario("UI")]
        public void AfterScenario()
        {
            var pdf_Util = new PDF_Utility();
            pdf_Util.finalizeScreenshotsFile();

            DriverHelper.DriverDispose();
            Log.Information("Driver disposed");
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            Log.CloseAndFlush();
        }

        [BeforeStep]
        public void BeforeStep()
        {
            DriverHelper.DismissSignInPopupIfPresent();
        }

        [AfterStep]
        public void AfterStep()
        {
            var pdf_Util = new PDF_Utility();
            pdf_Util.takeScreenshot();

            if (_scenarioContext.TestError != null)
                pdf_Util.loggerInNewPage(_scenarioContext.TestError.ToString());
        }

        private static void SetBaseUrl()
        {
            string propertiesFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + Config.EnvironmentValFilePath);
            PropertiesReader propertiesReader = new PropertiesReader(propertiesFilePath);
            Config.BaseUrl = propertiesReader.Get(Config.EnvironmentVal);
        }
    }
}
