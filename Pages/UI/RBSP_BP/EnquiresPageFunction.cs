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
        public void ClickEnquiriesTab()
        {
            EnquiriesTabElement.Click();
        }

        public void ClickEnquiriesNewBtn()
        {
            EnquiriesNewBtnElement.Click();
        }

        public bool VerifyNewEnquiryHeadlineTextIsDisplayed(string headLineText)
        {
            return NewEnquiryHeadlineElement.Text.Contains(headLineText);
        }

        public void PickDateReceivedInVOADateEnquiryPage(float days)
        {
            var currentDate = DateTime.Now.AddDays(days).ToString("dd/MM/yyyy");
            ScrollDiv.ScrollUntilSelectorDisplayedAndSendKeysIntoDatePicker(DateReceivedInVOADatePickerSelector, currentDate, 10);
            // ClickDatePickerLabelDateReceivedOnEnquiriesPage();
        }

        public void DateProposalReceivedInVOADateEnquiryPage(float days)
        {
            var currentDate = DateTime.Now.AddDays(days).ToString("dd/MM/yyyy");
            ScrollDiv.ScrollUntilSelectorDisplayedAndSendKeys(DateProposalReceivedInVOADatePickerSelector, currentDate, 10);
        }

        public void FillTaskTypeOnEnquiriesPage(string taskReason)
        {
            TasktypeSearchIcon.ScrollAndClick();
            TasktypeTextBox.Click();
            TasktypeTextBox.SendKeys(Keys.Backspace);
            TasktypeTextBox.SendKeys(taskReason);
            TasktypeValue.Click();
        }

        public void FillCallTypeOnEnquiriesPage(string callReason)
        {
            CallTypeSearchIcon.ScrollAndClick();
            CallTypeTextBox.Click();
            CallTypeTextBox.SendKeys(Keys.Backspace);
            CallTypeTextBox.SendKeys(callReason);
            CallTypeValue.Click();
        }

        public void FillEnquiryTypeOnEnquiriesPage(string EnquiryReason)
        {
            EnquiryTypeSearchIcon.ScrollAndClick();
            EnquiryTypeTextBox.Click();
            EnquiryTypeTextBox.SendKeys(Keys.Backspace);
            EnquiryTypeTextBox.SendKeys(EnquiryReason);
            EnquiryTypeValue.Click();
        }

        public void FillCouncilTaxPayerOnEnquiriesPage(string name)
        {
            EnquiryTypeSearchIcon.ScrollAndClick();
            EnquiryTypeTextBox.Click();
            EnquiryTypeTextBox.SendKeys(Keys.Backspace);
            EnquiryTypeTextBox.SendKeys(name);
            EnquiryTypeValue.Click();
        }

        public void FillEnquirySubTypeOnEnquiriesPage(string subEnquiryReason)
        {
            EnquirySubTypeSearchIcon.ScrollAndClick();
            EnquirySubTypeTextBox.Click();
            EnquirySubTypeTextBox.SendKeys(Keys.Backspace);
            EnquirySubTypeTextBox.SendKeys(subEnquiryReason);
            EnquirySubTypeValue.Click();
        }

        public void SelectHasCustomerUsedWebsiteOrDigitalServiceDropDown(string WebDigitalDrp)
        {
            HasCustomerUsedWebsiteOrDigitalServiceDropDownElement.SelectElementByText(WebDigitalDrp);
        }

        public void FillPartProposerOnEnquiriesPage(string PartProposer)
        {
            PartProposerSearchIcon.ScrollAndClick();
            PartProposerTextBox.Click();
            PartProposerTextBox.SendKeys(Keys.Backspace);
            PartProposerTextBox.SendKeys(PartProposer);
            PartProposerValue.Click();
        }

        public void FillInvalidityDecisionReasonDrpCTProposalsPage(string decision)
        {
            InvalidityDecisionReasonDrpCTProposalsElement.SelectElementByText(decision);
        }

        public void FillDoesTheCustomerWantToRaiseACR15CTProposalsPage(string custWantToRaise)
        {
            DoesTheCustomerWantToRaiseACR15DrpCTProposalsElement.SelectElementByText(custWantToRaise); ;
        }

        public void ClickSaveBtnOnEnquiriesPage()
        {
            SaveBtnElement.Click();
            System.Threading.Thread.Sleep(3000);
        }

        public void ClickCTProposalsTabOnEnquiriesPage()
        {
            CTProposalsTabElement.Click();
        }

        public void ClickProposalTriageOutcomeFailCTProposalsPage()
        {
            ProposalTriageOutcomeFailCTProposalsElement.Click();
        }

        public void ClickProposalValidityDecisionInvalidCTProposalsPage()
        {
            ProposalValidityDecisionInvalidCTProposalsElement.Click();
        }

        public void ClickBeginProposalTriageCTProposalsPage()
        {
            BeginProposalTriageCTProposalsElement.Click();
        }

        public void SelectInvalidityDecisionReasonDrpOnCTProposalsPageDropDown(string WebDigitalDrp)
        {
            HasCustomerUsedWebsiteOrDigitalServiceDropDownElement.SelectElementByText(WebDigitalDrp);
        }


        public void SelectDoesTheCustomerWantToRaiseACR15CTProposalsDropDown(string WebDigitalDrp)
        {
            HasCustomerUsedWebsiteOrDigitalServiceDropDownElement.SelectElementByText(WebDigitalDrp);
        }

        public void ClickProposalTriageOutcomePassCTProposalsPage()
        {
            ProposalTriageOutcomePassCTProposalsElement.Click();
        }

        public void ClickProposalValidityDecisionValidCTProposalsPage()
        {
            ProposalValidityDecisionValidCTProposalsElement.Click();
        }
        public void ClickGenerateJobOptionYesOnEnquiriesPage()
        {
            GenerateJobOptionYesEnquiriesPageElement.ClickUntilNoClickInterruptable();
        }

        public void ClickCCYCTBDataTabEnquiriesPage()
        {
            CCYCTBDataTabElement.Click();
        }

        public void FillPropertyDetails(string property)
        {
            ScrollDiv.ScrollUntilSelectorDisplayedAndSendKeys(PropertyLookUpFieldSelector, property, 10);
            PropertyLookUpResultFirstValue.ClickUntilNoClickInterruptable();
        }

        public string CaptureEnquiryIdCreatedOnEnquiryPage()
        {
            EnquiryId = EnquiryIdTextBoxElement.GetAttribute("title");
            return EnquiryId;
        }

        public bool VerifyEnquiryIdFieldContainsId()
        {
            var enquiryId = EnquiryIdTextBoxElement.GetAttribute("title").Contains(EnquiryId);
            return enquiryId;
        }

        public bool VerifyHeadersUnderCCYCTBDataTabAreValid(string header1, string header2, string header3, string header4)
        {
            return ContactInformationOnCCYCTBDataElement.GetAttribute("title").Contains(header1) && CorrespondenceInformationOnCCYCTBDataElement.GetAttribute("title").Contains(header2)
                  && PayerInformationOnCCYCTBDataElement.GetAttribute("title").Contains(header3) && ProposalSectionOnCCYCTBDataElement.GetAttribute("title").Contains(header4);
        }

        public void FillRequestTypeOnEnquiryCreation(string requestType)
        {
            ScrollDiv.ScrollUntilSelectorDisplayedAndSendKeys(RequestTypeLookupEnquiriesPageField,
                requestType, 10);
            RequestTypeEnquiryPageValueElement.Click();
        }

        public void FillCodedReasonOnJobCreation(string codedReason)
        {
            JobTypeSearchIconEnquiryPageElement.ScrollAndClick();
            CodedReasonTextBoxEnquiriesPageElement.Click();
            CodedReasonTextBoxEnquiriesPageElement.SendKeys(Keys.Backspace);
            CodedReasonTextBoxEnquiriesPageElement.SendKeys(codedReason);
            CodedReasonEnquiriesPageValue.Click();
        }

        public void PickEffectiveDateOnEnquiriesPage(float days)
        {
            var currentDate = DateTime.Now.AddDays(days).ToString("dd/MM/yyyy");
            var currentDate_02 = DateTime.Now.AddDays(days).ToString("d, MMMM, yyyy");
            ScrollDiv.ScrollAndClick(ProposedDatePickerEnquiriesPageSelector, 20);
            SeleniumExtensions.SetTheDateForTheTableCalendar(currentDate_02, 1);
        }

        public string GetEnquiryIdOnEnquiryPage()
        {
            ClickSaveBtnOnMainNav();
            EnquiryIDTextBox.ScrollAndClick();
            System.Threading.Thread.Sleep(4000);
            Driver.WaitUntilElementHasValue(EnquiryIdTextBoxSelector, "---", false, 30000);
            EnquiryId = EnquiryIDTextBox.GetAttribute("title");
            return EnquiryId;
        }

        public void ClickDatePickerLabelDateReceivedOnEnquiriesPage()
        {
            DatePickerLabelDateReceivedOnEnquiriesPageElement.Click();
        }
    }
}
