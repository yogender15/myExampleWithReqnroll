using POMSeleniumFrameworkPoc1.Helpers;
using System;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POMSeleniumFrameworkPoc1.Pages
{
    public class LoginPage : BasePage
    {
        public IWebElement select_existing_user => 
            Driver.WaitForElement(By.XPath("(*//*[contains(@class,'table')]/*[contains(@class,'content')])[1]"));
        public void GoToLoginPage()
        {
            Driver.Navigate().GoToUrl(Config.BaseUrl);
        }

        public void LoginWithExistingUser()
        {
            select_existing_user.Click();
        }
    

    }
}
