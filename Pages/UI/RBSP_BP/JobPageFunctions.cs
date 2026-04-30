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

        public void ClickPlusNewJobBtn()
        {
            NewJobLabel.ScrollAndClick();
        }

        public void ClickDetailsTabUnderJobPage()
        {
            System.Threading.Thread.Sleep(7000);
            DetailsTabUnderJobPageElement.Click();
        }
        public void ClickCasesMenu()
        {
            JobsMenuUnderServices.ScrollAndClick();
        }
        public void ClickOkOnScriptErrorWindow()
        {
            NewJobLabel.ScrollAndClick();
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
            CodedReasonSearchIcon.ScrollAndClick();
            CodedReasonTextBox.Click();
            CodedReasonTextBox.SendKeys(Keys.Backspace);
            CodedReasonTextBox.SendKeys(codedReason);
            CodedReasonValue.Click();
        }

        public void ClickCodedReason(string codedReason)
        {
            CodedReasonSearchIcon.ScrollAndClick();
            CodedReasonTextBox.Click();
            CodedReasonTextBox.SendKeys(Keys.Backspace);
            CodedReasonTextBox.SendKeys(codedReason);
            CodedReasonTextBox.Click();
            System.Threading.Thread.Sleep(2000);
        }

        public void PickProposedDate(float days)
        {
            var currentDate = DateTime.Now.AddDays(days).ToString("dd/MM/yyyy");
            var currentDate_02 = DateTime.Now.AddDays(days).ToString("d, MMMM, yyyy");
            ScrollDiv.ScrollAndClick(ProposedDatePickerSelector, 20);
            SeleniumExtensions.SetTheDateForTheTableCalendar(currentDate_02, 1);
            //  ScrollDiv.ScrollUntilSelectorDisplayedAndSendKeys(ProposedDatePickerSelector, currentDate, 10);
        }

        public void PickTargetDate(float days)
        {
            var currentDate = DateTime.Now.AddDays(days).ToString("dd/MM/yyyy");
            var currentDate_02 = DateTime.Now.AddDays(days).ToString("d, MMMM, yyyy");
            ScrollDiv.ScrollAndClick(TargetDatePickerSelector, 20);
            SeleniumExtensions.SetTheDateForTheTableCalendar(currentDate_02, 1);
            //  ScrollDiv.ScrollUntilSelectorDisplayedAndSendKeys(ProposedDatePickerSelector, currentDate, 10);
        }

        public void FillPartyDetails(string party)
        {
            ScrollDiv.ScrollUntilSelectorDisplayedAndSendKeys(PartyLookUpSelector, party, 10);
            PartyLookUpResultFirstValue.Click();
        }

        public void FillHereditamentDetails(string hereditament)
        {
            System.Threading.Thread.Sleep(3000);
            ScrollDiv.ScrollUntilSelectorDisplayedAndSendKeys(HereditamentLookUpFieldSelector, hereditament, 10);
            HereditamentLookUpResultFirstValue.ClickUntilNoClickInterruptable();
        }

        public void FillProposedAddress(string proposedAddress)
        {
            ScrollDiv.ScrollUntilSelectorDisplayedAndSendKeys(ProposedAddressSelector, proposedAddress, 10);
            ProposedAddressElement.ClickUntilNoClickInterruptable();
        }

        public void FillSubmittedByDetails(string SubmittedBy)
        {
            FillAndSelectLookUpResult(SubmittedbyLookupInput
                , SubmittedBy);
        }

        public void SelectCCACTypeCategorisation(string category)
        {
            SeleniumExtensions.SelectElementByText(CCACTypeCategorisationElement, category);
        }

        public void FillCCACTypeCategorisation(string reason)
        {
            ScrollDiv.ScrollUntilSelectorDisplayedAndSendKeys(ReasonGroundCategorisationSearchSelector, reason, 30);
            ReasonGroundCategorisationElement.ClickUntilNoClickInterruptable();
        }

        public void FillRelationshipRole(string RelationshipRole)
        {
            FillAndSelectLookUpResult(RelationshipRoleSearchSelector, RelationshipRole);
        }

        public string SaveJob()
        {
            SaveLabel.Click();
            VerifyDuplicateRecords();
            JobIDTextBox.ScrollAndClick();
            Driver.WaitUntilElementHasValue(JobIDTextBoxSelector, "---", false, 30000);
            JobId = JobIDTextBox.GetAttribute("title");
            return JobId;
        }

        public void SimpleSave()
        {
            SaveLabel.Click();
            VerifyDuplicateRecords();
        }

        public bool IsJobCreatedSearchable(string job_Id)
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
            Driver.WaitForElementToDissapear(CopyDesktopOptionTab, 500);
            DetailsNextStageButton.Click();
        }

        public void SelectDetailsTab()
        {
            System.Threading.Thread.Sleep(8000);
            DetailsOptionTab.ClickUsingJavascript();
        }

        public void VerifySaveAndContinue()
        {
            try
            {
                // System.Threading.Thread.Sleep(2000);
                SaveAndContinueBtnElement.Click();
            }
            catch
            {
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

        public void ClickActionsTab()
        {
            SavingLoaderLabel.IsElementDisplayed(4000);
            StatusSavedLabel.IsElementDisplayed(15);
            ActionsTabElement.ClickUntilNoClickInterruptableWithSelector(ActionsTabElementSelector, 40000);
        }

        public void ClickCreateATimelineRecordBtn()
        {
            System.Threading.Thread.Sleep(2000);
            CreateATimelineRecordBtnElement.Click();
        }

        public bool IsListOfJobsDisplayed()
        {
            return ListOfJobsElement.Displayed;
        }

        public void ClickAdvancedLookupOnCodedReasonlink()
        {
            AdvancedLookupOnCodedReasonlinkElement.Click();
        }

        public void ClickCodedReasonLookupViewOnAdvancedLookUpInJobpge()
        {
            CodedReasonLookupViewJobPageElement.Click();
        }
        public void SelectActiveCodedReasonDropDownOnAdvancedLookUpInJobPage()
        {
            ActiveCodedReasonDropDownJobPageElement.Click();
        }

        public void SelectReconstitutionMergeCTOptionAdvancedLookUp()
        {
            ActiveCodedReasonBtnElement.Click();
            CloseBtnAdvancedLookUpCodedReasonElement.Click();
        }

        public void ClickDoneBtnInAdvancedLookUpCodedReasonJobPage()
        {
            DoneBtnAdvancecLookUpJobPageElement.Click();
        }

        public void ClickBulkCreationOfJobsTabOnJobPage()
        {
            BulkCreationOfJobsTabOnJobPageElement.ScrollAndClick();
        }

        public void ClickAddExistingHereditamentLinkUnderBulkCreationJobs()
        {
            AddExistingHereditamentLinkElement.ScrollAndClick();
        }

        public void ClickAddBtnUnderBulkCreationOfJobsTab()
        {
            AddBtnUnderBulkCreationOfJobsTabElement.ScrollAndClick();
        }

        public void ClickContinueBtnUnderBulkCreationOfJobsTab()
        {
            ContinueBtnUnderBulkCreationOfJobsTabElement.ScrollAndClick();
            VerifyDuplicateRecords();
        }

        public void ClickOverviewTabOnJobsPage()
        {
            OverviewTabOnJobsPageElement.ScrollAndClick();
        }

        public bool VerifyMapViewUnderOverviewTabOnJobsPage()
        {
            System.Threading.Thread.Sleep(4000);
            return MapViewUnderOverviewTabElement.Displayed;
        }

        public void ClickEnquiriesTabOnJobsPage()
        {
            EnquiriesTabElement.Click();
        }

        public void ClickNewEnquiriesBtnUnderEnquiriesTab()
        {
            NewEnquiriesBtnElement.Click();
        }
    }
}
