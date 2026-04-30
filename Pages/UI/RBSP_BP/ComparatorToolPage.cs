using OpenQA.Selenium;
using POMSeleniumFrameworkPoc1.Helpers;
using POMSeleniumFrameworkPoc1.Pages.UI.CommonComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace POMSeleniumFrameworkPoc1.Pages.UI.RBSP_BP
{

    public class ComparatorToolPage : BasePage
    {
        private By loadComparatorBy = By.CssSelector("button[aria-label='Load to Comparator']");
        private By validateComparatorBy = By.CssSelector("button[aria-label='Validate Comparators']");
        private By systemDecision = By.CssSelector("[data-id='[data-id='voa_systemsuggesteddecisionid.fieldControl-LookupResultsDropdown_voa_systemsuggesteddecisionid_selected_tag_text']");
        public void FindHereditamentForComparator(string expUPRN)
        {
            waitHelpers wh = new waitHelpers();
            CommonPage commonpage = new CommonPage();
            var hereditamentsPage = new HereditamentsPage();
            hereditamentsPage.selectSearchByDropDown("UPRN");
            wh.GetWebElement(uprnBY).ClearAndSendkeys(expUPRN);
            SearchBtnOnDialog.ClickUsingJavascript();
            hereditamentsPage.selectHeridetamentByuprnWithOutSelect(expUPRN);
            wh.clickOnElement(loadComparatorBy);
            wh.clickOnElement(validateComparatorBy);
            Thread.Sleep(3000);
            SeleniumExtensions.SwitchWindow("Comparator Tool Outcome");
        }

        public void overrideSystemDecision(string systemDecisionFieldName,string overridedecision,string overrideOption)
        {
            waitHelpers wh = new waitHelpers();
            string systemDecisionTxt = wh.getElementText(systemDecision);
            if (systemDecisionTxt.Equals("Rejected"))
            {
                SeleniumExtensions.clickElementAndSelectText(overridedecision, overrideOption);
            }
        }

        }
}
