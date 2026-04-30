using OpenQA.Selenium;
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
        private IWebElement AskForHelpIntimelineRecordElement => Driver.WaitForElement(By.CssSelector("ul li[aria-label='Ask for Help Activity']"));
        private By AskForHelpSubjectTextSelector => By.CssSelector("input[aria-label='Subject']");
        private IWebElement AskForHelpSubjectTextElement => Driver.WaitForElement(By.CssSelector("input[aria-label='Subject']"));
        private By AskForHelpDescriptionTextSelector => By.CssSelector("textarea[aria-label='Description']");
        private IWebElement ScrollDivAskForHelp => Driver.WaitForElement(By.CssSelector("div #tab-section4"));
        private IWebElement ScrollDivAskForHelpHomeElement => Driver.WaitForElement(By.CssSelector("div #tab-section5"));
        private IWebElement AskForHelpSaveAndCloseBtnElement => Driver.WaitForElement(By.CssSelector("button[aria-label='Save & Close']"));
        private IWebElement AskForHelpSubjectNotesElement => Driver.WaitForElement(By.CssSelector("label[data-id*='notescontrol-timeline_field_subject']"));
        private IWebElement AskForHelpDescriptionTextElement => Driver.WaitForElement(By.CssSelector("div[id*='timeline_field_description']"));
        private IWebElement ActivityStatusOpenCloseElement => Driver.WaitForElement(By.CssSelector("div[id*='timeline_record_content']"));
        private IWebElement ActivityStatusElement => Driver.WaitForElement(By.CssSelector("div[id*='timeline_field_statecode']"));
        private By ActivityStatusSelector => By.CssSelector("div[class*='pa-en pa-hm']");
        private IWebElement RecordOpenElement => Driver.WaitForElement(By.CssSelector("a[aria-label='Open Record']"));
        private IWebElement ServiceLevelTitleElement => Driver.WaitForElement(By.CssSelector("h2[title='Service Level Agreement']"));
        private By ServiceLevelTitleSelector => By.CssSelector("h2[title='Service Level Agreement']");
        private IWebElement OpenRecordElement => Driver.WaitForElement(By.CssSelector("a[aria-label='Open Record']"));
        private IWebElement AssignIconOrBtnElement => Driver.WaitForElement(By.CssSelector("button[aria-label='Assign']"));
        private IWebElement AssignButton => Driver.WaitForElement(By.CssSelector("[id*='dialogFooterContainer'] button[aria-label='Assign']"));
        private IWebElement AssignToTeamOrUserHeaderElement => Driver.WaitForElement(By.CssSelector("h1[aria-label='Assign to Team or User']"));
        private IWebElement AssignToDropdownElement => Driver.WaitForElement(By.CssSelector("select[aria-label='Assign To']"));
        private By AssignToTeamOrUserLookUpSelector => By.CssSelector("input[aria-label='User or team, Lookup']");
        private IWebElement AssignToTeamOrUserFirstValueElement => Driver.WaitForElement(By.CssSelector("div ul li[aria-label='ACADEMY TEAM']"));
        private IWebElement AskForhelpOwnerTextElement => Driver.WaitForElement(By.CssSelector("a[aria-label='ACADEMY TEAM']"));
        private IWebElement CancelAskForHelpRadioBtnElement => Driver.WaitForElement(By.CssSelector("button[aria-label='Cancel Ask for Help: No']"));
        private IWebElement ReasonForCancellationTextElement => Driver.WaitForElement(By.CssSelector("textarea[aria-label='Reason for Cancellation']"));
        private IWebElement ReadOnlyCancellationNotificationElement => Driver.WaitForElement(By.CssSelector("div[id*='message-formReadOnlyNotification']"));
        private IWebElement ActivityStatusTextElement => Driver.WaitForElement(By.XPath("*//*[contains(text(),'Activity Status')]/parent::*//*[contains(@role,'presentation') and contains(@class,'pa-tn')]"));
        private IWebElement EnterAnoteBtnAskForHelpElement => Driver.WaitForElement(By.CssSelector("button[aria-label='Enter a note...']"));
        private By CreateNotesTimelineSelector => By.TagName("iframe");
        private IWebElement CreateNotesTimelineElement => Driver.WaitForElement(By.CssSelector("iframe[aria-label]"));
        private IWebElement CreateTitleTimelineElement => Driver.WaitForElement(By.CssSelector("input[aria-label='Title']"));
        private IWebElement AddNoteBtnTimelineElement => Driver.WaitForElement(By.CssSelector("button[aria-label='Add note']"));
        private IWebElement TimelineNotesTextModifiedElement => Driver.WaitForElement(By.CssSelector("div[id*='timeline_record_title_text']"));
        public By IgnoreAndSaveBtnOnAskForHelpSelector => By.CssSelector("button[title='Ignore And Save']");
        public IWebElement IgnoreAndSaveBtnOnAskForHelpElement => Driver.WaitForElement(By.CssSelector("button[title='Ignore And Save']"));

    }
}
