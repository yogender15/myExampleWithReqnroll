using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using POMSeleniumFrameworkPoc1.Helpers;
using POMSeleniumFrameworkPoc1.Pages.UI.RBSP_BP;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace POMSeleniumFrameworkPoc1.Pages
{
    public class LoginPage : BasePage
    {
        waitHelpers wh = new waitHelpers();
        public IWebElement select_existing_user =>
            Driver.WaitForElement(By.XPath("(*//*[contains(@class,'table')]/*[contains(@class,'content')])[1]"));
        public IWebElement UserNameElement => Driver.WaitForElement(By.CssSelector("input[type='email']"));
        public IWebElement PasswordElement => Driver.WaitForElement(By.CssSelector("input[type*='pass']"));
        public IWebElement NextElement => Driver.WaitForElement(By.CssSelector("input[type = 'submit']"));

        public IWebElement Search => Driver.WaitForElement(By.XPath("//a[contains(@class,'primary')and @href='/search']"));

        public IWebElement UPRNSearch => Driver.WaitForElement(By.CssSelector("[data-bs-title*='(UPRN) and find out its address.']"));

        public IWebElement UPRNtextbox => Driver.WaitForElement(By.CssSelector("[placeholder='Please enter a UPRN']"));

        public IWebElement FindUPRNbtn => Driver.WaitForElement(By.CssSelector("button[id='find_uprn']"));

        public By userProfileLogo => (By.CssSelector("button#mectrl_main_trigger"));
        public By signOut => (By.CssSelector("button#mectrl_body_signOut"));

        public By userProfileID = By.CssSelector("[id = 'mectrl_currentAccount_secondary']");
        public void GoToLoginPage(string url)
        {
            try
            {
                waitHelpers wh = new waitHelpers();
                CommonPage cp = new CommonPage();
                Driver.Navigate().GoToUrlAsync(url);
                cp.waitTillPageLoading(60);
                if (url.Contains("voabstsit.powerappsportals") || url.Contains("laportal-preprod"))
                {

                }
                else
                {
                    var appsListPage = new AppsListPage();
                    string userID = "";
                    string pwd = "";
                    foreach (KeyValuePair<string, string> userData in new userConfigHelper().getUserData("VOA Team Manager1"))
                    {
                        userID = userData.Key;
                        pwd = passwordEncryptor.Decrypt(userData.Value);
                        Console.WriteLine($"userID: {userID}");
                    }
                    appsListPage.loginToCTdynamicsWithOutLoggedInUserValidation(userID, pwd);
                    appsListPage.ClickCouncilTaxAppIcon();
                }
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
            }
            catch (Exception e)
            {
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
                pdf_Util.exceptionPdFLogger(e);
            }
        }

        public void GoToLoginPageWithSSO(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }

        public void GoToUATloginPage(string url)
        {
            Driver.Navigate().GoToUrl(url);
            waitHelpers wh = new waitHelpers();
            wh.clickOnElement(By.XPath("//button[text()='Continue']"));
            //SeleniumExtensions.WaitForElementAndClick(By.XPath("//button[text()='Continue']"));
        }

        public void LoginWithExistingUser()
        {
            if (SeleniumExtensions.ElementVisisbleUsingExplicitWait(select_existing_user, 2) == true)
            {
                select_existing_user.Click();
            }
            try
            {
                var isHomePageDisplayed = Driver.WaitForElement(By.CssSelector("a[aria-label='Skip to main content']")).Displayed;
            }
            catch
            {
                Driver.Url = Driver.Url.Replace("https://", $"https://{Config.Username}:{Config.Password}@");
            }

        }


        public void LogoutFromApp()
        {
            wh.elementClickableAndDisplayed(userProfileLogo);
            wh.clickOnElement(userProfileLogo);
            wh.elementToClickble(signOut);
            wh.jsClickOnElement(signOut);
            string title = "Sign in to your account";
            wh.waitTillTitle(title, 60);
            Thread.Sleep(5000);
        }

        public void loginToApp(string userRole)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(60));
            wait.Until(d =>
                ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete")
            );
            Console.WriteLine("Page fully loaded.");
            string userID = "";
            string pwd = "";
            foreach (KeyValuePair<string, string> userData in new userConfigHelper().getUserData(userRole))
            {
                userID = userData.Key;
                pwd = passwordEncryptor.Decrypt(userData.Value);
                Console.WriteLine($"userID: {userID}");
            }
            waitHelpers wh = new waitHelpers();
            var appsListPage = new AppsListPage();
            appsListPage.loginToCTdynamics(userID, pwd);
            appsListPage.ClickCouncilTaxAppIcon();
            appsListPage.getCurrentLandingScreen();
        }

        public void loginToApp_LAPortal(string userRole)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(60));
            wait.Until(d =>
                ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete")
            );
            Console.WriteLine("Page fully loaded.");
            string userID = "";
            string pwd = "";
            foreach (KeyValuePair<string, string> userData in new userConfigHelper().getUserData(userRole))
            {
                userID = userData.Key;
                pwd = passwordEncryptor.Decrypt(userData.Value);
                Console.WriteLine($"userID: {userID}");
            }
            waitHelpers wh = new waitHelpers();
            var appsListPage = new AppsListPage();
            appsListPage.loginToLAportal(userID, pwd);
        }



        public void loginToApp(string url, string userRole, string pdfFileName)
        {
            PDF_Utility pdf_util = new PDF_Utility();
            pdf_util.initializeScreenshotsFile(pdfFileName);
            waitHelpers wh = new waitHelpers();
            var appsListPage = new AppsListPage();
            CommonPage cp = new CommonPage();
            Driver.Navigate().GoToUrlAsync(url);
            cp.waitTillPageLoading(60);
            string userID = "";
            string pwd = "";
            foreach (KeyValuePair<string, string> userData in new userConfigHelper().getUserData(userRole))
            {
                userID = userData.Key;
                pwd = passwordEncryptor.Decrypt(userData.Value);
                Console.WriteLine($"userID: {userID}");
            }


            appsListPage.loginToCTdynamics(userID, pwd);
            appsListPage.ClickCouncilTaxAppIcon();
            //wh.clickOnElement(By.XPath("//button[text()='Continue']"));


        }


        public bool validateRequiredUser(string userID)
        {
            bool requiredUser = false;
            string requiredUsertxt = "";
            try
            {
                if (wh.elementClickableAndDisplayed(userProfileLogo))
                {
                    wh.clickOnElement(userProfileLogo);
                    wh.GetWebElement(userProfileID);
                    requiredUsertxt = wh.getElementText(userProfileID);
                    Console.WriteLine($"user mail ID : {requiredUsertxt}");
                    if (userID.Equals(requiredUsertxt))
                    {
                        requiredUser = true;
                    }
                    else
                        requiredUser = false;
                }
                else
                {
                    requiredUser = true;
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Exception : {e.Message} in {MethodBase.GetCurrentMethod().Name}");
            }

            return requiredUser;
        }
    }
}
