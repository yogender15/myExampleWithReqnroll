using OpenQA.Selenium;
using POMSeleniumFrameworkPoc1.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POMSeleniumFrameworkPoc1.Pages.UI.RBSP_BP
{
    public partial class BulkCreationOfJobsPage : BasePage
    {
        private By HereditamentLookUpFieldSelectorOnBulkCreationJobspageElement => By.CssSelector("input[aria-label='Select record, Multiple Selection Lookup']");
        private IWebElement HereditamentLookUpResultFirstValueOnBulkCreationJobspageElement => Driver.WaitForElement(By.XPath("//li[@data-id='MscrmControls.FieldControls.SimpleLookupControl-LookupResultsPopup_falseBoundLookup_resultsContainer'][1]"));
        private IWebElement CreateBulkJobsBtnUnderBulkCreationOfJobsTabElement => Driver.WaitForElement(By.XPath("//div[@aria-label='Create Bulk Jobs']"));
    }
}
