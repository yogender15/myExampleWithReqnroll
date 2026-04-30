using OpenQA.Selenium;
using POMSeleniumFrameworkPoc1.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POMSeleniumFrameworkPoc1.Pages.UI.RBSP_BP
{
    public partial class EnquiresPage : BasePage
    {
        public IWebElement EnquiriesTabElement => Driver.WaitForElement(By.CssSelector("li[aria-label='Customer Enquiries']"));
        public IWebElement EnquiriesNewBtnElement => Driver.WaitForElement(By.CssSelector("button[aria-label='New']"));
        public IWebElement NewEnquiryHeadlineElement => Driver.WaitForElement(By.CssSelector("div h1[title='New Enquiry']"));
        private By DateReceivedInVOADatePickerSelector => By.XPath("//input[@data-id='voa_datereceived.fieldControl-date-time-input']");
        private IWebElement DatePickerLabelDateReceivedOnEnquiriesPageElement => Driver.WaitForElement(By.CssSelector("input[data-id='voa_datereceived.fieldControl-date-time-input']"));
        private By DateProposalReceivedInVOADatePickerSelector => By.CssSelector("input[aria-label='Date of Date Proposal Received']");
        private IWebElement TasktypeSearchIcon => Driver.WaitForElement(By.CssSelector("button[aria-label='Search records for Task Type, Lookup field']"));
        private IWebElement TasktypeTextBox => Driver.WaitForElement(By.CssSelector("input[aria-label='Task Type, Lookup']"));
        private IWebElement TasktypeValue => Driver.WaitForElement(By.XPath("//*[contains(@data-id,'voa_tasktypeid.fieldControl-voa_name0_0_0')]"));
        private IWebElement CallTypeSearchIcon => Driver.WaitForElement(By.CssSelector("button[aria-label='Search records for Call Type, Lookup field']"));
        private IWebElement CallTypeTextBox => Driver.WaitForElement(By.CssSelector("input[aria-label='Call Type, Lookup']"));
        private IWebElement CallTypeValue => Driver.WaitForElement(By.CssSelector("ul li[aria-label='CT']"));
        private IWebElement EnquiryTypeSearchIcon => Driver.WaitForElement(By.CssSelector("button[aria-label='Search records for Enquiry Type, Lookup field']"));
        private IWebElement EnquiryTypeTextBox => Driver.WaitForElement(By.CssSelector("input[aria-label='Enquiry Type, Lookup']"));
        private IWebElement EnquiryTypeValue => Driver.WaitForElement(By.XPath("//*[@data-id='voa_enquirytypeid.fieldControl-voa_name0_0_0']"));
        private IWebElement EnquirySubTypeSearchIcon => Driver.WaitForElement(By.CssSelector("button[aria-label='Search records for Enquiry Sub Type, Lookup field']"));
        private IWebElement EnquirySubTypeTextBox => Driver.WaitForElement(By.CssSelector("input[aria-label='Enquiry Sub Type, Lookup']"));
        private IWebElement EnquirySubTypeValue => Driver.WaitForElement(By.XPath("//*[@data-id='voa_enquirysubtypeid.fieldControl-voa_name0_0_0']"));
        private IWebElement HasCustomerUsedWebsiteOrDigitalServiceDrpElement => Driver.WaitForElement(By.XPath("//*[@data-id='voa_enquirysubtypeid.fieldControl-voa_name0_0_0']"));

        private IWebElement CouncilTaxPayerSearchIcon => Driver.WaitForElement(By.CssSelector("button[aria-label='Search records for Council Tax Payer, Lookup field']"));
        private IWebElement CouncilTaxPayerTextBox => Driver.WaitForElement(By.CssSelector("input[aria-label='Council Tax Payer, Lookup']"));
        private IWebElement CouncilTaxPayerValue => Driver.WaitForElement(By.XPath("//*[@data-id='voa_ctpayerid.fieldControl-fullname0_0_2']"));

        private IWebElement HasCustomerUsedWebsiteOrDigitalServiceDropDownElement => Driver.WaitForElement(By.XPath("//*[@aria-label='Has Customer Used Website Or Digital Service?']"));
        private IWebElement PartProposerSearchIcon => Driver.WaitForElement(By.CssSelector("button[aria-label='Search records for Party / Proposer, Lookup field']"));
        private IWebElement PartProposerTextBox => Driver.WaitForElement(By.CssSelector("input[aria-label='Party / Proposer, Lookup']"));
        private IWebElement PartProposerValue => Driver.WaitForElement(By.XPath("//*[@data-id='voa_customerparty.fieldControl-LookupResultsDropdown_voa_customerparty_resultsContainer'][1]"));
        private IWebElement SaveBtnElement => Driver.WaitForElement(By.CssSelector("button[aria-label='Save (CTRL+S)']"));
        private IWebElement CTProposalsTabElement => Driver.WaitForElement(By.CssSelector("li[aria-label='CT Proposals']"));
        private IWebElement BeginProposalTriageCTProposalsElement => Driver.WaitForElement(By.CssSelector("button[aria-label='Begin Proposal Triage: No']"));
        private IWebElement ProposalTriageOutcomeFailCTProposalsElement => Driver.WaitForElement(By.CssSelector("div[aria-label='Fail']"));
        private IWebElement ProposalValidityDecisionInvalidCTProposalsElement => Driver.WaitForElement(By.CssSelector("div[aria-label='Invalid']"));
        private IWebElement ProposalTriageOutcomePassCTProposalsElement => Driver.WaitForElement(By.CssSelector("div[aria-label='Fail']"));
        private IWebElement ProposalValidityDecisionValidCTProposalsElement => Driver.WaitForElement(By.CssSelector("div[aria-label='Invalid']"));
        private IWebElement InvalidityDecisionReasonDrpCTProposalsElement => Driver.WaitForElement(By.CssSelector("select[aria-label='Invalidity Decision Reason']"));
        private IWebElement DoesTheCustomerWantToRaiseACR15DrpCTProposalsElement => Driver.WaitForElement(By.CssSelector("select[aria-label='Does the Customer want to raise a CR15?']"));
        private IWebElement GenerateJobOptionYesEnquiriesPageElement => Driver.WaitForElement(By.CssSelector("button[aria-label='Generate Job: No']"));
        private By PropertyLookUpFieldSelector => By.CssSelector("input[aria-label='Property, Lookup']");
        private IWebElement PropertyLookUpResultFirstValue => Driver.WaitForElement(By.XPath("//*[@data-id='voa_propertyid.fieldControl-LookupResultsDropdown_voa_propertyid_resultsContainer'][1]"));
        private IWebElement EnquiryIdTextBoxElement => Driver.WaitForElement(By.XPath("//input[@aria-label='Enquiry ID']"));
        private IWebElement CCYCTBDataTabElement => Driver.WaitForElement(By.CssSelector("li[aria-label='CCYCTB Data']"));
        private IWebElement ContactInformationOnCCYCTBDataElement => Driver.WaitForElement(By.CssSelector("h2[title='Contact information']"));
        private IWebElement CorrespondenceInformationOnCCYCTBDataElement => Driver.WaitForElement(By.CssSelector("h2[title='Correspondence Information']"));
        private IWebElement PayerInformationOnCCYCTBDataElement => Driver.WaitForElement(By.CssSelector("h2[title='CT Payer information']"));
        private IWebElement ProposalSectionOnCCYCTBDataElement => Driver.WaitForElement(By.CssSelector("h2[title='Proposal Section']"));
        //private IWebElement RequestTypeEnquiryPageValueElement => Driver.WaitForElement(By.XPath("//*[@data-id='voa_requesttypeid.fieldControl-LookupResultsDropdown_voa_requesttypeid_resultsContainer'][1]')]"));
        // private IWebElement RequestTypeEnquiryPageValueElement => Driver.WaitForElement(By.XPath("//*[@data-id='voa_requesttypeid.fieldControl - LookupResultsDropdown_voa_requesttypeid_SelectedRecordList']"));
        private IWebElement RequestTypeEnquiryPageValueElement => Driver.WaitForElement(By.XPath("//libutton[aria-label='Search records for Job Type, Lookup field']"));
        public By RequestTypeLookupEnquiriesPageField => By.CssSelector("input[aria-label='Request Type, Lookup']");

        private IWebElement JobTypeSearchIconEnquiryPageElement => Driver.WaitForElement(By.CssSelector("button[aria-label='Search records for Job Type, Lookup field']"));
        private IWebElement CodedReasonTextBoxEnquiriesPageElement => Driver.WaitForElement(By.CssSelector("input[aria-label='Job Type, Lookup']"));
        private IWebElement CodedReasonEnquiriesPageValue => Driver.WaitForElement(By.XPath("//li[@data-id='voa_codedreasonid.fieldControl-LookupResultsDropdown_voa_codedreasonid_resultsContainer'][1]"));
        private By ProposedDatePickerEnquiriesPageSelector => By.CssSelector("input[aria-label='Date of Effective Date']");

        public string EnquiryId;
        private IWebElement EnquiryIDTextBox => Driver.WaitForElement(By.XPath("//input[@aria-label='Enquiry ID']"));
        private By EnquiryIdTextBoxSelector => By.XPath("//input[@aria-label='Enquiry ID']");

    }
}
