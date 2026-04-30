
using Npgsql;
using NUnit.Framework;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using POMSeleniumFrameworkPoc1.Pages.UI;
using POMSeleniumFrameworkPoc1.Pages.UI.CommonComponents;
using POMSeleniumFrameworkPoc1.Pages.UI.RBSP_BP;
using Reqnroll;
using SeleniumExtras.WaitHelpers;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Log = Serilog.Log;


namespace POMSeleniumFrameworkPoc1.Helpers
{
    public static class SeleniumExtensions
    {

        static By gridRootSec = By.CssSelector("div[data - id = 'GridRoot'] > div[role = 'presentation']");
        static By tabSec = By.CssSelector("[id *= 'tab-section']");
        static By dashBoardSec = By.CssSelector("[id='DashboardScrollView']");
        public static void SelectElementByText(this IWebElement element, string text)
        {
            try
            {
                Thread.Sleep(3000);
                new SelectElement(element).SelectByText(text);
                Log.Information($"The element  is selected with text {text}");

            }
            catch (Exception e)
            {
                Log.Error($"The error was thrown while selecting {text} for element  with message {e}");
                throw e;
            }
        }

        public static string buildaddress(string buildingNumber, string street, string locality, string town, string postcode)
        {
            var addressparts = new List<string>
            {
                buildingNumber,
                street,locality,town,postcode

            };
            return string.Join(", ", addressparts.Where(part => !string.IsNullOrWhiteSpace(part)).Select(part => part.Trim()));

        }

        public static void SelectElementByText_BARef(this IWebElement element, string text)
        {
            try
            {
                Thread.Sleep(3000);

                var selectElement = new SelectElement(element);
                var options = selectElement.Options;
                Assert.That(options.Count, Is.EqualTo(1),
                            $"Expected exactly one option in the dropdown, but found {options.Count}");
                new SelectElement(element).SelectByText(text);
                Log.Information($"The element  is selected with text {text}");

            }
            catch (Exception e)
            {
                Log.Error($"The error was thrown while selecting {text} for element  with message {e}");
                throw e;
            }
        }

        public static IWebElement GetElementWithSelector(this By selector)
        {
            waitHelpers wh = new waitHelpers();
            try
            {
                Actions act = new Actions(DriverHelper.Driver);
                act.MoveToElement(wh.getElement(selector, 60)).Build().Perform();
                IWebElement element = wh.GetWebElement(selector);
                return element;


            }
            catch (Exception e)
            {
                return null;
            }
        }


        public static void SelectElementByValue(this IWebElement element, string value)
        {
            try
            {
                new SelectElement(element).SelectByText(value);
                Log.Information($"The element  is selected with text {value}");

            }
            catch (Exception e)
            {
                Log.Error($"The error was thrown while selecting {value} for element  with message {e}");
                throw e;
            }
        }

        public static void ScrollAndClick(this IWebElement element)
        {
            var executor = ((IJavaScriptExecutor)DriverHelper.Driver);
            executor.ExecuteScript("arguments[0].scrollIntoView(true);", element);
            executor.ExecuteScript("arguments[0].click();", element);
            Log.Information($"Scrolled to  and clicked");

        }

        public static void ScrollUntilSelectorDisplayed(this IWebElement divElement, By selector, int timeout)
        {
            var executor = ((IJavaScriptExecutor)DriverHelper.Driver);
            int waited = 0;

            //DriverHelper.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(1));

            while (timeout > 0 && !(waited >= timeout))
            {

                try
                {

                    if (DriverHelper.Driver.FindElement(selector).Displayed)
                    {
                        Log.Information($"The element with selector {selector} is displayed afte scroll");

                        //DriverHelper.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(Config.DefaultDriverTimeOut));
                        return;
                    }

                }
                catch
                {
                    Thread.Sleep(1000);
                    executor.ExecuteScript("arguments[0].scrollBy(0,90);", divElement);
                }

                waited += 1;

            }
            //DriverHelper.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(Config.DefaultDriverTimeOut));

        }

        public static void ScrollUntilSelectorDisplayedAndSendKeys(this IWebElement divElement, By selector, string text, int timeout)
        {
            var executor = ((IJavaScriptExecutor)DriverHelper.Driver);
            int waited = 0;
            //DriverHelper.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(0.5));

            while (timeout > 0 && !(waited >= timeout))
            {
                try
                {
                    if (DriverHelper.Driver.FindElement(selector).Displayed)
                    {
                        if (DriverHelper.Driver.FindElement(selector).GetAttribute("value").Equals(text))
                        {
                            //DriverHelper.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(Config.DefaultDriverTimeOut));
                            return;
                        }
                        DriverHelper.Driver.FindElement(selector).Clear();
                        DriverHelper.Driver.FindElement(selector).ClickUsingJavascript();
                        DriverHelper.Driver.FindElement(selector).SendKeys(Keys.Control + "A");
                        DriverHelper.Driver.FindElement(selector).SendKeys(Keys.Backspace);
                        DriverHelper.Driver.FindElement(selector).SendKeys(text);
                        Log.Information($"The element with selector {selector} is displayed afte scroll and {text} is enetered");


                        if (DriverHelper.Driver.FindElement(selector).GetAttribute("value").Equals(text))
                        {

                            DriverHelper.Driver.WaitForElements(By.CssSelector("[data-id*='Lookup'] ul li span[id*='name']"), 1000).Where(t => t.Text.Contains(text)).FirstOrDefault().IsElementDisplayed(1);
                            //DriverHelper.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(Config.DefaultDriverTimeOut));
                            return;
                        }
                    }
                    else
                    {
                        executor.ExecuteScript("arguments[0].scrollBy(0,90);", divElement);

                    }
                }
                catch
                {
                    try
                    {
                        executor.ExecuteScript("arguments[0].scrollBy(0,90);", divElement);
                    }
                    catch
                    {
                        Log.Error($"The element with selector {selector} is not displayed afte scroll");

                    }
                }
                waited += 1;
            }
            //DriverHelper.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(Config.DefaultDriverTimeOut));
        }


        public static void ScrollUntilSelectorDisplayedAndSendKeysIntoDatePicker(this IWebElement divElement, By selector, string text, int timeout)
        {
            var executor = ((IJavaScriptExecutor)DriverHelper.Driver);
            int waited = 0;
            //DriverHelper.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(1));

            while (timeout > 0 && !(waited >= timeout))
            {
                try
                {
                    if (DriverHelper.Driver.FindElement(selector).Displayed)
                    {
                        if (DriverHelper.Driver.FindElement(selector).GetAttribute("value").Equals(text))
                        {
                            //DriverHelper.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(Config.DefaultDriverTimeOut));
                            return;
                        }
                        //DriverHelper.Driver.FindElement(selector).Clear();
                        //DriverHelper.Driver.FindElement(selector).Click();
                        //DriverHelper.Driver.FindElement(selector).SendKeys(Keys.Backspace);
                        DriverHelper.Driver.FindElement(selector).SendKeys(text);
                        DriverHelper.Driver.FindElement(selector).SendKeys(Keys.Alt);
                        if (DriverHelper.Driver.FindElement(selector).GetAttribute("value").Equals(text))
                        {
                            //DriverHelper.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(Config.DefaultDriverTimeOut));
                            return;
                        }
                    }
                    else
                    {
                        executor.ExecuteScript("arguments[0].scrollBy(0,90);", divElement);
                    }
                }
                catch
                {
                    Thread.Sleep(1000);
                    try
                    {
                        executor.ExecuteScript("arguments[0].scrollBy(0,90);", divElement);
                    }
                    catch { }
                }
                waited += 1;
            }
            //DriverHelper.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(Config.DefaultDriverTimeOut));
        }

        public static void ScrollAndClick(this IWebElement divElement, By selector, int timeout)
        {
            var executor = ((IJavaScriptExecutor)DriverHelper.Driver);
            int waited = 0;
            //DriverHelper.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(1));

            while (timeout > 0 && !(waited >= timeout))
            {
                try
                {
                    if (DriverHelper.Driver.FindElement(selector).Displayed)
                    {
                        //DriverHelper.Driver.FindElement(selector).Clear();
                        DriverHelper.Driver.FindElement(selector).ClickUsingJavascript();
                        Log.Information($"The element with selector {selector} is displayed afte scroll");

                        return;
                    }
                    else
                    {
                        executor.ExecuteScript("arguments[0].scrollBy(0,90);", divElement);
                    }
                }
                catch
                {
                    Thread.Sleep(1000);
                    try
                    {
                        executor.ExecuteScript("arguments[0].scrollBy(0,90);", divElement);
                    }
                    catch
                    {
                        Log.Error($"The element with selector {selector} is not displayed afte scroll");

                    }
                }
                waited += 1;
            }
            //DriverHelper.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(Config.DefaultDriverTimeOut));
        }
        // *//button[@aria-label="9, January, 2023"]/parent::*[@data-is-focusable="true"]

        public static void SetTheDateForTheTableCalendar(string date, int turns)
        {

            var selectorXpath = By.XPath($"*//button[@aria-label='{date}']/parent::*[@data-is-focusable=\"true\"]");
            // var turns = 1;
            while (turns >= 0)
            {
                try
                {
                    Thread.Sleep(1000);
                    if (DriverHelper.Driver.FindElement(selectorXpath).Displayed)
                    {
                        Thread.Sleep(1000);
                        DriverHelper.Driver.FindElement(selectorXpath).ClickUsingJavascript();
                        Log.Information($"The element with selector {selectorXpath} is displayed afte clicked");

                        return;
                    }
                    else
                    {
                        Thread.Sleep(1000);
                        DriverHelper.Driver.FindElement(By.CssSelector("input[aria-label='Proposed Effective Date']")).ClickUsingJavascript();
                        turns -= 1;
                    }
                }
                //  }
                catch
                {
                    Thread.Sleep(1000);
                    DriverHelper.Driver.FindElement(By.CssSelector("input[aria-label='Proposed Effective Date']")).ClickUsingJavascript();

                    turns -= 1;
                }

            }
        }

        public static void SetTheDateForTheTableCalendar_New(string date, int turns)
        {

            var selectorXpath = By.XPath($"*//button[@aria-label='{date}']");
            // var turns = 1;
            while (turns >= 0)
            {
                try
                {
                    Thread.Sleep(1000);
                    if (DriverHelper.Driver.FindElement(selectorXpath).Displayed)
                    {
                        Thread.Sleep(1000);
                        DriverHelper.Driver.FindElement(selectorXpath).ClickUsingJavascript();
                        Log.Information($"The element with selector {selectorXpath} is displayed afte clicked");

                        return;
                    }
                    else
                    {
                        Thread.Sleep(1000);
                        DriverHelper.Driver.FindElement(By.CssSelector("input[aria-label='Proposed Effective Date']")).ClickUsingJavascript();
                        turns -= 1;
                    }
                }
                //  }
                catch
                {
                    Thread.Sleep(1000);
                    DriverHelper.Driver.FindElement(By.CssSelector("input[aria-label='Proposed Effective Date']")).ClickUsingJavascript();

                    turns -= 1;
                }

            }
        }

        //   DriverHelper.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(Config.DefaultDriverTimeOut));



        public static void SendKeysIntoLookUp(By selector, string text)
        {
            DriverHelper.Driver.FindElement(selector).SendKeys(Keys.Backspace);
            DriverHelper.Driver.FindElement(selector).SendKeys(Keys.Backspace);
            DriverHelper.Driver.FindElement(selector).SendKeys(Keys.Backspace);
            DriverHelper.Driver.FindElement(selector).SendKeys(text);
            Log.Information($"The text {text} was sent to element with selector {selector}");
        }

        public static void WaitForElementToDisplayProperly(this IWebDriver driver, By elementSelector, int turns = 5)
        {
            int i = 0;
            bool isDisplayed = false;
            while (!isDisplayed && i <= turns)
            {
                try
                {
                    isDisplayed = driver.WaitForElement(elementSelector).Displayed;
                    Log.Information($"The boolean value is {isDisplayed} for the element with {elementSelector}");

                    Thread.Sleep(2000);
                    i += 1;
                }
                catch
                {
                    Log.Error($"The boolean value is {isDisplayed} for the element with {elementSelector}");
                }
            }
        }

        public static List<IWebElement> WaitForElements(this IWebDriver driver, By elementSelector, long timeOutInMilliSeconds = 30000)
        {
            //DriverHelper.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(Config.DefaultDriverTimeOut));
            long waited = 0;

            while (!(waited >= timeOutInMilliSeconds))
            {
                try
                {
                    Thread.Sleep(1000);
                    var ele = driver.FindElements(elementSelector);
                    Log.Information($"The element {ele} with {elementSelector}");

                    return ele.ToList();
                }
                catch
                {

                }
                waited += 1000;
            }
            Log.Error($"The element with {elementSelector} is not displayed yet");
            throw new Exception($"the element with selector {elementSelector} is not displayed yet");
        }

        public static void WaitForElementToDissapear(this IWebDriver driver, By elementSelector, long timeOutInMilliSeconds = 3000)
        {
            long waited = 0;
            //DriverHelper.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(1));

            while (!(waited >= timeOutInMilliSeconds))
            {
                try
                {
                    Thread.Sleep(3000);
                    var ele = driver.FindElement(elementSelector);
                }
                catch
                {
                    //DriverHelper.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(Config.DefaultDriverTimeOut));
                    return;
                }
                waited += 1000;
            }
            //DriverHelper.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(Config.DefaultDriverTimeOut));
            throw new Exception($"the element with selector {elementSelector} is not displayed yet");
        }

        public static void ClickUntilNoClickInterruptable(this IWebElement element, long timeOutInMilliSeconds = 3000)
        {
            long waited = 0;
            //DriverHelper.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(1));

            while (!(waited >= timeOutInMilliSeconds))
            {
                try
                {
                    Thread.Sleep(3000);
                    element.ClickUsingJavascript();
                    //   Log.Information($"The element  was clicked without interruption");
                    return;
                }
                catch (Exception e)
                {
                }
                waited += 1000;
            }
            //DriverHelper.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(Config.DefaultDriverTimeOut));
            //   Log.Error($"The element  is not clickable");
            throw new Exception($"the element is not clickable");
        }

        public static void ClickUntilNoClickInterruptableWithSelector(this IWebElement element, By selector, long timeOutInMilliSeconds = 3000)
        {
            long waited = 0;
            //DriverHelper.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(1));

            while (!(waited >= timeOutInMilliSeconds))
            {
                try
                {
                    Thread.Sleep(3000);
                    element.ClickUsingJavascript();
                    return;
                }
                catch (Exception e)
                {
                    element = DriverHelper.Driver.WaitForElement(selector);
                }
                waited += 1000;
            }
            //DriverHelper.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(Config.DefaultDriverTimeOut));
            throw new Exception($"the element is not clickable");
        }

        public static void ClickUntilDisappear(this IWebElement element, long timeOutInMilliSeconds = 3000)
        {
            long waited = 0;
            // DriverHelper.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(1));

            while (!(waited >= timeOutInMilliSeconds) && (element.IsElementDisplayed(2)))
            {
                try
                {
                    Thread.Sleep(3000);
                    element.ClickUsingJavascript();
                    return;
                }
                catch (Exception e)
                {
                }
                waited += 1000;
            }
            //DriverHelper.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(Config.DefaultDriverTimeOut));
            throw new Exception($"the element is not clickable");
        }

        public static IWebElement WaitForElement(this IWebDriver driver, By elementSelector, long timeOut = 3000)
        {
            return driver.WaitForElements(elementSelector, timeOut).FirstOrDefault();
        }

        public static void MoveToElement(this IWebElement element)
        {
            try
            {
                Actions actions = new Actions(DriverHelper.Driver);
                actions.MoveToElement(element).Click().Build().Perform();
                Log.Information($"The element  was displayed and clicked");
            }
            catch (Exception e)
            {
                Log.Error($"The element  was not displayed and the error was thrown {e.Message}");
                throw e;
            }
        }

        public static void WaitUntilElementHasValue(this IWebDriver driver, By elementSelector, string value, bool condition, int timeout)
        {
            int waited = 0;
            var elementValue = driver.FindElement(elementSelector).GetAttribute("value");

            while (elementValue.ToLower().Equals(value.ToLower()) != condition)
            {
                if (timeout > 0 && waited >= timeout)
                {
                    return;
                }
                try
                {
                    Thread.Sleep(500);
                    elementValue = driver.FindElement(elementSelector).GetAttribute("value");
                    Log.Information($"The element with selector {elementSelector} has value {elementValue} which is equal to {value}");

                }
                catch (Exception e)
                {
                    Log.Error($"The element with selector {elementSelector} has value {elementValue} which is not equal to {value}");

                }
                waited += 500;
            }
        }

        public static void ClickUsingJavascript(this IWebElement element)
        {
            try
            {
                var executor = (IJavaScriptExecutor)DriverHelper.Driver;
                executor.ExecuteScript("arguments[0].click();", element);
                Log.Information($"User clicked on element ");
            }
            catch (JavaScriptException e)
            {
                Thread.Sleep(3000);
                waitHelpers wh = new waitHelpers();
                wh.elementToClickble(element);
                var executor = (IJavaScriptExecutor)DriverHelper.Driver;
                executor.ExecuteScript("arguments[0].click();", element);
                Log.Information($"User clicked on element ");
            }

        }

        public static void RefreshPage()
        {
            DriverHelper.Driver.Navigate().Refresh();
            Log.Information($"The page was refreshed");


        }

        public static void ScrollUntilElementIsDisplayed(this IWebElement div, By selector, int timeout)
        {
            var executor = ((IJavaScriptExecutor)DriverHelper.Driver);
            int waited = 0;
            //DriverHelper.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(1));

            while (timeout > 0 && !(waited >= timeout))
            {
                try
                {
                    if (DriverHelper.Driver.FindElement(selector).Displayed)
                    {
                        Log.Information($"The element with {selector} was displayed");
                        //DriverHelper.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(Config.DefaultDriverTimeOut)); ;
                        return;
                    }
                }
                catch (Exception e)
                {
                    executor.ExecuteScript("arguments[0].scrollBy(0,90);", div);
                }
                waited += 1;
            }
            //DriverHelper.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Config.DefaultDriverTimeOut);
        }

        public static IWebElement WaitUntilElementWithSelectorIsDisplayed(this IWebDriver driver, By elementSelector, long timeOutInMilliSeconds)
        {
            long waited = 0;

            while (!(waited >= timeOutInMilliSeconds))
            {
                try
                {
                    Thread.Sleep(1000);
                    driver.FindElements(elementSelector);
                    Log.Information($"The element with {elementSelector} was displayed");
                    return driver.FindElements(elementSelector).First();
                }
                catch (Exception e)
                {
                }
                waited += 1000;
            }
            Log.Information($"The element with {elementSelector} displayed {null} value");

            return null;
        }

        public static void WaitUntilElementSelectorIsDisappeared(this IWebElement element, long timeOutInMilliSeconds)
        {
            long waited = 0;

            while (!(waited >= timeOutInMilliSeconds))
            {
                try
                {
                    Thread.Sleep(1000);
                    if (!element.Displayed)
                        return;
                }
                catch (Exception e)
                {
                    if (!element.Displayed)
                        return;
                }
                waited += 1000;
            }

        }

        public static bool IsElementDisplayed(this IWebElement element, int timeOutInSeconds)
        {
            //DriverHelper.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeOutInSeconds);

            try
            {
                //  IWebElement currElement = DriverHelper.Driver.FindElement(element);
                var isDisplayed = element.Displayed;
                //DriverHelper.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Config.DefaultDriverTimeOut);
                Log.Information($"The element  is displayed");
                return isDisplayed;
            }
            catch (Exception e)
            {
                //DriverHelper.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Config.DefaultDriverTimeOut);
                Log.Error($"The element  was not displayed");

                return false;
            }
        }

        public static void DoubleClickElement(this IWebElement element)
        {
            Actions actions = new Actions(DriverHelper.Driver);
            actions.DoubleClick(element).Perform();
            Log.Information($"User double clicked on element ");
        }
        public static void SwitchToIframe(By selectorFrame)
        {
            DriverHelper.Driver.SwitchTo().Frame(DriverHelper.Driver.WaitForElement(selectorFrame));
            Log.Information($"User switched to iframe {selectorFrame}");
        }
        public static void SwitchToDefaultFrame()
        {
            DriverHelper.Driver.SwitchTo().DefaultContent();
            Log.Information($"User switched to default frame");
        }

        public static void SwitchWindow()
        {
            DriverHelper.Driver.SwitchTo().Window(DriverHelper.Driver.WindowHandles.Last());
            Log.Information($"User switched to last window in window handles");
        }
        public static void SwitchWindow(string title)
        {
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(5000));
            wait.Until(d => d.WindowHandles.Count > 1);

