using OpenQA.Selenium;
using POMSeleniumFrameworkPoc1.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POMSeleniumFrameworkPoc1.Pages.UI.RBSP_BP
{
    public partial class BPFPage : BasePage
    {
        public IWebElement HasTheDecisionLetterNoticeBeenCreatedYesNoElement => Driver.WaitForElement(By.CssSelector("select[aria-label='Has the Decision Letter / Notice been created? ']"));
        public IWebElement CorrespondenceChecksCompleteYesNoElement => Driver.WaitForElement(By.CssSelector("select[aria-label='Correspondence Checks Complete?']"));
        public IWebElement SystemRequiresQualityAssuranceYesNoElement => Driver.WaitForElement(By.CssSelector("select[aria-label='System Requires Quality Assurance?']"));
        private IWebElement FinishedAfterReleaseAndPublishBPFElement => Driver.WaitForElement(By.CssSelector("button[title='Finished']"));

        private IWebElement closeBtnBPFElement => Driver.WaitForElement(By.CssSelector("[aria-label='Close']"));

        private By closeBtnBPFElementBy = By.CssSelector("[aria-label='Close']");


    }
}
