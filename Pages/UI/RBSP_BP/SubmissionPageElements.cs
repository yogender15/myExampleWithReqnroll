using OpenQA.Selenium;
using POMSeleniumFrameworkPoc1.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POMSeleniumFrameworkPoc1.Pages.UI
{
    public partial class SubmissionPage : BasePage
    {
        private IWebElement LookUpResultFirstValue => Driver.WaitForElement(By.CssSelector("[aria-label*='Lookup results'] ul li [data-id*='infoContainer']"));
        private IWebElement SiteMapItemNewTab => Driver.WaitForElement(By.XPath("//*[contains(@title,'Open site map item in a new tab')]"));
        private IWebElement ActiveSubmissionsTab => Driver.WaitForElement(By.CssSelector("ul[role='menu'] li[aria-label='Submissions']"));
        private IWebElement ScrollDiv => Driver.WaitForElement(By.CssSelector("#tab-section2"));
        private IWebElement SubmissionMenuButton => Driver.WaitForElement(By.CssSelector("li[aria-label='Submissions']"));
        private IWebElement NewSubmissionPlusButton => Driver.WaitForElement(By.CssSelector("ul[aria-label='Submission Commands'] li button[aria-label='New']"));
        private IWebElement SaveSubmissionMenuButton => Driver.WaitForElement(By.CssSelector("ul[role='menubar'] button[aria-label='Save (CTRL+S)']"));
        private By SubmittedbyLookupInputSelector => By.XPath("//input[contains(@aria-label,'Submitted By')]");
        private IWebElement SubmittedByLookUpResultItem => Driver.WaitForElement(By.XPath("//*[contains(@data-id,\"voa_primarycustomerid.fieldControl\")]//ul[contains(@id,'voa_primarycustomerid.fieldControl-LookupResultsDropdown')]/li"));
        private By SubmissionIdSelector => By.XPath("//input[@aria-label=\"Submission ID\"]");
        private IWebElement SubmissionIdElement => Driver.WaitForElement(By.XPath("//input[@aria-label=\"Submission ID\"]"));
        private IWebElement RelatedJobsSubmission => Driver.WaitForElement(By.CssSelector("li[aria-label='Related Jobs']"));
        private IWebElement RelatedTabSubmission1 => Driver.WaitForElement(By.XPath("//div[contains(@class,'pa-a flexbox') and contains(., 'Related')]"));

        private By IntegrationDataSourcLookupInput => By.CssSelector("input[aria-label='Integration Data Source, Lookup']");
        private By RelationShipRoleLookUp => By.CssSelector("input[aria-label='Relationship Role, Lookup']");
        private IWebElement MoreCommandsRelatedRequest => Driver.WaitForElement(By.CssSelector("section[aria-label='Related Requests'] [data-id*='commandBar'] button[aria-label='More commands for Request']"));


        private IWebElement IntegrationDataSourceLookUpFirstValue => Driver.WaitForElement(By.CssSelector("[aria-label='Integration Data Source Lookup results'] ul li [data-id*='infoContainer']"));


        private IWebElement RelatedTabElement => Driver.WaitForElement(By.CssSelector("li[aria-label='Related']"));
        private IWebElement RequestsUnderRelatedElement => Driver.WaitForElement(By.CssSelector("[aria-label='Requests Related - Common']"));


        public string SubmissionId;

        // Estate
        private By EstateActionTypeLookupInput => By.CssSelector("input[aria-label='//input[@aria-label = 'Estate Action Type, Lookup']");
        private IWebElement EstateTextBox => Driver.WaitForElement(By.XPath("(//label[text() = 'Estate Action Type']//following::div)[3]//input"));
        private IWebElement NumberofPlotTextBox => Driver.WaitForElement(By.XPath("(//label[text() = 'Number of Plots']//following::div)[3]//input"));
        private IWebElement SwitchedTypeButton => Driver.WaitForElement(By.XPath("//button[@role = 'switch']"));
        private IWebElement SaveAndClosePopop => Driver.WaitForElement(By.XPath("//span[text() = 'Save and Close']"));
        private IWebElement PlotStartingNumberTextBox => Driver.WaitForElement(By.XPath("(//label[text() = 'Plot Starting Number']//following::div)[3]//input"));
        private IWebElement EstateActionTypeLookUpFirstValue => Driver.WaitForElement(By.CssSelector("ul[data-id*='estateactiontypeid'] li"));
        private IWebElement NumberofPlotsLookUpFirstValue => Driver.WaitForElement(By.CssSelector("ul[data-id*='estateactiontypeid'] li"));
        private IWebElement PlotStartingNumberLookUpFirstValue => Driver.WaitForElement(By.CssSelector("ul[data-id*='estateactiontypeid'] li"));
        private IWebElement EstateTab => Driver.WaitForElement(By.XPath("//li[@aria-label = 'Estate Actions']"));
        private IWebElement CreatePlotsValidation => Driver.WaitForElement(By.XPath("//li[@aria-label = 'Estate Actions']"));
        private IWebElement EstateAction => Driver.WaitForElement(By.XPath("//span[text() = 'New Estate Action']"));
        private IWebElement EstateActionType => Driver.WaitForElement(By.XPath("//input[contains(@aria-label, 'Estate Action Type')]"));
        private IWebElement EstateActionTypeItem(string itemType) => Driver.WaitForElement(By.XPath($"(//span[starts-with(text(), '{itemType}')])[1]"));
        private IWebElement SubmitButton => Driver.WaitForElement(By.XPath("//button[contains(@aria-label, 'Submit')]"));
        private IWebElement SaveAndClose => Driver.WaitForElement(By.XPath("//span[text() = 'Save and Close']"));
        private IWebElement CreatePlotsValidationItem => Driver.WaitForElement(By.XPath("//span[text() = 'Create Plots']"));
        private IWebElement PlotsTab => Driver.WaitForElement(By.XPath("//li[text() = 'Plot Manager']"));
        private IWebElement PlotsTab46 => Driver.WaitForElement(By.XPath("//input[@value = '46']"));
        private IWebElement PlotsTab47 => Driver.WaitForElement(By.XPath("//input[@value = '47']"));
        private IWebElement PlotsTab48 => Driver.WaitForElement(By.XPath("//input[@value = '48']"));
        private IWebElement HouseTypeTab => Driver.WaitForElement(By.XPath("//li[text() = 'House Types']"));
        private IWebElement HouseTypePlusButton => Driver.WaitForElement(By.XPath("//span[text() = 'New House Type']"));
        private IWebElement HouseTypeNameTextBox => Driver.WaitForElement(By.XPath("(//label[text() = 'Name']//following::div)[3]//input"));
        private IWebElement HouseTypeItemCreated => Driver.WaitForElement(By.XPath("(//a[contains(@href, 'voa_housetype')])[1]"));
        private IWebElement PAdItemButton => Driver.WaitForElement(By.XPath("//button[@aria-label = 'Validate PAD Code: No']"));
        private IWebElement ContinueWarningButton => Driver.WaitForElement(By.XPath("//button[@aria-label = 'Continue with Warnings?: No']"));
        private IWebElement ValidatePadItemMenu => Driver.WaitForElement(By.XPath("(//div[@title = 'Validate PAD'])[1]"));
        private IWebElement BandingItemMenu => Driver.WaitForElement(By.XPath("(//div[@title = 'Banding'])[1]"));
        private IWebElement NextButtonMenu => Driver.WaitForElement(By.XPath("//button[@title = 'Next Stage']"));
        private IWebElement LowerBandingMenu => Driver.WaitForElement(By.XPath("//li[@title = 'Banding']"));
        private By ProposedTaxBandLookup => By.CssSelector("input[aria-label='Proposed Tax Band, Lookup']");
        private IWebElement ProposedTaxBand => Driver.WaitForElement(By.CssSelector("input[aria-label='Proposed Tax Band, Lookup']"));
        private IWebElement ProposedTaxBandEngland => Driver.WaitForElement(By.XPath("(//span[contains(@id, 'voa_counciltaxbandid')])[3]"));
        private IWebElement BandingOptions => Driver.WaitForElement(By.CssSelector("select[aria-label='Is Banding Complete?']"));
        //label[text() = 'Finish']
        private IWebElement FinishButton => Driver.WaitForElement(By.XPath("//label[text() = 'Finish']"));
        private IWebElement HouseTypeRecord => Driver.WaitForElement(By.XPath("//a[contains(@href, 'housetype')]"));
        private IWebElement Rooms => Driver.WaitForElement(By.XPath("//input[@aria-label = 'Rooms']"));
        private IWebElement BedRooms => Driver.WaitForElement(By.XPath("//input[@aria-label = 'Bedrooms']"));
        private By Parking => By.CssSelector("input[aria-label='Parking, Lookup']");
        private IWebElement ParkingTextBox => Driver.WaitForElement(By.CssSelector("input[aria-label='Parking, Lookup']"));
        private IWebElement ParkingLookUpResultFirstValue(string p1) => Driver.WaitForElement(By.XPath($"(//span[text() = '{p1}'])[2]"));
        private IWebElement EstateButton => Driver.WaitForElement(By.CssSelector("li[aria-label='Estate Files']"));
        private IWebElement SaveButton => Driver.WaitForElement(By.XPath("//button[contains(@title,'CTRL+S')]"));
        private IList<IWebElement> ListOfSubmissions => Driver.WaitForElements(By.CssSelector("*[col-id*='createdon'] div[class*='truncatableText'] label div")).ToList();

        private IWebElement OverflowBtnInRelatedRequests => Driver.WaitForElement(By.CssSelector("button[aria-label='More commands for Request']"));
        private IWebElement NewRequestBtnUnderRelatedRequestSection => Driver.WaitForElement(By.CssSelector("ul li button[aria-label='New Request. Add New Request']"));


        //Ashish's Code 240124
        private By ActiveSubmissionsRows => By.XPath("//*[contains(@class,'ag-body-viewport')]//*[contains(@class,'ag-center-cols-container')]//*[contains(@class,'ag-row-position-absolute')]");

        private By CreatedOnColumnVal => By.XPath("//*[contains(@class,'ag-cell-value') and @col-id='createdon']//label/div[text()]");



        private IWebElement SubmissionID => Driver.WaitForElement(By.XPath("//section[@aria-label='Details']//label[text()='Submission ID']//parent::*//parent::*//parent::*//following-sibling::*//input"));
        private IWebElement IDS => Driver.WaitForElement(By.XPath("//section[@aria-label='Details']//label[text()='Integration Data Source']//parent::*//parent::*//parent::*//following-sibling::*//li/div/div"));

        private IWebElement Remarks => Driver.WaitForElement(By.XPath("//section[@aria-label='Remarks']//textarea[@aria-label='Remarks']"));
        // "//section[@aria-label='Related Requests']//button[@role='link']"
        private IWebElement RequestLink => Driver.WaitForElement(By.XPath("//section[@aria-label='Related Requests']//*[@role='link']"));


        private IWebElement LatestVOANameCell(String LatestDate)
        {
            string xpathSelector = $"//*[contains(@class,'ag-cell-value') and @col-id='createdon']//label/div[text()='{LatestDate}']/ancestor::*[contains(@class,'ag-row') and @role='row']//*[@col-id='voa_name']//a";
            return Driver.WaitForElement(By.XPath(xpathSelector));
        }
        private IWebElement SubmissionIDCell(String LatestDate)
        {
            string xpathSelector = $"//*[contains(@class,'ag-cell-value') and @col-id='createdon']//label/div[text()='{LatestDate}']/ancestor::*[contains(@class,'ag-row') and @role='row']//*[@col-id='voa_requestreferenceid']//label/div";
            return Driver.WaitForElement(By.XPath(xpathSelector));
        }


        private IWebElement SelectFilterValue(String filterValue)
        {
            string xpathSelector = $"//*[contains(@class,'ms-Checkbox') and @title='{filterValue}']//label/span";
            return Driver.WaitForElement(By.XPath(xpathSelector));
        }

        private IWebElement CommandOptionRelatedRequest(String optionVal)
        {
            string customizeSelector = $"[data-id ='OverflowFlyout'] li[id*= 'voa_requestlineitem'] button[aria-label *= '{optionVal}']";
            return Driver.WaitForElement(By.CssSelector(customizeSelector));
        }

    }
}
