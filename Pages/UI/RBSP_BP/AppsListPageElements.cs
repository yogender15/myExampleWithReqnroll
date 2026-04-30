using OpenQA.Selenium;
using POMSeleniumFrameworkPoc1.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POMSeleniumFrameworkPoc1.Pages.UI.RBSP_BP
{
    public partial class AppsListPage : BasePage
    {
        public By IframeSelector => By.Id("AppLandingPage");
        public By CouncilTaxAppLoc => By.CssSelector("*[title*='Council Tax']");

        public IWebElement CouncilTaxApp => Driver.WaitForElement(By.CssSelector("*[title*='Council Tax']"));
    }
}
