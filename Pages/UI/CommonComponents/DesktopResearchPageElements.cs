using OpenQA.Selenium;
using POMSeleniumFrameworkPoc1.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POMSeleniumFrameworkPoc1.Pages.UI.CommonComponents
{
    public partial class DesktopResearchPage : BasePage
    {
        public IWebElement DetailsBpf => Driver.WaitForElement(By.XPath("//div[contains(@title,'Details') and contains(@id,'stageNameContainer')]"));
        public IWebElement NextStageDesktopResearhBPF => Driver.WaitForElement(By.CssSelector("button[aria-label='Next Stage']"));
        private IWebElement DetailsOptionTab => Driver.WaitForElement(By.XPath("//*[@data-id='MscrmControls.Containers.ProcessBreadCrumb-stageStatusLabel']"));
        private IWebElement AutoProcessDropDown => Driver.WaitForElement(By.XPath("//*[@data-id='header_process_voa_autoprocess.fieldControl-checkbox-select']"));
        private IWebElement DetailsNextStageButton => Driver.WaitForElement(By.XPath("//span[contains(@title,'Auto Process')]/ancestor::*//button[@aria-label='Next Stage']"));
        private By DesktopOptionTab => By.CssSelector("[title='Desktop Research']");
        public IWebElement SetActiveDetails => Driver.WaitForElement(By.XPath("//button[contains(@aria-label,'Set Active')]"));
        public IWebElement IsFurtherinformationRequiredDesktopResearch =>
            Driver.WaitForElement(By.XPath("//select[contains(@aria-label,'Is Further Information Required?')]"));
        public IWebElement DesktopResearchDate => Driver.WaitForElement(By.XPath(" //input[contains(@aria-label,'Date of Desktop Research date')]"));
        public IWebElement RateableValueType => Driver.WaitForElement(By.CssSelector("[id*='ProcessStageControl-processHeaderStageFlyoutInnerContainer'] select[title='Nil']"));
        public IWebElement IsDesktopResearchComplete => Driver.WaitForElement(By.CssSelector("select[aria-label='Is Desktop Research complete?']"));
        public IWebElement ProceedToInspection => Driver.WaitForElement(By.CssSelector("select[aria-label='Proceed to Inspection']"));
        public IWebElement ReasonForInspection => Driver.WaitForElement(By.CssSelector("textarea[aria-label='Reason for Inspection']"));
        public IWebElement VerifyAddressGrid => Driver.WaitForElement(By.CssSelector("section[aria-label='Verify Address']"));
        public IWebElement IstheBAReferenceMissing => Driver.WaitForElement(By.CssSelector("button[aria-label='Is the BA Reference Missing?: No']"));
        public IWebElement BAReferenceMissingComments => Driver.WaitForElement(By.CssSelector("textarea[aria-label='BA Reference Missing Comments']"));
        public IWebElement TCTRDetailsTab => Driver.WaitForElement(By.CssSelector("li[aria-label='TCTR Details']"));
        public IWebElement ReasonForIncompleteTCTRResponse => Driver.WaitForElement(By.CssSelector("input[aria-label='Reason for Incomplete TCTR Response']"));
        public IWebElement AreTCTRResponsesComplete => Driver.WaitForElement(By.CssSelector("select[aria-label='Are TCTR Responses Complete? ']"));
        public IWebElement ReasonForIncompleteTCTRCheckDetail => Driver.WaitForElement(By.CssSelector("li[aria-label='TCTR Details']")); 
        public IWebElement IsTheRALDCheckComplete => Driver.WaitForElement(By.CssSelector("button[aria-label='Is the RALD Check Complete?: No']"));

    }
}
