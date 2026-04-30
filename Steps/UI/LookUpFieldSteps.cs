using POMSeleniumFrameworkPoc1.Helpers;
using POMSeleniumFrameworkPoc1.Pages.UI.RBSP_BP;
using Reqnroll;
using System;
using System.IO;


namespace POMSeleniumFrameworkPoc1.Steps.UI
{
    [Binding]
    public class LookUpFieldSteps : BasePage
    {
        public DTO.InputOutputData inputoutputdata;
        public ScenarioContext _scenarioContext;
        public FeatureContext _featureContext;



        public LookUpFieldSteps(ScenarioContext scenarioContext, FeatureContext featureContext,
            DTO.InputOutputData _inputoutputdata)
        {
            //_submissionTestData = submissionTestData;
            //_excelUtility = excelUtility;
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
            //_testDataMetaData = testDataMetaData;
            //BasePage.SetTestDataInfo(_scenarioContext, _featureContext, _testDataMetaData);
            //_submissionTestData = _excelUtility.GetTestDataAndMap<SubmissionTestData>(_testDataMetaData.TestDataSheetName, _testDataMetaData.TestDataID);
            string TestDataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + Config.TestDataExcelFilePath);
            this.inputoutputdata = _inputoutputdata;
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
        }

        [Given(@"User looked for value '(.*)' in '(.*)' field")]
        public void GivenUserLookedForInFieldValue(string value, string fieldName)
        {
            try
            {
                var testData = value;
                //FillAndSelectLookUpResult(fieldName, testData);
                _featureContext[fieldName] = testData;
                SeleniumExtensions.lookUpFieldBasedOnText(fieldName, value, 120);
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

        [Given(@"User looked for value '(.*)' in '(.*)' field by search icon")]
        public void GivenUserLookedForValueInFieldByEarchIcon(string value, string fieldName)
        {
            try
            {
                var testData = value;
                SeleniumExtensions.ScrollUntilSelectorDisplayedAndSendKeysBySearchIcon(value, fieldName, 120);
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

        [Given(@"User looked for value '(.*)' in '(.*)' field with keyboard")]
        public void GivenUserLookedForValueInFieldWithKeyboard(string value, string fieldName)
        {
            try
            {
                var testData = value;
                SeleniumExtensions.lookUpFieldBasedOnTextWithKeyPad(fieldName, value, 120);
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





        [Given(@"User looked for testdata '(.*)' value for '(.*)' field")]
        public void GivenUserLookedForTestdataValueForField(string tdValue, string fieldName)
        {
            try
            {
                var testData = inputoutputdata.testData;
                _featureContext[fieldName] = testData[tdValue];
                FillAndSelectLookUpResultWhenDataNotEntered(fieldName, _featureContext, 60);
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
            }
            catch (Exception e)
            {
                var testData = inputoutputdata.testData;
                FillAndSelectLookUpResultWhenDataNotEntered(fieldName, testData);
                _featureContext[fieldName] = testData[fieldName];
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
                pdf_Util.exceptionPdFLogger(e);
            }
        }



        [Given(@"User looked for value '(.*)' for '(.*)' field")]
        public void GivenUserLookedForValueForField(string value, string fieldName)
        {
            try
            {
                var testData = value;
                SeleniumExtensions.lookUpFieldBasedOnExactText(fieldName, value, 120);
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


        [Given(@"User looked for value '(.*)' sequentially in '(.*)' field")]
        public void GivenUserLookedForValueSequentiallyInField(string value, string fieldName)
        {
            try
            {
                var testData = value;
                FillAndSelectLookUpResult(fieldName, value);
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

        [Given(@"User looked for value '(.*)' in '(.*)' field by clicking on search icon")]
        public void GivenUserLookedForValueInFieldByClickingOnSearchIcon(string fieldValue, string fieldName)
        {
            try
            {
                _featureContext[fieldName] = fieldValue;
                SeleniumExtensions.ScrollUntilSelectorDisplayedAndSendKeysBySearchIcon(fieldValue, fieldName, 120);
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


        [Given(@"User looked for '(.*)' field value by clicking on search icon")]
        public void GivenUserLookedForFieldValueByClickingOnSearchIcon(string fieldName)
        {
            try
            {
                var testData = inputoutputdata.testData;
                _featureContext[fieldName] = testData[fieldName];
                SeleniumExtensions.ScrollUntilSelectorDisplayedAndSendKeysBySearchIcon(fieldName, testData, 120);
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

        [Given(@"User looked for '(.*)' field with '(.*)' from feature context")]
        public void GivenUserLookedForFieldWithFromFeatureContext(string fieldName, string featureFieldName)
        {
            try
            {
                var testData = inputoutputdata.testData;
                _featureContext[fieldName] = (String)_featureContext[featureFieldName];
                FillAndSelectLookUpResultWhenDataNotEntered(fieldName, (String)_featureContext[featureFieldName], 60);
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
            }
            catch (Exception e)
            {
                var testData = inputoutputdata.testData;
                FillAndSelectLookUpResultWhenDataNotEntered(fieldName, testData);
                _featureContext[fieldName] = testData[fieldName];
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
                pdf_Util.exceptionPdFLogger(e);
            }
        }



        [Given(@"User looked for '(.*)' field value")]
        public void GivenUserLookedForFieldValue(string fieldName)
        {
            try
            {
                var testData = inputoutputdata.testData;
                _featureContext[fieldName] = testData[fieldName];
                //FillAndSelectLookUpResult(fieldName, testData);
                FillAndSelectLookUpResultWhenDataNotEntered(fieldName, testData, 20);
                //SeleniumExtensions.ScrollUntilSelectorDisplayedAndSendKeys_New(fieldName, testData, 120);
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
            }
            catch (Exception e)
            {
                var testData = inputoutputdata.testData;
                FillAndSelectLookUpResultWhenDataNotEntered(fieldName, testData);
                _featureContext[fieldName] = testData[fieldName];
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
                pdf_Util.exceptionPdFLogger(e);
            }

        }

        [Given(@"User looked for '(.*)' field value from context '(.*)'")]
        public void GivenUserLookedForFieldValueFrom(string fieldName, string storageContext)
        {
            try
            {
                String testData = null;
                if (storageContext.Equals("scenarioContext", StringComparison.InvariantCultureIgnoreCase))
                {
                    testData = (string)_scenarioContext[fieldName];//"Newcastle-upon-Tyne - 07/12/2024 19:06";//
                }
                SeleniumExtensions.ScrollUntilSelectorDisplayedAndSendKeys_Storage(fieldName, testData, 120);
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


        [Given(@"User looked for (.*) position '(.*)' field value")]
        public void GivenUserLookedForPositionFieldValue(int position, string fieldName)
        {
            try
            {
                var testData = inputoutputdata.testData;
                //FillAndSelectLookUpResult(fieldName, testData);
                SeleniumExtensions.ScrollUntilSelectorDisplayedAndSendKeys_New(position, fieldName, testData, 120);
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


        [Given(@"User looked for '(.*)' field value by scroll")]
        public void GivenUserLookedForFieldValueByScroll(string fieldName)
        {
            try
            {
                var testData = inputoutputdata.testData;
                FillAndSelectLookUpResult(fieldName, testData);
                //SeleniumExtensions.ScrollUntilSelectorDisplayedAndSendKeys_New(fieldName, testData, 120);
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

        [Given(@"User looked for '(.*)' field value only when data not entered")]
        public void GivenUserLookedForFieldValueOnlyWhenDataNotEntered(string fieldName)
        {
            try
            {
                var testData = inputoutputdata.testData;
                FillAndSelectLookUpResultWhenDataNotEntered(fieldName, testData);
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

        [Given(@"User looked for first element '(.*)' field value only when data not entered")]
        public void GivenUserLookedForFirstElementFieldValueOnlyWhenDataNotEntered(string fieldName)
        {
            try
            {
                var testData = inputoutputdata.testData;
                FillAndSelectLookUpFirstResultWhenDataNotEntered(fieldName, testData);
            }
            catch (Exception e)
            {
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
                pdf_Util.exceptionPdFLogger(e);
            }

        }

        [Given(@"User looked for '(.*)' field value from '(.*)' for '(.*)'")]
        public void GivenUserLookedForFieldValueFromFor(string fieldName, string sheetName, string rowID)
        {
            try
            {
                FillAndSelectLookUpResult(fieldName, sheetName, rowID);
            }
            catch (Exception e)
            {
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
                pdf_Util.exceptionPdFLogger(e);
            }
        }
        [Given("User validate {string} is not mandatory")]
        public void GivenUserValidateIsNotMandatory(string fieldname)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                CommonPage.lockedfield(fieldname);

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

        [Given(@"User looked for '(.*)' single character field value")]
        public void GivenUserLookedForSingleCharacterFieldValue(string fieldName)
        {
            try
            {
                var testData = inputoutputdata.testData;
                CommonPage commonpage = new CommonPage();
                SeleniumExtensions.scrollToBtnElement(fieldName);
                _featureContext["Proposed Tax Band"] = testData[fieldName];
                commonpage.EnterDataInSingleCharLookUpField(fieldName, testData);
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
