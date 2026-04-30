using OpenQA.Selenium;
using POMSeleniumFrameworkPoc1.Helpers;
using Reqnroll;
using System;
using System.IO;


namespace POMSeleniumFrameworkPoc1.Steps.UI
{
    [Binding]
    public class SelectFieldSteps : BasePage
    {
        public DTO.InputOutputData inputoutputdata;
        public ScenarioContext _scenarioContext;
        public FeatureContext _featureContext;

        public SelectFieldSteps(ScenarioContext scenarioContext, FeatureContext featureContext,
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

        [Given(@"User clicks on '(.*)' dropdown and select '(.*)' value")]
        public void GivenUserClicksOnDropdownAndSelectedValue(string fieldName, string fieldValue)
        {
            try
            {
                waitHelpers wh = new waitHelpers();
                Scroll scr = new Scroll();
                By clickEle = SeleniumExtensions.getLocator($"button[aria-label*='{fieldName}']");
                if (wh.isElementDisplayed(clickEle, 10))
                {
                    SeleniumExtensions.clickElementAndSelectText(fieldName, fieldValue);
                }
                else
                {
                    try
                    {
                        SeleniumExtensions.scrollToBtnElement(fieldName);
                        SeleniumExtensions.clickElementAndSelectText(fieldName, fieldValue);
                    }
                    catch (NoSuchElementException e)
                    {
                        scr.scrollUntillAriaLabelEleVisible(fieldName, 150);
                        SeleniumExtensions.clickElementAndSelectText(fieldName, fieldValue);
                    }
                    catch (Exception e)
                    {
                        scr.scrollUntillAriaLabelEleVisible(fieldName, 150);
                        SeleniumExtensions.clickElementAndSelectText(fieldName, fieldValue);
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


        [Given(@"User selects '(.*)' value for '(.*)' dropdown")]
        public void GivenUserSelectsValueForDropdown(string fieldValue, string fieldName)
        {
            try
            {
                SeleniumExtensions.scrollToBtnElement(fieldName);
                SeleniumExtensions.clickElementAndSelectText(fieldName, fieldValue);
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

        [Given(@"User selects '(.*)' value for '(.*)' dropdown field")]
        public void GivenUserSelectsValueForDropdownField(string fieldValue, string fieldName)
        {
            try
            {
                SeleniumExtensions.scrollToBtnElement(fieldName);
                SeleniumExtensions.SelectElementByText(fieldName, fieldValue);
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

        [Given(@"User selects '(.*)' value '(.*)' dropdown field on No Action dialog")]
        public void GivenUserSelectsDropdownFieldOnNoActionDialog(string fieldValue, string fieldName)
        {
            try
            {
                SeleniumExtensions.NoActionSelectElementByText(fieldName, fieldValue);
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
