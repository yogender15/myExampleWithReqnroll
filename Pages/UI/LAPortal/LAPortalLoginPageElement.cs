using OpenQA.Selenium;
using POMSeleniumFrameworkPoc1.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POMSeleniumFrameworkPoc1.Pages.UI.LAPortal
{
    public partial class LAPortalLoginPage : BasePage
    {
        public IWebElement StartNowBtnElement => Driver.WaitForElement(By.CssSelector("a[class='govuk-button govuk-button--start']"));
        public IWebElement UserNameLAPortalElement => Driver.WaitForElement(By.CssSelector("input[id='Username']"));
        public IWebElement PasswordLAPortalElement => Driver.WaitForElement(By.CssSelector("input[id='PasswordValue']"));
        public IWebElement SignInLAPortalBtnElement => Driver.WaitForElement(By.CssSelector("button[id='submit-signin-local']"));
    }
}
