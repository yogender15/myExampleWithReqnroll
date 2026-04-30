using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace POMSeleniumFrameworkPoc1.Helpers
{
    class waitHelpers
    {
        int poolingIntervalTime = 1;
        WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(120));
        public IWebElement GetWebElement(By locator)
        {
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(120));

            wait.PollingInterval = TimeSpan.FromMilliseconds(poolingIntervalTime);
            IWebElement ele = null;
            try
            {
                ele = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));
            }
            catch (ElementClickInterceptedException e)
            {
                Thread.Sleep(3000);
                ele = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));
            }
            catch (StaleElementReferenceException e)
            {
                Thread.Sleep(3000);
                SeleniumExtensions.WaitForPageLoad();
                ele = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));
            }
            catch (Exception e)
            {
                Thread.Sleep(3000);
                ele = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));
            }
            return ele;
        }

        public IWebElement GetWebElementByTimeOut(By locator,int TimeOut)
        {
            WebDriverWait wait1 = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(TimeOut));
            wait1.PollingInterval = TimeSpan.FromMilliseconds(poolingIntervalTime);
            IWebElement ele = null;
            try
            {
                ele = wait1.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));
            }
            catch (Exception e)
            {
                Thread.Sleep(3000);
                ele = wait1.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));
            }
            return ele;
        }

        public String getElementText(By locator)
        {
            wait.PollingInterval = TimeSpan.FromSeconds(poolingIntervalTime);
            return GetWebElement(locator).Text.Trim();

        }

        public bool GetWebElement(By locator, int timeout)
        {
            try
            {
                WebDriverWait wait1 = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromMilliseconds(timeout));
                wait1.PollingInterval = TimeSpan.FromMilliseconds(1);
                return wait1.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator)).Displayed;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IWebElement getElement(By locator, int timeout)
        {
            WebDriverWait wait1 = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(timeout));
            wait1.PollingInterval = TimeSpan.FromMilliseconds(100);
            IWebElement ele = null;
            ele = wait1.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));
            return ele;
        }
        public IWebElement getElementWithTryCatch(By locator, int timeout)
        {
            IWebElement ele = null;
            WebDriverWait wait_new = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(timeout));
            wait_new.PollingInterval = TimeSpan.FromMilliseconds(100);

            try
            {
                ele = wait_new.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));
            }
            catch (ElementClickInterceptedException e)
            {
                Thread.Sleep(3000);
                ele = wait_new.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));
            }
            catch (StaleElementReferenceException e)
            {
                Thread.Sleep(3000);
                ele = wait_new.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));
            }
            catch (Exception e)
            {
                ele = null;
            }
            return ele;
        }

        public IWebElement getElementInMilliSecs(By locator, int timeout)
        {
            WebDriverWait wait1 = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromMilliseconds(timeout));
            IWebElement ele = null;
            ele = wait1.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));
            return ele;
        }

        public bool isElementDisplayed(By locator, int timeout)
        {
            bool isEleDisplayed = false;
            try
            {
                try
                {
                    IWebElement ele = getElementWithTryCatch(locator, timeout);
                    isEleDisplayed = ele.Displayed;
                }
                catch (NullReferenceException nre)
                {
                    isEleDisplayed = false;
                }
                return isEleDisplayed;
            }
            catch (ElementClickInterceptedException e)
            {
                try
                {
                    Thread.Sleep(3000);
                    IWebElement ele = getElementWithTryCatch(locator, timeout);
                    isEleDisplayed = ele.Displayed;
                }
                catch (NullReferenceException nre)
                {
                    isEleDisplayed = false;
                }
                return isEleDisplayed;
            }
            catch (StaleElementReferenceException e)
            {
                try
                {
                    Thread.Sleep(3000);
                    IWebElement ele = getElementWithTryCatch(locator, timeout);
                    isEleDisplayed = ele.Displayed;
                }
                catch (NullReferenceException nre)
                {
                    isEleDisplayed = false;
                }
                return isEleDisplayed;
            }
            catch (NullReferenceException e)
            {
                isEleDisplayed = false;
                return isEleDisplayed;
            }
            catch (Exception e)
            {
                isEleDisplayed = false;
                return isEleDisplayed;
            }
        }

        public bool isElementDisplayed(IWebElement ele, int timeout)
        {
            bool isEleDisplayed = false;
            DateTime endTime = DateTime.UtcNow.AddSeconds(timeout);
            while (DateTime.UtcNow < endTime)
            {
                try
                {
                    isEleDisplayed = ele.Displayed;
                    return isEleDisplayed;
                }
                catch (Exception e)
                {
                    isEleDisplayed = false;
                    Thread.Sleep(500);
                    return isEleDisplayed;
                }
                if (isEleDisplayed) break;
            }
            return isEleDisplayed;
        }


        public bool isElementDisplayedWithOutWait(By locator, int timeout)
        {
            bool isEleDisplayed = false;
            try
            {
                IWebElement ele = getElement(locator, timeout);
                isEleDisplayed = ele.Displayed;
                return isEleDisplayed;
            }
            catch (Exception e)
            {
                isEleDisplayed = false;
                return isEleDisplayed;
            }
        }

        public bool isElementDisplayedInMilliSec(By locator, int timeout)
        {
            bool isEleDisplayed = false;
            try
            {
                IWebElement ele = getElementInMilliSecs(locator, timeout);
                isEleDisplayed = ele.Displayed;
                return isEleDisplayed;
            }
            catch (Exception e)
            {
                isEleDisplayed = false;
                return isEleDisplayed;
            }
        }



        public IWebElement GetWebElementVisble(By locator)
        {
            WebDriverWait wait1 = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(90));
            wait1.PollingInterval = TimeSpan.FromMilliseconds(10);
            return wait1.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
        }

        public IWebElement GetWebElementVisble(By locator, WebDriverWait w)
        {
            w.PollingInterval = TimeSpan.FromMilliseconds(1);
            return w.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
        }

        public IWebElement elementToClickble(By locator)
        {
            wait.PollingInterval = TimeSpan.FromMilliseconds(2);
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
        }

        public IWebElement elementToClickble(IWebElement ele)
        {
            wait.PollingInterval = TimeSpan.FromMilliseconds(50);
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(ele));
        }

        public void clickOnElement(By locator)
        {
            try
            {
                wait.PollingInterval = TimeSpan.FromSeconds(7);
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator)).Click();
            }
            catch (ElementClickInterceptedException e)
            {
                Thread.Sleep(4000);
                wait.PollingInterval = TimeSpan.FromSeconds(5);
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator)).Click();
            }
            catch (ElementNotInteractableException e)
            {
                Thread.Sleep(4000);
                wait.PollingInterval = TimeSpan.FromSeconds(5);
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator)).Click();
            }
            catch (Exception e)
            {
                Thread.Sleep(4000);
                wait.PollingInterval = TimeSpan.FromSeconds(5);
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator)).Click();
            }
        }

        public void jsClickOnElement(By locator)
        {
            wait.PollingInterval = TimeSpan.FromMilliseconds(1);
            IWebElement ele = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
            var executor = ((IJavaScriptExecutor)DriverHelper.Driver);
            executor.ExecuteScript("arguments[0].scrollIntoView(true);", ele);
            executor.ExecuteScript("arguments[0].click();", ele);
        }

        public void jsClickOnElement(IWebElement ele)
        {
            wait.PollingInterval = TimeSpan.FromMilliseconds(1);
            var executor = ((IJavaScriptExecutor)DriverHelper.Driver);
            executor.ExecuteScript("arguments[0].scrollIntoView(true);", ele);
            executor.ExecuteScript("arguments[0].click();", ele);
        }

        public void jsClickOnElement(By locator, WebDriverWait wait1)
        {
            wait1.PollingInterval = TimeSpan.FromMilliseconds(1);
            IWebElement ele = wait1.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));
            var executor = ((IJavaScriptExecutor)DriverHelper.Driver);
            executor.ExecuteScript("arguments[0].scrollIntoView(true);", ele);
            executor.ExecuteScript("arguments[0].click();", ele);
        }

        public void clickOnWebElement(By locator)
        {
            try
            {
                GetWebElement(locator).Click();
            }
            catch (StaleElementReferenceException e)
            {
                Thread.Sleep(5000);
                GetWebElement(locator).Click();
            }
            catch (ElementClickInterceptedException e)
            {
                Thread.Sleep(5000);
                GetWebElement(locator).Click();
            }
            catch (ElementNotInteractableException e)
            {
                Thread.Sleep(5000);
                GetWebElement(locator).Click();
            }

        }
        public void clickOnWebElementIfDisplayed(By locator)
        {
            try
            {
                if(isElementDisplayed(locator,20))
                GetWebElement(locator).Click();
            }
            catch (StaleElementReferenceException e)
            {
                Thread.Sleep(5000);
                if (isElementDisplayed(locator, 20))
                    GetWebElement(locator).Click();
            }
            catch (ElementClickInterceptedException e)
            {
                Thread.Sleep(5000);
                if (isElementDisplayed(locator, 20))
                    GetWebElement(locator).Click();
            }
            catch (ElementNotInteractableException e)
            {
                Thread.Sleep(5000);
                if (isElementDisplayed(locator, 20))
                    GetWebElement(locator).Click();
            }

        }

        public void clickOnElement(By locator, WebDriverWait wait1)
        {
            wait1.PollingInterval = TimeSpan.FromMilliseconds(1);
            wait1.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator)).Click();
        }

        public void clickOnElement(IWebElement ele)
        {
            wait.PollingInterval = TimeSpan.FromMilliseconds(1);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(ele)).Click();
        }

        public void frameToBeAvailableAndSwitchToIt(String locator)
        {
            wait.PollingInterval = TimeSpan.FromMilliseconds(5);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.FrameToBeAvailableAndSwitchToIt(locator));
        }

        public void frameToBeAvailableAndSwitchToIt(By locator)
        {
            wait.PollingInterval = TimeSpan.FromMilliseconds(1);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.FrameToBeAvailableAndSwitchToIt(locator));
        }


        public bool waitTillElementInvisible(By locator)
        {
            wait.PollingInterval = TimeSpan.FromMilliseconds(1);
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(locator));
        }



        public bool waitTillElementInvisible(By locator, WebDriverWait wait)
        {
            wait.PollingInterval = TimeSpan.FromMilliseconds(1);
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(locator));
        }

        public bool elementClickableAndDisplayed(By locator)
        {
            wait.PollingInterval = TimeSpan.FromMilliseconds(1);
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator)).Displayed;
        }

        public bool elementDisplayed(By locator)
        {
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator)).Displayed;
        }

        public bool elementDisplayed(By locator, int timeout)
        {
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromMilliseconds(timeout));
            wait.PollingInterval = TimeSpan.FromMilliseconds(1);
            bool eleDisplayed = false;
            try
            {
                eleDisplayed = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator)).Displayed;
            }
            catch (Exception e)
            {
                eleDisplayed = false;
            }
            return eleDisplayed;
        }

        public bool elementDisplayed(IWebElement ele, int timeout)
        {
            WebDriverWait wait1 = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromMilliseconds(timeout));
            wait1.PollingInterval = TimeSpan.FromMilliseconds(1);
            bool eleDisplayed = false;
            try
            {
                eleDisplayed = ele.Displayed;
            }
            catch (StaleElementReferenceException se)
            {
                Thread.Sleep(3000);
                eleDisplayed = ele.Displayed;
            }
            catch (ElementClickInterceptedException se)
            {
                Thread.Sleep(3000);
                eleDisplayed = ele.Displayed;
            }
            catch (ElementNotInteractableException enie)
            {
                Thread.Sleep(3000);
                eleDisplayed = ele.Displayed;
            }

            catch (Exception e)
            {
                eleDisplayed = false;
            }
            return eleDisplayed;
        }
        public bool elementDisplays(By locator)
        {
            wait.PollingInterval = TimeSpan.FromMilliseconds(10);
            bool isdisplayed = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator)).Displayed;
            return isdisplayed;
        }

        public bool elementTextContains(By locator, String text)
        {
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            IWebElement ele = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElement(ele, text));
        }

        public bool elementLocateTextContains(By locator, String text)
        {
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementLocated(locator, text));
        }

        public bool elementValueTextContains(By locator, String text)
        {
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementValue(locator, text));
        }

        public IList<IWebElement> GetWebElements(By locator)
        {
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(locator));
        }

        public bool WaitTillElementInvisibleInSeconds(By locator, int timeout)
        {
            WebDriverWait wait1 = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(timeout));
            wait1.PollingInterval = TimeSpan.FromMilliseconds(5);
            return wait1.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(locator));
        }

        public IWebElement WaitForElementToBeClickble(By locator, int timeout)
        {
            WebDriverWait wait1 = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(timeout));
            wait1.PollingInterval = TimeSpan.FromMilliseconds(200);
            return wait1.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
        }

        public IWebElement WaitForElementToBeDisplayed(By locator, int timeout)
        {
            WebDriverWait wait1 = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(timeout));
            wait1.PollingInterval = TimeSpan.FromMilliseconds(1);
            bool isdisplayed = wait1.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator)).Displayed;
            return wait1.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));
        }

        public IWebElement WaitForElementToBeReady(By locator, int timeout)
        {

            WebDriverWait wait1 = new WebDriverWait(new SystemClock(), DriverHelper.Driver, TimeSpan.FromSeconds(timeout), TimeSpan.FromMilliseconds(500));
            try
            {
                wait1.Until(ExpectedConditions.ElementExists(locator));
                wait1.Until(ExpectedConditions.ElementIsVisible(locator));
                return wait1.Until(ExpectedConditions.ElementToBeClickable(locator));
            }
            catch (WebDriverTimeoutException ex)
            {
                throw new Exception($"Element with locator {locator} was not found or is  not interactable within the specified time.", ex);
            }
        }

        public bool WaitTillElementDisplayed(By locator, int timeout)
        {
            waitHelpers wh = new waitHelpers();
            //WebDriverWait wait = new WebDriverWait(new SystemClock(), DriverHelper.Driver, TimeSpan.FromMilliseconds(timeout), TimeSpan.FromMilliseconds(200));
            //wait.PollingInterval = TimeSpan.FromMilliseconds(1);
            //wait.Until(ExpectedConditions.ElementIsVisible(locator));
            //wait.Until(ExpectedConditions.ElementToBeClickable(locator));
            //return wait.Until(ExpectedConditions.ElementExists(locator)).Displayed;
            return wh.isElementDisplayed(locator, timeout);
        }
        public List<String> getAllWebElementsText(By locator)
        {
            List<String> allText = new List<string>();
            GetWebElement(locator);
            IList<IWebElement> eleLi = DriverHelper.Driver.FindElements(locator);
            foreach (var ele in eleLi)
            {
                allText.Add(ele.Text);
            }
            return allText;
        }


        public IList<IWebElement> getAllWebElements(By locator)
        {
            IList<IWebElement> eles;
            if (isElementDisplayed(locator, 120))
            {
                elementClickableAndDisplayed(locator);
                isElementDisplayed(locator, 60);
                eles = DriverHelper.Driver.FindElements(locator);
            }
            else
            {
                isElementDisplayed(locator, 60);
                eles = DriverHelper.Driver.FindElements(locator);
            }
            return eles;
        }

        public IList<IWebElement> getAllWebElements(By locator, int time)
        {
            GetWebElement(locator);
            elementClickableAndDisplayed(locator);
            isElementDisplayed(locator, time);
            IList<IWebElement> eles = DriverHelper.Driver.FindElements(locator);
            return eles;
        }

        public void WaitForSeconds(int seconds)
        {
            if (seconds < 0)
                throw new ArgumentOutOfRangeException(nameof(seconds), "Seconds cannot be negative.");
            Thread.Sleep(seconds * 1000);
        }

        public bool waitTillTitle(string title, int timeout)
        {
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(timeout));
            wait.PollingInterval = TimeSpan.FromMilliseconds(5);
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleContains(title));
        }

        public bool clickOnElementUntilElementDisappear(By clickElemet,By disappearElement)
        {
            try
            {
                GetWebElement(disappearElement);
            }
            catch (Exception ex)
            {
            }

            return false;
        }



    }
}
