using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using POMSeleniumFrameworkPoc1.DTO.UI;
using POMSeleniumFrameworkPoc1.Helpers;
using POMSeleniumFrameworkPoc1.Pages;
using POMSeleniumFrameworkPoc1.Pages.UI.CommonComponents;
using POMSeleniumFrameworkPoc1.Pages.UI.RBSP_BP;
using Reqnroll;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace POMSeleniumFrameworkPoc1.Steps.UI
{
    [Binding]
    public class HereditamentsSteps
    {
        public ScenarioContext _scenarioContext;
        public FeatureContext _featureContext;
        public NewHereditament newHereditamentInformation;
        public DTO.InputOutputData inputoutputdata;
        private readonly ExcelTestDataUtility exceltestdatautility;
        public HereditamentsSteps(ScenarioContext context, FeatureContext featureContext, NewHereditament _newHereditament, DTO.InputOutputData _inputoutputdata)
        {
            this.newHereditamentInformation = _newHereditament;
            string TestDataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + Config.TestDataExcelFilePath);
            this.inputoutputdata = _inputoutputdata;
            _scenarioContext = context;
            _featureContext = featureContext;
            this.exceltestdatautility = new ExcelTestDataUtility(TestDataFilePath);
        }

        [When(@"the user clicks Hereditaments menu")]
        public void WhenTheUserClicksHereditamentsMenu()
        {
            var hereditaments = new HereditamentsPage();
            hereditaments.ClickHereditament();
        }

        [Then(@"all the hereditaments in the system should display in the list")]
        public void ThenAllTheHereditamentsInTheSystemShouldDisplayInTheList()
        {
            var hereditamentsPage = new HereditamentsPage();
            Assert.IsTrue(hereditamentsPage.IsListOfHereditamentsDisplayed(), "The list of jobs are not displayed");
        }

        [When(@"the user clicks \\+ button")]
        public void WhenTheUserClicksButton()
        {
            var hereditamentsPage = new HereditamentsPage();
            hereditamentsPage.ClickPlusNewHereditamentBtn();
        }

        [When(@"User selects Billing Authority as ""(.*)""")]
        public void WhenUserSelectsBillingAuthorityAs(string billingAuthority)
        {
            var hereditamentsPage = new HereditamentsPage();
            hereditamentsPage.FillBillingAuthorityDetails(billingAuthority);
        }

        [When(@"User pick the effective date as (.*) days")]
        public void WhenUserPickTheEffectiveDateAsDays(int days)
        {
            var hereditamentsPage = new HereditamentsPage();
            hereditamentsPage.PickEffectiveDate(days);
        }

        [When(@"User save the Hereditament")]
        public void WhenUserSaveTheHereditament()
        {
            var hereditamentsPage = new HereditamentsPage(); ;
            newHereditamentInformation.HereditamentId = hereditamentsPage.SaveHereditament();
        }

        [When(@"I click Documents tab")]
        public void WhenIClickDocumentsTab()
        {
            var hereditamentsPage = new HereditamentsPage();
            hereditamentsPage.ClickDocumentTab();
        }

        [Then(@"I should see Upload, External Links, Refresh buttons enabled")]
        public void ThenIShouldSeeUploadExternalLinksRefreshButtonsEnabled()
        {
            var hereditamentsPage = new HereditamentsPage();
            Assert.IsTrue(hereditamentsPage.VerifyUploadRefreshLinksControlsAreEnabled(),
                "Upload or Refresh or External links are NOT enabled");
        }

        [Then(@"I should see Download, Copy, Move buttons disabled")]
        public void ThenIShouldSeeDownloadCopyMoveButtonsDisabled()
        {
            var hereditamentsPage = new HereditamentsPage();
            Assert.IsTrue(hereditamentsPage.VerifyUploadRefreshLinksControlsAreEnabled(),
                "Copy or Move or Download controls are enabled");
        }

        [Given(@"User enters data in ""(.*)"" field from '(.*)' for '(.*)'")]
        public void GivenUserEntersDataInFieldFromFor(string fieldName, string sheetName, string rowID)
        {
            var commonPage = new CommonPage();
            commonPage.enterDataInFieldsBasedOnLabel(fieldName, sheetName, rowID);
        }

        [Given(@"User enters data in ""(.*)"" field for reconstition")]
        public void GivenUserEntersDataInFieldForReconstition(string fn)
        {
            try
            {
                var testData = inputoutputdata.testData;
                var commonPage = new CommonPage();
                string[] fnArr = fn.Split('_');
                string fieldName = fnArr[0];
                string fieldValue = (string)_featureContext[fn.ToLower()];
                commonPage.enterDataInFieldsBasedOnLabel(fieldName, fieldValue);
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

        [Given(@"User enters data in ""(.*)"" field for swap heriditament")]
        public void GivenUserEntersDataInFieldForSwapHeriditament(string fc_FieldName)
        {
            try
            {
                string[] fieldNamearr = fc_FieldName.Split('_');
                string fieldName = fieldNamearr[0];
                var testData = inputoutputdata.testData;
                var commonPage = new CommonPage();
                commonPage.enterDataInFieldsBasedOnLabel(fieldName, fc_FieldName, testData, _featureContext);
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

        [Given(@"User enters data in ""(.*)"" field for new property find address")]
        public void GivenUserEntersDataInFieldForNewPropertyFindAddress(string fieldName)
        {
            try
            {
                var testData = inputoutputdata.testData;
                var commonPage = new CommonPage();
                commonPage.enterDataInPostCodeField_NewProperty(fieldName, testData, _featureContext);
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



        [Given(@"User enters data in ""(.*)"" field")]
        public void GivenUserEntersDataInField(string fieldName)
        {
            try
            {
                var testData = inputoutputdata.testData;
                var commonPage = new CommonPage();
                commonPage.enterDataInFieldsBasedOnLabel(fieldName, testData, _featureContext);
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


        [Given(@"User selects '(.*)' row from search Hereditament results")]
        public void GivenUserSelectsRowFromSearchHereditamentResults(int rowNum)
        {
            try
            {
                var hereditamentsPage = new HereditamentsPage();
                hereditamentsPage.selectFirstHeridetamentResult(rowNum);
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

        [Given(@"User selects '(.*)' row from search plot results")]
        public void GivenUserSelectsRowFromSearchPlotResults(int p0)
        {
            try
            {
                var hereditamentsPage = new HereditamentsPage();
                hereditamentsPage.selectFirstPlotResult();
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
        [Given("User slects spcific {string} row from search hereditament results for find service address")]
        public void GivenUserSlectsSpcificRowFromSearchHereditamentResultsForFindServiceAddress(string uprn)
        {
            try
            {
                var hereditamentsPage = new HereditamentsPage();
                String uprnValue = (string)_featureContext[uprn];
                hereditamentsPage.selectHeridetamentByuprn_NewProperty(uprnValue);
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


        [Given(@"User slects spcific '(.*)' row from search hereditament results for new property")]
        public void GivenUserSlectsSpcificRowFromSearchHereditamentResultsForNewProperty(string uprn)
        {
            try
            {
                var hereditamentsPage = new HereditamentsPage();
                String uprnValue = (string)_featureContext[uprn];
                hereditamentsPage.selectHeridetamentByuprn_NewProperty(uprnValue);
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

        [Given(@"User slects non spcific '(.*)' row from search hereditament results for new property")]
        public void GivenUserSlectsNonSpcificRowFromSearchHereditamentResultsForNewProperty(string uprn)
        {
            try
            {
                var hereditamentsPage = new HereditamentsPage();
                String uprnValue = (string)_featureContext[uprn];
                hereditamentsPage.selectHeridetamentNotByuprn_NewProperty(uprnValue);
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

        [Given(@"User slects spcific new property '(.*)' row from search hereditament results")]
        public void GivenUserSlectsSpcificNewPropertyRowFromSearchHereditamentResults(string uprn)
        {
            try
            {
                var hereditamentsPage = new HereditamentsPage();
                String uprnValue = (string)_featureContext[uprn];
                hereditamentsPage.selectHeridetamentByuprn_NewProperty(uprnValue);
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

        [Given("User select '(.*)' value from '(.*)' dropdown")]
        public void GivenUserSelectValueFromDropdown(string btnName, string fieldLabel)
        {
            try
            {
                var hereditamentsPage = new HereditamentsPage();
                hereditamentsPage.selectSearchByDropDown(btnName);
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
        [Given("User select {string} value from {string} dropdown for milo")]
        public void GivenUserSelectValueFromDropdownForMilo(string btnName, string fieldLabel)
        {
            try
            {
                var hereditamentsPage = new HereditamentsPage();
                hereditamentsPage.selectSearchByDropDown_Milo(btnName);
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

        [Given("User select specific {string} row from search hereditament results and save in {string}")]
        public void GivenUserSelectSpecificRowFromSearchHereditamentResultsAndSaveIn(string uprn, string storageContext)
        {
            var hereditamentsPage = new HereditamentsPage();
            String uprnValue = (string)_featureContext[uprn];
            String addressValue = hereditamentsPage.selectHeridetamentByuprn_new(uprnValue, _featureContext);
            Console.WriteLine("Unique addressValue : " + addressValue);
            if (storageContext.Equals("scenarioContext", StringComparison.InvariantCultureIgnoreCase))
            {
                _scenarioContext["Address"] = addressValue;
            }
            if (storageContext.Equals("featureContext", StringComparison.InvariantCultureIgnoreCase))
            {
                _featureContext["Address"] = addressValue;
            }
        }

        [Given("User slects {string} {string} row from search hereditament results")]
        public void GivenUserSlectsRowFromSearchHereditamentResults(string status, string uprn)
        {
            try
            {
                var hereditamentsPage = new HereditamentsPage();
                String uprnValue = (string)_featureContext[uprn];
                hereditamentsPage.selectHeridetamentByuprn(uprnValue, status);
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

        [Given("User slects spcific {string} row {string} by clicking on {string} from search hereditament results")]
        public void GivenUserSlectsSpcificRowByClickingOnFromSearchHereditamentResults(string uprn, string band, string bandBtn)
        {
            try
            {
                var hereditamentsPage = new HereditamentsPage();
                String uprnValue = (string)_featureContext[uprn];
                hereditamentsPage.selectHeridetamentByDeletedUPRN(uprnValue, band);
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



        [Given(@"User slects spcific '(.*)' row from search hereditament results")]
        public void GivenUserSlectsSpcificRowFromSearchHereditamentResults(string uprn)
        {
            try
            {
                var hereditamentsPage = new HereditamentsPage();
                String uprnValue = (string)_featureContext[uprn];
                hereditamentsPage.selectHeridetamentByuprn(uprnValue);
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

        [Given("User slects spcific {string} row from search hereditament results from Comparator Tool")]
        public void GivenUserSlectsSpcificRowFromSearchHereditamentResultsFromComparatorTool(string uprn)
        {
            try
            {
                var hereditamentsPage = new HereditamentsPage();
                String uprnValue = (string)_featureContext[uprn];
                hereditamentsPage.selectHeridetamentByuprnWithOutSelect(uprnValue);
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



        [Given(@"User selects unique address from search hereditament results")]
        public void GivenUserSelectsUniqueAddressFromSearchHereditamentResults()
        {
            try
            {
                var hereditamentsPage = new HereditamentsPage();
                hereditamentsPage.selectHeridetamentNoJobsandSingleliveEntry();
            }
            catch (Exception e)
            {
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
                pdf_Util.exceptionPdFLogger(e);
            }
        }


       


        [Given(@"User selects newly created ""(.*)"" from '(.*)'")]
        public void GivenUserSelectsNewlyCreatedFrom(string fieldName, string storageContext)
        {
            var hereditamentsPage = new HereditamentsPage();
            String addressValue = null;
            if (storageContext.Equals("scenarioContext", StringComparison.InvariantCultureIgnoreCase))
            {
                addressValue = (String)_scenarioContext[fieldName];

            }
            if (storageContext.Equals("featureContext", StringComparison.InvariantCultureIgnoreCase))
            {
                addressValue = (String)_featureContext[fieldName];
            }

            hereditamentsPage.selectHeridetamentBasedOnLabel(addressValue);
        }


    }
}
