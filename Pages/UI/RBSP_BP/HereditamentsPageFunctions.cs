using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using POMSeleniumFrameworkPoc1.Helpers;
using POMSeleniumFrameworkPoc1.Pages.UI.RBSP_BP;
using Reqnroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace POMSeleniumFrameworkPoc1.Pages.UI.CommonComponents
{
    public partial class HereditamentsPage : BasePage
    {
        int waitTime = 120;
        public void ClickHereditament()
        {
            HereditamentsTitle.Click();
        }

        public bool IsListOfHereditamentsDisplayed()
        {
            var hereditamentCount = ListOfHereditamentsList.Count();

            if (hereditamentCount > 0)
                return true;
            return false;
        }

        public void ClickPlusNewHereditamentBtn()
        {
            NewbuttonElement.Click();
        }

        public void ClickDocumentTab()
        {
            DocumentsTab.Click();
            if (OkButton.IsElementDisplayed(10))
                OkButton.Click();
        }

        public void FillBillingAuthorityDetails(string billingAuthority)
        {
            ScrollDiv.ScrollUntilSelectorDisplayedAndSendKeys(BillingAuthorityLookupInput
                , billingAuthority, 30);
            BillingAuthorityLookUpFirstValue.ClickUntilNoClickInterruptable();
        }

        public string SaveHereditament()
        {
            SaveLabel.Click();
            VerifyDuplicateRecords();
            HereditamentIDTextBox.ScrollAndClick();
            Driver.WaitUntilElementHasValue(HereditamentIDTextBoxSelector, "---", false, 30000);
            HereditamentId = HereditamentIDTextBox.GetAttribute("title");
            return HereditamentId;
        }

        public void PickEffectiveDate(float days)
        {
            var currentDate = DateTime.Now.AddDays(days).ToString("dd/MM/yyyy");
            ScrollHereditamentPageDiv.ScrollUntilSelectorDisplayedAndSendKeys(EffectiveDatePickerSelector, currentDate, 10);
        }

        public void VerifyGeoMapUnderVMSTab(float days)
        {
            var currentDate = DateTime.Now.AddDays(days).ToString("dd/MM/yyyy");
            ScrollHereditamentPageDiv.ScrollUntilSelectorDisplayedAndSendKeys(EffectiveDatePickerSelector, currentDate, 10);
        }

        public void ClickVMSTabUnderHereditaments()
        {
            System.Threading.Thread.Sleep(4000);
            VMSTabElement.ScrollAndClick();
        }

        public bool VerifyGeoMapLoadedUnderVMSUnderHereditaments()
        {
            var windows = Driver.WindowHandles;
            Driver.SwitchTo().Window(windows.Last());
            Driver.WaitForElementToDisplayProperly(VMSWindowIFrameSelector, 5);
            Driver.SwitchTo().Frame(VMSWindowIFrame);
            var displayed = GeoMapUnderVMSTabUnderHereditamentsElement.Displayed;
            Driver.SwitchTo().Window(windows.First());
            return displayed;
        }

        public bool VerifyGeoMapLoadedUnderAddressSectionUnderHereditaments()
        {
            System.Threading.Thread.Sleep(5000);
            return GeoMapUnderAddressSectionUnderHereditamentsElement.Displayed;
        }

        public bool VerifyUploadRefreshLinksControlsAreEnabled()
        {
            UploadButtonEnabledElement.IsElementDisplayed(10);
            return UploadButtonEnabledElement.Displayed
                && RefreshButtonEnabledElement.Displayed;
            //&& ExternalLinkButtonEnabledElement.Displayed;
        }

        public bool VerifyMoveDownloadCopyControlsAreDisabled()
        {
            return MoveButtonEnabledElement.Displayed
                && CopyButtonEnabledElement.Displayed
                && DownloadButtonEnabledElement.Displayed;
        }

        public void selectFirstHeridetamentResult(int rowNumber)
        {
            waitHelpers wh = new waitHelpers();
            IList<IWebElement> webEleLi = wh.getAllWebElements(By.CssSelector("div[role='radio'] div[class*='Check root']"));
            var hereditamentCount = webEleLi.Count;
            for (int i = 0; i < hereditamentCount; i++)
            {
                if (i == rowNumber - 1)
                {
                    wh.isElementDisplayed(webEleLi.ElementAt(i), waitTime);
                    webEleLi.ElementAt(i).Click();
                }

            }
        }

        public void selectFirstPlotResult()
        {
            waitHelpers wh = new waitHelpers();
            wh.clickOnWebElement(By.XPath($"(//div[@role='radio']//div[contains(@class,'Check root')])[1]"));

        }

        public void selectHeridetamentByuprn_NewProperty(string uprnValue)
        {
            waitHelpers wh = new waitHelpers();
            int rowSize = wh.getAllWebElements(By.CssSelector("div[aria-label='Address search results grid'] div[class='ms-List-page'] div[class='ms-List-cell']")).Count;
            By uprnBy = By.CssSelector("div[class='ms-List-page'] div[class='ms-List-cell'] [data-automation-key='uprn'] div");
            wh.isElementDisplayed(uprnBy, 60);
            SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[@class='ms-List-page']//div[@class='ms-List-cell'])[{rowSize}-1]")));
            SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[@class='ms-List-page']//div[@class='ms-List-cell'])[1]")));
            bool uprnRowDisplayed = wh.getAllWebElementsText(uprnBy).Contains(uprnValue);
            bool nextPageBtnDisplayed = false;
            By UPRNrow = By.XPath($"//div[text()='{uprnValue}']/parent::div[@data-automation-key='uprn']");
            By nextPageBtnOnDialog = By.XPath("//*[contains(@class,'ms-Dialog-main')]//button[@title='Next Page']");
            IJavaScriptExecutor js = DriverHelper.Driver as IJavaScriptExecutor;
            int i = 0;
            do
            {
                if (uprnRowDisplayed)
                {
                    int rowNum = getRowOfUPRN_NewProperty(uprnValue);
                    SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[@class='ms-List-page']//div[@class='ms-List-cell'])[{rowNum}]")));
                    IWebElement we = wh.GetWebElement(UPRNrow);
                    js.ExecuteScript("arguments[0].scrollIntoView(true);", we);
                    wh.clickOnElement(UPRNrow);
                    wh.GetWebElement(By.XPath("//button//*[text()='Use Address']")).ClickUsingJavascript();
                }
                else
                {
                    SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[@class='ms-List-page']//div[@class='ms-List-cell'])[{rowSize}-1]")));
                    nextPageBtnDisplayed = wh.isElementDisplayed(nextPageBtnOnDialog, 20);
                    if (nextPageBtnDisplayed)
                    {
                        wh.clickOnElement(nextPageBtnOnDialog);
                        wh.isElementDisplayed(uprnBy, 60);
                        uprnRowDisplayed = wh.getAllWebElementsText(uprnBy).Contains(uprnValue);
                        if (uprnRowDisplayed)
                        {
                            int rowNum = getRowOfUPRN_NewProperty(uprnValue);
                            SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[@class='ms-List-page']//div[@class='ms-List-cell'])[{rowNum}]")));
                            IWebElement we1 = wh.GetWebElement(UPRNrow);
                            IJavaScriptExecutor jse = DriverHelper.Driver as IJavaScriptExecutor;
                            jse.ExecuteScript("arguments[0].scrollIntoView(true);", we1);
                            wh.clickOnElement(UPRNrow);
                            wh.GetWebElement(By.XPath("//button//*[text()='Use Address']")).ClickUsingJavascript();

                        }

                    }
                }
                i = i++;

                if (i == 50)
                {
                    break;
                }

            } while (!uprnRowDisplayed);
        }

        public void selectHeridetamentByuprn_findAddress(string uprnValue)
        {
            waitHelpers wh = new waitHelpers();
            int rowSize = wh.getAllWebElements(By.CssSelector("div[aria-label='Address search results grid'] div[class='ms-List-page'] div[class='ms-List-cell']")).Count;
            By uprnBy = By.CssSelector("div[class='ms-List-page'] div[class='ms-List-cell'] [data-automation-key='uprn'] div");
            wh.isElementDisplayed(uprnBy, 60);
            SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[@class='ms-List-page']//div[@class='ms-List-cell'])[{rowSize}-1]")));
            SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[@class='ms-List-page']//div[@class='ms-List-cell'])[1]")));
            bool uprnRowDisplayed = wh.getAllWebElementsText(uprnBy).Contains(uprnValue);
            bool nextPageBtnDisplayed = false;
            By UPRNrow = By.XPath($"//div[text()='{uprnValue}']/parent::div[@data-automation-key='uprn']");
            By nextPageBtnOnDialog = By.XPath("//*[contains(@class,'ms-Dialog-main')]//button[@title='Next Page']");
            IJavaScriptExecutor js = DriverHelper.Driver as IJavaScriptExecutor;
            int i = 0;
            do
            {
                if (uprnRowDisplayed)
                {
                    int rowNum = getRowOfUPRN_NewProperty(uprnValue);
                    SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[@class='ms-List-page']//div[@class='ms-List-cell'])[{rowNum}]")));
                    IWebElement we = wh.GetWebElement(UPRNrow);
                    js.ExecuteScript("arguments[0].scrollIntoView(true);", we);
                    wh.clickOnElement(UPRNrow);
                    wh.GetWebElement(By.XPath("//button//*[text()='Use Address']")).ClickUsingJavascript();
                }
                else
                {
                    SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[@class='ms-List-page']//div[@class='ms-List-cell'])[{rowSize}-1]")));
                    nextPageBtnDisplayed = wh.isElementDisplayed(nextPageBtnOnDialog, 20);
                    if (nextPageBtnDisplayed)
                    {
                        wh.clickOnElement(nextPageBtnOnDialog);
                        wh.isElementDisplayed(uprnBy, 60);
                        uprnRowDisplayed = wh.getAllWebElementsText(uprnBy).Contains(uprnValue);
                        if (uprnRowDisplayed)
                        {
                            int rowNum = getRowOfUPRN_NewProperty(uprnValue);
                            SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[@class='ms-List-page']//div[@class='ms-List-cell'])[{rowNum}]")));
                            IWebElement we1 = wh.GetWebElement(UPRNrow);
                            IJavaScriptExecutor jse = DriverHelper.Driver as IJavaScriptExecutor;
                            jse.ExecuteScript("arguments[0].scrollIntoView(true);", we1);
                            wh.clickOnElement(UPRNrow);
                            wh.GetWebElement(By.XPath("//button//*[text()='Use Address']")).ClickUsingJavascript();

                        }

                    }
                }
                i = i++;

                if (i == 50)
                {
                    break;
                }

            } while (!uprnRowDisplayed);
        }

        public void selectHeridetamentNotByuprn_NewProperty(string uprnValue)
        {
            waitHelpers wh = new waitHelpers();
            int rowSize = wh.getAllWebElements(By.CssSelector("div[class='ms-List-page'] div[class='ms-List-cell']")).Count;
            By uprnBy = By.CssSelector("div[class='ms-List-page'] div[class='ms-List-cell'] [data-automation-key='uprn'] div");
            wh.isElementDisplayed(uprnBy, 60);
            SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[@class='ms-List-page']//div[@class='ms-List-cell'])[{rowSize}-1]")));
            SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[@class='ms-List-page']//div[@class='ms-List-cell'])[1]")));
            bool nonuprnRowDisplayed = !wh.getAllWebElementsText(uprnBy).Contains(uprnValue);
            bool nextPageBtnDisplayed = false;
            By UPRNrow = By.XPath($"//div[text()='{uprnValue}']/parent::div[@data-automation-key='uprn']");
            By nextPageBtnOnDialog = By.XPath("//*[contains(@class,'ms-Dialog-main')]//button[@title='Next Page']");
            IJavaScriptExecutor js = DriverHelper.Driver as IJavaScriptExecutor;
            int i = 0;
            do
            {
                if (nonuprnRowDisplayed)
                {
                    int rowNum = getRowOfNonUPRN_NewProperty(uprnValue);
                    SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[@class='ms-List-page']//div[@class='ms-List-cell'])[{rowNum}]")));
                    IWebElement we = wh.GetWebElement(UPRNrow);
                    js.ExecuteScript("arguments[0].scrollIntoView(true);", we);
                    wh.clickOnElement(UPRNrow);
                    wh.GetWebElement(By.XPath("//button//*[text()='Use Address']")).ClickUsingJavascript();
                }
                else
                {
                    SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[@class='ms-List-page']//div[@class='ms-List-cell'])[{rowSize}-1]")));
                    nextPageBtnDisplayed = wh.isElementDisplayed(nextPageBtnOnDialog, 20);
                    if (nextPageBtnDisplayed)
                    {
                        wh.clickOnElement(nextPageBtnOnDialog);
                        wh.isElementDisplayed(uprnBy, 60);
                        nonuprnRowDisplayed = !(wh.getAllWebElementsText(uprnBy).Contains(uprnValue));
                        if (nonuprnRowDisplayed)
                        {
                            int rowNum = getRowOfNonUPRN_NewProperty(uprnValue);
                            SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[@class='ms-List-page']//div[@class='ms-List-cell'])[{rowNum}]")));
                            IWebElement we1 = wh.GetWebElement(UPRNrow);
                            IJavaScriptExecutor jse = DriverHelper.Driver as IJavaScriptExecutor;
                            jse.ExecuteScript("arguments[0].scrollIntoView(true);", we1);
                            wh.clickOnElement(UPRNrow);
                            wh.GetWebElement(By.XPath("//button//*[text()='Use Address']")).ClickUsingJavascript();

                        }

                    }
                }
                i = i++;

                if (i == 50)
                {
                    break;
                }

            } while (!nonuprnRowDisplayed);
        }

        public int getRowOfUPRN_NewProperty(String uprnVallue)
        {
            waitHelpers wh = new waitHelpers();
            int rowNum = 0;
            IList<IWebElement> WebEleList = wh.getAllWebElements(By.CssSelector("div[class='ms-List-page'] div[class='ms-List-cell']"));
            By uprnBy = By.CssSelector("div[class='ms-List-page'] div[class='ms-List-cell'] [data-automation-key='uprn']");
            IList<IWebElement> WebEleListTxt = wh.getAllWebElements(uprnBy);
            int i = 0;
            foreach (var uprnEle in WebEleListTxt)
            {
                i = i + 1;
                String uprnEleTxt = uprnEle.Text;
                if (!String.IsNullOrEmpty(uprnEleTxt))
                {
                    if (uprnEleTxt.Equals(uprnVallue))
                    {
                        rowNum = i;
                        break;
                    }
                }

            }
            return rowNum;
        }

        public int getRowOfNonUPRN_NewProperty(String uprnVallue)
        {
            waitHelpers wh = new waitHelpers();
            int rowNum = 0;
            IList<IWebElement> WebEleList = wh.getAllWebElements(By.CssSelector("div[class='ms-List-page'] div[class='ms-List-cell']"));
            By uprnBy = By.CssSelector("div[class='ms-List-page'] div[class='ms-List-cell'] [data-automation-key='uprn']");
            IList<IWebElement> WebEleListTxt = wh.getAllWebElements(uprnBy);
            int i = 0;
            foreach (var uprnEle in WebEleListTxt)
            {
                i = i + 1;
                String uprnEleTxt = uprnEle.Text;
                if (!String.IsNullOrEmpty(uprnEleTxt))
                {
                    if (!(uprnEleTxt.Equals(uprnVallue)))
                    {
                        rowNum = i;
                        break;
                    }
                }

            }
            return rowNum;
        }

        public void selectSearchByDropDown(string fieldName)
        {
            waitHelpers wh = new waitHelpers();
            By searchByDropDown = SeleniumExtensions.getLocator("//div[contains(@id,'ModalFocusTrapZone')]//div[contains(@id,'Dropdown')]");
            By fieldNameBtn = SeleniumExtensions.getLocator($" //span[text()='{fieldName}']//ancestor::button");
            wh.clickOnElement(searchByDropDown);
            wh.clickOnElement(fieldNameBtn);
        }
        public void selectSearchByDropDown_Milo(string fieldName)
        {
            waitHelpers wh = new waitHelpers();
            By searchByDropDown = SeleniumExtensions.getLocator("//div[contains(@id,'Dropdown')]");
            By fieldNameBtn = SeleniumExtensions.getLocator($" //span[text()='{fieldName}']//ancestor::button");
            wh.clickOnElement(searchByDropDown);
            wh.clickOnElement(fieldNameBtn);
        }

        public String selectHeridetamentByuprn_new(string uprnValue, FeatureContext FC)
        {

            waitHelpers wh = new waitHelpers();

            SeleniumExtensions.scrollToBtnBasedOnLabelAndClick("Select");
            String addressValue = "";
            int rowSize = wh.getAllWebElements(By.CssSelector("div[class='ms-List-page'] div[class='ms-List-cell']")).Count;
            By uprnBy = By.CssSelector("div[class='ms-List-page'] div[class='ms-List-cell'] [data-automation-key='uprn_column']");
            wh.isElementDisplayed(uprnBy, 60);
            if (rowSize != 1) { }


            By UPRNrow = By.XPath($"//div[text()='{uprnValue}']/parent::div[@data-automation-key='uprn_column']");

            try
            {
                SeleniumExtensions.ScrollToElement(wh.GetWebElement(UPRNrow));
            }
            catch (Exception e)
            {
                SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[contains(@class,'ms-Modal-scrollableContent') and contains(@class,'scrollableContent')]//div[@class='ms-List-page']//div[@class='ms-List-cell'])[{rowSize}-1]")));
                SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[contains(@class,'ms-Modal-scrollableContent') and contains(@class,'scrollableContent')]//div[@class='ms-List-page']//div[@class='ms-List-cell'])[1]")));
            }


            bool uprnRowDisplayed = wh.getAllWebElementsText(uprnBy).Contains(uprnValue);
            bool nextPageBtnDisplayed = false;
            By nextPageBtnOnDialog = By.XPath("//*[contains(@class,'ms-Dialog-main')]//button[@title='Next Page']");
            IJavaScriptExecutor js = DriverHelper.Driver as IJavaScriptExecutor;
            int i = 0;
            do
            {
                if (uprnRowDisplayed)
                {
                    int rowNum = getRowOfUPRN(uprnValue);

                    SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[@class='ms-List-page']//div[@class='ms-List-cell'])[{rowNum}]")));
                    IWebElement we = wh.GetWebElement(UPRNrow);
                    wh.isElementDisplayed(we, waitTime);
                    js.ExecuteScript("arguments[0].scrollIntoView(true);", we);
                    wh.clickOnElement(UPRNrow);
                    String addressXpath = $"(//*[@data-automation-key='address_column']//button[@type='button'])[{rowNum}]";
                    addressValue = DriverHelper.Driver.FindElement(By.XPath(addressXpath)).Text.Trim();

                }
                else
                {
                    nextPageBtnDisplayed = wh.isElementDisplayed(nextPageBtnOnDialog, 20);
                    if (nextPageBtnDisplayed)
                    {
                        wh.clickOnElement(nextPageBtnOnDialog);
                        rowSize = wh.getAllWebElements(By.CssSelector("div[class*='ms-Modal-scrollableContent scrollableContent'] div[class='ms-List-page'] div[class='ms-List-cell']")).Count;
                        SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[@class='ms-List-page']//div[@class='ms-List-cell'])[{rowSize}-1]")));
                        wh.isElementDisplayed(uprnBy, waitTime);
                        uprnRowDisplayed = wh.getAllWebElementsText(uprnBy).Contains(uprnValue);
                        if (uprnRowDisplayed)
                        {
                            int rowNum = getRowOfUPRN(uprnValue);
                            SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[@class='ms-List-page']//div[@class='ms-List-cell'])[{rowNum}]")));
                            IWebElement we1 = wh.GetWebElement(UPRNrow);
                            IJavaScriptExecutor jse = DriverHelper.Driver as IJavaScriptExecutor;
                            jse.ExecuteScript("arguments[0].scrollIntoView(true);", we1);
                            wh.clickOnElement(UPRNrow);
                            String addressXpath = $"(//*[@data-automation-key='address_column']//button[@type='button'])[{rowNum}]";
                            addressValue = DriverHelper.Driver.FindElement(By.XPath(addressXpath)).Text.Trim();

                        }

                    }
                }
                i = i + 1;


                if (i == 10)
                {
                    throw new Exception($"unable to find hereditament with specific UPRN value {uprnValue}");
                    break;
                }

            } while (!uprnRowDisplayed);
            return addressValue;
        }


        public void selectHeridetamentByuprn(string uprnValue)
        {
            waitHelpers wh = new waitHelpers();
            SeleniumExtensions.scrollToBtnBasedOnLabelAndClick("Select");
            //int rowSize = wh.getAllWebElements(By.CssSelector("div[class='ms-List-page'] div[class='ms-List-cell']")).Count;
            int rowSize = wh.getAllWebElements(By.CssSelector("div[class*='ms-Modal-scrollableContent scrollableContent'] div[class='ms-List-page'] div[class='ms-List-cell']")).Count;
            By uprnBy = By.CssSelector("div[class='ms-List-page'] div[class='ms-List-cell'] [data-automation-key='uprn_column']");
            wh.isElementDisplayed(uprnBy, 60);
            if (rowSize != 1) { }
            //SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[@class='ms-List-page']//div[@class='ms-List-cell'])[{rowSize}-1]")));
            //SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[@class='ms-List-page']//div[@class='ms-List-cell'])[last()-1]")));

            By UPRNrow = By.XPath($"//div[text()='{uprnValue}']/parent::div[@data-automation-key='uprn_column']");

            try
            {
                SeleniumExtensions.ScrollToElement(wh.GetWebElement(UPRNrow));
            }
            catch (Exception e)
            {
                SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[contains(@class,'ms-Modal-scrollableContent') and contains(@class,'scrollableContent')]//div[@class='ms-List-page']//div[@class='ms-List-cell'])[{rowSize}-1]")));
                SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[contains(@class,'ms-Modal-scrollableContent') and contains(@class,'scrollableContent')]//div[@class='ms-List-page']//div[@class='ms-List-cell'])[1]")));
            }

            //SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[@class='ms-List-page']//div[@class='ms-List-cell'])[1]")));
            bool uprnRowDisplayed = wh.getAllWebElementsText(uprnBy).Contains(uprnValue);
            bool nextPageBtnDisplayed = false;
            By nextPageBtnOnDialog = By.XPath("//*[contains(@class,'ms-Dialog-main')]//button[@title='Next Page']");
            IJavaScriptExecutor js = DriverHelper.Driver as IJavaScriptExecutor;
            int i = 0;
            do
            {
                if (uprnRowDisplayed)
                {
                    int rowNum = getRowOfUPRN(uprnValue);
                    //SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[@class='ms-List-page']//div[@class='ms-List-cell'])[{rowNum}]")));
                    SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[contains(@class,'ms-Modal-scrollableContent') and contains(@class,'scrollableContent')]//div[@class='ms-List-page']//div[@class='ms-List-cell'])[{rowNum}]")));
                    IWebElement we = wh.GetWebElement(UPRNrow);
                    wh.isElementDisplayed(we, waitTime);
                    js.ExecuteScript("arguments[0].scrollIntoView(true);", we);
                    wh.clickOnElement(UPRNrow);

                }
                else
                {
                    nextPageBtnDisplayed = wh.isElementDisplayed(nextPageBtnOnDialog, 20);
                    if (nextPageBtnDisplayed)
                    {
                        wh.clickOnElement(nextPageBtnOnDialog);
                        rowSize = wh.getAllWebElements(By.CssSelector("div[class*='ms-Modal-scrollableContent scrollableContent'] div[class='ms-List-page'] div[class='ms-List-cell']")).Count;
                        SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[@class='ms-List-page']//div[@class='ms-List-cell'])[{rowSize}-1]")));
                        wh.isElementDisplayed(uprnBy, waitTime);
                        uprnRowDisplayed = wh.getAllWebElementsText(uprnBy).Contains(uprnValue);
                        if (uprnRowDisplayed)
                        {
                            int rowNum = getRowOfUPRN(uprnValue);
                            SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[@class='ms-List-page']//div[@class='ms-List-cell'])[{rowNum}]")));
                            IWebElement we1 = wh.GetWebElement(UPRNrow);
                            IJavaScriptExecutor jse = DriverHelper.Driver as IJavaScriptExecutor;
                            jse.ExecuteScript("arguments[0].scrollIntoView(true);", we1);
                            wh.clickOnElement(UPRNrow);

                        }

                    }
                }
                i = i + 1;


                if (i == 10)
                {
                    throw new Exception($"unable to find hereditament with specific UPRN value {uprnValue}");
                    break;
                }

            } while (!uprnRowDisplayed);
        }

        public void selectHeridetamentByuprnWithOutSelect(string uprnValue)
        {
            waitHelpers wh = new waitHelpers();
            int rowSize = wh.getAllWebElements(By.CssSelector("div[class*='ms-Modal-scrollableContent scrollableContent'] div[class='ms-List-page'] div[class='ms-List-cell']")).Count;
            By uprnBy = By.CssSelector("div[class='ms-List-page'] div[class='ms-List-cell'] [data-automation-key='uprn_column']");
            wh.isElementDisplayed(uprnBy, 60);
            if (rowSize != 1) { }

            By UPRNrow = By.XPath($"//div[text()='{uprnValue}']/parent::div[@data-automation-key='uprn_column']");

            try
            {
                SeleniumExtensions.ScrollToElement(wh.GetWebElement(UPRNrow));
            }
            catch (Exception e)
            {
                SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[contains(@class,'ms-Modal-scrollableContent') and contains(@class,'scrollableContent')]//div[@class='ms-List-page']//div[@class='ms-List-cell'])[{rowSize}-1]")));
                SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[contains(@class,'ms-Modal-scrollableContent') and contains(@class,'scrollableContent')]//div[@class='ms-List-page']//div[@class='ms-List-cell'])[1]")));
            }

            bool uprnRowDisplayed = wh.getAllWebElementsText(uprnBy).Contains(uprnValue);
            bool nextPageBtnDisplayed = false;
            By nextPageBtnOnDialog = By.XPath("//*[contains(@class,'ms-Dialog-main')]//button[@title='Next Page']");
            IJavaScriptExecutor js = DriverHelper.Driver as IJavaScriptExecutor;
            int i = 0;
            do
            {
                if (uprnRowDisplayed)
                {
                    int rowNum = getRowOfUPRN(uprnValue);
                    SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[contains(@class,'ms-Modal-scrollableContent') and contains(@class,'scrollableContent')]//div[@class='ms-List-page']//div[@class='ms-List-cell'])[{rowNum}]")));
                    IWebElement we = wh.GetWebElement(UPRNrow);
                    wh.isElementDisplayed(we, waitTime);
                    js.ExecuteScript("arguments[0].scrollIntoView(true);", we);
                    wh.clickOnElement(UPRNrow);

                }
                else
                {
                    nextPageBtnDisplayed = wh.isElementDisplayed(nextPageBtnOnDialog, 20);
                    if (nextPageBtnDisplayed)
                    {
                        wh.clickOnElement(nextPageBtnOnDialog);
                        rowSize = wh.getAllWebElements(By.CssSelector("div[class*='ms-Modal-scrollableContent scrollableContent'] div[class='ms-List-page'] div[class='ms-List-cell']")).Count;
                        SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[@class='ms-List-page']//div[@class='ms-List-cell'])[{rowSize}-1]")));
                        wh.isElementDisplayed(uprnBy, waitTime);
                        uprnRowDisplayed = wh.getAllWebElementsText(uprnBy).Contains(uprnValue);
                        if (uprnRowDisplayed)
                        {
                            int rowNum = getRowOfUPRN(uprnValue);
                            SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[@class='ms-List-page']//div[@class='ms-List-cell'])[{rowNum}]")));
                            IWebElement we1 = wh.GetWebElement(UPRNrow);
                            IJavaScriptExecutor jse = DriverHelper.Driver as IJavaScriptExecutor;
                            jse.ExecuteScript("arguments[0].scrollIntoView(true);", we1);
                            wh.clickOnElement(UPRNrow);

                        }

                    }
                }
                i = i + 1;


                if (i == 10)
                {
                    throw new Exception($"unable to find hereditament with specific UPRN value {uprnValue}");
                    break;
                }

            } while (!uprnRowDisplayed);
        }

        public void selectHeridetamentByDeletedUPRN(string uprnValue, string status)
        {
            waitHelpers wh = new waitHelpers();
            SeleniumExtensions.scrollToBtnBasedOnLabelAndClick("Select");
            IList<IWebElement> webEles = wh.getAllWebElements(By.CssSelector("[data-automation-key='band'] button"));
            foreach(var ele in webEles)
            {
                Thread.Sleep(1000);
                ele.Click();
            }
            int rowSize = wh.getAllWebElements(By.CssSelector("div[class*='ms-Modal-scrollableContent scrollableContent'] div[class='ms-List-page'] div[class='ms-List-cell']")).Count;
            By uprnBy = By.CssSelector("div[class='ms-List-page'] div[class='ms-List-cell'] [data-automation-key='uprn_column']");
            wh.isElementDisplayed(uprnBy, 60);
            if (rowSize != 1) { }

            By UPRNrow = By.XPath($"//div[text()='{uprnValue}']/parent::div[@data-automation-key='uprn_column']//following-sibling::div[@data-automation-key='band']/div[contains(text(),'{status}')]//parent::div[@data-automation-key='band']");

            try
            {
                SeleniumExtensions.ScrollToElement(wh.GetWebElement(UPRNrow));
            }
            catch (Exception e)
            {
                SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[contains(@class,'ms-Modal-scrollableContent') and contains(@class,'scrollableContent')]//div[@class='ms-List-page']//div[@class='ms-List-cell'])[{rowSize}-1]")));
                SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[contains(@class,'ms-Modal-scrollableContent') and contains(@class,'scrollableContent')]//div[@class='ms-List-page']//div[@class='ms-List-cell'])[1]")));
            }

            //SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[@class='ms-List-page']//div[@class='ms-List-cell'])[1]")));
            bool uprnRowDisplayed = wh.getAllWebElementsText(uprnBy).Contains(uprnValue);
            bool nextPageBtnDisplayed = false;
            By nextPageBtnOnDialog = By.XPath("//*[contains(@class,'ms-Dialog-main')]//button[@title='Next Page']");
            IJavaScriptExecutor js = DriverHelper.Driver as IJavaScriptExecutor;
            int i = 0;
            do
            {
                if (uprnRowDisplayed)
                {
                    int rowNum = getRowOfUPRN(uprnValue);
                    //SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[@class='ms-List-page']//div[@class='ms-List-cell'])[{rowNum}]")));
                    SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[contains(@class,'ms-Modal-scrollableContent') and contains(@class,'scrollableContent')]//div[@class='ms-List-page']//div[@class='ms-List-cell'])[{rowNum}]")));
                    IWebElement we = wh.GetWebElement(UPRNrow);
                    wh.isElementDisplayed(we, waitTime);
                    js.ExecuteScript("arguments[0].scrollIntoView(true);", we);
                    wh.clickOnElement(UPRNrow);

                }
                else
                {
                    nextPageBtnDisplayed = wh.isElementDisplayed(nextPageBtnOnDialog, 20);
                    if (nextPageBtnDisplayed)
                    {
                        wh.clickOnElement(nextPageBtnOnDialog);
                        rowSize = wh.getAllWebElements(By.CssSelector("div[class*='ms-Modal-scrollableContent scrollableContent'] div[class='ms-List-page'] div[class='ms-List-cell']")).Count;
                        SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[@class='ms-List-page']//div[@class='ms-List-cell'])[{rowSize}-1]")));
                        wh.isElementDisplayed(uprnBy, waitTime);
                        uprnRowDisplayed = wh.getAllWebElementsText(uprnBy).Contains(uprnValue);
                        if (uprnRowDisplayed)
                        {
                            int rowNum = getRowOfUPRN(uprnValue);
                            SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[@class='ms-List-page']//div[@class='ms-List-cell'])[{rowNum}]")));
                            IWebElement we1 = wh.GetWebElement(UPRNrow);
                            IJavaScriptExecutor jse = DriverHelper.Driver as IJavaScriptExecutor;
                            jse.ExecuteScript("arguments[0].scrollIntoView(true);", we1);
                            wh.clickOnElement(UPRNrow);

                        }

                    }
                }
                i = i + 1;


                if (i == 10)
                {
                    throw new Exception($"unable to find hereditament with specific UPRN value {uprnValue}");
                    break;
                }

            } while (!uprnRowDisplayed);
        }

        public void selectHeridetamentByuprn(string uprnValue,string status)
        {
            waitHelpers wh = new waitHelpers();
            SeleniumExtensions.scrollToBtnBasedOnLabelAndClick("Select");
            int rowSize = wh.getAllWebElements(By.CssSelector("div[class*='ms-Modal-scrollableContent scrollableContent'] div[class='ms-List-page'] div[class='ms-List-cell']")).Count;
            By uprnBy = By.CssSelector("div[class='ms-List-page'] div[class='ms-List-cell'] [data-automation-key='uprn_column']");
            wh.isElementDisplayed(uprnBy, 60);
            if (rowSize != 1) { }

            By UPRNrow = By.XPath($"//div[text()='{uprnValue}']/parent::div[@data-automation-key='uprn_column']//following-sibling::div[@data-automation-key='addressstatus_column']/div[text()='{status}']//parent::div[@data-automation-key='addressstatus_column']");

            try
            {
                SeleniumExtensions.ScrollToElement(wh.GetWebElement(UPRNrow));
            }
            catch (Exception e)
            {
                SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[contains(@class,'ms-Modal-scrollableContent') and contains(@class,'scrollableContent')]//div[@class='ms-List-page']//div[@class='ms-List-cell'])[{rowSize}-1]")));
                SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[contains(@class,'ms-Modal-scrollableContent') and contains(@class,'scrollableContent')]//div[@class='ms-List-page']//div[@class='ms-List-cell'])[1]")));
            }

            //SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[@class='ms-List-page']//div[@class='ms-List-cell'])[1]")));
            bool uprnRowDisplayed = wh.getAllWebElementsText(uprnBy).Contains(uprnValue);
            bool nextPageBtnDisplayed = false;
            By nextPageBtnOnDialog = By.XPath("//*[contains(@class,'ms-Dialog-main')]//button[@title='Next Page']");
            IJavaScriptExecutor js = DriverHelper.Driver as IJavaScriptExecutor;
            int i = 0;
            do
            {
                if (uprnRowDisplayed)
                {
                    int rowNum = getRowOfUPRN(uprnValue);
                    //SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[@class='ms-List-page']//div[@class='ms-List-cell'])[{rowNum}]")));
                    SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[contains(@class,'ms-Modal-scrollableContent') and contains(@class,'scrollableContent')]//div[@class='ms-List-page']//div[@class='ms-List-cell'])[{rowNum}]")));
                    IWebElement we = wh.GetWebElement(UPRNrow);
                    wh.isElementDisplayed(we, waitTime);
                    js.ExecuteScript("arguments[0].scrollIntoView(true);", we);
                    wh.clickOnElement(UPRNrow);

                }
                else
                {
                    nextPageBtnDisplayed = wh.isElementDisplayed(nextPageBtnOnDialog, 20);
                    if (nextPageBtnDisplayed)
                    {
                        wh.clickOnElement(nextPageBtnOnDialog);
                        rowSize = wh.getAllWebElements(By.CssSelector("div[class*='ms-Modal-scrollableContent scrollableContent'] div[class='ms-List-page'] div[class='ms-List-cell']")).Count;
                        SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[@class='ms-List-page']//div[@class='ms-List-cell'])[{rowSize}-1]")));
                        wh.isElementDisplayed(uprnBy, waitTime);
                        uprnRowDisplayed = wh.getAllWebElementsText(uprnBy).Contains(uprnValue);
                        if (uprnRowDisplayed)
                        {
                            int rowNum = getRowOfUPRN(uprnValue);
                            SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[@class='ms-List-page']//div[@class='ms-List-cell'])[{rowNum}]")));
                            IWebElement we1 = wh.GetWebElement(UPRNrow);
                            IJavaScriptExecutor jse = DriverHelper.Driver as IJavaScriptExecutor;
                            jse.ExecuteScript("arguments[0].scrollIntoView(true);", we1);
                            wh.clickOnElement(UPRNrow);

                        }

                    }
                }
                i = i + 1;


                if (i == 10)
                {
                    throw new Exception($"unable to find hereditament with specific UPRN value {uprnValue}");
                    break;
                }

            } while (!uprnRowDisplayed);
        }

        public void selectHeridetamentByuprnNewProp(string uprnValue)
        {
            waitHelpers wh = new waitHelpers();
            int rowSize = wh.getAllWebElements(By.CssSelector("div[class='ms-List-page'] div[class='ms-List-cell']")).Count;
            By uprnBy = By.CssSelector("div[class='ms-List-page'] div[class='ms-List-cell'] [data-automation-key='uprn'] div");
            wh.isElementDisplayed(uprnBy, 60);
            SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[@class='ms-List-page']//div[@class='ms-List-cell'])[{rowSize}-1]")));
            SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[@class='ms-List-page']//div[@class='ms-List-cell'])[1]")));
            bool uprnRowDisplayed = wh.getAllWebElementsText(uprnBy).Contains(uprnValue);
            bool nextPageBtnDisplayed = false;
            By UPRNrow = By.XPath($"//div[text()='{uprnValue}']/parent::div[@data-automation-key='uprn']");
            By nextPageBtnOnDialog = By.XPath("//*[contains(@class,'ms-Dialog-main')]//button[@title='Next Page']");
            IJavaScriptExecutor js = DriverHelper.Driver as IJavaScriptExecutor;
            int i = 0;
            do
            {
                if (uprnRowDisplayed)
                {
                    int rowNum = getRowOfUPRN_NewProp(uprnValue);
                    SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[@class='ms-List-page']//div[@class='ms-List-cell'])[{rowNum}]")));
                    IWebElement we = wh.GetWebElement(UPRNrow);
                    js.ExecuteScript("arguments[0].scrollIntoView(true);", we);
                    wh.clickOnElement(UPRNrow);

                }
                else
                {
                    SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[@class='ms-List-page']//div[@class='ms-List-cell'])[{rowSize}-1]")));
                    nextPageBtnDisplayed = wh.isElementDisplayed(nextPageBtnOnDialog, 20);
                    if (nextPageBtnDisplayed)
                    {
                        wh.clickOnElement(nextPageBtnOnDialog);
                        wh.isElementDisplayed(uprnBy, 60);
                        uprnRowDisplayed = wh.getAllWebElementsText(uprnBy).Contains(uprnValue);
                        if (uprnRowDisplayed)
                        {
                            int rowNum = getRowOfUPRN_NewProp(uprnValue);
                            SeleniumExtensions.ScrollToElement(wh.GetWebElement(By.XPath($"(//div[@class='ms-List-page']//div[@class='ms-List-cell'])[{rowNum}]")));
                            IWebElement we1 = wh.GetWebElement(UPRNrow);
                            IJavaScriptExecutor jse = DriverHelper.Driver as IJavaScriptExecutor;
                            jse.ExecuteScript("arguments[0].scrollIntoView(true);", we1);
                            wh.clickOnElement(UPRNrow);

                        }

                    }
                }
                i = i + 1;

                if (i == 50)
                {
                    break;
                }

            } while (!uprnRowDisplayed);
        }

        public int getRowOfUPRN(String uprnVallue)
        {
            waitHelpers wh = new waitHelpers();
            int rowNum = 0;
            IList<IWebElement> WebEleList = wh.getAllWebElements(By.CssSelector("div[class*='ms-Modal-scrollableContent scrollableContent'] div[class='ms-List-page'] div[class='ms-List-cell']"));
            By uprnBy = By.CssSelector("div[class='ms-List-page'] div[class='ms-List-cell'] [data-automation-key='uprn_column']");
            IList<IWebElement> WebEleListTxt = wh.getAllWebElements(uprnBy);
            int i = 0;
            foreach (var uprnEle in WebEleListTxt)
            {
                i = i + 1;
                String uprnEleTxt = uprnEle.Text;
                if (!String.IsNullOrEmpty(uprnEleTxt))
                {
                    if (uprnEleTxt.Equals(uprnVallue))
                    {
                        rowNum = i;
                        break;
                    }
                }

            }
            return rowNum;
        }

        public int getRowOfUPRN_NewProp(String uprnVallue)
        {
            waitHelpers wh = new waitHelpers();
            int rowNum = 0;
            IList<IWebElement> WebEleList = wh.getAllWebElements(By.CssSelector("div[class='ms-List-page'] div[class='ms-List-cell']"));
            By uprnBy = By.CssSelector("div[class='ms-List-page'] div[class='ms-List-cell'] [data-automation-key='uprn']");
            IList<IWebElement> WebEleListTxt = wh.getAllWebElements(uprnBy);
            int i = 0;
            foreach (var uprnEle in WebEleListTxt)
            {
                i = i + 1;
                String uprnEleTxt = uprnEle.Text;
                if (!String.IsNullOrEmpty(uprnEleTxt))
                {
                    if (uprnEleTxt.Equals(uprnVallue))
                    {
                        rowNum = i;
                        break;
                    }
                }

            }
            return rowNum;
        }

        public void selectHeridetamentNoJobsandSingleliveEntry()
        {
            waitHelpers wh = new waitHelpers();
            int i = 0;
            WebDriverWait ww = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(5));
            IList<IWebElement> hereditamentList = wh.GetWebElements(By.CssSelector("div[data-automationid='DetailsRowFields'] div[data-automation-key='address_column'] button"));
            IList<IWebElement> relatedJobs = new List<IWebElement>();
            IList<IWebElement> assments = new List<IWebElement>();
            bool isRelatedJobs = false;
            bool isLiveEntry = false;
            CommonPage cp = new CommonPage();
            while (!isRelatedJobs && !isLiveEntry)
            {
                foreach (var hereditamentLink in hereditamentList)
                {
                    hereditamentLink.ClickUsingJavascript();
                    IList<string> windowHandles = new List<string>(DriverHelper.Driver.WindowHandles);
                    DriverHelper.Driver.SwitchTo().Window(windowHandles[1]);
                    SeleniumExtensions.WaitForReadyStateToComplete();
                    cp.clickOnDataResearchFormTab("Related Jobs", "Hereditament Form", ww);
                    relatedJobs = DriverHelper.Driver.FindElements(By.CssSelector("div[class='ag-center-cols-container']>div"));
                    cp.clickOnDataResearchFormTab("Assessments", "Hereditament Form");
                    assments = wh.GetWebElements(SeleniumExtensions.getLocator($"a[role = 'link']"));
                    if (assments.Count() == 1)
                    {
                        isLiveEntry = true;
                    }
                    if (relatedJobs.Count() == 0)
                    {
                        isRelatedJobs = true;
                    }

                    if (isLiveEntry && isRelatedJobs)
                    {
                        DriverHelper.Driver.Close();
                        DriverHelper.Driver.SwitchTo().Window(windowHandles[0]);
                        ListOfSearchedHereditamentsRadioList.ElementAt(i).Click();
                        break;
                    }
                    else
                    {
                        DriverHelper.Driver.Close();
                        DriverHelper.Driver.SwitchTo().Window(windowHandles[0]);
                    }
                    if ((isLiveEntry || isRelatedJobs) && NextPageOnDialog.IsEnabled())
                    {
                        i = 0;
                        NextPageOnDialog.ClickUsingJavascript();
                        hereditamentList = wh.GetWebElements(By.CssSelector("div[data-automationid='DetailsRowFields'] div[data-automation-key='address_column'] button"));
                    }
                    i = i + 1;

                }
            }

        }

        public void selectHeridetamentBasedOnLabel(String Value)
        {
            waitHelpers wh = new waitHelpers();
            bool isEleDisplayed = false;
            while (!isEleDisplayed)
            {
                var hereditamentCount = ListOfSearchedHereditamentsRadioList.Count();
                for (int i = 0; i < hereditamentCount; i++)
                {
                    String addressXpath = $"(//*[@data-automation-key='address_column']//button)[{i + 1}]";
                    String addressValue = DriverHelper.Driver.FindElement(By.XPath(addressXpath)).Text;
                    Console.WriteLine("Value : " + Value + "& address value from find heriditament tool : " + addressValue);
                    if (Value.Equals(addressValue))
                    {
                        ListOfSearchedHereditamentsRadioList.ElementAt(i).Click();
                        isEleDisplayed = true;
                        break;
                    }
                    if (i == (hereditamentCount - 1))
                    {
                        bool paginationDisplayed = wh.GetWebElement(By.XPath("//button[@title='Next Page' and not(contains(@class,'disable'))]"), 1);
                        if (paginationDisplayed && !isEleDisplayed)
                        {
                            wh.clickOnElement(By.XPath("//button[@title='Next Page' and not(contains(@class,'disable'))]"));

                        }
                    }
                }
            }
        }



    }
}
