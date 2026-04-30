using NUnit.Framework;
using OpenQA.Selenium;
using POMSeleniumFrameworkPoc1.DTO;
using POMSeleniumFrameworkPoc1.DTO.UI;
using POMSeleniumFrameworkPoc1.Helpers;
using POMSeleniumFrameworkPoc1.Pages;
using POMSeleniumFrameworkPoc1.Pages.UI.CommonComponents;
using POMSeleniumFrameworkPoc1.Pages.UI.LAPortal;
using Reqnroll;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace POMSeleniumFrameworkPoc1.Steps.UI
{
    [Binding]
    public class LAPortal
    {
        public DTO.InputOutputData inputoutputdata;
        public ScenarioContext _scenarioContext;
        public FeatureContext _featureContext;

        public LAPortal(ScenarioContext scenarioContext, FeatureContext featureContext, DTO.InputOutputData _inputoutputdata)
        {
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
            this.inputoutputdata = _inputoutputdata;

        }

        [Given("User is logged in to LA Portal to work for {string} case")]
        public void GivenUserIsLoggedInToLAPortalToWorkForCase(string pdfFileName)
        {
            var loginPage = new LoginPage();
            waitHelpers wh = new waitHelpers();
            string propertiesFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + Config.EnvironmentValFilePath);
            PropertiesReader _propertiesReader = new PropertiesReader(propertiesFilePath);
            Config.LAPortalBaseUrl = _propertiesReader.Get("LAPortal_" + Config.EnvironmentVal);
            loginPage.GoToLoginPage(Config.LAPortalBaseUrl);
            var lAPortalLoginPage = new LAPortalLoginPage();
            if (wh.isElementDisplayed(By.CssSelector("a[class='govuk-button govuk-button--start']"), 30))
            {
                wh.clickOnElement(By.CssSelector("a[class='govuk-button govuk-button--start']"));
                loginPage.loginToApp_LAPortal("VOA Team Manager1");
            }
            PDF_Utility pdf_util = new PDF_Utility();
            pdf_util.initializeScreenshotsFile(pdfFileName);
        }


        [Given(@"User is logged in to LA Portal")]
        public void GivenUserIsLoggedInToLAPortal()
        {
            var loginPage = new LoginPage();
            waitHelpers wh = new waitHelpers();
            string propertiesFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + Config.EnvironmentValFilePath);
            PropertiesReader _propertiesReader = new PropertiesReader(propertiesFilePath);
            Config.LAPortalBaseUrl = _propertiesReader.Get("LAPortal_" + Config.EnvironmentVal);
            loginPage.GoToLoginPage(Config.LAPortalBaseUrl);
            var lAPortalLoginPage = new LAPortalLoginPage();
            if (wh.isElementDisplayed(By.CssSelector("a[class='govuk-button govuk-button--start']"), 30))
            {
                wh.clickOnElement(By.CssSelector("a[class='govuk-button govuk-button--start']"));

            }
            //lAPortalLoginPage.ClickStartNowBtnElement();
            //lAPortalLoginPage.EnterLoginCredentialsAndClickSignInToLAPortal(Config.LAPortalUsername, Config.LAPortalPassword);
        }

        [When(@"I click Single report tab")]
        public void WhenIClickSingleReportTab()
        {
            try
            {
                var lAPortalDashboardPage = new LAPortalDashboardPage();
                lAPortalDashboardPage.ClickCreateSingleReportLink();
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

        [Then(@"I choose '(.*)' as the reason code for the submission")]
        public void ThenIChooseAsTheReasonCodeForTheSubmission(string reasonCode)
        {
            try
            {
                var lAPortalDashboardPage = new LAPortalDashboardPage();
                lAPortalDashboardPage.SelectReasonCodeOnLAPortal(reasonCode);
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

        [Then(@"I create '(.*)' request from LA Portal")]
        public void ThenICreateRequestFromLAPortal(string requestCode)
        {
            var testData = inputoutputdata.testData;
            var lAPortalDashboardPage = new LAPortalDashboardPage();
            lAPortalDashboardPage.CreateRequestOnLAPortal(requestCode, testData);
        }


        [Then(@"User enter details on References page")]
        public void ThenUserEnterDetailsPage()
        {
            var testData = inputoutputdata.testData;
            var lAPortalDashboardPage = new LAPortalDashboardPage();
            lAPortalDashboardPage.EnterReferencesDataForLAPortalReq(testData);
        }

        [Then(@"User selects '(.*)' for the property on the Select Council Tax Band page")]
        public void ThenUserSelectsForThePropertyOnTheSelectCouncilTaxBandPage(string bandCode)
        {
            var lAPortalDashboardPage = new LAPortalDashboardPage();
            lAPortalDashboardPage.SelectBandForLAPortalRequest(bandCode);
        }

        [Then(@"User enter details on Contact Details page")]
        public void ThenUserEnterDetailsOnContactDetailsPage()
        {
            try
            {
                var testData = inputoutputdata.testData;
                var lAPortalDashboardPage = new LAPortalDashboardPage();
                lAPortalDashboardPage.EnterContactDataForLAPortalReq(testData);
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

        [Then(@"User enter EFfective Date and Remarks on Change request details page")]
        public void ThenUserEnterEFfectiveDateAndRemarksOnChangeRequestDetailsPage()
        {
            try
            {
                var testData = inputoutputdata.testData;
                var lAPortalDashboardPage = new LAPortalDashboardPage();
                lAPortalDashboardPage.EnterEffectiveDateAndRemarksLAPortal(testData);
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

        [Then("User selects the postcode and Address from db for the CR03 Portal request")]
        public void ThenUserSelectsThePostcodeAndAddressFromDbForTheCRPortalRequest()
        {
            try
            {
                var testData = inputoutputdata.testData;
                var lAPortalDashboardPage = new LAPortalDashboardPage();
                lAPortalDashboardPage.EnterPostCodeAndSelectAddress(_featureContext);
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


        [Then(@"User selects the postcode and Address for the CR03 Portal request")]
        public void ThenUserSelectsThePostcodeAndAddressForTheLAPortalRequest()
        {
            try
            {
                var testData = inputoutputdata.testData;
                var lAPortalDashboardPage = new LAPortalDashboardPage();
                lAPortalDashboardPage.EnterPostCodeAndSelectAddress(testData);
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

        [Then(@"User answers the questionnaire about Planning Portal Reference")]
        public void ThenUserAnswersTheQuestionnaireAboutPlanningPortalReference()
        {
            try
            {
                var testData = inputoutputdata.testData;
                var lAPortalDashboardPage = new LAPortalDashboardPage();
                lAPortalDashboardPage.SelectValuesForPlanningPortalRef(testData);
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

        [Then(@"User enter details on References page for CR(.*)")]
        public void ThenUserEnterDetailsOnReferencesPageForCR(int p0)
        {
            try
            {
                var testData = inputoutputdata.testData;
                var lAPortalDashboardPage = new LAPortalDashboardPage();
                lAPortalDashboardPage.EnterReferencesDataForCR03LAPortalReq(testData);
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

        [Then(@"User enter details on References page from db")]
        public void ThenUserEnterDetailsOnReferencesPageFromDb()
        {
            var lAPortalDashboardPage = new LAPortalDashboardPage();
            String baReportNum = DateTimeOffset.Now.ToString("yyyyMMddHHmmssffff");
            lAPortalDashboardPage.EnterReferencesDataForCR03LAPortalReq((String)_featureContext["ba_reference"], baReportNum);
        }

        [Then(@"User click on '(.*)' button on '(.*)' page")]
        public void ThenUserClickOnButtonOnPage(string p0, string pageName)
        {
            var lAPortalDashboardPage = new LAPortalDashboardPage();
            lAPortalDashboardPage.clickOnContinue(pageName);

        }

        [Then(@"User select '(.*)' Band")]
        public void ThenUserSelectBand(string band)
        {
            var lAPortalDashboardPage = new LAPortalDashboardPage();
            lAPortalDashboardPage.SelectReasonCodeOnLAPortal(band);
        }


        [Then(@"User clicks on continue and selectes the council tax band for Minor Change of Address")]
        public void ThenUserSelectsCouncilTaxband()
        {
            var testData = inputoutputdata.testData;
            var lAPortalDashboardPage = new LAPortalDashboardPage();
            lAPortalDashboardPage.SelectCouncilTaxBand();
        }


        [Then(@"User selects the planning portal reference")]
        public void ThenUserSelectsThePlanningPortalReference()
        {
            var testData = inputoutputdata.testData;
            var lAPortalDashboardPage = new LAPortalDashboardPage();
            lAPortalDashboardPage.UserSelectsThePlanningPortalReference(testData);
        }

    }
}
