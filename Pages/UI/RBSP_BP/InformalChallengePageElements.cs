using OpenQA.Selenium;
using POMSeleniumFrameworkPoc1.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POMSeleniumFrameworkPoc1.Pages.UI.RBSP_BP
{
    public partial class InformalChallengePage : BasePage
    {
        private IWebElement InformalChallengeTabUnderEnquiriesElement => Driver.WaitForElement(By.CssSelector("li[aria-label='CT Informal Challenge']"));
        private IWebElement PropertySearchIconInformalChallengePageElement => Driver.WaitForElement(By.CssSelector("button[aria-label='Search records for Coded Reason, Lookup field']"));
        private IWebElement PropertyTextBoxInformalChallengePageElement => Driver.WaitForElement(By.XPath("//input[contains(@data-id,'voa_propertyid.fieldControl-LookupResultsDropdown_voa_propertyid_textInputBox_with_filter_new') and contains(@data-pa-landmark-active-element,'true')]"));
        private IWebElement PropertyInformalChallengePageValue => Driver.WaitForElement(By.XPath("//li[@data-id='voa_codedreasonid.fieldControl-LookupResultsDropdown_voa_codedreasonid_resultsContainer'][1]"));


    }
}
