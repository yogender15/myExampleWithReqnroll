using OpenQA.Selenium;
using POMSeleniumFrameworkPoc1.Helpers;
using POMSeleniumFrameworkPoc1.Pages.UI.CommonComponents;
using POMSeleniumFrameworkPoc1.Pages.UI.RBSP_BP;
using Reqnroll;
using System;
using System.IO;
using System.Threading;


namespace POMSeleniumFrameworkPoc1.Steps.UI
{
    [Binding]
    public class MenuBarAndTabSteps : BasePage
    {
        public DTO.InputOutputData inputoutputdata;
        public ScenarioContext _scenarioContext;
        public FeatureContext _featureContext;



        public MenuBarAndTabSteps(ScenarioContext scenarioContext, FeatureContext featureContext,
            DTO.InputOutputData _inputoutputdata)
        {
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
            string TestDataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + Config.TestDataExcelFilePath);
            this.inputoutputdata = _inputoutputdata;
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
        }

        [Given(@"User click on '(.*)' button from '(.*)'")]
        public void GivenUserClickOnButtonFrom(string buttonName, string section)
        {
            try
            {
                if (section.ToLower().Equals("dialog"))
                {
                    try
                    {
                        if (!buttonName.Equals("Save & Close"))
                        {
                            clickOnDialogMenuElement(buttonName);
                        }
                        else
                        {
                            clickOnSaveAndCloseOnDialog(buttonName);
                        }
                    }
                    catch (Exception e)
                    {
                        clickOnSaveAndCloseOnDialog(buttonName);
                    }
                }
                else
                {
                    CommonPage cp = new CommonPage();
                    waitHelpers wh = new waitHelpers();
                    cp.waitforTopMenuBarLoading();
                    try
                    {
                        clickOnMainMenuMoreElement_New(buttonName);
                    }
                    catch (Exception e)
                    {
                        Thread.Sleep(2000);
                        clickOnMainMenuMoreElement_New(buttonName);
                    }
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

        [Given(@"User clicks on '(.*)' under '(.*)'")]
        public void GivenUserClicksOnUnder(string MenuOption, string requestAction)
        {
            try
            {
                var requestPage = new RequestPage();
                requestPage.ClickMenuListOptionFromCommandBarWithOuJS(MenuOption, requestAction);
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
            }
            catch (ElementClickInterceptedException e)
            {
                CommonPage commonpage = new CommonPage();
                commonpage.waitTillProgressBarDisaddpears();
                commonpage.waitTillSavingDisaddpears("Saving...", "progressbar");
                var requestPage = new RequestPage();
                requestPage.ClickMenuListOptionFromCommandBarWithOuJS(MenuOption, requestAction);
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

        [Given(@"User clicked on '(.*)' tab under '(.*)'")]
        public void GivenUserClickedOnTabUnder(string tabName, string researchForm)
        {
            try
            {
                SeleniumExtensions.WaitForPageLoad();
                CommonPage commonpage = new CommonPage();
                commonpage.clickOnTab_Latest(tabName, researchForm);
                SeleniumExtensions.WaitForPageLoad();
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

        [Given(@"User click on '(.*)' under '(.*)' section")]
        public void GivenUserClickOnUnderSection(string menuItem, string sectionName)
        {
            try
            {
                var commonPage = new CommonPage();
                commonPage.NavigateToMenuItem_new(sectionName, menuItem);
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

    }
}
