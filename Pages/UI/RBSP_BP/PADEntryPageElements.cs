using OpenQA.Selenium;
using POMSeleniumFrameworkPoc1.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POMSeleniumFrameworkPoc1.Pages.UI.RBSP_BP
{
    public partial class PADEntryPage : BasePage
    {
        private IWebElement ChevronRightMed => Driver.WaitForElement(By.XPath("//*[@data-icon-name='ChevronRightMed']"));

        private IWebElement StatusCircleCheckmark => Driver.WaitForElement(By.XPath("//*[@data-icon-name='StatusCircleCheckmark']"));

        private By effectiveFromDate = By.CssSelector("div[data-automation-key='fromDateColumn'] div[class*='ms-Stack css']");
        private IWebElement SaveAndContinue => Driver.WaitForElement(By.XPath("//div[text()='Save and continue']"));


        private By GetPADTab(string tabName)
        {
            string customizeSelector = $"//*[text()='{tabName}']";
            return By.XPath(customizeSelector);

        }

        private IWebElement FindByText(string popUpName)
        {
            string customizeSelector = $"//*[text()='{popUpName}']//ancestor::button";
            return Driver.WaitForElement(By.XPath(customizeSelector));

        }

        private IWebElement GetNDRValue(string Value)
        {
            string customizeSelector = $"//button[@aria-label='{Value}']";
            return Driver.WaitForElement(By.XPath(customizeSelector));

        }

        private IWebElement NDRAssessmentPopUp => Driver.WaitForElement(By.CssSelector("[aria-label='Need review for NDR assessment']"));
    }
}
