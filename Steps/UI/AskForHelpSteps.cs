using NUnit.Framework;
using POMSeleniumFrameworkPoc1.Helpers;
using POMSeleniumFrameworkPoc1.Pages;
using POMSeleniumFrameworkPoc1.Pages.UI.CommonComponents;
using Reqnroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POMSeleniumFrameworkPoc1.Steps.UI
{
    [Binding]
    public sealed class AskForHelpSteps
    {
        [When(@"user click on the Action tab")]
        public void WhenUserClickOnTheActionTab()
        {
            try
            {
                var jobPage = new JobPage();
                jobPage.ClickActionsTab();
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

        [When(@"click Plus sign")]
        public void WhenClickPlusSign()
        {
            try
            {
                var jobPage = new JobPage();
                jobPage.ClickCreateATimelineRecordBtn();
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

        [When(@"click Ask for help from the list")]
        public void WhenClickAskForHelpFromTheList()
        {
            try
            {
                var askForHelpPage = new AskForHelpPage();
                askForHelpPage.ClickAskForHelpIntimelineRecordElement();
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

        [When(@"user fill Subject as ""(.*)""")]
        public void WhenUserFillSubjectAs(string subject)
        {
            try
            {
                var askForHelpPage = new AskForHelpPage();
                askForHelpPage.EnterAskForHelpSubjectText(subject);
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

        [When(@"provides description as ""(.*)""")]
        public void WhenProvidesDescriptionAs(string description)
        {
            try
            {
                var askForHelpPage = new AskForHelpPage();
                askForHelpPage.EnterAskForHelpDescriptionText(description);
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

        [When(@"user clicks Save and close")]
        public void WhenUserClicksSaveAndClose()
        {
            try
            {
                var askForHelpPage = new AskForHelpPage();
                askForHelpPage.ClickAskForHelpSaveAndCloseBtn();
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

        [Then(@"request should be created in the timeline section of Action tab")]
        public void ThenRequestShouldBeCreatedInTheTimelineSectionOfActionTab()
        {
            try
            {
                var askForHelpPage = new AskForHelpPage();
                //  Assert.IsTrue(askForHelpPage.SubjectNotesTextIsVisible("Help needed"), "subject text is not displayed");
                Assert.IsTrue(askForHelpPage.DescriptionIsVisible("More information needed"), "Description text is not displayed");
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

        [Then(@"Activity Status should set as ""(.*)"" and ""(.*)""")]
        public void ThenActivityStatusShouldSetAsAnd(string status1, string status2)
        {
            try
            {
                var askForHelpPage = new AskForHelpPage();
                //Assert.IsTrue(askForHelpPage.ActivityStatusOpenCloseIsDisplayed(status1), "active/inactive status is not displayed");
                Assert.IsTrue(askForHelpPage.ActivityStatusTextIsDisplayed(status1), "Open/Close text is not displayed");
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

        [When(@"user click Open Record icon")]
        public void WhenUserClickOpenRecordIcon()
        {
            try
            {
                var askForHelpPage = new AskForHelpPage();
                askForHelpPage.ClickRecordOpen();
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

        [Then(@"verify SLA-Service Level Agreement section")]
        public void ThenVerifySLA_ServiceLevelAgreementSection()
        {
            try
            {
                var askForHelpPage = new AskForHelpPage();
                Assert.IsTrue(askForHelpPage.IsServiceLevelTitleDisplayed(), "ServiceLevelTitle is not displayed");
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

        [When(@"user clicks Assign icon")]
        public void WhenUserClicksAssignIcon()
        {
            try
            {
                var askForHelpPage = new AskForHelpPage();
                askForHelpPage.ClickAssignIconOrBtn();
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

        [Then(@"Assign to Team or User popup open up")]
        public void ThenAssignToTeamOrUserPopupOpenUp()
        {
            try
            {
                var askForHelpPage = new AskForHelpPage();
                Assert.IsTrue(askForHelpPage.AssignToTeamOrUserHeaderIsDisplayed(), "Assign To Team Or User pop up is not Displayed");
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

        [When(@"user selects  Assign To as ""(.*)""")]
        public void WhenUserSelectsAssignToAs(string dropDownText)
        {
            try
            {
                var askForHelpPage = new AskForHelpPage();
                askForHelpPage.SelectAssignToDropdown(dropDownText);
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

        [When(@"User fill search name as '(.*)'")]
        public void WhenUserFillSearchNameAs(string teamName)
        {
            try
            {
                var askForHelpPage = new AskForHelpPage();
                askForHelpPage.FillUserOrTeamDetails(teamName);
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

        [When(@"user click open record by clicking on open record icon")]
        public void WhenUserClickOpenRecordByClickingOnOpenRecordIcon()
        {
            try
            {
                var askForHelpPage = new AskForHelpPage();
                askForHelpPage.ClickRecordOpen();
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

        [Then(@"record open up and owner fields updates as per selection from the lookup as '(.*)'")]
        public void ThenRecordOpenUpAndOwnerFieldsUpdatesAsPerSelectionFromTheLookupAs(string ownerDetails)
        {
            try
            {
                var askForHelpPage = new AskForHelpPage();
                Assert.IsTrue(askForHelpPage.VerifyAskForHelpOwnerDetails(ownerDetails), "the Ask for help owner details are incorrect");
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

        [When(@"User click Cancel Ask for Help toggle as Yes")]
        public void WhenUserClickCancelAskForHelpToggleAsYes()
        {
            try
            {
                var askForHelpPage = new AskForHelpPage();
                askForHelpPage.SelectCancelAskForHelpRadioBtn();
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

        [When(@"enters Reason for Cancellation as raise with mistake")]
        public void WhenEntersReasonForCancellationAsRaiseWithMistake()
        {
            try
            {
                var askForHelpPage = new AskForHelpPage();
                askForHelpPage.EnterReasonForCancellationText("Automation test cancellation");
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

        [Then(@"the record should become read only with the message as '(.*)'")]
        public void ThenTheRecordShouldBecomeReadOnlyWithTheMessageAs(string readOnlyNotificationMessage)
        {
            try
            {
                var askForHelpPage = new AskForHelpPage();
                Assert.IsTrue(askForHelpPage.VerifyReadOnlyCancellationNotificationIsDisplayed(readOnlyNotificationMessage), "the Ask for help cancellation notication yext message is not displayed");
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

        [Then(@"Activity Status should set as '(.*)'")]
        public void ThenActivityStatusShouldSetAs(string status)
        {
            try
            {
                var askForHelpPage = new AskForHelpPage();
                Assert.IsTrue(askForHelpPage.VerifyActivityStatusText(status), "the activity status is not cancelled");
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

        [When(@"click assign button")]
        public void WhenClickAssignButton()
        {
            try
            {
                var askForHelpPage = new AskForHelpPage();
                askForHelpPage.ClickAssignBtn();
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

        [When(@"User clicks Enter a note button in the timeline section")]
        public void WhenUserClicksEnterANoteButtonInTheTimelineSection()
        {
            try
            {
                var askForHelpPage = new AskForHelpPage();
                askForHelpPage.ClickEnterAnoteBtnAskForHelpElement();
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

        [When(@"User enter Title ""(.*)""")]
        public void WhenUserEnterTitle(string title)
        {
            try
            {
                var askForHelpPage = new AskForHelpPage();
                askForHelpPage.EnterTitleInTimeline(title);
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

        [When(@"text ""(.*)"" in the section")]
        public void WhenTextInTheSection(string timelinetext)
        {
            try
            {
                var askForHelpPage = new AskForHelpPage();
                askForHelpPage.EnterANoteInTimeLine(timelinetext);
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

        [When(@"click Add note button")]
        public void WhenClickAddNoteButton()
        {
            try
            {
                var askForHelpPage = new AskForHelpPage();
                askForHelpPage.ClickAddNoteBtnTimeline();
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

        [Then(@"notes display within the timeline section as ""(.*)""")]
        public void ThenNotesDisplayWithinTheTimelineSectionAs(string notes)
        {
            try
            {
                var askForHelpPage = new AskForHelpPage();
                Assert.IsTrue(askForHelpPage.VerifyTimelineNotesTextModifiedIsDisplayed(notes), "the notes in the timeline is incorrect and not displayed");
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

        [When(@"User save the changes")]
        public void WhenUserSaveTheChanges()
        {
            try
            {
                var jobPage = new JobPage();
                jobPage.SimpleSave();
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

    }
}
