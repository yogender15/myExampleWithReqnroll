using POMSeleniumFrameworkPoc1.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POMSeleniumFrameworkPoc1.Pages.UI.CommonComponents
{
    public partial class DesktopResearchPage : BasePage
    {
        public void EnterDetailsTabInfo(string option)
        {
            DetailsOptionTab.ClickUntilNoClickInterruptable();
            AutoProcessDropDown.SelectElementByText(option);
            Driver.WaitForElementToDissapear(DesktopOptionTab, 500);
            DetailsNextStageButton.Click();
        }
    }
}
