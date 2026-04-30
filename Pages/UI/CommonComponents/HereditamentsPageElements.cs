using OpenQA.Selenium;
using POMSeleniumFrameworkPoc1.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POMSeleniumFrameworkPoc1.Pages.UI.CommonComponents
{
    public partial class HereditamentsPage : BasePage
    {
        private IWebElement HereditamentsTitle => Driver.WaitForElement(By.XPath("//div[contains(@title,'Hereditaments')]"));
        private IWebElement HereditamentNewBtn => Driver.WaitForElement(By.XPath("//*[contains(@aria-label,'Hereditament Commands')]//button[contains(@aria-label,'New')]"));
        private IWebElement HereditamentLookUpResultFirstValue => Driver.WaitForElement(By.XPath("//*[@data-id='voa_statutoryspatialunitid.fieldControl-voa_name0_0_0']"));
        private By HereditamentLookUpFieldSelector => By.XPath("//*[@data-id='voa_statutoryspatialunitid.fieldControl-LookupResultsDropdown_voa_statutoryspatialunitid_textInputBox_with_filter_new']");
    }
}
