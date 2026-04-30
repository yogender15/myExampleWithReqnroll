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
        public void FillHereditamentDetailsUnderBulkCreationJobspage(string hereditament)
        {
            try
            {
                ScrollDiv.ScrollUntilSelectorDisplayedAndSendKeys(HereditamentLookUpFieldSelectorOnBulkCreationJobspageElement, hereditament, 10);
                HereditamentLookUpResultFirstValueOnBulkCreationJobspageElement.ClickUntilNoClickInterruptable();
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
            }
            catch (Exception e)
            {
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
                pdf_Util.exceptionPdFLogger(e);
            }
        }

        public void ClickCreateBulkJobsBtnUnderBulkCreationOfJobsTab()
        {
            try
            {
                CreateBulkJobsBtnUnderBulkCreationOfJobsTabElement.Click();
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
            }
            catch (Exception e)
            {
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
                pdf_Util.exceptionPdFLogger(e);
            }
        }

    }
}
