using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.BrowsingContext;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using POMSeleniumFrameworkPoc1.Helpers;
using Reqnroll;
using RestSharp.Validation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Threading;

namespace POMSeleniumFrameworkPoc1.Pages.UI.RBSP_BP
{
    public partial class CommonPage : BasePage
    {
        By consequentialsHereditamentBy = By.CssSelector("[data-id=\"voa_hereditament.fieldControl-LookupResultsDropdown_voa_hereditament_selected_tag_text\"]");
        By consequentialsJobTypeBy = By.CssSelector("[data-id=\"voa_consequentialjobtype.fieldControl-LookupResultsDropdown_voa_consequentialjobtype_selected_tag_text\"]");
        By succedingRequestBy = By.CssSelector("[data-id=\"voa_succeedingrequest.fieldControl-LookupResultsDropdown_voa_succeedingrequest_selected_tag_text\"]");
        public void ClickOnTabCommandBar(string tabNameCommandbar, string commandBarOption)
        {
            IWebElement commandOptionToClick = TabCommandBarItem(tabNameCommandbar, commandBarOption);
            Assert.IsTrue(SeleniumExtensions.IsElementVisible(commandOptionToClick), "The element is not visible yet");

            commandOptionToClick.ClickUsingJavascript();

        }

        public void SelectToggle(IWebElement toggleTextEle, IWebElement toggleElement, string toggleValue)
        {
            try
            {

                string actValue = toggleTextEle.Text;
                if (actValue != toggleValue)
                {
                    toggleElement.ClickUsingJavascript();
                }
                else
                {
                    Console.WriteLine("The value is already set to " + toggleValue);
                }
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

        public void ValidateFieldsInSection(string sectionName, string fieldName)
        {
            IWebElement fieldLabel = FieldLabelInSection(sectionName, fieldName);
            fieldLabel.ElementVisisbleUsingExplicitWait(10);
            Assert.IsTrue(fieldLabel.ValidateElementPresentOnUI());
        }

        public void ValidatetabsInSection(string sectionName, string tabName)
        {
            try
            {
                IWebElement tabNameEle = TabNameInSection(sectionName, tabName);
                tabNameEle.ElementVisisbleUsingExplicitWait(10);
                Assert.IsTrue(tabNameEle.ValidateElementPresentOnUI());
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

        public void ValidateLinksInSection(string sectionName, string linkName)
        {
            try
            {
                IWebElement tabNameEle = LinkNameInSection(sectionName, linkName);
                tabNameEle.ElementVisisbleUsingExplicitWait(10);
                Assert.IsTrue(tabNameEle.ValidateElementPresentOnUI());
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

        private IWebElement OptionsFromRelatedTab(String optionToSelect)
        {
            string customizeSelector = $"[aria-label = '{optionToSelect} Related - Common']";
            return Driver.WaitForElement(By.CssSelector(customizeSelector));
        }

        public void EnterFieldValuesInSection(string sectionName, string fieldValue, string fieldName, string fieldType)
        {
            IWebElement ele = null;
            switch (fieldType)
            {
                case "input":
                    ele = Driver.WaitForElement(By.CssSelector($"section[aria-label= '{sectionName}'] input[aria-label='{fieldName}']"));
                    ele.ClearAndEnterValueIntoTextBox(fieldValue);
                    break;
                case "select":
                    ele = Driver.WaitForElement(By.CssSelector($"section[aria-label= '{sectionName}'] select[aria-label='{fieldName}']"));
                    ele.SelectElementByText(fieldValue);
                    break;
                case "textrea":
                    ele = Driver.WaitForElement(By.CssSelector($"section[aria-label= '{sectionName}'] textarea[aria-label='{fieldName}']"));
                    ele.ClearAndEnterValueIntoTextBox(fieldValue);
                    break;
                case "lookup":
                    By lookupLoc = By.CssSelector($"section[aria-label= '{sectionName}'] input[aria-label='{fieldName}, Lookup']");
                    By SearchButton = By.XPath($"//section[@aria-label= '{sectionName}']//input[@aria-label='{fieldName}, Lookup']//following-sibling::button[@title='Search']");
                    By lookUpFirstValue = GetFirstLookUp(fieldValue);
                    SeleniumExtensions.EnterInLookUpField(lookUpFirstValue, lookupLoc, SearchButton, fieldValue);
                    break;
                case "toggle":
                    IWebElement toggleText = Driver.WaitForElement(By.XPath($"//section[@aria-label= '{sectionName}']//button[contains(@aria-label,'{fieldName}')]/following-sibling::label"));
                    ele = Driver.WaitForElement(By.XPath($"//section[@aria-label= '{sectionName}']//button[contains(@aria-label,'{fieldName}')]"));
                    SelectToggle(toggleText, ele, fieldValue);
                    break;
            }
        }

        public void FilterViewSelectorWithValue(string displayFilterValue)
        {
            try
            {
                ViewSelector.ElementVisisbleUsingExplicitWait(10);
                ViewSelector.ClickUsingJavascript();
                IWebElement valueToSelect = ViewValue(displayFilterValue);
                // Assert.IsTrue(valueToSelect.ElementVisisbleUsingExplicitWait(5));
                valueToSelect.ClickUsingJavascript();
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

        public void Filterestateteam(string dropdownvalue, string dropdownvalue_sub)
        {
            try
            {
                waitHelpers wh = new waitHelpers();
                By customerfilter = By.XPath($"//*[@aria-label='{dropdownvalue}']");
                wh.isElementDisplayed(customerfilter, 60);
                wh.jsClickOnElement(customerfilter);
                By estateteam = By.XPath($"//*[@aria-label='{dropdownvalue_sub}']");
                wh.isElementDisplayed(estateteam, 60);
                wh.jsClickOnElement(estateteam);
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


        public void ValidateActivityStatus(By rowElemment, string columnName, string expStatusCode)
        {
            try
            {
                string LatestDateTimeValue = SeleniumExtensions.GetLatestRowByDate(rowElemment, columnName);
                // Console.WriteLine(LatestDateTimeValue);
                IWebElement ActivityStatusValue = ActivityStatus(LatestDateTimeValue);
                string actStatusCode = ActivityStatusValue.Text;
                Assert.AreEqual(expStatusCode, actStatusCode);
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

        public void ValidateCorrespondenceLinkAndClick()
        {
            try
            {
                Assert.IsTrue(OutboundCorrespondenceLink.ElementVisisbleUsingExplicitWait(10), "The correspondence is not generated");
                OutboundCorrespondenceLink.ClickUsingJavascript();
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

        public void NoactionLinkAndClick()
        {
            try
            {
                Assert.IsTrue(NoactionLink.ElementVisisbleUsingExplicitWait(10), "No Action is  not generated");
                NoactionLink.ClickUsingJavascript();
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

        public bool NoactionLinkAndValidate()
        {
            try
            {
                if (NoactionLink.ElementVisisbleUsingExplicitWait(10))
                {
                    // Link is present — this is a failure condition
                    Assert.Fail("No action generated for proposal — but link is present, which is unexpected.");
                    return false; // This line won't be reached due to Assert.Fail
                }
                else
                {
                    // Link is not displayed — expected condition
                    return true;
                }
            }
            catch (NoSuchElementException)
            {
                // Element not found — treat as not displayed
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error while checking No Action link: {ex.Message}");
                Assert.Fail("Unexpected error occurred while validating No Action link.");
                return false;
            }
        }


        public void ValidateCorresspondencePDF()
        {
            try
            {
                Assert.IsTrue(OCStatusReasondropdown.ElementVisisbleUsingExplicitWait(10), "The Status reason is not displayed yet");
                //     OCStatusReasondropdown.ClickUsingJavascript();
                //   OCStatusReasonReady.ClickUsingJavascript();
                SeleniumExtensions.SelectDropdownValue(OCStatusReasondropdown, "Ready");

                string statusReasonText = OCStatusReasonText.Text;
                while (statusReasonText != "Sent")
                {
                    ClickCommandBarOption("Refresh");
                    statusReasonText = OCStatusReasonText.Text;
                    if (statusReasonText == "Sent")
                    {
                        Assert.IsTrue(CorrespondencePDFLink.ElementVisisbleUsingExplicitWait(10), "The correspondence pdf is not generated");
                        string pdfName = CorrespondencePDFLink.GetAttribute("title");
                        if (pdfName.Contains(".pdf"))
                        {
                            Assert.IsTrue(true);
                        }
                        else
                        {
                            Assert.IsTrue(false, "The pdf is not generated");
                        }
                    }
                    else if (statusReasonText.Contains("Failed"))
                    {
                        Assert.Fail("There was an error generating the correspondence and it failed with status reason " + statusReasonText);
                    }

                }
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

        public void ValidateCorresspondenceMessage()
        {
            try
            {
                Assert.IsTrue(OCStatusReasondropdown.ElementVisisbleUsingExplicitWait(10), "The Status reason is not displayed yet");
                string statusReasonText = OCStatusReasonText.Text;

                //OCStatusReasondropdown.ClickUsingJavascript();
                //OCStatusReasonReady.ClickUsingJavascript();
                //  string statusReasonText = OCStatusReasonText.Text;
                if (statusReasonText != "Processing")
                {
                    //   OCStatusReasondropdown.ClickUsingJavascript();
                    // OCStatusReasonReady.ClickUsingJavascript();
                    SeleniumExtensions.SelectDropdownValue(OCStatusReasondropdown, "Ready");

                    ClickCommandBarOption("Save");
                    while (statusReasonText != "Processing")
                    {
                        ClickCommandBarOption("Refresh");
                        statusReasonText = OCStatusReasonText.Text;
                        if (statusReasonText == "Processing")
                        {
                            ClickOnOptionInRelatedTabOnForm("Integration Messages", "Related", "Outbound Correspondence Form");
                            ValidateAndClickOnTab("Integration Messages", "Outbound Correspondence Form");
                            //  ValidateAutoProcessingRuntimeStatus("Completed");
                            ValidateCorrespondenceIntegrationMessageStatus("Sent");

                        }
                        else if (statusReasonText.Contains("Failed"))
                        {
                            Assert.Fail("There was an error generating the correspondence and it failed with status reason " + statusReasonText);
                        }

                    }
                }
                else
                {
                    ClickOnOptionInRelatedTabOnForm("Integration Messages", "Related", "Outbound Correspondence Form");
                    ValidateAndClickOnTab("Integration Messages", "Outbound Correspondence Form");
                    ValidateCorrespondenceIntegrationMessageStatus("Sent");
                }
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

        public void ClickOnBacKButton()
        {
            try
            {
                GoBackLink.ClickUsingJavascript();
                SelectOverflowOption("New Request");
                if (OKBtn.IsElementDisplayed(3))
                {
                    OKBtn.ClickUsingJavascript();
                    Thread.Sleep(3000);

                }
                else
                {

                    Console.WriteLine("Ok button is not displayed");
                }
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

        public void EnterMandatoryDataInSubmissionForm(string IDSVal, string submittedByVal, string relationshipRoleVal)
        {
            try
            {
                FillAndSelectLookUpResult(IntegrationDataSourcLookupInput, IDSVal);
                FillAndSelectLookUpResult(SubmittedbyLookupInputSelector, submittedByVal);
                FillAndSelectLookUpResult(RelationShipRoleLookUp, relationshipRoleVal);
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
        public void SelectOverflowOption(string optionValue)
        {
            try
            {

                if (NewRequestBtnUndrReltdReqSec.IsElementVisibleUsingByLocator(2))
                {
                    NewRequestBtnUnderRelatedRequestSection.ClickUsingJavascript();
                }
                else
                {
                    MoreCommandsRelatedRequestPrev.ClickUsingJavascript();
                    IWebElement OptionToSelect = CommandOptionRelatedRequest(optionValue);
                    OptionToSelect.ElementVisisbleUsingExplicitWait(2);
                    OptionToSelect.ClickUsingJavascript();
                }
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

        public void SelectOverflowOptionValue(string optionValue)
        {
            waitHelpers wh = new waitHelpers();
            wh.clickOnElement(moreCommandsRelatedRequestBy);
            By OptionToSelectSelector = SeleniumExtensions.getLocator($"[data-id ='OverflowFlyout'] li[id*= 'voa_requestlineitem'] button[aria-label *= '{optionValue}']");
            wh.clickOnElement(OptionToSelectSelector);
        }

        public void clickOnFindPlotBtn()
        {
            waitHelpers wh = new waitHelpers();
            wh.clickOnElement(By.CssSelector("div[class='findPlotButtonContainer'] button"));
        }

        public void clickOnresetBtn(string buttonname)
        {
            waitHelpers wh = new waitHelpers();
            wh.clickOnElement(By.XPath($"//*[@data-automation-key='{buttonname}']"));
        }

        public void selectJobMenuBarOption(string optionValue)
        {


        }
        public void CreateRequestThroughSubmission(Dictionary<string, string> testdata)
        {

            // var ExpectedBARefNO = ExpProposedBARefValue.GetAttribute("title").ToString().Trim();
            DateTime expDate = DateTime.ParseExact("03/09/2024", "dd/MM/yyyy", null);
            var ExpectedBARefNO = expDate.ToString("M/d/yyyy");
            //  Assert.AreEqual(BARefVal, ExpectedBARefNO, $"The BA Reference number is {BARefVal}");

            NavigateToMenuItem("Service", "Submissions");
            ClickCommandBarOption("New");
            EnterMandatoryDataInSubmissionForm(testdata["IDS"], testdata["SubmittedBy"], testdata["RelationshipRole"]);
            //  ClickCommandBarOption("Save");
            //  SelectOverflowOption("New Request");
            clickOnMainMenuMoreElement_New("Save");
            SelectOverflowOptionValue("New Request");
        }

        public void NavigateToNewCustomerEnquiryPage()
        {
            NavigateToMenuItem("Service", "Customer Enquiries");
            ClickCommandBarOption("New");

        }

        public void ValidateSaveInProgressDialog()
        {
            if (SaveInProgressError.ElementVisisbleUsingExplicitWait(5))
            {
                SaveInProgressOKBtn.ClickUsingJavascript();
                ClickCommandBarOption("Save");
            }

        }

        public Dictionary<string, string> GetDBData(string propertyKey)
        {
            var dbTestData = SeleniumExtensions.GetDBResult(propertyKey);

            return dbTestData;
        }

        public Dictionary<string, string> GetVOA_DBData(string propertyKey)
        {
            var dbTestData = SeleniumExtensions.GetDBResult(propertyKey);

            return dbTestData;
        }

        public Dictionary<string, string> GetDBData(string propertyKey, string queryParameter, FeatureContext fc)
        {
            var dbTestData = SeleniumExtensions.GetDBResultByModifiyingQuery(propertyKey, queryParameter, fc);

            return dbTestData;
        }

        public Dictionary<string, string> GetDBData(string propertyKey, string queryParameter, Dictionary<String, String> fc)
        {
            var dbTestData = SeleniumExtensions.GetDBResultByModifiyingQuery(propertyKey, queryParameter, fc);

            return dbTestData;
        }





        public void enterDataInFieldsBasedOnLabel(String fieldName, String sheetName, String RowID)
        {
            string TestDataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + Config.TestDataExcelFilePath);
            var excelUtility = new ExcelTestDataUtility(TestDataFilePath);
            string value = excelUtility.getFieldTestData(fieldName, sheetName, RowID);
            By selector = SeleniumExtensions.getLocator($"//*[normalize-space(text())='{fieldName}']/following-sibling::*//input");
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(120));
            wait.PollingInterval = TimeSpan.FromMilliseconds(200);
            wait.Until(driver => DriverHelper.Driver.FindElement(selector).Displayed);
            IWebElement ele = DriverHelper.Driver.FindElement(selector);
            ele.SendKeys(value);
        }



        public String getFeatureContextValue(String fieldName, FeatureContext fc)
        {
            String featureContextValue = "";
            try
            {
                switch (fieldName)
                {
                    case "Town/City":
                        featureContextValue = (String)fc["town"];
                        //"CARDIFF";
                        //
                        break;
                    case "Street":
                        featureContextValue = (String)fc["street"];
                        break;
                    case "Building Name/Number":
                        featureContextValue = (String)fc["building_number"];
                        break;
                    case "Postcode":
                        featureContextValue = (String)fc["postcode"];
                        //
                        break;
                    case "Postcode_new":
                        featureContextValue = (String)fc["postcode_new"];
                        //
                        break;
                    case "UPRN":
                        featureContextValue = (String)fc["uprn"];
                        break;
                        //
                    case "Proposed BA Reference Number":
                        featureContextValue = (String)fc["ba_reference"];
                        break;

                }
            }
            catch (Exception e)
            {
                featureContextValue = "";
            }

            return featureContextValue;

        }

        public void enterDataInFieldsBasedOnLabel(String fieldName, Dictionary<String, String> testData, FeatureContext fc)
        {
            string value = "";
            String contextValue = getFeatureContextValue(fieldName, fc);
            if (!String.IsNullOrEmpty(contextValue))
            {
                value = contextValue;
            }
            else
            {
                value = testData[fieldName];
            }

            By selector = SeleniumExtensions.getLocator($"//*[normalize-space(text())='{fieldName}']/following-sibling::*//input");
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(120));
            wait.PollingInterval = TimeSpan.FromMilliseconds(200);
            wait.Until(driver => DriverHelper.Driver.FindElement(selector).Displayed);
            IWebElement ele = DriverHelper.Driver.FindElement(selector);
            ele.SendKeys(value);
        }

        public void enterDataInPostCodeField_NewProperty(String fieldName, Dictionary<String, String> testData, FeatureContext fc)
        {
            string value = "";
            String contextValue = getFeatureContextValue(fieldName, fc);
            if (!String.IsNullOrEmpty(contextValue))
            {
                value = contextValue;
            }
            else
            {
                value = testData[fieldName];
            }

            By selector = By.CssSelector("[class*='ms-Modal-scrollableConten'] input[name='Address Search Input']");
            waitHelpers wh = new waitHelpers();
            IWebElement ele = wh.GetWebElement(selector);
            ele.SendKeys(value);
        }

        public void enterDataInFieldsBasedOnLabel(String fieldName, string fc_fieldName, Dictionary<String, String> testData, FeatureContext fc)
        {
            string value = "";
            String contextValue = getFeatureContextValue(fc_fieldName, fc);
            if (!String.IsNullOrEmpty(contextValue))
            {
                value = contextValue;
            }
            else
            {
                value = testData[fieldName];
            }

            By selector = SeleniumExtensions.getLocator($"//*[normalize-space(text())='{fieldName}']/following-sibling::*//input");
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(120));
            wait.PollingInterval = TimeSpan.FromMilliseconds(200);
            wait.Until(driver => DriverHelper.Driver.FindElement(selector).Displayed);
            IWebElement ele = DriverHelper.Driver.FindElement(selector);
            ele.SendKeys(value);
        }



        public void enterDataInFieldsBasedOnLabel(String fieldName, String value)
        {
            By selector = SeleniumExtensions.getLocator($"//*[normalize-space(text())='{fieldName}']/following-sibling::*//input");
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(120));
            Actions act = new Actions(DriverHelper.Driver);
            wait.PollingInterval = TimeSpan.FromMilliseconds(200);
            wait.Until(driver => DriverHelper.Driver.FindElement(selector).Displayed);
            IWebElement ele = DriverHelper.Driver.FindElement(selector);
            act.ScrollToElement(ele).Pause(TimeSpan.FromMilliseconds(500)).SendKeys(value).Build().Perform();
        }

        public bool assertStatus(String expectedStatus, String processStage)
        {
            waitHelpers wh = new waitHelpers();
            SeleniumExtensions.WaitForReadyStateToComplete();
            bool isStatusEqul = false;
            String locator = $"//div[@data-id='form-header']//div[contains(@id,'headerControlsList')]//*[normalize-space(text())='{expectedStatus}']";
            //$"//*[normalize-space(text())='{expectedStatus}']";
            By eleBy = SeleniumExtensions.getLocator(locator);
            String actualStatus = wh.GetWebElement(eleBy).Text.Trim();
            //SeleniumExtensions.waitUnitlElementToBeDisplayed(eleBy, 120).Text.Trim();
            if (expectedStatus.Equals(actualStatus))
            {
                isStatusEqul = true;
            }
            else
            {
                isStatusEqul = false;
            }
            return isStatusEqul;
        }

        public void searchbutton()
        {
            waitHelpers wh = new waitHelpers();
            By VoaBtn = By.XPath("(//span[@data-automationid='splitbuttonprimary'])[5]");
            wh.clickOnElement(VoaBtn);
        }

        public void milofilter(string dropdown, string filter)
        {
            waitHelpers wh = new waitHelpers();
            By itemsImWorkingBtn = By.CssSelector($"button[aria-label='{dropdown}']");
            By allItems = By.XPath($"//*[text()='{filter}']/ancestor::button");
            //By allJobs = By.CssSelector($"div[aria-label = 'All Jobs']");
            wh.isElementDisplayed(itemsImWorkingBtn, 60);
            wh.jsClickOnElement(itemsImWorkingBtn);
            wh.isElementDisplayed(allItems, 60);
            wh.jsClickOnElement(allItems);
            By locator = SeleniumExtensions.getLocator("//*[@col-id='voa_doeracode' and not(contains(@class,'header'))]");
            wh.GetWebElement(locator);
            wh.isElementDisplayed(locator, 120);
            wh.jsClickOnElement(locator);
        }

        public void milofilter_new(string dropdown, string filter)
        {
            waitHelpers wh = new waitHelpers();
            By itemsImWorkingBtn = By.CssSelector($"button[aria-label='{dropdown}']");
            By allItems = By.XPath($"//*[text()='{filter}']/ancestor::button");
            //By allJobs = By.CssSelector($"div[aria-label = 'All Jobs']");
            wh.isElementDisplayed(itemsImWorkingBtn, 60);
            wh.jsClickOnElement(itemsImWorkingBtn);
            wh.isElementDisplayed(allItems, 60);
            wh.jsClickOnElement(allItems);
            //    By locator = SeleniumExtensions.getLocator("//*[@col-id='voa_doeracode' and not(contains(@class,'header'))]");
            //    wh.GetWebElement(locator);
            //    wh.isElementDisplayed(locator, 120);
            //    wh.jsClickOnElement(locator);
        }


        public void miloclicklinkhereditament(String buttonname)
        {
            waitHelpers wh = new waitHelpers();
            By btnNameLoc = By.CssSelector($"button[aria-label*='{buttonname}']");
            wh.GetWebElement(btnNameLoc);
            wh.isElementDisplayed(btnNameLoc, 120);
            wh.jsClickOnElement(btnNameLoc);
        }
        public void filtermilorecord(String coloumName, String filterValue)
        {
            waitHelpers wh = new waitHelpers();
            //By itemsImWorkingBtn = By.CssSelector("button[aria-label='Active MILO Transactions']");
            //By allItems = By.XPath("//*[text()='Linked MILO Transactions']/ancestor::button");
            By filterColoumn = By.XPath($"//*[@data-testid='columnHeader']//label/div[text()='{coloumName}']");
            By filterBy = By.XPath("//ul[contains(@class,'ms-ContextualMenu-list')]/li/button[@name='Filter by']");
            By filterByValue1 = By.CssSelector($"input[aria-label='Filter by value']");
            By applyBtn = By.XPath("//button//*[text()='Apply']");
            By Textfieldvalue = By.XPath("//*[@data - automationid = 'columnHeader']");
            //By jobCheckJob = By.XPath($"//*[text()=\"" + filterValue + "\"]/ancestor::div[@aria-label='Press SPACE to select this row.']/div[@col-id='__row_status']//div[contains(@class,'ms-Checkbox')]");
            //By btnNameLoc = By.CssSelector($"button[aria-label*='{btnName}']");
            //By dialogBtnName = By.XPath($"//div[contains(@id,'dialogContentContainer')]//button[@title='{btnName}']");
            //By jobId = By.XPath("//*[@col-id='ticketnumber' and not(contains(@class,'header'))]");
            //By allJobs = By.CssSelector($"div[aria-label = 'All Jobs']");
            string loading = "[id= 'appProgressIndicatorContainer']";
            string processingMsg = "[id= 'appProgressIndicatorMessage']";
            //wh.isElementDisplayed(itemsImWorkingBtn, 60);
            // wh.jsClickOnElement(itemsImWorkingBtn);
            //wh.isElementDisplayed(allItems, 60);
            //wh.jsClickOnElement(allItems);
            //wh.GetWebElementVisble(allJobs);
            if (Config.EnvironmentVal == "DEV")
            {
                filterColoumn = By.XPath($"//*[@data-testid='columnHeader']//label/div[text()='Job ID']");
                wh.clickOnElement(filterColoumn);
            }
            else
            {
                wh.clickOnElement(filterColoumn);
            }
            wh.clickOnElement(filterBy);
            wh.GetWebElement(filterByValue1).SendKeys(filterValue);
            Thread.Sleep(1000);
            //wh.clickOnElement(Textfieldvalue);
            wh.clickOnElement(applyBtn);
            //wh.clickOnElement(jobCheckJob);
        }
        public void assignJobFromAllJobs_abort(String coloumName, String filterValue, String btnName)
        {
            waitHelpers wh = new waitHelpers();
            By itemsImWorkingBtn = By.CssSelector("button[aria-label='My Active Jobs']");
            By allItems = By.XPath("//*[text()='All Jobs']/ancestor::button");
            By filterColoumn = By.XPath($"//*[@data-testid='columnHeader']//label/div[text()='{coloumName}']");
            By filterBy = By.XPath("//ul[contains(@class,'ms-ContextualMenu-list')]/li/button[@name='Filter by']");
            By filterByValue1 = By.CssSelector($"input[aria-label='Filter by value']");
            By applyBtn = By.XPath("//button//*[text()='Apply']");
            By jobCheckJob = By.XPath($"//*[text()=\"" + filterValue + "\"]/ancestor::div[@aria-label='Press SPACE to select this row.']/div[@col-id='__row_status']//div[contains(@class,'ms-Checkbox')]");
            By btnNameLoc = By.CssSelector($"button[aria-label*='{btnName}']");
            By dialogBtnName = By.XPath($"//div[contains(@id,'dialogContentContainer')]//button[@title='{btnName}']");
            By jobId = By.XPath("//*[@col-id='ticketnumber' and not(contains(@class,'header'))]");
            By allJobs = By.CssSelector($"div[aria-label = 'All Jobs']");
            string loading = "[id= 'appProgressIndicatorContainer']";
            string processingMsg = "[id= 'appProgressIndicatorMessage']";
            By processingMsgLoc = By.CssSelector(processingMsg);
            wh.isElementDisplayed(itemsImWorkingBtn, 60);
            wh.jsClickOnElement(itemsImWorkingBtn);
            wh.isElementDisplayed(allItems, 60);
            wh.jsClickOnElement(allItems);
            wh.GetWebElementVisble(allJobs);
            filterColoumn = By.XPath($"//*[@data-testid='columnHeader']//label/div[text()='Job ID']");
            wh.clickOnElement(filterColoumn);

            wh.clickOnElement(filterBy);
            wh.GetWebElement(filterByValue1).SendKeys(filterValue);
            wh.clickOnElement(applyBtn);
            wh.clickOnElement(jobCheckJob);

            if (btnName.Equals("Assign"))
            {
                wh.isElementDisplayed(btnNameLoc, 60);
                wh.clickOnElement(btnNameLoc);
                wh.isElementDisplayed(dialogBtnName, 60);
                wh.clickOnElement(dialogBtnName);

                waitTillappProgressIndicatorDisaddpears_();

                SeleniumExtensions.waitUntillElementInvisible(processingMsgLoc);

            }
        }

        public void assignJobFromAllJobs(String coloumName, String filterValue, String btnName)
        {
            waitHelpers wh = new waitHelpers();
            By itemsImWorkingBtn = By.CssSelector("button[aria-label='My Active Jobs']");
            By allItems = By.XPath("//*[text()='All Jobs']/ancestor::button");
            By filterColoumn = By.XPath($"//*[@data-testid='columnHeader']//label/div[text()='{coloumName}']");
            By filterBy = By.XPath("//ul[contains(@class,'ms-ContextualMenu-list')]/li/button[@name='Filter by']");
            By filterByValue1 = By.CssSelector($"input[aria-label='Filter by value']");
            By applyBtn = By.XPath("//button//*[text()='Apply']");
            By jobCheckJob = By.XPath($"//*[text()=\"" + filterValue + "\"]/ancestor::div[@aria-label='Press SPACE to select this row.']/div[@col-id='__row_status']//div[contains(@class,'ms-Checkbox')]");
            By btnNameLoc = By.CssSelector($"button[aria-label*='{btnName}']");
            By dialogBtnName = By.XPath($"//div[contains(@id,'dialogContentContainer')]//button[@title='{btnName}']");
            By jobId = By.XPath("//*[@col-id='ticketnumber' and not(contains(@class,'header'))]");
            By allJobs = By.CssSelector($"div[aria-label = 'All Jobs']");
            string loading = "[id= 'appProgressIndicatorContainer']";
            string processingMsg = "[id= 'appProgressIndicatorMessage']";
            By processingMsgLoc = By.CssSelector(processingMsg);
            wh.isElementDisplayed(itemsImWorkingBtn, 60);
            wh.jsClickOnElement(itemsImWorkingBtn);
            wh.isElementDisplayed(allItems, 60);
            wh.jsClickOnElement(allItems);
            wh.GetWebElementVisble(allJobs);
            if (Config.EnvironmentVal == "DEV")
            {
                filterColoumn = By.XPath($"//*[@data-testid='columnHeader']//label/div[text()='Job ID']");
                wh.clickOnElement(filterColoumn);
            }
            else
            {
                wh.clickOnElement(filterColoumn);
            }
            
            wh.clickOnElement(filterBy);
            wh.GetWebElement(filterByValue1).SendKeys(filterValue);
            wh.clickOnElement(applyBtn);
            wh.clickOnElement(jobCheckJob);

            if (btnName.Equals("Assign"))
            {
                wh.isElementDisplayed(btnNameLoc, 60);
                wh.clickOnElement(btnNameLoc);
                wh.isElementDisplayed(dialogBtnName, 60);
                wh.clickOnElement(dialogBtnName);
                //waitTillappProgressIndicatorDisaddpears();
                waitTillappProgressIndicatorDisaddpears_();
                //waitTillEleDisappears(loading);
                SeleniumExtensions.waitUntillElementInvisible(processingMsgLoc);
                //waitTillEleDisappears(processingMsg);

            }

            //wh.isElementDisplayed(jobId, 90);
            //wh.elementToClickble(jobId);
            //wh.GetWebElement(jobId);
            //wh.clickOnElement(jobId);

        }

        public void openRequestFromCTrequest(String filterValue, String coloumName, String btnName)
        {
            waitHelpers wh = new waitHelpers();
            By itemsImWorkingBtn = By.CssSelector("button[aria-label='Active Requests']");
            By allItems = By.XPath("//*[text()='CT Requests']/ancestor::button");
            By filterColoumn = By.XPath($"//*[@data-testid='columnHeader']//label/div[text()='{coloumName}']");
            By filterBy = By.XPath("//ul[contains(@class,'ms-ContextualMenu-list')]/li/button[@name='Filter by']");
            By filterByValue1 = By.CssSelector($"input[aria-label='Filter by value']");
            By applyBtn = By.XPath("//button//*[text()='Apply']");
            By jobCheckJob = By.XPath($"//span[text()='{filterValue}']/ancestor::div[@aria-label='Press SPACE to select this row.']/div[@col-id='__row_status']//div[contains(@class,'ms-Checkbox')]");
            By allJobs = By.CssSelector($"button[aria-label = 'CT Requests']");
            string loading = "[id= 'appProgressIndicatorContainer']";
            string processingMsg = "[id= 'appProgressIndicatorMessage']";
            wh.isElementDisplayed(itemsImWorkingBtn, 60);
            wh.jsClickOnElement(itemsImWorkingBtn);
            wh.isElementDisplayed(allItems, 60);
            wh.jsClickOnElement(allItems);
            wh.GetWebElementVisble(allJobs);
            wh.clickOnElement(filterColoumn);
            wh.clickOnElement(filterBy);
            wh.GetWebElement(filterByValue1).SendKeys(filterValue);
            wh.clickOnElement(applyBtn);
            wh.clickOnElement(jobCheckJob);
            waitTillappProgressIndicatorDisaddpears();
            waitTillEleDisappears(processingMsg);
        }


        public void childjobstatus(string columnName, string jobName, string expectedStatus)
        {
            waitHelpers wh = new waitHelpers();
            By statusLocator = By.CssSelector($"label[aria-label='{expectedStatus}']");
            IWebElement statusElement = wh.getElement(statusLocator, 60);
            string actualStatus = statusElement.Text.Trim();
            Assert.AreEqual("Resolved", actualStatus, $"Job '{jobName}' is not in Resolved status. Found: {actualStatus}");
        }

        public void filterJobFromAllJobs(String coloumName, String filterValue)
        {
            waitHelpers wh = new waitHelpers();
            By itemsImWorkingBtn = By.CssSelector("button[aria-label='My Active Jobs']");
            By allItems = By.XPath("//*[text()='All Jobs']/ancestor::button");
            By filterColoumn = By.XPath($"//*[@data-testid='columnHeader']//label/div[text()='{coloumName}']");
            By filterBy = By.XPath("//ul[contains(@class,'ms-ContextualMenu-list')]/li/button[@name='Filter by']");
            By filterByValue1 = By.CssSelector($"input[aria-label='Filter by value']");
            By applyBtn = By.XPath("//button//*[text()='Apply']");
            By jobCheckJob = By.XPath($"//span[text()='{filterValue}']/ancestor::div[@aria-label='Press SPACE to select this row.']/div[@col-id='__row_status']//div[contains(@class,'ms-Checkbox')]");
            By jobId = By.XPath("//*[@col-id='ticketnumber' and not(contains(@class,'header'))]");
            By allJobs = By.CssSelector($"div[aria-label = 'All Jobs']");
            string loading = "[id= 'appProgressIndicatorContainer']";
            string processingMsg = "[id= 'appProgressIndicatorMessage']";
            wh.isElementDisplayed(itemsImWorkingBtn, 60);
            wh.jsClickOnElement(itemsImWorkingBtn);
            wh.isElementDisplayed(allItems, 60);
            wh.jsClickOnElement(allItems);
            wh.GetWebElementVisble(allJobs);
            wh.clickOnElement(filterColoumn);
            wh.clickOnElement(filterBy);
            wh.GetWebElement(filterByValue1).SendKeys(filterValue);
            wh.clickOnElement(applyBtn);
            wh.clickOnElement(jobCheckJob);
        }



        public void pickJob(String coloumName, String filterValue, String btnName)
        {
            waitHelpers wh = new waitHelpers();
            By itemsImWorkingBtn = By.CssSelector("button[aria-label='Items I am working on']");
            By allItems = By.XPath("//*[text()='All Items']/ancestor::button");
            By selectQueueInput = By.CssSelector("input[aria-label='Select a queue filter']");//.Clear.sendkeys("All Queues")
            By SelectQueueDiv = By.CssSelector("div[aria-label='Select a queue filter']");
            By filterColoumn = By.XPath($"//*[@data-testid='columnHeader']//label/div[text()='{coloumName}']");
            By filterBy = By.XPath("//ul[contains(@class,'ms-ContextualMenu-list')]/li/button[@name='Filter by']");
            By filterByValue = By.XPath("//*[@role='alertdialog' and contains(@aria-label,'Filter')]//*[@aria-label='Filter by value']");
            By filterByValue1 = By.CssSelector($"input[aria-label='Filter by value']");
            By applyBtn = By.XPath("//button//*[text()='Apply']");
            By AllQueues = By.CssSelector("li[data-text='All Queues']");
            By jobCheckJob = By.XPath($"//span[text()='{filterValue}']/ancestor::div[@aria-label='Press SPACE to select this row.']/div[@col-id='__row_status']//input[@type='checkbox']");
            By btnNameLoc = By.CssSelector($"button[aria-label*='{btnName}']");
            By dialogBtnName = By.XPath($"//div[contains(@id,'dialogContentContainer')]//button[@title='{btnName}']");
            wh.jsClickOnElement(itemsImWorkingBtn);
            wh.jsClickOnElement(allItems);
            wh.jsClickOnElement(SelectQueueDiv);
            wh.jsClickOnElement(AllQueues);

            //wh.GetWebElement(By.CssSelector(selectQueue)).SendKeys(Keys.Control + "a");
            //wh.GetWebElement(By.CssSelector(selectQueue)).SendKeys(Keys.Backspace);
            //wh.GetWebElement(By.CssSelector(selectQueue)).SendKeys("All Queues");
            wh.clickOnElement(filterColoumn);
            wh.clickOnElement(filterBy);
            wh.GetWebElement(filterByValue1).SendKeys(filterValue);
            wh.clickOnElement(applyBtn);
            wh.clickOnElement(jobCheckJob);
            if (btnName.Equals("Pick"))
            {
                wh.clickOnElement(btnNameLoc);
                wh.clickOnElement(dialogBtnName);
            }

            if (btnName.Equals("Assign"))
            {
                wh.clickOnElement(btnNameLoc);


            }


        }




        public void clickButtonOnDialog(String buttonName)
        {
            waitHelpers wh = new waitHelpers();
            String locator = $"//*[@role='dialog']//*[normalize-space(text())='{buttonName}']/ancestor::button";
            By eleBy = SeleniumExtensions.getLocator(locator);
            wh.clickOnElement(eleBy);
            //SeleniumExtensions.waitForElementToBeDisplayed(eleBy, 120).Click();
        }

        public void waitTillappProgressIndicatorDisaddpears()
        {
            waitHelpers wh = new waitHelpers();
            String locator = $"[id='appProgressIndicatorContent']";
            By eleBy = SeleniumExtensions.getLocator(locator);
            //wh.waitTillElementInvisible(eleBy);
            bool isEleDisplayed = false;
            int i = 0;
            do
            {
                i = i + 1;
                isEleDisplayed = wh.isElementDisplayed(eleBy, 10);
                if (!isEleDisplayed || i == 50)
                    break;
            } while (isEleDisplayed);
        }

        public void waitTillappProgressIndicatorDisaddpears_()
        {
            waitHelpers wh = new waitHelpers();
            String loadingSec = $"section[id = 'appProgressIndicatorView_popupContainer']";
            By loadingSecBy = SeleniumExtensions.getLocator(loadingSec);
            String locator = $"div[id='global-app-progress-indicator']";
            String loadingLoc = $"div[id='appProgressIndicatorContainer']";
            By eleBy = SeleniumExtensions.getLocator(locator);
            //string loading = "[id= 'appProgressIndicatorContainer']";
            string processingMsg = "[id='appProgressIndicatorMessage']";
            By processingMsgBy = SeleniumExtensions.getLocator(processingMsg);
            By loadingLocBy = SeleniumExtensions.getLocator(loadingLoc);
            string loadingSpinner = "//div[contains(@class,'ms-Modal-scrollableContent scrollableContent')]//div[contains(@class,'ms-Spinner-circle ms-Spinner')]";
            By loadingSpinnerBy = SeleniumExtensions.getLocator(loadingSpinner);
            //"[id= 'appProgressIndicatorMessage']";
            if (wh.isElementDisplayed(loadingSpinnerBy, 1))
            {
                SeleniumExtensions.waitUntillElementInvisible(loadingSpinnerBy);
            }
            if (wh.isElementDisplayed(eleBy, 1))
            {
                SeleniumExtensions.waitUntillElementInvisible(eleBy);
            }
            if (wh.isElementDisplayed(loadingLocBy, 1))
            {
                SeleniumExtensions.waitUntillElementInvisible(loadingLocBy);
            }
            if (wh.isElementDisplayed(processingMsgBy, 1))
            {
                SeleniumExtensions.waitUntillElementInvisible(processingMsgBy);
            }
            if (wh.isElementDisplayed(loadingSecBy, 1))
            {
                SeleniumExtensions.waitUntillElementInvisible(loadingSecBy);
            }
        }

        public void waitTillunlockstageProgressIndicatorDisaddpears(String labelName)
        {
            waitHelpers wh = new waitHelpers();
            String locator = $"//div[contains(@class,'ms-Spinner-label') and contains(text(),'{labelName}')]";
            By eleBy = SeleniumExtensions.getLocator(locator);
            bool isEleDisplayed = false;
            if (wh.isElementDisplayed(eleBy, 1))
            {
                SeleniumExtensions.waitUntillElementInvisible(eleBy);
            }
            //int i = 0;
            //do
            //{
            //    i = i + 1;
            //    isEleDisplayed = wh.isElementDisplayed(eleBy, 10);
            //    if (!isEleDisplayed || i == 50)
            //        break;
            //} while (isEleDisplayed);
        }


        public void waitTillEleDisappears(String locator)
        {
            waitHelpers wh = new waitHelpers();
            By eleBy = SeleniumExtensions.getLocator(locator);
            //wh.waitTillElementInvisible(eleBy);
            bool isEleDisplayed = false;
            int i = 0;
            do
            {
                i = i + 1;
                isEleDisplayed = wh.isElementDisplayedInMilliSec(eleBy, 5);
                if (!isEleDisplayed || i == 70)
                    break;
            } while (isEleDisplayed);

        }



        public void waitTillSpinnerDisable()
        {
            waitHelpers wh = new waitHelpers();
            By progressLocator = By.XPath($"//div[contains(@class,'Spinner')]");
            //[class*="ms-Spinner-circle ms-Spinner--large circle"]
            wh.waitTillElementInvisible(progressLocator);
        }

        public void waitTillSavingDisaddpears_new(String labelName, String roleType)
        {
            waitHelpers wh = new waitHelpers();

            By spinner = By.CssSelector("div[role*='progressbar'][aria-live='assertive']");
            By savedLocator = SeleniumExtensions.getLocator("//h1/span[contains(text(),'Saved')]");

            int spinnerTimeoutSec = 30;
            int savedTimeoutSec = 30;

            // 1) Wait UNTIL spinner disappears  
            int s1 = 0;
            while (s1 < spinnerTimeoutSec)
            {
                bool visible = wh.isElementDisplayed(spinner, 1);  // QUICK probe, 1sec
                if (!visible) break;
                s1++;
            }

            // 2) Wait UNTIL Saved appears  
            int s2 = 0;
            while (s2 < savedTimeoutSec)
            {
                bool savedVisible = wh.isElementDisplayed(savedLocator, 1);
                if (savedVisible) break;
                s2++;
            }
        }

        public void waitTillSavingDisaddpears(String labelName, String roleType)
        {

            By eleBy = By.CssSelector($"div[role*='progressbar'][aria-live='assertive']");
            By savedLocator = SeleniumExtensions.getLocator($"//h1/span[contains(text(),'Saved')]");
            waitHelpers wh = new waitHelpers();
            bool isEleDisplayed = false;
            bool isSavedDisplayed = false;
            int counter = 0;

            do
            {
                isEleDisplayed = wh.isElementDisplayed(eleBy, 1);
                if (!isEleDisplayed)
                    break;
            } while (isEleDisplayed);
            do
            {
                isSavedDisplayed = wh.isElementDisplayed(savedLocator, 1);
                if (isSavedDisplayed || counter == 25)
                    break;
                counter = counter + 1;
            } while (isSavedDisplayed);

        }

        public void waitTillSavingDisaddpears()
        {

            By eleBy = By.CssSelector($"div[role*='progressbar'][aria-live='assertive']");
            By savedLocator = SeleniumExtensions.getLocator($"//h1/span[contains(text(),'Saved')]");
            waitHelpers wh = new waitHelpers();
            bool isEleDisplayed = false;
            bool isSavedDisplayed = false;
            int counter1 = 0;
            int counter2 = 0;

            SeleniumExtensions.waitUntillElementInvisible(eleBy);
            SeleniumExtensions.isElementDisplayed_New(savedLocator);

            //do
            //{
            //    isEleDisplayed = wh.isElementDisplayedInMilliSec(eleBy, 10);
            //    if (!isEleDisplayed || counter1 == 50)
            //        break;
            //    counter1 = counter1 + 1;

            //} while (isEleDisplayed || counter1 == 50);
            //do
            //{
            //    isSavedDisplayed = wh.isElementDisplayedInMilliSec(savedLocator, 10);
            //    if (isSavedDisplayed || counter2 == 50)
            //        break;
            //    counter2 = counter2 + 1;
            //} while (isSavedDisplayed || counter2 == 50);

        }

        public void waitTillSpinnerDisaddpears()
        {
            By eleBy = By.CssSelector($"div[id='quick-create-spinner']");
            By savingBy = By.CssSelector($"[aria-label='Saving...']");

            SeleniumExtensions.waitUntillElementInvisible(eleBy);
            SeleniumExtensions.waitUntillElementInvisible(savingBy);

        }


        public void waitTillSavingDisaddpears_Old()
        {

            By eleBy = By.CssSelector($"div[role*='progressbar'][aria-live='assertive']");
            By savedLocator = SeleniumExtensions.getLocator($"//h1/span[contains(text(),'Saved')]");
            waitHelpers wh = new waitHelpers();
            bool isEleDisplayed = false;
            bool isSavedDisplayed = false;
            int counter1 = 0;
            int counter2 = 0;

            do
            {
                isEleDisplayed = wh.isElementDisplayedInMilliSec(eleBy, 10);
                if (!isEleDisplayed || counter1 == 50)
                    break;
                counter1 = counter1 + 1;

            } while (isEleDisplayed || counter1 == 50);
            do
            {
                isSavedDisplayed = wh.isElementDisplayedInMilliSec(savedLocator, 10);
                if (isSavedDisplayed || counter2 == 50)
                    break;
                counter2 = counter2 + 1;
            } while (isSavedDisplayed || counter2 == 50);

        }

        public void waitTillSavingDisaddpearswithnocheck(String labelName, String roleType)
        {

            By eleBy = By.CssSelector($"div[role*='progressbar'][aria-live='assertive']");
            By savedLocator = SeleniumExtensions.getLocator($"//h1/span[contains(text(),'Saved')]");
            By nocheckLocator = By.XPath($"//*[contains(@id,'dialog')]//ul[contains(@data-lp-id,'commandbar')]//li//*[text()='Check Acceptance']//ancestor::button");
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(240));
            waitHelpers wh = new waitHelpers();
            bool isEleDisplayed = false;
            do
            {
                isEleDisplayed = wh.isElementDisplayed(eleBy, 5);
                if (!isEleDisplayed)
                    break;
            } while (isEleDisplayed);
            wh.GetWebElement(savedLocator);
            wh.waitTillElementInvisible(nocheckLocator, wait);
        }

        public void waitTillSavingDisaddpearsDialog(String labelName)
        {
            String locator = $"div[role*='progressbar'][aria-live='assertive']";
            By eleBy = SeleniumExtensions.getLocator(locator);
            By savedLocator = SeleniumExtensions.getLocator($"//h2/span[contains(text(),'Saved')]");
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(240));
            waitHelpers wh = new waitHelpers();
            wh.waitTillElementInvisible(eleBy, wait);
            wh.GetWebElementVisble(savedLocator, wait);
        }


        public void waitTillRecordSaved(String labelName)
        {
            By savedLocator = SeleniumExtensions.getLocator($"//h1/span[contains(text(),'Saved')]");
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(240));
            waitHelpers wh = new waitHelpers();
            wh.GetWebElementVisble(savedLocator, wait);
        }

        public void waitTillSavingOnlyDisaddpears(String labelName, String roleType)
        {

            //String locator = $"//*[@role='{roleType}' and text()='{labelName}'] | //*[text()='{labelName}']";
            String locator = $"//Label[contains(@id,'spinner') and text()='{labelName}']";
            By eleBy = SeleniumExtensions.getLocator(locator);
            //By savedLocator = SeleniumExtensions.getLocator($"//h1/span[contains(text(),'Saved')]");
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(240));
            wait.PollingInterval = TimeSpan.FromMilliseconds(1);
            waitHelpers wh = new waitHelpers();
            //wh.waitTillElementInvisible(eleBy, wait);
            bool isEleDisplayed = false;
            do
            {
                isEleDisplayed = wh.isElementDisplayed(eleBy, 2);
                if (!isEleDisplayed)
                    break;
            } while (isEleDisplayed);
        }

        public void waitTillSavingOnlyDisaddpearsMilliSec(String labelName, String roleType)
        {
            String locator = $"//Label[contains(@id,'spinner') and text()='{labelName}']";
            By eleBy = SeleniumExtensions.getLocator(locator);
            waitHelpers wh = new waitHelpers();
            bool isEleDisplayed = false;
            do
            {
                isEleDisplayed = wh.isElementDisplayedInMilliSec(eleBy, 2);
                if (!isEleDisplayed)
                    break;
            } while (isEleDisplayed);
        }

        public void waitTillSavedOnRecord(String labelName, String roleType)
        {

            String locator = $"//*[@role='{roleType}' and text()='{labelName}'] | //*[text()='{labelName}']";
            By eleBy = SeleniumExtensions.getLocator(locator);
            By savedLocator = SeleniumExtensions.getLocator($"//h1/span[contains(text(),'Saved')]");
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(240));
            wait.PollingInterval = TimeSpan.FromMilliseconds(10);
            waitHelpers wh = new waitHelpers();
            //wh.waitTillElementInvisible(eleBy, wait);
            wh.GetWebElementVisble(savedLocator);
        }



        public void waitTillSavingDisaddpears(String labelName, String roleType, String savedLabel)
        {

            String locator = $"//*[@role='{roleType}' and text()='{labelName}'] | //*[text()='{labelName}']";
            By eleBy = SeleniumExtensions.getLocator(locator);
            By savedLocator = SeleniumExtensions.getLocator($"//h1/span[contains(text(),'{savedLabel}')]");
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(240));
            wait.PollingInterval = TimeSpan.FromMilliseconds(10);
            waitHelpers wh = new waitHelpers();
            wh.waitTillElementInvisible(eleBy, wait);
            wh.GetWebElementVisble(savedLocator);
            //wait.Until(SeleniumExtensions.InvisibilityOfElementLocatedBy(eleBy));
            //wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(savedLocator));
        }

        public void waitTillCTBTLoadingDisaddpears()
        {
            waitHelpers wh = new waitHelpers();
            By locator = By.CssSelector("[id='dal-loading-message']");
            bool isEleAvailable = false;
            int i = 0;
            do
            {
                isEleAvailable = wh.isElementDisplayed(locator, 5);
                i = i + 1;
                if (i == 100) break;

            } while (isEleAvailable);
            //wh.waitTillElementInvisible(By.CssSelector("[class*='ms-Dialog-main main']"));
        }
        public void waitTillDialogDisaddpears(String roleType)
        {
            waitHelpers wh = new waitHelpers();
            String locator = $"//*[@role='{roleType}']";
            By eleBy = SeleniumExtensions.getLocator(locator);
            //WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(120));
            //wait.PollingInterval = TimeSpan.FromMilliseconds(1);
            //wait.Until(SeleniumExtensions.InvisibilityOfElementLocatedBy(eleBy));
            wh.waitTillElementInvisible(eleBy);
        }

        public void closeDialogIfPresent()
        {
            String locator = $"//button[contains(@id,'defaultDialogChromeCloseIconButton')]";
            By eleBy = SeleniumExtensions.getLocator(locator);
            waitHelpers wh = new waitHelpers();
            if (wh.isElementDisplayed(eleBy, 5))
            {
                wh.clickOnElement(eleBy);
            }
        }

        public void closeDialogWithTitle(string dialogTitle)
        {
            String locator = $"//*[text()='{dialogTitle}']/parent::div/following-sibling::button[contains(@id,'defaultDialogChromeCloseIconButton')]";
            By eleBy = SeleniumExtensions.getLocator(locator);
            waitHelpers wh = new waitHelpers();
            if (wh.isElementDisplayed(eleBy, 5))
            {
                wh.clickOnElement(eleBy);
            }
        }



        public void closeDialogIfPresent(string title)

        {
            if (title.Equals("New Request Data Synchronisation"))
            {
                waitTillappProgressIndicatorDisaddpears_();
                waitTillSavingDisaddpears();
            }
            String locator = $"//*[contains(text(),'{title}')]//ancestor::div[@role='dialog']//button[@aria-label='Close']";
            By eleBy = SeleniumExtensions.getLocator(locator);
            waitHelpers wh = new waitHelpers();
            if (wh.isElementDisplayed(eleBy, 5))
            {
                wh.clickOnElement(eleBy);
            }
        }

        public void waitTillProgressBarDisaddpears()
        {
            waitHelpers wh = new waitHelpers();
            String locator = $"//*[contains(@class,'indeterminateProgressRing') and contains(@data-id,'progress-indicator') and not(contains(@style,'display:none'))]";
            string loadingSpinner = "//div[contains(@class,'ms-Modal-scrollableContent scrollableContent')]//div[contains(@class,'ms-Spinner-circle ms-Spinner')]";

            By eleBy = SeleniumExtensions.getLocator(locator);
            By loadingSpinnerBy = SeleniumExtensions.getLocator(loadingSpinner);

            //WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(240));
            //wait.PollingInterval = TimeSpan.FromMilliseconds(200);
            //wait.Until(SeleniumExtensions.InvisibilityOfElementLocatedBy(eleBy));
            bool isEleAvailable = false;
            bool isEleAvailable_new = false;

            int i = 0;
            do
            {
                isEleAvailable = wh.isElementDisplayedInMilliSec(eleBy, 10);
                isEleAvailable_new = wh.isElementDisplayedInMilliSec(loadingSpinnerBy, 10);

                i = i + 1;
                if (i == 50) break;

            } while (isEleAvailable && isEleAvailable_new);
        }

        public void waitTillFindHeridetamentDisaddpears()
        {
            waitHelpers wh = new waitHelpers();
            By locator = By.CssSelector("[class*='ms-Dialog-main main']");
            bool isEleAvailable = false;
            int i = 0;
            do
            {
                //isEleAvailable = wh.isElementDisplayed(locator, 10);
                isEleAvailable = SeleniumExtensions.waitUntillElementInvisible(locator);
                i = i + 1;
                if (i == 2000) break;

            } while (!isEleAvailable);
            //wh.waitTillElementInvisible(By.CssSelector("[class*='ms-Dialog-main main']"));
        }

        public List<String> getBusinessStagesDetails()
        {
            waitHelpers wh = new waitHelpers();
            SeleniumExtensions.WaitForReadyStateToComplete();
            List<String> businessStagesNames = new List<String>();
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(120));
            wait.PollingInterval = TimeSpan.FromMilliseconds(200);
            wait.Until(SeleniumExtensions.PresenceOfAllElementsLocated(By.CssSelector($"button[id*='ProcessBreadCrumb-processHeaderStageButton'] div[id*='ProcessBreadCrumb-processHeaderStageName']")));
            IList<IWebElement> businessStageNamesLi = wh.GetWebElements(businessStageNamesBy);
            foreach (IWebElement ele in businessStageNamesLi)
            {
                //SeleniumExtensions.waitForElementToBeDisplayed(ele, 120);
                String s = ele.Text.Trim();
                try
                {
                    businessStagesNames.Add(ele.Text.Trim());
                }
                catch (Exception e)
                {
                    clickOnMainMenuMoreElement("Refresh");
                    businessStagesNames.Add(ele.Text.Trim());
                }
            }
            return businessStagesNames;
        }

        public List<String> getQuessionairyDetails()
        {
            SeleniumExtensions.WaitForReadyStateToComplete();
            //String frameLocator1 = $"[name*='webPlayer-iFrame']";
            //String frameLocator2 = $"[id = 'fullscreen-app-host']";
            //String questionsLocator = $"//*[@data-control-name='Title4' and not(contains(@style,'display: none;'))]//*[@data-control-part='text']";
            String questionsLocator = $"//*[@class='___ls441z0 fk6fouc fkhj508 fl43uef f1i3iumi fcpy4bm f1lgi7cd']";
            //By FramEleBy1 = SeleniumExtensions.getLocator(frameLocator1);
            //By FramEleBy2 = SeleniumExtensions.getLocator(frameLocator2);
            By QuisonEleBy = SeleniumExtensions.getLocator(questionsLocator);
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(240));
            //wait.Until(SeleniumExtensions.FrameToBeAvailableAndSwitchToit(FramEleBy1));
            //wait.Until(SeleniumExtensions.FrameToBeAvailableAndSwitchToit(FramEleBy2));
            List<String> QuestionsDetails = new List<String>();
            List<IWebElement> questionsEle = DriverHelper.Driver.FindElements(QuisonEleBy).ToList();
            foreach (IWebElement ele in questionsEle)
            {
                SeleniumExtensions.waitForElementToBeDisplayed(ele, 60);
                SeleniumExtensions.ScrollToElement(ele);
                String s = ele.Text.Trim();
                QuestionsDetails.Add(ele.Text.Trim());
            }
            return QuestionsDetails;
        }

        public void clickOnDataResearchFormTab(String tabName, String researchForm)
        {
            waitHelpers wh = new waitHelpers();
            WebDriverWait ww = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(15));
            try
            {
                String locator = $"//ul[@aria-label='{researchForm}']//li[@role='tab' and text()='{tabName}']";
                By eleBy = SeleniumExtensions.getLocator(locator);
                wh.clickOnElement(eleBy, ww);
            }
            catch (Exception e)
            {
                String moreTabsLocator = $"//ul[@aria-label='{researchForm}']//li[@role='tab' and @aria-label='More Tabs']";
                By moretabsBy = SeleniumExtensions.getLocator(moreTabsLocator);
                String tabLocator = $"//div[contains(@aria-label,'{tabName}')] | //li[contains(@aria-label,'{tabName}')] | //span[text()='{tabName}']/parent::div";
                By tabBy = SeleniumExtensions.getLocator(tabLocator);
                wh.clickOnElement(moretabsBy, ww);
                wh.clickOnElement(tabBy, ww);
            }
        }



        public void clickOnTab_Latest(String tabName, String researchForm)
        {
            waitHelpers wh = new waitHelpers();
            By locator = By.CssSelector($"ul[aria-label='{researchForm}'] li[role='tab']");
            Thread.Sleep(5000);
            wh.isElementDisplayed(locator, 120);
            wh.GetWebElement(locator);
            bool isEleAvailable = wh.getAllWebElementsText(locator).Contains(tabName);
            IList<IWebElement> eleLi = wh.getAllWebElements(locator);
            foreach (var ele in eleLi)
            {
                try
                {
                    if (isEleAvailable)
                    {
                        if (ele.Text.Trim().Equals(tabName))
                        {
                            wh.jsClickOnElement(ele);
                            //ele.Click();
                            break;
                        }
                    }
                    else
                    {
                        By moreTabsLocatorBy = By.XPath($"//ul[@aria-label='{researchForm}']//li[@role='tab' and @aria-label='More Tabs']");
                        By tabLocatorBy1 = By.XPath($"//span[text()='{tabName}']/parent::div[@role='menuitem']");
                        By tabLocatorBy = By.XPath($"//div[contains(@aria-label,'{tabName}')] | //li[contains(@aria-label,'{tabName}')]");
                        if (wh.isElementDisplayed(moreTabsLocatorBy, 10))
                        {
                            wh.clickOnElement(moreTabsLocatorBy);
                            if (wh.isElementDisplayed(tabLocatorBy1, 10))
                            {
                                wh.clickOnElement(tabLocatorBy1);
                                break;
                            }
                            else if (wh.isElementDisplayed(tabLocatorBy, 10))
                            {
                                wh.clickOnElement(tabLocatorBy);
                                break;
                            }
                        }
                        else
                        {
                            var cp = new CommonPage();
                            cp.clickOnSubTab(tabName, "Related");
                            break;
                        }
                    }
                }
                catch (Exception e)
                {
                    By moreTabsLocatorBy = By.XPath($"//ul[@aria-label='{researchForm}']//li[@role='tab' and @aria-label='More Tabs']");
                    By tabLocatorBy = By.XPath($"//div[contains(@aria-label,'{tabName}')] | //li[contains(@aria-label,'{tabName}')]");

                    if (wh.isElementDisplayed(moreTabsLocatorBy, 30))
                    {
                        wh.clickOnElement(moreTabsLocatorBy);
                        wh.clickOnElement(tabLocatorBy);
                    }
                    else
                    {
                        var cp = new CommonPage();
                        cp.clickOnSubTab(tabName, "Related");
                    }
                }
            }
        }

        public void clickOnTab_proposal(String tabName, String researchForm)
        {
            waitHelpers wh = new waitHelpers();
            By locator = By.CssSelector($"ul[aria-label='{researchForm}'] li[role='tab']");
            Thread.Sleep(5000);
            wh.isElementDisplayed(locator, 120);
            wh.GetWebElement(locator);
            bool isEleAvailable = wh.getAllWebElementsText(locator).Contains(tabName);
            IList<IWebElement> eleLi = wh.getAllWebElements(locator);
            foreach (var ele in eleLi)
            {
                try
                {
                    if (isEleAvailable)
                    {
                        if (ele.Text.Trim().Equals(tabName))
                        {
                            ele.Click();
                            break;
                        }
                    }
                    else
                    {
                        By moreTabsLocatorBy = By.XPath($"//ul[@aria-label='{researchForm}']//li[@role='tab' and @aria-label='More Tabs']");
                        By tabLocatorBy1 = By.XPath($"//span[text()='{tabName}']/parent::div[@role='menuitem']");
                        By tabLocatorBy = By.XPath($"(//div[contains(@aria-label,'{tabName}')] | //li[contains(@aria-label,'{tabName}')])[2]");

                        if (wh.isElementDisplayed(moreTabsLocatorBy, 10))
                        {
                            wh.clickOnElement(moreTabsLocatorBy);
                            if (wh.isElementDisplayed(tabLocatorBy1, 10))
                            {
                                wh.clickOnElement(tabLocatorBy1);
                                break;
                            }
                            else if (wh.isElementDisplayed(tabLocatorBy, 10))
                            {
                                wh.clickOnElement(tabLocatorBy);
                                break;
                            }
                        }
                        else
                        {
                            var cp = new CommonPage();
                            cp.clickOnSubTab(tabName, "Related");
                            break;
                        }
                    }
                }
                catch (Exception e)
                {
                    By moreTabsLocatorBy = By.XPath($"//ul[@aria-label='{researchForm}']//li[@role='tab' and @aria-label='More Tabs']");
                    By tabLocatorBy = By.XPath($"(//div[contains(@aria-label,'{tabName}')] | //li[contains(@aria-label,'{tabName}')])[2]");


                    if (wh.isElementDisplayed(moreTabsLocatorBy, 30))
                    {
                        wh.clickOnElement(moreTabsLocatorBy);
                        wh.clickOnElement(tabLocatorBy);
                    }
                    else
                    {
                        var cp = new CommonPage();
                        cp.clickOnSubTab(tabName, "Related");
                    }
                }
            }
            }
        public void clickOnDataResearchFormTab(String tabName, String researchForm, WebDriverWait wait)
        {
            waitHelpers wh = new waitHelpers();
            try
            {
                String locator = $"//ul[@aria-label='{researchForm}']//li[@role='tab' and text()='{tabName}']";
                By eleBy = SeleniumExtensions.getLocator(locator);
                wh.waitTillElementInvisible(eleBy, wait);
                wh.clickOnElement(eleBy, wait);
            }
            catch (Exception e)
            {
                String moreTabsLocator = $"//ul[@aria-label='{researchForm}']//li[@role='tab' and @aria-label='More Tabs']";
                By moretabsBy = SeleniumExtensions.getLocator(moreTabsLocator);
                String tabLocator = $"//div[contains(@aria-label,'{tabName}')] | //li[contains(@aria-label,'{tabName}')]";
                By tabBy = SeleniumExtensions.getLocator(tabLocator);
                wh.clickOnElement(moretabsBy, wait);
                wh.clickOnElement(tabBy, wait);
            }
        }
        public void selectHereditemetStatusRecord_Milo(String status)
        {
            waitHelpers wh = new waitHelpers();
            String locator = $"(//div[text()='{status}']//ancestor::div[contains(@class,'ms-DetailsRow')]/div[contains(@class,'ms-DetailsRow-cellCheck')])[1]";
            By eleBy = SeleniumExtensions.getLocator(locator);
            Thread.Sleep(5000);
            wh.GetWebElementVisble(eleBy);
            wh.GetWebElement(eleBy);
            Thread.Sleep(5000);
            wh.isElementDisplayed(eleBy, 120);
            Actions act = new Actions(DriverHelper.Driver);
            act.ScrollToElement(wh.GetWebElement(eleBy)).Pause(TimeSpan.FromSeconds(10)).Build().Perform();
            //act.ScrollToElement(SeleniumExtensions.waitForElementToBeDisplayed(eleBy, 90)).Pause(TimeSpan.FromSeconds(10)).Build().Perform();
            //SeleniumExtensions.waitForElementToBeDisplayed(eleBy, 90).ClickUsingJavascript();
            wh.elementClickableAndDisplayed(eleBy);
            wh.clickOnWebElement(eleBy);

        }

        public void selectHereditemetStatusRecord(String status)
        {
            waitHelpers wh = new waitHelpers();
            By expander = By.CssSelector($"[class*='cellIsGroupExpander']");
            Thread.Sleep(5000);
            wh.GetWebElementVisble(expander);
            wh.isElementDisplayed(expander, 120);
            wh.elementClickableAndDisplayed(expander);
            wh.clickOnWebElement(expander);
            //SeleniumExtensions.waitForElementToBeDisplayed(expanderEle, 120).Click();
            String locator = $"(//div[text()='{status}']//ancestor::div[contains(@class,'ms-DetailsRow')]/div[contains(@class,'ms-DetailsRow-cellCheck')])[1]";
            By eleBy = SeleniumExtensions.getLocator(locator);
            Thread.Sleep(5000);
            wh.GetWebElementVisble(eleBy);
            wh.GetWebElement(eleBy);
            Thread.Sleep(5000);
            wh.isElementDisplayed(eleBy, 120);
            Actions act = new Actions(DriverHelper.Driver);
            act.ScrollToElement(wh.GetWebElement(eleBy)).Pause(TimeSpan.FromSeconds(10)).Build().Perform();
            //act.ScrollToElement(SeleniumExtensions.waitForElementToBeDisplayed(eleBy, 90)).Pause(TimeSpan.FromSeconds(10)).Build().Perform();
            //SeleniumExtensions.waitForElementToBeDisplayed(eleBy, 90).ClickUsingJavascript();
            wh.elementClickableAndDisplayed(eleBy);
            wh.clickOnWebElement(eleBy);
        }

        public string selectassessment(string Status, string Assessment)
        {
            waitHelpers wh = new waitHelpers();
            // Click checkbox for the row with matching Status
            string locator = $"(//div[text()='{Status}']//ancestor::div[contains(@class,'ms-DetailsRow')]/div[contains(@class,'ms-DetailsRow-cellCheck')])[1]";
            By eleBy = SeleniumExtensions.getLocator(locator);
            wh.clickOnElement(eleBy);

            By Modifiedband = By.XPath($"//div[@role='row' and @data-automationid='DetailsRow' and .//div[text()='{Status}']]//div[@data-automation-key='bandColumn']");

            // Click assessment button

            string locator_ = $"(//span[contains(@class,'ms-Button-label') and contains(normalize-space(text()),'{Assessment}')])";
            By eleBy_ = SeleniumExtensions.getLocator(locator_);
            wh.clickOnElement(eleBy_);
            //By selectassessmentbutton = By.XPath($"//span[contains(@class,'ms-Button-label') and contains(normalize-space(text()),'{Assessment}')]");
            //wh.GetWebElement(selectassessmentbutton);
            ////wh.isElementDisplayed(selectassessmentbutton, 120);
            //wh.clickOnElement(selectassessmentbutton);

            // Assert success message
            By successMessage = By.XPath("//span[contains(text(),'Assessment linked successfully.')]");
            IWebElement textElement = wh.GetWebElement(successMessage);
            string actualText = textElement.Text;
            Assert.AreEqual("Assessment linked successfully.", actualText.Trim(), "Status text does not match.");


            return wh.getElementText(Modifiedband);
        }
        public void selectassessment_EDC(string Status, string Action)
        {
            waitHelpers wh = new waitHelpers();
            string locator = $"//div[normalize-space(.)=\"{Status}\"]//following::*[normalize-space(.)=\"{Action}\"]//ancestor::div[@data-automationid='DetailsRow']//div[@data-automationid='DetailsRowCheck']";
            By eleBy = SeleniumExtensions.getLocator(locator);
            //wh.clickOnElement(eleBy);
            wh.clickOnWebElement(eleBy);
        }

        public void selectlinkedAssesment(string Assessment)
        {
            waitHelpers wh = new waitHelpers();
            string locator = $"(//span[contains(@class,'ms-Button-label') and contains(normalize-space(text()),'{Assessment}')])";
            By eleBy = SeleniumExtensions.getLocator(locator);
            //wh.clickOnElement(eleBy);
            wh.clickOnWebElement(eleBy);
        }

        public bool waitforTopMenuBarLoading()
        {
            waitHelpers wh = new waitHelpers();
            bool elementclickable = false;
            try
            {
                elementclickable = wh.WaitForElementToBeClickble(By.XPath("//ul[@data-id='CommandBar'  and contains(@aria-label,'Commands')]"), 60).Displayed;
            }
            catch (ElementClickInterceptedException e)
            {
                Thread.Sleep(3000);
                elementclickable = wh.WaitForElementToBeClickble(By.XPath("//ul[@data-id='CommandBar'  and contains(@aria-label,'Commands')]"), 60).Displayed;
            }
            catch (ElementNotInteractableException e)
            {
                Thread.Sleep(3000);
                elementclickable = wh.WaitForElementToBeClickble(By.XPath("//ul[@data-id='CommandBar'  and contains(@aria-label,'Commands')]"), 60).Displayed;
            }
            catch (StaleElementReferenceException e)
            {
                Thread.Sleep(3000);
                elementclickable = wh.WaitForElementToBeClickble(By.XPath("//ul[@data-id='CommandBar'  and contains(@aria-label,'Commands')]"), 60).Displayed;
            }
            catch (Exception e)
            {
                Thread.Sleep(3000);
                elementclickable = wh.WaitForElementToBeClickble(By.XPath("//ul[@data-id='CommandBar'  and contains(@aria-label,'Commands')]"), 60).Displayed;
            }
            return elementclickable;

        }

        public Dictionary<String, String> getPADattributesOfStatusRecord(String status)
        {
            waitHelpers wh = new waitHelpers();
            String locator = $"(//div[text()='{status}'])[1]/ancestor::div[@data-automation-key='statusColumn']/following-sibling::div[@data-automationid='DetailsRowCell' and @data-automation-key='padsColumn']//div[contains(@id,'voaPadsCircle')]";
            IList<IWebElement> padAttributes = wh.getAllWebElements(By.XPath(locator));
            Dictionary<String, String> padAttr = new Dictionary<string, string>();
            int i = 0;
            foreach (IWebElement ele in padAttributes)
            {
                i = i + 1;
                if (i == 1)
                {
                    padAttr.Add("Group", ele.Text);
                }
                else if (i == 2)
                {
                    padAttr.Add("Type", ele.Text);
                }
                else if (i == 3)
                {
                    padAttr.Add("Age Code", ele.Text);
                }
                else if (i == 4)
                {
                    padAttr.Add("Area", ele.Text);
                }
                else if (i == 5)
                {
                    padAttr.Add("Heating", ele.Text);
                }
                else if (i == 6)
                {
                    padAttr.Add("Rooms", ele.Text);
                }
                else if (i == 7)
                {
                    padAttr.Add("Bedrooms", ele.Text);
                }
                else if (i == 8)
                {
                    padAttr.Add("Bathrooms", ele.Text);
                }
                else if (i == 9)
                {
                    padAttr.Add("Floors", ele.Text);
                }
                else if (i == 10)
                {
                    padAttr.Add("Level", ele.Text);
                }
                else if (i == 11)
                {
                    padAttr.Add("Parking", ele.Text);
                }
                else if (i == 12)
                {
                    padAttr.Add("ReasonCode", ele.Text);
                }
                else if (i == 13)
                {
                    padAttr.Add("Conservatory Type", ele.Text);
                }
                else if (i == 14)
                {
                    padAttr.Add("Conservatory Area", ele.Text);
                }
                else if (i == 15)
                {
                    padAttr.Add("PlotSize", ele.Text);
                }
            }

            return padAttr;
        }

        public void expandPVTs()
        {
            SeleniumExtensions.waitForElementToBeDisplayed(expanderEle, 120).Click();
        }

        public void clickEleOnDialog(String btnName, String dialogTitle)
        {
            waitHelpers wh = new waitHelpers();
            //By locator = By.XPath($"//*[text()='{dialogTitle}']//ancestor::*[contains(@id,'dialogContentContainer')]//button[text()='{btnName}'] | //*[text()='{dialogTitle}']//ancestor::*[contains(@id,'ModalFocusTrap')]//*[text()='{btnName}']/ancestor::button | //*[text()='{dialogTitle}']//ancestor::*[contains(@id,'modalDialog')]//*[text()='{btnName}']/ancestor::button | //*[text()='{dialogTitle}']//ancestor::*[contains(@data-id,'Assign')]//*[text()='{btnName}']/ancestor::button");
            By locator = null;
            if (dialogTitle.Equals("Clone and Amend PADs?"))
            {
                locator = By.XPath($"//*[text()='{dialogTitle}']//ancestor::*[contains(@id,'ModalFocusTrap')]//*[text()='{btnName}']/ancestor::button");
            }
            else if (dialogTitle.Equals("Confirm MILO Transaction Activation"))
            {
                locator = By.XPath($"//*[text()='{dialogTitle}']//ancestor::*[contains(@role,'dialog')]//*[text()='{btnName}']");
            }
            else if (dialogTitle.Equals("Confirm Deactivation"))
            {
                locator = By.XPath($"//*[text()='{dialogTitle}']//ancestor::*[contains(@role,'dialog')]//*[text()='{btnName}']");
            }
            else if (dialogTitle.Equals("Success"))
            {
                locator = By.XPath($"//*[text()='{dialogTitle}']//ancestor::*[contains(@role,'document')]//*[text()='{btnName}']/ancestor::button");
            }
            else if (dialogTitle.Equals("Cannot perform this action"))
            {
                locator = By.XPath($"//*[text()='{dialogTitle}']//ancestor::*[contains(@role,'presentation')]//*[text()='{btnName}']/ancestor::button");

            }
            else
            {
                locator = By.XPath($"//*[text()='{dialogTitle}']//ancestor::*[contains(@id,'modalDialog')]//*[text()='{btnName}']/ancestor::button");
            }
            wh.clickOnElement(locator);

        }

        public bool isStatusDisplayed_new(string refreshBtn, string btnName, string dialogTitle, string status)
        {
            bool isStatusDisplayed = false;
            int attempt = 0;
            int maxAttempts = 10;
            waitHelpers wh = new waitHelpers();

            do
            {
                SeleniumExtensions.WaitForPageLoad();

                // ✅ Click refresh button
                clickOnMainMenuMoreElement_New(refreshBtn);

                // ✅ Handle popup if it appears
                try
                {
                    By popupLocator = By.XPath($"//*[text()='{dialogTitle}']//ancestor::*[contains(@id,'modalDialog')]//*[text()='{btnName}']/ancestor::button");
                    if (wh.isElementDisplayed(popupLocator, 3))
                    {
                        wh.clickOnElement(popupLocator);
                        Console.WriteLine($"Popup '{dialogTitle}' handled by clicking '{btnName}'.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("No popup appeared or error handling popup: " + ex.Message);
                }
               

                // ✅ Check if status is displayed
                By statusLocator = By.XPath($"(//*[normalize-space(text())='{status}'])[1] | (//*[normalize-space(text())='{status}'])[2]");
                isStatusDisplayed = wh.isElementDisplayed(statusLocator, 5);
                Console.WriteLine($"Attempt {attempt + 1}: Status displayed? {isStatusDisplayed}");

                attempt++;
                if (isStatusDisplayed || attempt >= maxAttempts)
                {
                    Assert.IsTrue(isStatusDisplayed, $"Status '{status}' was not displayed after {maxAttempts} attempts.");
                    break;
                }

            } while (!isStatusDisplayed);

            return isStatusDisplayed;
        }





        public void clickEleOnDialog(int position, String btnName, String dialogTitle)
        {
            waitHelpers wh = new waitHelpers();
            //By locator = By.XPath($"//*[text()='{dialogTitle}']//ancestor::*[contains(@id,'dialogContentContainer')]//button[text()='{btnName}'] | //*[text()='{dialogTitle}']//ancestor::*[contains(@id,'ModalFocusTrap')]//*[text()='{btnName}']/ancestor::button | //*[text()='{dialogTitle}']//ancestor::*[contains(@id,'modalDialog')]//*[text()='{btnName}']/ancestor::button | //*[text()='{dialogTitle}']//ancestor::*[contains(@data-id,'Assign')]//*[text()='{btnName}']/ancestor::button");
            By locator = null;
            if (dialogTitle.Equals("Clone and Amend PADs?"))
            {
                locator = By.XPath($"//*[text()='{dialogTitle}']//ancestor::*[contains(@id,'ModalFocusTrap')]//*[text()='{btnName}']/ancestor::button");
            }
            else
            {
                locator = By.XPath($"(//*[text()='{dialogTitle}']//ancestor::*[contains(@id,'modalDialog')]//*[text()='{btnName}']/ancestor::button)[{position}]");
            }
            wh.clickOnElement(locator);

        }

        public void clickEleOnconfirmDialog(String buttonText)
        {
            waitHelpers wh = new waitHelpers();
            By locator = null;
            locator = By.XPath($"//button[.//div[text()='{buttonText}']]");
            wh.clickOnElement(locator);

        }





        public void clickconfirmbutton(String dialogTitle, String btnName)
        {
            waitHelpers wh = new waitHelpers();
            By locator = null;
            locator = By.XPath($"//*[text()='{dialogTitle}']//ancestor::*[contains(@class,'ms-Modal-scrollableContent')]//*[text()='{btnName}']/ancestor::button");
            wh.clickOnElement(locator);

        }

        public void openRecord()
        {
            waitHelpers wh = new waitHelpers();
            By locator = null;
            locator = SeleniumExtensions.getLocator($"div[col-id='PlaceHolder'] button");
            wh.clickOnElement(locator);
        }


        public void clickEleOnDialogIfappears(String btnName, String dialogTitle)
        {
            waitHelpers wh = new waitHelpers();
            SeleniumExtensions.WaitForReadyStateToComplete();
            String locatorStr = null;
            if (dialogTitle.Equals("Clone and Amend PADs?"))
            {
                locatorStr = $"//*[text()='{dialogTitle}']//ancestor::*[contains(@id,'ModalFocusTrap')]//*[text()='{btnName}']/ancestor::button";
            }
            else
            {
                locatorStr = $"//*[text()='{dialogTitle}']//ancestor::*[contains(@id,'modalDialog')]//*[text()='{btnName}']/ancestor::button";
            }
            By eleBy = SeleniumExtensions.getLocator(locatorStr);
            if (wh.isElementDisplayed(eleBy, 10))
            {
                wh.clickOnWebElement(eleBy);
            }
        }


        public void switchToAssessmentFrame()
        {
            SeleniumExtensions.WaitForReadyStateToComplete();
            String frameLocator1 = $"[name*='webPlayer-iFrame']";
            String frameLocator2 = $"[id = 'fullscreen-app-host']";
            By FramEleBy1 = SeleniumExtensions.getLocator(frameLocator1);
            By FramEleBy2 = SeleniumExtensions.getLocator(frameLocator2);
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(90));
            wait.Until(SeleniumExtensions.FrameToBeAvailableAndSwitchToit(FramEleBy1));
            wait.Until(SeleniumExtensions.FrameToBeAvailableAndSwitchToit(FramEleBy2));
        }

        public int getRowCount()
        {
            waitHelpers wh = new waitHelpers();
            String locator = $"[class='ms-List-cell'] ";
            int assessmentCount = 0;
            By locatorBy = SeleniumExtensions.getLocator(locator);
            try
            {
                if (wh.isElementDisplayed(locatorBy, 120))
                {
                    var eleList = wh.getAllWebElements(locatorBy);
                    assessmentCount = eleList.Count;
                }
                else
                {
                    assessmentCount = 0;
                }
            }
            catch (Exception e)
            {
                assessmentCount = 0;
            }

            return assessmentCount;
        }
        public int getRowCount_correspondence()
        {
            waitHelpers wh = new waitHelpers();
            //String locator = $"//div[contains(@data-id,'entity_control-powerapps_onegrid_control_container')] //div[@role='row']";
            String locator = $"//div[contains(@data-id,'entity_control-powerapps_onegrid_control_container')] //div[@role='row' and not(contains(@class,'header'))]";

            int assessmentCount = 0;
            By locatorBy = SeleniumExtensions.getLocator(locator);
            try
            {
                if (wh.isElementDisplayed(locatorBy, 20))
                {
                    var eleList = wh.getAllWebElements(locatorBy);
                    assessmentCount = eleList.Count;
                }
                else
                {
                    assessmentCount = 0;
                }
            }
            catch (Exception e)
            {
                assessmentCount = 0;
            }
            Console.WriteLine($"[INFO] Assessment row count: {assessmentCount}");
            return assessmentCount;
        }


        public void validateFieldValue(String fieldName, String condition)
        {
            waitHelpers wh = new waitHelpers();
            String locator = $"//ul[@title='{fieldName}' and contains(@id,'SelectedRecordList')]";
            int count = 0;
            By locatorBy = SeleniumExtensions.getLocator(locator);
            try
            {
                if (wh.isElementDisplayed(locatorBy, 20))
                {
                    var eleList = DriverHelper.Driver.FindElements(locatorBy);
                    count = eleList.Count;
                }
                else
                {
                    count = 0;
                }
            }
            catch (Exception e)
            {
                count = 0;
            }
            if(condition.Equals("Yes"))
                Assert.IsTrue(count==1);
            else
                Assert.IsTrue(count == 0);
        }

        public int getJobIdRowCount()
        {
            SeleniumExtensions.WaitForReadyStateToComplete();
            String locator = $"//*[@col-id='ticketnumber' and not(contains(@class,'header'))]/ancestor::*[@role='row']";
            By locatorBy = SeleniumExtensions.getLocator(locator);
            SeleniumExtensions.WaitForElementToDisplayed(locatorBy, 120);
            var eleList = DriverHelper.Driver.FindElements(locatorBy);
            return eleList.Count;
        }

        public bool assertProposedBAReference(int rowSize, string RefreshBtn)
        {
            Boolean isCountMatched = false;
            waitHelpers wh = new waitHelpers();
            int i = 0;
            do
            {
                //Thread.Sleep(10 * 1000);
                String selector = $"ul[aria-label='Proposed BA Reference Amendment Commands'] button[aria-label = '{RefreshBtn}']";
                By eleBy = SeleniumExtensions.getLocator(selector);
                wh.isElementDisplayed(eleBy, 5);
                wh.clickOnElement(eleBy);
                bool isEleDisplayed = false;
                try
                {
                    String locator = $"//*[@col-id='voa_billingauthorityid' and not(contains(@class,'header'))]/ancestor::*[@role='row']";
                    By locatorBy = SeleniumExtensions.getLocator(locator);
                    isEleDisplayed = wh.isElementDisplayed(locatorBy, 5);
                    if (isEleDisplayed)
                    {
                        var eleList = wh.getAllWebElements(locatorBy);
                        if (eleList.Count == rowSize)
                        {
                            isCountMatched = true;
                        }
                    }

                }
                catch (Exception e)
                {
                }
                i = i + 1;
                if (i == 50 || isCountMatched) break;
            } while (!isCountMatched);
            return isCountMatched;
        }


        public bool assertRelatedJobs(int rowSize, string RefreshBtn)
        {
            Boolean isCountMatched = false;
            waitHelpers wh = new waitHelpers();
            int i = 0;
            do
            {
                //Thread.Sleep(10 * 1000);
                String selector = $"ul[aria-label='Job Commands'] button[aria-label = '{RefreshBtn}']";
                By eleBy = SeleniumExtensions.getLocator(selector);
                wh.isElementDisplayed(eleBy, 5);
                wh.clickOnElement(eleBy);
                bool isEleDisplayed = false;
                try
                {
                    String locator = $"//*[@col-id='ticketnumber' and not(contains(@class,'header'))]/ancestor::*[@role='row']";
                    By locatorBy = SeleniumExtensions.getLocator(locator);
                    isEleDisplayed = wh.isElementDisplayed(locatorBy, 5);
                    if (isEleDisplayed)
                    {
                        var eleList = wh.getAllWebElements(locatorBy);
                        if (eleList.Count == rowSize)
                        {
                            isCountMatched = true;
                        }
                    }

                }
                catch (Exception e)
                {
                }
                i = i + 1;
                if (i == 50 || isCountMatched) break;
            } while (!isCountMatched);
            return isCountMatched;
        }


        public bool assertAssosiatedJobs(int rowSize, string RefreshBtn)
        {
            Boolean isCountMatched = false;
            waitHelpers wh = new waitHelpers();
            int i = 0;
            do
            {
                String selector = $"ul[aria-label='Job Commands'] button[aria-label = '{RefreshBtn}']";
                By eleBy = SeleniumExtensions.getLocator(selector);
                wh.isElementDisplayed(eleBy, 5);
                wh.clickOnElement(eleBy);
                bool isEleDisplayed = false;
                try
                {
                    String locator = $"div[class='ag-center-cols-container'] div[role='row']";
                    By locatorBy = SeleniumExtensions.getLocator(locator);
                    isEleDisplayed = wh.isElementDisplayed(locatorBy, 5);
                    if (isEleDisplayed)
                    {
                        var eleList = wh.getAllWebElements(locatorBy);
                        if (eleList.Count == rowSize)
                        {
                            isCountMatched = true;
                        }
                    }

                }
                catch (Exception e)
                {
                }
                i = i + 1;
                if (i == 50 || isCountMatched) break;
            } while (!isCountMatched);
            return isCountMatched;
        }



        public void userSelectAssesment(int rowNum)
        {
            SeleniumExtensions.WaitForReadyStateToComplete();
            String locator = $"[class='ms-List-cell']:nth-child({rowNum}) [class*='ms-DetailsRow-cellCheck']";
            By locatorBy = SeleniumExtensions.getLocator(locator);
            SeleniumExtensions.WaitForElementAndClick(locatorBy);
        }


        public bool isTabDisplayed(String refreshBtn, String tabName)
        {
            bool isEleDisplayed = false;
            int i = 0;
            do
            {
                String locator = $"//ul[@aria-label='Job Form']//li[@role='tab' and text()='{tabName}']";
                By eleBy = SeleniumExtensions.getLocator(locator);
                //clickOnMainMenuMoreElement_Latest(refreshBtn);
                //SeleniumExtensions.WaitForReadyStateToComplete();
                //isEleDisplayed = SeleniumExtensions.isElementDisplayed(eleBy);
                waitHelpers wh = new waitHelpers();
                //waitforTopMenuBarLoading();
                clickOnMainMenuMoreElement_New(refreshBtn);
                isEleDisplayed = wh.isElementDisplayed(eleBy, 10);
                i = i + 1;
                if (i == 10) break;
            } while (!isEleDisplayed);
            return isEleDisplayed;
        }

        public bool isStageSelected(String refreshBtn, String stageName)
        {
            bool isEleDisplayed = false;
            int i = 0;
            do
            {
                By stageSelected = SeleniumExtensions.getLocator($"//div[contains(@id,'MscrmControls.Containers.ProcessBreadCrumb-stageIndicatorContainer') and contains(@title,'{stageName}')]/div/div[contains(@id,'MscrmControls.Containers.ProcessBreadCrumb-stageIndicatorInnerHolder')]");
                waitHelpers wh = new waitHelpers();
                clickOnMainMenuMoreElement_New(refreshBtn);
                isEleDisplayed = wh.isElementDisplayed(stageSelected, 10);
                i = i + 1;
                if (i == 20) break;
            } while (!isEleDisplayed);
            return isEleDisplayed;
        }




        public bool isStatusDisplayed(String refreshBtn, String status)
        {
            bool isStatusDisplayed = false;
            int i = 0;
            waitHelpers wh = new waitHelpers();
            CommonPage commonpage = new CommonPage();

            do
            {
                By locator = By.XPath($"(//*[normalize-space(text())='{status}'])[1] | (//*[normalize-space(text())='{status}'])[2]");
                SeleniumExtensions.WaitForPageLoad(1000, 30);
                try
                {
                    clickOnMainMenuMoreElement_New(refreshBtn);
                }
                catch (Exception e)
                {
                    Thread.Sleep(2000);
                    clickOnMainMenuMoreElement_New(refreshBtn);
                }
                SeleniumExtensions.WaitForPageLoad();
                commonpage.waitTillSavingDisaddpears();
                isStatusDisplayed = wh.isElementDisplayed(locator, 5);
                Console.WriteLine("isStatusDisplayed : " + isStatusDisplayed);
                i = i + 1;
                if (i == 10 || isStatusDisplayed)
                {
                    Assert.IsTrue(isStatusDisplayed);
                    break;
                }
            } while (!isStatusDisplayed);
            return isStatusDisplayed;
        }

        public void currentbandcomparison(String Modifiedband, FeatureContext ft)
        {
            waitHelpers wh = new waitHelpers();
            String currentTaxBand = wh.GetWebElement(By.XPath("//div[contains(@data-id,'voa_initialcounciltaxbandid.fieldControl-LookupResultsDropdown_voa_initialcounciltaxbandid_selected_tag_text')]")).Text;
            //String Expectedvalue = _featureContext[Modifiedband].ToString();
            string Expectedvalue = (String)ft[Modifiedband];
            Assert.AreEqual(Expectedvalue, currentTaxBand.Trim(), "Tax band value does not match expected.");
        }
        public void validateandunlink(String MiloID, ScenarioContext sc, String buttonname)
        {
            waitHelpers wh = new waitHelpers();
            String actualvalue = wh.GetWebElement(By.XPath("//div[@role='row' and @data-automationid='DetailsRow']//div[@data-automation-key='name']")).Text;

            String Milovalue = (String)sc["MiloID"];
            Assert.AreEqual(Milovalue, actualvalue.Trim(), "Not matching");
            By locater = By.XPath("//div[@role='row' and @data-automationid='DetailsRow']//div[@data-automation-key='name']");
            wh.clickOnElement(locater);
            string locator_ = $"(//span[contains(@class,'ms-Button-label') and contains(normalize-space(text()),'{buttonname}')])";
            By eleBy_ = SeleniumExtensions.getLocator(locator_);
            wh.clickOnElement(eleBy_);
            By successMessage = By.XPath("//span[@title ='Milo records successfully unlinked to the hereditament.']");
            IWebElement textElement = wh.GetWebElement(successMessage);
            string actualText = textElement.Text;
            Assert.AreEqual("Milo records successfully unlinked to the hereditament.", actualText.Trim(), "Status text does not match.");
        }
        public void billingautoritycheck(String fieldName, FeatureContext ft)
        {
            waitHelpers wh = new waitHelpers();
            ////String Bareference = wh.GetWebElement(By.XPath("//input[contains(@data-id,'quickViewLatestBaLink.voa_billingauthorityreference.fieldControl-text-box-text')]")).Text;
            ////String Expectedvalue = _featureContext[Modifiedband].ToString();
            //string Expectedvalue = (String)ft[Proposed BA Reference Number];
            //Assert.AreEqual(Expectedvalue, Bareference.Trim(), "Values are not matching.");

            IWebElement baInput = wh.GetWebElement(
                   By.CssSelector("input[data-id*='quickViewLatestBaLink.voa_billingauthorityreference']"));
            string actual = baInput?.GetAttribute("value")?.Trim();
            string expected = ft[fieldName] as string;
            Assert.IsNotNull(expected, $"No value was stored in FeatureContext for key '{fieldName}'.");
            Assert.IsNotNull(actual, "Billing Authority Reference input value could not be read (null).");
            Assert.AreEqual(expected.Trim(), actual, $"Values are not matching for '{fieldName}'.");


        }


        public bool isFieldStatusDislayed(String refreshBtn, String fieldName, String status)
        {
            bool isStatusDisplayed = false;
            int i = 0;
            waitHelpers wh = new waitHelpers();
            if (Config.EnvironmentVal != "SIT")
            {
                SeleniumExtensions.clickElementAndSelectText("Status Reason", "Sent");
                clickOnMainMenuMoreElement_New("Save");
                waitTillSavingDisaddpears("Saving...", "progressbar");

            }
            else
            {
                do
                {
                    By locator = By.CssSelector($"[aria-label='{fieldName}']");
                    //waitforTopMenuBarLoading();
                    clickOnMainMenuMoreElement_New(refreshBtn);
                    wh.isElementDisplayed(locator, 1);
                    String eleTxt = wh.getElementText(locator);
                    if (eleTxt.Trim().Equals(status))
                    {
                        isStatusDisplayed = true;
                    }
                    else
                    {
                        isStatusDisplayed = false;
                    }
                    if (eleTxt.Trim().Equals("Failed to Send"))
                    {
                        Assert.IsTrue(false, $"Status is not expected , actual '{fieldName}' is '{eleTxt.Trim()}' and expcted '{fieldName}' is '{status}'");
                        break;
                    }
                    Console.WriteLine("isStatusDisplayed : " + isStatusDisplayed);
                    i = i + 1;
                    if (i == 100)
                    {
                        Assert.IsTrue(isStatusDisplayed);
                        break;
                    }
                } while (!isStatusDisplayed);
            }
            return isStatusDisplayed;
        }

        public bool isKPINonCompliantDisplayed(String KPI_Name, String KPI_Status, String SLARefreshBtn)
        {
            bool isStatusDisplayed = false;
            int i = 0;
            waitHelpers wh = new waitHelpers();
            do
            {
                By locator = By.CssSelector($"[title='{KPI_Name}']~[title='{KPI_Status}']");
                By SLARefreshBtnLoc = By.CssSelector($"[aria-label='{SLARefreshBtn}']");
                SeleniumExtensions.WaitForPageLoad();
                //waitforTopMenuBarLoading();
                wh.isElementDisplayed(SLARefreshBtnLoc, 10);
                wh.clickOnElement(SLARefreshBtnLoc);
                isStatusDisplayed = wh.isElementDisplayed(locator, 2);
                Console.WriteLine("isStatusDisplayed : " + isStatusDisplayed);
                i = i + 1;
                if (i == 50)
                {
                    Assert.IsTrue(isStatusDisplayed);
                    break;
                }
            } while (!isStatusDisplayed);
            return isStatusDisplayed;
        }

        public String getRequestName()
        {
            waitHelpers wh = new waitHelpers();
            By requestNameBy = By.CssSelector("h1[id *= 'formHeaderTitle']");
            wh.isElementDisplayed(requestNameBy, 60);
            return wh.getElementText(requestNameBy).Replace("- Saved", "");

        }
        public void EnterDataInSingleCharLookUpField(String fieldName, Dictionary<string, string> testdata)
        {
            String fieldValue = testdata[fieldName];
            String field = $"input[aria-label = '{fieldName}, Lookup']";
            String searchField = $"button[aria-label = 'Search records for {fieldName}, Lookup field']";
            String dropDownOption = $"//*[text()='{fieldValue}']/ancestor::*[@*='voa_counciltaxbandid.fieldControl-LookupResultsDropdown_voa_counciltaxbandid_infoContainer']";
            By lookUpLocator = SeleniumExtensions.getLocator(field);
            By dropDownOptionLocator = SeleniumExtensions.getLocator(dropDownOption);

            DriverHelper.Driver.WaitForElement(SeleniumExtensions.getLocator(searchField)).ClickUsingJavascript();
            DriverHelper.Driver.WaitForElement(By.CssSelector("[id*='tab-section']")).ScrollUntilSelectorDisplayedAndSendKeys(lookUpLocator,
           fieldValue, 10);
            DriverHelper.Driver.FindElement(dropDownOptionLocator).ClickUsingJavascript();
        }

        public string EnterDataInSingleCharLookUpField(String fieldName, String fieldValue)
        {
            waitHelpers wh = new waitHelpers();
            String field = $"input[aria-label = '{fieldName}, Lookup']";
            String searchField = $"button[aria-label = 'Search records for {fieldName}, Lookup field']";
            String dropDownOption = $"//*[text()='{fieldValue}']/ancestor::*[@*='voa_counciltaxbandid.fieldControl-LookupResultsDropdown_voa_counciltaxbandid_infoContainer']";
            By lookUpLocator = SeleniumExtensions.getLocator(field);
            By dropDownOptionLocator = SeleniumExtensions.getLocator(dropDownOption);
            DriverHelper.Driver.WaitForElement(By.CssSelector("[id*='tab-section']")).ScrollUntilSelectorDisplayedAndSendKeys(lookUpLocator,
           fieldValue, 5);
            wh.clickOnWebElement(SeleniumExtensions.getLocator(searchField));
            //wh.GetWebElement(lookUpLocator).
            Thread.Sleep(500);
            wh.GetWebElement(dropDownOptionLocator).ClickUsingJavascript();
            //DriverHelper.Driver.FindElement(dropDownOptionLocator).ClickUsingJavascript();
            return fieldValue;

        }

        public void EnterDataInSingleCharLookUpField(String fieldName, String sheetName, String testRowID)
        {
            string TestDataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + Config.TestDataExcelFilePath);
            var excelUtility = new ExcelTestDataUtility(TestDataFilePath);
            string value = excelUtility.getFieldTestData(fieldName, sheetName, testRowID);
            String field = $"input[aria-label = '{fieldName}, Lookup']";
            String searchField = $"button[aria-label = 'Search records for {fieldName}, Lookup field']";
            String dropDownOption = $"//*[text()='{value}']/ancestor::*[@*='voa_counciltaxbandid.fieldControl-LookupResultsDropdown_voa_counciltaxbandid_infoContainer']";
            By lookUpLocator = SeleniumExtensions.getLocator(field);
            By dropDownOptionLocator = SeleniumExtensions.getLocator(dropDownOption);

            DriverHelper.Driver.WaitForElement(SeleniumExtensions.getLocator(searchField)).ClickUsingJavascript();
            DriverHelper.Driver.WaitForElement(By.CssSelector("[id*='tab-section']")).ScrollUntilSelectorDisplayedAndSendKeys(lookUpLocator,
           value, 10);
            DriverHelper.Driver.FindElement(dropDownOptionLocator).ClickUsingJavascript();
        }

        public void selectLookUpFieldPosition(String fieldName, int position)
        {
            waitHelpers wh = new waitHelpers();
            String field = $"input[aria-label = '{fieldName}, Lookup']";
            String searchField = $"button[aria-label = 'Search records for {fieldName}, Lookup field']";
            String dropDownOption = $"//ul[contains(@aria-label,'Lookup results')]/li[{position}]/div[2]";
            By lookUpLocator = SeleniumExtensions.getLocator(field);
            By dropDownOptionLocator = SeleniumExtensions.getLocator(dropDownOption);
            DriverHelper.Driver.WaitForElement(SeleniumExtensions.getLocator(searchField)).ClickUsingJavascript();
            //DriverHelper.Driver.FindElement(dropDownOptionLocator).ClickUsingJavascript();
            wh.GetWebElement(dropDownOptionLocator).Click();
        }

        public void clickOnFieldNameLink(String fieldName)
        {
            // String field = $"//*[text()='{fieldName}']/parent::*/following-sibling::div//div[@role='link']";
            String field = $"//*[@title='{fieldName}']//div[@role='link']";
            By lookUpLocator = SeleniumExtensions.getLocator(field);
            //SeleniumExtensions.WaitForElementAndClick(lookUpLocator);
            waitHelpers wh = new waitHelpers();
            wh.isElementDisplayed(lookUpLocator, 60);
            wh.clickOnWebElement(lookUpLocator);
        }

        public void clickOnlinkname(String linkname)
        {
            // String field = $"//*[text()='{fieldName}']/parent::*/following-sibling::div//div[@role='link']";
            //String field = $"a[aria-label='{linkname}'][role='link']";
            String field = $"a[aria-label=\"{linkname}\"][role='link']";
            By lookUpLocator = SeleniumExtensions.getLocator(field);
            //SeleniumExtensions.WaitForElementAndClick(lookUpLocator);
            waitHelpers wh = new waitHelpers();
            wh.isElementDisplayed(lookUpLocator, 60);
            wh.clickOnWebElement(lookUpLocator);
        }

        public void clickOnlistitems(String Listitems)
        {

            String field1 = $"button[aria-label='{Listitems}'][role='menuitem']";
            By lookUpLocator = SeleniumExtensions.getLocator(field1);
            waitHelpers wh = new waitHelpers();
            wh.isElementDisplayed(lookUpLocator, 60);
            wh.clickOnWebElement(lookUpLocator);
        }

        public void clickOnHeaderJobLink()
        {
            waitHelpers wh = new waitHelpers();
            SeleniumExtensions.WaitForPageLoad();
            //waitforTopMenuBarLoading();
            String field = $"(//div[contains(@id,'headerControlsList')]//a)[1]";
            By lookUpLocator = SeleniumExtensions.getLocator(field);
            wh.GetWebElement(lookUpLocator);
            wh.isElementDisplayed(lookUpLocator, 60);
            wh.elementToClickble(lookUpLocator);
            wh.jsClickOnElement(lookUpLocator);
        }

        public void changeStatus(String btnName, String targetStatus, String destinationStatus)
        {
            waitHelpers wh = new waitHelpers();
            String committedRec = $"(//div[text()='{destinationStatus}']//ancestor::div[contains(@class,'ms-DetailsRow')])[1]";
            String proposedRec = $"(//div[text()='{targetStatus}']//ancestor::div[contains(@class,'ms-DetailsRow')])[1]";
            By commitedBy = SeleniumExtensions.getLocator(committedRec);
            By proposedBy = SeleniumExtensions.getLocator(proposedRec);
            By pvtRefreshBy = SeleniumExtensions.getLocator($"//*[contains(@data-id,'pcf-container-id')]//*[text()='{btnName}']/ancestor::button");
            bool proposedRecord = wh.GetWebElement(proposedBy, 2);
            bool committedRecord = wh.GetWebElement(commitedBy, 2);
            int i = 0;
            if (wh.GetWebElement(commitedBy, 3))
            {

            }
            else
            {
                do
                {
                    //proposedRecord = SeleniumExtensions.isElementDisplayed(proposedBy);
                    wh.clickOnElement(pvtRefreshBy);
                    proposedRecord = wh.GetWebElement(proposedBy, 3);
                    SeleniumExtensions.WaitForReadyStateToComplete();
                    i = i + 1;
                    if (i == 15) break;
                } while (!proposedRecord);
            }
        }


        public List<String> getStatusDetails()
        {
            SeleniumExtensions.WaitForReadyStateToComplete();
            By statusLocator = SeleniumExtensions.getLocator($"//*[@class='ms-Viewport']//*[@data-automation-key='statusColumn']//div[contains(@class,'TooltipHost root')]/div[1]");
            //*[@class='ms-Viewport']/ancestor::div[@class='ms - List - page']//*[@data-automation-key='statusColumn']/div[contains(@class,'ms - TooltipHost root')]/div
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(120));
            wait.PollingInterval = TimeSpan.FromMilliseconds(50);
            wait.Until(SeleniumExtensions.PresenceOfAllElementsLocated(statusLocator));
            IList<IWebElement> statusRecords = DriverHelper.Driver.FindElements(statusLocator);
            List<string> statusRecordsLi = new List<string>();
            foreach (IWebElement ele in statusRecords)
            {
                //SeleniumExtensions.waitForElementToBeDisplayed(ele, 120);
                String s = ele.Text.Trim();
                statusRecordsLi.Add(ele.Text.Trim());
            }
            return statusRecordsLi;
        }

        public void validateRecordsForDataEnhancement(string effectivefromDate, string effectiveToDate, string typeOfPAD, Table t)
        {
            waitHelpers wh = new waitHelpers();
            if (typeOfPAD.Equals("Change Date"))
            {
                By committedRecord = SeleniumExtensions.getLocator($"//*[@data-automation-key='fromDateColumn']//div[text()='{effectiveToDate}']//ancestor::div[@role='row']//*[@data-automation-key='effectiveToDateColumn']//div[contains(@class,'ms-Stack') and not(text())]//ancestor::div[@role='row']//*[@data-automation-key='statusColumn']//*[text()='Committed']");
                By superSededRecord = SeleniumExtensions.getLocator($"//*[@data-automation-key='fromDateColumn']//div[text()='{effectivefromDate}']//ancestor::div[@role='row']//*[@data-automation-key='effectiveToDateColumn']//div[contains(@class,'ms-Stack') and (text()='{effectiveToDate}')]//ancestor::div[@role='row']//*[@data-automation-key='statusColumn']//*[text()='Superseded']");
                wh.GetWebElementVisble(committedRecord);
                wh.GetWebElementVisble(superSededRecord);
            }

            if (typeOfPAD.Equals("CloneToNewDate_Committed"))
            {
                By committedRecord = SeleniumExtensions.getLocator($"//*[@data-automation-key='fromDateColumn']//div[text()='{effectiveToDate}']//ancestor::div[@role='row']//*[@data-automation-key='effectiveToDateColumn']//div[contains(@class,'ms-Stack') and not(text())]//ancestor::div[@role='row']//*[@data-automation-key='statusColumn']//*[text()='Committed']");
                By committedRecord1 = SeleniumExtensions.getLocator($"//*[@data-automation-key='fromDateColumn']//div[text()='{effectivefromDate}']//ancestor::div[@role='row']//*[@data-automation-key='effectiveToDateColumn']//div[contains(@class,'ms-Stack') and (text()='{effectiveToDate}')]//ancestor::div[@role='row']//*[@data-automation-key='statusColumn']//*[text()='Committed']");
                wh.GetWebElementVisble(committedRecord);
                wh.GetWebElementVisble(committedRecord1);
            }

            if (typeOfPAD.Equals("CloneToNewDate_Pending"))
            {
                By pendingRecord = SeleniumExtensions.getLocator($"//*[@data-automation-key='fromDateColumn']//div[text()='{effectiveToDate}']//ancestor::div[@role='row']//*[@data-automation-key='effectiveToDateColumn']//div[contains(@class,'ms-Stack') and not(text())]//ancestor::div[@role='row']//*[@data-automation-key='statusColumn']//*[text()='Pending']");
                By committedRecord = SeleniumExtensions.getLocator($"//*[@data-automation-key='fromDateColumn']//div[text()='{effectivefromDate}']//ancestor::div[@role='row']//*[@data-automation-key='effectiveToDateColumn']//div[contains(@class,'ms-Stack') and not(text())]//ancestor::div[@role='row']//*[@data-automation-key='statusColumn']//*[text()='Committed']");
                wh.GetWebElementVisble(pendingRecord);
                wh.GetWebElementVisble(committedRecord);
            }


        }

        public void clickOnSubTab(String subTabName, String mainTabName)
        {
            waitHelpers wh = new waitHelpers();
            By mainTab = SeleniumExtensions.getLocator($"//li[@aria-label='{mainTabName}']");
            By subTab = SeleniumExtensions.getLocator($"[aria-label*='{subTabName}']");
            wh.clickOnElement(mainTab);
            wh.clickOnElement(subTab);
        }

        public void updateJobOwner()
        {
            waitHelpers wh = new waitHelpers();
            Actions act = new Actions(DriverHelper.Driver);
            By headerFieldsExpandBtn = SeleniumExtensions.getLocator($"button[id*='headerFieldsExpandButton']");
            wh.clickOnElement(headerFieldsExpandBtn);
            By removeLookUpFieldBtn = SeleniumExtensions.getLocator($"//button[contains(@data-id,'ownerid_selected_tag_delete')]");
            By header_OwnerField = SeleniumExtensions.getLocator($"(//div[contains(@data-id,'header_ownerid.fieldControl-Lookup_ownerid')])[3]");
            //act.MoveToElement(wh.GetWebElement(header_OwnerField)).ScrollToElement(wh.GetWebElement(header_OwnerField)).Pause(TimeSpan.FromSeconds(2)).Click(wh.GetWebElement(removeLookUpFieldBtn)).Build().Perform();
            act.MoveToElement(wh.GetWebElement(header_OwnerField)).Pause(TimeSpan.FromMilliseconds(500)).Build().Perform();
            wh.clickOnElement(removeLookUpFieldBtn);
            SeleniumExtensions.ScrollUntilSelectorDisplayedAndSendKeys_New("Owner", "Srinivas", 120);
        }

        public void updatenewlinkedassessment()
        {

            waitHelpers wh = new waitHelpers();
            By removeLookUpFieldBtn = SeleniumExtensions.getLocator("//span[contains(@data-id,'linkedassessmentid_microsoftIcon_cancelButton')]");
            wh.clickOnElement(removeLookUpFieldBtn);
            //SeleniumExtensions.ScrollUntilSelectorDisplayedAndSendKeys_New("Owner", "", 10);
            //By firstLookupValue = SeleniumExtensions.getLocator("[data-id*='Lookup'] ul li span[id*='name']");
            By searchlinkedassessment = By.XPath("//span[@data-id='voa_linkedassessmentid.fieldControl-Lookup_voa_linkedassessmentid_microsoftIcon_searchButton']");
            wh.clickOnElement(searchlinkedassessment);
        }

        public string decrementCharacter(String band, int decrementCuntr)
        {
            char bandChar = char.Parse(band);
            char newBand = (bandChar == 'A' ? 'A' : (char)(bandChar - decrementCuntr));
            return newBand.ToString();
        }

        public string incrementtCharacter(String band, int incrementCuntr)
        {
            char bandChar = char.Parse(band);
            char newBand = (bandChar == 'F' ? 'F' : (char)(bandChar + incrementCuntr));
            return newBand.ToString();
        }

        public void userSelectRow()
        {
            waitHelpers wh = new waitHelpers();
            Actions act = new Actions(DriverHelper.Driver);
            By inputRow = By.CssSelector("div[class*='ms-Checkbox is-enabled RowSelectionCheckMarkSpan']");
            act.MoveToElement(wh.GetWebElement(inputRow)).Build().Perform();
            wh.clickOnWebElement(inputRow);
        }

        public void jobCommadsEditRecord(String btnName)
        {
            waitHelpers wh = new waitHelpers();
            By btnEle = By.CssSelector($"ul[aria - label = 'Job Commands'] button[aria - label = '{btnName}']");

            By moreCommands = By.CssSelector($"ul[aria - label = 'Job Commands'] button[aria - label = 'More commands for Job']");

            By overFlowBtnEle = By.CssSelector($"ul[data - id = 'OverflowFlyout'] button[aria - label = '{btnName}']");
            if (wh.elementDisplayed(btnEle, 2))
            {
                wh.clickOnElement(btnEle);
            }
            else
            {
                wh.clickOnElement(moreCommands);
                wh.clickOnElement(overFlowBtnEle);
            }
        }

        public void userDoubleClickRow()
        {
            waitHelpers wh = new waitHelpers();
            Actions act = new Actions(DriverHelper.Driver);
            By inputRow = By.CssSelector("div[class*='ms-Checkbox is-enabled RowSelectionCheckMarkSpan']");
            IWebElement ele = wh.GetWebElement(inputRow);
            act.MoveToElement(ele).DoubleClick(ele).Build().Perform();
            //wh.clickOnWebElement(inputRow);
        }
        public void clickOnBtnEleclose(String btnName)
        {
            waitHelpers wh = new waitHelpers();
            By btnEle = By.XPath($"//button[@title='{btnName}']");
            wh.isElementDisplayed(btnEle, 60);
            wh.clickOnWebElement(btnEle);
        }

        public void clickOnBtnEle(String btnName)
        {
            waitHelpers wh = new waitHelpers();
            By btnEle = By.XPath($"//button[contains(@aria-label,'{btnName}')] | //button[@title='{btnName}']");
            wh.isElementDisplayed(btnEle, 60);
            wh.clickOnWebElement(btnEle);
        }

        public void userValidatebannernotification(String text)
        {
            waitHelpers wh = new waitHelpers();
            By btnEle = By.XPath($"//span[@role='presentation' and @data-id='warningNotification' and contains(., \"{text}\")]");
            Thread.Sleep(2500);
            Assert.IsTrue(wh.GetWebElement(btnEle, 3));
        }


        public void userValidateText(String text)
        {
            waitHelpers wh = new waitHelpers();
            By btnEle = By.XPath($"//*[text()='{text}']");
            Thread.Sleep(2500);
            Assert.IsTrue(wh.GetWebElement(btnEle, 3));
        }

        public void userValidateText_new(String text)
        {
            waitHelpers wh = new waitHelpers();
            By btnEle = By.XPath($"//*[text()=\"{text}\"]");
            Assert.IsTrue(wh.GetWebElement(btnEle).Displayed,$"Expected text {text} is not visible on the screen");
        }


        public void userValidatestatus(String status)
        {
            waitHelpers wh = new waitHelpers();
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(120));
            wait.PollingInterval = TimeSpan.FromMilliseconds(10);
            By btnEle = By.XPath($"(//*[normalize-space(text())='{status}'])[1]");
            Thread.Sleep(2500);
            Assert.IsTrue(wh.GetWebElement(btnEle, 3));
        }

        public void userValidateContainsText(String text)
        {
            waitHelpers wh = new waitHelpers();
            By btnEle = By.XPath($"//*[contains(text(),'{text}')]");
            Assert.IsTrue(wh.GetWebElement(btnEle, 30));
        }

        public void userModifiesFieldValue(By fieldLocator, By filedValueClose)
        {
            Actions act = new Actions(DriverHelper.Driver);
            waitHelpers wh = new waitHelpers();
            By jobOwnerFieldLoc = By.CssSelector("div[data-id = 'voa_jobowner.fieldControl-Lookup_voa_jobowner']");
            By JobOwnerFieldClose = By.XPath("//button[@data-id='voa_jobowner.fieldControl-LookupResultsDropdown_voa_jobowner_selected_tag_delete']");
            act.MoveToElement(wh.GetWebElement(jobOwnerFieldLoc)).Pause(TimeSpan.FromMilliseconds(500)).Build().Perform();
            wh.clickOnWebElement(JobOwnerFieldClose);
        }
        public void userupdatejobtype(string value, String fieldName)
        {
            Actions act = new Actions(DriverHelper.Driver);
            waitHelpers wh = new waitHelpers();
            By jobfieldvalue = By.CssSelector("div[data-id = 'voa_codedreasonid.fieldControl-Lookup_voa_codedreasonid']");
            By jobfield = By.XPath("//span[@data-id='voa_codedreasonid.fieldControl-LookupResultsDropdown_voa_codedreasonid_microsoftIcon_cancelButton']");
            act.MoveToElement(wh.GetWebElement(jobfieldvalue)).Pause(TimeSpan.FromMilliseconds(500)).Build().Perform();
            wh.clickOnWebElement(jobfield);
            SeleniumExtensions.lookUpFieldBasedOnText(fieldName, value, 120);

        }

        public void userupdateacceptancedecision(string value, String fieldName)
        {
            Actions act = new Actions(DriverHelper.Driver);
            waitHelpers wh = new waitHelpers();
            By fieldLoca = By.CssSelector("div[data-id = 'voa_acceptancedecisionid.fieldControl-Lookup_voa_acceptancedecisionid']");
            By fieldDelete = By.XPath("//button[@data-id='voa_acceptancedecisionid.fieldControl-LookupResultsDropdown_voa_acceptancedecisionid_selected_tag_delete']");
            act.MoveToElement(wh.GetWebElement(fieldLoca)).Pause(TimeSpan.FromMilliseconds(500)).Build().Perform();
            wh.clickOnWebElement(fieldDelete);
            SeleniumExtensions.lookUpFieldBasedOnText(fieldName, value, 120);

        }
        public void userClickOnJobUnderAssociatedJobs(String JobType)
        {
            Actions act = new Actions(DriverHelper.Driver);
            waitHelpers wh = new waitHelpers();
            By associatedJobRecord = By.XPath($"//a[@aria-label='{JobType}']//ancestor::div[@aria-colindex='3']/parent::div//div[@aria-colindex='4']");
            act.DoubleClick(wh.GetWebElement(associatedJobRecord)).Build().Perform();
        }

        public void userClickOnfileuploadrow(String JobType)
        {
            Actions act = new Actions(DriverHelper.Driver);
            waitHelpers wh = new waitHelpers();
            By fileuploadrecord = By.XPath($"//div[contains(@class,'ms-DetailsRow')]//*[text()='VOA Provided']/ancestor::div[contains(@class,'ms-DetailsRow')]");
            act.Click(wh.GetWebElement(fileuploadrecord)).Build().Perform();
        }

        public void userClickonpropertylink(String fieldname)
        {
            Actions act = new Actions(DriverHelper.Driver);
            waitHelpers wh = new waitHelpers();
            //By propertylinkRecord = By.XPath($"//a[contains(@aria-label, '{fieldname}') and @role='link']");
            By propertylinkRecord = By.XPath($"//label[contains(@aria-label, '{fieldname}')]");
            act.DoubleClick(wh.GetWebElement(propertylinkRecord)).Build().Perform();
        }

        public List<String> GetDekstopResearchQuestionnaire()
        {
            SeleniumExtensions.WaitForReadyStateToComplete();
            List<String> QuestionsDetails = new List<String>();
            List<IWebElement> questionsEle = DriverHelper.Driver.FindElements(QuestionnaireList).ToList();
            foreach (IWebElement ele in questionsEle)
            {
                SeleniumExtensions.waitForElementToBeDisplayed(ele, 60);
                SeleniumExtensions.ScrollToElement(ele);
                String s = ele.Text.Trim();
                QuestionsDetails.Add(ele.Text.Trim());
            }
            return QuestionsDetails;
        }

        public void ValidateStatusAfterReleaseAndPublish(string statusText)
        {
            int maxretries = 15;
            int retrydelay = 10000;
            bool validateFlag = false;
            var getStatus = "";
            for (int retry = 0; retry < maxretries; retry++)
            {
                try
                {
                    if (Statustext.ElementVisisbleUsingExplicitWait(10))
                    {
                        getStatus = Statustext.Text;
                        if (getStatus == statusText)
                        {
                            validateFlag = true;
                            break;
                        }
                        else
                        {
                            ClickCommandBarOption("Refresh");
                        }
                    }
                    else
                    {
                        Console.WriteLine("The statuses is not Visible");
                    }

                }

                catch (NoSuchElementException)
                {
                    Console.WriteLine("Required status not displayed after retrying");
                }
                Thread.Sleep(retrydelay);
            }
            if (validateFlag == false)
            {
                Assert.Fail($"List Updated is not displaying on the Status only {getStatus} is appearing on the UI");
                // Log.Error("");
            }
            else
            {
                Console.WriteLine("List Updated status is displayed on the UI");
            }
        }

        public void clickOnSupplementaryJob()
        {
            try
            {
                waitHelpers wh = new waitHelpers();
                bool isEleDisplayed = false;
                int i = 0;
                do
                {
                    String selector = $"ul[aria-label='Supplementary Job Requisition Commands'] button[aria-label = 'Refresh']";
                    By eleBy = SeleniumExtensions.getLocator(selector);
                    wh.clickOnElement(eleBy);
                    By eleRowBy = By.CssSelector("[col-id='voa_supplementaryjobid'] a");
                    try
                    {
                        isEleDisplayed = wh.isElementDisplayed(eleRowBy, 30);
                        if (isEleDisplayed)
                        {
                            wh.clickOnElement(eleRowBy);
                            break;
                        }
                    }
                    catch (Exception e)
                    {
                        isEleDisplayed = false;
                    }
                    i = i + 1;
                    if (i == 20) break;
                } while (!isEleDisplayed);
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

        public void validateOutboundCorrespondanceTemplate(string templateName)
        {
            try
            {
                waitHelpers wh = new waitHelpers();
                By templateBy = SeleniumExtensions.getLocator($"div[col-id='voa_obcconfigurationid'] a[aria-label='{templateName}']");
                Assert.True(wh.elementDisplayed(templateBy));
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

        public By getNoActionElementLocator(String fieldName)
        {
            By elementXpath = null;
            switch (fieldName)
            {
                case "No Action Code":
                    elementXpath = By.XPath("//div[@data-control-name='drpNoActionCode']//select");
                    break;
                case "No Action SubCode":
                    elementXpath = By.XPath("//div[@data-control-name='drpNoActionSubCode']//select");
                    break;
                case "Internal Remarks":
                    elementXpath = By.XPath("//div[@data-control-name='txtInternalRemarks']//textarea");
                    break;
                case "BA Remarks":
                    elementXpath = By.XPath("//div[@data-control-name='txtBaRemarks']//textarea");
                    break;
                case "Resolve Request":
                    elementXpath = By.XPath("//div[@data-control-name='tglResolveRequest']");
                    break;
                case "End Date Improvement Indicator":
                    elementXpath = By.XPath("//div[@data-control-name='lblImprovementIndicator']");
                    break;
            }
            return elementXpath;
        }

        public void waitTillPageLoading(int timeInSecs)
        {
            WebDriverWait wait1 = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeInSecs));
            wait1.Until(d =>
                ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete")
            );
            Console.WriteLine("Page fully loaded.");
        }

        public void collapseNavIfItIsOpen()
        {
            try
            {
                waitHelpers wh = new waitHelpers();
                By navBtn = By.XPath("//button[@data-id='navbutton']");
                if (wh.GetWebElement(navBtn).GetAttribute("aria-expanded").Equals("true"))
                {
                    wh.jsClickOnElement(navBtn);
                }
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

        public void expandnewrecord(String newrecord)
        {
            try
            {
                waitHelpers wh = new waitHelpers();
                By newrecordbutton = By.XPath($"//*[@aria-Label='{newrecord}']");
                if (wh.GetWebElement(newrecordbutton).GetAttribute("aria-expanded").Equals("false"))
                {
                    wh.jsClickOnElement(newrecordbutton);
                }
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

        public void CR10NotificationMsgValidate(String msg)
        {
            try
            {
                waitHelpers wh = new waitHelpers();
                By navBtn = By.XPath($"//div[@id='message-relatedCR10sNotification']//*[text()='{msg}']");
                Assert.IsTrue(wh.elementDisplayed(navBtn));
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

        public void clickOnLabel(String LabelName)
        {
            try
            {
                waitHelpers wh = new waitHelpers();
                By labelEle = By.XPath($"//label[text()='{LabelName}']");
                wh.clickOnWebElement(labelEle);
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


        public void ValidatesheChangeCodeeMessage()
        {
            try
            {
                string statusReasonText = OCStatusReasondropdownvalue.Text;

                if (statusReasonText != "Processing")
                {
                    OCStatusReasondropdownvalue.ClickUsingJavascript();
                    SeleniumExtensions.SelectDropdownValue(OCStatusReasondropdown, "Ready");

                    ClickCommandBarOption("Save");
                    while (statusReasonText != "Processing")
                    {
                        ClickCommandBarOption("Refresh");
                        statusReasonText = OCStatusReasonText.Text;
                        if (statusReasonText == "Processing")
                        {
                            ClickOnOptionInRelatedTabOnForm("Integration Messages", "Related", "Outbound Correspondence Form");
                            ValidateAndClickOnTab("Integration Messages", "Outbound Correspondence Form");
                            //  ValidateAutoProcessingRuntimeStatus("Completed");
                            ValidateCorrespondenceIntegrationMessageStatus("Sent");

                        }
                        else if (statusReasonText.Contains("Failed"))
                        {
                            Assert.Fail("There was an error generating the correspondence and it failed with status reason " + statusReasonText);
                        }

                    }
                }
                else
                {
                    ClickOnOptionInRelatedTabOnForm("Integration Messages", "Related", "Outbound Correspondence Form");
                    ValidateAndClickOnTab("Integration Messages", "Outbound Correspondence Form");
                    ValidateCorrespondenceIntegrationMessageStatus("Sent");
                }
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
     
        public bool StatusDisplayed(String refreshBtn, String statusText)
        {
            waitHelpers wh = new waitHelpers();
            var count = 0;
            bool Statusflag = false;
            //Assert.IsTrue(Statustext.ElementVisisbleUsingExplicitWait(10), "The element is not visible yet");
            var getStatus = "";
            if (wh.isElementDisplayed(Statustext, 90))
            {
                getStatus = Statustext.Text;

            }
            while (getStatus != statusText)
            {
                ClickCommandBarOption("Refresh");
                CommonPage cp = new CommonPage();
                cp.waitTillPageLoading(60);
                try
                {
                    wh.isElementDisplayed(Statustext, 90);
                }
                catch (Exception e)
                {
                    Assert.IsTrue(Statustext.ElementVisisbleUsingExplicitWait(10), "The element is not visible yet");
                }
                if (getStatus == "Pending Approval")
                {
                    ValidateAndClickOnTab("Quality Review", "Job Form");
                    CheckandUpdatethePendingApprovaltab("Active");
                    Statusflag = true;
                }
                if (wh.GetWebElement(StatustextBy).Text == statusText)
                {
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
                    //Log.Error($"The expected status {statusText} does not matches with the actual {getStatus}");

                    break;
                }
            }
            if (Statustext.Text == statusText)
            {
                Statusflag = true;
                return Statusflag;
            }
            else if (getStatus == "Pending Approval")
            {
                Statusflag = true;
                return Statusflag;
            }
            else
            {
                return Statusflag;
            }

        }

        
        public void ClickAndOpenTheJob()
        {
            try
            {
                waitHelpers wh1 = new waitHelpers();
                CommonPage cp = new CommonPage();
                JobCreatedRowOnRelatedJobs.ClickUsingJavascript();
                Thread.Sleep(3000);
                clickOnMainMenuMoreElement_New("Assign");
                wh1.elementDisplayed(AssignBtn, 5);
                AssignBtnNew.ClickUsingJavascript();
                //cp.clickEleOnDialog("Assign", "Assign to Team or User");
                waitHelpers wh = new waitHelpers();
                By progressLocator = By.XPath($"//div[contains(@class,'Spinner')]");
                wh.waitTillElementInvisible(progressLocator);
                JobCreatedRowOnRelatedJobs.DoubleClickElementUsingJSExecutor();
                waitTillSavingDisaddpears("Saving...", "progressbar");
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
        public static void lockedfield(string fieldname)
        {
            waitHelpers wh1 = new waitHelpers();
            CommonPage cp = new CommonPage();
            By lockeddate = By.XPath($"//div[@aria-label = 'Locked {fieldname}']");
            Assert.IsTrue(Driver.FindElement(lockeddate).Displayed, "Field is not locked.");

   }
        public void enterDataInproposedbareferencefield(String fieldName, Dictionary<String, String> testData, FeatureContext fc)
        {
            string value = "";
            String contextValue = getFeatureContextValue(fieldName, fc);
            if (!String.IsNullOrEmpty(contextValue))
            {
                value = contextValue;
            }
            else
            {
                value = testData[fieldName];
            }

            By selector = SeleniumExtensions.getLocator($"//input[contains(@aria-label,'{fieldName}')]");
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(120));
            wait.PollingInterval = TimeSpan.FromMilliseconds(200);
            wait.Until(driver => DriverHelper.Driver.FindElement(selector).Displayed);
            IWebElement ele = DriverHelper.Driver.FindElement(selector);
            ele.SendKeys(value);
        }

        public bool isSucceedingRequestDisplayed(String refreshBtn, String status)
        {
            bool isStatusDisplayed = false;
            int i = 0;
            waitHelpers wh = new waitHelpers();
            CommonPage commonpage = new CommonPage();
            

            do
            {
                SeleniumExtensions.WaitForPageLoad(1000, 30);
                try
                {
                    clickOnMainMenuMoreElement_New(refreshBtn);
                }
                catch (Exception e)
                {
                    Thread.Sleep(2000);
                    clickOnMainMenuMoreElement_New(refreshBtn);
                }
                SeleniumExtensions.WaitForPageLoad();
                commonpage.waitTillSavingDisaddpears();
                isStatusDisplayed = wh.isElementDisplayed(succedingRequestBy, 5);
                Console.WriteLine("isStatusDisplayed : " + isStatusDisplayed);
                i = i + 1;
                if (i == 500 || isStatusDisplayed)
                {
                    Assert.IsTrue(isStatusDisplayed,$"required {status} displayed");
                    string actual = wh.getElementText(succedingRequestBy);
                    string expected = formSucceedingRequestName();

                    Assert.That(actual, Is.EqualTo(expected) ,$"Mismatch for field '{status}'. Expected: {expected}, Actual: {actual}");
                    break;
                }
            } while (!isStatusDisplayed);
            return isStatusDisplayed;
        }

        public string formSucceedingRequestName()
        { string requestName = "";
            waitHelpers wh = new waitHelpers();
            string hereditament = wh.getElementText(consequentialsHereditamentBy);
            string consequentialsJobType = wh.getElementText(consequentialsJobTypeBy);
            requestName = $"CT: Request, {consequentialsJobType}, {hereditament}";
            return requestName;
        }

        public void navigateToSucceedingRequest()
        {
            waitHelpers wh = new waitHelpers();
            wh.clickOnElement(succedingRequestBy);
        }



    }
}
