using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;
using Reqnroll;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace POMSeleniumFrameworkPoc1.Helpers
{
    public class DriverHelper
    {

        public static IWebDriver Driver;

        public static double DriverWait = 30;

        public IWebDriver DriverInitiation()
        {
            List<string> ls = new List<string>();
            switch (Config.BrowserType)
            {
                case "edge":
                    string userDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                    string[] UserDirectoryParts = userDirectory.Split(new string[] { "\\" }, StringSplitOptions.None);
                    string userID = UserDirectoryParts[2]; //"ServiceProfiles";
                    Console.Write("userDirectory : " + userDirectory);
                    Console.Write("UserDirectoryParts : " + UserDirectoryParts);
                    Console.Write("userID : " + userID);

                    var edgeoptions = new EdgeOptions();
                    //ls.Add("enable-automation");
                    edgeoptions.AddExcludedArguments(ls);
                    edgeoptions.AddArgument("disable-infobars");
                    edgeoptions.AddArgument("start-maximized");
                    edgeoptions.AddArgument("disable-web-security");
                    edgeoptions.AddArgument("ignore-certificate-errors");
                    edgeoptions.AddArgument("no-sandbox");
                    //edgeoptions.AddArgument("--window-size=1920,1080");
                    edgeoptions.AddArgument("--start-maximized");
                    edgeoptions.AddArgument("--disable-gpu");
                    edgeoptions.AddArgument($"user-data-dir={System.Environment.GetEnvironmentVariable("LOCALAPPDATA")}\\Microsoft\\Edge\\User Data");
                    //var service = EdgeDriverService.CreateDefaultService($"C:\\Users\\8902876\\Desktop\\Drivers\\edgedriver_win64\\msedgedriver.exe");
                    //Driver = new EdgeDriver($"C:\\Program Files (x86)\\Microsoft\\Edge\\Application",edgeoptions);
                    Driver = new EdgeDriver(EdgeDriverService.CreateDefaultService(), edgeoptions, System.TimeSpan.FromSeconds(360));
                    if (userID.Equals("ServiceProfiles"))
                    {
                        var screenWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
                        var screenHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
                        Console.WriteLine("screenWidth : "+ screenWidth);
                        Console.WriteLine("screenHeight : "+ screenHeight);
                        Driver.Manage().Window.Size = new System.Drawing.Size(screenWidth, screenHeight);
                    }
                    else
                    {
                        Driver.Manage().Window.Maximize();
                    }
                    Driver.Manage().Timeouts().PageLoad.Add(System.TimeSpan.FromSeconds(360));
                    return Driver;

                case "edge1":
                    var edgeoptions_1 = new EdgeOptions();
                    edgeoptions_1.AddExcludedArguments(ls);
                    edgeoptions_1.AddArgument("disable-infobars");
                    edgeoptions_1.AddArgument("start-maximized");
                    edgeoptions_1.AddArgument("ignore-certificate-errors");
                    Driver = new EdgeDriver();
                    Driver.Manage().Window.Maximize();
                    Driver.Manage().Timeouts().PageLoad.Add(System.TimeSpan.FromSeconds(360));
                    return Driver;
                case "edgeheadless":
                    edgeoptions = new EdgeOptions();
                    ls.Add("enable-automation");
                    edgeoptions.AddExcludedArguments(ls);
                    edgeoptions.AddArgument("disable-infobars");
                    edgeoptions.AddArgument("no-sandbox");

                    edgeoptions.AddArgument("--start-maximized");
                    edgeoptions.AddArgument("disable-web-security");
                    edgeoptions.AddArgument("ignore-certificate-errors");
                    edgeoptions.AddArgument($"user-data-dir={System.Environment.GetEnvironmentVariable("LOCALAPPDATA")}\\Microsoft\\Edge\\User Data");
                    edgeoptions.AddArguments("--headless=new");
                    edgeoptions.AddArgument("--window-size=1920,1080");

                    // Set device scale factor (e.g., 1.25 = 125% zoom)
                    //edgeoptions.AddArgument("--force-device-scale-factor=0.8");
                    Driver = new EdgeDriver(edgeoptions);
                    //IJavaScriptExecutor jse = Driver as IJavaScriptExecutor;
                    //jse.ExecuteScript("document.body.style.zoom='0.75'");
                    return Driver;
                case "newEdge":
                    var edgeoptions1 = new EdgeOptions();
                    ls.Add("enable-automation");
                    edgeoptions1.AddExcludedArguments(ls);
                    edgeoptions1.AddArgument("disable-infobars");
                    edgeoptions1.AddArgument("start-maximized");
                    edgeoptions1.AddArgument("disable-web-security");
                    edgeoptions1.AddArgument("ignore-certificate-errors");
                    edgeoptions1.AddArgument("no-sandbox");
                    //edgeoptions1.AddAdditionalEdgeOption("InPrivate", true);
                    edgeoptions1.AddArgument("inprivate");

                    //edgeoptions.AddArgument($"user-data-dir={System.Environment.GetEnvironmentVariable("LOCALAPPDATA")}\\Microsoft\\Edge\\User Data");
                    //Driver = new EdgeDriver($"C:\\Program Files (x86)\\Microsoft\\Edge\\Application",edgeoptions);
                    Driver = new EdgeDriver(edgeoptions1);
                    Driver.Manage().Window.Maximize();
                    return Driver;
                case "chrome":
                    var options = new ChromeOptions();
                    ls.Add("enable-automation");
                    options.AddExcludedArguments(ls);
                    options.AddArgument("disable-infobars");
                    options.AddArgument("start-maximized");
                    options.AddArgument("disable-web-security");
                    options.AddArgument("ignore-certificate-errors");
                    options.AddArgument($"user-data-dir={System.Environment.GetEnvironmentVariable("LOCALAPPDATA")}\\Google\\Chrome\\User Data");

                    Driver = new ChromeDriver(options);
                    Driver.Manage().Window.Maximize();
                    Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(DriverWait);
                    //Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(DriverWait);
                    return Driver;


                case "EdgeGrid":
                    var gridoptions = new ChromeOptions();
                    //gridoptions.AddAdditionalEdgeOption();
                    System.Environment.SetEnvironmentVariable("webdriver.chrome.driver", @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe");
                    gridoptions.BinaryLocation = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";
                    //ls.Add("enable-automation");
                    //gridoptions.AddArgument("--remote-debugging-pipe");

                    //gridoptions.AddExcludedArguments(ls);
                    //gridoptions.AddArgument("disable-infobars");
                    //gridoptions.AddArgument("start-maximized");
                    //gridoptions.AddArgument("disable-web-security");
                    //gridoptions.AddArgument("ignore-certificate-errors");
                    //gridoptions.AddArgument($"user-data-dir={System.Environment.GetEnvironmentVariable("LOCALAPPDATA")}\\Microsoft\\Edge\\User Data");
                    try
                    {
                        Driver = new RemoteWebDriver(new Uri("http://10.186.28.35:4444/wd/hub/"), gridoptions.ToCapabilities(), TimeSpan.FromSeconds(1000));
                    }
                    catch (Exception e)
                    {
                        Console.Write(e.InnerException);
                        Console.Write(e.StackTrace);
                    }
                    Driver.Manage().Window.Maximize();
                    return (RemoteWebDriver)Driver;
            }
            return Driver;
        }



        public static string TakeScreenShot()
        {
            try
            {
                var artifactDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Screenshots");
                if (!Directory.Exists(artifactDirectory))
                {
                    Directory.CreateDirectory(artifactDirectory);
                }

                ITakesScreenshot takesScreenshot = Driver as ITakesScreenshot;
                if (takesScreenshot == null)
                {
                    return null;
                }

                Screenshot screenshot = takesScreenshot.GetScreenshot();

                string screenshotFilePath = Path.Combine(artifactDirectory, DateTime.Now.ToString("ssfff") + $"{ScenarioContext.Current.ScenarioInfo.Title.Replace(" ", "_")}_screenshot.jpeg");
                //takesScreenshot.GetScreenshot().SaveAsFile(screenshotFilePath, ScreenshotImageFormat.Jpeg);
                screenshot.SaveAsFile(screenshotFilePath);
                Console.WriteLine($"the filepath is {screenshotFilePath}");
                return screenshotFilePath;
            }
            catch { return null; }
        }

        public static void DriverDispose()
        {
            try
            {
                if (Driver != null)
                {
                    Driver.Quit();
                    Driver = null;
                }
            }
            catch { }
        }
    }
}
