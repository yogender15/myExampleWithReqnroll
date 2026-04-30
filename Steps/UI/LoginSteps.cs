using POMSeleniumFrameworkPoc1.Helpers;
using POMSeleniumFrameworkPoc1.Pages;
using Reqnroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POMSeleniumFrameworkPoc1.Steps.UI
{
    [Binding]
    class LoginSteps : BasePage
    {
        [Given("encrypt and decrypt given password")]
        public void GivenEncryptAndDecryptGivenPassword()
        {
            try
            {
                String s = passwordEncryptor.Encrypt("Newpassword04!").ToString();
                Console.WriteLine($"Encrypted password : " + s);
                String des = passwordEncryptor.Decrypt(s);
                Console.WriteLine($"Decrypted password : " + des);
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
        [Given("User logged out from Dynamics application")]
        public void GivenUserLoggedOutFromDynamicsApplication()
        {
            try
            {
                LoginPage loginPage = new LoginPage();
                loginPage.LogoutFromApp();
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

        [Given("Login to Dynamics application with {string} user")]
        public void GivenLoginToDynamicsApplicationWithUser(string userRole)
        {
            try
            {
                LoginPage loginPage = new LoginPage();
                loginPage.loginToApp(userRole);
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

        [Given("Login to Dynamics application with {string} user to work for {string} case")]
        public void GivenLoginToDynamicsApplicationWithUserToWorkForCase(string userRole, string pdfFileName)
        {
            try
            {

                LoginPage loginPage = new LoginPage();
                loginPage.loginToApp(Config.BaseUrl, userRole, pdfFileName);
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

        [Given(@"User is logged in to Dynamics application to work for ""(.*)"" case")]
        public void GivenUserIsLoggedInToDynamicsApplicationToWorkForCase(string caseName)
        {
            PDF_Utility pdf_util = new PDF_Utility();
            pdf_util.initializeScreenshotsFile(caseName);
            var loginPage = new LoginPage();
            loginPage.GoToLoginPage(Config.BaseUrl);
            if (Config.BrowserType.Equals("edge"))
            {

            }
            else
            {
                loginPage.LoginWithExistingUser();
            }

        }

        [Given(@"User closes browser")]
        public void GivenUserClosesBrowser()
        {
            PDF_Utility pdf_util = new PDF_Utility();
            pdf_util.finalizeScreenshotsFile();
        }




    }
}
