using OpenQA.Selenium;
using POMSeleniumFrameworkPoc1.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POMSeleniumFrameworkPoc1.Pages.UI.CommonComponents
{
    public partial class DesktopResearchPage : BasePage
    {
        private IWebElement DetailsBpf => Driver.WaitForElement(By.XPath("//div[contains(@title,'Details') and contains(@id,'stageNameContainer')]"));
        private IWebElement DesktopResearchFormElement => Driver.WaitForElement(By.CssSelector("ul[aria-label='Desktop Research Form']"));
        private IWebElement NextStageBtnElementOnBPF => Driver.WaitForElement(By.CssSelector("button[aria-label='Next Stage']"));
        private IWebElement DetailsOptionTab => Driver.WaitForElement(By.XPath("//*[@data-id='MscrmControls.Containers.ProcessBreadCrumb-stageStatusLabel']"));
        private IWebElement AutoProcessDropDown => Driver.WaitForElement(By.XPath("//*[@data-id='header_process_voa_autoprocess.fieldControl-checkbox-select']"));
        private IWebElement DetailsNextStageButton => Driver.WaitForElement(By.XPath("//span[contains(@title,'Auto Process')]/ancestor::*//button[@aria-label='Next Stage']"));
        private IWebElement ResearchingBPFTabElement => Driver.WaitForElement(By.XPath("*//div[contains(@title,'Researching')][1]"));
        private IWebElement NextStageButtonOnBPF => Driver.WaitForElement(By.CssSelector("button[aria-label='Next Stage']"));
        private By DesktopOptionTab => By.CssSelector("[title='Desktop Research']");
        private IWebElement SetActiveDetails => Driver.WaitForElement(By.XPath("//button[contains(@aria-label,'Set Active')]"));
        private IWebElement IsFurtherinformationRequiredDesktopResearch =>
            Driver.WaitForElement(By.XPath("//select[contains(@aria-label,'Is Further Information Required?')]"));
        private By DesktopResearchDateSelector => By.XPath("//input[contains(@aria-label,'Date of Desktop Research date')]");
        private IWebElement DektopResearchScrollDiv => Driver.WaitForElement(By.CssSelector("div[id~='MscrmControls.Containers.ProcessStageControl-processHeaderStageFlyoutItems_bef1655c-f88d-4116-a724-868d8cf7a50f']"));
        private IWebElement RateableValueType => Driver.WaitForElement(By.CssSelector("[id*='ProcessStageControl-processHeaderStageFlyoutInnerContainer'] select[title='Nil']"));
        private IWebElement IsDesktopResearchComplete => Driver.WaitForElement(By.CssSelector("select[aria-label='Is Desktop Research Complete?']"));
        private IWebElement IsInspectionRequired => Driver.WaitForElement(By.CssSelector("select[aria-label='Is an Inspection required?']"));
        private IWebElement PADCodeCorrectElement => Driver.WaitForElement(By.CssSelector("select[aria-label='PAD Validated?']"));
        private IWebElement AffectedAssessmentConfirmedDropdownElement => Driver.WaitForElement(By.CssSelector("select[aria-label='Affected Assessment Confirmed']"));
        private IWebElement CorrespondanceChecksCompleteDropdownElement => Driver.WaitForElement(By.CssSelector("select[aria-label='Correspondence Checks Complete?']"));
        private IWebElement ReasonForInspection => Driver.WaitForElement(By.CssSelector("select[aria-label='Reason for Inspection']"));
        private IWebElement ReasonForInspectionInDetail => Driver.WaitForElement(By.CssSelector("textarea[ aria-label='Reason for Inspection Detail']"));
        private IWebElement VerifyAddressGrid => Driver.WaitForElement(By.CssSelector("section[aria-label='Verify Address']"));
        private IWebElement CloseButtonElement => Driver.WaitForElement(By.CssSelector("button[aria-label='Close']"));
        private IWebElement IstheBAReferenceMissing => Driver.WaitForElement(By.CssSelector("button[aria-label='Is the BA Reference Missing?: No']"));
        private IWebElement BAReferenceMissingComments => Driver.WaitForElement(By.CssSelector("textarea[aria-label='BA Reference Missing Comments']"));
        private IWebElement BAReferenceRequestedFromLocalAuthorityElement => Driver.WaitForElement(By.CssSelector("button[aria-label='BA Reference Requested From Local Authority?: No']"));
        private IWebElement SaveStatusElement => Driver.WaitForElement(By.CssSelector("span[aria-label='Save status - Saved']"));
        private IWebElement TCTRInformationTab => Driver.WaitForElement(By.CssSelector("li[aria-label='TCTR Information']"));
        private IWebElement ReasonForIncompleteTCTRResponse => Driver.WaitForElement(By.CssSelector("input[aria-label='Reason for Incomplete TCTR Response']"));
        private IWebElement ReasonForIncompleteTCTRResponseDropdownElement => Driver.WaitForElement(By.CssSelector("label[title='TCTR Option 1']"));
        private IWebElement AreTCTRResponsesComplete => Driver.WaitForElement(By.CssSelector("select[aria-label='Are TCTR Responses Complete? ']"));
        private IWebElement ReasonForIncompleteTCTRCheckDetail => Driver.WaitForElement(By.CssSelector("li[aria-label='TCTR Details']"));
        private IWebElement IsTheRALDCheckComplete => Driver.WaitForElement(By.CssSelector("button[aria-label='Is the RALD Check Complete?: No']"));
        private IWebElement VerifyAddressGridElement => Driver.WaitForElement(By.CssSelector("h2[title='Verify Address']"));
        private IWebElement SavebtnElement => Driver.WaitForElement(By.CssSelector("button[aria-label='Save (CTRL+S)']"));
        private IWebElement GeneraltabScrollDiv => Driver.WaitForElement(By.CssSelector("[id*=mainContentContainer] .webkitScroll"));
        private By DesktopResearchDetailsSelectionSelector(string jobId) => By.CssSelector($"div li[aria-label='Desktop Research: {jobId}']");
        private List<IWebElement> MaintainAssessmentElementList => Driver.WaitForElements(By.CssSelector("div[title='Maintain Assessment']")).ToList();
        private IWebElement FinishReleaseAndPublishBtnBPFElement => Driver.WaitForElement(By.CssSelector("button[aria-label='Finish']"));
        private IWebElement MaintainAssessmentCompleteDrpBPFElement => Driver.WaitForElement(By.CssSelector("select[aria-label='Maintain Assessment Complete? ']"));
        private IWebElement SystemRequiresQualityAssurance => Driver.WaitForElement(By.CssSelector("label[id*='systemrequiressignoffcode']"));
        private IWebElement CloseButtonOnProcessStage => Driver.WaitForElement(By.XPath("//button[@aria-label='Close'][contains(@id,'CancelContainer')]"));

        private IWebElement LinkedAssessmentConfirmedLinkCurrentElement => Driver.WaitForElement(By.CssSelector("div[title*='Current (live entry)']"));

        private IWebElement LinkedAssessmentConfirmedLinkPreviousElement => Driver.WaitForElement(By.CssSelector("div[title*='Previously']"));
        private By LinkedAssessmentElement => By.CssSelector("section[aria-label='Linked Assessment'] input[aria-label='Linked Assessment, Lookup']");
        private IWebElement CompositeChangeTypeDropdownElement => Driver.WaitForElement(By.CssSelector("button[aria-label='Composite Change Type']"));

        private IWebElement RelatedCompositePropertyElement => Driver.WaitForElement(By.XPath("//label[contains(text(),'Related Composite Property')]"));

        private IWebElement RelatedCompositePropertyValueElement => Driver.WaitForElement(By.CssSelector("[id*=relatedcompositeproperty][type='text']"));

        private IWebElement CompositeIndicatorValueElement => Driver.WaitForElement(By.CssSelector("input[aria-label='Composite Indicator']"));

        private IWebElement ChevronRightMed => Driver.WaitForElement(By.XPath("//*[@data-icon-name='ChevronRightMed']"));

        private IWebElement StatusCircleCheckmark => Driver.WaitForElement(By.XPath("//*[@data-icon-name='StatusCircleCheckmark']"));

        private IWebElement SelectAssessment => Driver.WaitForElement(By.XPath(" //span[text()='Select Assessment']//ancestor::button"));

        private IWebElement LinkedAssessmentConfirmed => Driver.WaitForElement(By.CssSelector("section[aria-label='General'] select[aria-label='Linked Assessment Confirmed?']"));

        private IWebElement MaterialIncreaseIndicator => Driver.WaitForElement(By.XPath("//label[contains(@aria-label,'Has Associated CR10')]"));

        private IWebElement NoLinkedAssessment => Driver.WaitForElement(By.CssSelector("[data-id='voa_subjectofassessmentid-FieldSectionItemContainer'] [title='Select to enter data']"));

        private IWebElement HasRelevantTransactionDropdownElement => Driver.WaitForElement(By.CssSelector("select[aria-label='Has relevant transaction? ']"));

        private IWebElement ValidateForRelease => Driver.WaitForElement(By.CssSelector("*[aria-label='Validate for Release']"));

        private IWebElement QAQCContinue => Driver.WaitForElement(By.CssSelector("button[aria-label='Continue']"));

        private IWebElement HereditamentLink => Driver.WaitForElement(By.CssSelector("[data-id*='voa_statutoryspatialunitid.fieldControl-LookupResultsDropdown_voa_statutoryspatialunitid_selected_tag_text']"));
        private By HereditamentLinkUsingBy => By.CssSelector("[data-id*='voa_statutoryspatialunitid.fieldControl-LookupResultsDropdown_voa_statutoryspatialunitid_selected_tag_text']");

        private IWebElement HereditamentDetailsSection => Driver.WaitForElement(By.CssSelector("section[aria-label='Hereditament Details']"));
        //*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row')]//*[@col-id='voa_statutoryspatialunitid']//a

        private By HereditamentLinkOnAssociatedJob => By.XPath("//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row')]//*[@col-id='voa_statutoryspatialunitid']//a");

        private IWebElement IsDekstopResearchComplete => Driver.WaitForElement(By.CssSelector("section[aria-label='General'] button[aria-label='Is Desktop Research Complete?']"));



    }
}
