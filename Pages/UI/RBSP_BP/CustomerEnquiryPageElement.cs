using OpenQA.Selenium;
using POMSeleniumFrameworkPoc1.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POMSeleniumFrameworkPoc1.Pages.UI.RBSP_BP
{
    public partial class CustomerEnquiryPage : BasePage
    {
        // Validation Section

        public By ProposedEffectiveDateCE => By.CssSelector("section[aria-label='Validation'] input[aria-label='Date of Proposed Effective Date']");
        private IWebElement DocumnetType => Driver.WaitForElement(By.CssSelector("section[aria-label='Validation'] select[aria-label='Document Type']"));
        private IWebElement AuthorityToActRequired => Driver.WaitForElement(By.CssSelector("section[aria-label='Validation'] button[aria-label='Authority to Act Required']"));
        private IWebElement AuthorityToActReceived => Driver.WaitForElement(By.CssSelector("section[aria-label='Validation'] button[aria-label='Authority to Act Received']"));

        //Caller Information
        public By PartyProposer => By.CssSelector("section[aria-label='Caller Information'] input[aria-label='Party / Proposer, Lookup']");

        public By PartyProposerSearchBtn => By.CssSelector("section[aria-label='Caller Information'] input[aria-label='Party / Proposer, Lookup']+button[title='Search']");

        public By PartyRelationshipRole => By.CssSelector("section[aria-label='Caller Information'] input[aria-label='Party Relationship Role, Lookup']");

        public By PartyRelationshipRoleSearchBtn => By.CssSelector("section[aria-label='Caller Information'] input[aria-label='Party Relationship Role, Lookup']+button[title='Search']");

        //   Enquiry Information
        public By DateReceivedInVOA => By.CssSelector("section[aria-label='Enquiry Information'] input[aria-label='Date of Date Received in VOA']");

        public By EvidenceSufficientDate => By.CssSelector("section[aria-label='Enquiry Information'] input[aria-label='Date of Evidence Sufficient Date']");

        public By TaskType => By.CssSelector("section[aria-label='Enquiry Information'] input[aria-label='Task Type, Lookup']");

        public By TaskTypeSearchBtn => By.CssSelector("section[aria-label='Enquiry Information'] input[aria-label='Task Type, Lookup']+button[title='Search']");

        public By ContactType => By.CssSelector("section[aria-label='Enquiry Information'] input[aria-label='Contact Type, Lookup']");

        public By ContactTypeSearchBtn => By.CssSelector("section[aria-label='Enquiry Information'] input[aria-label='Contact Type, Lookup']+button[title='Search']");

        public By CallType => By.CssSelector("section[aria-label='Enquiry Information'] input[aria-label='Call Type, Lookup']");

        public By CallTypeSearchBtn => By.CssSelector("section[aria-label='Enquiry Information'] input[aria-label='Call Type, Lookup']+button[title='Search']");

        public By CustEnquiryType => By.CssSelector("section[aria-label='Enquiry Information'] input[aria-label='Cust. Enquiry Type, Lookup']");

        public By CustEnquiryTypeSearchBtn => By.CssSelector("section[aria-label='Enquiry Information'] input[aria-label='Cust. Enquiry Type, Lookup']+button[title='Search']");

        public By CustEnquirySubType => By.CssSelector("section[aria-label='Enquiry Information'] input[aria-label='Cust. Enquiry Sub Type, Lookup']");

        public By CustEnquirySubTypeSearchBtn => By.CssSelector("section[aria-label='Enquiry Information'] input[aria-label='Cust. Enquiry Sub Type, Lookup']+button[title='Search']");

        public By CouncilTaxPayerlookup => By.CssSelector("section[aria-label='Enquiry Information'] input[aria-label='Council Tax Payer, Lookup']");

        public By CouncilTaxPayerSearchBtn => By.CssSelector("section[aria-label='Enquiry Information'] input[aria-label='Council Tax Payer, Lookup']+button[title='Search']");

        // Customer Enquiry Information
        public By CustomerCRM => By.CssSelector("section[aria-label='Customer Information'] input[aria-label='Customer, Lookup']");

        public By CustomerCRMSearchBtn => By.CssSelector("section[aria-label='Customer Information'] input[aria-label='Customer, Lookup']+button[title='Search']");
        public By CustomerReltnshpRole => By.CssSelector("section[aria-label='Customer Information'] input[aria-label='Customer Relationship Role, Lookup']");

        public By CustomerReltnshpRoleSearchBtn => By.CssSelector("section[aria-label='Customer Information'] input[aria-label='Customer Relationship Role, Lookup']+button[title='Search']");

        // SetStateDialog
        private IWebElement SetStatusReasonForEnquiry => Driver.WaitForElement(By.CssSelector("[data-id='SetStateDialog'] button[aria-label='Status Reason']"));

        private IWebElement AuthorizationDecision => Driver.WaitForElement(By.CssSelector("button[aria-label='Authorisation to Act Decision']"));
        public By DSRLookUpField => By.CssSelector("section[aria-label='Details'] input[aria-label='Data Source Role, Lookup']");
        private IWebElement ReasonForValidation => Driver.WaitForElement(By.CssSelector("textarea[aria-label='Reason For Validation']"));
        private By EvidenceDatePickerSelector => By.CssSelector("input[aria-label='Date of Evidence Sufficient Date']");

        private IWebElement SetStateButton(String buttonName)
        {
            string customizeSelector = $"[data-id='SetStateDialog'] button[title='{buttonName}']";
            return Driver.WaitForElement(By.CssSelector(customizeSelector));
        }

        private IWebElement ErrorButton => Driver.WaitForElement(By.CssSelector("button[title='OK']"));

        private IWebElement RefreshBtnUnderSupplementaryReqTab => Driver.WaitForElement(By.XPath("//ul[@aria-label='Supplementary Job Requisition Commands']//li//button[@aria-label='Refresh']"));
        private IWebElement SupplementryJobStatusCode => Driver.WaitForElement(By.XPath("//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row')]//*[@col-id='statuscode']//label"));
        private IWebElement SupplementryRquestLink => Driver.WaitForElement(By.XPath("//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row')]//*[@col-id='voa_supplementaryjobid']//a"));

    }
}
