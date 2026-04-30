using OpenQA.Selenium;
using POMSeleniumFrameworkPoc1.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POMSeleniumFrameworkPoc1.Pages.UI.RBSP_BP
{
    public partial class BPFPage : BasePage
    {
        public void SelectHasTheDecisionLetterNoticeBeenCreatedDropDown(string dropDownDecision)
        {
            HasTheDecisionLetterNoticeBeenCreatedYesNoElement.SelectElementByText(dropDownDecision);
        }
        public void SelectCorrespondenceChecksCompleteYesNoDropDown(string dropDownDecision)
        {
            CorrespondenceChecksCompleteYesNoElement.SelectElementByText(dropDownDecision);
        }

        public void SelectSystemRequiresQualityAssuranceYesNoDropDown(string dropDownDecision)
        {
            SystemRequiresQualityAssuranceYesNoElement.SelectElementByText(dropDownDecision);
        }

        public bool VerifyFinishedBtnDisplayedAfterReleaseAndPublishBPF()
        {
            System.Threading.Thread.Sleep(8000);
            return FinishedAfterReleaseAndPublishBPFElement.Displayed;
        }


        public bool VerifyButtonNotVisible(string label)
        {
            try
            {
                var elements = Driver.FindElements(
                    By.CssSelector($"button[aria-label='{label}']"));

                bool result =
                    elements.Count == 0 || !elements.First().Displayed;

                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();

                return result;
            }
            catch (Exception e)
            {
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
                pdf_Util.exceptionPdFLogger(e);
                throw; // keep behavior consistent with failures elsewhere
            }
        }
        public void closeBPFcontainer()
        {
            try
            {
                waitHelpers wh = new waitHelpers();
                wh.clickOnElement(closeBtnBPFElementBy);
                //closeBtnBPFElement.Click();
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

        public void closeBPFcontainerIfStillOpen()
        {
            try
            {
                waitHelpers wh = new waitHelpers();
                if (wh.isElementDisplayed(closeBtnBPFElementBy,10))  
                wh.clickOnElement(closeBtnBPFElementBy);
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
