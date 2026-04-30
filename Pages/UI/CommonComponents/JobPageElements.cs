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
        private IWebElement JobsButton => Driver.WaitForElement(By.XPath("//li[@aria-label=\"Jobs\"]"));
        private IWebElement NewJobLabel => Driver.WaitForElement(By.XPath("//*[@title='New Job']"));
        private IWebElement ChannelDropDown => Driver.WaitForElement(By.XPath("//*[@aria-label='Channel']"));
        private IWebElement OKButton => Driver.WaitForElement(By.XPath("//*[@aria-label='OK']"));
        private IWebElement RequestTypeValue => Driver.WaitForElement(By.XPath("//*[contains(@data-id,'voa_requesttype.fieldControl-voa_name0_0_0')]"));
        private By RequestTypeLookupField => By.XPath("//*[@data-id='voa_requesttype.fieldControl-LookupResultsDropdown_voa_requesttype_textInputBox_with_filter_new']");
        private IWebElement CodedReasonTextBox => Driver.WaitForElement(By.XPath("//*[@data-id='voa_codedreason.fieldControl-LookupResultsDropdown_voa_codedreason_textInputBox_with_filter_new']"));
        private IWebElement CodedReasonValue => Driver.WaitForElement(By.XPath("//*[@data-id='voa_codedreason.fieldControl-voa_name0_0_0']"));
        private By EffectiveDatePickerSelector => By.CssSelector("[aria-label='Date of Effective Date']");
        private By PartyLookUpSelector => By.XPath("//*[@data-id='customerid.fieldControl-LookupResultsDropdown_customerid_textInputBox_with_filter_new']");
        private IWebElement PartyLookUpResultFirstValue => Driver.WaitForElement(By.XPath("//*[@data-id='customerid.fieldControl-fullname0_0_0']"));
        private IWebElement HereditamentLookUpResultFirstValue => Driver.WaitForElement(By.XPath("//*[@data-id='voa_statutoryspatialunitid.fieldControl-voa_name0_0_0']"));
        private By HereditamentLookUpFieldSelector => By.XPath("//*[@data-id='voa_statutoryspatialunitid.fieldControl-LookupResultsDropdown_voa_statutoryspatialunitid_textInputBox_with_filter_new']");
        private IWebElement SaveLabel => Driver.WaitForElement(By.XPath("//*[text()='Save']"));
        private IWebElement JobIDTextBox => Driver.WaitForElement(By.XPath("//input[@aria-label='Job ID']"));
        private By JobIDTextBoxSelector => By.XPath("//input[@aria-label='Job ID']");
        private IWebElement JobSearchTextBox => Driver.WaitForElement(By.XPath("//*[@aria-label='Job Search this view']"));
        private IWebElement JobSearchButton => Driver.WaitForElement(By.XPath("//*[@aria-label='Start search']"));
        private IWebElement TCTRDetailsTitle => Driver.WaitForElement(By.XPath("//*[@title='TCTR Details']"));
        private IWebElement RatePayerTextBox => Driver.WaitForElements(By.CssSelector("input[data-id*='LookupResultsDropdown_voa_accountid_textInputBox_with_filter_new']"))[1];
        private IWebElement RatePayerLookupResultValue => Driver.WaitForElement(By.CssSelector("ul[aria-label='Lookup results'] li"));
        private IWebElement DetailsOptionTab => Driver.WaitForElement(By.XPath("//*[@data-id='MscrmControls.Containers.ProcessBreadCrumb-stageStatusLabel']"));
        private IWebElement AutoProcessDropDown => Driver.WaitForElement(By.XPath("//*[@data-id='header_process_voa_autoprocess.fieldControl-checkbox-select']"));
        private IWebElement DetailsNextStageButton => Driver.WaitForElement(By.XPath("//span[contains(@title,'Auto Process')]/ancestor::*//button[@aria-label='Next Stage']"));
        private IWebElement IgnoreAndSaveButton => Driver.WaitForElement(By.XPath("//*[@data-id='ignore_save']"));
        private By CopyDesktopOptionTab => By.CssSelector("[title='COPY Desktop Research']");
        private IWebElement FinishButton => Driver.WaitForElement(By.XPath("//*[@data-id='MscrmControls.Containers.ProcessStageControl-finishButtonContainer']"));
        private IWebElement FinishedButton => Driver.WaitForElement(By.XPath("//*[@data-id='MscrmControls.Containers.ProcessStageControl-finishedLabelContainer'][@title='Finished']"));
        private IWebElement PublishListUpdatesDropDown => Driver.WaitForElement(By.XPath("//*[@data-id='header_process_voa_publishlistupdates.fieldControl-checkbox-select']"));
        private IWebElement ScrollDiv => Driver.WaitForElement(By.CssSelector("#tab-section2"));
        private By SubmittedbyLookupInput => By.XPath("//input[contains(@aria-label,'Submitted By')]");
        // private IWebElement SubmittedByLookUpResultItem => Driver.WaitForElement(By.XPath("//*[contains(@data-id,\"voa_primarycustomerid.fieldControl\")]//ul[contains(@id,'voa_primarycustomerid.fieldControl-LookupResultsDropdown')]/li"));
        private By SubmittedBySearchSelector => By.CssSelector("input[aria-label='Submitted By, Lookup']");
        private IWebElement SubmittedByLookUpResultItem => Driver.WaitForElement(By.CssSelector("div[title='Adur Council']"));
        private IWebElement Cases => Driver.WaitForElement(By.CssSelector("div[title='Cases']"));
        private By JobIdSelector(string jobId) => By.XPath($"//*[text()='{jobId}']");
        public string JobId;

    }
}
