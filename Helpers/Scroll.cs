using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using POMSeleniumFrameworkPoc1.Pages.UI.RBSP_BP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace POMSeleniumFrameworkPoc1.Helpers
{
    class Scroll
    {
        // Scroll by a specific pixel amount inside an element
        public void ScrollElement(IWebElement element, int pixels)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)DriverHelper.Driver;
            js.ExecuteScript("arguments[0].scrollTop += arguments[1];", element, pixels);

        }

        // Scroll to the bottom of an element
        public void ScrollElementToBottom(IWebElement element)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)DriverHelper.Driver;
            js.ExecuteScript("arguments[0].scrollTop = arguments[0].scrollHeight;", element);
        }

        // Scroll until a specific child element is visible inside the container
        public void ScrollToChildElement(IWebElement container, IWebElement child)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)DriverHelper.Driver;
            js.ExecuteScript("arguments[0].scrollTop = arguments[1].offsetTop;", container, child);
        }

        public void ScrollIntoViewEle(IWebElement element)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)DriverHelper.Driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }


        public void scrollUntillAriaLabelEleVisible(String fieldName, int scrollPixel)
        {
            waitHelpers wh = new waitHelpers();
            CommonPage cp = new CommonPage();
            bool isReqEleDisplayed = false;
            By selector = SeleniumExtensions.getLocator($"[aria-label*='{fieldName}']");
            int counter = 0;
            int counter1 = 0;

            int eleSize = 0;
            IWebElement secEle = SeleniumExtensions.getwindowSectionEle();
            do
            {
                isReqEleDisplayed = wh.isElementDisplayed(selector, 5);
                if (isReqEleDisplayed)
                {

                    IWebElement element = wh.getElement(selector, 5);
                    ScrollIntoViewEle(element);
                    break;
                }
                else
                {
                    while (eleSize <= 0)
                    {
                        cp.waitTillPageLoading(60);
                        ScrollElement(secEle, scrollPixel);
                        eleSize = DriverHelper.Driver.FindElements(selector).Count;
                        if (counter1 == 20) break;
                        counter1 = counter1 + 1;

                    }
                }
                counter = counter + 1;
                if (counter == 4) break;
                isReqEleDisplayed = wh.isElementDisplayed(selector, 10);
            } while (!isReqEleDisplayed);
        }

        public void scrollUntillLookUpEleVisible(String fieldName,int scrollPixel)
        {
            waitHelpers wh = new waitHelpers();
            CommonPage cp = new CommonPage();
            bool isReqEleDisplayed = false;
            By selector = SeleniumExtensions.getLocator($"//input[contains(@aria-label,'{fieldName}') and (contains(@id,'Lookup'))]");
            int counter = 0;
            int counter1 = 0;

            int eleSize = 0;
            IWebElement secEle = SeleniumExtensions.getwindowSectionEle();
            do
            {
                isReqEleDisplayed = wh.isElementDisplayed(selector, 5);
                if (isReqEleDisplayed)
                {
                    cp.waitTillPageLoading(60);
                    IWebElement element = wh.getElement(selector, 5);
                    ScrollIntoViewEle(element);
                    break;
                }
                else
                {
                    while (eleSize <= 0)
                    {
                        cp.waitTillPageLoading(60);
                        ScrollElement(secEle, scrollPixel);
                        eleSize = DriverHelper.Driver.FindElements(selector).Count;
                        if (counter1 == 20) break;
                        counter1 = counter1 + 1;
                    }
                }
                counter = counter + 1;
                if (counter == 5) break;
                isReqEleDisplayed = wh.isElementDisplayed(selector, 10);
            } while (!isReqEleDisplayed);
        }
    }
}
