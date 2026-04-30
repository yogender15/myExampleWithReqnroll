using POMSeleniumFrameworkPoc1.Helpers;
using System;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace POMSeleniumFrameworkPoc1.Pages
{
    public partial class JobPage : BasePage
    {
        private IWebElement JobsButton => Driver.WaitForElement(By.XPath("//li[@aria-label=\"Jobs\"]"));
        private IWebElement NewJobLabel => Driver.WaitForElement(By.CssSelector("button[aria-label='New Job']"));
        private IWebElement DetailsTabUnderJobPageElement => Driver.WaitForElement(By.CssSelector("li[aria-label='Details']"));
        private IWebElement ChannelDropDown => Driver.WaitForElement(By.CssSelector("select[aria-label='Channel']"));
        private IWebElement OKButton => Driver.WaitForElement(By.XPath("//*[@aria-label='OK']"));
        private IWebElement RequestTypeValue => Driver.WaitForElement(By.XPath("//*[contains(@data-id,'voa_requesttype.fieldControl-voa_name0_0_0')]"));
        private IWebElement CodedReasonTextBox => Driver.WaitForElement(By.CssSelector("input[aria-label='Coded Reason, Lookup']"));
        private IWebElement CodedReasonValue => Driver.WaitForElement(By.XPath("//*[@data-id='voa_codedreason.fieldControl-voa_name0_0_0']"));
        private IWebElement CodedReasonPartDemolitionValue => Driver.WaitForElement(By.CssSelector("li[aria-label*='Deletion - Ceased to be a Dwelling/Derelict']"));
        private IWebElement CodedReasonSearchIcon => Driver.WaitForElement(By.CssSelector("button[aria-label='Search records for Coded Reason, Lookup field']"));
        private IWebElement CodedReasonSelectedTagDelete => Driver.WaitForElement(By.CssSelector("button[data-id*='voa_codedreason_selected_tag_delete']"));
        private By ProposedDatePickerSelector => By.CssSelector("input[aria-label='Date of Proposed Effective Date']");
        private By TargetDatePickerSelector => By.CssSelector("input[aria-label='Date of Target Date']");
        private By PartyLookUpSelector => By.XPath("//*[@data-id='customerid.fieldControl-LookupResultsDropdown_customerid_textInputBox_with_filter_new']");
        private IWebElement PartyLookUpResultFirstValue => Driver.WaitForElement(By.XPath("//*[@data-id='customerid.fieldControl-fullname0_0_0']"));
        private IWebElement HereditamentLookUpResultFirstValue => Driver.WaitForElement(By.CssSelector("ul[aria-label*='Lookup results'][id*='voa_statutoryspatialunit'] li"));
        private By HereditamentLookUpFieldSelector => By.CssSelector("input[aria-label='Hereditament, Lookup']");
        // private IWebElement SaveLabel => Driver.WaitForElement(By.XPath("//*[text()='Save']"));
        private IWebElement JobIDTextBox => Driver.WaitForElement(By.XPath("//input[@aria-label='Job ID']"));
        private By JobIDTextBoxSelector => By.XPath("//input[@aria-label='Job ID']");
        private IWebElement JobSearchTextBox => Driver.WaitForElement(By.XPath("//*[@aria-label='Job Search this view']"));
        private IWebElement JobSearchButton => Driver.WaitForElement(By.XPath("//*[@aria-label='Start search']"));
        private IWebElement TCTRDetailsTitle => Driver.WaitForElement(By.XPath("//*[@title='TCTR Details']"));
        private IWebElement RatePayerTextBox => Driver.WaitForElements(By.CssSelector("input[data-id*='LookupResultsDropdown_voa_accountid_textInputBox_with_filter_new']"))[1];
        private IWebElement RatePayerLookupResultValue => Driver.WaitForElement(By.CssSelector("ul[aria-label='Lookup results'] li"));
        private IWebElement DetailsOptionTab => Driver.WaitForElement(By.XPath("*//button[contains(@aria-label,'Details') and contains(@data-id,'MscrmControls.Containers.ProcessBreadCrumb-processHeaderStageButton')]"));
        private IWebElement AutoProcessDropDown => Driver.WaitForElement(By.XPath("//*[@data-id='header_process_voa_autoprocess.fieldControl-checkbox-select']"));
        private IWebElement DetailsNextStageButton => Driver.WaitForElement(By.XPath("//span[contains(@title,'Auto Process')]/ancestor::*//button[@aria-label='Next Stage']"));
        private IWebElement SaveAndContinueBtnElement => Driver.WaitForElement(By.CssSelector("button[aria-label='Save and continue']"));
        private By CopyDesktopOptionTab => By.CssSelector("[title='COPY Desktop Research']");
        private IWebElement FinishButton => Driver.WaitForElement(By.XPath("//*[@data-id='MscrmControls.Containers.ProcessStageControl-finishButtonContainer']"));
        private IWebElement FinishedButton => Driver.WaitForElement(By.XPath("//*[@data-id='MscrmControls.Containers.ProcessStageControl-finishedLabelContainer'][@title='Finished']"));
        private IWebElement PublishListUpdatesDropDown => Driver.WaitForElement(By.XPath("//*[@data-id='header_process_voa_publishlistupdates.fieldControl-checkbox-select']"));
        // private IWebElement ScrollDiv => Driver.WaitForElement(By.CssSelector("#tab-section2"));
        // private By SubmittedbyLookupInput => By.CssSelector("[id*='voa_primarycustomerid'] input[aria-label='Look for records']");

        private By SubmittedbyLookupInput => By.CssSelector("input[aria-label='Submitted By, Lookup']");

        //private By SubmittedbyLookupInput => By.XPath("//input[contains(@aria-label,'Submitted By')]");
        private By RelationshipRole => By.CssSelector("input[aria-label='Relationship Role, Lookup']");
        private IWebElement SubmittedByLookUpResultItem => Driver.WaitForElement(By.XPath("//*[contains(@data-id,\"voa_primarycustomerid.fieldControl\")]//ul[contains(@id,'voa_primarycustomerid.fieldControl-LookupResultsDropdown')]/li"));
        private By SubmittedBySearchSelector => By.CssSelector("button[aria-label='Search records for Submitted By, Lookup field']");
        private IWebElement SubmittedByLookUpFirstValue => Driver.WaitForElement(By.CssSelector("ul[aria-label='Lookup results'] li"));
        // private IWebElement SubmittedByLookUpFirstValue => Driver.WaitForElement(By.CssSelector("li[data-id*='customerid.fieldControl-LookupResultsDropdown_customerid_resultsContainer']"));
        private IWebElement JobsMenuUnderServices => Driver.WaitForElement(By.CssSelector("li[aria-label='Jobs']"));
        private By JobIdSelector(string jobId) => By.XPath($"//*[text()='{jobId}']");
        private IWebElement CCACTypeCategorisationElement => Driver.WaitForElement(By.CssSelector("select[aria-label='CCA/CT Type']"));
        private By ReasonGroundCategorisationSearchSelector => By.CssSelector("input[aria-label='Reason/Ground, Lookup']");
        private IWebElement ReasonGroundCategorisationElement => Driver.WaitForElement(By.CssSelector("div ul li[aria-label='01, Confirm the property details are correct or report a change to them']"));
        //  private By RelationshipRoleSearchSelector => By.CssSelector("input[aria-label='Look for Relationship Role']");
        private By RelationshipRoleSearchSelector => By.CssSelector("input[aria-label='Relationship Role, Lookup']");
        private IWebElement RelationshipRoleElement => Driver.WaitForElement(By.CssSelector("ul[id*='LookupResultsDropdown_voa_relationshiproleid'] li"));
        private IWebElement ProposedAddressElement => Driver.WaitForElement(By.CssSelector("div ul li[aria-label='WEST CROYDE, CROYDE, BRAUNTON, DEVON, EX33 1QA']"));
        private By ProposedAddressSelector => By.CssSelector("input[aria-label='Proposed Address, Lookup']");
        private IWebElement ActionsTabElement => Driver.WaitForElement(By.CssSelector("li[aria-label='Actions']"));
        private By ActionsTabElementSelector => By.CssSelector("li[aria-label='Actions']");
        private IWebElement CreateATimelineRecordBtnElement => Driver.WaitForElement(By.CssSelector("button[aria-label='Create a timeline record.']"));
        private IWebElement ListOfJobsElement => Driver.WaitForElement(By.CssSelector("div[aria-label='Press SPACE to select this row.']"));
        private IWebElement AdvancedLookupOnCodedReasonlinkElement => Driver.WaitForElement(By.CssSelector("button[aria-label='Advanced lookup']"));
        private IWebElement CodedReasonLookupViewJobPageElement => Driver.WaitForElement(By.CssSelector("div[aria-label='Coded Reason Lookup View'][id*='Dropdown'].ms-Dropdown"));
        private IWebElement ActiveCodedReasonDropDownJobPageElement => Driver.WaitForElement(By.XPath("//span[contains(@class,'ms-Dropdown-optionText') and contains(text(),'Active Coded Reasons')]"));
        private IWebElement ActiveCodedReasonBtnElement => Driver.WaitForElement(By.CssSelector("button[aria-label='Reconstitution - Merge (CT)']"));
        private IWebElement CloseBtnAdvancedLookUpCodedReasonElement => Driver.WaitForElement(By.CssSelector("button[aria-label='Close']"));
        private IWebElement DoneBtnAdvancecLookUpJobPageElement => Driver.WaitForElement(By.CssSelector("button[title='Done']"));
        private IWebElement BulkCreationOfJobsTabOnJobPageElement => Driver.WaitForElement(By.CssSelector("li[aria-label='Bulk Creation of Jobs']"));
        private IWebElement AddExistingHereditamentLinkElement => Driver.WaitForElement(By.CssSelector("button[aria-label='Add Existing Hereditament']"));
        private IWebElement AddBtnUnderBulkCreationOfJobsTabElement => Driver.WaitForElement(By.CssSelector("button[aria-label='Add']"));
        private IWebElement ContinueBtnUnderBulkCreationOfJobsTabElement => Driver.WaitForElement(By.CssSelector("button[aria-label='Continue']"));
        private IWebElement OverviewTabOnJobsPageElement => Driver.WaitForElement(By.CssSelector("li[aria-label='Overview']"));
        private IWebElement MapViewUnderOverviewTabElement => Driver.WaitForElement(By.CssSelector("section[aria-label='Map View']"));
        private IWebElement EnquiriesTabElement => Driver.WaitForElement(By.XPath("//ul[contains(@aria-label,'Case Form')]//li[contains(@aria-label,'Enquiries')]"));
        private IWebElement NewEnquiriesBtnElement => Driver.WaitForElement(By.CssSelector("button[aria-label='New Enquiry. Add New Enquiry']"));

        public string JobId;
    }
}
