using NUnit.Framework;
using OpenQA.Selenium;
using POMSeleniumFrameworkPoc1.DTO;
using POMSeleniumFrameworkPoc1.DTO.UI;
using POMSeleniumFrameworkPoc1.Helpers;
using POMSeleniumFrameworkPoc1.Pages;
using POMSeleniumFrameworkPoc1.Pages.UI.CommonComponents;
using POMSeleniumFrameworkPoc1.Pages.UI.RBSP_BP;
using Reqnroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POMSeleniumFrameworkPoc1.Steps.UI
{
    [Binding]
    public class RequestSteps : BasePage
    {

        public DTO.InputOutputData inputoutputdata;
        public ScenarioContext _scenarioContext;
        public FeatureContext _featureContext;
        public NewJob newJobInformation;

        public RequestSteps(ScenarioContext scenarioContext, FeatureContext featureContext,
             NewJob _newJob, NewJob _newRequestId, DTO.InputOutputData _inputoutputdata)
        {
            this.inputoutputdata = _inputoutputdata;
            this.newJobInformation = _newJob;
            this.newJobInformation = _newRequestId;
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
        }

        [Given(@"the User choose Request Menu")]
        public void GivenTheUserChooseRequestMenu()
        {
            try
            {
                var requestPage = new RequestPage();
                requestPage.ClickRequestMenuItem();
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


        [Given(@"User pick the proposed date as (.*) days")]
        public void WhenUserPickTheProposedDateAsDays(int days)
        {
            try
            {
                var jobPage = new JobPage();
                jobPage.PickProposedDate(days);
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

        [When(@"User selects Validate Request")]
        public void WhenUserSelectsValidateRequest()
        {
            try
            {
                var requestPage = new RequestPage();
                requestPage.ClickValidateRequestSubMenuOption();
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

        [When(@"User clicks Save & Close button on Request validation window")]
        public void WhenUserClicksSaveCloseButtonOnRequestValidationWindow()
        {
            try
            {
                var requestPage = new RequestPage();

                requestPage.ClickSaveAndCloseRequestValidateDialog();
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

        [When(@"user clicks Related Jobs tab")]
        public void WhenUserClicksRelatedJobsTab()
        {
            try
            {
                var requestPage = new RequestPage();
                requestPage.ClickRelatedJobsSubTab();
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

        [When(@"I click created job")]
        public void WhenIClickCreatedJob()
        {
            try
            {
                var requestPage = new RequestPage();
                newJobInformation.JobId = requestPage.ClickOnJobCreatedAndSaveJobId();
                _featureContext["Job_ID"] = newJobInformation.JobId;
                Console.WriteLine("This is the new Job ID" + _featureContext["Job_ID"]);
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

        [When(@"I click created job for '(.*)' Request")]
        public void WhenIClickCreatedJobForRequest(string businessProcessName)
        {
            try
            {
                var requestPage = new RequestPage();
                newJobInformation.JobId = requestPage.ClickOnJobCreatedAndSaveJobId();
                if (businessProcessName == "Individual")
                {

                    _featureContext["Ind_JobID"] = newJobInformation.JobId;
                    Console.WriteLine("This is the new Job ID" + _featureContext["Ind_JobID"]);
                }
                else if (businessProcessName == "Deletion")
                {

                    _featureContext["Del_JobID"] = newJobInformation.JobId;
                    Console.WriteLine("This is the new Job ID" + _featureContext["Del_JobID"]);
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


        [Then(@"I should be on the created job page")]
        public void ThenIShouldBeOnTheCreatedJobPage()
        {
            try
            {
                var requestPage = new RequestPage();
                Assert.IsTrue(requestPage.IsJobCreatedRequestPageIsDisplayed(newJobInformation.JobId));
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

        [Given(@"User enters the Sub Job Type in Details Tab")]
        public void GivenUserEntersTheSubJobTypeInDetailsTab()
        {
            try
            {
                var testData = inputoutputdata.testData;
                var requestPage = new RequestPage();
                requestPage.EnterSubJobTypeAtDetails(testData);
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


        [Then(@"I should be able to see the job created with Job")]
        public void ThenIShouldBeAbleToSeeTheJobCreatedWithJob()
        {
            try
            {
                var requestPage = new RequestPage();
                Assert.IsTrue(requestPage.VerifyJobCreatedDisplayedOnRelatedJobsTab());
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

        [When(@"User clicks Save on Requests Page")]
        public void WhenUserClicksSaveOnRequestsPage()
        {
            try
            {
                var requestPage = new RequestPage();
                newJobInformation.RequestId = requestPage.GetRequestId();
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

        [When(@"User clicks Save on Requests Page under Service menu")]
        public void WhenUserClicksSaveOnRequestsPageUnderServiceMenu()
        {
            try
            {
                var requestPage = new RequestPage();
                requestPage.ClickSaveBtnOnMainNav();
                Assert.IsTrue(requestPage.IsRequestSaved(), "Request is NOT saved");
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

        [Then(@"I should be able to Saved status")]
        public void ThenIShouldBeAbleToSavedStatus()
        {
            try
            {
                var requestPage = new RequestPage();
                Assert.IsTrue(requestPage.VerifySaveBtnIsDisplayed(), "Saved text is not displayed and Request is not saved");
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


        [When(@"User clicks Requests Action dropdown tab on the main Nav")]
        public void WhenUserClicksRequestsActionDropdownTabOnTheMainNav()
        {
            try
            {
                var requestPage = new RequestPage();
                requestPage.ClickRequestActionDropdownMenuBtn();
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

        [When(@"User clicks Requests Action dropdown tab on the Menu")]
        public void WhenUserClicksRequestsActionDropdownTabOnTheMenu()
        {
            try
            {
                var requestPage = new RequestPage();
                requestPage.ClickJobActionDropdownMenuBtn();
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


        [When(@"User selects Submit job")]
        public void WhenUserSelectsSubmitJob()
        {
            try
            {
                var requestPage = new RequestPage();
                requestPage.ClickSubmitJobSubMenu();
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

        [When(@"User selects No Action dropdown option under Request Action")]
        public void WhenUserSelectsNoActionDropdownOptionUnderRequestAction()
        {
            try
            {
                var requestPage = new RequestPage();
                requestPage.ClickNoActionRequestJobSubMenu();
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

        [When(@"user enters name of New Action as ""(.*)""")]
        public void WhenUserEntersNameOfNewActionAs(string noActionName)
        {
            try
            {
                var requestPage = new RequestPage();
                requestPage.ClickNoActionRequestJobSubMenu();
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

        [Given("User validate {string} text and click {string} button")]
        public void GivenUserValidateTextAndClickButton(string text, string okbutton)
        {

            try
            {
                var requestPage = new RequestPage();
                requestPage.clickokonpopup(text, okbutton);
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



        [When(@"User clicks on UnsavedChanges OK button")]
        public void WhenUserClicksOnUnsavedChangesOKButton()
        {
            try
            {
                var requestPage = new RequestPage();
                requestPage.ClickOkButtonOnDialog();
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

        [Given(@"User enters reason for validation as '(.*)'")]
        public void WhenUserEntersReasonForValidationAs(string reason)
        {
            try
            {
                var requestPage = new RequestPage();
                requestPage.SetReasonForValidation(reason);
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

        [When(@"User clicks on OK")]
        public void WhenUserClicksOnOK()
        {
            try
            {
                var requestPage = new RequestPage();
                requestPage.ClickOkButtonOnDialog();
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


        [When(@"User clicks proceed")]
        public void WhenUserClicksProceed()
        {
            try
            {
                var requestPage = new RequestPage();
                requestPage.ClickProceedButtonOnDialog();
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

        [Given(@"User fills Request type as ""(.*)"" in Request Page")]
        public void WhenUserFillsRequestTypeAsInRequestPage(string requestType)
        {
            try
            {
                var requestPage = new RequestPage();
                requestPage.FillRequestTypeOnRequestCreation(requestType);
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

        [When(@"User fills Coded Reason as ""(.*)"" in Request Page")]
        public void WhenUserFillsCodedReasonAsInRequestPage(string codedReason)
        {
            try
            {
                var requestPage = new RequestPage();
                requestPage.FillRequestTypeOnRequestCreation(codedReason);
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

        [Then(@"the Active Request is created successfully")]
        public void ThenTheActiveRequestIsCreatedSuccessfully()
        {
            try
            {
                var requestPage = new RequestPage();
                Assert.NotNull(requestPage.GetRequestId(), "the request Id on Request page is not displayed");
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

        [When(@"User fill Coded Reason on Request Page as ""(.*)""")]
        public void WhenUserFillCodedReasonOnRequestPageAs(string codedReason)
        {
            try
            {
                var requestPage = new RequestPage();
                requestPage.FillCodedReasonOnJobCreation(codedReason);
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

        [Given(@"User fill Job Type on Request Page as ""(.*)""")]
        public void WhenUserFillJobTypeOnRequestPageAs(string jobType)
        {
            try
            {
                var requestPage = new RequestPage();
                requestPage.FillJobTypeLookUpField(jobType);
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

        [When(@"User fill Request type on Request page as ""(.*)""")]
        public void WhenUserFillRequestTypeOnRequestPageAs(string requestType)
        {
            try
            {
                var requestPage = new RequestPage();
                requestPage.FillRequestTypeOnRequestCreation(requestType);
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

        [Given(@"User fill Job Subtype Type on Request Page as ""(.*)""")]
        public void WhenUserFillJobSubtypeTypeOnRequestPageAs(string jobSubType)
        {
            try
            {
                var requestPage = new RequestPage();
                requestPage.FillJobSubTypeOnRequestCreation(jobSubType);
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

        [Given(@"user selects Proposed Billing Authority as '(.*)'")]
        public void WhenUserSelectsProposedBillingAuthorityAs(string proposedBillingAuthority)
        {
            try
            {
                var requestPage = new RequestPage();
                requestPage.FillProposedBillingAuthority(proposedBillingAuthority);
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

        [Given(@"user enters Proposed BA Reference Number as '(.*)'")]
        public void WhenUserEntersProposedBAReferenceNumberAs(string proposedBAReferenceNumber)
        {
            try
            {
                var requestPage = new RequestPage();
                requestPage.FillProposedBAReferenceNumber(proposedBAReferenceNumber);
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

        [Given(@"user enter address label as '(.*)'")]
        public void WhenUserEnterAddressLabelAs(string addressLabel)
        {
            try
            {
                var requestPage = new RequestPage();
                // requestPage.FillAddressLabel(addressLabel);
                requestPage.SelectUniqueAddressLabel(addressLabel);
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

        [When(@"user enters the reason for validation as ""(.*)""")]
        public void WhenUserEntersTheReasonForValidationAs(string validationReason)
        {
            try
            {
                var requestPage = new RequestPage();
                requestPage.FillAddressLabel(validationReason);
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

        [Given(@"User fills the mandatory fields in the New Property Estate section under the Summary tab of the Request form")]
        public void WhenUserFillsTheMandatoryFieldsInTheCRNewPropertyEstateSectionUnderTheSummaryTabOfTheRequestForm()
        {
            try
            {
                var requestPage = new RequestPage();
                //   requestPage.EnterDataInCR03NewPropertySection("Adur Council - 19/04/2024 07:14");
                requestPage.EnterDataInCR03NewPropertySection((string)_featureContext["EstateFileName"]);
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

        [Given(@"User selects the plot from the dialog")]
        public void WhenUserSelectsThePlotFromTheDialog()
        {
            try
            {
                var testData = inputoutputdata.testData;
                var requestPage = new RequestPage();
                // requestPage.FindPlotFromDialog();
                requestPage.SelectPlotFromEstateFile(testData["HouseTypeName"]);
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

        [Given(@"the user selects '(.*)' option under Related Tab")]
        public void GivenTheUserSelectsOptionUnderRelatedTab(string optionToSelect)
        {
            try
            {
                var requestPage = new RequestPage();
                requestPage.ClickOnOptionInRelatedTab(optionToSelect);
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

        [Given(@"User validates the Auto Processing Runtime Status as '(.*)' for '(.*)' BP")]
        public void GivenUserValidatesTheAutoProcessingRuntimeStatusAs(string runtimeStatusText, string bpName)
        {
            try
            {
                var requestPage = new RequestPage();
                if (bpName == "New Property - Estate")
                {
                    requestPage.ValidateAutoProcessingRuntimeStatus(runtimeStatusText);
                }
                else if (bpName == "Full Demolition")
                {
                    requestPage.ValidateAutoProcessingRuntimeStatusForBP(runtimeStatusText);
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

        [Given(@"User validates the Job Status as '(.*)'")]
        public void GivenUserValidatesTheJobStatusAs(string statusCode)
        {
            try
            {
                var requestPage = new RequestPage();
                requestPage.ValidateJobStatusCode(statusCode);
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

        [Given(@"User clicks on the new Estate Job Created")]
        public void GivenUserClicksOnTheNewEstateJobCreated()
        {
            try
            {
                var requestPage = new RequestPage();
                string JobID = requestPage.ClickOnEtateJObID();
                Console.WriteLine(JobID);
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

        [Given(@"User fills the mandaory fields in the Categorisation section under Summary tab of Request Form")]
        public void GivenUserFillsTheMandaoryDetailsInTheCategorisationSectionUnderSummaryTabOfRequestForm()
        {
            try
            {
                var testData = inputoutputdata.testData;
                var requestPage = new RequestPage();
                if (testData["JobType"] == "New Property" || testData["JobType"] == "CR03")
                {
                    requestPage.EnterDetailsInCategorisationSection(testData["RequestType"], testData["JobType"], testData["SubJobType"]);

                }
                else
                {
                    requestPage.EnterDetailsInCategorisationSection(testData["RequestType"], testData["JobType"]);
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

        [Given(@"User fills the mandaory fields in the Details section under Summary tab of Request Form")]
        public void GivenUserFillsTheMandaoryDetailsInTheDetailsSectionUnderSummaryTabOfRequestForm()
        {
            try
            {
                var testData = inputoutputdata.testData;
                var requestPage = new RequestPage();
                requestPage.EnterFieldsInDetailsSection(float.Parse(testData["ProposedEffectiveDateDays"]), testData["ReasonForValidation"], testData["Channel"], testData["RelationshipRole"]);
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

        [Given(@"User fills the mandaory fields in the New Hereditament Details section under Summary tab of Request Form")]
        public void GivenUserFillsTheMandaoryFieldsInTheNewHereditamentDetailsSectionUnderSummaryTabOfRequestForm()
        {
            try
            {
                var testData = inputoutputdata.testData;
                var requestPage = new RequestPage();
                requestPage.EnterFieldsInNewHereditamentDetailsSection(testData["BillingAuthority"], testData["AddressLabel"]);
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

        [When(@"User fills the mandaory fields in the No Action Detail Pop-up")]
        public void WhenUserFillsTheMandaoryFieldsInTheNoActionDetailPop_Up()
        {
            try
            {
                var testData = inputoutputdata.testData;
                var requestPage = new RequestPage();
                requestPage.EnterFieldsInNoActionPopUp(testData["NoActionName"], testData["NoActionSubCode"], testData["NoActionCode"], testData["InternalRemarks"]);
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

        [When(@"User fills the mandatory fields in the No Action Detail Pop-up for Job Actions")]
        public void WhenUserFillsTheMandatoryFieldsInTheNoActionDetailPop_UpForJobActions()
        {
            try
            {
                var testData = inputoutputdata.testData;
                var requestPage = new RequestPage();
                requestPage.EnterFieldsInJobActionsNoActionPopUp(testData["NoActionName"], testData["NoActionSubCode"], testData["NoActionCode"], testData["InternalRemarks"]);
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


        [When(@"User validates the status as '(.*)'")]
        public void WhenUserValidatesTheStatusAs(string statusText)
        {
            try
            {
                var requestPage = new RequestPage();
                requestPage.ValidateNoActionStatus(statusText);
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

        [When(@"User selects '(.*)' option from the Request Action dropdown")]
        public void WhenUserSelectsOptionFromTheRequestActionDropdown(string requestActionOption)
        {
            try
            {
                var requestPage = new RequestPage();
                requestPage.SelectRequestActionDropdownOption(requestActionOption);
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

        [When(@"User clicks on the Save & Close button on the Dialog to Validate Request")]
        public void WhenUserClicksOnTheSaveCloseButtonOnTheDialogToValidateRequest()
        {
            try
            {
                var requestPage = new RequestPage();
                requestPage.ClickSaveAndCloseOnDialogforValidateRequest();
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

        [Given(@"User fills the mandaory fields in the BA Reference section under Summary tab of Request Form")]
        public void GivenUserFillsTheMandaoryFieldsInTheBAReferenceSectionUnderSummaryTabOfRequestForm()
        {
            try
            {
                var testData = inputoutputdata.testData;
                var requestPage = new RequestPage();
                requestPage.EnterFieldsInBAReferenceSection(testData["ProposedBARefNum"]);
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

        [Given(@"User click on the '(.*)' button")]
        public void GivenUserFindsTheHereditament(string fieldName)
        {
            try
            {
                if (fieldName.Equals("Find Hereditament"))
                {
                    var testData = inputoutputdata.testData;
                    var requestPage = new RequestPage();
                    requestPage.FindHereditamentForProcess(testData["AddressCardText"]);
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

        [Given(@"User selects '(.*)' job Type for the alternate Job")]
        public void GivenUserSelectsJobTypeForTheAlternateJob(string jobType)
        {
            try
            {
                var requestPage = new RequestPage();
                requestPage.SelectAlternateJobType(jobType);
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

        [Given(@"User clicks on OK Button on Leave this Page dialog")]
        public void GivenUserClicksOnOKButtonOnLeaveThisPageDialog()
        {
            try
            {
                var requestPage = new RequestPage();
                requestPage.ClickOnOkLeaveThisPageDialog();
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

        [Given(@"User selects Request Type as '(.*)' and Job Type as '(.*)' and creates the Request")]
        public void GivenUserSelectsRequestTypeAsAndJobTypeAsAndCreatesTheJob(string requestType, string jobType)
        {
            try
            {

                var testData = inputoutputdata.testData;
                Random rand = new Random();
                int randomBAReference = rand.Next(100000, 999999);
                testData["ProposedBARefNum"] = randomBAReference.ToString();
                try
                {
                    if (_featureContext["ba_reference_new"] != null)
                    {
                        testData["ProposedBARefNum"] = (string)_featureContext["ba_reference_new"];
                        _featureContext["ProposedBARefNum"] = testData["ProposedBARefNum"];
                    }
                }
                catch (KeyNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    _featureContext["ProposedBARefNum"] = testData["ProposedBARefNum"];

                }

                Console.WriteLine(_featureContext["ProposedBARefNum"]);
                var requestPage = new RequestPage();
                if (jobType == "New Property - Individual" || jobType == "Estate File")
                {
                    requestPage.CreateRequest(requestType, jobType, testData);

                }
                else if (jobType == "New Property - Estate")
                {
                    requestPage.CreateRequest(requestType, jobType, testData, (string)_featureContext["EstateFileName"]);

                }
                else
                {
                    _featureContext["expEffectiveChangedDate"] = requestPage.CreateRequest(requestType, jobType, testData, (string)_featureContext["effective_from_date"], (string)_featureContext["town"], (string)_featureContext["postcode"], (string)_featureContext["uprn"]);
                    //_featureContext["expEffectiveChangedDate"]=  requestPage.CreateRequest(requestType, jobType, testData, "1/27/2025 12:00:00 AM", "CARDIFF", "CF24 3PG", "10023549423");
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



        [Given(@"user select the '(.*)' for change of address")]
        public void GivenUserSelectTheForChangeOfAddress(string Postcode)
        {
            try
            {
                RequestPage requestPage = new RequestPage();
                var testData = inputoutputdata.testData;
                if (testData["Autoprocess"] == "Yes")
                {
                    FindAddressForChangeOfAddressforAuto(Postcode);
                }
                else
                {
                    requestPage.FindAddressForDuplicateChangeOfAddress(Postcode);

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



        [Given(@"User validates the Job creation by clicking on '(.*)' from '(.*)' menu for '(.*)' BP")]
        public void GivenUserValidatesTheJobCreationByClickingOnFromMenuForBP(string requestAction, string MenuOption, string jobType)
        {
            try
            {
                var testData = inputoutputdata.testData;
                var requestPage = new RequestPage();
                if (jobType.Contains("Change of BA Reference - Manual"))
                {
                    inputoutputdata.JobId = requestPage.ValidateJobCreation(requestAction, MenuOption, jobType, (string)_featureContext["ProposedBARefNum"], (string)_featureContext["town"], (string)_featureContext["postcode"], (string)_featureContext["uprn"], testData);
                    //  inputoutputdata.JobId = requestPage.ValidateJobCreation(requestAction, MenuOption, jobType, (string)_featureContext["expEffectiveChangedDate"], testData["SubmittedBy"],(string)_featureContext["ProposedBARefNum"]);
                    _featureContext[jobType] = inputoutputdata.JobId;
                }
                else if (jobType.Contains("Change of BA Reference - AutoProcess"))
                {
                    inputoutputdata.JobId = requestPage.ValidateJobCreation(requestAction, MenuOption, jobType, (string)_featureContext["ProposedBARefNum"], (string)_featureContext["town"], (string)_featureContext["postcode"], (string)_featureContext["uprn"], testData);
                    //  inputoutputdata.JobId = requestPage.ValidateJobCreation(requestAction, MenuOption, jobType, (string)_featureContext["expEffectiveChangedDate"], testData["SubmittedBy"],(string)_featureContext["ProposedBARefNum"]);
                    _featureContext[jobType] = inputoutputdata.JobId;
                }
                else if (jobType.Contains("Change of BA Reference - SwapHereditament"))
                {
                    inputoutputdata.JobId = requestPage.ValidateJobCreation(requestAction, MenuOption, jobType, (string)_featureContext["ProposedBARefNum"], (string)_featureContext["town_new"], (string)_featureContext["postcode_new"], (string)_featureContext["uprn_new"], testData);
                    //  inputoutputdata.JobId = requestPage.ValidateJobCreation(requestAction, MenuOption, jobType, (string)_featureContext["expEffectiveChangedDate"], testData["SubmittedBy"],(string)_featureContext["ProposedBARefNum"]);
                    _featureContext[jobType] = inputoutputdata.JobId;
                }
                else if (jobType.Contains("Reconstitution Split"))
                {
                    inputoutputdata.JobId = requestPage.ValidateJobCreation(requestAction, MenuOption, jobType, (string)_featureContext["ProposedBARefNum"], (string)_featureContext["town"], (string)_featureContext["postcode"], (string)_featureContext["uprn"], testData);
                    //  inputoutputdata.JobId = requestPage.ValidateJobCreation(requestAction, MenuOption, jobType, (string)_featureContext["expEffectiveChangedDate"], testData["SubmittedBy"],(string)_featureContext["ProposedBARefNum"]);
                    _featureContext[jobType] = inputoutputdata.JobId;
                }
                else if (jobType.Contains("Reconstitution Merge"))
                {
                    inputoutputdata.JobId = requestPage.ValidateJobCreation(requestAction, MenuOption, jobType, (string)_featureContext["ProposedBARefNum"], (string)_featureContext["town_new"], (string)_featureContext["postcode_new"], (string)_featureContext["uprn_new"], testData);
                    //  inputoutputdata.JobId = requestPage.ValidateJobCreation(requestAction, MenuOption, jobType, (string)_featureContext["expEffectiveChangedDate"], testData["SubmittedBy"],(string)_featureContext["ProposedBARefNum"]);
                    _featureContext[jobType] = inputoutputdata.JobId;
                }
                else if (jobType.Contains("New Estate"))
                {
                    inputoutputdata.JobId = requestPage.ValidateJobCreation(requestAction, MenuOption, jobType, testData);
                    _featureContext[jobType] = inputoutputdata.JobId;

                }
                else
                {
                    inputoutputdata.JobId = requestPage.ValidateJobCreation(requestAction, MenuOption, jobType);
                    _featureContext[jobType] = inputoutputdata.JobId;

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

        [Given(@"User click on '(.*)' from '(.*)' menu item")]
        public void GivenUserClickOnFromMenuItem(string requestAction, string MenuOption)
        {
            try
            {
                var requestPage = new RequestPage();
                requestPage.ClickMenuListOptionFromCommandBar(requestAction, MenuOption);
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


        [Given(@"User validates the '(.*)' value on the Status Reason Column on the Related CR tab")]
        public void GivenUserValidatesTheValueOnTheStatusReasonColumnOnTheRelatedCRTab(string statuslabel)
        {
            try
            {
                var requestPage = new RequestPage();
                requestPage.ValidateStatusReasonforMaterialIncreaseTab(statuslabel);
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


        
        [Given(@"User click on '(.*)' tab from '(.*)'")]
        public void GivenUserClickOnTabFrom(string tabName, string formName)
        {
            try
            {
                var requestPage = new RequestPage();
                requestPage.clickTabOnRequestForm(tabName, formName);
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

        [Given(@"user clicked on '(.*)' from the list")]
        public void GivenUserClickedOnFromTheList(string items)
        {
            try
            {
                var requestPage = new RequestPage();
                requestPage.clickchildjobs(items);
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



        [Given(@"User click on '(.*)' tab on dialog from '(.*)'")]
        public void GivenUserClickOnTabOnDialogFrom(string tabName, string formName)
        {
            try
            {
                var requestPage = new RequestPage();
                requestPage.clickTabOnDialog(tabName, formName);
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

        [Given(@"User clicks on request link under address tab")]
        public void GivenUserClicksOnRequestLinkUnderAddressTab()
        {
            try
            {
                var requestPage = new RequestPage();
                requestPage.clickOnRequestLinkUnderAddress();
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

        [Given(@"User navigates to filtered request")]
        public void GivenUserNavigatesToFilteredRequest()
        {
            try
            {
                var requestPage = new RequestPage();
                requestPage.navigateToRequest();
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
        [Given("User click on Hereditament link")]
        public void GivenUserClickOnHereditamentLink()
        {
            try
            {
                var requestPage = new RequestPage();
                requestPage.clickOnaddresslink();
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


        [Given(@"User click on ""(.*)"" element")]
        public void GivenUserClickOnElement(string p0)
        {
            try
            {
                var requestPage = new RequestPage();
                requestPage.clickOnJobID();
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

        [Given(@"user enters data in ""(.*)"" and selects unique address for new property")]
        public void GivenUserEntersDataInAndSelectsUniqueAddressForNewProperty(string fieldName)
        {
            try
            {
                var requestPage = new RequestPage();
                var testData = inputoutputdata.testData;
                requestPage.FindAddressForNewProperty(testData[fieldName]);
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

        [Given(@"user enters data in ""(.*)"" and selects unique address for new property with db data")]
        public void GivenUserEntersDataInAndSelectsUniqueAddressForNewPropertyWithDbData(string fieldName)
        {
            try
            {
                var requestPage = new RequestPage();
                var testData = inputoutputdata.testData;
                requestPage.FindAddressForNewPropertyzWithDB(testData[fieldName]);
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

        [Given(@"user enters data in ""(.*)"" and selects unique address in '(.*)' from db")]
        public void GivenUserEntersDataInAndSelectsUniqueAddressInFromDb(string fieldName, string storageContext)
        {
            try
            {
                var requestPage = new RequestPage();
                var testData = inputoutputdata.testData;
                String addressValue = requestPage.GetUniqueAddressForProperty(testData[fieldName], _featureContext);
                Console.WriteLine("Unique addressValue : " + addressValue);
                if (storageContext.Equals("scenarioContext", StringComparison.InvariantCultureIgnoreCase))
                {
                    _scenarioContext["Address"] = addressValue;
                }
                if (storageContext.Equals("featureContext", StringComparison.InvariantCultureIgnoreCase))
                {
                    _featureContext["Address"] = addressValue;
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

        [Given(@"user enters data in ""(.*)"" and selects unique address for estates in '(.*)'")]
        public void GivenUserEntersDataInAndSelectsUniqueAddressForEstatesIn(string fieldName, string storageContext)
        {
            try
            {
                var requestPage = new RequestPage();
                var testData = inputoutputdata.testData;
                String addressValue = requestPage.GetUniqueAddressForEsates(testData[fieldName], _featureContext);
                Console.WriteLine("Unique addressValue : " + addressValue);
                if (storageContext.Equals("scenarioContext", StringComparison.InvariantCultureIgnoreCase))
                {
                    _scenarioContext["Address"] = addressValue;
                }
                if (storageContext.Equals("featureContext", StringComparison.InvariantCultureIgnoreCase))
                {
                    _featureContext["Address"] = addressValue;
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
        [Given(@"user enters data in (.*) and selects all address for estates")]
        public void GivenUserEntersDataInAndSelectsAllAddressForEstates(string fieldName)
        {
            try
            {
                var requestPage = new RequestPage();
                requestPage.GetUniqueAddressForEsates_TestData(fieldName, _featureContext);
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





        [Given(@"user enters data in (.*) and selects (.*) unique address for estates in '(.*)'")]
        public void GivenUserEntersDataInAndSelectsUniqueAddressForEstatesInNew(string fieldName, string noOfPlots, string storageContext)
        {
            try
            {
                var requestPage = new RequestPage();
                var testData = inputoutputdata.testData;
                for (int i = 0; i < Int64.Parse(noOfPlots); i++)
                {
                    requestPage.GetUniqueMultipleAddressForEsates(fieldName);
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


        [Given(@"user enters data in ""(.*)"" and selects (.*) unique address for estates in '(.*)'")]
        public void GivenUserEntersDataInAndSelectsUniqueAddressForEstatesIn(string fieldName, string noOfPlots, string storageContext)
        {
            try
            {
                var requestPage = new RequestPage();
                var testData = inputoutputdata.testData;
                for (int i = 0; i < Int64.Parse(noOfPlots);)
                {
                    String addressValue = requestPage.GetUniqueAddressForEsates(testData[fieldName], _featureContext);
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

        [Given(@"User enters data for new property ""(.*)"" field")]
        public void GivenUserEntersDataForNewPropertyField(string fieldName)
        {
            try
            {
                var requestPage = new RequestPage();
                var testData = inputoutputdata.testData;
                String postCodeValue = testData[fieldName];
                requestPage.enterPostcodeForNewProperty(postCodeValue);
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

        [Given(@"user slects Unique address for change of address Auto process validation")]
        public void GivenUserSlectsUniqueAddressForChangeOfAddressAutoProcessValidation()
        {
            try
            {
                var requestPage = new RequestPage();
                var testData = inputoutputdata.testData;
                String addressValue = requestPage.GetUniqueAddressForPropertyForCOA_AP();
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


        [Given(@"user slects Unique address for change of address manual process validation")]
        public void GivenUserSlectsUniqueAddressForChangeOfAddressManualProcessValidation()
        {
            try
            {
                var requestPage = new RequestPage();
                var testData = inputoutputdata.testData;
                String addressValue = requestPage.GetUniqueAddressForPropertyForCOA();
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


        [Given(@"user slects already used address for change of address manual process validation")]
        public void GivenUserSlectsAlreadyUsedAddressForChangeOfAddressManualProcessValidation()
        {
            try
            {
                var requestPage = new RequestPage();
                var testData = inputoutputdata.testData;
                String addressValue = requestPage.GetUsedAddressForPropertyForCOA();
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




        [Given(@"user enters data in ""(.*)"" and selects unique address in '(.*)'")]
        public void GivenUserEntersDataInAndSelectsUniqueAddressInProperty(string fieldName, string storageContext)
        {
            try
            {
                var requestPage = new RequestPage();
                var testData = inputoutputdata.testData;
                String addressValue = requestPage.GetUniqueAddressForProperty(testData[fieldName], _featureContext);
                Console.WriteLine("Unique addressValue : " + addressValue);
                if (storageContext.Equals("scenarioContext", StringComparison.InvariantCultureIgnoreCase))
                {
                    _scenarioContext["Address"] = addressValue;
                }
                if (storageContext.Equals("featureContext", StringComparison.InvariantCultureIgnoreCase))
                {
                    _featureContext["Address"] = addressValue;
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




        [Given(@"User captures '(.*)' in '(.*)'")]
        public void GivenUserCapturesIn(string fieldName, string storageContext)
        {
            try
            {
                SeleniumExtensions.WaitForReadyStateToComplete();
                SeleniumExtensions.scrollToBtnElement(fieldName);
                RequestPage rp = new RequestPage();
                Dictionary<String, String> jobdetails = rp.UserCaptureFieldTitleAtributeDetails(fieldName);
                if (storageContext.Equals("scenarioContext", StringComparison.InvariantCultureIgnoreCase))
                {
                    _scenarioContext["Address"] = jobdetails[fieldName];
                }
                if (storageContext.Equals("featureContext", StringComparison.InvariantCultureIgnoreCase))
                {
                    _featureContext["Address"] = jobdetails[fieldName];
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

        [Given(@"User captures '(.*)' input field in '(.*)'")]
        public void GivenUserCapturesInputFieldIn(string fieldName, string storageContext)
        {
            try
            {
                SeleniumExtensions.WaitForReadyStateToComplete();
                SeleniumExtensions.scrollToBtnElement(fieldName);
                RequestPage rp = new RequestPage();
                Dictionary<String, String> jobdetails = rp.UserCaptureInPutFieldTitleAtributeDetails(fieldName);
                Console.WriteLine("Unique Address : " + jobdetails[fieldName]);
                if (storageContext.Equals("scenarioContext", StringComparison.InvariantCultureIgnoreCase))
                {
                    _scenarioContext[fieldName] = jobdetails[fieldName];
                }
                if (storageContext.Equals("featureContext", StringComparison.InvariantCultureIgnoreCase))
                {
                    _featureContext[fieldName] = jobdetails[fieldName];
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

        [Given(@"User captures ""(.*)"" for ""(.*)"" record in ""(.*)""")]
        public void GivenUserCapturesForRecordIn(string date, string pvtType, string storageContext)
        {
            try
            {
                SeleniumExtensions.WaitForReadyStateToComplete();
                PADEntryPage pvtPage = new PADEntryPage();
                if (storageContext.Equals("scenarioContext", StringComparison.InvariantCultureIgnoreCase))
                {
                    String x = pvtPage.getEffectiveFromDate();
                    _scenarioContext[date] = pvtPage.getEffectiveFromDate();
                }
                if (storageContext.Equals("featureContext", StringComparison.InvariantCultureIgnoreCase))
                {
                    _featureContext[date] = pvtPage.getEffectiveFromDate();
                }
            }
            catch (Exception e)
            {
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
                pdf_Util.exceptionPdFLogger(e);
            }
        }

        [Given(@"the user '(.*)' for '(.*)' and relationship As '(.*)' for the '(.*)'")]
        public void GivenTheUserForAndRelationship(string fieldName, string NameValue, string relatioshipValue, string bpName)
        {
            try
            {
                var testData = inputoutputdata.testData;
                var requestPage = new RequestPage();
                if (bpName == "Informal Challenge")
                {
                    requestPage.clickAddExisitingRequest(fieldName, NameValue, relatioshipValue, testData);
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

        [Given(@"the user adds record in '(.*)' for '(.*)' for the '(.*)'")]
        public void GivenTheUserAddsRecordInForForThe(string fieldName, string NameValue, string bpName)
        {
            try
            {
                var testData = inputoutputdata.testData;
                var requestPage = new RequestPage();
                if (bpName == "Informal Challenge")
                {
                    requestPage.AddCreatedPropertyLinkRecords(fieldName, NameValue, bpName, testData);
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

        [Given("User load and validate comparator")]
        public void GivenUserLoadAndValidateComparator()
        {
            try
            {
                var testData = inputoutputdata.testData;
                var comparatorTool = new ComparatorToolPage();
                comparatorTool.FindHereditamentForComparator((string)_featureContext["uprn_new"]);
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

        [Given("User {string} to {string},if {string} is {string}")]
        public void GivenUserToIfIs(string p0, string yes, string p2, string rejected)
        {
            try
            {
                var testData = inputoutputdata.testData;
                var comparatorTool = new ComparatorToolPage();
                comparatorTool.FindHereditamentForComparator((string)_featureContext["uprn_new"]);
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


        [Given(@"User provided comparable property details for informal challenge")]
        public void GivenUserProvidedComparablePropertyDetailsForInformalChallenge()
        {
            try
            {
                var testData = inputoutputdata.testData;
                var requestPage = new RequestPage();
                requestPage.IdentifyComparatorPropertyDetails((string)_featureContext["town_new"], (string)_featureContext["postcode_new"], (string)_featureContext["uprn_new"]);
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


        [Given(@"User Validates New Validity/Acceptance Check by clicking on '(.*)' from '(.*)' menu for '(.*)' BP")]
        public void GivenUserValidatesNewValidityAcceptanceCheckByClickingOnFromMenuForBP(string requestAction, string MenuOption, string jobType)
        {
            try
            {
                var testData = inputoutputdata.testData;
                var requestPage = new RequestPage();
                requestPage.ValidateCheckCTChallenge(requestAction, MenuOption, jobType, testData, (string)_featureContext["effective_from_date"], (string)_featureContext["town_new"], (string)_featureContext["postcode_new"], (string)_featureContext["uprn_new"]);
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

        [Given(@"User navigates to General tab and updates the Comparable Property Match Results for '(.*)' BP")]
        public void GivenUserNavigatesToGeneralTabAndUpdatesTheComparablePropertyMatchResultsForBP(string jobType)
        {
            try
            {
                var testData = inputoutputdata.testData;
                var requestPage = new RequestPage();
                requestPage.ValidateeComparablePropertyMatchResults(jobType, testData);
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


        [Given(@"the user validates the outbound Correspondence validates the '(.*)' and status as '(.*)'")]
        public void GivenTheUserValidatesTheOutboundCorrespondenceValidatesTheAndStatusAs(string country, string status)
        {
            try
            {
                var testData = inputoutputdata.testData;
                var requestPage = new RequestPage();
                requestPage.ValidatesTheOutboundCorrespondence(country, status);
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

        [Given(@"User validates Inspections '(.*)' with parent job id from '(.*)'")]
        public void GivenUserValidatesInspectionsWithParentJobIdFrom(string inspectionjobID, string storage)
        {
            try
            {
                var requestPage = new RequestPage();
                string jobId = null;
                if (storage.Equals("featureContext"))
                {
                    jobId = _featureContext[inspectionjobID].ToString();
                }
                if (storage.Equals("searchcontext"))
                {
                    jobId = _scenarioContext[inspectionjobID].ToString();

                }
                requestPage.ValidateInspectionJob(jobId);
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

        [Given(@"User clicks on Inspections '(.*)' element")]
        public void GivenUserClicksOnInspectionsElement(string p0)
        {
            try
            {
                var requestPage = new RequestPage();
                requestPage.clickOnInspectionJobID();
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


        [Given(@"User clicks on the Confirming Reconstitution Type")]
        public void GivenUserClicksOnTheConfirmingReconstitutionType()
        {
            try
            {
                var requestPage = new RequestPage();
                requestPage.ClickOnConfirmBtnForReconType();
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
        [Given(@"User filters '(.*)' coloum for '(.*)' request and navigate to it")]
        public void GivenUserFiltersColoumForRequestAndNavigateToIt(string coloumName, string requestType)
        {
            try
            {
                RequestPage requestPage = new RequestPage();
                requestPage.fileterNewlyCreatedRequest(coloumName, requestType);
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


        [Given(@"User filters '(.*)' coloum for newly created '(.*)' request and navigate to it")]
        public void GivenUserFiltersColoumForNewlyCreatedRequestAndNavigateToIt(string coloumName, string requestType)
        {
            try
            {
                RequestPage requestPage = new RequestPage();
                requestPage.fileterNewlyCreatedRequest(coloumName, requestType);

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

        [Given(@"User filters '(.*)' coloum for '(.*)' request with '(.*)' and navigate to it")]
        public void GivenUserFiltersColoumForCreatedRequestAndNavigateToIt(string coloumName, string requestType, String requetName)
        {
            try
            {
                RequestPage requestPage = new RequestPage();
                String requestName = (string)_featureContext[requetName];
                requestPage.fileterRequriedRequest(coloumName, requestName);
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
        [Given(@"User filters '(.*)' coloumn  with '(.*)' and navigate to it")]
        public void GivenUserFiltersColoumnWithAndNavigateToIt(string coloumName, String fieldvalue)
        {
            try
            {
                RequestPage requestPage = new RequestPage();

                requestPage.filterVOA(coloumName, fieldvalue);
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

        [Given(@"User filled (.*) '(.*)' details for '(.*)' in a proposal job")]
        public void GivenUserFilledDetailsForInAProposalJob(int numberOfSplits, string AddIncoming, string PostCode)
        {
            try
            {
                RequestPage requestPage = new RequestPage();
                var testData = inputoutputdata.testData;
                requestPage.EnterDetailsInReconTabForSplitproposal(numberOfSplits, testData[PostCode]);
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
        [Given("User filled {int} {string} details for {string} in a proposal job for guardrail validation")]
        public void GivenUserFilledDetailsForInAProposalJobForGuardrailValidation(int numberOfSplits, string AddIncoming, string PostCode)
        {
            try
            {
                RequestPage requestPage = new RequestPage();
                var testData = inputoutputdata.testData;
                requestPage.EnterDetailsInReconTabForSplitproposal_wales_guardrails(numberOfSplits, testData[PostCode]);
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

        [Given("User filled {int} {string} details for {string} in a proposal job for wales")]
        public void GivenUserFilledDetailsForInAProposalJobForWales(int numberOfSplits, string AddIncoming, string PostCode)
        {
            try
            {
                RequestPage requestPage = new RequestPage();
                var testData = inputoutputdata.testData;
                requestPage.EnterDetailsInReconTabForSplitproposal_wales(numberOfSplits, testData[PostCode]);
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


        [Given(@"User filled (.*) '(.*)' details for '(.*)'")]
        public void GivenUserFilledDetailsFor(int numberOfSplits, string AddIncoming, string PostCode)
        {
            try
            {
                RequestPage requestPage = new RequestPage();
                var testData = inputoutputdata.testData;
                requestPage.EnterDetailsInReconTabForSplit(numberOfSplits, testData[PostCode]);
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

        [Given("User enters data for  {string} field")]
        public void GivenUserEntersDataForField(string fieldName)
        {
            try
            {
                var testData = inputoutputdata.testData;
                var commonPage = new CommonPage();
                commonPage.enterDataInproposedbareferencefield(fieldName, testData, _featureContext);
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
