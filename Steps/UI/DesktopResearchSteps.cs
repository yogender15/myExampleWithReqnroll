using NUnit.Framework;
using POMSeleniumFrameworkPoc1.DTO;
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
    public class DesktopResearchSteps
    {
        public DTO.InputOutputData inputoutputdata;
        public ScenarioContext _scenarioContext;
        public FeatureContext _featureContext;
        public NewJob newJobInformation;
        public DesktopResearchSteps(ScenarioContext context, NewJob _newJob, DTO.InputOutputData _inputoutputdata)
        {
            this.newJobInformation = _newJob;
            _scenarioContext = context;
            this.inputoutputdata = _inputoutputdata;
        }

        [When(@"User selects Submitted by as ""(.*)"" on UAT")]
        public void WhenUserSelectsSubmittedByAsOnUAT(string submittedByName)
        {
            var jobPage = new JobPage();
            jobPage.FillSubmittedByDetails(submittedByName);
        }

        [When(@"User clicks Details BPF")]
        public void WhenUserClicksDetailsBPF()
        {
            var jobPage = new JobPage();
            jobPage.SelectDetailsTab();
        }

        [When(@"User clicks the close button")]
        public void WhenUserClicksTheCloseButton()
        {
            var desktopResearchPage = new DesktopResearchPage();
            desktopResearchPage.ClickCloseBtn();
        }


        [When(@"User clicks Next button on the Details BPF")]
        public void WhenUserClicksNextButtonOnTheDetailsBPF()
        {
            var desktopResearchPage = new DesktopResearchPage();
            desktopResearchPage.ClickNextStageButtonInDetailsBPF();
        }

        [When(@"user clicks PAD Entry tab under BPF flow")]
        public void WhenUserClicksPADEntryTabUnderBPFFlow()
        {
            var desktopResearchPage = new DesktopResearchPage();
            desktopResearchPage.ClickNextStageButtonInDetailsBPF();
        }


        [When(@"User clicks researching tab on the BPF")]
        public void WhenUserClicksResearchingTabOnTheBPF()
        {
            var desktopResearchPage = new DesktopResearchPage();
            desktopResearchPage.ClickResearchingBPFTab();
        }

        [When(@"User clicks Next button on the Researching BPF")]
        public void WhenUserClicksNextButtonOnTheResearchingBPF()
        {
            var desktopResearchPage = new DesktopResearchPage();
            desktopResearchPage.ClickNextStageButtonOnBPF();
        }

        [When(@"User enters Desktop Research Date as")]
        public void WhenUserEntersDesktopResearchDateAs()
        {
            var desktopResearchPage = new DesktopResearchPage();
            desktopResearchPage.PickDesktopResearchDate(4);
        }

        [When(@"User enters Is Desktop Search Complete as Yes")]
        public void WhenUserEntersIsDesktopSearchCompleteAsYes()
        {
            var desktopResearchPage = new DesktopResearchPage();
            desktopResearchPage.DesktopResearchCompleteDropdown();
        }

        [When(@"User set flag for Is an Inspection Required as Yes")]
        public void WhenUserSetFlagForIsAnInspectionRequiredAsYes()
        {
            var desktopResearchPage = new DesktopResearchPage();
            desktopResearchPage.IsInspectionRequiredDropdown();
        }

        [When(@"User selects ""(.*)"" from Reason for inspection dropdown")]
        public void WhenUserSelectsFromReasonForInspectionDropdown(string reasonForInspection)
        {
            var desktopResearchPage = new DesktopResearchPage();
            desktopResearchPage.ReasonForInspectionDropdown(reasonForInspection);
        }

        [When(@"User enters Reason for Inspection as More information needed ""(.*)""")]
        public void WhenUserEntersReasonForInspectionAsMoreInformationNeeded(string moreDetails)
        {
            var desktopResearchPage = new DesktopResearchPage();
            desktopResearchPage.ReasonForInspectionInMoreDetail(moreDetails);
        }

        [When(@"User set flag for Is PAD Code Correct\\? as Yes")]
        public void WhenUserSetFlagForIsPADCodeCorrectAsYes()
        {
            var desktopResearchPage = new DesktopResearchPage();
            desktopResearchPage.SelectPADCodeCorrectDropdown();
        }

        [When(@"user set flag Affected Assessment Confirmed as Yes")]
        public void WhenUserSetFlagAffectedAssessmentConfirmedAsYes()
        {
            var desktopResearchPage = new DesktopResearchPage();
            desktopResearchPage.SelectAffectedAssessmentConfirmedDropdown();
        }

        [When(@"And user selects the '(.*)' with any dropdown value")]
        public void WhenUserSelectsanyDropDownValue(string dropDownValue)
        {
            var testData = inputoutputdata.testData;
            var desktopResearchPage = new DesktopResearchPage();
            desktopResearchPage.SelectingDropDownValueforComposite(dropDownValue, testData);
        }

        [When(@"User checks the associated CR10 value as (.*) and then validates the tab")]
        public void WhenUserChecksTheSYesAndThenValidatesTheTab(string materialIncreaseValue)
        {
            var testData = inputoutputdata.testData;
            var desktopResearchPage = new DesktopResearchPage();
            desktopResearchPage.CheckHasAssociatedMaterialIncrease(materialIncreaseValue);
        }


        [Then(@"Verify Address grid displayed")]
        public void ThenVerifyAddressGridDisplayed()
        {
            var desktopResearchPage = new DesktopResearchPage();
            Assert.IsTrue(desktopResearchPage.VerifyAddressGridOnDeskTopReasearch(), "Address Grid is not available on the Desktop Research page");
        }

        [When(@"user move the toggle Is the BA Reference Missing\\? as Yes")]
        public void WhenUserMoveTheToggleIsTheBAReferenceMissingAsYes()
        {
            var desktopResearchPage = new DesktopResearchPage();
            desktopResearchPage.MoveTheToggleIstheBAReferenceMissing();
        }

        [When(@"enter BA Reference Missing Comments as Reference Needed as ""(.*)""")]
        public void WhenEnterBAReferenceMissingCommentsAsReferenceNeededAs(string comments)
        {
            var desktopResearchPage = new DesktopResearchPage();
            desktopResearchPage.EnterBAReferenceMissingComments(comments);
        }

        [When(@"user moves the toggle BA Reference Requested From Local Authority\\? as Yes")]
        public void WhenUserMovesTheToggleBAReferenceRequestedFromLocalAuthorityAsYes()
        {
            var desktopResearchPage = new DesktopResearchPage();
            desktopResearchPage.MoveTheToggleBAReferenceRequestedFromLocalAuthority();
        }

        [Then(@"record should be saved")]
        public void ThenRecordShouldBeSaved()
        {
            var desktopResearchPage = new DesktopResearchPage();
            Assert.IsTrue(desktopResearchPage.VerifySaveStatusIsDisplayed(), "Job is not saved as Saved message is not displayed");
        }

        [When(@"User navigate to TCTR Information tab")]
        public void WhenUserNavigateToTCTRDetailsTab()
        {
            var desktopResearchPage = new DesktopResearchPage();
            desktopResearchPage.ClickTCTRInformationTab();
        }

        [When(@"selects Are TCTR Responses Complete\? as ""(.*)""")]
        public void WhenSelectsAreTCTRResponsesCompleteAs(string tctrResponses)
        {
            var desktopResearchPage = new DesktopResearchPage();
            desktopResearchPage.SelectAreTCTRResponsesComplete(tctrResponses);
        }

        [When(@"Reason for Incomplete TCTR Response as ""(.*)""")]
        public void WhenReasonForIncompleteTCTRResponseAs(string tctrOption)
        {
            var desktopResearchPage = new DesktopResearchPage();
            desktopResearchPage.SelectReasonForIncompleteTCTRResponse(tctrOption);
        }

        [When(@"User clicks Save button")]
        public void WhenUserClicksSaveButton()
        {
            var desktopResearchPage = new DesktopResearchPage();
            desktopResearchPage.ClickSaveBtnOnMainNav();
        }

        [When(@"User selects the Desktop Research Job Id")]
        public void WhenUserSelectsTheDesktopResearchJobId()
        {
            var desktopResearchPage = new DesktopResearchPage();
            desktopResearchPage.SelectCreatedJobIdInDetailsTabNextStage(newJobInformation.JobId);
        }

        [When(@"User clicks Maintain Assessment tab on the BPF")]
        public void WhenUserClicksMaintainAssessmentTabOnTheBPF()
        {
            var desktopResearchPage = new DesktopResearchPage();
            desktopResearchPage.SelectMaintainAssessmentOnBPF();
        }

        [When(@"User clicks Next button on the  Maintain Assessment BPF")]
        public void WhenUserClicksNextButtonOnTheMaintainAssessmentBPF()
        {
            var desktopResearchPage = new DesktopResearchPage();
            desktopResearchPage.ClickNextStageButtonOnBPF();
        }

        [When(@"User clicks Next button under Desktop Research BPF")]
        public void WhenUserClicksNextButtonUnderDesktopResearchBPF()
        {
            var desktopResearchPage = new DesktopResearchPage();
            desktopResearchPage.ClickNextStageButtonOnBPF();
        }

        [When(@"user clicks Next button under Undertake Banding BPF")]
        public void WhenUserClicksNextButtonUnderUndertakeBandingBPF()
        {
            var desktopResearchPage = new DesktopResearchPage();
            desktopResearchPage.ClickNextStageButtonOnBPF();
        }


        [When(@"User clicks Finish button on Release And Publish tab on BPF")]
        public void WhenUserClicksFinishButtonOnReleaseAndPublishTabOnBPF()
        {
            var desktopResearchPage = new DesktopResearchPage();
            desktopResearchPage.ClickFinishBtnOnReleaseAndPublishBPF();
        }

        [When(@"User set flag for Maintain Assessment Required as ""(.*)""")]
        public void WhenUserSetFlagForMaintainAssessmentRequiredAs(string yesNoFlag)
        {
            var desktopResearchPage = new DesktopResearchPage();
            desktopResearchPage.SelectMaintainAssessmentCompleteDrpBPF(yesNoFlag);
        }

        [When(@"User set flag for Correspondence Checks Complete\? as ""(.*)""")]
        public void WhenUserSetFlagForCorrespondenceChecksCompleteAs(string yesNoFlag)
        {
            var bpfPage = new BPFPage();
            bpfPage.SelectCorrespondenceChecksCompleteYesNoDropDown(yesNoFlag);
        }

        [When(@"User set flag for System Requires Quality Assurance\? as ""(.*)""")]
        public void WhenUserSetFlagForSystemRequiresQualityAssuranceAs(string yesNoFlag)
        {
            var bpfPage = new BPFPage();
            bpfPage.SelectSystemRequiresQualityAssuranceYesNoDropDown(yesNoFlag);
        }

        [Then(@"the system requires quality assurance field is available")]
        public void ThenTheSystemRequiresQualityAssuranceFieldIsAvailable()
        {
            var desktopResearchPage = new DesktopResearchPage();
            Assert.IsTrue(desktopResearchPage.IsSystemRequiresQualityAssuranceOptionDisplayed(), "The system requires quality assurance field is NOT displayed");

        }

        [Then(@"I should be able to see Finshed button after Release and Publish")]
        public void ThenIShouldBeAbleToSeeFinshedButtonAfterReleaseAndPublish()
        {
            var bpfPage = new BPFPage();
            Assert.IsTrue(bpfPage.VerifyFinishedBtnDisplayedAfterReleaseAndPublishBPF(), "The Finsished button is not displayed after Release and publish");
        }

        [Given("the {string} button should not be visible")]
        public void ThenTheButtonShouldNotBeVisible(string label)
        {
            var bpfPage = new BPFPage();

            Assert.IsTrue(bpfPage.VerifyButtonNotVisible(label), $"The '{label}' button should not be visible.");

        }


        [Given(@"User validates Knowledge Section in the '(.*)' stage")]
        public void GivenUserValidatesKnowledgeSectionInTheStage(string stage, Table table)
        {
            DesktopResearchPage drPage = new DesktopResearchPage();
            SeleniumExtensions.WaitForReadyStateToComplete();
            drPage.validateKnowledgeSectionUrls(table);
        }

        [When(@"And user Updates the Check Facts and PVT Tab Values on the Desktop Research")]
        public void WhenUserUpdatesCheckFactsAndPVTTabOnTheDesktopResearch()
        {
            var testData = inputoutputdata.testData;
            var desktopResearchPage = new DesktopResearchPage();
            desktopResearchPage.UserUpdatesTheCheckFactsAndPVTValues(testData);
        }

        [Given(@"User Sets the Auto process to Yes on the Destop Researching Stage on the BPF journey")]
        public void GivenUserValidatesTheAutoProcessOnTheBPFFlowAndValidatesTillReleaseAndPublish()
        {
            var testData = inputoutputdata.testData;
            var desktopResearchPage = new DesktopResearchPage();
            desktopResearchPage.UserSetstheAutoProcess(testData);
        }

        [Given(@"User navigates to '(.*)' and on the Hereditaments details section")]
        public void GivenUserValidatesTheNewlyGeneratedBAReferenceOnTheHereditamentsDetailsSection(string tabName, Table table)
        {
            var testdata = inputoutputdata.testData;
            var desktopResearchPage = new DesktopResearchPage();
            desktopResearchPage.UserNavigatesToHereditamentsDetails(tabName);
            //var requestPage = new RequestPage();
            //requestPage.UserValidatestheBillingAuthorityTableValues(tabName, testdata["expBillingAuthority"], testdata["proposedBARef"], testdata["PostCode"], testdata["expEffectiveChangedDate"]);

            var individualJobPage = new IndividualJobPage();
            foreach (var row in table.Rows)
            {
                string BillingAuthority = row["BillingAuthority"];
                string BillingAuthorityReference = row["BillingAuthorityReference"];
                string CommunityCode = row["CommunityCode"];
                string EffectiveFromDays = row["EffectiveFrom"];
                string EffectiveToDays = row["EffectiveTo"];
                string Status = row["Status"];
                //  _featureContext["effective_from_date"] = "7/8/2024";
                // _featureContext["expEffectiveChangedDate"] = "7/10/2024";
                //if (EffectiveFromDays == "")
                //{
                //    _featureContext[EffectiveFromDays] = "";
                //}
                //if (EffectiveToDays == "")
                //{
                //    _featureContext[EffectiveToDays] = "";
                //}
                individualJobPage.UserValidatestheBillingAuthorityTableValues(tabName, testdata["BillingAuthority"], testdata["expBillingAuthorityReference"], testdata["BillingAuthorityReference"], testdata["CommunityCode"], testdata["EffectiveFromDays"], testdata["EffectiveToDays"], Status);
                //   individualJobPage.ValidateAssessmentForBP(tabName, "03/07/2018 12:00", "03/07/2018 12:00", TaxBand, CompIndicator, AssessmentStatus, PublicationStatus);


            }
        }

        [Given(@"User selects '(.*)' for the Validate to release field for Auto Processing")]
        public void GivenUserSelectsForTheValidateToReleaseFieldForAutoProcessing(string selectValue)
        {
            var desktopResearchPage = new DesktopResearchPage();
            desktopResearchPage.SelectValidateForReleaseValue(selectValue);

        }

        [Given(@"User navigates validate and clicks on the Hereditament Link on '(.*)' tab under '(.*)'")]
        public void GivenUserNavigatesValidateAndClicksOnTheHereditamentLinkOnTab(string tabName, string FormName)
        {
            var desktopResearchPage = new DesktopResearchPage();
            desktopResearchPage.UserClicksOnHereditamentLink(tabName, FormName);

        }

        [Given(@"User selects the supplementary job from the Associated Jobs tab")]
        public void GivenUserSelectsTheSupplementaryJobFromTheAssociatedJobsTab()
        {
            var testData = inputoutputdata.testData;
            var desktopResearchPage = new DesktopResearchPage();
            //   desktopResearchPage.SelectSupllementaryFromAssociatedJobTab(buildingNum);
            desktopResearchPage.SelectSupllementaryFromAssociatedJobTab(testData["addressString"]);

        }

        [Given(@"User captures the Address String from the Hereditament link")]
        public void GivenUserCapturesTheAddressStringFromTheHereditamentLink()
        {
            var testData = inputoutputdata.testData;
            var desktopResearchPage = new DesktopResearchPage();
            testData["addressString"] = desktopResearchPage.GetAddressStringFromHereditamentLink();
        }


    }
}