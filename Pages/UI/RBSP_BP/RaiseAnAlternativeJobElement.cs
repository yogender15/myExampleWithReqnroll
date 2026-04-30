using OpenQA.Selenium;
using POMSeleniumFrameworkPoc1.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POMSeleniumFrameworkPoc1.Pages.UI.RBSP_BP
{
    public partial class RaiseAnAlternativeJob : BasePage
    {
        private IWebElement JobActionsDropDownElement => Driver.WaitForElement(By.CssSelector("button[aria-label='Job Actions']"));
        private IWebElement RaiseAnAlternativeJobDropDownOnMainNavElement => Driver.WaitForElement(By.CssSelector("li button[aria-label='Raise Alternate Job']"));

        private By AlternativeJobTypeSelector => By.CssSelector("input[aria-label='Alternate Job Type, Lookup']");
        private IWebElement AlternativeJobTypeFirstElement => Driver.WaitForElement(By.CssSelector("ul li[aria-label='Council Tax']"));

        private By AlternateJobCodedReasonSelector => By.CssSelector("input[aria-label='Alternate Job Coded Reason, Lookup']");
        private IWebElement CodedReasonTextFieldOnAdvancedLookUpWindow => Driver.WaitForElement(By.CssSelector("input[aria-labelledby*='advanced_lookup']"));

        private IWebElement AlternateJobCodedReasonFirstElement => Driver.WaitForElement(By.CssSelector("ul li[aria-label='Council Tax']"));

        private IWebElement AlternateJobCodedReasonSearchElement => Driver.WaitForElement(By.CssSelector("button[aria-label='Search records for Alternate Job Coded Reason, Lookup field']"));

        private IWebElement AlternateJobAdvancedLookUpFieldElement => Driver.WaitForElement(By.CssSelector("a[aria-label='Advanced lookup']"));

        private IWebElement CodedReasonChevronElement => Driver.WaitForElement(By.CssSelector("div[aria-label='Coded Reason'] [id*='Dropdown']"));
        private IWebElement NoButton => Driver.WaitForElement(By.CssSelector("[aria-label*='No']"));
        private IWebElement ActiveCodedReasonFromDrpDownListElement => Driver.WaitForElement(By.XPath("*//button//*[contains(text(),'Active Coded Reasons')]"));

        private IWebElement ActiveCodedReasonResultDeletionOption => Driver.WaitForElements(By.CssSelector("button[aria-label='Deletion']")).FirstOrDefault();
        private IWebElement DoneButtonOnAdvacneLookUpWindow => Driver.WaitForElement(By.CssSelector("button[title='Done']"));

        private IWebElement CloseButtonOnAdvacneLookUpWindow => Driver.WaitForElement(By.CssSelector("[id*='Dialog'] button[title='Close']"));
        private By AlternativeJobProposedDatePickerSelector => By.CssSelector("input[aria-label='Date of Alternate Job Proposed Effective Date']");
        private IWebElement AlternativeJobProposedDatePicker => Driver.WaitForElement(By.CssSelector("input[aria-label='Date of Alternate Job Proposed Effective Date']"));

        private IWebElement SaveAndCloseButton => Driver.WaitForElement(By.CssSelector("button[aria-label='Save and Close']"));
        private IWebElement AlternativeHeaderTitle => Driver.WaitForElement(By.CssSelector("h1[title*='Alternate']"));

    }
}
