using OpenQA.Selenium;
using POMSeleniumFrameworkPoc1.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POMSeleniumFrameworkPoc1.Pages.UI.RBSP_BP
{
    public partial class CommonPage : BasePage
    {

        private IWebElement LookUpResult => Driver.WaitForElement(By.CssSelector("ul[aria - label = 'Lookup results'] li"));

        private By findHereditament => By.CssSelector("[class*='ms-Dialog-main main']");

        private By FirstLookUpResult => By.CssSelector("ul[id*='fieldControl-LookupResultsDropdown_voa'] li");

        private By moreoptionsRequest => By.XPath("//*[@title='More commands for Request']");

        private IWebElement MenuItem(string sectionName, string menuItem)
        {
            string customizeSelector = $"ul[aria-label='{sectionName}'] li[aria-label='{menuItem}']";
            return Driver.WaitForElement(By.CssSelector(customizeSelector));
        }

        private IWebElement FieldLabelInSection(string sectionName, string fieldName)
        {
            string customizeSelector = $"(//section[@aria-label='{sectionName}']//label[contains(text(),'{fieldName}')])[1]";
            return Driver.WaitForElement(By.XPath(customizeSelector));
        }

        private IWebElement TabNameInSection(string sectionName, string tabName)
        {
            string customizeSelector = $"//section[@aria-label='{sectionName}']//*[@role='tablist']//button//span[text()='{tabName}']";
            return Driver.WaitForElement(By.XPath(customizeSelector));
        }

        private IWebElement LinkNameInSection(string sectionName, string linkName)
        {
            string customizeSelector = $"//section[@aria-label='{sectionName}']//a[text()='{linkName}']";
            return Driver.WaitForElement(By.XPath(customizeSelector));
        }
        private IWebElement CommandBarItem(string commandBarOption)
        {
            string customizeSelector = $"//ul[contains(@data-lp-id,'commandbar')]//li//button//*[text()='{commandBarOption}']";
            return Driver.WaitForElement(By.XPath(customizeSelector));
        }

        private IWebElement CommandBarMenuListOption(string listOption)
        {
            string customizeSelector = $"ul li button[aria-label='{listOption}']";
            return Driver.WaitForElement(By.CssSelector(customizeSelector));
        }



        private IWebElement FindTabByTitleInList(String titleName, string tablistName)
        {
            string cssSelector = $"ul[aria-label = '{tablistName}'] li[title ='{titleName}']";
            return Driver.WaitForElement(By.CssSelector(cssSelector));
        }

        private IWebElement TabCommandBarItem(string tabNameCommandbar, string commandBarOption)
        {
            string customizeSelector = $"//ul[@aria-label='{tabNameCommandbar}']//li//*[text()='{commandBarOption}']";
            return Driver.WaitForElement(By.XPath(customizeSelector));
        }
        private IWebElement ActivityStatus(String LatestDate)
        {
            string xpathSelector = $"//*[contains(@class,'ag-cell-value')]//label/div[text()='{LatestDate}']/ancestor::*[contains(@class,'ag-row') and @role='row']//*[@col-id='statecode']//label/div";
            return Driver.WaitForElement(By.XPath(xpathSelector));
        }

        private IWebElement JourneyHeader(String journeyHeader)
        {
            string customizeSelector = $"*//button[contains(@aria-label,'{journeyHeader}') and contains(@data-id,'MscrmControls.Containers.ProcessBreadCrumb-processHeaderStageButton')]";
            return Driver.WaitForElement(By.XPath(customizeSelector));
        }

        private IWebElement JourneyStage(String buttonName)
        {
            string customizeSelector = $"[id*='MscrmControls.Containers.ProcessStageControl-processHeaderStageFlyoutContainer'] button[aria-label='{buttonName}']";
            return Driver.WaitForElement(By.CssSelector(customizeSelector));
        }

        private By getTabs(String tabListName)
        {
            string customizeSelector = $"//ul[@aria-label='{tabListName}']//li";
            return By.CssSelector(customizeSelector);

        }

        private By GetFirstLookUp(String lookUpValue)
        {

            //"ul[id*='fieldControl-LookupResultsDropdown_voa'] li[aria-label='{lookUpValue}']"
            ////ul[contains(@id,'fieldControl-LookupResultsDropdown_voa')]//li//*[text()='House4You']
            string customizeSelector = $"//ul[contains(@id,'fieldControl-LookupResultsDropdown_voa')]//li//*[text()='{lookUpValue}']";
            return By.XPath(customizeSelector);

        }


        // Correspondence
        private IWebElement OutboundCorrespondenceLink => Driver.WaitForElement(By.CssSelector("[class*='ag-row-position-absolute'] [col-id='voa_name'] [aria-label*='OBC']"));
        // "[aria-label='Status Reason'][id*='statuscode.fieldControl-pickliststatus-comboBox_button']"
        private IWebElement OCStatusReasondropdown => Driver.WaitForElement(By.CssSelector("button[aria-label='Status Reason']"));

        private IWebElement OCStatusReasondropdownvalue => Driver.WaitForElement(By.CssSelector("[aria-label='Status Reason']"));
        // "[id*='statuscode.fieldControl'] ul[aria-label='Status Reason'] li[data-text='Ready']"
        private IWebElement OCStatusReasonReady => Driver.WaitForElement(By.CssSelector("[id*='statuscode.fieldControl'] ul[aria-label='Status Reason'] li[data-text='Ready']"));
        // "[aria-label='Status Reason'] [id*='statuscode.fieldControl-pickliststatus-comboBox_text-value']"
        private IWebElement OCStatusReasonText => Driver.WaitForElement(By.CssSelector("[aria-label='Status Reason']"));
        //  "a[id*='voa_correspondence']"
        private IWebElement CorrespondencePDFLink => Driver.WaitForElement(By.CssSelector("a[id*='voa_correspondence']"));

        private IWebElement SaveInProgressError => Driver.WaitForElement(By.CssSelector("[data-id='errorDialogdialog'] [data-id='dialogTitleText']"));
        private IWebElement SaveInProgressOKBtn => Driver.WaitForElement(By.CssSelector("[data-id='errorDialogdialog'] button[aria-label='OK']"));

        private By IntegrationDataSourcLookupInput => By.CssSelector("input[aria-label='Integration Data Source, Lookup']");
        private By RelationShipRoleLookUp => By.CssSelector("input[aria-label='Relationship Role, Lookup']");
        private By SubmittedbyLookupInputSelector => By.XPath("//input[contains(@aria-label,'Submitted By')]");
        private IWebElement NewRequestBtnUnderRelatedRequestSection => Driver.WaitForElement(By.CssSelector("ul li button[aria-label='New Request. Add New Request']"));

        private By NewRequestBtnUndrReltdReqSec => By.CssSelector("ul li button[aria-label='New Request. Add New Request']");

        private IWebElement MoreCommandsRelatedRequestPrev => Driver.WaitForElement(By.CssSelector("section[aria-label='Related Requests'] [data-id*='commandBar'] button[aria-label='More commands for Request']"));
        private By MoreCommandsRelatedRequest => By.CssSelector("section[aria-label='Related Requests'] [data-id*='commandBar'] button[aria-label='More commands for Request']");
        By moreCommandsRelatedRequestBy = By.CssSelector("section[aria-label='Related Requests'] [data-id*='commandBar'] button[aria-label='More commands for Request']");
        private IWebElement CommandOptionRelatedRequest(String optionVal)
        {
            string customizeSelector = $"[data-id ='OverflowFlyout'] li[id*= 'voa_requestlineitem'] button[aria-label *= '{optionVal}']";
            return Driver.WaitForElement(By.CssSelector(customizeSelector));
        }

        private List<IWebElement> businessStageNames => Driver.WaitForElements(By.CssSelector($"button[id*='ProcessBreadCrumb-processHeaderStageButton'] div[id*='ProcessBreadCrumb-processHeaderStageName']"));
        By businessStageNamesBy = By.CssSelector($"button[id*='ProcessBreadCrumb-processHeaderStageButton'] div[id*='ProcessBreadCrumb-processHeaderStageName']");

        private List<IWebElement> ReseachQuetionsNames => Driver.WaitForElements(By.CssSelector($"//*[@data-control-name='Title4' and not(contains(@style,'display: none;'))]//*[@data-control-part='text']"));
        private IWebElement expanderEle => Driver.WaitForElement(By.CssSelector($"[class*='cellIsGroupExpander']"));

        private IWebElement GoBackLink => Driver.WaitForElement(By.CssSelector("[aria-label='Press Enter to go back.']"));

        private IWebElement OKBtn => Driver.WaitForElement(By.CssSelector("[role='dialog'] [aria-label='OK']"));

        private IWebElement ConfirmOKBtn => Driver.WaitForElement(By.CssSelector("[data-id='confirmdialog'] button[title='OK']"));

        private IWebElement LeavePageOKBtn => Driver.WaitForElement(By.CssSelector("[data-id='routerDialogdialog'] button[title='OK']"));

        private IWebElement JobCreatedRowOnRelatedJobs => Driver.WaitForElement(By.CssSelector("label[aria-label*='JOB-']"));


        private IWebElement NoactionLink => Driver.WaitForElement(By.CssSelector("[class*='ag-row-position-absolute'] [col-id='voa_name'] [aria-label*='NAD']"));

        private IWebElement AssignCommmandBar => Driver.WaitForElement(By.CssSelector("//button[@title='Assign']"));

        private IWebElement AssignBtnNew => Driver.WaitForElement(By.CssSelector("button[title='Assign']"));

        private By AssignBtn => By.CssSelector("button[title='Assign']");
    }
}
