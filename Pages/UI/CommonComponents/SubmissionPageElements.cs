using OpenQA.Selenium;
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
        private IWebElement SiteMapItemNewTab => Driver.WaitForElement(By.XPath("//*[contains(@title,'Open site map item in a new tab')]"));
        private IWebElement ActiveSubmissionsTab => Driver.WaitForElement(By.CssSelector("ul[role='menu'] li[aria-label='Submissions']"));
        private IWebElement ScrollDiv => Driver.WaitForElement(By.CssSelector("#tab-section2"));
        private IWebElement NewSubmissionMenuButton => Driver.WaitForElement(By.XPath("*[@role='menubar']//button[contains(@title, 'Create a new Submission record.') and contains(@aria-label,'New')]"));
        private IWebElement SaveSubmissionMenuButton => Driver.WaitForElement(By.XPath("//*[@role='menubar']//button[contains(@title,'Save this Submission')]"));
        private By SubmittedbyLookupInputSelector => By.XPath("//input[contains(@aria-label,'Submitted By')]");
        private IWebElement SubmittedByLookUpResultItem => Driver.WaitForElement(By.XPath("//*[contains(@data-id,\"voa_primarycustomerid.fieldControl\")]//ul[contains(@id,'voa_primarycustomerid.fieldControl-LookupResultsDropdown')]/li"));
        private By SubmissionIdSelector => By.XPath("//input[@aria-label=\"Submission ID\"]");
        private IWebElement SubmissionIdElement => Driver.WaitForElement(By.XPath("//input[@aria-label=\"Submission ID\"]"));
        private IWebElement RelatedJobsSubmission => Driver.WaitForElement(By.CssSelector("li[aria-label='Related Jobs']"));
        private IWebElement RelatedTabSubmission => Driver.WaitForElement(By.XPath("//div[contains(@class,'pa-a flexbox') and contains(., 'Related')]"));

        public string SubmissionId;
    }
}
