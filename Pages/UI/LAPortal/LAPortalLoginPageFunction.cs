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
        public void ClickStartNowBtnElement()
        {
            StartNowBtnElement.ClickUsingJavascript();
        }

        public void EnterLoginCredentialsAndClickSignInToLAPortal(string userName, string password)
        {
            //  UserNameLAPortalElement.ElementVisisbleUsingExplicitWait(5);
            UserNameLAPortalElement.WaitUntilElementAttached(5);
            UserNameLAPortalElement.SendKeys(userName);
            PasswordLAPortalElement.SendKeys(password);
            SignInLAPortalBtnElement.ClickUsingJavascript();
        }
    }
}
