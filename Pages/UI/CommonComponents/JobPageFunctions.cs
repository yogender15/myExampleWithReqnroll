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

        public void ClickJobsMenu()
        {
            JobsButton.Click();
        }

        public void NewJobLink()
        {
            NewJobLabel.Click();
        }

        public void ClickCasesMenu()
        {
            Cases.Click();
        }

        public void ClickOkOnScriptErrorWindow()
        {
            NewJobLabel.Click();
        }

        public void SelectOrigin(string origin)
        {
            ChannelDropDown.SelectElementByText(origin);
        }

        public void FillRequestTypeOnJobCreation(string requestType)
        {
            ScrollDiv.ScrollUntilSelectorDisplayedAndSendKeys(RequestTypeLookupField,
                requestType, 10);
            RequestTypeValue.Click();
        }

        public void FillCodedReasonOnJobCreation(string codedReason)
        {
            CodedReasonTextBox.Clear();
            CodedReasonTextBox.Click();
            CodedReasonTextBox.SendKeys(Keys.Backspace);
            CodedReasonTextBox.SendKeys(codedReason);
            CodedReasonValue.Click();
        }

        public void PickEffectiveDate(float days)
        {   
            
            var currentDate = DateTime.Now.AddDays(days).ToString("dd/MM/yyyy");
            ScrollDiv.ScrollUntilSelectorDisplayedAndSendKeys(EffectiveDatePickerSelector, currentDate, 10);
        }

        public void FillPartyDetails(string party)
        {
            ScrollDiv.ScrollUntilSelectorDisplayedAndSendKeys(PartyLookUpSelector
                , party, 10);
            PartyLookUpResultFirstValue.Click();
        }

        public void FillHereditamentDetails(string hereditament)
        {
            ScrollDiv.ScrollUntilSelectorDisplayedAndSendKeys(HereditamentLookUpFieldSelector
                , hereditament, 10);
            HereditamentLookUpResultFirstValue.ClickUntilNoClickInterruptable();

        }

        public void FillSubmittedByDetails(string SubmittedBy)
        {
            ScrollDiv.ScrollUntilSelectorDisplayedAndSendKeys(SubmittedBySearchSelector
                , SubmittedBy, 10);
            HereditamentLookUpResultFirstValue.ClickUntilNoClickInterruptable();

        }

        public string SaveJob()
        {
            SaveLabel.Click();
            VerifyDuplicateRecords();
            JobIDTextBox.ScrollAndClick();
            Driver.WaitUntilElementHasValue(JobIDTextBoxSelector,"---",false, 30000);
            JobId = JobIDTextBox.GetAttribute("title");
            return JobId;
        }

        public bool IsJobCreatedSearchable(string  job_Id)
        {
            ClickJobsMenu();
            JobSearchTextBox.Click();
            JobSearchTextBox.SendKeys(job_Id);
            JobSearchButton.Click();
            Driver.WaitUntilElementWithSelectorIsDisplayed(JobIdSelector(job_Id), 20000);
            return JobIdSelector(job_Id).GetElementWithSelector().Displayed;
        }

        public void SelectJobCreated(string job_Id)
        {
            ClickJobsMenu();
            JobSearchTextBox.Click();
            JobSearchTextBox.SendKeys(job_Id);
            JobSearchButton.Click();
        }

   
        public void OpenTheCreatedJobFromSearchResult(string job_Id)
        {
            JobIdSelector(job_Id).GetElementWithSelector().DoubleClickElement();
            VerifyDuplicateRecords();
        }

        public void SetTCTRDetails(string rateplyare)
        {
            TCTRDetailsTitle.Click();
            RatePayerTextBox.Click();
            RatePayerTextBox.Clear();
            RatePayerTextBox.SendKeys(rateplyare);
            RatePayerLookupResultValue.Click();
        }

        public void EnterDetailsTabInfo(string option)
        {
            DetailsOptionTab.ClickUntilNoClickInterruptable();
            AutoProcessDropDown.SelectElementByText(option);
            Driver.WaitForElementToDissapear(CopyDesktopOptionTab,500);
            DetailsNextStageButton.Click();
        }

        private void VerifyDuplicateRecords()
        {
            if (IgnoreAndSaveButton.IsElementDisplayed(5))
            {
                IgnoreAndSaveButton.Click();
            }
        }

  
        public bool EnterReleaseAndPublishDetails(string publishOption)
        {
            PublishListUpdatesDropDown.SelectElementByText(publishOption);
            if (OKButton.IsElementDisplayed(1))
                OKButton.ClickUntilNoClickInterruptable();
            FinishButton.Click();
            VerifyDuplicateRecords();
            return FinishedButton.Displayed;
        }


        private void ClickTctrTab()
        {
            ClickTctrTab();
        }

    }
}
