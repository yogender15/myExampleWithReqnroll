using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using POMSeleniumFrameworkPoc1.DTO.UI;
using POMSeleniumFrameworkPoc1.Pages.UI.CommonComponents;
using POMSeleniumFrameworkPoc1.Pages.UI.RBSP_BP;
using Reqnroll;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace POMSeleniumFrameworkPoc1.Helpers
{
    public abstract class BasePage
    {
        public static IWebDriver Driver;
        private By ProposedDatePickerSelector => By.CssSelector("input[aria-label='Date of Proposed Effective Date']");

        private By ProposedDatePickerSelectorDialog => By.CssSelector("[data-id*='voa_proposedeffectivedate'] input[aria-label='Date of Proposed Effective Date']");

        public static IWebElement ScrollDiv => Driver.WaitForElement(By.CssSelector("[id*='tab-section']"));
        public IWebElement ScrollHereditamentPageDiv => Driver.WaitForElement(By.CssSelector("#tab-section3"));
        public By EffectiveDatePickerSelector => By.CssSelector("input[aria-label='Date of Effective From Date']");
        // public By RequestTypeLookupField => By.CssSelector("input[aria-label='Look for Request Type']");
        public By RequestTypeLookupField => By.CssSelector("input[aria-label='Request Type, Lookup']");
        // public By JobSubTypeLookupField => By.CssSelector("input[aria-label='Look for Job Sub Type']");
        public By RequestTypeSearchBtn => By.CssSelector("section[aria-label='Categorisation'] input[aria-label='Request Type, Lookup']+button[title='Search']");

        public By JobSubTypeLookupField => By.CssSelector("input[aria-label='Sub Job Type, Lookup']");
        public By JobTypeSearchBtn => By.CssSelector("section[aria-label='Categorisation'] input[aria-label='Job Type, Lookup']+button[title='Search']");

        public By RequestTypeOnDialog => By.CssSelector("[role='dialog'] input[aria-label='Request Type, Lookup']");
        // public By JobSubTypeLookupField => By.CssSelector("input[aria-label='Look for Job Sub Type']");
        public By RequestTypeSearchBtnOnDialog => By.CssSelector("[role='dialog'] input[aria-label='Request Type, Lookup']+button[title='Search']");
        public By ReasonGround => By.CssSelector("[role='dialog'] input[aria-label='Reason/Ground, Lookup']");
        public By ReasonGroundSrcBtn => By.CssSelector("[role='dialog'] input[aria-label='Reason/Ground, Lookup']+button[title='Search']");

        public By JobTypeOnDialog => By.CssSelector("[role='dialog'] input[aria-label='Job Type, Lookup']");
        public By JobTypeSearchBtnOnDialog => By.CssSelector("[role='dialog'] input[aria-label='Job Type, Lookup']+button[title='Search']");
        // "[role='dialog'] button[aria-label='Close']"
        public IWebElement CloseButtOnDialog => Driver.WaitForElement(By.CssSelector("[role='dialog'] button[aria-label='Close']"));
        public IWebElement IgnoreAndSaveButton => Driver.WaitForElements(By.CssSelector("button[aria-label='Ignore and save']")).LastOrDefault();
        public IWebElement OkButton => Driver.WaitForElement(By.CssSelector("button[aria-label='OK']"));
        public IWebElement AssigntouserButton => Driver.WaitForElement(By.CssSelector("button[data-id='okButton']"));

        public IWebElement OkButtonopopup => Driver.WaitForElement(By.CssSelector("button[title='Ok']"));


        public IWebElement OkButtonpopupDialog => Driver.WaitForElement(By.CssSelector("button[data-id='confirmButton']"));

        public IWebElement SaveLabel => Driver.WaitForElement(By.XPath("//*[text()='Save']"));
        public IWebElement StatusSavedLabel => Driver.WaitForElement(By.CssSelector("[aria-label='Save status - Saved']"));
        public IWebElement StatusUnsavedLabel => Driver.WaitForElement(By.CssSelector("[aria-label*='Save status - Unsaved']"));
        public IWebElement StatusSavingLabel => Driver.WaitForElement(By.CssSelector("[aria-label*='Save status - Saving']"));
        public IWebElement SavingLoaderLabel => Driver.WaitForElement(By.XPath("//*[contains(text(),'Saving..')]"));

        public IWebElement LookUpResultFirstValue => Driver.WaitForElement(By.CssSelector("[aria-label*='Lookup results'] ul li [data-id*='infoContainer']"));

        private IWebElement JourneyDialogStageButton => Driver.WaitForElement(By.CssSelector("[role = 'dialog'] [id *= 'FooterContainer'] button"));
        public IWebElement FilterBy => Driver.WaitForElement(By.XPath("//ul[contains(@class,'ms-ContextualMenu-list')]/li/button[@name='Filter by']"));
        //  "//*[@role='alertdialog' and @aria-label='Filter by']//input[@aria-label='Filter by value']"
        public IWebElement FilterByValue => Driver.WaitForElement(By.XPath("//*[@role='alertdialog' and contains(@aria-label,'Filter')]//*[@aria-label='Filter by value']"));
        public IWebElement ApplyButton => Driver.WaitForElement(By.XPath("//button//*[text()='Apply']"));
        public IWebElement ViewSelector => Driver.WaitForElement(By.CssSelector("button[id*='ViewSelector'] i[data-icon-name='ChevronDown']"));
        public By ViewSelectorRows => By.XPath("//*[contains(@class,'ag-center-cols-container') and @role='rowgroup']//*[contains(@class,'ag-row-position-absolute')]");

        public IWebElement moreCommandsJob => Driver.WaitForElement(By.CssSelector("button[aria-label*='More commands']"));
        By moreCommandsJobBy = By.CssSelector("button[aria-label*='More commands']");

        public IWebElement moreTabsDialog => Driver.WaitForElement(By.CssSelector("[role='dialog'] [aria-label='More Tabs'] div"));
        public IWebElement moreTabsJob => Driver.WaitForElement(By.CssSelector("[aria-label='More Tabs'] div"));
        public By moreTabsJobBy = By.CssSelector("[aria-label='More Tabs'] div");

        // PVT Tab
        public IWebElement PVTTab(string tabName)
        {
            string customizeSelector = $"[aria-label='Property Visualisation Tool'] [role='tablist'] [name='{tabName}']";
            return Driver.WaitForElement(By.CssSelector(customizeSelector));
        }
        public IWebElement PVTWelcomeText => Driver.WaitForElement(By.CssSelector("[aria-label='Property Visualisation Tool'] [class*='gridContainer'] label[title='Welcome']"));

        public IWebElement PVTCreateButton => Driver.WaitForElement(By.CssSelector("[aria-label='Property Visualisation Tool'] [class*='gridContainer'] button[title='Create']"));
        public IWebElement PADSSuccessMsg => Driver.WaitForElement(By.CssSelector("[aria-label='Property Visualisation Tool'] [title='PAD created successfully.']"));

        public IWebElement PADExpandArrow => Driver.WaitForElement(By.CssSelector("[aria-label='Property Visualisation Tool'] [data-automationid='DetailsList'] [data-automationid='DetailsHeader'] [class*='cellIsGroupExpander']"));
        // "[aria-label='Property Visualisation Tool'] [data-automationid='DetailsRow']"
        public IWebElement PADRows => Driver.WaitForElement(By.CssSelector("[aria-label='Property Visualisation Tool'] [data-automationid='DetailsRow']"));

        public IWebElement PADDataCheckBox => Driver.WaitForElement(By.CssSelector("[aria-label='Property Visualisation Tool'] [data-automationid='DetailsRow'] [data-automationid='DetailsRowCheck']"));

        public IWebElement PADEdit => Driver.WaitForElement(By.CssSelector("[aria-label='Property Visualisation Tool'] [class*='gridContainer'] [class*='ms-CommandBar'] [class*='ms-OverflowSet'] button [class*='ms-Button-label']:contains('Edit')"));

        // // "//ul[@aria-label='Job Form']//li[@aria-label='More Tabs']"
        public IWebElement MoreTabs => Driver.WaitForElement(By.XPath("//ul[@aria-label='Job Form']//li[@aria-label='More Tabs']"));
        public IWebElement MoreTabsOnReviewForm => Driver.WaitForElement(By.XPath("//ul[@aria-label='Review Form']//li[@aria-label='More Tabs']"));

        private IWebElement SaveInProgressError => Driver.WaitForElement(By.CssSelector("[data-id='errorDialogdialog'] [data-id='dialogTitleText']"));
        private IWebElement SaveInProgressOKBtn => Driver.WaitForElement(By.CssSelector("[data-id='errorDialogdialog'] button[aria-label='OK']"));
        private By SavingAlert => By.CssSelector("[aria-live='polite'][role='alert']:contains('Saving')");

        private IWebElement MoreCommandsForJob => Driver.WaitForElement(By.CssSelector("[data-id*='CommandBar'] button[aria-label='More commands for Job']"));

        private By RowsOnTab => By.XPath("//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row')]");

        //    public IWebElement Statustext => Driver.WaitForElement(By.XPath("//*[text()='Status Reason']/preceding-sibling::*"));

        private IWebElement MoreTabsOptionJobForm(string optionName)
        {
            string customizeSelector = $"[aria-label*='{optionName}']";
            return Driver.WaitForElement(By.CssSelector(customizeSelector));
        }

        public IWebElement PVTCommandBarOption(string commandOption)
        {
            string customizeSelector = $"//*[@aria-label='Property Visualisation Tool']//*[contains(@class,'gridContainer')]//*[contains(@class,'ms-CommandBar')]//*[contains(@class,'ms-OverflowSet')]//button//*[contains(@class,'ms-Button-label') and text()='{commandOption}']";
            return Driver.WaitForElement(By.XPath(customizeSelector));
        }
        public IWebElement ClickButtonOnAssignDialog(string buttonName)
        {
            string customizeSelector = $"[role = 'dialog'] button[title ='{buttonName}']";
            return Driver.WaitForElement(By.CssSelector(customizeSelector));
        }
        // "[role='dialog'][data-lp-id='dialogView|Assign'] button[aria-label='Assign']"
        // Common


        public IWebElement ViewValue(string displayFilterValue)
        {
            string customizeSelector = $"//*[@aria-label='View Options']//ul//li//button//label[text()='{displayFilterValue}']";
            return Driver.WaitForElement(By.XPath(customizeSelector));
        }

        public IWebElement JourneyHeader(String journeyHeader)
        {
            // "//button[contains(@aria-label,'Maintain Assessment') and contains(@data-id,'MscrmControls.Containers.ProcessBreadCrumb-processHeaderStageButton')]//*[contains(@id,'stageIndicatorContainer') and @title='Maintain Assessment']//*[contains(@data-id,'ProcessBreadCrumb-stageIndicatorInnerHolder')]"
            //   string customizeSelector = $"//button[contains(@aria-label,'{journeyHeader}') and contains(@data-id,'MscrmControls.Containers.ProcessBreadCrumb-processHeaderStageButton')]//*[contains(@id,'stageNameContainer') and @title='{journeyHeader}']";
            string customizeSelector = $"//button[contains(@aria-label,'{journeyHeader}') and contains(@data-id,'MscrmControls.Containers.ProcessBreadCrumb-processHeaderStageButton')]//*[contains(@id,'stageNameContainer') and @title='{journeyHeader}']";
            return Driver.WaitForElement(By.XPath(customizeSelector));
        }


        public By JourneyHeaderUsingBySelector(string journeyHeader)
        {
            //stageNameContainer
            string customizeSelector = $"//button[contains(@aria-label,'{journeyHeader}') and contains(@data-id,'MscrmControls.Containers.ProcessBreadCrumb-processHeaderStageButton')]";////*[contains(@id,'stageIndicatorContainere0a1b32b') and @title='{journeyHeader}'];
            return By.XPath(customizeSelector);
        }

        private IWebElement JourneyStage(String buttonName)
        {
            string customizeSelector = $"//*[contains(@id,'MscrmControls.Containers.ProcessStageControl-processHeaderStageFlyoutContainer')]//button[@aria-label='{buttonName}']";
            return Driver.WaitForElement(By.XPath(customizeSelector));
        }
        private By JourneyStageUsingBy(String buttonName)
        {
            string customizeSelector = $"//*[contains(@id,'MscrmControls.Containers.ProcessStageControl-processHeaderStageFlyoutContainer')]//button[@aria-label='{buttonName}']";
            return By.XPath(customizeSelector);
        }

        private IWebElement ButtonByTitle(string buttonTitle)
        {
            string customizeSelector = $"button[title='{buttonTitle}']";
            return Driver.WaitForElement(By.CssSelector(customizeSelector));
        }
        public IWebElement Statustext => Driver.WaitForElement(By.XPath("//*[contains(@id,'headerControlsList')]//*[text()='Status Reason']/preceding-sibling::*"));

        public By StatustextBy => By.XPath("//*[contains(@id,'headerControlsList')]//*[text()='Status Reason']/preceding-sibling::*");

        private IWebElement headerTitle => Driver.WaitForElement(By.CssSelector("[data-id='headerContainer'] h1"));

        private IWebElement MenuItem(string sectionName, string menuItem)
        {
            string customizeSelector = $"ul[aria-label='{sectionName}'] li[aria-label='{menuItem}']";
            return Driver.WaitForElement(By.CssSelector(customizeSelector));
        }
        private IWebElement CommandBarItem(string commandBarOption)
        {
            string customizeSelector = $"//ul[contains(@data-lp-id,'commandbar')]//li//button//*[text()='{commandBarOption}']";
            return Driver.WaitForElement(By.XPath(customizeSelector));
        }

        private By CommandBarItemUsingBy(string commandBarOption)
        {
            string customizeSelector = $"//ul[contains(@data-lp-id,'commandbar')]//li//button//*[text()='{commandBarOption}']";
            return By.XPath(customizeSelector);
        }
        public By CommandBarItemOnDialog(string commandBarOption)
        {
            string customizeSelector = $"//*[@role='dialog']//ul[contains(@data-lp-id,'commandbar')]//li//button//*[text()='{commandBarOption}']";
            return By.XPath(customizeSelector);
        }

        public void clickOnMenuElement(String menuItem)
        {
            //string customizeSelector = $"//ul[contains(@data-lp-id,'commandbar')]//li//button//*[text()='{menuItem}']";
            string customizeSelector = $"//ul[contains(@data-lp-id,'commandbar')]//li//*[text()='{menuItem}']//ancestor::button";
            SeleniumExtensions.WaitForElementAndClick(By.XPath(customizeSelector));

        }

        public void clickOnDialogMenuElement(String menuItem)
        {
            String loadingSec = $"section[id = 'appProgressIndicatorView_popupContainer']";
            By loadingSecBy = SeleniumExtensions.getLocator(loadingSec);
            SeleniumExtensions.waitUntillElementInvisible(loadingSecBy);
            waitHelpers wh = new waitHelpers();
            By dialogMenuSelector = By.XPath($"//*[contains(@id,'dialog')]//ul[contains(@data-lp-id,'commandbar')]//li//*[text()='{menuItem}']//ancestor::button");
            wh.elementToClickble(dialogMenuSelector);
            wh.isElementDisplayed(dialogMenuSelector, 60);
            wh.jsClickOnElement(dialogMenuSelector);
        }

        public void clickOnDialogMenuElement(String menuItem, int position)
        {
            SeleniumExtensions.WaitForReadyStateToComplete();
            waitHelpers wh = new waitHelpers();
            By dialogMenuSelector = By.XPath($"(//*[contains(@id,'dialog')]//ul[contains(@data-lp-id,'commandbar')]//li//*[text()='{menuItem}'])[{position}]//ancestor::button");
            wh.elementToClickble(dialogMenuSelector);
            wh.isElementDisplayed(dialogMenuSelector, 60);
            wh.jsClickOnElement(dialogMenuSelector);
        }

        public void clickOnDialogBtnElement(String btnName)
        {
            waitHelpers wh = new waitHelpers();
            By dialogMenuSelector = By.XPath($"//div[@role='dialog']//button[@aria-label='{btnName}']");
            wh.elementToClickble(dialogMenuSelector);
            wh.isElementDisplayed(dialogMenuSelector, 60);
            wh.jsClickOnElement(dialogMenuSelector);
        }

        public void clickOnDialogBtnElement(String btnName, int position)
        {
            waitHelpers wh = new waitHelpers();
            By dialogMenuSelector = By.XPath($"(//div[@role='dialog']//button[@aria-label='{btnName}'])[{position}]");
            wh.elementToClickble(dialogMenuSelector);
            wh.isElementDisplayed(dialogMenuSelector, 60);
            wh.jsClickOnElement(dialogMenuSelector);
        }


        public void clickOnSaveAndCloseOnDialog(String menuItem)
        {
            String loadingSec = $"section[id = 'appProgressIndicatorView_popupContainer']";
            By loadingSecBy = SeleniumExtensions.getLocator(loadingSec);
            SeleniumExtensions.waitUntillElementInvisible(loadingSecBy);
            waitHelpers wh = new waitHelpers();
            CommonPage cp = new CommonPage();
            //cp.waitforTopMenuBarLoading();
            By dialogMenuSelector = By.XPath($"//*[contains(@id,'dialog')]//ul[contains(@data-lp-id,'commandbar')]//li//*[text()='{menuItem}']//ancestor::button");
            wh.isElementDisplayed(dialogMenuSelector, 60);
            By closeDialog = By.CssSelector($"[aria-label='Close']");
            try
            {
                if (wh.WaitTillElementDisplayed(dialogMenuSelector, 2))
                {
                    wh.isElementDisplayed(dialogMenuSelector, 60);
                    wh.elementToClickble(dialogMenuSelector);
                    wh.GetWebElement(dialogMenuSelector).Click();
                }
                else
                {
                    wh.elementToClickble(closeDialog);
                    wh.jsClickOnElement(closeDialog);
                    ClickMenuListOptionFromCommandBarWithOuJS("Validate Request", "Request Action");
                    wh.GetWebElement(dialogMenuSelector).Click();

                }
            }
            catch (Exception e)
            {
                wh.jsClickOnElement(closeDialog);
                Pages.UI.RBSP_BP.CommonPage commonpage = new Pages.UI.RBSP_BP.CommonPage();
                commonpage.clickEleOnDialogIfappears("Discard changes", "Unsaved changes");
                ClickMenuListOptionFromCommandBarWithOuJS("Validate Request", "Request Action");
                wh.GetWebElement(dialogMenuSelector).Click();
            }
        }

        public void clickOnMainMenuMoreElement(String menuItem)
        {
            SeleniumExtensions.WaitForReadyStateToComplete();
            waitHelpers wh = new waitHelpers();
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(5));
            try
            {
                string customizeSelector = $"//ul[contains(@data-lp-id,'commandbar')]//li//*[text()='{menuItem}']//ancestor::button";
                wh.clickOnElement(SeleniumExtensions.getLocator(customizeSelector), wait);
            }
            catch (Exception e)
            {
                moreCommandsJob.ClickUsingJavascript();
                string customizeSelector = $"//ul[contains(@id,'MenuSectionItemsOverflowButton')]/li/button[@aria-label='{menuItem}']";
                wh.clickOnElement(SeleniumExtensions.getLocator(customizeSelector), wait);
            }

        }

        public void clickOnMainMenuMoreElement(String menuItem, int timeout)
        {
            SeleniumExtensions.WaitForReadyStateToComplete();
            waitHelpers wh = new waitHelpers();
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(timeout));

            try
            {
                string customizeSelector = $"//ul[contains(@data-lp-id,'commandbar')]//li//*[text()='{menuItem}']//ancestor::button";
                wh.clickOnElement(SeleniumExtensions.getLocator(customizeSelector), wait);

                //SeleniumExtensions.WaitForElementAndClick(By.XPath(customizeSelector), 5);
            }
            catch (Exception e)
            {
                moreCommandsJob.ClickUsingJavascript();
                string customizeSelector = $"//ul[contains(@id,'MenuSectionItemsOverflowButton')]/li/button[@aria-label='{menuItem}']";
                wh.clickOnElement(SeleniumExtensions.getLocator(customizeSelector), wait);
                //SeleniumExtensions.WaitForElementAndClick(By.XPath(customizeSelector));
            }

        }

        public void clickOnMainMenuMoreElement_Validateerror(String menuItem)
        {
            waitHelpers wh = new waitHelpers();
            SeleniumExtensions.WaitForReadyStateToComplete();
            WebDriverWait wait5 = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(60));

            try
            {
                By customizeSelector = By.XPath($"//ul[contains(@data-lp-id,'commandbar-Form') and contains(@data-id,'CommandBar')]//li//*[text()='{menuItem}']//ancestor::button | //ul[contains(@data-lp-id,'commandbar-HomePageGrid') and contains(@data-id,'CommandBar')]//li//*[text()='{menuItem}']//ancestor::button");
                By menubarSelector = By.XPath($"//ul[contains(@aria-label,'Commands') and @role='menubar']");
                wh.isElementDisplayed(menubarSelector, 120);
                wh.isElementDisplayed(customizeSelector, 90);
                wh.clickOnElement(customizeSelector);
                By errorMessageBy = By.XPath($"//span[@data-id='voa_proposedbareferencenumber-error-message']");
                bool isErrorDisplayed = wh.isElementDisplayed(errorMessageBy, 5);
                Assert.True(isErrorDisplayed, "Error message dispalyed");
            }
            catch (Exception e)
            {
                Console.Write("Exception details : " + e);
                wh.jsClickOnElement(moreCommandsJobBy);
                By underMoreCommandsSelector = By.XPath($"//ul[contains(@id,'MenuSectionItemsOverflowButton')]/li/button[@aria-label='{menuItem}']");
                wh.isElementDisplayed(underMoreCommandsSelector, 5);
                wh.clickOnElement(underMoreCommandsSelector, wait5);

            }
        }


        public void clickOnMainMenuMoreElement_New(String menuItem)
        {
            waitHelpers wh = new waitHelpers();
            CommonPage commonpage = new CommonPage();
            //appProgressIndicatorContainer
            String loadingSec = $"section[id='appProgressIndicatorView_popupContainer']";
            By loadingSecBy = SeleniumExtensions.getLocator(loadingSec);
            SeleniumExtensions.waitUntillElementInvisible(loadingSecBy);
            commonpage.waitTillappProgressIndicatorDisaddpears_();
            WebDriverWait wait5 = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(60));

            try
            {
                By customizeSelector = By.XPath($"//ul[contains(@data-lp-id,'commandbar-Form') and contains(@data-id,'CommandBar')]//li//*[text()='{menuItem}']//ancestor::button | //ul[contains(@data-lp-id,'commandbar-HomePageGrid') and contains(@data-id,'CommandBar')]//li//*[text()='{menuItem}']//ancestor::button");
                By menubarSelector = By.XPath($"//ul[contains(@aria-label,'Commands') and @role='menubar']");
                wh.isElementDisplayed(menubarSelector, 120);
                wh.isElementDisplayed(customizeSelector, 90);
                wh.clickOnElement(customizeSelector);
            }
            catch (ElementClickInterceptedException e)
            {
                commonpage.waitTillappProgressIndicatorDisaddpears_();
                //Thread.Sleep(8000);
                By customizeSelector = By.XPath($"//ul[contains(@data-lp-id,'commandbar-Form') and contains(@data-id,'CommandBar')]//li//*[text()='{menuItem}']//ancestor::button | //ul[contains(@data-lp-id,'commandbar-HomePageGrid') and contains(@data-id,'CommandBar')]//li//*[text()='{menuItem}']//ancestor::button");
                By menubarSelector = By.XPath($"//ul[contains(@aria-label,'Commands') and @role='menubar']");
                wh.elementToClickble(customizeSelector);
                wh.isElementDisplayed(menubarSelector, 120);
                wh.isElementDisplayed(customizeSelector, 90);
                wh.clickOnElement(customizeSelector);
            }
            catch (ElementNotInteractableException e)
            {
                commonpage.waitTillappProgressIndicatorDisaddpears_();
                By customizeSelector = By.XPath($"//ul[contains(@data-lp-id,'commandbar-Form') and contains(@data-id,'CommandBar')]//li//*[text()='{menuItem}']//ancestor::button | //ul[contains(@data-lp-id,'commandbar-HomePageGrid') and contains(@data-id,'CommandBar')]//li//*[text()='{menuItem}']//ancestor::button");
                By menubarSelector = By.XPath($"//ul[contains(@aria-label,'Commands') and @role='menubar']");
                wh.elementToClickble(customizeSelector);
                wh.isElementDisplayed(menubarSelector, 120);
                wh.isElementDisplayed(customizeSelector, 90);
                wh.clickOnElement(customizeSelector);
            }
            catch (Exception e)
            {
                Console.Write("Exception details : " + e.Message);
                wh.jsClickOnElement(moreCommandsJobBy);
                By underMoreCommandsSelector = By.XPath($"//ul[contains(@id,'MenuSectionItemsOverflowButton')]/li/button[@aria-label='{menuItem}']");
                wh.isElementDisplayed(underMoreCommandsSelector, 5);
                wh.clickOnElement(underMoreCommandsSelector, wait5);
            }
        }

        public void clickOnMainMenuMoreElement_Latest(String menuItem)
        {
            waitHelpers wh = new waitHelpers();
            SeleniumExtensions.WaitForReadyStateToComplete();
            //SeleniumExtensions.WaitForPageLoad();
            WebDriverWait wait5 = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromMilliseconds(10));
            By locator = By.XPath("//ul[@role='menubar' and contains(@aria-label,'Commands')]/li//span[text()]");
            wh.isElementDisplayed(locator, 60);
            bool isEleAvailable = wh.getAllWebElementsText(locator).Contains(menuItem);
            if (isEleAvailable)
            {
                By customizeSelectorBy = By.XPath($"//ul[contains(@data-lp-id,'commandbar-Form') and contains(@data-id,'CommandBar')]//li//*[text()='{menuItem}']//ancestor::button | //ul[contains(@data-lp-id,'commandbar-HomePageGrid') and contains(@data-id,'CommandBar')]//li//*[text()='{menuItem}']//ancestor::button");
                //wh.elementClickableAndDisplayed(customizeSelectorBy);
                wh.isElementDisplayed(customizeSelectorBy, 10);
                wh.jsClickOnElement(customizeSelectorBy);
            }
            else
            {
                //wh.clickOnElement(moreCommandsJobBy);
                //moreCommandsJob.ClickUsingJavascript();
                wh.jsClickOnElement(moreCommandsJobBy);
                By underMoreCommandsSelectorBy = By.XPath($"//ul[contains(@id,'MenuSectionItemsOverflowButton')]/li/button[@aria-label='{menuItem}']");
                //wh.elementClickableAndDisplayed(underMoreCommandsSelectorBy);
                wh.jsClickOnElement(underMoreCommandsSelectorBy, wait5);
            }
        }


        private IWebElement CommandBarListItem(string commandBarListOption)
        {
            string customizeSelector = $"//ul[contains(@data-lp-id,'commandbar')]//button[contains(@aria-label,'Request Action More Commands. {commandBarListOption}')]";
            return Driver.WaitForElement(By.XPath(customizeSelector));
        }

        private IWebElement CommandBarListItemNew(string commandBarListOption)
        {
            string customizeSelector = $"//ul[contains(@data-lp-id,'commandbar')]//button[contains(@aria-label,'{commandBarListOption}')]";
            return Driver.WaitForElement(By.XPath(customizeSelector));
        }

        private IWebElement CommandBarMenuListOption(string listOption)
        {
            string customizeSelector = $"ul li button[aria-label*='{listOption}']";
            return Driver.WaitForElement(By.CssSelector(customizeSelector));
        }

        private IWebElement FindTabByTitleInList(String titleName, string tablistName)
        {
            string cssSelector = $"ul[aria-label = '{tablistName}'] li[title ='{titleName}']";
            return Driver.WaitForElement(By.CssSelector(cssSelector));
        }
        private IWebElement VOANameLink => Driver.WaitForElement(By.CssSelector("[class*='ag-row-position-absolute'] [col-id='voa_name'] a"));

        // DatePicker
        private static IWebElement DatePickerDialog => Driver.WaitForElement(By.CssSelector("[role='dialog'][aria-label='Calendar']"));
        private static By DatePickerDialogByLocator => By.CssSelector("[role='dialog'][aria-label='Calendar']");

        private static IWebElement MonthAndYearEleOnDatePicker => Driver.WaitForElement(By.CssSelector("[role='dialog'][aria-label='Calendar'] [class*='monthAndYear'] *"));

        private static IWebElement MonthAndYearElementOnDatePicker => Driver.WaitForElement(By.CssSelector("[role='dialog'][aria-label='Calendar'] button[aria-label*='Month picker for year'] *"));
        private static IWebElement PreviousEleOnDatePicker => Driver.WaitForElement(By.XPath("//div[@aria-label='Calendar']//button[contains(@aria-label,'switch to year picker')]//following-sibling::div//button[1]"));
        private static IWebElement NextEleOnDatePicker => Driver.WaitForElement(By.CssSelector("[role='dialog'][aria-label='Calendar'] button[title*='Navigate to next'] i"));
        private static IWebElement YearTextOnDatePicker => Driver.WaitForElement(By.CssSelector("[role='dialog'][aria-label='Calendar'] [class*='monthPickerWrapper'] button[aria-label*='Month picker for year'] *"));
        private static IWebElement DayElementForDatePicker(string dayValue)
        {
            string customizeSelector = $"//*[@role='dialog' and @aria-label='Calendar']//tr//td[contains(@class,'dayCell') and not(contains(@class,'dayOutsideNavigatedMonth'))]//button//span[contains(text(),'{dayValue}')]";
            return Driver.WaitForElement(By.XPath(customizeSelector));
        }
        private static IWebElement MonthElementForDatePicker(string MonthValue)
        {
            string customizeSelector = $"[role ='dialog'][aria-label ='Calendar'] button[aria-label ='{MonthValue}']";
            return Driver.WaitForElement(By.CssSelector(customizeSelector));
        }



        private By GetTabs(String tabListName)
        {
            string customizeSelector = $"//ul[@aria-label='{tabListName}']//li";
            return By.XPath(customizeSelector);

        }

        public By GetFirstLookUp(String lookUpValue)
        {

            string customizeSelector = $"//ul[contains(@id,'fieldControl-LookupResultsDropdown_voa')]//li//*[text()='{lookUpValue}']";
            return By.XPath(customizeSelector);

        }

        private IWebElement FindHereditament => Driver.WaitForElement(By.XPath("//button//*[text()='Find Hereditament']"));

        private IWebElement FindHereditamentQuickrootbutton => Driver.WaitForElement(By.XPath("//section[@data-id='quickCreateRoot']//span[text()='Find Hereditament']//ancestor::button"));
        private By FindHereditamentQuickrootbuttonBy => (By.XPath("//section[@data-id='quickCreateRoot']//span[text()='Find Hereditament']//ancestor::button"));

        public IWebElement FindHereditamentOnDialog => Driver.WaitForElement(By.XPath("//*[@role='dialog']//button//*[text()='Find Hereditament']"));

        private IWebElement PostCodeOnDialog => Driver.WaitForElement(By.XPath("//*[contains(@class,'ms-Dialog-main')]//label[text()='Postcode']//following-sibling::*//input"));

        private IWebElement PostCodeOnDialogQuickrootbutton => Driver.WaitForElement(By.XPath("//input[@id='postcodeCriteria']"));
        public  By uprnBY = By.CssSelector("input[id*='TextField']");
        private IWebElement TownCityOnDialog => Driver.WaitForElement(By.XPath("//*[contains(@class,'ms-Dialog-main')]//label[text()='Town/City']//following-sibling::*//input"));

        private IWebElement TownCityOnDialogQuickrootbutton => Driver.WaitForElement(By.XPath("//input[@id='townCityCriteria']"));

        public IWebElement SearchOnDialog => Driver.WaitForElement(By.XPath("//*[contains(@class,'ms-Dialog-main')]//button//*[text()='Search']"));
        public IWebElement SearchBtnOnDialog => Driver.WaitForElement(By.XPath("//*[contains(@class,'ms-Dialog-main')]//button[not(@role='tab')]//*[text()='Search']"));

        private By AddressRow => By.XPath("//*[@data-automationid='DetailsList']//*[@data-automationid='DetailsRow']");

        private IWebElement SelectOnDIalog => Driver.WaitForElement(By.XPath("//*[contains(@class,'ms-Dialog-main')]//button//*[text()='Select']"));
        private IWebElement NextPageOnDialog => Driver.WaitForElement(By.XPath("//*[contains(@class,'ms-Dialog-main')]//button[@title='Next Page']//i[@data-icon-name='Next']"));
        private IWebElement NextPageButtonOnDialog => Driver.WaitForElement(By.XPath("//*[contains(@class,'ms-Dialog-main')]//button[@title='Next Page']"));

        public IWebElement ContinueBtnOnConfirmDialog => Driver.WaitForElement(By.CssSelector("[data-id='confirmdialog'] button[title='Continue']"));
        public By CorrIntegrationMessageStatus => By.XPath("//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row')]//*[@col-id='statuscode']//label");

        public By QuestionnaireList => By.CssSelector("[class*='fyed02w'] div[class*='fk6fouc']");

        private IWebElement OptionsFromRelatedTab(String optionToSelect)
        {
            string customizeSelector = $"[aria-label = '{optionToSelect}']";
            return Driver.WaitForElement(By.CssSelector(customizeSelector));
        }

        private IWebElement NewOptionsFromRelatedTab(String optionToSelect)
        {
            string customizeSelector = $"[role='menuitem'][aria-label = '{optionToSelect}']";
            return Driver.WaitForElement(By.CssSelector(customizeSelector));
        }

        private IWebElement OverFlowCommandOptionsForJob(String optionVal)
        {
            string customizeSelector = $"[data-id ='OverflowFlyout'] button[aria-label*='{optionVal}']";
            return Driver.WaitForElement(By.CssSelector(customizeSelector));
        }

        private IWebElement DialogPopUpHeader => Driver.WaitForElement(By.CssSelector("[data-id='confirmdialog'] [data-id='dialogTitleText']"));
        private IWebElement DialogPopUpMessage => Driver.WaitForElement(By.CssSelector("[data-id='confirmdialog'] [data-id='dialogMessageText']"));

        private IWebElement YesDialogPopUp => Driver.WaitForElements(By.CssSelector("[data-id='confirmdialog'] button[title='Yes']")).LastOrDefault();

        private IWebElement NoDialogPopUp => Driver.WaitForElements(By.CssSelector("[data-id='confirmdialog'] button[aria-label='No']")).LastOrDefault();
        private IWebElement QAQCPopUp => Driver.WaitForElement(By.CssSelector("[data-id='confirmdialog'] [data-id='dialogTitleText']"));
        private IWebElement AllJobsCreatedButton => Driver.WaitForElement(By.CssSelector("[data-id='confirmdialog'] button[title='Yes']"));
        private IWebElement AllJobsCreatedNoButton => Driver.WaitForElement(By.CssSelector("[data-id='confirmdialog'] button[title='No']"));

        protected BasePage()
        {
            Driver = DriverHelper.Driver;
        }

        public void VerifyDuplicateRecords()
        {
            try
            {
                if (IgnoreAndSaveButton.ElementVisisbleUsingExplicitWait(5))
                    //System.Threading.Thread.Sleep(4000);
                    IgnoreAndSaveButton.Click();
            }
            catch (Exception e)
            {
                Console.WriteLine($"the exception is {e.Message}");
            }
        }

        public void ClickSaveBtnOnMainNav()
        {
            SaveLabel.ClickUsingJavascript();
            System.Threading.Thread.Sleep(3000);
        }

        public void FillAndSelectLookUpResult(By selector, string lookUpItem)
        {
            ScrollDiv.ScrollUntilSelectorDisplayedAndSendKeys(selector, lookUpItem, 30);
            LookUpResultFirstValue.ClickUsingJavascript();
        }

        public void FillAndSelectLookUpResult(String fieldName, string lookUpItem)
        {
            ScrollDiv.ScrollUntilSelectorDisplayedAndSendKeys(fieldName, lookUpItem, 30);
            LookUpResultFirstValue.ClickUsingJavascript();
        }

        public void FillAndSelectLookUpResult(String fieldName, String sheetName, String rowID)
        {
            ScrollDiv.ScrollUntilSelectorDisplayedAndSendKeys(fieldName, sheetName, rowID, 30);
            LookUpResultFirstValue.ClickUsingJavascript();
        }

        public void FillAndSelectLookUpResult(String fieldName, Dictionary<string, string> testdata)
        {
            ScrollDiv.ScrollUntilSelectorDisplayedAndSendKeys(fieldName, testdata, 30);
            LookUpResultFirstValue.ClickUsingJavascript();
        }



        public void NavigateToMenuItem(string sectionName, string menuItem)
        {

            SeleniumExtensions.WaitForReadyStateToComplete();
            IWebElement menuItemToSelect = MenuItem(sectionName, menuItem);
            //Assert.IsTrue(SeleniumExtensions.IsElementVisible(menuItemToSelect), "The element is not visible yet");
            menuItemToSelect.ClickUsingJavascript();
            Log.Information($"User navigated to {menuItem} under {sectionName}");

        }
        public void NavigateToMenuItem_new(string sectionName, string menuItem)
        {
            waitHelpers wh = new waitHelpers();
            string customizeSelector = $"ul[aria-label='{sectionName}'] li[aria-label='{menuItem}']";
            IWebElement menuItemToSelect = wh.GetWebElement(By.CssSelector(customizeSelector));
            menuItemToSelect.ClickUsingJavascript();
        }

        public void NavigateToMenuItem_New(string sectionName, string menuItem)
        {
            SeleniumExtensions.WaitForReadyStateToComplete();
            IWebElement menuItemToSelect = MenuItem(sectionName, menuItem);
            Assert.IsTrue(SeleniumExtensions.IsElementVisible(menuItemToSelect), "The element is not visible yet");
            SeleniumExtensions.WaitForElementAndClick(menuItemToSelect);
            Log.Information($"User navigated to {menuItem} under {sectionName}");

        }

        public void ClickCommandBarOption(string commandBarOption)
        {
            waitHelpers wh = new waitHelpers();
            IWebElement commandOptionToClick = CommandBarItem(commandBarOption);
            //  commandOptionToClick.ElementVisisbleUsingExplicitWait(10);
            Assert.IsTrue(wh.elementDisplayed(CommandBarItemUsingBy(commandBarOption)), "The element does not exists");
            commandOptionToClick.ClickUsingJavascript();
            //Thread.Sleep(2000);
            Log.Information($"User Clicks {commandBarOption} on the Commandbar");
            //if (commandBarOption=="Save") 
            //{
            //    ValidateSaveInProgressDialog();

            //}

        }

        public void ClickCommandBarOptionOnDialog(string commandBarOption)
        {
            //  Thread.Sleep(3000);
            waitHelpers wh = new waitHelpers();

            By commandOptionToClick = CommandBarItemOnDialog(commandBarOption);
            IWebElement CommandOptionToClick = wh.WaitForElementToBeReady(commandOptionToClick, 10);
            //   wh.clickOnElement(commandOptionToClick);
            CommandOptionToClick.ClickUsingJavascript();
            Log.Information($"User Clicks {commandBarOption} on the Commandbar");

        }

        public void ClickOnTab(string tabName, string tabListName)
        {
            IWebElement tabToNavigate = FindTabByTitleInList(tabName, tabListName);
            tabToNavigate.ClickUsingJavascript();
            Log.Information($"User navigates to {tabName} in {tabListName}");

            // Thread.Sleep(10000);
        }


        public void ValidateAndClickOnTab(string tabName, string tabListName)
        {
            waitHelpers wh = new waitHelpers();
            int retries = 0;
            By Tablist = GetTabs(tabListName);
            //  Assert.IsTrue(DriverHelper.Driver.FindElement(Tablist).WaitUntilElementAttached(10), "The Tablist is not yet attached to the Job Form");
            //  Assert.IsTrue(DriverHelper.Driver.FindElement(Tablist).ElementVisisbleUsingExplicitWait(10),"The Tablist is not yet visible");
            //Thread.Sleep(2000);
            wh.WaitForElementToBeReady(Tablist, 10);
            bool tabPresnt = SeleniumExtensions.ValidateTextInList(Tablist, "title", tabName);
            while (tabPresnt == false)
            {
                ClickCommandBarOption("Refresh");
                Tablist = GetTabs(tabListName);
                tabPresnt = SeleniumExtensions.ValidateTextInList(Tablist, "title", tabName);
                if (tabPresnt == true)
                {
                    Log.Information("The tab " + tabName + " was found in the list " + tabListName);
                    break;
                }
                retries++;
                if (retries == 10)
                {
                    Log.Information("The tab " + tabName + " is not present in the list " + tabListName);
                    Assert.Fail("The tab " + tabName + " is not present in the list " + tabListName);
                }
            }
            //  Assert.IsTrue(tabPresnt,"The tab " + tabName+ " is not present in the list "+ tabListName);

            IWebElement tabToNavigate = FindTabByTitleInList(tabName, tabListName);
            Assert.IsTrue(tabToNavigate.WaitUntilElementAttached(5));
            tabToNavigate.ClickUsingJavascript();
            Log.Information($"User has validated and clicked on {tabName} in {tabListName}");


        }

        public void waitTillSavingDisaddpears(String labelName, String roleType)
        {
            String locator = $"//*[@role='{roleType}' and text()='{labelName}'] | //*[@role='{roleType}'] | //*[text()='{labelName}']";
            By eleBy = SeleniumExtensions.getLocator(locator);
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(180));
            wait.PollingInterval = TimeSpan.FromMilliseconds(200);
            wait.Until(SeleniumExtensions.InvisibilityOfElementLocatedBy(eleBy));
        }
        private IWebElement GetPVTTabs(String PVTTabName)
        {
            string customizeSelector = $"//div[@aria-label='PVT']//*[@role='tablist']//button[not(contains(@class,'overflowMenuButton')) and @name='{PVTTabName}']";
            return DriverHelper.Driver.FindElement(By.XPath(customizeSelector));

        }


        public void ValidateAndClickOnPVTTab(string tabName)
        {
            IWebElement PVTTabName = GetPVTTabs(tabName);
            Assert.IsTrue(PVTTabName.WaitUntilElementAttached(15));
            Assert.IsTrue(PVTTabName.ElementVisisbleUsingExplicitWait(10));
            Thread.Sleep(6000);
            PVTTabName.ClickUsingJavascript();
            Thread.Sleep(2000);
        }

        public void ChickOnChevron()
        {   
            waitHelpers wh = new waitHelpers();
            if (wh.isElementDisplayed(ChevronRightMed, 180))
            {
                Console.WriteLine("ChevronRightMed is visible");
                ChevronRightMed.ClickUsingJavascript();
                Console.WriteLine("ChevronRightMed is clicked");

            }
        }

        public void ProgressJourney(string buttonName, string jorneyHeader)
        {
            //Thread.Sleep(12000);
            waitHelpers wh = new waitHelpers();
            By jorneyHeaderUsingBy = JourneyHeaderUsingBySelector(jorneyHeader);
            //  IWebElement journeyHeader = JourneyHeader(jorneyHeader);

            IWebElement headerElement = wh.WaitForElementToBeReady(jorneyHeaderUsingBy, 30);
            headerElement.ClickUntilNoClickInterruptable();
            IWebElement journeyButton = JourneyStage(buttonName);
            Assert.IsTrue(journeyButton.ElementVisisbleUsingExplicitWait(10), "The journey button is not visible");
            SeleniumExtensions.WaitForElementAndClick(journeyButton);

            Log.Information($"User clicks on {buttonName} under {jorneyHeader}");

            ClickOnCloseForJourney(jorneyHeader);


        }

        public void ProgressJourney_withOutJSclick(string buttonName, string jorneyHeader)
        {
            waitHelpers wh = new waitHelpers();
            By jorneyHeaderUsingBy = JourneyHeaderUsingBySelector(jorneyHeader);
            wh.clickOnWebElement(jorneyHeaderUsingBy);
            //jorneyHeaderUsingBy.WaitForElementToBeClickable(15);           
            string journeyButtonstr = $"//*[contains(@id,'MscrmControls.Containers.ProcessStageControl-processHeaderStageFlyoutContainer')]//button[@aria-label='{buttonName}']";
            By journeyButtonBy = SeleniumExtensions.getLocator(journeyButtonstr);
            //IWebElement journeyButton = wh.GetWebElement(journeyButtonBy);
            //Assert.IsTrue(journeyButton.ElementVisisbleUsingExplicitWait(10), "The journey button is not visible");
            //journeyButton.ClickUsingJavascript();
            //SeleniumExtensions.WaitForElementAndClick(journeyButton);
            wh.isElementDisplayed(journeyButtonBy, 60);
            wh.clickOnWebElement(journeyButtonBy);
            Log.Information($"User clicks on {buttonName} under {jorneyHeader}");
            //Thread.Sleep(6000);
            //ClickOnCloseForJourney_withOutJsScript();
        }

        public void waitUntillStageIsSelected(String stageName)
        {
            By stageSelected = SeleniumExtensions.getLocator($"//div[contains(@id,'MscrmControls.Containers.ProcessBreadCrumb-stageIndicatorContainer') and contains(@title,'{stageName}')]/div/div[contains(@id,'MscrmControls.Containers.ProcessBreadCrumb-stageIndicatorInnerHolder')]");
            //By releaseAndPublishStageloc = SeleniumExtensions.getLocator($"//div[contains(@id,'MscrmControls.Containers.ProcessBreadCrumb-stageIndicatorContainer') and contains(@title,'Release And Publish')]/div/*[contains(@id,'MscrmControls.Containers.ProcessBreadCrumb-processHeaderStageIndicator')]");
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(120));
            wait.PollingInterval = TimeSpan.FromMilliseconds(10);
            //if (stageName.Equals("Release And Publish"))
            //{
            //    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(releaseAndPublishStageloc));
            //}
            //else {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(stageSelected));
            //}
        }

        public void ClickOnCloseForJourney(string jorneyHeader)
        {
            waitHelpers wh = new waitHelpers();

            try
            {
                if (wh.WaitTillElementDisplayed(JourneyStageUsingBy("Close"), 5))
                {
                    IWebElement journeyButton = JourneyStage("Close");
                    string journeyName = journeyButton.FindElement(By.XPath("//preceding-sibling::button")).GetAttribute("aria-label");
                    if (!journeyName.Contains(jorneyHeader))
                    {
                        journeyButton.ClickUsingJavascript();
                        Log.Information($"User closes the BPF flow pop-up for {jorneyHeader}");

                    }

                }
            }
            catch (Exception e)
            {
                Log.Information($"The {jorneyHeader} was not displayed");
                Console.WriteLine("The close button was not displayed", e.Message);
            }
            //if (journeyButton.IsElementDisplayed(5))
            //{
            //    string journeyName = journeyButton.FindElement(By.XPath("//preceding-sibling::button")).GetAttribute("aria-label");
            //    if (!journeyName.Contains(jorneyHeader))
            //    {
            //        journeyButton.ClickUsingJavascript();
            //        Log.Information($"User closes the BPF flow pop-up for {jorneyHeader}");

            //    }
            //}
        }

        public void ClickOnCloseForJourney_withOutJsScript()
        {
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(5));
            By closeSelector = SeleniumExtensions.getLocator($"//*[contains(@id,'MscrmControls.Containers.ProcessStageControl-processHeaderStageFlyoutContainer')]//button[@aria-label='Close']");
            if (SeleniumExtensions.isElementDisplayed(closeSelector))
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(closeSelector)).Click();
            }
        }


        public void ClickOnCloseForJourney_withOutJSclick(string jorneyHeader)
        {

            IWebElement journeyButton = JourneyStage("Close");
            if (journeyButton.IsElementDisplayed(5))
            {
                string journeyName = journeyButton.FindElement(By.XPath("//preceding-sibling::button")).GetAttribute("aria-label");
                if (!journeyName.Contains(jorneyHeader))
                {
                    SeleniumExtensions.WaitForElementAndClick(journeyButton);
                    Log.Information($"User closes the BPF flow pop-up for {jorneyHeader}");

                }
            }
        }

        public void ValidateActiveBPF(string jorneyHeader, string expStatus)
        {
            //Thread.Sleep(12000);
            By jorneyHeaderUsingBy = JourneyHeaderUsingBySelector(jorneyHeader);
            jorneyHeaderUsingBy.WaitForElementToBeClickable(15);
            IWebElement journeyButton = JourneyStage("Close");
            if (journeyButton.IsElementDisplayed(5))
            {
                string actualStatus = journeyButton.FindElement(By.XPath("//preceding-sibling::button")).GetAttribute("aria-label");
                if (!actualStatus.Contains(expStatus))
                {
                    journeyButton.ClickUsingJavascript();
                }
            }
        }

        public void CloseBPFPopUp(string jorneyHeader)
        {
            IWebElement journeyButton = JourneyStage("Close");
            string journeyName = journeyButton.FindElement(By.XPath("//preceding-sibling::button[contains(@id,'stageDockModeButton')]")).GetAttribute("aria-label");
            if (journeyName.Contains(jorneyHeader))
            {
                journeyButton.ClickUsingJavascript();
                Log.Information($"User closes the BPF flow pop-up for {jorneyHeader}");

            }

        }


        public void ValidateJourneyStage(string stageName, string jorneyHeader)
        {
            By jorneyHeaderUsingBy = JourneyHeaderUsingBySelector(jorneyHeader);
            jorneyHeaderUsingBy.WaitForElementToBeClickable(15);

            //   IWebElement journeyHeader = JourneyHeader(jorneyHeader);
            //  journeyHeader.ClickUntilNoClickInterruptable();
            Assert.IsTrue(SeleniumExtensions.IsElementVisible(JourneyDialogStageButton));
            string stageText = JourneyDialogStageButton.GetAttribute("title");
            if (stageText == stageName)
            {
                CloseBPFPopUp(jorneyHeader);
            }

        }

        public bool ValidateStatus(string statusText)
        {
            var count = 0;
            bool Statusflag = false;
            Assert.IsTrue(Statustext.ElementVisisbleUsingExplicitWait(10), "The element is not visible yet");
            var getStatus = Statustext.Text;
            while (getStatus != statusText)
            {
                ClickCommandBarOption("Refresh");
                Assert.IsTrue(Statustext.ElementVisisbleUsingExplicitWait(10), "The element is not visible yet");
                if (Statustext.Text == statusText)
                {
                    Log.Information($"User has validated the status {getStatus}");

                    //  Assert.AreEqual(statusText, getStatus);
                    //  Console.WriteLine("The status " + getStatus + "has been validated");
                    Statusflag = true;
                    break;

                }
                else
                {
                    count++;
                }

                if (count == 20)
                {
                    Assert.Fail("The status " + getStatus + " does not match with the expected " + statusText);
                    Log.Error($"The expected status {statusText} does not matches with the actual {getStatus}");

                    break;
                }
            }
            if (Statustext.Text == statusText)
            {
                Statusflag = true;
                return Statusflag;
            }
            else
            {
                return Statusflag;
            }

        }

        public void ClickOnButtonByTitle(string buttonTitle)
        {
            IWebElement buttonElement = ButtonByTitle(buttonTitle);

            buttonElement.ClickUsingJavascript();
            Log.Information($"User clicked on button title {buttonTitle}");


        }

        public string GetHeaderTitle()
        {
            string headerTitleText = headerTitle.GetAttribute("title");
            Log.Information($"User fetches the header title {headerTitleText}");
            return headerTitleText;

        }

        public IWebElement GetFilterColumn(String columnName)
        {
            waitHelpers wh = new waitHelpers();
            string xpathSelector = $"//*[@data-testid='columnHeader']//label/div[text()='{columnName}']";
            bool isEleDisplayed = false;
            int counter = 0;
            do
            {
                isEleDisplayed = wh.isElementDisplayed(By.XPath(xpathSelector), 30);
                if (isEleDisplayed) break;
                counter = counter + 1;
                if (counter == 5)
                    throw new Exception($"Still element {xpathSelector} is not able to find");

            } while (!isEleDisplayed);
            return wh.GetWebElement(By.XPath(xpathSelector));
        }
        public IWebElement GetPendingValue(String Value)
        {
            string xpathSelector = $"//*[normalize-space(text())='{Value}']";
            return Driver.WaitForElement(By.XPath(xpathSelector));
        }
        public void FilterColumnWithValue(string columnName, string filterValue)
        {
            Thread.Sleep(5000);
            IWebElement colmnNameToFilter = GetFilterColumn(columnName);
            Assert.IsTrue(SeleniumExtensions.IsElementVisible(colmnNameToFilter));
            colmnNameToFilter.ClickUsingJavascript();
            Assert.IsTrue(SeleniumExtensions.IsElementVisible(FilterBy));
            FilterBy.ClickUsingJavascript();
            Assert.IsTrue(SeleniumExtensions.IsElementVisible(FilterByValue));
            FilterByValue.ClearAndSendkeys(filterValue);
            //ApplyButton.ClickUsingJavascript();
            // SeleniumExtensions.SelectRowCellValue(ViewSelectorRows, columnName, filterValue);

        }

        public void SelectFilteredRowValue(string columnName, string filterValue)
        {
            SeleniumExtensions.SelectRowCellValue(ViewSelectorRows, columnName, filterValue);
            Thread.Sleep(3000);

        }



        public void ClickMenuListOptionFromCommandBar(string menuListOption, string commndBarOption)
        {
            IWebElement commandOption = CommandBarListItem(commndBarOption);
            Assert.IsTrue(commandOption.ElementVisisbleUsingExplicitWait(10));
            commandOption.ClickUsingJavascript();
            IWebElement menuListOptionToSelect = CommandBarMenuListOption(menuListOption);
            // Assert.IsTrue(menuListOptionToSelect.ElementVisisbleUsingExplicitWait(5));
            menuListOptionToSelect.ClickUsingJavascript();
            Log.Information($"User clicks on {commndBarOption} under {menuListOption} list");



        }
        public void ClickMenuListOptionFromCommandBarNew(string menuListOption, string commndBarOption)
        {
            IWebElement commandOption = CommandBarListItemNew(commndBarOption);
            Assert.IsTrue(commandOption.ElementVisisbleUsingExplicitWait(10));
            commandOption.ClickUsingJavascript();
            IWebElement menuListOptionToSelect = CommandBarMenuListOption(menuListOption);
            // Assert.IsTrue(menuListOptionToSelect.ElementVisisbleUsingExplicitWait(5));
            menuListOptionToSelect.ClickUsingJavascript();
            Log.Information($"User clicks on {commndBarOption} under {menuListOption} list");



        }

        public void ChecKUnsavedChanges(string menuListOption, string commndBarOption)
        {

            String PopUpValue = UnSavedchanges.GetAttribute("title").Trim();
            if (PopUpValue == "Unsaved Changes")
            {

                OkButtonpopupDialog.ClickUsingJavascript();
                ClickCommandBarOption("Save");
                IWebElement commandOption = CommandBarListItem(commndBarOption);
                Assert.IsTrue(commandOption.ElementVisisbleUsingExplicitWait(10));
                commandOption.ClickUsingJavascript();
                IWebElement menuListOptionToSelect = CommandBarMenuListOption(menuListOption);
                // Assert.IsTrue(menuListOptionToSelect.ElementVisisbleUsingExplicitWait(5));
                menuListOptionToSelect.ClickUsingJavascript();
                Log.Information($"User clicks on {commndBarOption} under {menuListOption} list");
            }
            else
            {

                Console.WriteLine("No changes Required");
            }

        }
        public void ClickMenuListOptionFromCommandBarWithOuJS(string menuListOption, string commndBarOption)
        {
            waitHelpers wh = new waitHelpers();
            By commndBarOptionSelector = null;
            if (commndBarOption.Equals("Request Action"))
            {
                commndBarOptionSelector = By.XPath($"//ul[contains(@data-lp-id,'commandbar')]//li[contains(@title,'{commndBarOption}')]//button[contains(@aria-label,'Request Action More Commands')]");
            }
            else
            {
                commndBarOptionSelector = By.XPath($"//ul[contains(@data-lp-id,'commandbar')]//li[contains(@title,'{commndBarOption}')]//button[contains(@aria-label,'{commndBarOption}')]");
            }
            wh.clickOnElement(commndBarOptionSelector);
            By menuListOptionToSelector = By.CssSelector($"ul li button[aria-label*='{menuListOption}']");
            wh.clickOnElement(menuListOptionToSelector);
            Log.Information($"User clicks on {commndBarOption} under {menuListOption} list");



        }
        // MoreTabsOptionJobForm

        public void ClickOptionFromMoreTabs(string optionName)
        {
            MoreTabs.WaitUntilElementAttached(5);
            MoreTabs.ClickUsingJavascript();
            IWebElement OptionToSelect = MoreTabsOptionJobForm(optionName);
            Assert.IsTrue(OptionToSelect.ElementVisisbleUsingExplicitWait(5), "The option is not available in the list");
            OptionToSelect.ClickUsingJavascript();
            Log.Information($"User clicks on {optionName} under more tabs options");


        }
        public void ClickOptionFromMoreTabsOnReviewForm(string optionName)
        {
            MoreTabsOnReviewForm.WaitUntilElementAttached(5);
            MoreTabsOnReviewForm.ClickUsingJavascript();
            IWebElement OptionToSelect = MoreTabsOptionJobForm(optionName);
            Assert.IsTrue(OptionToSelect.ElementVisisbleUsingExplicitWait(5), "The option is not available in the list");
            OptionToSelect.ClickUsingJavascript();
            Log.Information($"User clicks on {optionName} under more tabs options");


        }

        public void ValidateSaveInProgressDialog()
        {
            if (SaveInProgressError.ElementVisisbleUsingExplicitWait(2))
            {
                SaveInProgressOKBtn.ClickUsingJavascript();
                ClickCommandBarOption("Save");
            }
        }


        public void FillAndSelectLookUpResultWhenDataNotEntered(String fieldName, Dictionary<string, string> testdata)
        {
            waitHelpers wh = new waitHelpers();

            By selectedValue = By.XPath($"//ul[@title='{fieldName}' and contains(@id,'SelectedRecordList')]");
            //$"//*[contains(@title,'{fieldName}')]//div[@role='link']";
            //By locator = SeleniumExtensions.getLocator(locatorStr);
            By inputselector = SeleniumExtensions.getLocator($"(//input[@aria-label='{fieldName}, Lookup'])[1]");
            //bool eleDisplayed = SeleniumExtensions.isElementDisplayed(selector);
            bool recordSelected = wh.isElementDisplayed(selectedValue, 1);
            if (recordSelected)
            {
                //SeleniumExtensions.ScrollUntilSelectorDisplayedAndSendKeys_New(fieldName, testdata, 30);
                //LookUpResultFirstValue.ClickUsingJavascript();
            }
            else
            {
                try
                {
                    SeleniumExtensions.ScrollUntilSelectorDisplayedAndSendKeys_New(fieldName, testdata, 30);
                }
                catch (Exception e)
                {
                    recordSelected = wh.isElementDisplayed(selectedValue, 1);
                    if (!recordSelected)
                        SeleniumExtensions.ScrollUntilSelectorDisplayedAndSendKeys_New(fieldName, testdata, 30);
                }

            }
        }

        public void FillAndSelectLookUpResultWhenDataNotEntered(String fieldName, Dictionary<string, string> testdata, int timeInSec)
        {
            waitHelpers wh = new waitHelpers();

            By selectedValue = By.XPath($"//ul[@title='{fieldName}' and contains(@id,'SelectedRecordList')]");
            By inputselector = SeleniumExtensions.getLocator($"(//input[@aria-label='{fieldName}, Lookup'])[1]");
            //bool recordSelected = wh.GetWebElement(selectedValue, 1);
            bool recordSelected = wh.isElementDisplayed(selectedValue, 1);

            if (recordSelected)
            {
            }
            else
            {
                try
                {
                    SeleniumExtensions.ScrollUntilSelectorDisplayedAndSendKeys_New(fieldName, testdata, timeInSec);
                }
                catch (Exception e)
                {
                    //recordSelected = wh.GetWebElement(selectedValue, 1);
                    recordSelected = wh.isElementDisplayed(selectedValue, 5);
                    if (!recordSelected)
                        SeleniumExtensions.ScrollUntilSelectorDisplayedAndSendKeys_New(fieldName, testdata, timeInSec);
                }

            }
        }

        public void FillAndSelectLookUpResultWhenDataNotEntered(String fieldName, string fieldValue, int timeInSec)
        {
            waitHelpers wh = new waitHelpers();

            By selectedValue = By.XPath($"//ul[@title='{fieldName}' and contains(@id,'SelectedRecordList')]");
            //$"//*[contains(@title,'{fieldName}')]//div[@role='link']";
            //By locator = SeleniumExtensions.getLocator(locatorStr);
            By inputselector = SeleniumExtensions.getLocator($"(//input[@aria-label='{fieldName}, Lookup'])[1]");
            //bool eleDisplayed = SeleniumExtensions.isElementDisplayed(selector);
            bool recordSelected = wh.isElementDisplayed(selectedValue, 1);
            if (recordSelected)
            {
                //SeleniumExtensions.ScrollUntilSelectorDisplayedAndSendKeys_New(fieldName, testdata, 30);
                //LookUpResultFirstValue.ClickUsingJavascript();
            }
            else
            {
                try
                {
                    SeleniumExtensions.ScrollUntilSelectorDisplayedAndSendKeys_withValue(fieldName, fieldValue, timeInSec);
                }
                catch (Exception e)
                {
                    recordSelected = wh.isElementDisplayed(selectedValue, 1);
                    if (!recordSelected)
                        SeleniumExtensions.ScrollUntilSelectorDisplayedAndSendKeys_withValue(fieldName, fieldValue, timeInSec);
                }

            }
        }

        public void FillAndSelectLookUpResultWhenDataNotEntered(String fieldName, FeatureContext fc, int timeInSec)
        {
            waitHelpers wh = new waitHelpers();

            By selectedValue = By.XPath($"//ul[@title='{fieldName}' and contains(@id,'SelectedRecordList')]");
            By inputselector = SeleniumExtensions.getLocator($"(//input[@aria-label='{fieldName}, Lookup'])[1]");
            bool recordSelected = wh.GetWebElement(selectedValue, 1);
            if (recordSelected)
            {
            }
            else
            {
                try
                {
                    SeleniumExtensions.ScrollUntilSelectorDisplayedAndSendKeys_New(fieldName, fc, timeInSec);
                }
                catch (Exception e)
                {
                    recordSelected = wh.GetWebElement(selectedValue, 1);
                    if (!recordSelected)
                        SeleniumExtensions.ScrollUntilSelectorDisplayedAndSendKeys_New(fieldName, fc, timeInSec);
                }

            }
        }

        public void FillAndSelectLookUpFirstResultWhenDataNotEntered(String fieldName, Dictionary<string, string> testdata)
        {
            waitHelpers wh = new waitHelpers();
            By inputselector = SeleniumExtensions.getLocator($"(//input[@aria-label='{fieldName}, Lookup'])[1]");
            bool eleDisplayed = wh.isElementDisplayed(inputselector, 30);
            //SeleniumExtensions.isElementDisplayed(inputselector);
            if (eleDisplayed)
            {

                SeleniumExtensions.ScrollUntilSelectorDisplayedAndSelectFirst_New(fieldName, 30);
                //LookUpResultFirstValue.ClickUsingJavascript();
            }
        }

        public void enterDataWhenDataNotEntered(String fieldName, Dictionary<string, string> testData)
        {
            waitHelpers wh = new waitHelpers();
            String locatorStr = $"//*[contains(@aria-label,'{fieldName}') and @title='Select to enter data']";
            By locator = SeleniumExtensions.getLocator(locatorStr);
            bool eleDisplayed = SeleniumExtensions.isElementDisplayed(locator);
            if (eleDisplayed)
            {
                String locatorGroupStr = $"//ul[contains(@title,'Group')]//div[@role='link']";
                By groupLocator = SeleniumExtensions.getLocator(locatorGroupStr);
                String groupTxt = wh.getElementText(groupLocator);
                //DriverHelper.Driver.FindElement(groupLocator).Text.Trim();
                if ((groupTxt.Contains("Flats") || groupTxt.Contains("flats") || groupTxt.Contains("Maisonettes") || groupTxt.Contains("Student Cluster")) && fieldName.Equals("Floors"))
                {
                }
                else
                {
                    SeleniumExtensions.sendKeysFieldValue(fieldName, testData);
                }

            }
        }

        public static void SelectDateFromDatePicker(string dayVal, string monthVal, string yearVal)
        {
            Assert.IsTrue(DatePickerDialog.ElementVisisbleUsingExplicitWait(5), "The calender dialog is not open");
            MonthAndYearEleOnDatePicker.ClickUsingJavascript();

            string currYearDisplayed = YearTextOnDatePicker.Text;
            while (currYearDisplayed != yearVal)
            {
                PreviousEleOnDatePicker.ClickUsingJavascript();
                currYearDisplayed = YearTextOnDatePicker.Text;
                if (currYearDisplayed == yearVal)
                {
                    break;
                }
            }
            IWebElement monthToSelect = MonthElementForDatePicker(monthVal);
            monthToSelect.ClickUsingJavascript();

            IWebElement dayToSelect = DayElementForDatePicker(dayVal);
            dayToSelect.ClickUsingJavascript();


        }

        public static void SelectDateFromDialogDatePicker(string dayVal, string monthVal, string yearVal)
        {
            Assert.IsTrue(DatePickerDialog.ElementVisisbleUsingExplicitWait(5), "The calender dialog is not open");
            //MonthAndYearEleOnDatePicker.ClickUsingJavascript();
            waitHelpers wh = new waitHelpers();
            By navYear = By.CssSelector("[role='dialog'][aria-label='Calendar'] button[aria-label*='Month picker for year'] *");
            By nxtYr = By.CssSelector("[class*='fui-CalendarPicker__navigationButtonsContainer'] button[title *= 'Navigate to next']");
            By PrvYr = By.CssSelector("[class*='fui-CalendarPicker__navigationButtonsContainer'] button[title *= 'Navigate to previous']");

            //[role= 'dialog'][aria-label = 'Calendar'] button[class*='CalendarPicker__itemButton ']
            string currYearDisplayed = wh.getElementText(navYear);
            Console.Write($"currYearDisplayed : " + currYearDisplayed);
            Console.Write($"yearVal : " + yearVal);
            int currYearDisplay = Int32.Parse(currYearDisplayed);
            int yearValDisplay = Int32.Parse(yearVal);


            while (currYearDisplayed != yearVal)
            {
                if (currYearDisplay<yearValDisplay) {
                    wh.clickOnElement(nxtYr);                   
                }
                else
                {
                    wh.clickOnElement(PrvYr);
                }
                Thread.Sleep(900);
                currYearDisplayed = wh.getElementText(navYear);
                if (currYearDisplayed == yearVal)
                {
                    break;
                }

            }
            IWebElement monthToSelect = MonthElementForDatePicker(monthVal);
            monthToSelect.ClickUsingJavascript();

            IWebElement dayToSelect = DayElementForDatePicker(dayVal);
            dayToSelect.ClickUsingJavascript();
        }

        public static void SelectDateFromPresentaionDatePicker(string dayVal, string monthVal, string yearVal)
        {
            Assert.IsTrue(DatePickerDialog.ElementVisisbleUsingExplicitWait(5), "The calender dialog is not open");
            //MonthAndYearEleOnDatePicker.ClickUsingJavascript();
            waitHelpers wh = new waitHelpers();
            By navYear = By.CssSelector("[aria-label='Calendar'] button[aria-label*='Year picker'] *");
            By nxtYr = By.CssSelector("button[title *= 'Navigate to next']");
            By PrvYr = By.CssSelector("button[title *= 'Navigate to previous']");

            //[role= 'dialog'][aria-label = 'Calendar'] button[class*='CalendarPicker__itemButton ']
            string currYearDisplayed = wh.getElementText(navYear).Split(' ')[1];
            Console.Write($"currYearDisplayed : " + currYearDisplayed);
            Console.Write($"yearVal : " + yearVal);

            while (currYearDisplayed != yearVal)
            {
                wh.clickOnElement(PrvYr);
                Thread.Sleep(900);
                currYearDisplayed = wh.getElementText(navYear).Split(' ')[1];
                if (currYearDisplayed == yearVal)
                {
                    break;
                }
            }
            By calenYear = By.CssSelector("[aria-label='Calendar'] button[aria-label*='Year picker']");
            wh.clickOnElement(calenYear);

            IWebElement monthToSelect = MonthElementForDatePicker(monthVal);
            monthToSelect.ClickUsingJavascript();

            IWebElement dayToSelect = DayElementForDatePicker(dayVal);
            dayToSelect.ClickUsingJavascript();
        }

        public static void SelectDateFromDialogDatePicker_fuiCalender(string dayVal, string monthVal, string yearVal)
        {
            Assert.IsTrue(DatePickerDialog.ElementVisisbleUsingExplicitWait(5), "The calender dialog is not open");
            //MonthAndYearEleOnDatePicker.ClickUsingJavascript();
            waitHelpers wh = new waitHelpers();
            By navYear = By.CssSelector("[role= 'dialog'][aria-label = 'Calendar'] button[class*='fui-CalendarPicker__currentItemButton'] *");
            By nxtYr = By.CssSelector("[class*='fui-CalendarPicker__navigationButtonsContainer'] button[title *= 'Go to next']");
            By PrvYr = By.CssSelector("[class*='fui-CalendarPicker__navigationButtonsContainer'] button[title *= 'Go to previous']");

            //
            string currYearDisplayed = wh.getElementText(navYear);
            while (currYearDisplayed != yearVal)
            {
                wh.clickOnElement(PrvYr);
                currYearDisplayed = wh.getElementText(navYear);
                if (currYearDisplayed == yearVal)
                {
                    break;
                }
            }
            IWebElement monthToSelect = MonthElementForDatePicker(monthVal);
            monthToSelect.ClickUsingJavascript();

            IWebElement dayToSelect = DayElementForDatePicker(dayVal);
            dayToSelect.ClickUsingJavascript();
        }




        public static void SelectDateFromDialogDatePicker_new(string dayVal, string monthVal, string yearVal)
        {
            Assert.IsTrue(DatePickerDialog.ElementVisisbleUsingExplicitWait(5), "The calender dialog is not open");
            waitHelpers wh = new waitHelpers();
            By navYear = By.CssSelector("div[class*='monthPickerWrapper'] button[class*='currentItemButton']");
            By nxtYr = By.CssSelector("button[title*='Go to next year']");
            By PrvYr = By.CssSelector("button[title*='Go to previous year']");

            string currYearDisplayed = wh.getElementText(navYear);
            while (currYearDisplayed != yearVal)
            {
                wh.clickOnElement(PrvYr);
                currYearDisplayed = wh.getElementText(navYear);
                if (currYearDisplayed == yearVal)
                {
                    break;
                }
            }
            IWebElement monthToSelect = MonthElementForDatePicker(monthVal);
            monthToSelect.ClickUsingJavascript();

            IWebElement dayToSelect = DayElementForDatePicker(dayVal);
            dayToSelect.ClickUsingJavascript();
        }

        public static void SelectDateFromDatePickernew(string dayVal, string monthVal, string yearVal)
        {
            Assert.IsTrue(DatePickerDialog.ElementVisisbleUsingExplicitWait(5), "The calender dialog is not open");
            MonthAndYearEleOnDatePicker.ClickUsingJavascript();

            string currYearDisplayed = YearTextOnDatePicker.Text;
            while (currYearDisplayed != yearVal)
            {
                PreviousEleOnDatePicker.ClickUsingJavascript();
                currYearDisplayed = YearTextOnDatePicker.Text;
                if (currYearDisplayed == yearVal)
                {
                    break;
                }
            }
            IWebElement monthToSelect = MonthElementForDatePicker(monthVal);
            monthToSelect.ClickUsingJavascript();

            IWebElement dayToSelect = DayElementForDatePicker(dayVal);
            dayToSelect.ClickUsingJavascript();


        }

        public void ValidateNameLinkAndClick()
        {
            Assert.IsTrue(VOANameLink.ElementVisisbleUsingExplicitWait(10), "The name link is not visible");
            VOANameLink.ClickUsingJavascript();

        }



        public void ValidateLookUpValueandSelectNewValue(string valuetoselect, By searchLocator)
        {
            By RTFirstValue = GetFirstLookUp(valuetoselect);
            DriverHelper.Driver.WaitForElement(searchLocator).ClickUsingJavascript();
            RTFirstValue.IsElementVisibleUsingByLocator(3);
            // Thread.Sleep(2000);
            DriverHelper.Driver.WaitForElement(RTFirstValue).ClickUsingJavascript();
        }

        public By GetFirstLookUpValue(String lookUpValue)
        {
            string customizeSelector = $"li[aria-label*='{lookUpValue}']";
            return By.CssSelector(customizeSelector);

        }

        public void ValidateColumnValuesInRows(string expBA, string expBARef, string effectiveFrom, string effectiveTo, string expStatus)
        {
            bool ValidationFlag = false;
            string expEffectiveDate = null;
            string expEffectiveTo = null;
            if (effectiveFrom != "")
            {
                string[] dateValue = effectiveFrom.Split(' ');
                string CuurentDateFormat = SeleniumExtensions.GetDateFormat(dateValue[0]);
                DateTime dateToEnter = DateTime.ParseExact(dateValue[0], CuurentDateFormat, CultureInfo.InvariantCulture);
                expEffectiveDate = dateToEnter.ToString("M/d/yyyy");
                Console.WriteLine(expEffectiveDate);
            }
            if (effectiveTo != "")
            {
                string[] dateValue = effectiveTo.Split(' ');
                string CuurentDateFormat = SeleniumExtensions.GetDateFormat(dateValue[0]);
                DateTime dateToEnter = DateTime.ParseExact(dateValue[0], CuurentDateFormat, CultureInfo.InvariantCulture);
                expEffectiveTo = dateToEnter.ToString("M/d/yyyy");
                Console.WriteLine(expEffectiveTo);
            }

            IList<IWebElement> RowsElement = Driver.FindElements(RowsOnTab);
            if (RowsElement.Count == 0)
            {
                Assert.Fail("There were no records found on the BA Links Tab");
                Log.Error("There were no records found on the BA Links Tab");

            }

            foreach (var row in RowsElement)
            {
                // SeleniumExtensions.HighlightElement(row);
                string rowindex = row.GetAttribute("row-index");
                string actBA = DriverHelper.Driver.WaitForElement(By.XPath($"//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row') and @row-index='{rowindex}']//*[@col-id='voa_billingauthorityid']//a")).GetAttribute("aria-label");
                string actBARef = DriverHelper.Driver.WaitForElement(By.XPath($"//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row') and @row-index='{rowindex}']//*[@col-id='voa_billingauthorityreference']//label")).GetAttribute("aria-label");
                string actEffectFrom = DriverHelper.Driver.WaitForElement(By.XPath($"//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row') and @row-index='{rowindex}']//*[@col-id='voa_effectivefrom']//label")).GetAttribute("aria-label");
                if (actEffectFrom != "")
                {
                    string DateFormatForEffectiveFrom = SeleniumExtensions.GetDateFormat2(actEffectFrom);
                    DateTime dateToValForEffectiveFrom = DateTime.ParseExact(actEffectFrom, DateFormatForEffectiveFrom, CultureInfo.InvariantCulture);
                    actEffectFrom = dateToValForEffectiveFrom.ToString("M/d/yyyy");
                }
                string actEffectiveTo = DriverHelper.Driver.WaitForElement(By.XPath($"//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row') and @row-index='{rowindex}']//*[@col-id='voa_effectiveto']//label")).GetAttribute("aria-label");
                if (actEffectiveTo != "")
                {
                    string DateFormatForEffectiveTo = SeleniumExtensions.GetDateFormat2(actEffectiveTo);
                    DateTime dateToValForEffectiveTo = DateTime.ParseExact(actEffectiveTo, DateFormatForEffectiveTo, CultureInfo.InvariantCulture);
                    actEffectiveTo = dateToValForEffectiveTo.ToString("M/d/yyyy");
                }
                string actStatus = DriverHelper.Driver.WaitForElement(By.XPath($"//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row') and @row-index='{rowindex}']//*[@col-id='voa_statusid']//a")).GetAttribute("aria-label");



                if (expBA == actBA && expBARef == actBARef && expEffectiveTo == actEffectiveTo && expStatus == actStatus)
                {
                    ValidationFlag = true;
                    Log.Information($"The expected {expBA} , {expBARef} &  {expEffectiveDate} & {expEffectiveTo} and {expStatus}  has been validated in {rowindex} row");
                    break;
                }
                else if (expBA == actBA && expBARef == actBARef && expEffectiveDate == actEffectFrom && expStatus == actStatus)
                {
                    ValidationFlag = true;
                    Log.Information($"The expected {expBA} , {expBARef} &  {expEffectiveDate} & {expEffectiveTo} and {expStatus}  has been validated in {rowindex} row");
                    break;
                }
                else
                {
                    Log.Information($"The record has not been validated in the {rowindex} row");
                    continue;
                }

            }
            if (ValidationFlag == true)
            {
                Assert.IsTrue(true);
                Log.Information($"All the Records from the datatable row has been validated");

            }
            else
            {
                Assert.IsTrue(false);
                Log.Error($"The record is not present in any Rows");

            }
        }


        public void EnterDetailsForSupplimentaryRecordOnDialog(string requestType, string jobType, string town, string postCode, string expUPRN)
        {
            string proposedEffectiveDateChanged = DateTime.Now.AddDays(-2).ToString("M/d/yyyy");

            //By RTFirstValue = GetFirstLookUp(requestType);
            //  SeleniumExtensions.EnterInLookUpField(RTFirstValue, RequestTypeOnDialog, RequestTypeSearchBtnOnDialog, requestType);
            By JTFirstValue = GetFirstLookUp(jobType);
            SeleniumExtensions.EnterInLookUpField(JTFirstValue, JobTypeOnDialog, JobTypeSearchBtnOnDialog, jobType);
            // SelectDateFromDatePickerElement(ProposedDatePickerSelector, -2);
            // FillAndSelectLookUpResult(JobTypeOnDialog, jobType);

            PickProposedDateForBP(proposedEffectiveDateChanged);
            // PickProposedDate(-2);
            switch (jobType)
            {
                case "Proposal":
                    By ReasonGroundValue = GetFirstLookUp("Proposal against List Entry - New Taxpayer");
                    SeleniumExtensions.EnterInLookUpField(ReasonGroundValue, ReasonGround, ReasonGroundSrcBtn, "Proposal against List Entry - New Taxpayer");
                    break;
                case "Appeal":
                    break;
            }
            FindHereditamentOnDialogForJob(town, postCode, expUPRN);
            ClickCommandBarOptionOnDialog("Save");
            ClickCommandBarOptionOnDialog("Submit");
            ContinueBtnOnConfirmDialog.ClickUsingJavascript();
            CloseButtOnDialog.ClickUsingJavascript();
        }

        public string EnterDetailsForSupplimentaryRecordOnDialogforEnquiry(string requestType, string jobType, string town, string postCode, string expUPRN)
        {
            string proposedEffectiveDateChanged = DateTime.Now.AddDays(-2).ToString("M/d/yyyy");

            By RTFirstValue = GetFirstLookUp(requestType);
            SeleniumExtensions.EnterInLookUpField(RTFirstValue, RequestTypeOnDialog, RequestTypeSearchBtnOnDialog, requestType);
            By JTFirstValue = GetFirstLookUp(jobType);
            SeleniumExtensions.EnterInLookUpField(JTFirstValue, JobTypeOnDialog, JobTypeSearchBtnOnDialog, jobType);
            // SelectDateFromDatePickerElement(ProposedDatePickerSelector, -2);
            // FillAndSelectLookUpResult(JobTypeOnDialog, jobType);

            PickProposedDateForBP(proposedEffectiveDateChanged);
            // PickProposedDate(-2);
            //switch (jobType)
            //{
            //    case "Proposal":
            //        By ReasonGroundValue = GetFirstLookUp("Proposal against List Entry - New Taxpayer");
            //        SeleniumExtensions.EnterInLookUpField(ReasonGroundValue, ReasonGround, ReasonGroundSrcBtn, "Proposal against List Entry - New Taxpayer");
            //        break;
            //    case "Appeal":
            //        break;
            //}
            FindHereditamentOnDialogForJob(town, postCode, expUPRN);
            ClickCommandBarOptionOnDialog("Save");
            ClickCommandBarOptionOnDialog("Submit");
            ContinueBtnOnConfirmDialog.ClickUsingJavascript();
            CloseButtOnDialog.ClickUsingJavascript();

            return proposedEffectiveDateChanged;
        }

        public void CreateSupplementaryJobAtJobLevel(string requestType, string jobType, string town, string postCode, string expUPRN)
        {
            string proposedEffectiveDateChanged = DateTime.Now.AddDays(-2).ToString("M/d/yyyy");

            FillAndSelectLookUpResult(JobTypeOnDialog, jobType);
            PickProposedDateForBP(proposedEffectiveDateChanged);
            FindHereditamentOnDialogForJob(town, postCode, expUPRN);
            ClickCommandBarOptionOnDialog("Save");
            ClickCommandBarOptionOnDialog("Submit");
            ContinueBtnOnConfirmDialog.ClickUsingJavascript();
            CommonPage commonpage = new CommonPage();
            commonpage.waitTillSavingDisaddpears("Saving...", "progressbar");
            CloseButtOnDialog.ClickUsingJavascript();
        }

        public void FindHereditamentForJobCreation(string town, string postCode, string expUPRN)
        {
            FindHereditament.ClickUsingJavascript();
            TownCityOnDialog.ClearAndSendkeys(town);
            PostCodeOnDialog.ClearAndSendkeys(postCode);

            SearchOnDialog.ClickUsingJavascript();

            while (true)
            {

                IList<IWebElement> HereditamentRows = Driver.FindElements(AddressRow);
                Console.WriteLine(HereditamentRows.Count);
                bool found = false;

                IWebElement UPRNEle = DriverHelper.Driver.WaitForElement(By.XPath($"//*[@data-automationid='DetailsList']//*[@data-item-index='{HereditamentRows.Count - 1}']//*[contains(@class,'cellCheck')]//following-sibling::*//*[@data-automation-key='uprn_column']//*[contains(@class,'ms-TooltipHost')]"));
                SeleniumExtensions.ScrollToElement(UPRNEle);
                HereditamentRows = Driver.FindElements(AddressRow);


                foreach (var row in HereditamentRows)
                {
                    //string sortOrder = FindHereditamentAddressHeaders.GetAttribute("data-icon-name");
                    //if (sortOrder == "SortDown")
                    //{
                    //    FindHereditamentAddressHeaders.ClickUsingJavascript();
                    //}
                    row.ElementVisisbleUsingExplicitWait(10);
                    SeleniumExtensions.ScrollToElement(row);

                    string rowindex = row.GetAttribute("data-item-index");
                    UPRNEle = DriverHelper.Driver.WaitForElement(By.XPath($"//*[@data-automationid='DetailsList']//*[@data-item-index='{rowindex}']//*[contains(@class,'cellCheck')]//following-sibling::*//*[@data-automation-key='uprn_column']//*[contains(@class,'ms-TooltipHost')]"));
                    if (UPRNEle != null)
                    {
                        string actUPRN = UPRNEle.Text;

                        if (actUPRN == expUPRN)
                        {
                            found = true;
                            row.ClickUsingJavascript();
                            SelectOnDIalog.ClickUsingJavascript();
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                if (found)
                {
                    Log.Information("The hereditament was found and selected");
                    break;
                }
                if (NextPageButtonOnDialog.ElementVisisbleUsingExplicitWait(5) == true)
                {
                    string isEnabledtext = NextPageButtonOnDialog.GetAttribute("class");
                    if (!isEnabledtext.Contains("is-disabled"))
                    {
                        NextPageOnDialog.ClickUsingJavascript();
                    }
                    else
                    {
                        Log.Error("Value not found and reached end of pagination.");
                        Console.WriteLine("Value not found and reached end of pagination.");
                        Assert.Fail($"The hereditament with the expected uprn {expUPRN} does not exists in the list");
                        break;
                    }
                }
                else
                {
                    Log.Error("Value not found and reached end of pagination.");
                    Console.WriteLine("Value not found and reached end of pagination.");
                    Assert.Fail($"The hereditament with the expected uprn {expUPRN} does not exists in the list");
                    break;
                }

            }
            Thread.Sleep(3000);

        }
        public void FindHereditamentForJobCreationUprn(string town, string postCode, string expUPRN)
        {
            var hereditamentsPage = new HereditamentsPage();
            FindHereditament.ClickUsingJavascript();
            TownCityOnDialog.ClearAndSendkeys(town);
            PostCodeOnDialog.ClearAndSendkeys(postCode);

            SearchOnDialog.ClickUsingJavascript();
            hereditamentsPage.selectHeridetamentByuprn(expUPRN);
            SeleniumExtensions.scrollToBtnBasedOnLabelAndClick("Select");
            CommonPage commonpage = new CommonPage();
            commonpage.waitTillFindHeridetamentDisaddpears();
            SeleniumExtensions.WaitForReadyStateToComplete();

        }

        public void FindHereditamentOnDialogForJob(string town, string postCode, string expUPRN)
        {
            FindHereditamentOnDialog.ClickUsingJavascript();
            TownCityOnDialog.ClearAndSendkeys(town);
            PostCodeOnDialog.ClearAndSendkeys(postCode);

            SearchOnDialog.ClickUsingJavascript();

            while (true)
            {

                IList<IWebElement> HereditamentRows = Driver.FindElements(AddressRow);

                bool found = false;
                IWebElement UPRNEle = DriverHelper.Driver.WaitForElement(By.XPath($"//*[@data-automationid='DetailsList']//*[@data-item-index='{HereditamentRows.Count - 1}']//*[contains(@class,'cellCheck')]//following-sibling::*//*[@data-automation-key='uprn_column']//*[contains(@class,'ms-TooltipHost')]"));
                SeleniumExtensions.ScrollToElement(UPRNEle);
                HereditamentRows = Driver.FindElements(AddressRow);

                foreach (var row in HereditamentRows)
                {
                    //string sortOrder = FindHereditamentAddressHeaders.GetAttribute("data-icon-name");
                    //if (sortOrder == "SortDown")
                    //{
                    //    FindHereditamentAddressHeaders.ClickUsingJavascript();
                    //}
                    row.ElementVisisbleUsingExplicitWait(10);
                    SeleniumExtensions.ScrollToElement(row);

                    string rowindex = row.GetAttribute("data-item-index");
                    UPRNEle = DriverHelper.Driver.WaitForElement(By.XPath($"//*[@data-automationid='DetailsList']//*[@data-item-index='{rowindex}']//*[contains(@class,'cellCheck')]//following-sibling::*//*[@data-automation-key='uprn_column']//*[contains(@class,'ms-TooltipHost')]"));
                    if (UPRNEle != null)
                    {
                        string actUPRN = UPRNEle.Text;

                        if (actUPRN == expUPRN)
                        {
                            found = true;
                            row.ClickUsingJavascript();
                            SelectOnDIalog.ClickUsingJavascript();
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                if (found)
                {
                    Log.Information("The hereditament was found and selected");
                    break;
                }
                if (NextPageButtonOnDialog.ElementVisisbleUsingExplicitWait(5) == true)
                {
                    string isEnabledtext = NextPageButtonOnDialog.GetAttribute("class");
                    if (!isEnabledtext.Contains("is-disabled"))
                    {
                        NextPageOnDialog.ClickUsingJavascript();
                    }
                    else
                    {
                        Log.Error("Value not found and reached end of pagination.");
                        Console.WriteLine("Value not found and reached end of pagination.");
                        Assert.Fail($"The hereditament with the expected uprn {expUPRN} does not exists in the list");
                        break;
                    }
                }
                else
                {
                    Log.Error("Value not found and reached end of pagination.");
                    Console.WriteLine("Value not found and reached end of pagination.");
                    Assert.Fail($"The hereditament with the expected uprn {expUPRN} does not exists in the list");
                    break;
                }


            }
            Thread.Sleep(4000);

        }


        public static void SelectDateFromDatePickerElement(By locator, float days)
        {
            string proposedEffectiveDateChanged;

            proposedEffectiveDateChanged = DateTime.Now.AddDays(days).ToString("M/d/yyyy");

            DateTime dateToEnter = DateTime.Parse(proposedEffectiveDateChanged);
            var currentDate_02 = dateToEnter.ToString("d, MMMM, yyyy");
            string[] dateComponent = currentDate_02.Split(',');
            string DayVal = dateComponent[0].Trim();
            string MonthVal = dateComponent[1].Trim();
            string YearVal = dateComponent[2].Trim();
            ScrollDiv.ScrollAndClick(locator, 20);
            SelectDateFromDatePicker(DayVal, MonthVal, YearVal);

        }

        public void ValidateCorrespondenceIntegrationMessageStatus(string statusText)
        {
            string actualStatus = "";
            IList<IWebElement> statusRecords = DriverHelper.Driver.FindElements(CorrIntegrationMessageStatus);
            foreach (var row in statusRecords)
            {
                actualStatus = row.GetAttribute("aria-label");
                Assert.AreEqual(statusText, actualStatus);
                break;
            }


        }

        public void ClickOnOptionInRelatedTabOnForm(string optionToSelect, string tabName, string FormName)
        {
            ValidateAndClickOnTab(tabName, FormName);
            IWebElement optionToSel = OptionsFromRelatedTab(optionToSelect);
            optionToSel.ClickUsingJavascript();
            Thread.Sleep(20000);
        }
        public void ClickOnNewOptionInRelatedTabOnForm(string optionToSelect, string tabName, string FormName)
        {
            Thread.Sleep(4000);
            ValidateAndClickOnTab(tabName, FormName);
            IWebElement optionToSel = NewOptionsFromRelatedTab(optionToSelect);
            optionToSel.ClickUsingJavascript();
            Thread.Sleep(20000);
        }

        public void SelectOptionFromMoreCommandsJob(string optionToSelect)
        {
            waitHelpers wh = new waitHelpers();
            //if (wh.elementDisplayed(CommandBarItemUsingBy(optionToSelect)))
            //{
            //    CommandBarItem(optionToSelect).ClickUsingJavascript();
            //}
            //else
            //{
            //    MoreCommandsForJob.ClickUsingJavascript();
            //    IWebElement OptionToSelect = OverFlowCommandOptionsForJob(optionToSelect);
            //    OptionToSelect.ElementVisisbleUsingExplicitWait(2);
            //    OptionToSelect.ClickUsingJavascript();
            //}
            MoreCommandsForJob.ClickUsingJavascript();
            IWebElement OptionToSelect = OverFlowCommandOptionsForJob(optionToSelect);
            OptionToSelect.ElementVisisbleUsingExplicitWait(2);
            OptionToSelect.ClickUsingJavascript();

        }

        public void AssignJobToSelf(string optionToSelect)
        {
            waitHelpers wh = new waitHelpers();
            try
            {
                if (wh.WaitTillElementDisplayed(CommandBarItemUsingBy(optionToSelect), 9) == true)
                {
                    ClickCommandBarOption(optionToSelect);
                    IWebElement ButtonToclick = ClickButtonOnAssignDialog("Assign");
                    ButtonToclick.ClickUsingJavascript();

                }

            }
            catch (Exception e)
            {
                SelectOptionFromMoreCommandsJob(optionToSelect);
                IWebElement ButtonToclick = ClickButtonOnAssignDialog("Assign");
                ButtonToclick.ClickUsingJavascript();
            }
            ClickCommandBarOption("Save");
        }

        public void ValidateDialogHeaderAndClickOnButton(string expHeaderText, string buttonName)
        {
            string actHeaderText = DialogPopUpHeader.GetAttribute("aria-label").Trim();
            Assert.AreEqual(expHeaderText, actHeaderText, "The dialog header is does not match");
            if (buttonName == "Yes")
            {
                YesDialogPopUp.ClickUsingJavascript();
            }
            else
            {
                NoDialogPopUp.ClickUsingJavascript();
            }

        }

        public void ValidateDialogMessageAndClickOnButton(string expMessageText, string buttonName)
        {
            string actMessageText = DialogPopUpMessage.Text.Trim();
            Assert.AreEqual(expMessageText, actMessageText, "The dialog header is does not match");
            if (buttonName == "Yes")
            {
                YesDialogPopUp.ClickUsingJavascript();
            }
            else
            {
                NoDialogPopUp.ClickUsingJavascript();
            }

        }

        private By SelectOptionFromDropdown(string optionToSelect)
        {
            string customizeSelector = $"[role='listbox'] [role='option']:contains('{optionToSelect}')";
            return By.CssSelector(customizeSelector);
        }

        public void FindHereditamentForJobCreationforComparator(string town, string postCode, string expUPRN)
        {
            waitHelpers wh = new waitHelpers();
            CommonPage commonpage = new CommonPage();

            //wh.isElementDisplayed(FindHereditamentQuickrootbuttonBy, 60);
            //FindHereditamentQuickrootbutton.ClickUsingJavascript();
            //TownCityOnDialogQuickrootbutton.ClearAndSendkeys(town);
            var hereditamentsPage = new HereditamentsPage();
            hereditamentsPage.selectSearchByDropDown("UPRN");
            wh.GetWebElement(uprnBY).ClearAndSendkeys(expUPRN);

            //PostCodeOnDialogQuickrootbutton.ClearAndSendkeys(postCode);

            SearchOnDialog.ClickUsingJavascript();
            hereditamentsPage.selectHeridetamentByuprn(expUPRN);
            SeleniumExtensions.scrollToBtnBasedOnLabelAndClick("Select");
            commonpage.waitTillFindHeridetamentDisaddpears();


            //while (true)
            //{

            //    IList<IWebElement> HereditamentRows = wh.getAllWebElements(AddressRow);
            //    Console.WriteLine(HereditamentRows.Count);
            //    bool found = false;

            //    IWebElement UPRNEle = DriverHelper.Driver.WaitForElement(By.XPath($"//*[@data-automationid='DetailsList']//*[@data-item-index='{HereditamentRows.Count - 1}']//*[contains(@class,'cellCheck')]//following-sibling::*//*[@data-automation-key='uprn_column']//*[contains(@class,'ms-TooltipHost')]"));
            //    SeleniumExtensions.ScrollToElement(UPRNEle);
            //    HereditamentRows = Driver.FindElements(AddressRow);


            //    foreach (var row in HereditamentRows)
            //    {
            //        //string sortOrder = FindHereditamentAddressHeaders.GetAttribute("data-icon-name");
            //        //if (sortOrder == "SortDown")
            //        //{
            //        //    FindHereditamentAddressHeaders.ClickUsingJavascript();
            //        //}
            //        row.ElementVisisbleUsingExplicitWait(10);
            //        SeleniumExtensions.ScrollToElement(row);

            //        string rowindex = row.GetAttribute("data-item-index");
            //        UPRNEle = DriverHelper.Driver.WaitForElement(By.XPath($"//*[@data-automationid='DetailsList']//*[@data-item-index='{rowindex}']//*[contains(@class,'cellCheck')]//following-sibling::*//*[@data-automation-key='uprn_column']//*[contains(@class,'ms-TooltipHost')]"));
            //        if (UPRNEle != null)
            //        {
            //            string actUPRN = UPRNEle.Text;

            //            if (actUPRN == expUPRN)
            //            {
            //                found = true;
            //                row.ClickUsingJavascript();
            //                SelectOnDIalog.ClickUsingJavascript();
            //                break;
            //            }
            //            else
            //            {
            //                continue;
            //            }
            //        }
            //        else
            //        {
            //            continue;
            //        }
            //    }
            //    if (found)
            //    {
            //        Log.Information("The hereditament was found and selected");
            //        break;
            //    }
            //    if (NextPageButtonOnDialog.ElementVisisbleUsingExplicitWait(5) == true)
            //    {
            //        string isEnabledtext = NextPageButtonOnDialog.GetAttribute("class");
            //        if (!isEnabledtext.Contains("is-disabled"))
            //        {
            //            NextPageOnDialog.ClickUsingJavascript();
            //        }
            //        else
            //        {
            //            Log.Error("Value not found and reached end of pagination.");
            //            Console.WriteLine("Value not found and reached end of pagination.");
            //            Assert.Fail($"The hereditament with the expected uprn {expUPRN} does not exists in the list");
            //            break;
            //        }
            //    }
            //    else
            //    {
            //        Log.Error("Value not found and reached end of pagination.");
            //        Console.WriteLine("Value not found and reached end of pagination.");
            //        Assert.Fail($"The hereditament with the expected uprn {expUPRN} does not exists in the list");
            //        break;
            //    }

            //}

        }
        private IWebElement ChevronRightMed => Driver.WaitForElement(By.XPath("//*[@data-icon-name='ChevronRightMed']"));

        public void PickProposedDateForBP(string dateCaptured)
        {
            DateTime dateToEnter = DateTime.Parse(dateCaptured);
            //M/d/yyyy
            var currentDate_02 = dateToEnter.ToString("d, MMMM, yyyy");
            string[] dateComponent = currentDate_02.Split(',');
            string DayVal = dateComponent[0].Trim();
            string MonthVal = dateComponent[1].Trim();
            string YearVal = dateComponent[2].Trim();
            ScrollDiv.ScrollAndClick(ProposedDatePickerSelectorDialog, 20);
            SelectDateFromDatePicker(DayVal, MonthVal, YearVal);
            //SeleniumExtensions.SetTheDateForTheTableCalendar(currentDate_02, 1);

        }


        public void PickProposedDate(float days)
        {
            var currentDate_02 = DateTime.Now.AddDays(days).ToString("d, MMMM, yyyy");
            ScrollDiv.ScrollAndClick(ProposedDatePickerSelector, 20);
            SeleniumExtensions.SetTheDateForTheTableCalendar(currentDate_02, 1);
        }

        public void ClickBtnOnAllJobsCreated(string btnName)
        {
            Assert.IsTrue(QAQCPopUp.ElementVisisbleUsingExplicitWait(5), "The QA/QC dialog is not displayed yet");
            string popUpTitle = QAQCPopUp.GetAttribute("title");
            Assert.AreEqual("All Jobs Created for Request", popUpTitle);
            if (btnName == "Yes")
            {
                AllJobsCreatedButton.ClickUsingJavascript();
            }
            else
            {
                AllJobsCreatedNoButton.ClickUsingJavascript();
            }

        }

        private By PendingApprovalRows => By.XPath("//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row')]");

        private IWebElement DeleteJobOwner => Driver.WaitForElement(By.CssSelector("[id*='voa_jobowner'][aria-label*='Delete']"));

        private IWebElement GoBack => Driver.WaitForElement(By.XPath("[title='Go back']"));

        private By JobOwner => By.CssSelector("[aria-label='Job Owner, Lookup']");


        public void CheckandUpdatethePendingApprovaltab(string statusText)
        {
            int findRowsTries = 0;
            bool validateFlag = false;
            bool findrowsFlag = false;
            IList<IWebElement> RowsElement = Driver.FindElements(PendingApprovalRows);

            if (RowsElement.Count == 0)
            {
                while (RowsElement.Count == 0 && findRowsTries < 3)
                {
                    ClickCommandBarOption("Refresh");
                    ValidateAndClickOnTab("Quality Review", "Desktop Research Form");
                    RowsElement = Driver.FindElements(PendingApprovalRows);
                    if (RowsElement.Count > 0)
                    {
                        findrowsFlag = true;
                        break;
                    }
                    findRowsTries++;
                }
                if (findrowsFlag == false)
                {
                    Assert.Fail("No Pending Approval Rows displayed");

                }


            }

            foreach (var row in RowsElement)
            {
                string rowindex = row.GetAttribute("row-index");
                IWebElement StatusReason = DriverHelper.Driver.WaitForElement(By.XPath($"//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row') and @row-index='{rowindex}']//*[contains(@col-id,'statuscode')]//label"));

                string actStatusReasonText = StatusReason.GetAttribute("aria-label");
                if (actStatusReasonText == statusText)
                {

                    IWebElement OpenQualityReview = DriverHelper.Driver.WaitForElement(By.XPath($"//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row') and @row-index='{rowindex}']//*[contains(@col-id,'voa_name')]"));
                    OpenQualityReview.DoubleClickElementUsingJSExecutor();
                    DeleteJobOwner.ClickUsingJavascript();
                    FillAndSelectLookUpResult(JobOwner, "Gupta");
                    ClickCommandBarOption("Save");
                    ClickCommandBarOption("Approve");
                    ClickCommandBarOption("Save");
                    OkButtonopopup.ClickUsingJavascript();
                    ClickCommandBarOption("Save & Close");

                }
            }

        }


        public IWebElement UnSavedchanges => Driver.WaitForElement(By.CssSelector("[aria-label='Unsaved Changes']"));

        public void FindAddressForChangeOfAddressforAuto(string postCode)
        {
            waitHelpers wh = new waitHelpers();
            Thread.Sleep(3000);
            SeleniumExtensions.ScrollToElement(FindAddress_New);
            FindAddress_New.ClickUsingJavascript();
            BtnSearchOnDialog.ClickUsingJavascript();

            while (true)
            {
                IList<IWebElement> AddressesRows = wh.getAllWebElements(AddressRow);
                int count = AddressesRows.Count;
                for (int i = 0; i <= count; i++)
                {
                    AddressesRows[i].ElementVisisbleUsingExplicitWait(10);

                    AddressesRows[i].ClickUsingJavascript();
                    BtnUseAddressOnDialog.ClickUsingJavascript();

                    if (StatusReasonAddress.IsElementDisplayed(5))
                    {
                        string statusReasonaddressExp = StatusReasonAddress.GetAttribute("value").ToString();

                        if (statusReasonaddressExp == "Duplicate Validation Passed")
                        {
                            break;
                        }

                    }
                }
                IWebElement status = wh.GetWebElement(By.CssSelector("[aria-label='Status Reason']"));
                string statusReason = status.GetAttribute("value");
                if (statusReason == "Duplicate Validation Passed")
                {
                    Console.WriteLine("Duplicate Validation Passed");
                    break;
                }
                else
                {
                    Assert.Fail("Duplicate Validation Failed");
                    Log.Error("Duplicate Validation Failed");
                    break;
                }
            }
        }


        public void FindAddressForChangeOfAddressforManual(string postCode)
        {
            waitHelpers wh = new waitHelpers();
            Thread.Sleep(3000);
            SeleniumExtensions.ScrollToElement(FindAddress_New);
            FindAddress_New.ClickUsingJavascript();
            BtnSearchOnDialog.ClickUsingJavascript();
            Thread.Sleep(4000);
            while (true)
            {
                IList<IWebElement> AddressesRows = wh.getAllWebElements(AddressRow);
                int count = AddressesRows.Count;
                for (int i = 0; i <= count; i++)
                {
                    AddressesRows[i].ElementVisisbleUsingExplicitWait(10);

                    AddressesRows[i].ClickUsingJavascript();
                    BtnUseAddressOnDialog.ClickUsingJavascript();

                    if (StatusReasonAddress.IsElementDisplayed(5))
                    {
                        string statusReasonaddressExp = StatusReasonAddress.GetAttribute("value").ToString();

                        if (statusReasonaddressExp == "Duplicate Validation Passed")
                        {
                            break;
                        }

                    }
                }
                IWebElement status = DriverHelper.Driver.WaitForElement(By.CssSelector("[aria-label='Status Reason']"));
                string statusReason = status.GetAttribute("value");
                if (statusReason == "Duplicate Validation Passed")
                {
                    Console.WriteLine("Duplicate Validation Passed");
                    break;
                }
                else
                {
                    Assert.Fail("Duplicate Validation Failed");
                    Log.Error("Duplicate Validation Failed");
                    break;
                }
            }
        }

        private IWebElement FindAddress_New => Driver.WaitForElement(By.CssSelector("button[aria-label='Find Address']"));


        private IWebElement BtnSearchOnDialog => Driver.WaitForElement(By.CssSelector("[class*='ms-Modal-scrollableConten'] button[aria-label='Click to search for address']"));

        private IWebElement BtnUseAddressOnDialog => Driver.WaitForElement(By.XPath("//button//*[text()='Use Address']"));

        private IWebElement StatusReasonAddress => Driver.WaitForElement(By.CssSelector("[aria-label='Status Reason']"));

        public static void SelectDateFromPresentationDatePicker(string dayVal, string monthVal, string yearVal)

        {

            Assert.IsTrue(DatePickerDialog.ElementVisisbleUsingExplicitWait(5), "The calender dialog is not open");

            //MonthAndYearEleOnDatePicker.ClickUsingJavascript();

            waitHelpers wh = new waitHelpers();

            By navYear = By.CssSelector("[aria-label='Calendar'] button[aria-label*='Year picker'] *");

            By nxtYr = By.CssSelector("button[title *= 'Navigate to next']");

            By PrvYr = By.CssSelector("button[title *= 'Navigate to previous']");

            //[role= 'dialog'][aria-label = 'Calendar'] button[class*='CalendarPicker__itemButton ']

            string currYearDisplayed = wh.getElementText(navYear).Split(' ')[1]; ;

            Console.Write($"currYearDisplayed : " + currYearDisplayed);

            Console.Write($"yearVal : " + yearVal);

            while (currYearDisplayed != yearVal)

            {

                wh.clickOnElement(PrvYr);

                Thread.Sleep(900);

                currYearDisplayed = wh.getElementText(navYear).Split(' ')[1];

                if (currYearDisplayed == yearVal)

                {

                    break;

                }

            }
            By calenYear = By.CssSelector("[aria-label='Calendar'] button[aria-label*='Year picker']");
            wh.clickOnElement(calenYear);
            IWebElement monthToSelect = MonthElementForDatePicker(monthVal);

            monthToSelect.ClickUsingJavascript();

            IWebElement dayToSelect = DayElementForDatePicker(dayVal);

            dayToSelect.ClickUsingJavascript();

        }

    }
}
