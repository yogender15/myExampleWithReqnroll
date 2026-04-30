using POMSeleniumFrameworkPoc1.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POMSeleniumFrameworkPoc1.Pages.UI.CommonComponents
{
    public partial class AskForHelpPage : BasePage
    {
        public void ClickAskForHelpIntimelineRecordElement()
        {
            AskForHelpIntimelineRecordElement.Click();
        }
        public void EnterAskForHelpSubjectText(string subject)
        {
            try
            {
                //System.Threading.Thread.Sleep(3000);
                //ScrollDivAskForHelp.ScrollUntilSelectorDisplayedAndSendKeys(AskForHelpSubjectTextSelector, subject, 10);
                AskForHelpSubjectTextElement.Click();
                AskForHelpSubjectTextElement.SendKeys(subject);
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
        public void EnterAskForHelpDescriptionText(string description)
        {
            try
            {
                ScrollDivAskForHelp.ScrollUntilSelectorDisplayedAndSendKeys(AskForHelpDescriptionTextSelector, description, 10);
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
        public void ClickAskForHelpSaveAndCloseBtn()
        {
            try
            {
                AskForHelpSaveAndCloseBtnElement.Click();
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

        public bool SubjectNotesTextIsVisible(string subject)
        {
            return AskForHelpSubjectNotesElement.Text.Contains(subject);
        }
        public bool DescriptionIsVisible(string description)
        {
            if (AskForHelpDescriptionTextElement.IsElementDisplayed(20))
                return AskForHelpDescriptionTextElement.Text.Contains(description);
            else
                return false;
        }

        public bool ActivityStatusOpenCloseIsDisplayed(string activityOpenCloseStatus)
        {
            IgnoreAndSaveBtnOnAskForHelp();
            SeleniumExtensions.MoveToElement(ActivityStatusOpenCloseElement);
            return ActivityStatusOpenCloseElement.Text.Contains(activityOpenCloseStatus);
        }

        public void IgnoreAndSaveBtnOnAskForHelp()
        {
            try
            {
                SeleniumExtensions.ScrollUntilElementIsDisplayed(ScrollDivAskForHelpHomeElement, IgnoreAndSaveBtnOnAskForHelpSelector, 10);
                IgnoreAndSaveBtnOnAskForHelpElement.Click();
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

        public bool ActivityStatusTextIsDisplayed(string activityStatusText)
        {
            //SeleniumExtensions.MoveToElement(ActivityStatusElement);
            SeleniumExtensions.ScrollUntilElementIsDisplayed(ScrollDivAskForHelpHomeElement, ActivityStatusSelector, 10);
            return ActivityStatusElement.Text.Contains(activityStatusText);
        }

        public void ClickRecordOpen()
        {
            try
            {
                var jobPage = new JobPage();
                jobPage.VerifySaveAndContinue();
                jobPage.VerifyDuplicateRecords();
                OpenRecordElement.ClickUntilNoClickInterruptable();
                jobPage.VerifyDuplicateRecords();
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

        public bool IsServiceLevelTitleDisplayed()
        {
            var jobPage = new JobPage();
            jobPage.VerifySaveAndContinue();
            // SeleniumExtensions.MoveToElement(ServiceLevelTitleElement);
            SeleniumExtensions.ScrollUntilElementIsDisplayed(ScrollDivAskForHelpHomeElement, ServiceLevelTitleSelector, 10);
            return ServiceLevelTitleElement.Displayed;
        }

        public void ClickAssignIconOrBtn()
        {
            AssignIconOrBtnElement.ClickUntilNoClickInterruptable();
        }

        public void ClickAssignBtn()
        {
            AssignButton.ClickUntilNoClickInterruptable();
            var jobPage = new JobPage();
            jobPage.VerifySaveAndContinue();
            jobPage.VerifyDuplicateRecords();
        }

        public bool AssignToTeamOrUserHeaderIsDisplayed()
        {
            return AssignToTeamOrUserHeaderElement.Displayed;
        }

        public void SelectAssignToDropdown(string dropDowntext)
        {
            SeleniumExtensions.SelectElementByText(AssignToDropdownElement, dropDowntext);
        }

        public void FillUserOrTeamDetails(string teamName)
        {
            ScrollDivAskForHelp.ScrollUntilSelectorDisplayedAndSendKeys(AssignToTeamOrUserLookUpSelector, teamName, 10);
            AssignToTeamOrUserFirstValueElement.ClickUntilNoClickInterruptable();
        }

        public bool VerifyAskForHelpOwnerDetails(string ownerDetails)
        {
            return AskForhelpOwnerTextElement.Text.Contains(ownerDetails);
        }

        public void SelectCancelAskForHelpRadioBtn()
        {
            SeleniumExtensions.ScrollAndClick(CancelAskForHelpRadioBtnElement);
        }

        public void EnterReasonForCancellationText(string cancellationReasonText)
        {
            ReasonForCancellationTextElement.SendKeys(cancellationReasonText);
        }

        public bool VerifyReadOnlyCancellationNotificationIsDisplayed(string notificationText)
        {
            return ReadOnlyCancellationNotificationElement.Text.Contains(notificationText);
        }
        public bool VerifyActivityStatusText(string activityStatus)
        {
            return ActivityStatusTextElement.Text.Contains(activityStatus);
        }
        public void ClickEnterAnoteBtnAskForHelpElement()
        {
            EnterAnoteBtnAskForHelpElement.ClickUsingJavascript();
        }

        public void EnterTitleInTimeline(string title)
        {
            CreateTitleTimelineElement.SendKeys(title);
        }

        public void EnterANoteInTimeLine(string notes)
        {
            SeleniumExtensions.SwitchToIframe(CreateNotesTimelineSelector);
            CreateNotesTimelineElement.SendKeys(notes);
            SeleniumExtensions.SwitchToDefaultFrame();
        }

        public void ClickAddNoteBtnTimeline()
        {
            AddNoteBtnTimelineElement.Click();
        }

        public bool VerifyTimelineNotesTextModifiedIsDisplayed(string notes)
        {
            return TimelineNotesTextModifiedElement.Text.Contains(notes);
        }
    }
}
