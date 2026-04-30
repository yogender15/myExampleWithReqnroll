using POMSeleniumFrameworkPoc1.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POMSeleniumFrameworkPoc1.Pages.UI
{
    public partial class SubmissionPage : BasePage
    {
        public void OpenSiteMapNewTab()
        {
            SiteMapItemNewTab.Click();
        }

        public void OpenActiveSubmissionTab()
        {
            ActiveSubmissionsTab.Click();
        }

        public void OpenNewSubmissionForm()
        {
            NewSubmissionMenuButton.Click();
        }

        public void SelectSubmittedBy(string submittedBy)
        {

            ScrollDiv.ScrollUntilSelectorDisplayedAndSendKeys(SubmittedbyLookupInputSelector,
                submittedBy, 10);
            SubmittedByLookUpResultItem.Click();
        }

        public string SaveSubmission()
        {
            SaveSubmissionMenuButton.Click();
            SubmissionIdElement.ScrollAndClick();
            Driver.WaitUntilElementHasValue(SubmissionIdSelector, "---", false, 30000);
            SubmissionId = SubmissionIdElement.GetAttribute("title");
            return SubmissionId;
        }
    }
}
