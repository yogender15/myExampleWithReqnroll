using NUnit.Framework;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using OpenQA.Selenium;
using POMSeleniumFrameworkPoc1.DTO.UI;
using POMSeleniumFrameworkPoc1.Helpers;
using POMSeleniumFrameworkPoc1.Pages;
using POMSeleniumFrameworkPoc1.Pages.UI;
using POMSeleniumFrameworkPoc1.Pages.UI.RBSP_BP;
using Reqnroll;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace POMSeleniumFrameworkPoc1.Steps.UI
{
    [Binding]
    public class SubmissionSteps : BasePage
    {
        public DTO.InputOutputData inputoutputdata;
        public ScenarioContext _scenarioContext;
        public FeatureContext _featureContext;



        public SubmissionSteps(ScenarioContext scenarioContext, FeatureContext featureContext,
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


        [Given(@"User creates Submission with Integration Data source as ""(.*)"" selects Submitted By as ""(.*)"" selects RelationshipRole as ""(.*)""")]
        public void GivenUserCreatesSubmissionWithIntegrationDataSourceAsSelectsSubmittedByAsSelectsRelationshipRoleAs(string integrationDataSourceName, string submittedByName, string relationshipRole)
        {
            var submissionPage = new SubmissionPage();
            submissionPage.ClickSubmissionMenu();
            submissionPage.ClickSubmissionNewPlusBtn();
            submissionPage.FillIntegrationDataSourceDetails(integrationDataSourceName);
            var jobPage = new JobPage();
            jobPage.FillSubmittedByDetails(submittedByName);
            jobPage.FillRelationshipRole(relationshipRole);
            submissionPage.SaveSubmission();
        }


        [Given(@"user clicks Submission menu")]
        public void GivenUserClicksSubmissionMenu()
        {
            var submissionPage = new SubmissionPage();
            submissionPage.ClickSubmissionMenu();
        }

        [Given(@"User selects Submitted by as ""(.*)""")]
        public void WhenUserSelectsSubmittedByAs(string submittedByName)
        {
            var jobPage = new JobPage();
            jobPage.FillSubmittedByDetails(submittedByName);
        }

        [When(@"User selects Submitted By")]
        public void WhenUserSelectsSubmittedBy()
        {
            var testData = inputoutputdata.testData;
            var jobPage = new JobPage();
            jobPage.FillSubmittedByDetails(testData["SubmittedBy"]);
        }

        [Given(@"user clicks Submission menu on UAT")]
        public void GivenUserClicksSubmissionMenuOnUAT()
        {
            var submissionPage = new SubmissionPage();
            submissionPage.ClickSubmissionMenu();
        }

        [Given(@"user clicks New submission button")]
        public void GivenUserClicksNewSubmissionButton()
        {
            var submissionPage = new SubmissionPage();
            submissionPage.ClickSubmissionNewPlusBtn();
        }

        [Given(@"user clicks New submission button on UAT")]
        public void GivenUserClicksNewSubmissionButtonOnUAT()
        {
            var submissionPage = new SubmissionPage();
            submissionPage.ClickSubmissionNewPlusBtn();
        }

        [Given(@"user selects Integration Data Source as ""(.*)""")]
        public void WhenUserSelectsIntegrationDataSourceAs(string integrationDataSourceName)
        {
            var submissionPage = new SubmissionPage();
            submissionPage.FillIntegrationDataSourceDetails(integrationDataSourceName);
        }

        [Given(@"user fills the mandatory fields on the Submission form")]
        public void GivenUserFillsTheMandatoryFieldsOnTheSubmissionForm()
        {
            WhenUserSelectsIntegrationDataSource();
            WhenUserSelectsSubmittedBy();
            WhenUserSelectsRelationshipRole();
        }


        [When(@"user selects Integration Data Source")]
        public void WhenUserSelectsIntegrationDataSource()
        {
            var testData = inputoutputdata.testData;
            var submissionPage = new SubmissionPage();
            submissionPage.FillIntegrationDataSourceDetails(testData["IntegrationDataSource"]);
        }

        [When(@"user selects RelationshipRole")]
        public void WhenUserSelectsRelationshipRole()
        {
            var testData = inputoutputdata.testData;
            var jobPage = new JobPage();
            jobPage.FillRelationshipRole(testData["RelationshipRole"]);
        }

        [When(@"user selects Integration Data Source as ""(.*)"" on UAT")]
        public void WhenUserSelectsIntegrationDataSourceAsOnUAT(string integrationDataSourceName)
        {
            var submissionPage = new SubmissionPage();
            submissionPage.FillIntegrationDataSourceDetails(integrationDataSourceName);
        }

        [When(@"User clicks overflow button under Related Requests")]
        public void WhenUserClicksOverflowButtonUnderRelatedRequests()
        {
            var submissionPage = new SubmissionPage();
            submissionPage.ClickOverFlowBtnUnderRelatedRequestSection();
        }

        [When(@"user selects New Request related requests sub menu")]
        public void WhenUserSelectsNewRequestRelatedRequestsSubMenu()
        {
            var submissionPage = new SubmissionPage();
            submissionPage.ClickNewRequestBtnUnderRequestsTab();
        }


        [Given(@"User clicks Save on Submission")]
        public void WhenUserClicksSaveOnSubmission()
        {
            var submissionPage = new SubmissionPage();
            submissionPage.SaveSubmission();
        }

        [Given(@"the User choose Related tab")]
        public void WhenTheUserChooseRelatedTab()
        {
            var submissionPage = new SubmissionPage();
            submissionPage.ClickRelatedTabElementOnSubmissionPage();
        }

        [Given(@"the user selects Requests option under RelatedTab")]
        public void WhenTheUserSelectsRequestsOptionUnderRelatedTab()
        {
            var submissionPage = new SubmissionPage();
            submissionPage.SelectRequestsUnderRelatedTab();
        }

        [Given(@"I click New Request Button")]
        public void WhenIClickNewRequestButton()
        {
            var submissionPage = new SubmissionPage();
            submissionPage.ClickNewRequestBtnUnderRequestsTab();
        }


        //***************Ashish's Step 240124******************
        [Given(@"User clicks on the latest submission from LA Portal")]
        public void GivenUserClicksClicksOnTheLatestSubmissionFromLAPortal()
        {
            try
            {
                var testData = inputoutputdata.testData;
                var submissionPage = new SubmissionPage();
                testData["SubmissionID"] = submissionPage.ClickOnTheLatestSubmission();
                Console.WriteLine(testData["SubmissionID"]);
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

        [Given(@"User filters the '(.*)' column with '(.*)' value")]
        public void GivenUserFiltersTheColumnWithValue(string columnName, string filterValue)
        {
            try
            {
                var submissionPage = new SubmissionPage();
                submissionPage.FilterColumnOnSubmissionTable(columnName, filterValue);
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

        [Given(@"User clicks on the Request link containing the Submission ID under Related Requests section")]
        public void GivenUserClicksOnTheRequestLinkContainingTheUnderSection()
        {
            try
            {
                var submissionPage = new SubmissionPage();
                submissionPage.ClickOnRequestLink();
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

        [Given(@"User validates the field values in Details section under Summary tab")]
        public void GivenUserValidatesTheFieldValuesInDetailsSectionUnderSummaryTab()
        {
            try
            {
                var testData = inputoutputdata.testData;
                var submissionPage = new SubmissionPage();
                submissionPage.ValidateFieldsInDetailsSection(testData["SubmissionID"], "LA Portal");
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

        [Given(@"User validates the field values in Remarks section under Summary tab")]
        public void GivenUserValidatesTheFieldValuesInRemarksSectionUnderSummaryTab()
        {
            try
            {
                var submissionPage = new SubmissionPage();
                submissionPage.ValidateFieldsInRemarksSection("QA Automation LA Portal testing");
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


        [Given(@"User fills the mandatory details on the Summary Tab of Submission Form")]
        public void GivenUserFillsTheMandatoryDetailsOnTheSummaryTabOfSubmissionForm()
        {
            var testData = inputoutputdata.testData;
            var submissionPage = new SubmissionPage();
            submissionPage.EnterMandatoryDataInSubmissionForm(testData["IDS"], testData["SubmittedBy"], testData["RelationshipRole"]);

        }

        [Given(@"User selects '(.*)' from the overflow command options under Related Requests")]
        public void GivenUserSelectsFromTheOverflowCommandOptionsUnderRelatedRequests(string optionToSelect)
        {
            var submissionPage = new SubmissionPage();
            submissionPage.SelectOverflowOption(optionToSelect);
        }



        [Given(@"User clicks on '(.*)' postion '(.*)' button from '(.*)'")]
        public void GivenUserClicksOnPostionButtonFrom(int position, string buttonName, string section)
        {
            try
            {

                if (section.ToLower().Equals("dialog"))
                {
                    try
                    {
                        if (!buttonName.Equals("Save & Close"))
                        {
                            //|| !buttonName.Equals("Complete Acceptance Check")
                            clickOnDialogMenuElement(buttonName, position);
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
                    //cp.waitforTopMenuBarLoading();
                    clickOnMainMenuMoreElement_New(buttonName);
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
        [Given(@"User click on '(.*)' button from '(.*)' and validate error message")]
        public void GivenUserClickOnButtonFromAndValidateErrorMessage(string buttonName, string section)
        {
            try
            {
                CommonPage cp = new CommonPage();
                clickOnMainMenuMoreElement_Validateerror(buttonName);
            }
            catch (Exception e)
            {
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
                pdf_Util.exceptionPdFLogger(e);
            }

        }



        [Given(@"User enter Property Attributes")]
        public void GivenUserEnterPropertyAttributes()
        {
            try
            {
                var testData = inputoutputdata.testData;


                if (String.IsNullOrWhiteSpace((string)_featureContext["Group"]))
                {
                    SeleniumExtensions.ScrollUntilSelectorDisplayedAndSendKeysBySearchIcon("Group", testData, 120);

                    //FillAndSelectLookUpResultWhenDataNotEntered("Group", testData);
                }
                if (String.IsNullOrWhiteSpace((string)_featureContext["Type"]))
                {
                    SeleniumExtensions.ScrollUntilSelectorDisplayedAndSendKeysBySearchIcon("Type", testData, 120);

                    //FillAndSelectLookUpResultWhenDataNotEntered("Type", testData);
                }
                if (String.IsNullOrWhiteSpace((string)_featureContext["Age Code"]))
                {
                    FillAndSelectLookUpFirstResultWhenDataNotEntered("Age Code", testData);

                    //FillAndSelectLookUpFirstResultWhenDataNotEntered("Age Code", testData);
                }
                if (String.IsNullOrWhiteSpace((string)_featureContext["Parking"]))
                {
                    FillAndSelectLookUpResultWhenDataNotEntered("Parking", testData);
                }
                /*if (String.IsNullOrWhiteSpace((string)_featureContext["Conservatory Type"]))
               {
                   FillAndSelectLookUpResultWhenDataNotEntered("Conservatory Type", testData);
               }*/
                if (String.IsNullOrWhiteSpace((string)_featureContext["Area"]))
                {
                    SeleniumExtensions.sendKeysFieldValue("Area", testData);
                }
                if (String.IsNullOrWhiteSpace((string)_featureContext["Rooms"]))
                {
                    SeleniumExtensions.sendKeysFieldValue("Rooms", testData);
                }
                if (String.IsNullOrWhiteSpace((string)_featureContext["Bedrooms"]))
                {
                    SeleniumExtensions.sendKeysFieldValue("Bedrooms", testData);
                }
                if (String.IsNullOrWhiteSpace((string)_featureContext["Bathrooms"]))
                {
                    SeleniumExtensions.sendKeysFieldValue("Bathrooms", testData);
                }
                if (!String.IsNullOrWhiteSpace((string)_featureContext["Floors"]))
                {
                }
                else
                {
                    enterDataWhenDataNotEntered("Floors", testData);
                }
                if (String.IsNullOrWhiteSpace((string)_featureContext["Level"]))
                {
                    SeleniumExtensions.sendKeysFieldValue("Level", testData);
                }

                //else if (String.IsNullOrWhiteSpace((string)_featureContext["Conservatory Area"]))
                //{
                //    SeleniumExtensions.sendKeysFieldValue("Conservatory Area", testData);
                //}

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




        [Given(@"User toggled on '(.*)'")]
        public void GivenUserToggledOn(string fieldName)
        {
            try
            {
                SeleniumExtensions.NoActionClickOnToggle(fieldName);
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



        [Given(@"User click on '(.*)' label")]
        public void GivenUserClickOnLabel(string fieldName)
        {
            try
            {
                SeleniumExtensions.clickOnElement(fieldName);
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



        [Given(@"User scroll to '(.*)' element")]
        public void GivenUserScrollToElement(string fieldName)
        {
            try
            {
                SeleniumExtensions.scrollToBtnElement(fieldName);
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

        [Given(@"User scroll to '(.*)' label element")]
        public void GivenUserScrollToLabelElement(string fieldName)
        {
            try
            {
                SeleniumExtensions.scrollToLabelElement(fieldName);
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


        [Given(@"User click '(.*)' button")]
        public void GivenUserClickButton(string fieldName)
        {
            try
            {
                var comsPage = new CommonPage();
                if (fieldName.Equals("New Request"))
                {
                    try
                    {
                        comsPage.SelectOverflowOptionValue(fieldName);
                    }
                    catch (Exception e)
                    {
                        SeleniumExtensions.scrollToBtnElementAndClick(fieldName);
                    }
                }
                else
                {
                    SeleniumExtensions.scrollToBtnElementAndClick(fieldName);
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

        [Given(@"User click on Find Plot button")]
        public void GivenUserClickOnFindPlotButton()
        {
            try
            {
                var comsPage = new CommonPage();
                comsPage.clickOnFindPlotBtn();
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

        [Given(@"User click on '(.*)' button for an incomplete plot")]
        public void GivenUserClickOnButtonForAnIncompletePlot(string buttonname)
        {
            try
            {
                var comsPage = new CommonPage();
                comsPage.clickOnresetBtn(buttonname);
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


        [Given(@"User click on (.*) position '(.*)' button on '(.*)'")]
        public void GivenUserClickOnPositionButtonOn(int position, string filedName, string role)
        {
            try
            {
                clickOnDialogBtnElement(filedName, position);
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


        [Given(@"User click on '(.*)' button on '(.*)'")]
        public void GivenUserClickOnButtonOn(string filedName, string role)
        {
            try
            {
                clickOnDialogBtnElement(filedName);
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


        [Given(@"User click on '(.*)' as title for button")]
        public void GivenUserClickOnAsTitleForButton(string fieldName)
        {
            try
            {
                var comsPage = new CommonPage();
                SeleniumExtensions.scrollToBtnWithTitleElementAndClick(fieldName);
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

        [Given(@"User click on '(.*)' button")]
        public void GivenUserClickOnButton(string fieldName)
        {
            try
            {
                var comsPage = new CommonPage();
                if (fieldName.Equals("New Request"))
                {
                    try
                    {
                        comsPage.SelectOverflowOption(fieldName);
                    }
                    catch (Exception e)
                    {
                        SeleniumExtensions.scrollToBtnElementAndClick(fieldName);
                    }
                }
                else
                {
                    SeleniumExtensions.scrollToBtnElementAndClick(fieldName);
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

        [Given(@"User entered data for '(.*)' field value from '(.*)' for '(.*)'")]
        public void GivenUserEnteredDataForFieldValueFromFor(string fieldName, string sheetName, string RowID)
        {
            SeleniumExtensions.setFieldValue(fieldName, sheetName, RowID);
        }

        [Given(@"User entered data for '(.*)' field value")]
        public void GivenUserEnteredDataForFieldValue(string fieldName)
        {
            var testData = inputoutputdata.testData;
            if (fieldName.Contains("Date") || fieldName.Equals("Internal Inspection SLA"))
            {
                SeleniumExtensions.SetTheDateForTheTableCalendar(fieldName, testData, 1);
            }
            else
            {
                SeleniumExtensions.setFieldValue(fieldName, testData);
            }

        }

        [Given(@"User enter '(.*)' in '(.*)' field in (.*) position")]
        public void GivenUserEnterInFieldInPosition(string value, string fieldName, int position)
        {
            try
            {
                SeleniumExtensions.sendKeysFieldValue(fieldName, value, position);
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


        [Given(@"User enter '(.*)' in '(.*)' field")]
        public void GivenUserEnterInField(string value, string fieldName)
        {
            try
            {
                SeleniumExtensions.sendKeysFieldValue(fieldName, value);
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

        [Given(@"User enter data for '(.*)' text area field")]
        public void GivenUserEnterDataForTextAreaField(string fieldName)
        {
            try
            {
                var testData = inputoutputdata.testData;
                _featureContext[fieldName] = testData[fieldName];
                SeleniumExtensions.sendKeysTextAreaFieldValue_1(fieldName, testData);
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

        [Given(@"User enter data for '(.*)' field value with value (.*)")]
        public void GivenUserEnterDataForFieldValueWithValue(string fieldName, string fieldValue)
        {
            try
            {
                var testData = inputoutputdata.testData;
                _featureContext[fieldName] = testData[fieldName];
                String numberOfPlotsStr = (string)_featureContext["numberOfPlots"];
                SeleniumExtensions.sendKeysFieldValue(fieldName, numberOfPlotsStr);
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


        [Given(@"user waits till '(.*)' '(.*)' disappears  and validate no check acceptance")]
        public void GivenUserWaitsTillDisappearsAndValidateNoCheckAcceptance(string hereditamentDate, string fieldName)
        {
            try
            {
                String hereditamentEffectiveDate = (string)_featureContext[hereditamentDate];
                _featureContext[fieldName] = SeleniumExtensions.enterAfterDateOfHereditamentEffectiveDateSequentially(hereditamentEffectiveDate, fieldName);
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