            IReadOnlyCollection<string> windowhandles = DriverHelper.Driver.WindowHandles;
            Console.WriteLine(windowhandles.Count);
            bool switchedToReqWindow = false;
            foreach (var window in windowhandles)
            {
                WaitForPageLoad(3000,30);
                DriverHelper.Driver.SwitchTo().Window(window);
                WaitForPageLoad(3000, 30);
                Console.WriteLine(DriverHelper.Driver.Title);
                if (DriverHelper.Driver.Title.Contains(title))
                {
                    switchedToReqWindow = true;
                    break;
                }

            }
            if (!switchedToReqWindow)
            {
                throw new Exception($"User unable to switch to required window with title : { title }");
            }
        }

        public static bool ValidateTextInList(By listElements, string attributeName, string textValue)
        {
            bool Flag = false;
            Thread.Sleep(3000);
            IList<IWebElement> listEle = DriverHelper.Driver.FindElements(listElements);


            foreach (var eachVal in listEle)
            {
                string tabTitle = eachVal.GetAttribute(attributeName);
                if (tabTitle == textValue)
                {
                    Log.Information($"User validated {textValue} in list {listEle} with {tabTitle}");
                    Flag = true;
                    break;
                }

            }

            return Flag;

        }


        public static void EnterInLookUpField(By lookUpFirstValue, By lookUpLocator, By SearchButton, string valueToEnter)
        {
            //  DriverHelper.Driver.WaitForElement(By.CssSelector("[id*='tab-section']")).ScrollUntilSelectorDisplayedAndSendKeys(lookUpLocator,
            //  valueToEnter, 10);
            waitHelpers wh = new waitHelpers();
            wh.GetWebElement(SearchButton).ClickUsingJavascript();
            wh.GetWebElement(By.CssSelector("[id*='tab-section']")).ScrollUntilSelectorDisplayedAndSendKeys(lookUpLocator, valueToEnter, 20);
            wh.GetWebElement(SearchButton).ClickUsingJavascript();
            lookUpFirstValue.IsElementVisibleUsingByLocator(3);
            // Thread.Sleep(2000);
            wh.GetWebElement(lookUpFirstValue).ClickUsingJavascript();
            Log.Information($"User entered {valueToEnter} from  lookup selector {lookUpLocator}");

        }

        public static void EnterInLookUpField(By lookUpFirstValue, By lookUpLocator, string valueToEnter)
        {
            DriverHelper.Driver.WaitForElement(By.CssSelector("[id*='tab-section']")).ScrollUntilSelectorDisplayedAndSendKeys(lookUpLocator,
            valueToEnter, 10);
            Thread.Sleep(2000);
            DriverHelper.Driver.WaitForElement(lookUpFirstValue).ClickUsingJavascript();
            Log.Information($"User entered {valueToEnter} from  lookup selector {lookUpLocator}");


        }

        public static bool IsElementVisible(this IWebElement element)
        {
            try
            {

                return element.Displayed;
            }
            catch (NoSuchElementException e)
            {
                return false;
            }

        }

