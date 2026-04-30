using NUnit.Framework;
using OpenQA.Selenium;
using POMSeleniumFrameworkPoc1.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace POMSeleniumFrameworkPoc1.Pages.UI.RBSP_BP
{
    public partial class PADEntryPage : BasePage
    {

        public void ClickOnChevronOnPVTTab(string ChevronName)
        {
            ValidateAndClickOnPVTTab("PADS");
            ChevronRightMed.ClickUsingJavascript();
            StatusCircleCheckmark.ClickUsingJavascript();

        }

        public void ValidateAndClickOnTabInPVT(string tabName, String popUpName)
        {
            By AmendTab = GetPADTab(tabName);
            Assert.IsTrue(DriverHelper.Driver.FindElement(AmendTab).ElementVisisbleUsingExplicitWait(6));

            IWebElement tabToNavigate = FindByText(tabName);
            Assert.IsTrue(tabToNavigate.WaitUntilElementAttached(5));
            tabToNavigate.ClickUsingJavascript();
            //Thread.Sleep(2000);

            IWebElement clickOnPopUp = FindByText(popUpName);
            Assert.IsTrue(clickOnPopUp.WaitUntilElementAttached(5));
            clickOnPopUp.ClickUsingJavascript();
            //Thread.Sleep(8000);
            //SaveAndContinue.ClickUsingJavascript();
            //Thread.Sleep(13000);
        }
        public void SelectNDRAssesmentValue(String value)
        {

            int maxretries = 20;
            int retrydelay = 8000;
            for (int retry = 0; retry < maxretries; retry++)
            {
                try
                {
                    if (NDRAssessmentPopUp.IsElementDisplayed(3))
                    {
                        IWebElement GetNDRoption = GetNDRValue(value);
                        Assert.IsTrue(GetNDRoption.WaitUntilElementAttached(5));
                        GetNDRoption.ClickUsingJavascript();
                        clickOnMainMenuMoreElement_New("Save");
                        break;

                    }
                    else
                    {
                        //clickOnMainMenuMoreElement_New("Save");
                        //Thread.Sleep(2000);
                        ClickCommandBarOption("Refresh");

                    }
                }
                catch (NoSuchElementException)
                {
                    Console.WriteLine(" NRD is not displayed after retrying");
                }
                Thread.Sleep(retrydelay);
            }
            //clickOnMainMenuMoreElement_New("Save & Close");
        }

        public String getEffectiveFromDate()
        {
            return new waitHelpers().getElementText(effectiveFromDate);
        }
    }

}
