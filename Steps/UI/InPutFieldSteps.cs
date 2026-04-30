using POMSeleniumFrameworkPoc1.Helpers;
using Reqnroll;
using System;
using System.IO;

namespace POMSeleniumFrameworkPoc1.Steps.UI
{
    [Binding]
    public class InPutFieldSteps : BasePage
    {

        public DTO.InputOutputData inputoutputdata;
        public ScenarioContext _scenarioContext;
        public FeatureContext _featureContext;

        public InPutFieldSteps(ScenarioContext scenarioContext, FeatureContext featureContext,
            DTO.InputOutputData _inputoutputdata)
        {
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
            string TestDataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + Config.TestDataExcelFilePath);
            this.inputoutputdata = _inputoutputdata;
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
        }

        [Given(@"User enter data for '(.*)' field value")]
        public void GivenUserEnterDataForFieldValue(string fieldName)
        {
            try
            {
                var testData = inputoutputdata.testData;
                _featureContext[fieldName] = testData[fieldName];
                SeleniumExtensions.sendKeysFieldValue(fieldName, testData);
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

        [Given(@"User enter data for (.*) position '(.*)' field value")]
        public void GivenUserEnterDataForPositionFieldValue(int position, string fieldName)
        {
            try
            {
                var testData = inputoutputdata.testData;
                _featureContext[fieldName] = testData[fieldName];
                SeleniumExtensions.sendKeysFieldValue(position, fieldName, testData);
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

        [Given(@"User enter data for (.*) position '(.*)' field with custom value")]
        public void GivenUserEnterDataForPositionFieldWithCustomValue(int position, string fieldName)
        {
            Random rand = new Random();
            int RandomNum = rand.Next(100000, 999999);
            var testData = inputoutputdata.testData;
            var testData1 = testData[fieldName] + RandomNum;
            _featureContext[fieldName] = testData1;
            SeleniumExtensions.sendKeysFieldValue(fieldName, testData1, position);

        }



        [Given(@"User enter data for '(.*)' field value only when data not entered")]
        public void GivenUserEnterDataForFieldValueOnlyWhenDataNotEntered(string fieldName)
        {
            try
            {
                var testData = inputoutputdata.testData;
                enterDataWhenDataNotEntered(fieldName, testData);
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

        [Given(@"User enter data for '(.*)' for swap BA ref")]
        public void GivenUserEnterDataForForSwapBARef(string fieldName)
        {
            try
            {
                var testData = inputoutputdata.testData;
                _featureContext[fieldName] = SeleniumExtensions.sendKeysRandomFieldValue(fieldName);
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

        [Given(@"User enter swap heriditament BA ref number for '(.*)' field value")]
        public void GivenUserEnterSwapHeriditamentBARefNumberForFieldValue(string fieldName)
        {
            try
            {
                string value = (string)_featureContext["ba_reference_new"];
                _featureContext[fieldName] = SeleniumExtensions.sendKeysFieldValue(fieldName, value);
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

        [Given(@"User enter same hereditament BA ref number for '(.*)' field value")]
        public void GivenUserEnterSameHereditamentBARefNumberForFieldValue(string fieldName)
        {
            try
            {
                string value = (string)_featureContext["ba_reference"];
                _featureContext[fieldName] = SeleniumExtensions.sendKeysFieldValue_sameref(fieldName, value);
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

        [Given(@"User enters second heraditament  BA ref number for '(.*)' field value")]
        public void GivenUserEntersSecondHeraditamentBARefNumberForFieldValue(string fieldName)
        {
            try
            {
                string value = (string)_featureContext["ba_reference"];
                _featureContext[fieldName] = SeleniumExtensions.sendKeysFieldValue_secondref(fieldName, value);
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



        [Given(@"User enter random number in '(.*)' position '(.*)' field value")]
        public void GivenUserEnterRandomNumberInPositionFieldValue(int position, string fieldName)
        {
            try
            {
                var testData = inputoutputdata.testData;
                _featureContext[fieldName] = SeleniumExtensions.sendKeysRandomFieldValue(fieldName, position);
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

        [Given("User enter {string} and {string} community code number for {string} field value")]
        public void GivenUserEnterCommunityCodeNumberForFieldValue(string town, string BA, string fieldName)
        {
            try
            {
                var testData = inputoutputdata.testData;
                var value = SeleniumExtensions.sendKeysCommunityFieldValue(BA,town,fieldName);
                _featureContext[fieldName] = value;
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


        [Given(@"User enter community code number for ""(.*)"" field value")]
        public void GivenUserEnterCommunityCodeNumberForFieldValue(string fieldName)
        {
            try
            {
                var testData = inputoutputdata.testData;
                var value = SeleniumExtensions.sendKeysCommunityFieldValue(fieldName);
                _featureContext[fieldName] = value;

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


        [Given(@"User enter random number for '(.*)' field value")]
        public void GivenUserEnterRandomNumberForFieldValue(string fieldName)
        {
            try
            {
                var testData = inputoutputdata.testData;
                _featureContext[fieldName] = SeleniumExtensions.sendKeysRandomFieldValue(fieldName);
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

        [Given(@"User entered random number for '(.*)' field value")]
        public void GivenUserEnteredRandomNumberForFieldValue(string fieldName)
        {
            try
            {
                var testData = inputoutputdata.testData;
                _featureContext[fieldName] = SeleniumExtensions.sendKeysRandomFieldValue_BAref(fieldName);
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



        [Given(@"User enter data for '(.*)' text area field value")]
        public void GivenUserEnterDataForTextAreaFieldValue(string fieldName)
        {
            try
            {
                var testData = inputoutputdata.testData;
                SeleniumExtensions.sendKeysTextAreaFieldValue(fieldName, testData);
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

        [Given(@"User enter data for Caseworker Conclusions \/ Remarks \/ Thought Process field")]
        public void GivenUserEnterDataForCaseworkerConclusionsRemarksThoughtProcessField()
        {
            try
            {
                SeleniumExtensions.enterCaseworkerConclusionsRemarksThoughtProcessData();
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





        [Given(@"User enter data for '(.*)' field value from '(.*)' for '(.*)'")]
        public void GivenUserEnterDataForFieldValueFromFor(string fieldName, string sheetName, string RowID)
        {
            try
            {
                SeleniumExtensions.sendKeysFieldValue(fieldName, sheetName, RowID);
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

        [Given(@"User entered date for '(.*)' field value")]
        public void GivenUserEnteredDateForFieldValue(string fieldName)
        {
            try
            {
                var testData = inputoutputdata.testData;
                SeleniumExtensions.enterDateSequentially(fieldName, testData[fieldName]);
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
