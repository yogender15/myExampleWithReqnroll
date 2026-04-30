using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OpenQA.Selenium;
using POMSeleniumFrameworkPoc1.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace POMSeleniumFrameworkPoc1.Pages.UI.RBSP_BP
{
    public partial class AppsListPage : BasePage
    {
        waitHelpers wh = new waitHelpers();
        By emailbY = SeleniumExtensions.getLocator("//input[@type='email']");
        By passwordbY = SeleniumExtensions.getLocator("//input[@type='password']");
        By submitbY = SeleniumExtensions.getLocator("//input[@type='submit']");
        By DontShowAgainBy = SeleniumExtensions.getLocator("input[name = 'DontShowAgain']");
        By pickUpAccountBy = SeleniumExtensions.getLocator("//div[text()='Pick an account']");
        By SignInBy = SeleniumExtensions.getLocator("//div[text()='Sign in']");
        By dashBoardContinue = By.XPath("//button[text()='Continue']");
        By otherAccount = SeleniumExtensions.getLocator("[id ='otherTileText']");
        By staySignedInBy = By.XPath("//div[@role='heading' and text()=\"Stay signed in?\"]");

        public void ClickCouncilTaxAppIcon()
        {
            waitHelpers wh = new waitHelpers();
            if (wh.isElementDisplayed(IframeSelector, 5))
            {
                SeleniumExtensions.SwitchToIframe(IframeSelector);
                if (wh.isElementDisplayed(CouncilTaxAppLoc, 5))
                {
                    CouncilTaxApp.Click();
                }
                SeleniumExtensions.SwitchToDefaultFrame();
            }

        }

        public string getCurrentLandingScreen()
        {
            String screenAtUrlLaunch = "";
            string pageTitle = DriverHelper.Driver.Title;
            if (pageTitle.Equals("Sign in to your account"))
            {
                if (wh.isElementDisplayed(SignInBy, 10))
                {
                    screenAtUrlLaunch = "Login";
                    Console.WriteLine("User landed into " + screenAtUrlLaunch);
                }
                else if (wh.isElementDisplayed(pickUpAccountBy, 10))
                {
                    screenAtUrlLaunch = "PickUpAccount";
                    Console.WriteLine("User landed into " + screenAtUrlLaunch);

                }
            }
            else
            {
                if (wh.isElementDisplayed(dashBoardContinue, 10))
                {
                    screenAtUrlLaunch = "DashBoard";
                    wh.clickOnWebElement(dashBoardContinue);
                    Console.WriteLine("User landed into " + screenAtUrlLaunch);
                }
            }
            return screenAtUrlLaunch;
        }
        public void loginToCTdynamics(String userEmail, String pwd)
        {
            try
            {
                LoginPage loginPage = new LoginPage();
                By loggedInUser = SeleniumExtensions.getLocator($"//div[@id='tilesHolder']//div[@data-test-id=\"{userEmail}\"]");
                String screenAtUrlLaunch = getCurrentLandingScreen();


                switch (screenAtUrlLaunch)
                {
                    case "Login":
                        login(userEmail, pwd);
                        break;
                    case "PickUpAccount":
                        string currentScreen = "";
                        if (wh.isElementDisplayed(loggedInUser, 5))
                        {
                            currentScreen = "loggeinUser";
                            Console.WriteLine("User landed into " + currentScreen);
                            wh.clickOnWebElement(loggedInUser);
                        }
                        else
                        {
                            currentScreen = "UseAnotherAccount";
                            Console.WriteLine("User landed into " + currentScreen);
                        }
                        switch (currentScreen)
                        {
                            case "loggeinUser":
                                login(pwd);
                                break;
                            case "UseAnotherAccount":
                                wh.clickOnElement(otherAccount);
                                login(userEmail, pwd);
                                break;
                        }
                        break;
                    case "DashBoard":
                        loginPage.LogoutFromApp();
                        wh.clickOnElement(otherAccount);
                        login(userEmail, pwd);
                        break;
                    default:
                        break;
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


        public void loginToLAportal(String userEmail, String pwd)
        {
            try
            {
                LoginPage loginPage = new LoginPage();
                By loggedInUser = SeleniumExtensions.getLocator($"//div[@id='tilesHolder']//div[@data-test-id=\"{userEmail}\"]");
                String screenAtUrlLaunch = getCurrentLandingScreen();


                switch (screenAtUrlLaunch)
                {
                    case "Login":
                        login(userEmail, pwd);
                        break;
                    case "PickUpAccount":
                        string currentScreen = "";
                        if (wh.isElementDisplayed(loggedInUser, 5))
                        {
                            currentScreen = "loggeinUser";
                            Console.WriteLine("User landed into " + currentScreen);
                            wh.clickOnWebElement(loggedInUser);
                        }
                        else
                        {
                            currentScreen = "UseAnotherAccount";
                            Console.WriteLine("User landed into " + currentScreen);
                        }
                        switch (currentScreen)
                        {
                            case "loggeinUser":
                                login_LAportal(pwd);
                                break;
                            case "UseAnotherAccount":
                                wh.clickOnElement(otherAccount);
                                login(userEmail, pwd);
                                break;
                        }
                        break;
                    case "DashBoard":
                        loginPage.LogoutFromApp();
                        wh.clickOnElement(otherAccount);
                        login(userEmail, pwd);
                        break;
                    default:
                        break;
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

        public void loginToCTdynamicsWithOutLoggedInUserValidation(String userEmail, String pwd)
        {
            LoginPage loginPage = new LoginPage();
            CommonPage cp = new CommonPage();
            By loggedInUser = SeleniumExtensions.getLocator($"//div[@id='tilesHolder']//div[@data-test-id=\"{userEmail}\"]");
            String screenAtUrlLaunch = getCurrentLandingScreen();


            switch (screenAtUrlLaunch)
            {
                case "Login":
                    login(userEmail, pwd);
                    break;
                case "PickUpAccount":
                    string currentScreen = "";
                    if (wh.isElementDisplayed(loggedInUser, 5))
                    {
                        currentScreen = "loggeinUser";
                        Console.WriteLine("User landed into " + currentScreen);
                        wh.clickOnWebElement(loggedInUser);

                    }
                    else
                    {
                        currentScreen = "UseAnotherAccount";
                        Console.WriteLine("User landed into " + currentScreen);
                    }
                    switch (currentScreen)
                    {
                        case "loggeinUser":
                            cp.waitTillPageLoading(60);
                            login(pwd);
                            break;
                        case "UseAnotherAccount":
                            wh.clickOnElement(otherAccount);
                            login(userEmail, pwd);
                            break;
                    }
                    break;
                case "DashBoard":
                    if (wh.isElementDisplayed(dashBoardContinue, 30))
                    {
                        wh.clickOnWebElementIfDisplayed(dashBoardContinue);
                    }
                    
                    break;
                default:
                    break;
            }
        }

        public bool login(string userEmail, string pwd)
        {
            wh.GetWebElementVisble(emailbY).SendKeys(userEmail);
            wh.clickOnElement(submitbY);
            wh.GetWebElementVisble(passwordbY).SendKeys(pwd);
            wh.clickOnElement(submitbY);
            Thread.Sleep(5000);
            if (wh.isElementDisplayed(staySignedInBy, 60))
                wh.GetWebElement(submitbY).Click();

            ClickCouncilTaxAppIcon();


            if (wh.elementClickableAndDisplayed(dashBoardContinue))
            {
                wh.GetWebElementVisble(By.CssSelector("[id*=\"WelcomePageDialogView\"]"));
                Thread.Sleep(500);
                wh.jsClickOnElement(dashBoardContinue);
            }

            return true;
        }

        public bool login(string pwd)
        {
            if (wh.isElementDisplayed(passwordbY,15)) { 
            wh.GetWebElementVisble(passwordbY).SendKeys(pwd);
            Thread.Sleep(500);
            wh.clickOnElement(submitbY);
            Thread.Sleep(2500);
            //wh.GetWebElement(staySignedInBy);
            if (wh.isElementDisplayed(staySignedInBy, 60))
            wh.GetWebElement(submitbY).Click();
            ClickCouncilTaxAppIcon();
            if (wh.elementClickableAndDisplayed(dashBoardContinue))
            {
                wh.GetWebElementVisble(By.CssSelector("[id*=\"WelcomePageDialogView\"]"));
                Thread.Sleep(500);
                wh.jsClickOnElement(dashBoardContinue);
            } 
            }
            else
            {
                if (wh.elementClickableAndDisplayed(dashBoardContinue))
                {
                    wh.GetWebElementVisble(By.CssSelector("[id*=\"WelcomePageDialogView\"]"));
                    Thread.Sleep(500);
                    wh.jsClickOnElement(dashBoardContinue);
                }

            }

            return true;
        }

        public bool login_LAportal(string pwd)
        {
            if (wh.isElementDisplayed(passwordbY, 15))
            {
                wh.GetWebElementVisble(passwordbY).SendKeys(pwd);
                Thread.Sleep(500);
                wh.clickOnElement(submitbY);
                Thread.Sleep(2500);
                By staySignedInBy = By.XPath("//div[@role='heading' and text()=\"Stay signed in?\"]");
                //wh.GetWebElement(staySignedInBy);
                if (wh.isElementDisplayed(staySignedInBy, 60))
                    wh.GetWebElement(submitbY).Click();
                
            }
            
            

            return true;
        }
    }
}


