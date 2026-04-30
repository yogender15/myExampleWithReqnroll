using POMSeleniumFrameworkPoc1.Helpers;
using System;

namespace POMSeleniumFrameworkPoc1.Pages.UI.RBSP_BP
{
    public partial class RaiseAnAlternativeJob : BasePage
    {

        public void ClickJobActionsDropDownOnMainNav()
        {
            JobActionsDropDownElement.Click();
        }

        public void SelectRaiseAnAlternativeJobDropDownOnMainNav()
        {
            RaiseAnAlternativeJobDropDownOnMainNavElement.Click();

        }

        public void FillAlternativeJobTypeLookUpField(string jobType)
        {
            ScrollDiv.ScrollUntilSelectorDisplayedAndSendKeys(AlternativeJobTypeSelector,
                jobType, 10);
            AlternativeJobTypeFirstElement.Click();
        }

        public void FillAlternateJobCodedReasonLookUpField(string jobType)
        {
            ScrollDiv.ScrollUntilSelectorDisplayedAndSendKeys(AlternativeJobTypeSelector,
                jobType, 10);
            AlternativeJobTypeFirstElement.Click();
        }

        public void ClickAlternateJobCodedReasonSearchField()
        {
            AlternateJobCodedReasonSearchElement.Click();



        }
        public void ClickAlternateJobAdvancedLookUpFieldOption()
        {
            AlternateJobAdvancedLookUpFieldElement.Click();
        }

        public void ClickCodedReasonChevron()
        {
            CodedReasonChevronElement.Click();
        }

        public void SelectActiveCodedReasonFromDrpDownList()
        {
            ActiveCodedReasonFromDrpDownListElement.Click();
        }

        public void SelectTheDeletionCodedReasonInResultList(string codedReason)
        {
            CodedReasonTextFieldOnAdvancedLookUpWindow.SendKeys(codedReason);
            ActiveCodedReasonResultDeletionOption.Click();
        }

        public void ClickDoneButtonOnAdvancedLookUpWindow()
        {
            DoneButtonOnAdvacneLookUpWindow.Click();
            if (IgnoreAndSaveButton.IsElementDisplayed(5))
                IgnoreAndSaveButton.Click();
        }

        public void PickProposedAlternativeJobDate(float days)
        {
            var currentDate_02 = DateTime.Now.AddDays(days).ToString("d, MMMM, yyyy");
            //ScrollDiv.ScrollAndClick(AlternativeJobProposedDatePickerSelector, 20);
            AlternativeJobProposedDatePicker.Click();
            SeleniumExtensions.SetTheDateForTheTableCalendar(currentDate_02, 1);
            //  ScrollDiv.ScrollUntilSelectorDisplayedAndSendKeys(ProposedDatePickerSelector, currentDate, 10);
        }

        public void ClickCloseButtonOnDialog()
        {
            CloseButtonOnAdvacneLookUpWindow.Click();
        }

        public void ClickSaveAndCloseOnDialog()
        {
            SaveAndCloseButton.Click();
            if (IgnoreAndSaveButton.IsElementDisplayed(5))
                IgnoreAndSaveButton.Click();
        }

        public bool VerifyHeaderTitleHasAlternative()
        {
            return AlternativeHeaderTitle.IsElementDisplayed(10);
        }
    }
}