        public static bool ElementVisisbleUsingExplicitWait(this IWebElement element, int timeout)
        {
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(timeout));
            try
            {

                wait.Until(d =>
                {
                    try
                    {

                        var isDisplayed = element.Displayed;
                        Log.Information($"The element  displayed is {isDisplayed}");
                        return isDisplayed;
                    }
                    catch (NoSuchElementException)
                    {
                        Log.Error($"The element  is not displayed with error `No such element`");
                        return false;
                    }
                    catch (StaleElementReferenceException)
                    {
                        Log.Error($"The element  is not displayed with error `Stale element`");
                        return false;
                    }
                    catch (NullReferenceException)
                    {
                        Log.Error($"The element  is not displayed with error `Null Reference`");
                        return false;
                    }

                });
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                Log.Error($"The element  is not displayed with error `Webdriver Exception`");
                return false;
            }

        }

        public static bool IsElementVisibleUsingByLocator(this By locator, int timeout)
        {
            IWebElement element = null;
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(timeout));
            try
            {

                wait.Until(d =>
                {
                    try
                    {
                        element = DriverHelper.Driver.FindElement(locator);
                        var isDisplayed = element.Displayed;
                        Log.Information($"The element  displayed is {isDisplayed}");
                        return isDisplayed;
                    }
                    catch (NoSuchElementException)
                    {
                        Log.Error($"The element  is not displayed with error `No such element`");
                        return false;
                    }
                    catch (StaleElementReferenceException)
                    {
                        Log.Error($"The element  is not displayed with error `Stale element`");
                        return false;
                    }
                    catch (NullReferenceException)
                    {
                        Log.Error($"The element  is not displayed with error `Null Reference`");
                        return false;
                    }

                });
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                Log.Error($"The element  is not displayed with error `Webdriver Exception`");
                return false;
            }

        }

        public static void WaitForElementToBeClickable(this By selector, int timeout)
        {
            bool flag;
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(timeout));
            var element = wait.Until(d => d.FindElement(selector));
            wait.Until(d =>
            {
                try
                {
                    element.ClickUsingJavascript();
                    flag = true;
                    return flag;
                }
                catch
                {
                    flag = false;
                    return flag;
                }

            });


        }


        public static void ClearAndEnterValueIntoTextBox(this IWebElement element, string text)
        {

            var executor = ((IJavaScriptExecutor)DriverHelper.Driver);
            executor.ExecuteScript("arguments[0].scrollIntoView(true);", element);
            element.ClickUsingJavascript();
            element.SendKeys(Keys.Clear);
            do
            {
                element.SendKeys(Keys.Backspace);
            } while (element.GetAttribute("value").Contains("-"));


            element.SendKeys(text);
            Log.Information($"The value {text} was entered for  element");

        }

        // The input field has the value as '---' by default where in selenium Clear() doesn't work so the following method is helpful
        public static void ClearAndSendkeys(this IWebElement element, string text)
        {

            element.ClickUsingJavascript();
            element.SendKeys(Keys.Control + "A");
            element.SendKeys(Keys.Backspace);
            element.SendKeys(text);
            element.SendKeys(Keys.Enter);
            Log.Information($"The value {text} was entered for  element");


        }


        public static void SelectToggle(IWebElement toggleTextEle, IWebElement toggleElement, string toggleValue)
        {

            string actValue = toggleTextEle.Text;
            if (actValue != toggleValue)
            {
                toggleElement.ScrollAndClick();
                Log.Information($"The toggle value {toggleValue} for toggle element  {toggleTextEle}");
            }
            else
            {
                Log.Information("The value is already set to " + toggleValue);

            }
        }

        public static void PressEnter(this IWebElement element)
        {
            try
            {
                Actions actions = new Actions(DriverHelper.Driver);
                actions.SendKeys(element, Keys.Enter).Build().Perform();
                Log.Information($"User pressed enter for element ");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void HighlightElement(IWebElement element)
        {
            var executor = ((IJavaScriptExecutor)DriverHelper.Driver);
            executor.ExecuteScript("arguments[0].setAttribute('style','border:2px solid red;background: yellow;');", element);
            Thread.Sleep(4000);
            executor.ExecuteScript("arguments[0].setAttribute('style',arguments[1]);", element, "");
            Log.Information($"User highlighted element ");

        }
        public static string GetLatestRowByDate(By rowsElement, string columnName)
        {
            IWebElement dateElement = null;
            Thread.Sleep(3000);
            IList<IWebElement> rows = DriverHelper.Driver.FindElements(rowsElement);

            Dictionary<DateTime, IWebElement> dateRowMapping = new Dictionary<DateTime, IWebElement>();
            waitHelpers wh = new waitHelpers();
            dateRowMapping.Clear();
            foreach (var row in rows)
            {
                // rows = DriverHelper.Driver.FindElements(rowsElement);
                //  HighlightElement(row);
                //   row.ClickUsingJavascript();
                string rowindex = row.GetAttribute("row-index");
                Console.WriteLine(rowindex);
                IWebElement activeRow = wh.GetWebElement(By.XPath($"//*[contains(@class,'ag-body-viewport')]//*[contains(@class,'ag-center-cols-container')]//*[contains(@class,'ag-row-position-absolute') and @row-index='{rowindex}']"));
                if (columnName == "CreatedOn")
                {
                    dateElement = wh.GetWebElement(By.XPath($"//*[contains(@class,'ag-body-viewport')]//*[contains(@class,'ag-center-cols-container')]//*[contains(@class,'ag-row-position-absolute') and @row-index='{rowindex}']//*[contains(@class,'ag-cell-value') and @col-id='createdon']//label/div[text()]"));
                }
                else if (columnName == "Actual End")
                {
                    dateElement = wh.GetWebElement(By.XPath($"//*[contains(@class,'ag-body-viewport')]//*[contains(@class,'ag-center-cols-container')]//*[contains(@class,'ag-row-position-absolute') and @row-index='{rowindex}']//*[contains(@class,'ag-cell-value') and @col-id='actualend']//label/div[text()]"));
                }


                //   HighlightElement(dateElement);
                string CreatedOnTextValue = dateElement.Text.Trim();
                // Thread.Sleep(2000);
                rows = DriverHelper.Driver.FindElements(rowsElement);

                if (DateTime.TryParseExact(CreatedOnTextValue, "dd/MM/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime date))
                {
                    Console.WriteLine(date);
                    if (!dateRowMapping.ContainsKey(date))
                    {
                        dateRowMapping.Add(date, activeRow);
                    }
                }
            }
            DateTime latestDate = dateRowMapping.Keys.Max();
            Console.WriteLine(dateRowMapping[latestDate]);
            string latestdateTimeValue = latestDate.ToString("dd/MM/yyyy HH:mm");
            Log.Information($"The lastest dateTime value is {latestdateTimeValue}");
            Console.WriteLine(latestdateTimeValue);

            return latestdateTimeValue;

        }

        public static void SelectRowCellValue(this By rowsElement, string colName, string expCellValue)
        {

            Thread.Sleep(3000);
            IList<IWebElement> rows = DriverHelper.Driver.FindElements(rowsElement);


            foreach (var row in rows)
            {
                IWebElement colElement = null;
                string colCellValue = "";
                string rowindex = row.GetAttribute("row-index");
                Console.WriteLine(rowindex);
                IWebElement activeRow = DriverHelper.Driver.WaitForElement(By.XPath($"//*[contains(@class,'ag-body-viewport')]//*[contains(@class,'ag-center-cols-container')]//*[contains(@class,'ag-row-position-absolute') and @row-index='{rowindex}']"));

                switch (colName)
                {
                    case "Created On":
                        colElement = DriverHelper.Driver.WaitForElement(By.XPath($"//*[contains(@class,'ag-body-viewport')]//*[contains(@class,'ag-center-cols-container')]//*[contains(@class,'ag-row-position-absolute') and @row-index='{rowindex}']//*[contains(@class,'ag-cell-value') and @col-id='createdon']//label"));
                        colCellValue = colElement.GetAttribute("aria-label").Trim();
                        break;
                    case "Submission ID":
                        colElement = DriverHelper.Driver.WaitForElement(By.XPath($"//*[contains(@class,'ag-body-viewport')]//*[contains(@class,'ag-center-cols-container')]//*[contains(@class,'ag-row-position-absolute') and @row-index='{rowindex}']//*[contains(@class,'ag-cell-value') and @col-id='voa_requestreferenceid']//label"));
                        colCellValue = colElement.GetAttribute("aria-label").Trim();
                        break;
                    case "Name":
                        colElement = DriverHelper.Driver.WaitForElement(By.XPath($"//*[contains(@class,'ag-body-viewport')]//*[contains(@class,'ag-center-cols-container')]//*[contains(@class,'ag-row-position-absolute') and @row-index='{rowindex}']//*[contains(@class,'ag-cell-value') and @col-id='voa_name']//a"));
                        colCellValue = colElement.GetAttribute("aria-label").Trim();
                        break;
                    case "Job ID":
                        colElement = DriverHelper.Driver.WaitForElement(By.XPath($"//*[contains(@class,'ag-body-viewport')]//*[contains(@class,'ag-center-cols-container')]//*[contains(@class,'ag-row-position-absolute') and @row-index='{rowindex}']//*[contains(@class,'ag-cell-value') and @col-id='ticketnumber']//label"));
                        colCellValue = colElement.GetAttribute("aria-label").Trim();
                        break;

                }
                rows = DriverHelper.Driver.FindElements(rowsElement);

                if (colCellValue == expCellValue)
                {
                    colElement.DoubleClickElementUsingJSExecutor();
                    Log.Information($"The column value {colCellValue} from column {colName} was selected");
                    break;
                }

            }

        }

        public static string ValidateFieldValueInUI(this IWebElement ele)

        {
            waitHelpers wh = new waitHelpers();
            string expFieldValue = null;

            Thread.Sleep(3000);
            wh.elementDisplayed(ele, 60);

            expFieldValue = ele.GetAttribute("value");
            if (expFieldValue == string.Empty)
            {
                expFieldValue = ele.Text;
                Log.Information($"The field value is {expFieldValue} captured from the UI");
            }


            return expFieldValue;

        }

        public static string ValidateFieldtitleInUI(this IWebElement ele)

        {
            waitHelpers wh = new waitHelpers();
            string expFieldValue = null;

            Thread.Sleep(3000);
            wh.elementDisplayed(ele, 60);

            expFieldValue = ele.GetAttribute("title");
            if (expFieldValue == string.Empty)
            {
                expFieldValue = ele.Text;
                Log.Information($"The field value is {expFieldValue} captured from the UI");
            }


            return expFieldValue;

        }

        public static void DoubleClickElementUsingJSExecutor(this IWebElement element)
        {
            var executor = ((IJavaScriptExecutor)DriverHelper.Driver);
            executor.ExecuteScript("arguments[0].dispatchEvent(new MouseEvent('dblclick',{bubbles:true}));", element);
            Log.Information($"User double clicked on ");
        }


        public static void ScrollToElement(IWebElement element)
        {
            var executor = ((IJavaScriptExecutor)DriverHelper.Driver);
            executor.ExecuteScript("arguments[0].scrollIntoView(true);", element);
            Log.Information($"User scrolled to ");
        }

        public static bool ValidateElementPresentOnUI(this IWebElement element)
        {

            try
            {
                // ScrollAndClick(element);
                ScrollToElement(element);
                IsElementVisible(element);
                Log.Information($"The element  present is UI is {IsElementVisible(element)}");
                return true;
            }
            catch (Exception)
            {
                Log.Error($"The element  present is UI is {IsElementVisible(element)}");
                Console.WriteLine($"The element  is not present");
                return false;
            }
        }


        public static void UploadFile()
        {
            string userDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            //string filePath = @"VOA_Automation\CTAzureAutomationRepo\BSTVOAQAAutomation\BSTVOAQAAutomation\DummyUpload.txt";
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "..\\..\\DummyUpload.txt");

            string DummyUpload = Path.Combine(userDirectory, filePath);
            string filePath_new = Path.GetFullPath(DummyUpload);
            Actions act = new Actions(DriverHelper.Driver);
            //act.Pause(TimeSpan.FromSeconds(5)).SendKeys(Keys.Enter).Pause(TimeSpan.FromSeconds(5)).SendKeys(DummyUpload).Pause(TimeSpan.FromSeconds(5)).SendKeys(Keys.Enter).Build().Perform();
            DriverHelper.Driver.FindElement(By.CssSelector("[id='chooseFiles']")).SendKeys(filePath_new);
            //AutoItX.WinWait("Open");
            //AutoItX.WinActivate("Open");
            //AutoItX.Send(filePath);
            //AutoItX.Send("{ENTER}");
            Log.Information("The file has been uploaded");
        }

        public static void UploadFile(string fileName)
        {
            string userDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "..\\..\\" + fileName + ".txt");

            string DummyUpload = Path.Combine(userDirectory, filePath);
            string filePath_new = Path.GetFullPath(DummyUpload);
            Actions act = new Actions(DriverHelper.Driver);
            DriverHelper.Driver.FindElement(By.CssSelector("[id='chooseFiles']")).SendKeys(filePath_new);
            Log.Information("The file has been uploaded");
        }

        public static void AppendToExcel(string town, string postCode, string actUPRN, string jobID)
        {
            string userDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string filePath = @"VOA_Automation\CTAzureAutomationRepo\BSTVOAQAAutomation\BSTVOAQAAutomation\OutputData.xlsx";
            // string ExcelFilePath = Path.Combine(userDirectory, filePath);

            //string userDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            //string filePath = @"VOA_Automation\CTAzureAutomationRepo\BSTVOAQAAutomation\BSTVOAQAAutomation\OutputData.xlsx";
            string ExcelFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + Config.TestOutPutExcelPath);
            //Path.Combine(userDirectory, filePath);

            FileInfo excelFile = new FileInfo(ExcelFilePath);
            using (ExcelPackage package = new ExcelPackage(excelFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["Output"];
                int rowCount = worksheet.Dimension?.Rows ?? 0;

                int currentRow = rowCount == 0 ? 1 : rowCount + 1;
                worksheet.Cells[currentRow, 1].Value = town;
                worksheet.Cells[currentRow, 2].Value = postCode;
                worksheet.Cells[currentRow, 3].Value = actUPRN;
                worksheet.Cells[currentRow, 4].Value = jobID;
                worksheet.Cells[currentRow, 5].Value = DateTime.Now;

                package.Save();
                Log.Information("The excel has been appeneded with new data");
            }


        }


        public static void captureTestDataToExcel(string JobID, string addressString, string sheetName)
        {
            string ExcelFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + Config.TestOutPutExcelPath);
            FileInfo excelFile = new FileInfo(ExcelFilePath);
            using (ExcelPackage package = new ExcelPackage(excelFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[sheetName];
                int rowCount = worksheet.Dimension?.Rows ?? 0;
                int currentRow = rowCount == 0 ? 1 : rowCount + 1;
                worksheet.Cells[currentRow, 1].Value = JobID;
                worksheet.Cells[currentRow, 2].Value = addressString;
                worksheet.Cells[currentRow, 3].Value = PDF_Utility.getCurrentSystemDate("dd-MM-yyyy_HH-mm");
                package.Save();
                Log.Information("The excel has been appeneded with new data");
            }
        }

        public static async Task<List<string>> getAllVOA_COA_quries()
        {
            var VOA_COA_quires = new List<string>
        {"0114","0116","0119","0121","0205","0215","0220","0230","0235","0240","0305","0310","0315","0320","0325","0330","0335","0340","0345","0350","0355","0360","0405","0410","0415","0425","0435","0440","0505","0510","0515","0520","0525","0530","0540","0605","0610","0615","0620","0625","0630","0635","0640","0650","0655","0660","0665","0724","0728","0734","0738","0805","0810","0815","0820","0825","0830","0835","0840","0905","0910","0915","0920","0925","0930","1005","1010","1015","1025","1030","1035","1040","1045","1055","1105","1110","1115","1120","1125","1130","1135","1140","1145","1150","1160","1165","1210","1215","1225","1230","1235","1240","1250","1255","1265","1305","1315","1320","1325","1330","1335","1340","1350","1355","1410","1415","1425","1430","1435","1445","1505","1510","1515","1520","1525","1530","1535","1540","1545","1550","1555","1560","1565","1570","1590","1595","1605","1610","1615","1620","1625","1630","1705","1710","1715","1720","1725","1730","1735","1740","1750","1760","1765","1775","1780","1805","1810","1815","1820","1825","1830","1835","1840","1845","1850","1860","1905","1910","1915","1920","1925","1930","1935","1940","1945","1950","2001","2002","2003","2004","2100","2205","2210","2215","2220","2225","2230","2235","2240","2245","2250","2255","2260","2265","2270","2280","2305","2310","2315","2320","2325","2330","2335","2340","2345","2350","2355","2360","2365","2370","2372","2373","2405","2410","2415","2420","2430","2435","2440","2465","2470","2505","2510","2515","2520","2525","2530","2535","2605","2610","2615","2620","2625","2630","2635","2705","2710","2715","2720","2725","2730","2735","2741","2805","2810","2815","2820","2825","2830","2835","2905","2910","2915","2920","2925","2930","2935","3005","3010","3015","3020","3025","3030","3035","3040","3060","3105","3110","3115","3120","3125","3205","3210","3215","3220","3225","3230","3240","3245","3305","3310","3315","3320","3325","3330","3405","3410","3415","3420","3425","3430","3435","3445","3455","3505","3510","3515","3520","3525","3530","3535","3540","3545","3605","3610","3615","3620","3625","3630","3635","3640","3645","3650","3655","3705","3710","3715","3720","3725","3805","3810","3815","3820","3825","3830","3835","3905","3910","3915","3925","3935","3940","4205","4210","4215","4220","4225","4230","4235","4240","4245","4250","4305","4310","4315","4320","4325","4405","4410","4415","4420","4505","4510","4515","4520","4525","4605","4610","4615","4620","4625","4630","4635","4705","4710","4715","4720","4725","5030","5060","5090","5120","5150","5180","5210","5240","5270","5300","5330","5360","5390","5420","5450","5480","5510","5540","5570","5600","5630","5660","5690","5720","5750","5780","5810","5840","5870","5900","5930","5960","5990","6805","6810","6815","6820","6825","6828","6829","6830","6835","6840","6845","6850","6853","6854","6855","6905","6910","6915","6920","6925","6930","6935","6940","6945","6950","6955"};
            return VOA_COA_quires;
        }

        public static async Task<Dictionary<string, string>> GetFirstAvailableDataAsync(List<string> queries)
        {
            if (queries == null || queries.Count == 0)
                throw new ArgumentException("At least one query must be provided.", nameof(queries));

            NpgsqlConnection conn = DBConnectionUtility.GetConnection();
            await conn.OpenAsync();
            var row = new Dictionary<string, string>();

            foreach (var query in queries)
            {
                if (string.IsNullOrWhiteSpace(query))
                    continue; // Skip empty queries

                var cmd = new NpgsqlCommand(query, conn);

                try
                {
                    var reader = await cmd.ExecuteReaderAsync();



                    while (await reader.ReadAsync())
                    {

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row[reader.GetName(i)] = (string)(reader.IsDBNull(i) ? null : reader.GetValue(i));
                        }


                    }

                    if (row.Count > 0)
                    {
                        return row; // Return first non-empty result set
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error executing query: {ex.Message}");
                    // Continue to next query if one fails
                }
            }

            return row; // No data found
        }



        public static Dictionary<string, string> GetDBResult(string queryKey)
        {
            NpgsqlDataReader dbData = null;
            string propertiesFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + Config.SQLQueryPropertiesFilePath);
            PropertiesReader _propertiesReader = new PropertiesReader(propertiesFilePath);

            string query = _propertiesReader.Get(queryKey);
            if (query == null)
            {
                Log.Information($"Query key '{queryKey}' not found in properties file.");
                throw new KeyNotFoundException($"Query key '{queryKey}' not found in properties file.");
            }
            else
            {
                Console.WriteLine(query);
            }
            var dbDataValue = new Dictionary<string, string>();
            using (NpgsqlConnection con = DBConnectionUtility.GetConnection())
            {
                try
                {

                    con.Open();
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        Console.WriteLine("DB Connected");
                        NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                        //NpgsqlCommand cmd = new NpgsqlCommand();
                        cmd.CommandTimeout = 300;
                        //cmd.CommandText = query;
                        //cmd.Connection = con;
                        try
                        {
                            dbData = cmd.ExecuteReader();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Npgsql Exception:" + ex.Message);
                        }
                        //  NpgsqlDataReader dbData = cmd.ExecuteReader();

                        while (dbData.Read())
                        {

                            for (int i = 0; i < dbData.FieldCount; i++)
                            {
                                string columnName = dbData.GetName(i);
                                object columnValueObj = dbData.GetValue(i);
                                string columnValue = (columnValueObj != DBNull.Value) ? columnValueObj.ToString() : null;
                                //   Console.WriteLine($"Column Name:{columnName}, value: {columnValue}");
                                dbDataValue.Add(columnName, columnValue);
                            }
                            Log.Information("{0} {1} {2}", dbData.GetValue(0), dbData.GetValue(1), dbData.GetValue(2));
                            Console.WriteLine("{0} {1} {2}", dbData.GetValue(0), dbData.GetValue(1), dbData.GetValue(2));

                        }

                    }

                }
                catch (NpgsqlException ex)
                {
                    Log.Error("Npgsql Exception:" + ex.Message);
                    Console.WriteLine("Npgsql Exception:" + ex.Message);
                }
                catch (Exception ex)
                {
                    Log.Error("Npgsql Exception:" + ex.Message);
                    Console.WriteLine("Npgsql Exception:" + ex.Message);
                }
                finally
                {
                    if (con.State != System.Data.ConnectionState.Closed)
                    {
                        con.Close();
                    }
                }
            }
            return dbDataValue;

        }

        public static bool validateVOADBdata(Dictionary<string, string> dbDataValues)
        {
            bool VOA_Unique_addr = false;
            string query = null;
            if (dbDataValues.Count > 1)
            {
                query = $"select count(label_source_id) from property.du_label where uprn='{dbDataValues["uprn"]}'";
            }

            long uprnVOACount = 0;
            using (NpgsqlConnection con = DBConnectionUtility.GetConnection())
            {
                try
                {
                    con.Open();
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        Console.WriteLine("DB Connected");
                        NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                        cmd.CommandTimeout = 300;

                        try
                        {
                            uprnVOACount = (long)cmd.ExecuteScalar();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Npgsql Exception:" + ex.Message);
                        }

                        if (uprnVOACount == 1)
                        {
                            VOA_Unique_addr = true;
                        }
                        else
                        {
                            VOA_Unique_addr = false;
                        }
                    }
                }
                catch (NpgsqlException ex)
                {
                    Log.Error("Npgsql Exception:" + ex.Message);
                    Console.WriteLine("Npgsql Exception:" + ex.Message);
                }
                catch (Exception ex)
                {
                    Log.Error("Npgsql Exception:" + ex.Message);
                    Console.WriteLine("Npgsql Exception:" + ex.Message);
                }
                finally
                {
                    if (con.State != System.Data.ConnectionState.Closed)
                    {
                        con.Close();
                    }
                }
            }
            return VOA_Unique_addr;
        }



        public static Dictionary<string, string> GetDBResultByModifiyingQuery(string queryKey, string querryParameter, FeatureContext fc)
        {
            NpgsqlDataReader dbData = null;
            string propertiesFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + Config.SQLQueryPropertiesFilePath);
            PropertiesReader _propertiesReader = new PropertiesReader(propertiesFilePath);

            string query = _propertiesReader.Get(queryKey);
            query = query.Replace("QueryParameter", (string)fc[querryParameter]);
            if (query == null)
            {
                Log.Information($"Query key '{queryKey}' not found in properties file.");
                throw new KeyNotFoundException($"Query key '{queryKey}' not found in properties file.");
            }
            else
            {
                Console.WriteLine(query);
            }
            var dbDataValue = new Dictionary<string, string>();
            using (NpgsqlConnection con = DBConnectionUtility.GetConnection())
            {
                try
                {

                    con.Open();
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        Console.WriteLine("DB Connected");
                        NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                        //NpgsqlCommand cmd = new NpgsqlCommand();
                        cmd.CommandTimeout = 300;
                        //cmd.CommandText = query;
                        //cmd.Connection = con;
                        try
                        {
                            dbData = cmd.ExecuteReader();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Npgsql Exception:" + ex.Message);
                        }
                        //  NpgsqlDataReader dbData = cmd.ExecuteReader();

                        while (dbData.Read())
                        {

                            for (int i = 0; i < dbData.FieldCount; i++)
                            {
                                string columnName = dbData.GetName(i);
                                object columnValueObj = dbData.GetValue(i);
                                string columnValue = (columnValueObj != DBNull.Value) ? columnValueObj.ToString() : null;
                                //   Console.WriteLine($"Column Name:{columnName}, value: {columnValue}");
                                dbDataValue.Add(columnName, columnValue);
                            }
                            Log.Information("{0} {1} {2}", dbData.GetValue(0), dbData.GetValue(1), dbData.GetValue(2));
                            Console.WriteLine("{0} {1} {2}", dbData.GetValue(0), dbData.GetValue(1), dbData.GetValue(2));

                        }

                    }

                }
                catch (NpgsqlException ex)
                {
                    Log.Error("Npgsql Exception:" + ex.Message);
                    Console.WriteLine("Npgsql Exception:" + ex.Message);
                }
                catch (Exception ex)
                {
                    Log.Error("Npgsql Exception:" + ex.Message);
                    Console.WriteLine("Npgsql Exception:" + ex.Message);
                }
                finally
                {
                    if (con.State != System.Data.ConnectionState.Closed)
                    {
                        con.Close();
                    }
                }
            }
            return dbDataValue;

        }

        public static Dictionary<string, string> GetDBResultByModifiyingQuery(string queryKey, string querryParameter, Dictionary<String, String> fc)
        {
            NpgsqlDataReader dbData = null;
            string propertiesFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + Config.SQLQueryPropertiesFilePath);
            PropertiesReader _propertiesReader = new PropertiesReader(propertiesFilePath);

            string query = _propertiesReader.Get(queryKey);
            query = query.Replace("QueryParameter", (string)fc[querryParameter]);
            if (query == null)
            {
                Log.Information($"Query key '{queryKey}' not found in properties file.");
                throw new KeyNotFoundException($"Query key '{queryKey}' not found in properties file.");
            }
            else
            {
                Console.WriteLine(query);
            }
            var dbDataValue = new Dictionary<string, string>();
            using (NpgsqlConnection con = DBConnectionUtility.GetConnection())
            {
                try
                {

                    con.Open();
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        Console.WriteLine("DB Connected");
                        NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                        //NpgsqlCommand cmd = new NpgsqlCommand();
                        cmd.CommandTimeout = 300;
                        //cmd.CommandText = query;
                        //cmd.Connection = con;
                        try
                        {
                            dbData = cmd.ExecuteReader();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Npgsql Exception:" + ex.Message);
                        }
                        //  NpgsqlDataReader dbData = cmd.ExecuteReader();

                        while (dbData.Read())
                        {

                            for (int i = 0; i < dbData.FieldCount; i++)
                            {
                                string columnName = dbData.GetName(i);
                                object columnValueObj = dbData.GetValue(i);
                                string columnValue = (columnValueObj != DBNull.Value) ? columnValueObj.ToString() : null;
                                //   Console.WriteLine($"Column Name:{columnName}, value: {columnValue}");
                                dbDataValue.Add(columnName, columnValue);
                            }
                            Log.Information("{0} {1} {2}", dbData.GetValue(0), dbData.GetValue(1), dbData.GetValue(2));
                            Console.WriteLine("{0} {1} {2}", dbData.GetValue(0), dbData.GetValue(1), dbData.GetValue(2));

                        }

                    }

                }
                catch (NpgsqlException ex)
                {
                    Log.Error("Npgsql Exception:" + ex.Message);
                    Console.WriteLine("Npgsql Exception:" + ex.Message);
                }
                catch (Exception ex)
                {
                    Log.Error("Npgsql Exception:" + ex.Message);
                    Console.WriteLine("Npgsql Exception:" + ex.Message);
                }
                finally
                {
                    if (con.State != System.Data.ConnectionState.Closed)
                    {
                        con.Close();
                    }
                }
            }
            return dbDataValue;

        }

        public static Func<IWebDriver, IWebElement> ElementExists(this IWebElement element)
        {

            return (d) =>
            {
                try
                {
                    //  var element = DriverHelper.Driver.FindElement(locator);
                    return element.Displayed ? element : null;
                }
                catch (NoSuchElementException)
                {
                    Log.Error("The element does not exists with error `No such element`");
                    return null;
                }
                catch (StaleElementReferenceException)
                {
                    Log.Error("The element does not exists with error `stale element'");
                    return null;
                }
            };


        }


        public static bool WaitUntilElementAttached(this IWebElement curreElement, int timeout)
        {
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(timeout));
            try
            {

                IWebElement element = wait.Until(ElementExists(curreElement));
                Log.Information($"The element {curreElement} is attached to DOM");
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                Log.Error($"The element is not attached to DOM");
                return false;
            }

        }

        public static Func<IWebDriver, IWebElement> ElementExists(By locator)
        {
            return (d) =>
            {
                return d.FindElement(locator);
            };
        }

        public static void WaitForElementAndClick(By locator)
        {
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(120));
            wait.PollingInterval = TimeSpan.FromMilliseconds(200);
            IWebElement ele = DriverHelper.Driver.FindElement(locator);
            wait.Until(elementTobeClickable(ele)).Click();
            //wait.Until(driver => driver.FindElement(locator).Displayed);
            //DriverHelper.Driver.FindElement(locator).Click();
        }

        public static void WaitForElementAndClick(IWebElement webEle)
        {
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(120));
            wait.PollingInterval = TimeSpan.FromMilliseconds(200);
            wait.Until(elementTobeClickable(webEle)).Click();
            //wait.Until(driver => driver.FindElement(locator).Displayed);
            //DriverHelper.Driver.FindElement(locator).Click();
        }

        public static void WaitForElementAndClick(IWebElement webEle, int time)
        {
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(time));
            wait.PollingInterval = TimeSpan.FromMilliseconds(200);
            wait.Until(elementTobeClickable(webEle)).Click();
        }

        public static void WaitForElementAndClick(By locator, int time)
        {
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(time));
            wait.PollingInterval = TimeSpan.FromMilliseconds(200);
            IWebElement ele = DriverHelper.Driver.FindElement(locator);
            wait.Until(elementTobeClickable(ele)).Click();
            //wait.Until(driver => driver.FindElement(locator).Displayed);
            //DriverHelper.Driver.FindElement(locator).Click();
        }

        public static void WaitForElementToDisplayed(By locator, int timeOuts)
        {
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(timeOuts));
            wait.PollingInterval = TimeSpan.FromMilliseconds(200);
            wait.Until(driver => driver.FindElement(locator).Displayed);
        }

        public static By getLocator(String locator)
        {
            if (locator.Contains("//"))
                return By.XPath(locator);
            else
                return By.CssSelector(locator);
        }
        public static void ScrollUntilSelectorDisplayedAndSendKeys(this IWebElement divElement, String fieldName, string text, int timeout)
        {
            By selector = getLocator($"//input[contains(@aria-label,'{fieldName}')]");
            var executor = ((IJavaScriptExecutor)DriverHelper.Driver);
            int waited = 0;
            //DriverHelper.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(0.5));

            while (timeout > 0 && !(waited >= timeout))
            {
                try
                {
                    if (DriverHelper.Driver.FindElement(selector).Displayed)
                    {
                        if (DriverHelper.Driver.FindElement(selector).GetAttribute("value").Equals(text))
                        {
                            //DriverHelper.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(Config.DefaultDriverTimeOut));
                            return;
                        }
                        DriverHelper.Driver.FindElement(selector).Clear();
                        DriverHelper.Driver.FindElement(selector).ClickUsingJavascript();
                        DriverHelper.Driver.FindElement(selector).SendKeys(Keys.Control + "A");
                        DriverHelper.Driver.FindElement(selector).SendKeys(Keys.Backspace);
                        DriverHelper.Driver.FindElement(selector).SendKeys(text);

                        if (DriverHelper.Driver.FindElement(selector).GetAttribute("value").Equals(text))
                        {

                            DriverHelper.Driver.WaitForElements(By.CssSelector("[data-id*='Lookup'] ul li span[id*='name']"), 1000).Where(t => t.Text.Contains(text)).FirstOrDefault().IsElementDisplayed(1);
                            //DriverHelper.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(Config.DefaultDriverTimeOut));
                            return;
                        }
                    }
                    else
                    {
                        executor.ExecuteScript("arguments[0].scrollBy(0,90);", divElement);
                    }
                }
                catch
                {
                    try
                    {
                        executor.ExecuteScript("arguments[0].scrollBy(0,90);", divElement);
                    }
                    catch { }
                }
                waited += 1;
            }
            //DriverHelper.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(Config.DefaultDriverTimeOut));
        }


        public static void ScrollUntilSelectorDisplayedAndSendKeys(this IWebElement divElement, String fieldName, string sheetName, String RowID, int timeout)
        {
            By selector = getLocator($"//input[contains(@aria-label,'{fieldName}')]");
            string TestDataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + Config.TestDataExcelFilePath);
            var excelUtility = new ExcelTestDataUtility(TestDataFilePath);
            string text = excelUtility.getFieldTestData(fieldName, sheetName, RowID);
            var executor = ((IJavaScriptExecutor)DriverHelper.Driver);
            int waited = 0;
            //DriverHelper.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(0.5));

            while (timeout > 0 && !(waited >= timeout))
            {
                try
                {
                    if (DriverHelper.Driver.FindElement(selector).Displayed)
                    {
                        if (DriverHelper.Driver.FindElement(selector).GetAttribute("value").Equals(text))
                        {
                            //DriverHelper.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(Config.DefaultDriverTimeOut));
                            return;
                        }
                        DriverHelper.Driver.FindElement(selector).Clear();
                        DriverHelper.Driver.FindElement(selector).ClickUsingJavascript();
                        DriverHelper.Driver.FindElement(selector).SendKeys(Keys.Control + "A");
                        DriverHelper.Driver.FindElement(selector).SendKeys(Keys.Backspace);
                        DriverHelper.Driver.FindElement(selector).SendKeys(text);

                        if (DriverHelper.Driver.FindElement(selector).GetAttribute("value").Equals(text))
                        {

                            DriverHelper.Driver.WaitForElements(By.CssSelector("[data-id*='Lookup'] ul li span[id*='name']"), 1000).Where(t => t.Text.Contains(text)).FirstOrDefault().IsElementDisplayed(1);
                            //DriverHelper.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(Config.DefaultDriverTimeOut));
                            return;
                        }
                    }
                    else
                    {
                        executor.ExecuteScript("arguments[0].scrollBy(0,90);", divElement);
                        //executor.ExecuteScript("arguments[0].scrollBy(0,90);", divElement);
                    }
                }
                catch
                {
                    try
                    {
                        executor.ExecuteScript("arguments[0].scrollBy(0,90);", divElement);
                    }
                    catch { }
                }
                waited += 1;
            }
            //DriverHelper.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(Config.DefaultDriverTimeOut));
        }

        public static void ScrollUntilSelectorDisplayedAndSendKeys(this IWebElement divElement, String fieldName, Dictionary<string, string> testdata, int timeout)
        {
            By selector = getLocator($"//input[contains(@aria-label,'{fieldName}')]");
            //string TestDataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + Config.TestDataExcelFilePath);
            //var excelUtility = new ExcelTestDataUtility(TestDataFilePath);
            string text = testdata[fieldName];
            var executor = ((IJavaScriptExecutor)DriverHelper.Driver);
            int waited = 0;
            //DriverHelper.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(0.5));

            while (timeout > 0 && !(waited >= timeout))
            {
                try
                {
                    if (DriverHelper.Driver.FindElement(selector).Displayed)
                    {
                        if (DriverHelper.Driver.FindElement(selector).GetAttribute("value").Equals(text))
                        {
                            //DriverHelper.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(Config.DefaultDriverTimeOut));
                            return;
                        }
                        DriverHelper.Driver.FindElement(selector).Clear();
                        DriverHelper.Driver.FindElement(selector).ClickUsingJavascript();
                        DriverHelper.Driver.FindElement(selector).SendKeys(Keys.Control + "A");
                        DriverHelper.Driver.FindElement(selector).SendKeys(Keys.Backspace);
                        DriverHelper.Driver.FindElement(selector).SendKeys(text);

                        if (DriverHelper.Driver.FindElement(selector).GetAttribute("value").Equals(text))
                        {

                            DriverHelper.Driver.WaitForElements(By.CssSelector("[data-id*='Lookup'] ul li span[id*='name']"), 1000).Where(t => t.Text.Contains(text)).FirstOrDefault().IsElementDisplayed(1);
                            //DriverHelper.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(Config.DefaultDriverTimeOut));
                            return;
                        }
                    }
                    else
                    {
                        executor.ExecuteScript("arguments[0].scrollBy(0,90);", divElement);
                    }
                }
                catch
                {
                    try
                    {
                        executor.ExecuteScript("arguments[0].scrollBy(0,90);", divElement);
                    }
                    catch { }
                }
                waited += 1;
            }
            //DriverHelper.Driver.Manage().Timeouts().ImplicitWait = (TimeSpan.FromSeconds(Config.DefaultDriverTimeOut));
        }

        public static void ScrollUntilSelectorDisplayedAndSendKeys_New(String fieldName, Dictionary<string, string> testdata, int timeout)
        {
            waitHelpers wh = new waitHelpers();
            string text = testdata[fieldName];
            By selector = getLocator($"//input[contains(@aria-label,'{fieldName}')]");
            By lookUpSpan = getLocator($"[data-id*='Lookup'] ul li span[id*='name']");
            By lookUpSpan1 = getLocator($"//span[text()='{text}']");
            wh.isElementDisplayed(selector, 60);
            Actions act = new Actions(DriverHelper.Driver);
            IWebElement ele = wh.GetWebElement(selector);
            act.MoveToElement(ele).Pause(TimeSpan.FromMilliseconds(1000)).Build().Perform();
            ele.ClearAndSendkeys(text);

            if (wh.isElementDisplayed(lookUpSpan, 30))
            {
                wh.clickOnElement(lookUpSpan);
            }
            else
            {
                By selectedValue = By.XPath($"//ul[@title='{fieldName}' and contains(@id,'SelectedRecordList')]");
                bool lookUprecordSelected = wh.GetWebElement(selectedValue, 5);
                if (!lookUprecordSelected)
                {
                    throw new Exception($"LookUp value is not select for {fieldName}");
                }
            }
        }

        public static void ScrollUntilSelectorDisplayedAndSendKeys_withValue(String fieldName, String fieldValue, int timeout)
        {
            waitHelpers wh = new waitHelpers();
            string text = fieldValue;
            By selector = getLocator($"//input[contains(@aria-label,'{fieldName}')]");
            By lookUpSpan = getLocator($"[data-id*='Lookup'] ul li span[id*='name']");
            By lookUpSpan1 = getLocator($"//span[text()='{text}']");
            wh.isElementDisplayed(selector, 60);
            Actions act = new Actions(DriverHelper.Driver);
            IWebElement ele = wh.GetWebElement(selector);
            act.MoveToElement(ele).Pause(TimeSpan.FromMilliseconds(1000)).Build().Perform();
            ele.ClearAndSendkeys(text);

            if (wh.isElementDisplayed(lookUpSpan, 30))
            {
                wh.clickOnElement(lookUpSpan);
            }
            else
            {
                By selectedValue = By.XPath($"//ul[@title='{fieldName}' and contains(@id,'SelectedRecordList')]");
                bool lookUprecordSelected = wh.GetWebElement(selectedValue, 5);
                if (!lookUprecordSelected)
                {
                    throw new Exception($"LookUp value is not select for {fieldName}");
                }
            }
        }


        public static void ScrollUntilSelectorDisplayedAndSendKeys_New(String fieldName, FeatureContext fc, int timeout)
        {
            waitHelpers wh = new waitHelpers();
            string text = (string)fc[fieldName];
            By selector = getLocator($"//input[contains(@aria-label,'{fieldName}')]");
            By lookUpSpan = getLocator($"[data-id*='Lookup'] ul li span[id*='name']");
            By lookUpSpan1 = getLocator($"//span[text()='{text}']");
            wh.isElementDisplayed(selector, 60);
            Actions act = new Actions(DriverHelper.Driver);
            IWebElement ele = wh.GetWebElement(selector);
            act.MoveToElement(ele).Pause(TimeSpan.FromMilliseconds(1000)).Build().Perform();
            ele.ClearAndSendkeys(text);

            if (wh.isElementDisplayed(lookUpSpan, 30))
            {
                wh.clickOnElement(lookUpSpan);
            }
            else
            {
                By selectedValue = By.XPath($"//ul[@title='{fieldName}' and contains(@id,'SelectedRecordList')]");
                bool lookUprecordSelected = wh.GetWebElement(selectedValue, 5);
                if (!lookUprecordSelected)
                {
                    throw new Exception($"LookUp value is not select for {fieldName}");
                }
            }
        }

        public static void ScrollUntilSelectorDisplayedAndSendKeysBySearchIcon(String fieldName, Dictionary<string, string> testdata, int timeout)
        {
            waitHelpers wh = new waitHelpers();
            string text = testdata[fieldName];
            By selector = getLocator($"//input[contains(@aria-label,'{fieldName}')]");
            By lookUpSpan = getLocator($"[data-id*='Lookup'] ul li span[id*='name']");
            By lookUpSpan1 = getLocator($"//span[text()='{text}']");
            By searchIcon = getLocator($"//input[contains(@aria-label,'{fieldName}')]//following-sibling::button[@title='Search']");
            wh.isElementDisplayed(selector, 60);
            Actions act = new Actions(DriverHelper.Driver);
            IWebElement ele = wh.GetWebElement(selector);
            ele.Click();
            //ele.SendKeys(text);
            foreach (char letter in text)
            {
                ele.SendKeys(letter.ToString());
                //Thread.Sleep(100);
            }
            //act.MoveToElement(ele).Pause(TimeSpan.FromMilliseconds(100)).Build().Perform();
            try
            {
                wh.clickOnElement(searchIcon);
                if (wh.isElementDisplayed(lookUpSpan, 30))
                {
                    wh.clickOnElement(lookUpSpan);
                }
                else
                {
                    throw new Exception($"unable to find lookup suggestion even after clicking on search for {fieldName} field");
                }

            }
            catch (Exception e)
            {
                By selectedValue = By.XPath($"//ul[@title='{fieldName}' and contains(@id,'SelectedRecordList')]");
                bool recordSelected = wh.GetWebElement(selectedValue, 1);
                if (!recordSelected)
                {
                    wh.clickOnElement(searchIcon);
                    if (wh.isElementDisplayed(lookUpSpan, 30))
                    {
                        wh.clickOnElement(lookUpSpan);
                    }
                    else
                    {
                        throw new Exception($"Still unable to find lookup suggestion even after clicking on search for {fieldName} field");
                    }
                }

            }
        }

        public static void ScrollUntilSelectorDisplayedAndSendKeysBySearchIcon(String fieldValue, String fieldName, int timeout)
        {
            waitHelpers wh = new waitHelpers();
            string text = fieldValue;
            By selector = getLocator($"//input[contains(@aria-label,'{fieldName}')]");
            By lookUpSpan = getLocator($"[data-id*='Lookup'] ul li span[id*='name']");
            By lookUpSpan1 = getLocator($"//span[text()='{text}']");
            By searchIcon = getLocator($"//input[contains(@aria-label,'{fieldName}')]//following-sibling::button[@title='Search']");
            wh.isElementDisplayed(selector, 60);
            Actions act = new Actions(DriverHelper.Driver);
            IWebElement ele = wh.GetWebElement(selector);
            ele.Click();
            foreach (char letter in text)
            {
                ele.SendKeys(letter.ToString());
            }
            try
            {
                wh.clickOnElement(searchIcon);
                if (wh.isElementDisplayed(lookUpSpan, 30))
                {
                    wh.clickOnElement(lookUpSpan);
                }
                else
                {
                    throw new Exception($"unable to find lookup suggestion even after clicking on search for {fieldName} field");
                }

            }
            catch (Exception e)
            {
                By selectedValue = By.XPath($"//ul[@title='{fieldName}' and contains(@id,'SelectedRecordList')]");
                bool recordSelected = wh.GetWebElement(selectedValue, 1);
                if (!recordSelected)
                {
                    wh.clickOnElement(searchIcon);
                    if (wh.isElementDisplayed(lookUpSpan, 30))
                    {
                        wh.clickOnElement(lookUpSpan);
                    }
                    else
                    {
                        throw new Exception($"Still unable to find lookup suggestion even after clicking on search for {fieldName} field");
                    }
                }

            }
        }

        public static void ScrollUntilSelectorDisplayedAndSendKeys_Storage(String fieldName, String testdata, int timeout)
        {
            waitHelpers wh = new waitHelpers();
            string text = testdata;
            By selector = getLocator($"//input[contains(@aria-label,'{fieldName}')]");
            By lookUpSpan = getLocator($"[data-id*='Lookup'] ul li span[id*='name']");
            //By lookUpSpan1 = getLocator($"//span[text()='{text}']");
            Actions act = new Actions(DriverHelper.Driver);
            //scrollToBtnElement(fieldName);
            IWebElement ele = wh.GetWebElement(selector);
            ele.Click();
            ele.SendKeys(text);
            act.MoveToElement(ele).Pause(TimeSpan.FromMilliseconds(100)).Build().Perform();
            wh.clickOnElement(lookUpSpan);
        }

        public static void openEstateFile(String fileName)
        {
            waitHelpers wh = new waitHelpers();
            By estateFileLink = By.XPath($"//a[@aria-label='{fileName}']");
            wh.clickOnElement(estateFileLink);
        }

        public static void ScrollUntilSelectorDisplayedAndSendKeys_New(int position, String fieldName, Dictionary<string, string> testdata, int timeout)
        {
            waitHelpers wh = new waitHelpers();
            string text = testdata[fieldName];
            By selector = getLocator($"(//input[contains(@aria-label,'{fieldName}')])[{position}]");
            By lookUpSpan = getLocator($"[data-id*='Lookup'] ul li span[id*='name']");
            By lookUpSpan1 = getLocator($"//span[text()='{text}']");
            Actions act = new Actions(DriverHelper.Driver);
            //scrollToBtnElement(fieldName);
            IWebElement ele = wh.GetWebElement(selector);
            //waitUnitlElementToBeDisplayed(selector, timeout);
            ele.Click();
            ele.SendKeys(text);
            //ele.ClearAndSendkeys(text);
            act.MoveToElement(ele).Pause(TimeSpan.FromMilliseconds(100)).Build().Perform();
            wh.clickOnElement(lookUpSpan);
            //WaitForElementAndClick(lookUpSpan);
        }


        public static void ScrollUntilSelectorDisplayedAndSendKeys_New(String fieldName, String fieldTxt, int timeout)
        {
            string text = fieldTxt;
            By selector = getLocator($"//input[contains(@aria-label,'{fieldName}') and not(contains(@id,'Date'))]");
            By lookUpSpan = getLocator($"[data-id*='Lookup'] ul li span[id*='name']");
            By lookUpSpan1 = getLocator($"//span[contains(text(),'{text}')]");
            Actions act = new Actions(DriverHelper.Driver);
            IWebElement ele = waitUnitlElementToBeDisplayed(selector, timeout);
            ele.Click();
            foreach (char letter in text)
            {
                ele.SendKeys(letter.ToString());
            }

            //ele.SendKeys(text);
            act.MoveToElement(ele).Pause(TimeSpan.FromMilliseconds(300)).Build().Perform();
            WaitForElementAndClick(lookUpSpan);
        }

        public static void lookUpFieldBasedOnText(String fieldName, String fieldTxt, int timeout)
        {
            string text = fieldTxt;
            waitHelpers wh = new waitHelpers();
            Scroll scr = new Scroll();
            CommonPage cp = new CommonPage();
            cp.waitTillPageLoading(60);
            By selector = getLocator($"//input[contains(@aria-label,'{fieldName}') and (contains(@id,'Lookup'))]");
            By lookUpSpan = getLocator($"[data-id*='Lookup'] ul li span[id*='name']");
            if (fieldName.Equals("Template Group"))
                lookUpSpan = getLocator($"[data-id *= 'Lookup'] ul li span[id *= 'templategroupid']");
            By searchIcon = getLocator($"(//input[contains(@aria-label,'{fieldName}')]//following-sibling::button[@title='Search'])[1]");
            By lookUpResults = getLocator($"//ul[@aria-label='Lookup results']//li");
            IWebElement ele; ;
            int counter = 0;
            bool isLookUpResultsDisplayed = false;
            By selectedValue = By.XPath($"//ul[@title='{fieldName}' and contains(@id,'SelectedRecordList')]");
            bool recordSelected = false;
            while (!recordSelected)
            {
                try
                {
                    ele = wh.GetWebElementByTimeOut(selector, 10);
                    scr.ScrollIntoViewEle(ele);
                    ele.Click();
                }
                catch (StaleElementReferenceException e)
                {
                    Thread.Sleep(1000);
                    if (!wh.isElementDisplayed(selectedValue, 2))
                    {
                        ele = wh.GetWebElement(selector);
                        ele.Click();
                    }
                }
                catch (ElementClickInterceptedException e)
                {
                    Thread.Sleep(1000);
                    if (!wh.isElementDisplayed(selectedValue, 3))
                    {
                        ele = wh.GetWebElement(selector);
                        ele.Click();
                    }
                }
                catch (ElementNotInteractableException e)
                {
                    Thread.Sleep(1000);
                    if (!wh.isElementDisplayed(selectedValue, 3))
                    {
                        ele = wh.GetWebElement(selector);
                        ele.Click();
                    }
                }
                catch (WebDriverTimeoutException e)
                {
                    scr.scrollUntillLookUpEleVisible(fieldName, 100);
                    ele = wh.GetWebElement(selector);
                    ele.Click();
                }
                ele = wh.GetWebElement(selector);
                ele.SendKeys(Keys.Control + "a");
                ele.SendKeys(Keys.Backspace);
                ele.SendKeys(text);
                do
                {
                    isLookUpResultsDisplayed = isElementDisplayed_New(lookUpSpan);
                    if (!isLookUpResultsDisplayed)
                    {
                        wh.clickOnElement(searchIcon);
                        Thread.Sleep(1000);
                        isLookUpResultsDisplayed = isElementDisplayed_New(lookUpSpan);
                    }
                    recordSelected = wh.isElementDisplayed(selectedValue, 1);
                    if (isLookUpResultsDisplayed || recordSelected) break;
                    counter = counter + 1;
                } while (isLookUpResultsDisplayed || counter == 50);
                bool isSpanDisplayed = wh.isElementDisplayed(lookUpSpan, 1);
                if (isSpanDisplayed)
                {
                    wh.clickOnElement(lookUpSpan);
                }
                recordSelected = wh.isElementDisplayed(selectedValue, 2);
                if (recordSelected) break;
            }
        }

        public static void lookUpFieldBasedOnTextWithKeyPad(String fieldName, String fieldTxt, int timeout)
        {
            string text = fieldTxt;
            waitHelpers wh = new waitHelpers();
            Scroll scr = new Scroll();
            CommonPage cp = new CommonPage();
            cp.waitTillPageLoading(60);
            By selector = getLocator($"//input[contains(@aria-label,'{fieldName}') and (contains(@id,'Lookup'))]");
            By lookUpSpan = getLocator($"[data-id*='Lookup'] ul li span[id*='name']");
            //By lookUpSpan1 = getLocator($"//span[contains(text(),'{text}')]");
            By searchIcon = getLocator($"(//input[contains(@aria-label,'{fieldName}')]//following-sibling::button[@title='Search'])[1]");
            By lookUpResults = getLocator($"//ul[@aria-label='Lookup results']//li");
            IWebElement ele;
            int counter = 0;
            bool isLookUpResultsDisplayed = false;
            By selectedValue = By.XPath($"//ul[@title='{fieldName}' and contains(@id,'SelectedRecordList')]");
            bool recordSelected = false;
            while (!recordSelected)
            {
                try
                {
                    ele = wh.GetWebElementByTimeOut(selector, 10);
                    scr.ScrollIntoViewEle(ele);
                    ele.Click();
                }
                catch (StaleElementReferenceException e)
                {
                    Thread.Sleep(1000);
                    if (!wh.isElementDisplayed(selectedValue, 2))
                    {
                        ele = wh.GetWebElement(selector);
                        ele.Click();
                    }
                }
                catch (ElementClickInterceptedException e)
                {
                    Thread.Sleep(1000);
                    if (!wh.isElementDisplayed(selectedValue, 3))
                    {
                        ele = wh.GetWebElement(selector);
                        ele.Click();
                    }
                }
                catch (ElementNotInteractableException e)
                {
                    Thread.Sleep(1000);
                    if (!wh.isElementDisplayed(selectedValue, 3))
                    {
                        ele = wh.GetWebElement(selector);
                        ele.Click();
                    }
                }
                catch (WebDriverTimeoutException e)
                {
                    scr.scrollUntillLookUpEleVisible(fieldName, 100);
                    ele = wh.GetWebElement(selector);
                    ele.Click();
                }
                ele = wh.GetWebElement(selector);
                ele.SendKeys(Keys.Control + "a");
                ele.SendKeys(Keys.Backspace);

                foreach (char letter in text)
                {
                    ele.SendKeys(letter.ToString());
                    Thread.Sleep(250);
                }
                //ele.SendKeys(text);
                //wh.clickOnElement(searchIcon);
                do
                {
                    isLookUpResultsDisplayed = isElementDisplayed_New(lookUpSpan);
                    recordSelected = wh.isElementDisplayed(selectedValue, 1);



                    if (!isLookUpResultsDisplayed)
                        wh.clickOnElement(searchIcon);

                    if (isLookUpResultsDisplayed || recordSelected) break;
                    counter = counter + 1;
                } while (isLookUpResultsDisplayed || counter == 50);
                bool isSpanDisplayed = wh.isElementDisplayed(lookUpSpan, 1);
                if (isSpanDisplayed)
                {
                    wh.clickOnElement(lookUpSpan);
                }
                recordSelected = wh.isElementDisplayed(selectedValue, 2);
                if (recordSelected) break;
            }
        }

        public static bool isElementDisplayed_New(By locator)
        {
            bool isEleDisplayed = false;
            int counter = 0;
            while (!isEleDisplayed)
            {
                counter = counter + 1;
                try
                {
                    IWebElement ele = DriverHelper.Driver.FindElement(locator);
                    isEleDisplayed = ele.Displayed;
                }
                catch (Exception e)
                {
                    isEleDisplayed = false;

                }
                if (isEleDisplayed) break;
                else if (counter == 30 && isEleDisplayed == false)
                {
                    isEleDisplayed = false;
                    break;
                }
            }
            return isEleDisplayed;
        }

        public static bool isElementDisplayed_New(By locator, int timeout)
        {
            waitHelpers wh = new waitHelpers();
            bool isEleDisplayed = false;
            int counter = 0;
            while (!isEleDisplayed)
            {
                counter = counter + 1;
                wh.WaitForSeconds(timeout);
                try
                {
                    IWebElement ele = DriverHelper.Driver.FindElement(locator);
                    isEleDisplayed = ele.Displayed;
                }
                catch (Exception e)
                {
                    isEleDisplayed = false;

                }
                if (isEleDisplayed) break;
                else if (counter == 30 && isEleDisplayed == false)
                {
                    isEleDisplayed = false;
                    break;
                }
            }
            return isEleDisplayed;
        }

        public static bool waitUntillElementInvisible(By locator)
        {
            bool isEleDisplayed = false;
            while (!isEleDisplayed)
            {
                try
                {
                    IWebElement ele = DriverHelper.Driver.FindElement(locator);
                    if (ele.Displayed) isEleDisplayed = false;
                }
                catch (StaleElementReferenceException e)
                {
                    Thread.Sleep(4000);
                    waitHelpers wh = new waitHelpers();
                    if (wh.isElementDisplayed(locator, 3)) isEleDisplayed = false;
                }
                catch (ElementClickInterceptedException e)
                {
                    Thread.Sleep(4000);
                    waitHelpers wh = new waitHelpers();
                    if (wh.isElementDisplayed(locator, 3)) isEleDisplayed = false;
                }
                catch (ElementNotInteractableException e)
                {
                    Thread.Sleep(4000);
                    waitHelpers wh = new waitHelpers();
                    IWebElement ele = wh.GetWebElement(locator);
                    if (ele.Displayed) isEleDisplayed = false;
                }
                catch (NoSuchElementException e)
                {
                    isEleDisplayed = true;
                    break;
                }
                catch (Exception e)
                {
                    isEleDisplayed = false;
                    throw new Exception("Getting exception " + e.Message + " to find element");
                }
                if (isEleDisplayed) break;
            }
            return isEleDisplayed;
        }


        public static void lookUpFieldBasedOnExactText(String fieldName, String fieldTxt, int timeout)
        {
            string text = fieldTxt;
            By selector = getLocator($"//input[@aria-label='{fieldName}' and not(contains(@id,'Date'))]");
            By lookUpSpan = getLocator($"[data-id*='Lookup'] ul li span[id*='name']");
            By lookUpSpan1 = getLocator($"//span[contains(text(),'{text}')]");
            Actions act = new Actions(DriverHelper.Driver);
            IWebElement ele = waitUnitlElementToBeDisplayed(selector, timeout);
            ele.Click();
            foreach (char letter in text)
            {
                ele.SendKeys(letter.ToString());
                Thread.Sleep(500);
            }
            act.MoveToElement(ele).Pause(TimeSpan.FromMilliseconds(300)).Build().Perform();
            WaitForElementAndClick(lookUpSpan);
        }

        public static void ScrollUntilSelectorDisplayedAndSelectFirst_New(String fieldName, int timeout)
        {
            waitHelpers wh = new waitHelpers();
            By selector = getLocator($"//input[contains(@aria-label,'{fieldName}')]");
            By lookUpSpan = getLocator($"[data-id*='Lookup'] ul li span[id*='name']");
            By searchSelector = getLocator($"//button[@aria-label='Search records for {fieldName}, Lookup field']");
            //Actions act = new Actions(DriverHelper.Driver);
            //IWebElement ele = wh.GetWebElement(selector);
            //IWebElement searchEle = wh.GetWebElement(searchSelector);
            //act.MoveToElement(ele).Pause(TimeSpan.FromMilliseconds(100)).Click().Build().Perform();
            wh.isElementDisplayed(searchSelector, 90);
            wh.GetWebElement(searchSelector);
            //wh.clickOnWebElement(searchSelector);
            //wh.isElementDisplayed(lookUpSpan, 90);
            isLookUpSpanDisplayed(lookUpSpan, searchSelector);
            wh.clickOnWebElement(lookUpSpan);

        }

        public static bool isLookUpSpanDisplayed(By lookUpSpanBy, By searchSelector)
        {
            bool islookUpSpanDisplayed = false;
            int counter = 0;
            waitHelpers wh = new waitHelpers();
            do
            {
                try
                {
                    if (wh.isElementDisplayed(lookUpSpanBy, 1))
                    {

                    }
                    else
                    {
                        wh.clickOnWebElement(searchSelector);
                        islookUpSpanDisplayed = wh.isElementDisplayed(lookUpSpanBy, 5);
                        if (islookUpSpanDisplayed || counter == 25)
                            break;

                    }
                }
                catch (Exception e)
                {
                    wh.clickOnWebElement(searchSelector);
                    islookUpSpanDisplayed = wh.isElementDisplayed(lookUpSpanBy, 5);
                    if (islookUpSpanDisplayed || counter == 25)
                        break;

                }
                counter = counter + 1;

            } while (islookUpSpanDisplayed);

            return islookUpSpanDisplayed;

        }


        public static void SelectElementByText(String fieldName, string text)
        {
            By selector = getLocator($"select[aria-label*='{fieldName}']");

            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(120));
            wait.PollingInterval = TimeSpan.FromMilliseconds(200);
            wait.Until(driver => driver.FindElement(selector));
            try
            {
                new SelectElement(DriverHelper.Driver.FindElement(selector)).SelectByText(text);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void NoActionSelectElementByText(String fieldName, string text)
        {
            var comsPage = new CommonPage();
            By selector = comsPage.getNoActionElementLocator(fieldName);
            waitHelpers wh = new waitHelpers();
            wh.isElementDisplayed(selector, 120);
            IWebElement ele = wh.GetWebElement(selector);
            //WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(120));
            //wait.PollingInterval = TimeSpan.FromMilliseconds(200);
            //wait.Until(driver => driver.FindElement(selector));
            try
            {
                new SelectElement(ele).SelectByText(text);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static void NoActionClickOnToggle(String fieldName)
        {
            By selector = By.XPath($"//*[text()='{fieldName}']/ancestor::*//button[@aria-checked='false']");
            waitHelpers wh = new waitHelpers();
            wh.clickOnWebElement(selector);
        }


        public static void clickElementAndSelectText(String fieldName, string text)
        {
            waitHelpers wh = new waitHelpers();
            By clickEle = getLocator($"button[aria-label*='{fieldName}']");
            By textEle = getLocator($"//*[@role='option' and text()='{text}']");
            ScrollToElement(wh.GetWebElement(clickEle));
            wh.clickOnWebElement(clickEle);
            wh.clickOnWebElement(textEle);
        }


        public static void scrollToBtnElementAndClick(String fieldName)
        {
            waitHelpers wh = new waitHelpers();
            By selector = getLocator($"button[aria-label*='{fieldName}']");
            wh.isElementDisplayed(selector, 30);
            Actions act = new Actions(DriverHelper.Driver);
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(Config.DefaultDriverTimeOut));
            wait.PollingInterval = TimeSpan.FromMilliseconds(200);
            IWebElement ele = wh.GetWebElement(selector);// DriverHelper.Driver.FindElement(selector);
            wh.WaitTillElementDisplayed(selector, 30);
            //wait.Until(driver => ele.Displayed);
            act.ScrollToElement(ele).Pause(TimeSpan.FromMilliseconds(500)).Click(ele).Pause(TimeSpan.FromMilliseconds(500)).Build().Perform();
        }

        public static void scrollToBtnWithTitleElementAndClick(String fieldName)
        {
            waitHelpers wh = new waitHelpers();
            By selector = getLocator($"button[title*='{fieldName}']");
            wh.isElementDisplayed(selector, 30);
            Actions act = new Actions(DriverHelper.Driver);
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(Config.DefaultDriverTimeOut));
            wait.PollingInterval = TimeSpan.FromMilliseconds(200);
            IWebElement ele = DriverHelper.Driver.FindElement(selector);
            wait.Until(driver => ele.Displayed);
            act.ScrollToElement(ele).Pause(TimeSpan.FromMilliseconds(500)).Click(ele).Pause(TimeSpan.FromMilliseconds(500)).Build().Perform();
        }

        public static void scrollToBtnElementAndClick(By moreOptions, String fieldName)
        {
            Actions act = new Actions(DriverHelper.Driver);
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(120));
            By selector = getLocator($"button[aria-label*='{fieldName}']");
            try
            {
                IWebElement ele = DriverHelper.Driver.FindElement(selector);
                wait.Until(driver => ele.Displayed);
                act.ScrollToElement(ele).Pause(TimeSpan.FromMilliseconds(500)).Click(ele).Pause(TimeSpan.FromMilliseconds(500)).Build().Perform();
            }
            catch (Exception e)
            {
                IWebElement moreOptioneEle = DriverHelper.Driver.FindElement(moreOptions);
                wait.Until(driver => moreOptioneEle.Displayed);
                moreOptioneEle.Click();
                IWebElement ele = DriverHelper.Driver.FindElement(selector);
                wait.Until(driver => ele.Displayed);
                act.ScrollToElement(ele).Pause(TimeSpan.FromMilliseconds(500)).Click(ele).Pause(TimeSpan.FromMilliseconds(500)).Build().Perform();
            }
        }
        public static string getwindowSection()
        {
            waitHelpers wh = new waitHelpers();
            By gridRootSec = By.CssSelector("div[data - id = 'GridRoot'] > div[role = 'presentation']");
            By tabSec = By.CssSelector("[id *= 'tab-section']");
            By dashBoardSec = By.CssSelector("[id='DashboardScrollView']");
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(60));
            wait.Until(d =>
                ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete")
            );
            Console.WriteLine("Page fully loaded.");
            String currentScreenSec = "";
            if (wh.isElementDisplayed(tabSec, 20))
            {
                currentScreenSec = "Tab";
                Console.WriteLine("Current screensection of application " + currentScreenSec);
            }
            else if (wh.isElementDisplayed(dashBoardSec, 20))
            {
                currentScreenSec = "Dashboard";
                Console.WriteLine("Current screensection of application " + currentScreenSec);
            }
            else if (wh.isElementDisplayed(gridRootSec, 20))
            {
                currentScreenSec = "Grid";
                Console.WriteLine("Current screensection of application " + currentScreenSec);
            }
            return currentScreenSec;
        }

        public static IWebElement getwindowSectionEle()
        {
            waitHelpers wh = new waitHelpers();
            IWebElement ele = null;

            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(60));
            wait.Until(d =>
                ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete")
            );
            Console.WriteLine("Page fully loaded.");
            String currentScreenSec = "";
            if (wh.isElementDisplayed(tabSec, 20))
            {
                currentScreenSec = "Tab";
                ele = wh.GetWebElement(tabSec);
                Console.WriteLine("Current screensection of application " + currentScreenSec);
            }
            else if (wh.isElementDisplayed(dashBoardSec, 20))
            {
                currentScreenSec = "Dashboard";
                ele = wh.GetWebElement(dashBoardSec);
                Console.WriteLine("Current screensection of application " + currentScreenSec);
            }
            else if (wh.isElementDisplayed(gridRootSec, 20))
            {
                currentScreenSec = "Grid";
                ele = wh.GetWebElement(gridRootSec);
                Console.WriteLine("Current screensection of application " + currentScreenSec);
            }
            return ele;
        }
        public static void ScrollToEleByWindow(String fieldName)
        {
            waitHelpers wh = new waitHelpers();
            CommonPage cp = new CommonPage();
            bool isReqEleDisplayed = false;
            IJavaScriptExecutor js = DriverHelper.Driver as IJavaScriptExecutor;
            By selector = getLocator($"[aria-label*='{fieldName}']");

            int counter = 0;
            string currentScreenSec = getwindowSection();
            do
            {
                isReqEleDisplayed = wh.isElementDisplayed(selector, 10);
                if (isReqEleDisplayed)
                {
                    IWebElement element = wh.getElement(selector, 5);
                    js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
                    break;
                }
                else
                {
                    switch (currentScreenSec)
                    {
                        case "Tab":
                            IWebElement scrollEle = wh.GetWebElement(tabSec);
                            Actions actions = new Actions(DriverHelper.Driver);
                            //actions.ClickAndHold(scrollEle).MoveByOffset(0, 100).Release().Perform();
                            js.ExecuteScript("arguments[0].scrollTop = arguments[0].scrollHeight;", scrollEle);
                            cp.waitTillPageLoading(60);
                            js.ExecuteScript("arguments[0].scrollTop = 0", scrollEle);

                            break;
                        case "Dashboard":
                            scrollEle = wh.GetWebElement(dashBoardSec);
                            js.ExecuteScript("arguments[0].scrollTop = arguments[0].scrollHeight;", scrollEle);
                            cp.waitTillPageLoading(60);
                            js.ExecuteScript("arguments[0].scrollTop = 0", scrollEle);
                            break;
                        case "Grid":
                            scrollEle = wh.GetWebElement(gridRootSec);
                            js.ExecuteScript("arguments[0].scrollTop = arguments[0].scrollHeight;", scrollEle);
                            cp.waitTillPageLoading(60);
                            js.ExecuteScript("arguments[0].scrollTop = 0", scrollEle);
                            break;
                    }
                    counter = counter + 1;
                }
                counter = counter + 1;
                if (counter == 20) break;
                isReqEleDisplayed = wh.isElementDisplayed(selector, 10);
            } while (!isReqEleDisplayed);
        }
        public static void scrollToBtnElement(String fieldName)
        {
            waitHelpers wh = new waitHelpers();
            CommonPage cp = new CommonPage();
            By selector = getLocator($"[aria-label*='{fieldName}']");
            Actions act = new Actions(DriverHelper.Driver);
            wh.isElementDisplayed(selector, 30);
            wh.GetWebElementVisble(selector);
            //cp.waitforTopMenuBarLoading();
            IWebElement ele = wh.GetWebElementByTimeOut(selector, 20);
            try
            {
                ele = wh.GetWebElementByTimeOut(selector, 20);
                act.ScrollToElement(ele).Pause(TimeSpan.FromMilliseconds(200)).Build().Perform();

            }
            catch (StaleElementReferenceException e)
            {
                Thread.Sleep(2000);
                ele = wh.GetWebElement(selector);
                IJavaScriptExecutor js = DriverHelper.Driver as IJavaScriptExecutor;
                js.ExecuteScript("arguments[0].scrollIntoView(true);", ele);
                //act.ScrollToElement(ele).Pause(TimeSpan.FromSeconds(5)).Build().Perform();
            }
            catch (ElementClickInterceptedException e)
            {
                Thread.Sleep(2000);
                ele = wh.GetWebElement(selector);
                act.ScrollToElement(ele).Pause(TimeSpan.FromMilliseconds(200)).Build().Perform();
            }
            catch (ElementNotInteractableException e)
            {
                Thread.Sleep(2000);
                ele = wh.GetWebElement(selector);
                act.ScrollToElement(ele).Pause(TimeSpan.FromMilliseconds(200)).Build().Perform();
            }
            catch (NoSuchElementException e)
            {
                throw new Exception("Unable to find specific element with exception : " + e.Message);
            }
            catch (Exception e)
            {
                throw new Exception("Exception details : " + e.Message);
            }
        }

        public static void clickOnElement(String fieldName)
        {
            waitHelpers wh = new waitHelpers();
            By selector = getLocator($"[aria-label*='{fieldName}']");
            Actions act = new Actions(DriverHelper.Driver);
            wh.isElementDisplayed(selector, 120);
            wh.GetWebElementVisble(selector);
            //cp.waitforTopMenuBarLoading();
            IWebElement ele = wh.GetWebElement(selector);
            try
            {
                wh.clickOnElement(selector);

            }
            catch (StaleElementReferenceException e)
            {
                Thread.Sleep(2000);
                ele = wh.GetWebElement(selector);
                wh.clickOnElement(selector);


            }
            catch (ElementClickInterceptedException e)
            {
                CommonPage commonpage = new CommonPage();
                commonpage.waitTillappProgressIndicatorDisaddpears_();
                commonpage.waitTillSavingDisaddpears();

                ele = wh.GetWebElement(selector);
                wh.clickOnElement(selector);
            }
            catch (ElementNotInteractableException e)
            {
                Thread.Sleep(2000);
                ele = wh.GetWebElement(selector);
                wh.clickOnElement(selector);
            }
            catch (NoSuchElementException e)
            {
                throw new Exception("Unable to find specific element with exception : " + e.Message);
            }
            catch (Exception e)
            {
                throw new Exception("Exception details : " + e.Message);
            }
        }

        public static void scrollToLabelElement(String fieldName)
        {
            waitHelpers wh = new waitHelpers();
            By selector = By.XPath($"//label[text()='{fieldName}']");
            Actions act = new Actions(DriverHelper.Driver);
            IWebElement ele = null;
            bool isDisplayed = wh.elementDisplayed(selector, 1);
            int i = 0;
            if (!isDisplayed)
            {
                while (!isDisplayed)
                {
                    ((IJavaScriptExecutor)DriverHelper.Driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight - 150)");
                    i = i + 1;
                    if (i > 15)
                        break;
                }
                isDisplayed = wh.elementDisplayed(selector, 1);
                if (isDisplayed)
                    act.MoveToElement(ele).Pause(TimeSpan.FromMilliseconds(100)).Build().Perform();

            }
            else
            {
                ele = wh.GetWebElement(selector);
                act.MoveToElement(ele).Pause(TimeSpan.FromMilliseconds(100)).Build().Perform();

            }

        }

        public static void setFieldValue(String fieldName, String sheetName, String RowID)
        {
            string TestDataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + Config.TestDataExcelFilePath);
            var excelUtility = new ExcelTestDataUtility(TestDataFilePath);
            string value = excelUtility.getFieldTestData(fieldName, sheetName, RowID);

            By selector = getLocator($"//*[contains(@aria-label,'{fieldName}')]");
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(120));
            Actions act = new Actions(DriverHelper.Driver);
            wait.PollingInterval = TimeSpan.FromMilliseconds(200);
            wait.Until(driver => DriverHelper.Driver.FindElement(selector).Displayed);
            IWebElement ele = DriverHelper.Driver.FindElement(selector);
            act.ScrollToElement(ele).Pause(TimeSpan.FromMilliseconds(500)).Build().Perform();
            var executor = ((IJavaScriptExecutor)DriverHelper.Driver);
            executor.ExecuteScript("arguments[0].setAttribute('value', '" + value + "')", ele);
        }

        public static void setFieldValue(String fieldName, Dictionary<string, string> testdata)
        {
            string value = testdata[fieldName];
            var currentDate = "";
            if (fieldName.Contains("Date"))
            {
                if (!value.Contains("/") || !value.Contains("\\"))
                {
                    currentDate = DateTime.Now.AddDays(Convert.ToDouble(value)).ToString("dd/MM/yyyy");
                }
                else
                {
                    currentDate = value;
                }
            }
            By selector = getLocator($"//*[contains(@aria-label,'{fieldName}')]");
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(120));
            Actions act = new Actions(DriverHelper.Driver);
            wait.PollingInterval = TimeSpan.FromMilliseconds(200);
            wait.Until(driver => DriverHelper.Driver.FindElement(selector).Displayed);
            IWebElement ele = DriverHelper.Driver.FindElement(selector);
            act.ScrollToElement(ele).Pause(TimeSpan.FromMilliseconds(500)).Build().Perform();
            var executor = ((IJavaScriptExecutor)DriverHelper.Driver);
            executor.ExecuteScript("arguments[0].setAttribute('value', '" + currentDate + "')", ele);
        }

        public static void enterDataOnDocumentDialog(string fieldName, Dictionary<string, string> testdata)
        {
            String text = testdata[fieldName];
            waitHelpers wh = new waitHelpers();
            //(//*[contains(@class,"ms-Dialog-inner inner")]//*[contains(text(),'Document Type')]/following-sibling::*//input)[1]
            By fieldLocator = By.XPath($"(//*[contains(@class,'ms-Dialog-inner inner')]//*[contains(text(),'{fieldName}')]/following-sibling::*//input)[1]");
            wh.GetWebElement(fieldLocator).SendKeys(text);
        }


        public static bool IsEnabled(this IWebElement element)
        {
            try
            {
                Log.Information($"The element {element}is enabled: {element.Enabled}");
                return element.Enabled;
            }
            catch (Exception e)
            {
                Log.Information($"The element {element} is not enabled with error {e.Message}");
                Console.WriteLine(e.Message);
                return false;
            }

        }
        public static void ScrollToheading(String Headingname)
        {

            waitHelpers wh = new waitHelpers();
            Scroll scr = new Scroll();
            By headingLoc = By.XPath($"//div[@role='presentation']//h3[contains(text(),'{Headingname}')]");
            bool isReqEleDisplayed = false;
            IJavaScriptExecutor js = DriverHelper.Driver as IJavaScriptExecutor;
            do
            {
                if (isReqEleDisplayed)
                {
                    IWebElement element = wh.getElement(headingLoc, 5);
                    js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
                    break;
                }
                else
                {
                    IWebElement scrollEle = getwindowSectionEle();
                    scr.ScrollElement(scrollEle, 150);

                }
                isReqEleDisplayed = wh.isElementDisplayedInMilliSec(headingLoc, 30);
            } while (!isReqEleDisplayed);
        }

        public static void ScrollToSectionEle(String fieldName)
        {
            waitHelpers wh = new waitHelpers();
            bool isReqEleDisplayed = false;
            IJavaScriptExecutor js = DriverHelper.Driver as IJavaScriptExecutor;
            By selector = getLocator($"[aria-label*='{fieldName}']");
            By gridRootSec = By.CssSelector("div[data - id = 'GridRoot'] > div[role = 'presentation']");
            By tabSec = By.CssSelector("[id *= 'tab-section']");
            By dashBoardSec = By.CssSelector("[id='DashboardScrollView']");
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(60));
            wait.Until(d =>
                ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete")
            );
            Console.WriteLine("Page fully loaded.");
            String currentScreenSec = "";
            if (wh.isElementDisplayed(tabSec, 20))
            {
                currentScreenSec = "Tab";
                Console.WriteLine("Current screensection of application " + currentScreenSec);
            }
            else if (wh.isElementDisplayed(dashBoardSec, 20))
            {
                currentScreenSec = "Dashboard";
                Console.WriteLine("Current screensection of application " + currentScreenSec);
            }
            else if (wh.isElementDisplayed(gridRootSec, 20))
            {
                currentScreenSec = "Grid";
                Console.WriteLine("Current screensection of application " + currentScreenSec);
            }
            int counter = 0;

            do
            {
                isReqEleDisplayed = wh.isElementDisplayed(selector, 10);
                if (isReqEleDisplayed)
                {
                    IWebElement element = wh.getElement(selector, 5);
                    js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
                    break;
                }
                else
                {
                    switch (currentScreenSec)
                    {
                        case "Tab":
                            IWebElement scrollEle = wh.GetWebElement(tabSec);
                            js.ExecuteScript("arguments[0].scrollTop = arguments[0].scrollHeight;", scrollEle);
                            break;
                        case "Dashboard":
                            scrollEle = wh.GetWebElement(dashBoardSec);
                            js.ExecuteScript("arguments[0].scrollTop = arguments[0].scrollHeight;", scrollEle);
                            break;
                        case "Grid":
                            scrollEle = wh.GetWebElement(gridRootSec);
                            js.ExecuteScript("arguments[0].scrollTop = arguments[0].scrollHeight;", scrollEle);
                            break;
                    }
                    counter = counter + 1;

                }
                if (counter == 20) break;
                SeleniumExtensions.WaitForPageLoad();
                isReqEleDisplayed = wh.isElementDisplayedInMilliSec(selector, 30);
            } while (!isReqEleDisplayed);
        }

        public static void scrollToBtnEleBasedOnLabelAndClick(By locator)
        {
            waitHelpers wh = new waitHelpers();
            WaitForReadyStateToComplete();
            Actions act = new Actions(DriverHelper.Driver);
            IWebElement ele = wh.GetWebElement(locator);
            wh.isElementDisplayed(locator, 120);
            IJavaScriptExecutor js = DriverHelper.Driver as IJavaScriptExecutor;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", ele);
            act.ScrollToElement(ele).Pause(TimeSpan.FromMilliseconds(500)).Click(ele).Pause(TimeSpan.FromMilliseconds(500)).Build().Perform();
        }

        public static void scrollToBtnBasedOnLabelAndClick(String fieldName)
        {
            waitHelpers wh = new waitHelpers();
            WaitForReadyStateToComplete();
            By selector = getLocator($"//*[text()='{fieldName}']/ancestor::button");
            Actions act = new Actions(DriverHelper.Driver);
            IWebElement ele = wh.GetWebElement(selector);
            wh.isElementDisplayed(selector, 120);
            IJavaScriptExecutor js = DriverHelper.Driver as IJavaScriptExecutor;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", ele);
            act.ScrollToElement(ele).Pause(TimeSpan.FromMilliseconds(500)).Click(ele).Pause(TimeSpan.FromMilliseconds(500)).Build().Perform();
        }


        public static void scrollToBtnBasedOnLabelAndClick_recon(String fieldName)
        {
            waitHelpers wh = new waitHelpers();
            WaitForReadyStateToComplete();
            //By selector = getLocator($"//*[text()='{fieldName}']/ancestor::button");
            By locator = By.XPath($"//span[contains(@class,'ms-Button-label') and contains(normalize-space(text()),'{fieldName}')]");
            Actions act = new Actions(DriverHelper.Driver);
            IWebElement ele = wh.GetWebElement(locator);
            wh.isElementDisplayed(locator, 120);
            IJavaScriptExecutor js = DriverHelper.Driver as IJavaScriptExecutor;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", ele);
            act.ScrollToElement(ele).Pause(TimeSpan.FromMilliseconds(500)).Click(ele).Pause(TimeSpan.FromMilliseconds(500)).Build().Perform();
        }


        public static void scrollToBtnBasedOnLabelAndClick_New(String fieldName)
        {
            waitHelpers wh = new waitHelpers();
            By selector = getLocator($"//Button[text()='{fieldName}']");
            wh.clickOnElement(selector);
            //Actions act = new Actions(DriverHelper.Driver);
            //WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(120));
            //wait.PollingInterval = TimeSpan.FromMilliseconds(200);
            //IWebElement ele = DriverHelper.Driver.FindElement(selector);
            //wait.Until(driver => ele.Displayed);
            //act.ScrollToElement(ele).Pause(TimeSpan.FromMilliseconds(500)).Click(ele).Pause(TimeSpan.FromMilliseconds(500)).Build().Perform();
            //WaitForReadyStateToComplete();
        }


        public static void scrollToBtnContainsLabelAndClick(String fieldName, String sectionName)
        {
            By selector = getLocator($"//div[@aria-label='{sectionName}']//*[contains(text(),'{fieldName}')]/ancestor::button");
            Actions act = new Actions(DriverHelper.Driver);
            waitHelpers wh = new waitHelpers();
            IWebElement ele = wh.GetWebElement(selector); ;
            act.ScrollToElement(ele).Pause(TimeSpan.FromMilliseconds(500)).Click(ele).Pause(TimeSpan.FromMilliseconds(500)).Build().Perform();
        }

        public static void scrollToCheckBoxAndClick(String fieldName)
        {
            By selector = getLocator($"//input[@value='{fieldName}']/ancestor::div[@role='row']//div[@role='checkbox']");
            Actions act = new Actions(DriverHelper.Driver);
            waitHelpers wh = new waitHelpers();
            IWebElement ele = wh.GetWebElement(selector); ;
            act.ScrollToElement(ele).Pause(TimeSpan.FromMilliseconds(500)).Click(ele).Pause(TimeSpan.FromMilliseconds(500)).Build().Perform();
        }

        public static void scrollToBtnBasedOnLabelAndClick(String fieldName, String sectionName)
        {
            WaitForReadyStateToComplete();
            ////div[@aria-label='Plot Manager']//*[text()='Save']/ancestor::button
            By selector = getLocator($"//div[@aria-label='{sectionName}']//*[text()='{fieldName}']/ancestor::button");
            Actions act = new Actions(DriverHelper.Driver);
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(120));
            wait.PollingInterval = TimeSpan.FromMilliseconds(200);
            IWebElement ele = DriverHelper.Driver.FindElement(selector);
            wait.Until(driver => ele.Displayed);
            act.ScrollToElement(ele).Pause(TimeSpan.FromMilliseconds(500)).Click(ele).Pause(TimeSpan.FromMilliseconds(500)).Build().Perform();
            WaitForReadyStateToComplete();
        }

        public static void scrollToBtnBasedOnLabelAndClickOnHereditamentDialog(String fieldName)
        {
            WaitForReadyStateToComplete();
            waitHelpers wh = new waitHelpers();
            By selector = getLocator($"//div[contains(@class,'ms-OverflowSet ms-CommandBar-primaryCommand primarySet')]//*[text()='{fieldName}']/ancestor::button");
            By expanderSelector = getLocator($"[class*='ms-DetailsHeader-cell cellIsGroupExpander']");
            if (wh.GetWebElement(expanderSelector).GetAttribute("aria-expanded").Equals("false"))
            {
                wh.clickOnElement(expanderSelector);
            }
            wh.clickOnElement(selector);
            //Actions act = new Actions(DriverHelper.Driver);
            //WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(120));
            //wait.PollingInterval = TimeSpan.FromMilliseconds(200);
            //IWebElement ele = DriverHelper.Driver.FindElement(selector);
            //wait.Until(driver => ele.Displayed);
            //act.ScrollToElement(ele).Pause(TimeSpan.FromMilliseconds(500)).Click(ele).Pause(TimeSpan.FromMilliseconds(500)).Build().Perform();
            WaitForReadyStateToComplete();
        }

        public static void scrollToBtnBasedOnLabelAndClick(String fieldName, int position)
        {
            WaitForReadyStateToComplete();
            By selector = getLocator($"(//*[text()='{fieldName}']/ancestor::button)[{position}]");
            Actions act = new Actions(DriverHelper.Driver);
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(120));
            wait.PollingInterval = TimeSpan.FromMilliseconds(200);
            IWebElement ele = DriverHelper.Driver.FindElement(selector);
            wait.Until(driver => ele.Displayed);
            act.ScrollToElement(ele).Pause(TimeSpan.FromMilliseconds(500)).Click(ele).Pause(TimeSpan.FromMilliseconds(500)).Build().Perform();
            WaitForReadyStateToComplete();
        }

        public static void scrollToBtnBasedOnLabelAndJSClick(String fieldName)
        {
            By selector = getLocator($"//*[text()='{fieldName}']/ancestor::button");
            Actions act = new Actions(DriverHelper.Driver);
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(120));
            wait.PollingInterval = TimeSpan.FromMilliseconds(200);
            //IWebElement ele = DriverHelper.Driver.FindElement(selector);
            IWebElement ele = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(selector));
            //wait.Until(driver => ele.Displayed);
            //act.ScrollToElement(ele).Pause(TimeSpan.FromMilliseconds(100)).Build().Perform();
            //ele.ClickUsingJavascript();
            //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(selector));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(ele)).Click();


        }

        public static void scrollToBtnEleAndClick(String fieldName)
        {
            By selector = getLocator($"//button[text()='{fieldName}']");
            Actions act = new Actions(DriverHelper.Driver);
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(120));
            wait.PollingInterval = TimeSpan.FromMilliseconds(200);
            IWebElement ele = DriverHelper.Driver.FindElement(selector);
            wait.Until(driver => ele.Displayed);
            act.ScrollToElement(ele).Pause(TimeSpan.FromMilliseconds(50)).Click(ele).Pause(TimeSpan.FromMilliseconds(50)).Build().Perform();
        }

        public static void scrollToToggleBtnBasedOnLabelAndClick(String fieldName)
        {
            WaitForReadyStateToComplete();
            waitHelpers wh = new waitHelpers();
            By selector = getLocator($"[aria-label*='{fieldName}'][class*='Switch']");
            Actions act = new Actions(DriverHelper.Driver);
            var ele = wh.GetWebElement(selector);
            act.ScrollToElement(ele).Pause(TimeSpan.FromMilliseconds(50)).Click(ele).Pause(TimeSpan.FromMilliseconds(50)).Build().Perform();

        }

        public static void scrollToToggleBtnonDialogBasedOnLabelAndClick(String fieldName)
        {
            WaitForReadyStateToComplete();
            waitHelpers wh = new waitHelpers();
            By selector = getLocator($"[aria-labelledby*='{fieldName}']");
            Actions act = new Actions(DriverHelper.Driver);
            var ele = wh.GetWebElement(selector);
            act.ScrollToElement(ele).Pause(TimeSpan.FromMilliseconds(50)).Click(ele).Pause(TimeSpan.FromMilliseconds(50)).Build().Perform();
        }
        public static void sendKeysFieldValue(String fieldName, String sheetName, String RowID)
        {
            string TestDataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + Config.TestDataExcelFilePath);
            var excelUtility = new ExcelTestDataUtility(TestDataFilePath);
            string value = excelUtility.getFieldTestData(fieldName, sheetName, RowID);
            By selector = getLocator($"//*[contains(@aria-label,'{fieldName}')]");
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(120));
            //Actions act = new Actions(DriverHelper.Driver);
            wait.PollingInterval = TimeSpan.FromMilliseconds(200);
            wait.Until(driver => DriverHelper.Driver.FindElement(selector).Displayed);
            IWebElement ele = DriverHelper.Driver.FindElement(selector);
            ele.ClearAndSendkeys(value);
            //ele.SendKeys(value);
            //act.ScrollToElement(ele).Pause(TimeSpan.FromMilliseconds(500)).Click().SendKeys(value).Build().Perform();
        }



        public static void sendKeysFieldValue(String fieldName, Dictionary<String, String> testData)
        {
            string value = testData[fieldName];
            waitHelpers wh = new waitHelpers();
            By selector = getLocator($"//*[contains(@aria-label,'{fieldName}')]");
            wh.isElementDisplayed(selector, 30);
            IWebElement ele = wh.GetWebElement(selector);
            Actions act = new Actions(DriverHelper.Driver);
            act.ScrollToElement(ele).Build().Perform();
            //ele.ClearAndSendkeys(value);
            ele.Click();
            ele.SendKeys(Keys.Control + "a");
            ele.SendKeys(Keys.Backspace);
            ele.SendKeys(value);

        }

        public static void sendKeysTextAreaFieldValue_1(String fieldName, Dictionary<String, String> testData)
        {
            string value = testData[fieldName];
            waitHelpers wh = new waitHelpers();
            By selector = getLocator($"//textarea[contains(@aria-label,'{fieldName}')]");
            wh.isElementDisplayed(selector, 30);
            IWebElement ele = wh.GetWebElement(selector);
            Actions act = new Actions(DriverHelper.Driver);
            act.ScrollToElement(ele).Build().Perform();
            ele.ClearAndSendkeys(value);
        }

        public static void sendKeysFieldValue(int position, String fieldName, Dictionary<String, String> testData)
        {
            string value = testData[fieldName];
            waitHelpers wh = new waitHelpers();
            By selector = getLocator($"(//*[contains(@aria-label,'{fieldName}')])[{position}]");
            IWebElement ele = wh.GetWebElement(selector);
            //WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(120));
            Actions act = new Actions(DriverHelper.Driver);
            act.ScrollToElement(ele).Build().Perform();
            ele.ClearAndSendkeys(value);
        }

        public static string sendKeysFieldValue(String fieldName, String value)
        {
            waitHelpers wh = new waitHelpers();
            By selector = getLocator($"//*[contains(@aria-label,'{fieldName}')]");
            IWebElement ele = wh.GetWebElement(selector);
            Actions act = new Actions(DriverHelper.Driver);
            act.ScrollToElement(ele).Build().Perform();
            ele.ClearAndSendkeys(value);
            return value;
        }

        public static string sendKeysFieldValue_sameref(String fieldName, String value)
        {
            waitHelpers wh = new waitHelpers();
            By selector = getLocator($"//*[contains(@aria-label,'{fieldName}')]");
            IWebElement ele = wh.GetWebElement(selector);
            Actions act = new Actions(DriverHelper.Driver);
            act.ScrollToElement(ele).Build().Perform();
            ele.ClearAndSendkeys(value);
            By selector_ = getLocator($"//*[contains(@aria-label,'Reason For Validation')]");
            wh.isElementDisplayed(selector, 30);
            IWebElement ele_ = wh.GetWebElement(selector_);
            ele_.Click();
            By errorMessageBy = By.XPath($"//span[@data-id='voa_proposedbareferencenumber-error-message']");
            bool isErrorDisplayed = wh.isElementDisplayed(errorMessageBy, 5);
            //Assert.True(isErrorDisplayed, "Error message dispalyed");
            Log.Information($"The error message {isErrorDisplayed} is displayed");
            return value;
        }

        public static string sendKeysFieldValue_secondref(String fieldName, String value)
        {
            waitHelpers wh = new waitHelpers();
            //By selector = getLocator($"//*[contains(@aria-label,'{fieldName}')]");
            //By selector = getLocator($"//*[@aria-label ='{fieldName}' and @title ='Select to enter data']");
            By selector = getLocator($"(//*[@aria-label ='{fieldName}' and @type ='text'])[2]");
            IWebElement ele = wh.GetWebElement(selector);
            Actions act = new Actions(DriverHelper.Driver);
            act.ScrollToElement(ele).Build().Perform();
            //ele.ClearAndSendkeys(value);
            ele.SendKeys(value);
            return value;
        }

        public static string sendKeysFieldValue(String fieldName, String value, int position)
        {
            waitHelpers wh = new waitHelpers();
            By selector = getLocator($"(//*[contains(@aria-label,'{fieldName}')])[{position}]");
            IWebElement ele = wh.GetWebElement(selector);
            Actions act = new Actions(DriverHelper.Driver);
            act.ScrollToElement(ele).Build().Perform();
            ele.ClearAndSendkeys(value);
            return value;
        }

        public static string sendKeysRandomFieldValue(string fieldName)
        {
            waitHelpers wh = new waitHelpers();
            var timestamp = DateTimeOffset.Now.ToString("yyyyMMddHHmmssffff");
            By selector = getLocator($"//input[contains(@aria-label,'{fieldName}')]");
            IWebElement ele = wh.GetWebElement(selector);
            Actions act = new Actions(DriverHelper.Driver);
            act.ScrollToElement(ele).Pause(TimeSpan.FromMilliseconds(500)).Build().Perform();
            var executor = ((IJavaScriptExecutor)DriverHelper.Driver);
            executor.ExecuteScript("arguments[0].scrollIntoView(true);", ele);
            ele.ClearAndSendkeys(timestamp + "TT");
            return timestamp + "TT";

            //WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(120));
            //wait.PollingInterval = TimeSpan.FromMilliseconds(200);
            //wait.Until(driver => DriverHelper.Driver.FindElement(selector).Displayed);
            //IWebElement ele = DriverHelper.Driver.FindElement(selector);
            //act.ScrollToElement(ele).Pause(TimeSpan.FromMilliseconds(500)).Build().Perform();

        }
        public static string sendKeysRandomFieldValue_BAref(string fieldName)
        {
            waitHelpers wh = new waitHelpers();
            var timestamp = DateTimeOffset.Now.ToString("yyyyMMddHHmmssffff");
            By selector = getLocator($"//input[contains(@aria-label,'{fieldName}')]");
            IWebElement ele = wh.GetWebElement(selector);
            Actions act = new Actions(DriverHelper.Driver);
            act.ScrollToElement(ele).Pause(TimeSpan.FromMilliseconds(500)).Build().Perform();
            var executor = ((IJavaScriptExecutor)DriverHelper.Driver);
            executor.ExecuteScript("arguments[0].scrollIntoView(true);", ele);
            ele.ClearAndSendkeys(timestamp + "TT");
            By selector_ = getLocator($"//*[contains(@aria-label,'Reason For Validation')]");
            wh.isElementDisplayed(selector, 30);
            IWebElement ele_ = wh.GetWebElement(selector_);
            ele_.Click();
            return timestamp + "TT";


        }

        public static string sendKeysCommunityFieldValue(string fieldName)
        {
            waitHelpers wh = new waitHelpers();
            var timestamp = DateTimeOffset.Now.ToString("yyyyMMddHHmmssffff");
            By selector = getLocator($"//input[contains(@aria-label,'{fieldName}')]");
            IWebElement ele = wh.GetWebElement(selector);
            Actions act = new Actions(DriverHelper.Driver);
            act.ScrollToElement(ele).Pause(TimeSpan.FromMilliseconds(500)).Build().Perform();
            var executor = ((IJavaScriptExecutor)DriverHelper.Driver);
            executor.ExecuteScript("arguments[0].scrollIntoView(true);", ele);
            ele.ClearAndSendkeys("32343" + timestamp + "TT");
            return timestamp + "TT";

        }

        public static string sendKeysCommunityFieldValue(string BA, string town, string fieldName)
        {
            waitHelpers wh = new waitHelpers();
            var timestamp = DateTimeOffset.Now.ToString("yyyyMMddHHmmssffff");
            By selector = getLocator($"//input[contains(@aria-label,'{fieldName}')]");
            IWebElement ele = wh.GetWebElement(selector);
            Actions act = new Actions(DriverHelper.Driver);
            act.ScrollToElement(ele).Pause(TimeSpan.FromMilliseconds(500)).Build().Perform();
            var executor = ((IJavaScriptExecutor)DriverHelper.Driver);
            executor.ExecuteScript("arguments[0].scrollIntoView(true);", ele);
            string communityCode = null;
            switch (BA)
            {
                case "Swansea":
                    if (string.Equals(town, "PENLLERGAER", StringComparison.OrdinalIgnoreCase))
                    {
                        communityCode = "37088";

                    }
                    else
                    {
                        communityCode = "37051";

                    }
                    //"37092";
                    break;
                case "Cardiff":
                    communityCode = "32343";
                    break;
                default:
                    communityCode = "32343";
                    break;

            }
            ele.ClearAndSendkeys(communityCode + timestamp + "TT");
            return timestamp;

        }

        public static string sendKeysRandomFieldValue(string fieldName, int position)
        {
            waitHelpers wh = new waitHelpers();
            var timestamp = DateTimeOffset.Now.ToString("yyyyMMddHHmmssffff");
            By selector = getLocator($"(//input[contains(@aria-label,'{fieldName}')])[{position}]");
            IWebElement ele = wh.GetWebElement(selector);
            Actions act = new Actions(DriverHelper.Driver);
            act.ScrollToElement(ele).Pause(TimeSpan.FromMilliseconds(500)).Build().Perform();
            var executor = ((IJavaScriptExecutor)DriverHelper.Driver);
            executor.ExecuteScript("arguments[0].scrollIntoView(true);", ele);
            ele.ClearAndSendkeys(timestamp + "TT");
            return timestamp + "TT";
        }


        public static void sendKeysTextAreaFieldValue(String fieldName, Dictionary<String, String> testData)
        {
            string value = "Automation Testing"; //testData[fieldName];
            waitHelpers wh = new waitHelpers();
            By selector = getLocator($"//textarea[contains(@aria-label,'{fieldName}')]");
            if (wh.GetWebElement(selector, 0))
            {
                IWebElement ele = wh.GetWebElement(selector);
                ele.ClearAndSendkeys(value);
            }
            else
            {
                Actions act = new Actions(DriverHelper.Driver);
                while (!wh.GetWebElement(selector, 0))
                {
                    IJavaScriptExecutor js = (IJavaScriptExecutor)DriverHelper.Driver;
                    js.ExecuteScript("window.scrollBy(0,500)", "");
                }
                IWebElement ele = wh.GetWebElement(selector);
                ele.ClearAndSendkeys(value);
            }
            //WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(120));
            //Actions act = new Actions(DriverHelper.Driver);
            //wait.PollingInterval = TimeSpan.FromMilliseconds(200);
            //wait.Until(driver => DriverHelper.Driver.FindElement(selector).Displayed);
            //IWebElement ele = DriverHelper.Driver.FindElement(selector);
            //act.ScrollToElement(ele).Pause(TimeSpan.FromMilliseconds(500)).Build().Perform();
            //ele.ClearAndSendkeys(value);
            //ele.SendKeys(value);
            //act.ScrollToElement(ele).Pause(TimeSpan.FromMilliseconds(500)).Click().SendKeys(value).Build().Perform();
        }

        public static void sendKeysTextAreaFieldValue_1(String fieldName, String testData)
        {
            waitHelpers wh = new waitHelpers();
            By selector = getLocator($"//textarea[contains(@aria-label,'{fieldName}')]");
            if (wh.GetWebElement(selector, 0))
            {
                IWebElement ele = wh.GetWebElement(selector);
                ele.ClearAndSendkeys(testData);
            }
            else
            {
                Actions act = new Actions(DriverHelper.Driver);
                while (!wh.GetWebElement(selector, 0))
                {
                    IJavaScriptExecutor js = (IJavaScriptExecutor)DriverHelper.Driver;
                    js.ExecuteScript("window.scrollBy(0,500)", "");
                }
                IWebElement ele = wh.GetWebElement(selector);
                ele.ClearAndSendkeys(testData);
            }
        }

        public static void enterCaseworkerConclusionsRemarksThoughtProcessData()
        {
            waitHelpers wh = new waitHelpers();
            //By frame1 = getLocator($"//div[@data-id='voa_caseworkerconclusionsremarksthoughtprocess-FieldSectionItemContainer']//iframe[@aria-label='Designer']");
            //By frame2 = getLocator($"//iframe[contains(@title,'Caseworker Conclusions / Remarks / Thought Process')]");
            //wh.frameToBeAvailableAndSwitchToIt(frame1);
            //wh.frameToBeAvailableAndSwitchToIt(frame2);
            wh.GetWebElement(By.XPath($"//div[@data-id='voa_caseworkerconclusionsremarksthoughtprocess-FieldSectionItemContainer']//div[@role='textbox']")).SendKeys("Automation Testing");
            //SwitchToDefaultFrame();

        }
        public static void SetTheDateForTheTableCalendar(String fieldName, string date, int turns)
        {
            var selectorXpath = By.XPath($"/button[@aria-label='{date}']/parent::*[@data-is-focusable=\"true\"]");
            // var turns = 1;
            while (turns >= 0)
            {
                try
                {
                    Thread.Sleep(1000);
                    if (DriverHelper.Driver.FindElement(selectorXpath).Displayed)
                    {
                        Thread.Sleep(1000);
                        DriverHelper.Driver.FindElement(selectorXpath).ClickUsingJavascript();
                        return;
                    }
                    else
                    {
                        Thread.Sleep(1000);
                        DriverHelper.Driver.FindElement(By.CssSelector($"input[aria-label*='{fieldName}']")).ClickUsingJavascript();
                        turns -= 1;
                    }
                }
                //  }
                catch
                {
                    Thread.Sleep(1000);
                    DriverHelper.Driver.FindElement(By.CssSelector($"input[aria-label*='{fieldName}']")).ClickUsingJavascript();

                    turns -= 1;
                }

            }
        }

        public static void SetTheDateForTheTableCalendar(String fieldName, Dictionary<String, String> testData, int turns)
        {
            scrollToBtnElement(fieldName);
            var value = testData[fieldName];
            var date = "";
            if (!value.Contains("/") || !value.Contains("\\"))
            {
                date = DateTime.Now.AddDays(Convert.ToDouble(value)).ToString("d, MMMM, yyyy");
            }
            else
            {
                date = DateTime.Parse(value).Date.ToString("d, MMMM, yyyy");
            }

            var selectorXpath = By.XPath($"//button[@aria-label='{date}']/parent::*[@data-is-focusable=\"true\"]");
            // var turns = 1;
            while (turns >= 0)
            {
                try
                {
                    if (DriverHelper.Driver.FindElement(selectorXpath).Displayed)
                    {
                        DriverHelper.Driver.FindElement(selectorXpath).ClickUsingJavascript();
                        return;
                    }
                    else
                    {
                        DriverHelper.Driver.FindElement(By.CssSelector($"input[aria-label*='{fieldName}']")).ClickUsingJavascript();
                        turns -= 1;
                    }
                }
                //  }
                catch
                {
                    DriverHelper.Driver.FindElement(By.CssSelector($"input[aria-label*='{fieldName}']")).ClickUsingJavascript();

                    turns -= 1;
                }

            }
        }
        public static IWebElement waitForElementToBeDisplayed(this IWebDriver driver, By selector, int numOfSec)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(numOfSec));
                wait.PollingInterval = TimeSpan.FromMilliseconds(200);
                IWebElement ele = DriverHelper.Driver.FindElement(selector);
                wait.Until(d => ele.Displayed);
                return ele;
            }
            catch (Exception e)
            {
                throw new Exception($"user could not able to find web element with given {selector} locator");
            }
        }

        public static bool waitForElementToBeDisplayed(this IWebDriver driver, By selector)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(Config.DefaultDriverTimeOut));
                wait.PollingInterval = TimeSpan.FromMilliseconds(200);
                IWebElement ele = DriverHelper.Driver.FindElement(selector);
                wait.Until(d => ele.Displayed);
                return ele.Displayed;
            }
            catch (Exception e)
            {
                return true;
            }
        }

        public static IWebElement waitForElementToBeDisplayed(By selector, int numOfSec)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(numOfSec));
                wait.PollingInterval = TimeSpan.FromMilliseconds(200);
                //IWebElement ele = DriverHelper.Driver.FindElement(selector);
                //wait.Until(d => ele.Displayed);
                IWebElement ele = wait.Until(ElementExists(selector));

                return wait.Until(elementTobeClickable(ele));
            }
            catch (Exception e)
            {
                throw new Exception($"user could not able to find web element with given {selector} locator");
            }
        }

        public static IWebElement waitForElementToBeDisplayed(By selector)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(10));
                wait.PollingInterval = TimeSpan.FromMilliseconds(200);
                //IWebElement ele = DriverHelper.Driver.FindElement(selector);
                //wait.Until(d => ele.Displayed);
                IWebElement ele = wait.Until(ElementExists(selector));

                return wait.Until(elementTobeClickable(ele));
            }
            catch (Exception e)
            {
                throw new Exception($"user could not able to find web element with given {selector} locator");
            }
        }


        public static IWebElement waitUnitlElementToBeDisplayed(By selector, int numOfSec)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(numOfSec));
                wait.PollingInterval = TimeSpan.FromMilliseconds(200);
                IWebElement ele = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(selector));
                //DriverHelper.Driver.FindElement(selector);
                //wait.Until(d => ele.Displayed);
                //IWebElement ele = wait.Until(ElementExists(selector));
                return ele;
            }
            catch (Exception e)
            {
                throw new Exception($"user could not able to find web element with given {selector} locator");
            }
        }
        public static IWebElement waitForElementToBeDisplayed(IWebElement ele, int numOfSec)
        {
            //try
            //{
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(numOfSec));
            wait.PollingInterval = TimeSpan.FromMilliseconds(200);
            wait.Until(d => ele.Displayed);
            return ele;
            //}
            //catch (Exception e)
            //{
            //    CommonPage cp = new CommonPage();
            //    cp.clickOnMainMenuMoreElement("Refresh");
            //    WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(numOfSec));
            //    wait.PollingInterval = TimeSpan.FromMilliseconds(200);
            //    wait.Until(d => ele.Displayed);
            //    return ele;
            //}
            //return ele;
        }

        public static IWebElement validateFieldWithValueOnly(String value)
        {
            waitHelpers wh = new waitHelpers();
            By Locator = By.XPath($"//a[@aria-label = '{value}' and normalize-space(text())='{value}']");
            wh.isElementDisplayed(Locator, 90);
            return wh.GetWebElement(Locator);
        }


        public static Func<IWebDriver, bool> InvisibilityOfElementLocatedBy(By locator)
        {
            return d =>
            {
                try
                {
                    var ele = d.FindElement(locator);
                    return !ele.Displayed;
                }
                catch (NoSuchElementException nse)
                {
                    return true;
                }
                catch (StaleElementReferenceException ser)
                {
                    return true;
                }
            };

        }

        public static Func<IWebDriver, IWebElement> elementTobeClickable(IWebElement ele)
        {
            return d =>
            {
                try
                {
                    if (ele != null && ele.Displayed && ele.Enabled) return ele;
                    return null;


                }
                catch (StaleElementReferenceException nse)
                {
                    return null;
                }
            };
        }
        public static bool WaitForReadyStateToComplete()
        {
            bool isPageLoaded = false;
            //WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(5));
            //wait.Until((x) =>
            //{
            //    isPageLoaded = ((IJavaScriptExecutor)DriverHelper.Driver).ExecuteScript("return document.readyState").Equals("complete");
            //    return isPageLoaded;
            //});
            return isPageLoaded;
        }

        public static bool WaitForPageLoad()
        {
            bool isPageLoaded = false;
            Thread.Sleep(5000);
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(60));
            wait.Until((x) =>
            {
                isPageLoaded = ((IJavaScriptExecutor)DriverHelper.Driver).ExecuteScript("return document.readyState").Equals("complete");
                return isPageLoaded;
            });
            return isPageLoaded;
        }

        public static bool WaitForPageLoad(int staticTimeOut, int wwTimeOut)
        {
            bool isPageLoaded = false;
            Thread.Sleep(staticTimeOut);
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(wwTimeOut));
            wait.Until((x) =>
            {
                isPageLoaded = ((IJavaScriptExecutor)DriverHelper.Driver).ExecuteScript("return document.readyState").Equals("complete");
                return isPageLoaded;
            });
            return isPageLoaded;
        }

        public static Func<IWebDriver, IWebDriver> FrameToBeAvailableAndSwitchToit(By locator)
        {
            return d =>
            {
                try
                {
                    var frameElement = d.FindElement(locator);
                    return d.SwitchTo().Frame(frameElement);
                }
                catch (Exception e)
                {
                    return null;
                }
            };
        }


        public static string GetElementName(this IWebElement element)
        {
            string expName = null;
            //try
            //{
            //    expName = element.GetAttribute("aria-label");
            //    if (!string.IsNullOrEmpty(expName))
            //    {
            //        Log.Information("Element value found");

            //    }
            //    else 
            //    {
            //        expName = element.GetAttribute("title");
            //        if (!string.IsNullOrEmpty(expName))
            //        {
            //            Log.Information("Element value found");

            //        }
            //        else
            //        {
            //            expName = element.Text;
            //            if (!string.IsNullOrEmpty(expName))
            //            {
            //                Log.Information("Element value found");

            //            }
            //        }
            //    }
            //    return expName;
            //}
            //catch (Exception) 
            //{
            //    Log.Error("Element Name not found");
            //    return null;
            //}
            return expName;
        }

        public static IWebElement validateFieldWithValue(String value, String fieldName)
        {
            waitHelpers wh = new waitHelpers();
            By Locator = By.XPath($"//*[@aria-label='{fieldName}' and @value='{value}'] | //*[@aria-label='{fieldName}' and @title='{value}']");
            wh.isElementDisplayedInMilliSec(Locator, 90);
            return wh.GetWebElement(Locator);
        }

        public static bool validateFieldWithValue_(String value, String fieldName)
        {
            waitHelpers wh = new waitHelpers();
            By Locator = By.XPath($"//*[@aria-label='{fieldName}' and @value='{value}'] | //*[@aria-label='{fieldName}' and @title='{value}']");
            return SeleniumExtensions.isElementDisplayed_New(Locator);
        }

        public static IWebElement validateDateFieldWithValue(String value, String fieldName)
        {
            waitHelpers wh = new waitHelpers();
            By Locator = By.XPath($"//*[contains(@aria-label,'{fieldName}') and @value='{value}'] | //*[contains(@aria-label,'{fieldName}') and @title='{value}']");
            wh.isElementDisplayed(Locator, 90);
            return wh.GetWebElement(Locator);
        }

        public static IWebElement validateFieldWithValue(String value, String fieldName, FeatureContext fc)
        {
            waitHelpers wh = new waitHelpers();
            String fieldValue = (String)fc[value];
            if (fieldName.Contains("Date") || fieldName.Contains("date"))
            {
                fieldName = fieldName;// "Date of " +
            }
            By Locator = By.XPath($"//*[@aria-label='{fieldName}' and @value='{fieldValue}'] | //*[@aria-label='{fieldName}' and @title='{fieldValue}']");
            wh.isElementDisplayed(Locator, 90);
            return wh.GetWebElement(Locator);
        }

        public static IWebElement validate_FieldWithValue(String value, String fieldName)
        {
            waitHelpers wh = new waitHelpers();
            By Locator = By.XPath($"//*[contains(@title,'{fieldName}')]//following-sibling::div[@title='{value}']");
            return wh.GetWebElement(Locator);
        }

        public static IWebElement validateFieldWithTitle(String fieldName, String value)
        {
            String locatorStr = $"//*[@title='{fieldName}']//*[text()='{value}']";
            By Locator = getLocator(locatorStr);
            waitHelpers wh = new waitHelpers();
            Actions act = new Actions(DriverHelper.Driver);
            IWebElement ele = wh.GetWebElement(Locator);
            act.ScrollToElement(ele).Pause(TimeSpan.FromMilliseconds(100)).Build().Perform();
            return ele;
        }

        public static IWebElement validateFieldWithTitle(String fieldName, String value, String type)
        {
            String locatorStr = $"//*[@role='{type}']//*[@title='{fieldName}']//*[text()='{value}']";
            By Locator = getLocator(locatorStr);
            waitHelpers wh = new waitHelpers();
            Actions act = new Actions(DriverHelper.Driver);
            IWebElement ele = wh.GetWebElement(Locator);
            //waitUnitlElementToBeDisplayed(Locator, 120);
            act.ScrollToElement(ele).Pause(TimeSpan.FromMilliseconds(100)).Build().Perform();
            return ele;
        }

        public static bool isElementDisplayed(IWebElement ele)
        {
            try
            {
                //WebDriverWait ww = new WebDriverWait(DriverHelper.Driver,TimeSpan.FromSeconds(timeOut));
                //ww.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(ele)).Displayed
                return ele.Displayed;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        public static bool isElementDisplayed(By by)
        {
            try
            {
                IWebElement ele = DriverHelper.Driver.FindElement(by);
                return ele.Displayed;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool WaitElementToDissapear(By locator, int maxtTries)
        {
            bool SavingFlag = false;
            int retries = 0;
            while (retries < maxtTries)
            {
                try
                {
                    var element = DriverHelper.Driver.FindElement(locator);
                    if (!element.Displayed)
                    {
                        SavingFlag = true;
                        return SavingFlag;

                    }

                }
                catch (NoSuchElementException)
                {
                    SavingFlag = true;
                    return SavingFlag;
                }
                catch (StaleElementReferenceException)
                {
                    SavingFlag = true;
                    return SavingFlag;
                }
                if (SavingFlag == true)
                {
                    break;
                }
                retries++;
                Thread.Sleep(1000);
            }
            return SavingFlag;
        }

        public static void SetTheDateForTheTableCalendarOnDialog(String fieldName, Dictionary<String, String> testData)
        {
            var value = testData[fieldName];
            var date = "";
            if (!value.Contains("/") || !value.Contains("\\"))
            {
                date = DateTime.Now.AddDays(Convert.ToDouble(value)).ToString("d, MMMM, yyyy");
            }
            else
            {
                date = DateTime.Parse(value).Date.ToString("d, MMMM, yyyy");
            }
            var selectorXpath = By.XPath($"//button[@aria-label='{date}']");
            var elementSelector = By.XPath($"//*[text()='{fieldName}']/following-sibling::*//input[contains(@id,'DatePicker')] | //*[text()='{fieldName}']/parent::div/following-sibling::*//input[contains(@aria-label,'{fieldName}')]");//Date of 
            //By.XPath($"//*[text()='{fieldName}']/following-sibling::*//input[contains(@id,'DatePicker')]");
            WaitForElementAndClick(elementSelector);
            WaitForElementAndClick(selectorXpath);

            //DriverHelper.Driver.FindElement(selectorXpath).ClickUsingJavascript();
        }

        public static Func<IWebDriver, ReadOnlyCollection<IWebElement>> PresenceOfAllElementsLocated(By locator)
        {
            return driver =>
            {
                try
                {
                    var elements = DriverHelper.Driver.FindElements(locator);
                    return elements.Any() ? elements : null;

                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            };
        }

        public static string SelectDateFromDatePicker(String fieldName, Dictionary<String, String> testData)
        {
            string proposedEffectiveDateChanged = " ";
            var value = testData[fieldName];
            if (!value.Contains("/") || !value.Contains("\\"))
            {
                proposedEffectiveDateChanged = DateTime.Now.AddDays(Convert.ToDouble(value)).ToString("d, MMMM, yyyy");
            }
            else
            {
                proposedEffectiveDateChanged = DateTime.Parse(value).Date.ToString("d, MMMM, yyyy");
            }
            var selectorXpath = By.XPath($"//button[@aria-label='{proposedEffectiveDateChanged}']");
            var elementSelector = By.CssSelector($"[data-id='voa_newproposedeffectivedate'] input[id*='DatePicker']");
            var goToday = By.XPath($"//button[normalize-space(text())='Go to today']");
            WaitForElementAndClick(elementSelector);
            WaitForElementAndClick(goToday);
            WaitForElementAndClick(selectorXpath);
            return proposedEffectiveDateChanged;
        }


        public static string GetDateFormat3(string dateString)
        {
            string[] dateIndividualValue = dateString.Split(',');
            string monthValue = dateIndividualValue[0];
            if (monthValue.Length == 1)
            {
                monthValue = "M";
            }
            else
            {
                monthValue = "MM";
            }
            string dayValue = dateIndividualValue[1];
            if (dayValue.Length == 1)
            {
                dayValue = "d";
            }
            else
            {
                dayValue = "dd";
            }
            string yearValue = dateIndividualValue[2];
            yearValue = "yyyy";
            string formattedDate = monthValue + "/" + dayValue + "/" + yearValue;
            return formattedDate;
        }

        public static string GetDateFormat(string dateString)
        {
            string[] dateIndividualValue = dateString.Split('/');
            string monthValue = dateIndividualValue[0];
            if (monthValue.Length == 1)
            {
                monthValue = "M";
            }
            else
            {
                monthValue = "MM";
            }
            string dayValue = dateIndividualValue[1];
            if (dayValue.Length == 1)
            {
                dayValue = "d";
            }
            else
            {
                dayValue = "dd";
            }
            string yearValue = dateIndividualValue[2];
            yearValue = "yyyy";
            string formattedDate = monthValue + "/" + dayValue + "/" + yearValue;
            return formattedDate;
        }

        public static string GetDateFormat2(string dateString)
        {
            string[] dateIndividualValue = dateString.Split('/');
            string dayValue = dateIndividualValue[0];
            if (dayValue.Length == 1)
            {
                dayValue = "d";
            }
            else
            {
                dayValue = "dd";
            }
            string monthValue = dateIndividualValue[1];
            if (monthValue.Length == 1)
            {
                monthValue = "M";
            }
            else
            {
                monthValue = "MM";
            }
            string yearValue = dateIndividualValue[2];
            yearValue = "yyyy";
            string formattedDate = dayValue + "/" + monthValue + "/" + yearValue;
            return formattedDate;
        }

        public static void enterDateSequentiallyOnDialog(String fieldName, String value)
        {
            //scrollToBtnElement(fieldName);
            var elementSelector = By.XPath($"//*[text()='{fieldName}']/following-sibling::*//input[contains(@id,'DatePicker')]");
            IWebElement element = DriverHelper.Driver.FindElement(elementSelector);
            IJavaScriptExecutor jse = (IJavaScriptExecutor)DriverHelper.Driver;
            jse.ExecuteScript("arguments[0].value='" + value + "';", element);
            //foreach (char letter in value)
            //{
            //    element.SendKeys(letter.ToString());
            //    System.Threading.Thread.Sleep(200);
            //}
        }

        public static void enterDateSequentially(String fieldName, String sheetName, String RowID)
        {
            string TestDataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + Config.TestDataExcelFilePath);
            var excelUtility = new ExcelTestDataUtility(TestDataFilePath);
            string value = excelUtility.getFieldTestData(fieldName, sheetName, RowID);

            scrollToBtnElement(fieldName);
            IWebElement element = DriverHelper.Driver.FindElement(By.CssSelector($"input[aria-label*='{fieldName}']"));

            foreach (char letter in value)
            {
                element.SendKeys(letter.ToString());

                System.Threading.Thread.Sleep(200);
            }
        }
        public static void enterDateSequentially(String fieldName, String value)
        {
            scrollToBtnElement(fieldName);
            IWebElement element = DriverHelper.Driver.FindElement(By.CssSelector($"input[aria-label*='{fieldName}']"));

            foreach (char letter in value)
            {
                element.SendKeys(letter.ToString());

                System.Threading.Thread.Sleep(200);
            }
        }

        public static string enterAfterDateSequentially(int days, String fieldName)
        {
            scrollToBtnElement(fieldName);
            waitHelpers wh = new waitHelpers();
            IWebElement element = wh.GetWebElement(By.CssSelector($"input[aria-label*='{fieldName}']"));
            DateTime _futureDate = DateTime.Now + TimeSpan.FromDays(days);
            //String value = _futureDate.ToString("dd/MM/yyyy");
            string userDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string[] UserDirectoryParts = userDirectory.Split(new string[] { "\\" }, StringSplitOptions.None);
            string userID = UserDirectoryParts[2];
            Console.Write("userID : " + userID);
            String value = null;

            //if (userID.Contains("7865773@hmrc.gov.uk") || userID.Contains("7865772@hmrc.gov.uk"))
            //{
            //    value = _futureDate.ToString("MM/dd/yyyy");

            //}
            //else
            //{
            value = _futureDate.ToString("dd/MM/yyyy");
            //}
            foreach (char letter in value)
            {
                element.SendKeys(letter.ToString());
            }
            return value;
        }



        public static void enterBeforeDateSequentially(int days, String fieldName, String role)
        {
            scrollToBtnElement(fieldName);
            waitHelpers wh = new waitHelpers();
            //[role= 'dialog'] input[aria - label *= 'Proposed Effective Date']
            IWebElement element = wh.GetWebElement(By.CssSelector($"[role= '{role}'] input[aria-label*='{fieldName}']"));
            DateTime _futureDate = DateTime.Now - TimeSpan.FromDays(days);
            String value = _futureDate.ToString("dd/MM/yyyy");
            //element.Clear();
            //element.SendKeys(Keys.Control+"a"+Keys.Delete);

            foreach (char letter in value)
            {
                element.SendKeys(letter.ToString());
            }
        }

        public static void enterBeforeCalenderDateSequentially(int days, String fieldName, String role)
        {
            waitHelpers wh = new waitHelpers();
            SubmissionPage sp = new SubmissionPage();
            IWebElement element = null;
            DateTime _futureDate = DateTime.Now - TimeSpan.FromDays(days);
            String value = _futureDate.ToString("M/d/yyyy");
            //= wh.GetWebElement(By.CssSelector($"[role= '{role}'] input[aria-label*='{fieldName}']"));
            switch (fieldName)
            {
                case "Enter the Effective Date for the new set of PADs":
                    element = wh.GetWebElement(By.CssSelector($"[role= '{role}'] div[title*='{fieldName}'] input"));
                    sp.selectProposedDate_new(element, value);
                    //sp.selectProposedDate(element, value);
                    break;
                case "Internal Inspection SLA":
                    element = wh.GetWebElement(By.CssSelector($"input[aria-label*='{fieldName}']"));
                    sp.selectProposedDate(element, value);
                    break;
                case "Validity/Acceptance Decision Date":
                    element = wh.GetWebElement(By.XPath($"//*[@role= '{role}']//div[contains(@title,'{fieldName}')]/following-sibling::div//input"));
                    //By.CssSelector($"[role= '{role}'] div[title*='{fieldName}'] input"));
                    sp.selectProposedDate(element, value);
                    break;
                default:
                    scrollToBtnElement(fieldName);
                    element = wh.GetWebElement(By.CssSelector($"[role= '{role}'] input[aria-label*='{fieldName}']"));
                    sp.selectProposedDate(element, value);
                    break;

            }
            //if (fieldName.Equals("Enter the Effective Date for the new set of PADs"))
            //{
            //    element = wh.GetWebElement(By.CssSelector($"[role= '{role}'] div[title*='{fieldName}'] input"));
            //    sp.selectProposedDate_new(element, value);
            //}
            //else
            //{
            //    scrollToBtnElement(fieldName);
            //    element = wh.GetWebElement(By.CssSelector($"[role= '{role}'] input[aria-label*='{fieldName}']"));
            //   sp.selectProposedDate(element,value);
            //}

            // element.Clear();
        }

        public static void enterBeforeCalenderDateSequentially(string date, String fieldName, String role)
        {
            waitHelpers wh = new waitHelpers();
            SubmissionPage sp = new SubmissionPage();
            IWebElement element = null;
            DateTime _futureDate = DateTime.Parse(date);
            String value = _futureDate.ToString("M/d/yyyy");
            //= wh.GetWebElement(By.CssSelector($"[role= '{role}'] input[aria-label*='{fieldName}']"));
            switch (fieldName)
            {
                case "Enter the Effective Date for the new set of PADs":
                    element = wh.GetWebElement(By.CssSelector($"[role= '{role}'] div[title*='{fieldName}'] input"));
                    sp.selectProposedDate_new(element, value);
                    //sp.selectProposedDate(element, value);
                    break;
                case "Internal Inspection SLA":
                    element = wh.GetWebElement(By.CssSelector($"input[aria-label*='{fieldName}']"));
                    sp.selectProposedDate(element, value);
                    break;
                case "Validity/Acceptance Decision Date":
                    element = wh.GetWebElement(By.XPath($"//*[@role= '{role}']//div[contains(@title,'{fieldName}')]/following-sibling::div//input"));
                    //By.CssSelector($"[role= '{role}'] div[title*='{fieldName}'] input"));
                    sp.selectProposedDate(element, value);
                    break;
                default:
                    scrollToBtnElement(fieldName);
                    element = wh.GetWebElement(By.CssSelector($"[role= '{role}'] input[aria-label*='{fieldName}']"));
                    sp.selectProposedDate(element, value);
                    break;

            }
        }


        public static string enterBeforeCalenderDateSameAsEffectiveDate(int days, string hereditamentDate, String fieldName, String role)
        {
            waitHelpers wh = new waitHelpers();
            SubmissionPage sp = new SubmissionPage();
            IWebElement element = null;
            string[] dateValue = hereditamentDate.Split(' ');
            DateTime hereditamentEffectiveDateAfterFormat = DateTime.Parse(dateValue[0]);
            DateTime _futureDate = hereditamentEffectiveDateAfterFormat - TimeSpan.FromDays(days);
            String value = _futureDate.ToString("M/d/yyyy");

            switch (fieldName)
            {
                case "Enter the Effective Date for the new set of PADs":
                    element = wh.GetWebElement(By.CssSelector($"[role= '{role}'] div[title*='{fieldName}'] input"));
                    sp.selectProposedDate_new(element, value);
                    break;
                case "Internal Inspection SLA":
                    element = wh.GetWebElement(By.CssSelector($"input[aria-label*='{fieldName}']"));
                    sp.selectProposedDate(element, value);
                    break;
                case "New Proposed Effective Date":
                    element = wh.GetWebElement(By.XPath("//div[@data-id='voa_newproposedeffectivedate.fieldControl_container']//input"));
                    sp.selectProposedDate_fuiCal(element, value);
                    String value1 = _futureDate.ToString("dd/MM/yyyy");
                    value = value1;
                    break;
                default:
                    scrollToBtnElement(fieldName);
                    element = wh.GetWebElement(By.CssSelector($"[role= '{role}'] input[aria-label*='{fieldName}']"));
                    sp.selectProposedDate(element, value);
                    break;

            }
            return value;
        }

        public static string enterAfterCalenderDateSameAsEffectiveDate(int days, string hereditamentDate, String fieldName, String role)
        {
            waitHelpers wh = new waitHelpers();
            SubmissionPage sp = new SubmissionPage();
            IWebElement element = null;
            string[] dateValue = hereditamentDate.Split(' ');
            DateTime hereditamentEffectiveDateAfterFormat = DateTime.Parse(dateValue[0]);
            DateTime _futureDate = hereditamentEffectiveDateAfterFormat + TimeSpan.FromDays(days);
            String value = _futureDate.ToString("M/d/yyyy");
            switch (fieldName)
            {
                case "Enter the Effective Date for the new set of PADs":
                    element = wh.GetWebElement(By.CssSelector($"[role= '{role}'] div[title*='{fieldName}'] input"));
                    sp.selectProposedDate_new(element, value);
                    break;
                case "Internal Inspection SLA":
                    element = wh.GetWebElement(By.CssSelector($"input[aria-label*='{fieldName}']"));
                    sp.selectProposedDate(element, value);
                    break;
                case "Proposed Effective Date":
                    element = wh.GetWebElement(By.CssSelector($"input[aria-label*='{fieldName}']"));
                    if (!role.Equals("dialog"))
                    {
                        sp.selectPresentationProposedDate(element, value);
                    }
                    else
                    {
                        sp.selectProposedDate(element, value);
                    }
                    break;

                default:
                    scrollToBtnElement(fieldName);
                    element = wh.GetWebElement(By.CssSelector($"[role= '{role}'] input[aria-label*='{fieldName}']"));
                    sp.selectProposedDate(element, value);
                    break;

            }
            return value;
        }



        public static string enterBeforeDateSequentially(int days, String fieldName)
        {
            scrollToBtnElement(fieldName);
            waitHelpers wh = new waitHelpers();
            IWebElement element = wh.GetWebElement(By.CssSelector($"input[aria-label*='{fieldName}']"));//Date of 
            String proposdEffectiveDate = null;
            //IWebElement element = wh.GetWebElement(By.CssSelector($"input[aria-label*='{fieldName}'][id*='Date']"));
            DateTime _futureDate = DateTime.Now - TimeSpan.FromDays(days);
            string userDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string[] UserDirectoryParts = userDirectory.Split(new string[] { "\\" }, StringSplitOptions.None);
            string userID = UserDirectoryParts[2];
            Console.Write("userID : " + userID);
            String value = null;

            //if (userID.Contains("7865773@hmrc.gov.uk") || userID.Contains("7865772@hmrc.gov.uk"))
            //{
            //    value = _futureDate.ToString("MM/dd/yyyy");

            //}
            //else
            //{
            value = _futureDate.ToString("dd/MM/yyyy");
            proposdEffectiveDate = _futureDate.ToString("M/d/yyyy");
            //element.Clear();
            //}
            foreach (char letter in value)
            {
                element.SendKeys(letter.ToString());
            }
            //if (fieldName.Equals("Date Received"))
            //{
            //    SeleniumExtensions.clearAndEnterBeforeTime("Time of Date Received", 2);
            //}
            return proposdEffectiveDate;
        }

        public static string enterBeforeDateToGoLegeslativeDateSequentially(int days, String BaseProposedEffectiveDate, String fieldName)
        {
            scrollToBtnElement(fieldName);
            waitHelpers wh = new waitHelpers();
            IWebElement element = wh.GetWebElement(By.CssSelector($"input[aria-label*='{fieldName}']"));//Date of 
            String proposdEffectiveDate = null;
            string[] dateValue = Config.BaseProposedEffectiveDate.Split(' ');
            DateTime hereditamentEffectiveDateAfterFormat = DateTime.Parse(dateValue[0]);
            DateTime _futureDate = hereditamentEffectiveDateAfterFormat - TimeSpan.FromDays(days);

            string userDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string[] UserDirectoryParts = userDirectory.Split(new string[] { "\\" }, StringSplitOptions.None);
            string userID = UserDirectoryParts[2];
            Console.Write("userID : " + userID);
            String value = null;
            value = _futureDate.ToString("dd/MM/yyyy");
            proposdEffectiveDate = _futureDate.ToString("M/d/yyyy");
            foreach (char letter in value)
            {
                element.SendKeys(letter.ToString());
            }
            return proposdEffectiveDate;
        }

        public static string enterBeforeDateSequentially(int days, String fieldName, int elePosition)
        {
            scrollToBtnElement(fieldName);
            waitHelpers wh = new waitHelpers();
            IWebElement element = wh.GetWebElement(By.XPath($"(//input[contains(@aria-label,'{fieldName}')])[{elePosition}]"));//Date of 
            String proposdEffectiveDate = null;
            DateTime _futureDate = DateTime.Now - TimeSpan.FromDays(days);
            string userDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string[] UserDirectoryParts = userDirectory.Split(new string[] { "\\" }, StringSplitOptions.None);
            string userID = UserDirectoryParts[2];
            Console.Write("userID : " + userID);
            String value = null;
            value = _futureDate.ToString("dd/MM/yyyy");
            proposdEffectiveDate = _futureDate.ToString("M/d/yyyy");
            foreach (char letter in value)
            {
                element.SendKeys(letter.ToString());
            }
            return proposdEffectiveDate;
        }

        public static string enterBeforeDateOfHereditamentEffectiveDateSequentially(int days, String hereditamentEffectiveDate, String fieldName)
        {
            scrollToBtnElement(fieldName);
            waitHelpers wh = new waitHelpers();
            IWebElement element = wh.GetWebElement(By.CssSelector($"input[aria-label*='{fieldName}']"));//Date of 
            string[] dateValue = hereditamentEffectiveDate.Split(' ');
            DateTime hereditamentEffectiveDateAfterFormat = DateTime.Parse(dateValue[0]);
            DateTime _futureDate = hereditamentEffectiveDateAfterFormat - TimeSpan.FromDays(days);

            string userDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string[] UserDirectoryParts = userDirectory.Split(new string[] { "\\" }, StringSplitOptions.None);
            string userID = UserDirectoryParts[2];
            Console.Write("userID : " + userID);
            String value = null;

            //if (userID.Contains("7865773@hmrc.gov.uk") || userID.Contains("7865772@hmrc.gov.uk"))
            //{
            //    value = _futureDate.ToString("MM/dd/yyyy");

            //}
            //else
            //{
            value = _futureDate.ToString("dd/MM/yyyy");
            //}
            foreach (char letter in value)
            {
                element.SendKeys(letter.ToString());
            }
            string proposdEffectiveDate = _futureDate.ToString("M/d/yyyy");
            return proposdEffectiveDate;
        }

        public static string enterAfterDateOfHereditamentEffectiveDateSequentially(int days, String hereditamentEffectiveDate, String fieldName)
        {
            scrollToBtnElement(fieldName);
            waitHelpers wh = new waitHelpers();
            IWebElement element = wh.GetWebElement(By.CssSelector($"input[aria-label*='{fieldName}']"));//Date of 
            string[] dateValue = hereditamentEffectiveDate.Split(' ');
            DateTime hereditamentEffectiveDateAfterFormat = DateTime.Parse(dateValue[0]);
            DateTime _futureDate = hereditamentEffectiveDateAfterFormat + TimeSpan.FromDays(days);

            string userDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string[] UserDirectoryParts = userDirectory.Split(new string[] { "\\" }, StringSplitOptions.None);
            string userID = UserDirectoryParts[2];
            Console.Write("userID : " + userID);
            String value = null;

            //if (userID.Contains("7865773@hmrc.gov.uk") || userID.Contains("7865772@hmrc.gov.uk"))
            //{
            //    value = _futureDate.ToString("MM/dd/yyyy");

            //}
            //else
            //{
            value = _futureDate.ToString("dd/MM/yyyy");
            //}
            foreach (char letter in value)
            {
                element.SendKeys(letter.ToString());
            }
            String proposdEffectiveDate = _futureDate.ToString("M/d/yyyy");
            return proposdEffectiveDate;

        }

        public static string enterAfterDateOfHereditamentEffectiveDateSequentially(String hereditamentEffectiveDate, String fieldName)
        {
            scrollToBtnElement(fieldName);
            waitHelpers wh = new waitHelpers();
            IWebElement element = wh.GetWebElement(By.CssSelector($"input[aria-label*='{fieldName}']"));//Date of 
            string[] dateValue = hereditamentEffectiveDate.Split(' ');
            DateTime hereditamentEffectiveDateAfterFormat = DateTime.Parse(dateValue[0]);
            Random rand = new Random();
            int RandomNum = rand.Next(2, 12);
            DateTime _futureDate = hereditamentEffectiveDateAfterFormat + TimeSpan.FromDays(RandomNum);

            string userDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string[] UserDirectoryParts = userDirectory.Split(new string[] { "\\" }, StringSplitOptions.None);
            string userID = UserDirectoryParts[2];
            Console.Write("userID : " + userID);
            String value = null;

            //if (userID.Contains("7865773@hmrc.gov.uk") || userID.Contains("7865772@hmrc.gov.uk"))
            //{
            //    value = _futureDate.ToString("MM/dd/yyyy");

            //}
            //else
            //{
            value = _futureDate.ToString("dd/MM/yyyy");
            //}
            foreach (char letter in value)
            {
                element.SendKeys(letter.ToString());
            }
            String proposdEffectiveDate = _futureDate.ToString("M/d/yyyy");
            return proposdEffectiveDate;

        }


        public static string enterAfterDateOfHereditamentEffectiveDateSequentially(String hereditamentEffectiveDate, String fieldName, FeatureContext fc)
        {
            scrollToBtnElement(fieldName);
            waitHelpers wh = new waitHelpers();
            IWebElement element = wh.GetWebElement(By.CssSelector($"input[aria-label*='{fieldName}']"));//Date of 
            string[] dateValue = hereditamentEffectiveDate.Split(' ');
            DateTime hereditamentEffectiveDateAfterFormat = DateTime.Parse(dateValue[0]);
            Random rand = new Random();
            int RandomNum = rand.Next(2, 12);
            fc["RandomNum"] = RandomNum.ToString();
            DateTime _futureDate = hereditamentEffectiveDateAfterFormat + TimeSpan.FromDays(RandomNum);
            string userDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string[] UserDirectoryParts = userDirectory.Split(new string[] { "\\" }, StringSplitOptions.None);
            string userID = UserDirectoryParts[2];
            Console.Write("userID : " + userID);
            String value = null;
            value = _futureDate.ToString("dd/MM/yyyy");
            foreach (char letter in value)
            {
                element.SendKeys(letter.ToString());
            }
            String proposdEffectiveDate = _futureDate.ToString("M/d/yyyy");
            return proposdEffectiveDate;
        }


        public static IWebElement WaitForElementToDisplayed(IWebElement ele, int timeOuts)
        {
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(timeOuts));
            wait.PollingInterval = TimeSpan.FromMilliseconds(200);
            wait.Until(driver => ele.Displayed);
            return ele;
        }

        public static void SelectDropdownValue(this IWebElement ele, string optionToSelect)
        {

            waitHelpers wh = new waitHelpers();
            ele.ClickUsingJavascript();
            By DropdownOptionToSelect = By.XPath($"//*[@role='listbox']//*[contains(@class,'fui-Option') and @role='option']//following-sibling::*[text()='{optionToSelect}']");

            wh.WaitForElementToBeReady(DropdownOptionToSelect, 20).ClickUsingJavascript();
            //IWebElement dropdownOptionToSelect = wh.WaitForElementToBeReady(DropdownOptionToSelect, 20);
            //dropdownOptionToSelect.ClickUsingJavascript();


        }
        public static void validateSLAKPIStatus(string kpiName, string status)
        {
            By kpi = By.XPath($"//div[@class=\"kpiname\" and text()=\"{kpiName}\"]/following-sibling::*[text()='{status}']");
            waitHelpers wh = new waitHelpers();
            Assert.IsTrue(wh.isElementDisplayed(kpi, 90), $"{kpiName} displayed with status {status}");

        }

        public static void scrollToBtnBasedOnLabelClick(String fieldName)
        {
            waitHelpers wh = new waitHelpers();
            WaitForReadyStateToComplete();
            By selector = getLocator($"//div[@role='dialog']//*[text()='{fieldName}']/ancestor::button");
            Actions act = new Actions(DriverHelper.Driver);
            //IWebElement ele = wh.GetWebElement(selector);
            //WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(120));
            //wait.PollingInterval = TimeSpan.FromMilliseconds(200);
            IWebElement ele = wh.GetWebElement(selector);
            wh.isElementDisplayed(selector, 120);
            IJavaScriptExecutor js = DriverHelper.Driver as IJavaScriptExecutor;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", ele);
            //wait.Until(driver => ele.Displayed);
            act.ScrollToElement(ele).Pause(TimeSpan.FromMilliseconds(500)).Click(ele).Pause(TimeSpan.FromMilliseconds(500)).Build().Perform();
            WaitForReadyStateToComplete();
        }

        public static string GetTime(int mins)
        {
            DateTime currentTime = DateTime.Now;

            DateTime futureTime = currentTime.AddMinutes(mins);

            return futureTime.ToString("HH:mm");
        }

        public static string GetBeforeTime(int mins)
        {
            DateTime currentTime = DateTime.Now;

            DateTime futureTime = currentTime.Subtract(TimeSpan.FromMinutes(mins));

            return futureTime.ToString("HH:mm");
        }

        public static void clearAndEnterTime(String arialLabel, int timeinMins)
        {
            waitHelpers wh = new waitHelpers();
            By timeField = By.CssSelector($"[aria-label='{arialLabel}']");
            string time = GetTime(timeinMins);
            wh.GetWebElement(timeField).ClearAndSendkeys(time);
            //wh.GetWebElement(timeField).SendKeys(time);

        }

        public static void clearAndEnterBeforeTime(String arialLabel, int timeinMins)
        {
            waitHelpers wh = new waitHelpers();
            By timeField = By.CssSelector($"[aria-label='{arialLabel}']");
            string time = GetBeforeTime(timeinMins);
            wh.GetWebElement(timeField).ClearAndSendkeys(time);
        }


    }
}
