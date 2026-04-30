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
        public void ClickInformalChallengeTabUnderEnquiries()
        {
            InformalChallengeTabUnderEnquiriesElement.Click();
        }

        public void FillPropertyOnInformalChallengePage(string property)
        {
            //CodedReasonSearchIcon.ScrollAndClick();
            //CodedReasonTextBox.Click();
            //CodedReasonTextBox.SendKeys(Keys.Backspace);
            //CodedReasonTextBox.SendKeys(codedReason);
            //CodedReasonValue.Click();
        }
    }
}
