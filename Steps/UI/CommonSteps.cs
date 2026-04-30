using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using POMSeleniumFrameworkPoc1.DTO.UI;
using POMSeleniumFrameworkPoc1.Helpers;
using POMSeleniumFrameworkPoc1.Pages;
using POMSeleniumFrameworkPoc1.Pages.UI;
using POMSeleniumFrameworkPoc1.Pages.UI.CommonComponents;
using POMSeleniumFrameworkPoc1.Pages.UI.RBSP_BP;
using Reqnroll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;


namespace POMSeleniumFrameworkPoc1.Steps.UI
{
    [Binding]
    class CommonSteps : BasePage
    {
        public ScenarioContext _scenarioContext;
        public FeatureContext _featureContext;
        public DTO.InputOutputData inputoutputdata;
        private readonly ExcelTestDataUtility exceltestdatautility;



        public CommonSteps(ScenarioContext scenarioContext, FeatureContext featureContext, DTO.InputOutputData _inputoutputdata)

        {
            string TestDataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + Config.TestDataExcelFilePath);
            this.inputoutputdata = _inputoutputdata;
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
            this.exceltestdatautility = new ExcelTestDataUtility(TestDataFilePath);
        }

        [Given(@"User uses test data with ID '(.*)' from '(.*)' sheet")]
        public void GivenUserUsesTestDataWithIDFromSheet(string testCaseID, string sheetName)
        {
            inputoutputdata.testData = exceltestdatautility.GetTestDataByID(sheetName, testCaseID);
            var testData = inputoutputdata.testData;

        }

        [Given(@"I run the scenario (.*) times")]
        public void GivenIRunTheScenarioTimes(int iterationNum)
        {
            Console.WriteLine("This is the " + iterationNum + " iteration");

        }

        [Given(@"User navigates to '(.*)' under '(.*)'")]
        public void GivenUserNavigatesToUnder(string menuItem, string sectionName)
        {
            try
            {
                NavigateToMenuItem(sectionName, menuItem);
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

        [Given(@"User clicks on '(.*)' on the commandbar")]
        public void GivenUserClicksOnOnTheCommandbar(string commandBarOption)
        {
            ClickCommandBarOption(commandBarOption);
        }



        [When(@"User clicks on '(.*)' on the commandbar")]
        public void GivenTheUserClicksOnOnTheCommandbar(string commandBarOption)
        {
            ClickCommandBarOption(commandBarOption);
        }

        [Given(@"User clicks on the '(.*)' tab present in the '(.*)' tablist")]
        public void GivenUserClicksOnTheTabPresentInTheTablist(string tabName, string tabListsName)
        {
            ClickOnTab(tabName, tabListsName);
        }

        [Given(@"User validates and clicks the '(.*)' tab present in the '(.*)' tablist")]
        public void GivenUserValidatesAndClicksTheTabPresentInTheTablist(string tabName, string tabListsName)
        {
            try
            {
                ValidateAndClickOnTab(tabName, tabListsName);
                //var pdf_Util = new PDF_Utility();
                //pdf_Util.takeScreenshot();
            }
            catch (Exception e)
            {
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
                pdf_Util.exceptionPdFLogger(e);
            }

        }

        [Given(@"User clicks on '(.*)' for '(.*)' journey on the headerbar")]
        public void GivenUserClicksOnForJourneyOnTheHeaderbar(string buttonName, string jorneyHeader)
        {
            try
            {
                ProgressJourney(buttonName, jorneyHeader);
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
            }
            catch (Exception e)
            {
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
                pdf_Util.exceptionPdFLogger(e);
                throw new Exception("Exception details : " + e.Message);
            }
        }


        [Given(@"User clicked on '(.*)' for '(.*)' journey on the headerbar")]
        public void GivenUserClickedOnForJourneyOnTheHeaderbar(string buttonName, string jorneyHeader)
        {
            try
            {
                try
                {
                    ProgressJourney_withOutJSclick(buttonName, jorneyHeader);
                }
                catch (ElementClickInterceptedException e)
                {
                    waitHelpers wh = new waitHelpers();
                    CommonPage commonpage = new CommonPage();
                    commonpage.waitTillSavingDisaddpears_Old();
                    By jorneyHeaderUsingBy = JourneyHeaderUsingBySelector(jorneyHeader);
                    wh.GetWebElement(jorneyHeaderUsingBy);
                    ProgressJourney_withOutJSclick(buttonName, jorneyHeader);

                }
                catch (ElementNotInteractableException e)
                {
                    Thread.Sleep(4000);
                    waitHelpers wh = new waitHelpers();
                    By jorneyHeaderUsingBy = JourneyHeaderUsingBySelector(jorneyHeader);
                    wh.GetWebElement(jorneyHeaderUsingBy);
                    ProgressJourney_withOutJSclick(buttonName, jorneyHeader);
                }
            }
            catch (Exception e)
            {
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
                pdf_Util.exceptionPdFLogger(e);
                throw new Exception("Exception details : " + e.Message);
            }
        }


        [Given(@"User validate value '(.*)' for Owner field")]
        public void GivenUserValidateValueForOwnerField(string value)
        {
            try
            {
                Assert.IsTrue(SeleniumExtensions.validateFieldWithValueOnly(value).Displayed);
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


        [Given(@"User waits till '(.*)' stage selected")]
        public void GivenUserWaitsTillStageSelected(string stageName)
        {
            try
            {
                waitUntillStageIsSelected(stageName);
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


        [Given(@"User validates '(.*)' BPF journey as '(.*)'")]
        public void GivenUserValidatesBPFJourneyAs(string jorneyHeader, string expStatus)
        {
            ValidateActiveBPF(jorneyHeader, expStatus);
        }


        [Given(@"User validates the status as '(.*)'")]
        public void WhenUserValidatesTheStatusAs(string statusText)
        {
            ValidateStatus(statusText);
        }

        [Given(@"User validates the '(.*)' stage on the '(.*)' journey on the headerbar")]
        public void GivenUserValidtesTheStageOnTheJourneyOnTheHeaderbar(string stageText, string jouerneyName)
        {
            ValidateJourneyStage(stageText, jouerneyName);
        }

        [Given(@"User clicks on '(.*)' button")]
        public void GivenUserClicksOnButton(string buttonName)
        {
            ClickOnButtonByTitle(buttonName);
        }

        [Given(@"User selects '(.*)' menu list option from '(.*)' Commandbar menu")]
        public void GivenUserSelectsMenuListOptionFromCommandbarMenu(string menuListOption, string commandBarMenu)
        {
            ClickMenuListOptionFromCommandBar(menuListOption, commandBarMenu);
        }

        [Given(@"User selects the '(.*)' menu list option from '(.*)' Commandbar menu")]
        public void GivenUserSelectsMenuListOptionFromCommandbarMenuNew(string menuListOption, string commandBarMenu)
        {
            ClickMenuListOptionFromCommandBarNew(menuListOption, commandBarMenu);
        }

        [Given(@"user clicks on '(.*)' option from more tabs list")]
        public void GivenUserClicksOnOption(string optionName)
        {
            ClickOptionFromMoreTabs(optionName);
        }

        [Given(@"user validates the correspondence link and clicks on it")]
        public void GivenUserValidatesTheCorrespondenceLinkAndClicksOnIt()
        {
            CommonPage commonpage = new CommonPage();
            commonpage.ValidateCorrespondenceLinkAndClick();
        }

        [Given(@"user validates the No Action link  and clicks on it")]
        public void GivenUserValidatesTheNoActionLinkAndClicksOnIt()
        {
            CommonPage commonpage = new CommonPage();
            commonpage.NoactionLinkAndClick();
        }

        [Given(@"user validates the No Action link  not present")]
        public void GivenUserValidatesTheNoActionLinkNotPresent()
        {
            CommonPage commonpage = new CommonPage();
            commonpage.NoactionLinkAndValidate();
        }


        [Given(@"user validates the PDF generated")]
        public void GivenUserValidatesThePDFGenerated()
        {
            CommonPage commonpage = new CommonPage();
            commonpage.ValidateCorresspondenceMessage();
        }

        [Given(@"User clicks on the Navigate back")]
        public void GivenUserClicksOnTheNavigateBack()
        {
            CommonPage commonpage = new CommonPage();
            commonpage.ClickOnBacKButton();


        }


        [Given(@"User creates a New Request through a Submission Form")]
        public void GivenUserCreatesANewRequestThroughASubmissionForm()
        {
            var testData = inputoutputdata.testData;
            CommonPage commonpage = new CommonPage();
            commonpage.CreateRequestThroughSubmission(testData);

        }

        [Given(@"User Navigates to New Customer Enquiry Page")]
        public void GivenUserNavigatesToNewCustomerEnquiryPage()
        {
            CommonPage commonpage = new CommonPage();
            commonpage.NavigateToNewCustomerEnquiryPage();

        }

        [Given(@"User connects to the DB and retrieves the data for '(.*)' for reconstitution")]
        public void GivenUserConnectsToTheDBAndRetrievesTheDataForForReconstitution(string propertyKey, Table table)
        {
            var testData = inputoutputdata.testData;
            CommonPage commonpage = new CommonPage();
            string previousUprn = (string)_featureContext["uprn"];
            bool isUPRNdiff = false;
            int counter = 0;
            string queryParameter = "postcode";
            var DBData = commonpage.GetDBData(propertyKey, queryParameter, _featureContext);
            do
            {
                isUPRNdiff = previousUprn.Equals((string)DBData["uprn"]);
                counter = counter + 1;
                if (counter == 5 || !isUPRNdiff) break;
                DBData = commonpage.GetDBData(propertyKey, queryParameter, _featureContext);
            } while (!isUPRNdiff);

            foreach (var row in table.Rows)
            {
                string fieldName = row["fieldName"];
                try
                {


                    if (_featureContext[fieldName] != null && _featureContext[fieldName] != null)
                    {
                        testData[fieldName] = DBData[fieldName];
                        _featureContext[fieldName] = DBData[fieldName];
                        Console.WriteLine(_featureContext[fieldName]);
                    }

                    if (_scenarioContext[fieldName] != null && _scenarioContext[fieldName] != null)
                    {
                        testData[fieldName] = DBData[fieldName];
                        _scenarioContext[fieldName] = DBData[fieldName];
                        //Console.WriteLine(_scenarioContext[fieldName]);
                    }

                }
                catch (KeyNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    testData[fieldName] = DBData[fieldName];
                    _featureContext[fieldName] = DBData[fieldName];
                    Console.WriteLine(_featureContext[fieldName]);
                    testData[fieldName] = DBData[fieldName];
                    _scenarioContext[fieldName] = DBData[fieldName];
                }
            }
        }

        [Given(@"User connects to the DB and modifies query with '(.*)' and retrieves the unique data for '(.*)'")]
        public void GivenUserConnectsToTheDBAndModifiesQueryWithAndRetrievesTheUniqueDataFor(string queryParameter, string propertyKey, Table table)
        {
            var testData = inputoutputdata.testData;
            CommonPage commonpage = new CommonPage();
            var DBData = commonpage.GetDBData(propertyKey, queryParameter, testData);
            foreach (var row in table.Rows)
            {
                string fieldName = row["fieldName"];
                try
                {


                    if (_featureContext[fieldName] != null && _featureContext[fieldName] != null)
                    {
                        testData[fieldName] = DBData[fieldName];
                        _featureContext[fieldName] = DBData[fieldName];
                        Console.WriteLine(_featureContext[fieldName]);
                    }

                    if (_scenarioContext[fieldName] != null && _scenarioContext[fieldName] != null)
                    {
                        testData[fieldName] = DBData[fieldName];
                        _scenarioContext[fieldName] = DBData[fieldName];
                        //Console.WriteLine(_scenarioContext[fieldName]);
                    }

                }
                catch (KeyNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    testData[fieldName] = DBData[fieldName];
                    _featureContext[fieldName] = DBData[fieldName];
                    Console.WriteLine(_featureContext[fieldName]);
                    testData[fieldName] = DBData[fieldName];
                    _scenarioContext[fieldName] = DBData[fieldName];
                }
            }
        }

        [Given(@"User connects to the DB and modifies query with '(.*)' and retrieves the data for '(.*)'")]
        public void GivenUserConnectsToTheDBAndModifiesQueryWithAndRetrievesTheDataFor(string queryParameter, string propertyKey, Table table)
        {
            var testData = inputoutputdata.testData;
            CommonPage commonpage = new CommonPage();
            string previousUprn = (string)_featureContext["uprn"];
            bool isUPRNdiff = false;
            int counter = 0;
            var DBData = commonpage.GetDBData(propertyKey, queryParameter, _featureContext);
            do
            {
                isUPRNdiff = previousUprn.Equals((string)DBData["uprn"]);
                counter = counter + 1;
                if (counter == 5 || !isUPRNdiff) break;
                DBData = commonpage.GetDBData(propertyKey, queryParameter, _featureContext);
            } while (!isUPRNdiff);

            foreach (var row in table.Rows)
            {
                string fieldName = row["fieldName"];
                try
                {


                    if (_featureContext[fieldName] != null && _featureContext[fieldName] != null)
                    {
                        testData[fieldName] = DBData[fieldName];
                        _featureContext[fieldName] = DBData[fieldName];
                        Console.WriteLine(_featureContext[fieldName]);
                    }

                    if (_scenarioContext[fieldName] != null && _scenarioContext[fieldName] != null)
                    {
                        testData[fieldName] = DBData[fieldName];
                        _scenarioContext[fieldName] = DBData[fieldName];
                        //Console.WriteLine(_scenarioContext[fieldName]);
                    }

                }
                catch (KeyNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    testData[fieldName] = DBData[fieldName];
                    _featureContext[fieldName] = DBData[fieldName];
                    Console.WriteLine(_featureContext[fieldName]);
                    testData[fieldName] = DBData[fieldName];
                    _scenarioContext[fieldName] = DBData[fieldName];
                }
            }
        }

        [Given(@"User connects to the DB and retrieves the '(.*)' set of data for '(.*)'")]
        public void GivenUserConnectsToTheDBAndRetrievesTheSetOfDataFor(int datasetNum, string propertyKey, Table table)
        {
            var testData = inputoutputdata.testData;
            CommonPage commonpage = new CommonPage();
            Dictionary<String, String> DBData = new Dictionary<string, string>();
            int counter = 0;
            if (datasetNum == 1)
            {
                while (DBData.Count == 0)
                {
                    DBData = commonpage.GetDBData(propertyKey);
                    counter = counter + 1;
                    if (counter == 50)
                        break;
                }
            }
            Boolean isSameData = false;
            if (datasetNum != 1)
            {
                while (!isSameData)
                {
                    DBData = commonpage.GetDBData(propertyKey);
                    try
                    {
                        if (_featureContext["uprn"] == DBData["uprn"])
                            isSameData = false;
                        else
                            isSameData = true;
                    }
                    catch (Exception e)
                    {
                        isSameData = false;
                    }
                    counter = counter + 1;
                    if (counter == 50)
                        break;
                }

            }

            foreach (var row in table.Rows)
            {
                string fieldName = row["fieldName"];
                try
                {
                    if (_featureContext[fieldName] != null && _featureContext[fieldName] != null)
                    {
                        testData[fieldName + "_new"] = DBData[fieldName];
                        _featureContext[fieldName + "_new"] = DBData[fieldName];
                        Console.WriteLine(_featureContext[fieldName + "_new"]);
                    }

                    if (_scenarioContext[fieldName] != null && _scenarioContext[fieldName] != null)
                    {
                        testData[fieldName] = DBData[fieldName];
                        _scenarioContext[fieldName] = DBData[fieldName];
                        Console.WriteLine(_scenarioContext[fieldName]);
                    }

                }
                catch (KeyNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    testData[fieldName] = DBData[fieldName];
                    _featureContext[fieldName] = DBData[fieldName];
                    //Console.WriteLine(testData[fieldName]);
                    Console.WriteLine(_featureContext[fieldName]);
                    testData[fieldName] = DBData[fieldName];
                    _scenarioContext[fieldName] = DBData[fieldName];
                    //Console.WriteLine(_scenarioContext[fieldName]);

                }


            }

        }
        [Given(@"User connects to the DB and retrieves the unique VOA change of address '(.*)'")]
        public void GivenUserConnectsToTheDBAndRetrievesTheUniqueVOAChangeOfAddressFindhereditamentForVOAAddress(string propertyKey, Table table)
        {
            var testData = inputoutputdata.testData;
            CommonPage commonpage = new CommonPage();
            var DBData = new Dictionary<string, string>();
            bool UniqueVOAaddFound = false;
            int j = 0;
            while (!UniqueVOAaddFound)
            {
                DBData = commonpage.GetVOA_DBData(propertyKey);
                if (DBData.Count != 0)
                    UniqueVOAaddFound = SeleniumExtensions.validateVOADBdata(DBData);
                else
                    continue;
                if (!UniqueVOAaddFound)
                {
                    j = j + 1;
                }
                if (j == 10)
                    throw new Exception($"After {j} tries,still we are having DB connectivity issue and unable to fecth data for query : {propertyKey}");
            }

            foreach (var row in table.Rows)
            {
                string fieldName = row["fieldName"];
                try
                {
                    if (_featureContext[fieldName] != null && _featureContext[fieldName] != null)
                    {
                        testData[fieldName + "_new"] = DBData[fieldName];
                        _featureContext[fieldName + "_new"] = DBData[fieldName];
                        Console.WriteLine(_featureContext[fieldName + "_new"]);
                    }

                    if (_scenarioContext[fieldName] != null && _scenarioContext[fieldName] != null)
                    {
                        testData[fieldName] = DBData[fieldName];
                        _scenarioContext[fieldName] = DBData[fieldName];
                        Console.WriteLine(_scenarioContext[fieldName]);
                    }

                }
                catch (KeyNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    testData[fieldName] = DBData[fieldName];
                    _featureContext[fieldName] = DBData[fieldName];
                    //Console.WriteLine(testData[fieldName]);
                    Console.WriteLine(_featureContext[fieldName]);
                    testData[fieldName] = DBData[fieldName];
                    _scenarioContext[fieldName] = DBData[fieldName];
                    //Console.WriteLine(_scenarioContext[fieldName]);

                }
            }
        }



        [Given(@"User connects to the DB and retrieves the data for '(.*)'")]
        public void GivenUserConnectsToTheDBAndRetrievesTheData(string propertyKey, Table table)
        {
            var testData = inputoutputdata.testData;
            CommonPage commonpage = new CommonPage();
            var DBData = new Dictionary<string, string>();
            for (int j = 0; j <= 10; j++)
            {
                DBData = commonpage.GetDBData(propertyKey);
                if (DBData.Count != 0)
                    break;
                if (j == 10)
                    throw new Exception($"After {j} tries,still we are having DB connectivity issue and unable to fecth data for query : {propertyKey}");
            }

            foreach (var row in table.Rows)
            {
                string fieldName = row["fieldName"];
                try
                {
                    if (_featureContext[fieldName] != null && _featureContext[fieldName] != null)
                    {
                        testData[fieldName + "_new"] = DBData[fieldName];
                        _featureContext[fieldName + "_new"] = DBData[fieldName];
                        Console.WriteLine(_featureContext[fieldName + "_new"]);
                    }

                    if (_scenarioContext[fieldName] != null && _scenarioContext[fieldName] != null)
                    {
                        testData[fieldName] = DBData[fieldName];
                        _scenarioContext[fieldName] = DBData[fieldName];
                        Console.WriteLine(_scenarioContext[fieldName]);
                    }

                }
                catch (KeyNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    testData[fieldName] = DBData[fieldName];
                    _featureContext[fieldName] = DBData[fieldName];
                    //Console.WriteLine(testData[fieldName]);
                    Console.WriteLine(_featureContext[fieldName]);
                    testData[fieldName] = DBData[fieldName];
                    _scenarioContext[fieldName] = DBData[fieldName];
                    //Console.WriteLine(_scenarioContext[fieldName]);

                }

                //if (_featureContext[fieldName] != null)
                //{
                //    testData[fieldName + "_new"] = DBData[fieldName];
                //    _featureContext[fieldName + "_new"]= DBData[fieldName];
                //    Console.WriteLine(_featureContext[fieldName + "_new"]);
                //}
                //testData[fieldName] = DBData[fieldName];
                //_featureContext[fieldName] = DBData[fieldName];
                //Console.WriteLine(testData[fieldName]);

            }

        }

        [Given(@"User clicked on '(.*)' button on new UI")]
        public void GivenUserClickedOnButtonOnNewUI(string fieldName)
        {
            try
            {
                SeleniumExtensions.scrollToBtnBasedOnLabelAndClick_New(fieldName);
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

        [Given("User clicked on the {string} button")]
        public void GivenUserClickedOnTheButton(string search)
        {
            try
            {
                CommonPage CP = new CommonPage();
                CP.searchbutton();
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

        [Given("User clicked on {string} button under {string} grid")]
        public void GivenUserClickedOnButtonUnderGrid(string buttonName, string sectionName)
        {
            try
            {
                By selector = SeleniumExtensions.getLocator($"//*[@aria-label='{sectionName}']//button[contains(@aria-label,'{buttonName}')]");
                SeleniumExtensions.scrollToBtnEleBasedOnLabelAndClick(selector);
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



        [Given(@"User clicked on '(.*)' button")]
        public void GivenUserClickedOnButton(string fieldName)
        {
            try
            {
                SeleniumExtensions.scrollToBtnBasedOnLabelAndClick(fieldName);
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
            }
            catch (Exception e)
            {
                try
                {
                    if (fieldName.Equals("Create"))
                    {
                        CommonPage commonpage = new CommonPage();
                        commonpage.selectHereditemetStatusRecord("Proposed");
                        SeleniumExtensions.scrollToBtnBasedOnLabelAndClick("Edit");
                    }
                }
                catch (Exception ex)
                {
                    var pdf_Util = new PDF_Utility();
                    pdf_Util.takeScreenshot();
                    pdf_Util.exceptionPdFLogger(e);
                }
            }

        }
        [Given(@"User selects '(.*)' from '(.*)' this dropdown")]
        public void GivenUserSelectsFromThisDropdown(string optionText, string dropdownLabel)
        {
            waitHelpers wh = new waitHelpers();
            By dropdownLocator = By.CssSelector($"div[id*='{dropdownLabel}'] select");
            wh.isElementDisplayed(dropdownLocator, 10);
            IWebElement dropdownElement = wh.GetWebElement(dropdownLocator);
            dropdownElement.SelectElementByText(optionText);
        }

        [Given(@"User selects '(.*)' from '(.*)' this dropdown and validate value")]
        public void GivenUserSelectsFromThisDropdownAndValidateValue(string optionText, string dropdownLabel)
        {
            waitHelpers wh = new waitHelpers();
            By dropdownLocator = By.CssSelector($"div[id*='{dropdownLabel}'] select");
            wh.isElementDisplayed(dropdownLocator, 10);
            IWebElement dropdownElement = wh.GetWebElement(dropdownLocator);
            dropdownElement.SelectElementByText_BARef(optionText);
        }




        [Given(@"User clicked on '(.*)' button field")]
        public void GivenUserClickedOnButtonField(string fieldName)
        {

            {
                try
                {
                    SeleniumExtensions.scrollToBtnBasedOnLabelAndClick_recon(fieldName);
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



        [Given(@"User clicked '(.*)' button on '(.*)' section")]
        public void GivenUserClickedButtonOnSection(string fieldName, string sectioName)
        {
            try
            {
                SeleniumExtensions.scrollToBtnBasedOnLabelAndClick(fieldName, sectioName);
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


        [Given(@"User clicked on '(.*)' button under '(.*)'")]
        public void GivenUserClickedOnButtonUnder(string buttonName, string tabName)
        {
            waitHelpers wh = new waitHelpers();
            By buttonEleBy = SeleniumExtensions.getLocator($"ul[aria-label*='{tabName}'] button[aria-label *='{buttonName}']");
            wh.clickOnElement(buttonEleBy);
        }


        [Given(@"User clicked on '(.*)' button on hereditament dialog")]
        public void GivenUserClickedOnButtonOnHereditamentDialog(string fieldName)
        {
            try
            {
                SeleniumExtensions.scrollToBtnBasedOnLabelAndClickOnHereditamentDialog(fieldName);
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


        [Given(@"User clicked on (.*) position '(.*)' button")]
        public void GivenUserClickedOnPositionButton(int position, string fieldName)
        {
            try
            {
                SeleniumExtensions.scrollToBtnBasedOnLabelAndClick(fieldName, position);
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


        [Given(@"User clicked on '(.*)' BPF Journey button")]
        public void GivenUserClickedOnBPFJourneyButton(string fieldName)
        {
            try
            {
                SeleniumExtensions.WaitForReadyStateToComplete();
                SeleniumExtensions.scrollToBtnBasedOnLabelAndJSClick(fieldName);
                SeleniumExtensions.WaitForReadyStateToComplete();
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


        [Given(@"User clicked on '(.*)' button element")]
        public void GivenUserClickedOnButtonElement(string fieldName)
        {
            SeleniumExtensions.scrollToBtnEleAndClick(fieldName);
        }


        [Given(@"User click on '(.*)' toggle button")]
        public void GivenUserClickOnToggleButton(string fieldName)
        {
            SeleniumExtensions.scrollToToggleBtnBasedOnLabelAndClick(fieldName);
        }

        [Given(@"User click on '(.*)' toggle button on dialog")]
        public void GivenUserClickOnToggleButtonOnDialog(string fieldName)
        {
            SeleniumExtensions.scrollToToggleBtnonDialogBasedOnLabelAndClick(fieldName);
        }

        [Given(@"User fetch '(.*)' in '(.*)'")]
        public void GivenUserFetchIn(string requestName, string p1)
        {
            try
            {
                CommonPage cp = new CommonPage();
                _featureContext[requestName] = cp.getRequestName();
            }
            catch (Exception e)
            {
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
                pdf_Util.exceptionPdFLogger(e);
            }

        }


        [Given(@"User waits till '(.*)' becomes '(.*)' by '(.*)' under '(.*)'")]
        public void GivenUserWaitsTillBecomesByUnder(string KPI_Name, string KPI_Status, string SLARefreshBtn, string gridName)
        {
            try
            {
                CommonPage cp = new CommonPage();
                SeleniumExtensions.scrollToBtnElement(gridName);
                cp.isKPINonCompliantDisplayed(KPI_Name, KPI_Status, SLARefreshBtn);

            }
            catch (Exception e)
            {
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
                pdf_Util.exceptionPdFLogger(e);
            }
        }

        [Given(@"User validate '(.*)' value for '(.*)' field")]
        public void GivenUserValidateValue_orField(string value, string fieldName)
        {
            try
            {
                Assert.IsTrue(SeleniumExtensions.validate_FieldWithValue(value, fieldName).Displayed);
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


        [Given(@"User validate value '(.*)' for '(.*)' field")]
        public void GivenUserValidateValueForField(string value, string fieldName)
        {
            try
            {
                Assert.IsTrue(SeleniumExtensions.validateFieldWithValue_(value, fieldName));
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


        [Given(@"User validate '(.*)' value display for '(.*)' from feature context")]
        public void GivenUserValidateValueDisplayForFromFeatureContext(string valuefield, string fieldName)
        {
            try
            {
                Assert.IsTrue(SeleniumExtensions.validateFieldWithValue(valuefield, fieldName, _featureContext).Displayed);
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




        [Given(@"User validate '(.*)' status of '(.*)'")]
        public void GivenUserValidateStatusOf(string status, string level)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                try
                {
                    Assert.IsTrue(commonpage.assertStatus(status, level));
                }
                catch (Exception e)
                {
                    Assert.IsTrue(commonpage.assertStatus(status, level));
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

        [Given(@"user waits for '(.*)' secs")]
        public void GivenUserWaitsForSecs(int numOfSecs)
        {
            Thread.Sleep(numOfSecs * 1000);
            SeleniumExtensions.WaitForReadyStateToComplete();
        }


        [Given(@"User clicks on '(.*)' button on dialog")]
        public void GivenUserClicksOnButtonOnDialog(string buttonName)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                commonpage.clickButtonOnDialog(buttonName);
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

        [Given(@"User get all Supplementary Job details in '(.*)'")]
        public void GivenUserGetAllSupplementaryJobDetailsIn(string storageContext)
        {
            try
            {
                RequestPage rp = new RequestPage();
                String jobName = (String)_featureContext["Job Name"];
                String jobdetails = rp.getSupplementaryJobDetails(jobName);

                if (storageContext.Equals("scenarioContext", StringComparison.InvariantCultureIgnoreCase))
                {
                    _scenarioContext["Supplementary Job Name"] = jobdetails;
                    //String JobName = (string)_scenarioContext["Supplementary Job Name"];
                }
                if (storageContext.Equals("featureContext", StringComparison.InvariantCultureIgnoreCase))
                {
                    _featureContext["Supplementary Job Name"] = jobdetails;
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

        [Given(@"User gets Associated job details in '(.*)'")]
        public void GivenUserGetsAssociatedJobDetailsIn(string storageContext)
        {
            try
            {
                RequestPage rp = new RequestPage();
                String jobName = (String)_featureContext["Job Name"];
                List<String> Associatedjobdetails = rp.getTheAssociatedJobsDetails();
                List<string> supplementoryJobLi = Associatedjobdetails.Where(otherJobNames => otherJobNames != jobName).ToList();
                string suplimentoryJob = supplementoryJobLi.First();
                if (storageContext.Equals("scenarioContext", StringComparison.InvariantCultureIgnoreCase))
                {
                    _scenarioContext["Supplementary Job Name"] = suplimentoryJob;
                }
                if (storageContext.Equals("featureContext", StringComparison.InvariantCultureIgnoreCase))
                {
                    _featureContext["Supplementary Job Name"] = suplimentoryJob;
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



        [Given(@"User get Supplementary Job details in '(.*)'")]
        public void GivenUserGetSupplementaryJobDetailsIn(string storageContext)
        {
            try
            {
                RequestPage rp = new RequestPage();
                String jobName = (String)_featureContext["Job Name"];
                String jobdetails = rp.getSupplementaryJobDetails(jobName);

                if (storageContext.Equals("scenarioContext", StringComparison.InvariantCultureIgnoreCase))
                {
                    _scenarioContext["Supplementary Job Name"] = jobdetails;
                    //String JobName = (string)_scenarioContext["Supplementary Job Name"];
                }
                if (storageContext.Equals("featureContext", StringComparison.InvariantCultureIgnoreCase))
                {
                    _featureContext["Supplementary Job Name"] = jobdetails;
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
        [Given(@"User get Supplementary Job details in the '(.*)'")]
        public void GivenUserGetSupplementaryJobDetailsInThe(string storageContext)
        {
            try
            {
                RequestPage rp = new RequestPage();
                String jobName = (String)_featureContext["Job Name"];
                String jobdetails = rp.getSupplementaryJobDetails_new(jobName);

                if (storageContext.Equals("scenarioContext", StringComparison.InvariantCultureIgnoreCase))
                {
                    _scenarioContext["Supplementary Job Name"] = jobdetails;
                    //String JobName = (string)_scenarioContext["Supplementary Job Name"];
                }
                if (storageContext.Equals("featureContext", StringComparison.InvariantCultureIgnoreCase))
                {
                    _featureContext["Supplementary Job Name"] = jobdetails;
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


        [Given(@"User capture all (.*) Jobs ""(.*)"" and ""(.*)"" in ""(.*)"" by ""(.*)"" grid")]
        public void GivenUserCaptureAllJobsAndInByGrid(int numberOfJobs, string jobID, string jobName, string storageContext, string RefreshBtn)
        {
            try
            {
                RequestPage rp = new RequestPage();
                Dictionary<String, String> jobdetails = rp.UserCaptureAllJobNames(numberOfJobs, jobID, jobName, _featureContext, RefreshBtn);
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



        [Given(@"User captures all (.*) Jobs ""(.*)"" and ""(.*)"" in ""(.*)"" by ""(.*)"" grid")]
        public void GivenUserCapturesAllJobsAndInByGrid(int numberOfJobs, string jobID, string jobName, string storageContext, String RefreshBtn)
        {
            try
            {
                RequestPage rp = new RequestPage();
                Dictionary<String, String> jobdetails = rp.UserCaptureAllJobDetails_New(numberOfJobs, jobID, jobName, _featureContext, RefreshBtn);
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

        [Given(@"User waits till all (.*) jobs has name displayed by ""(.*)"" grid")]
        public void GivenUserWaitsTillAllJobsHasNameDisplayedByGrid(int numberOfJobs, string RefreshBtn)
        {
            try
            {
                RequestPage rp = new RequestPage();
                Dictionary<String, String> jobdetails = rp.UserWaitsTillAllJobNamesDisplay(numberOfJobs, RefreshBtn);
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



        [Given(@"User captures all (.*) recon ""(.*)"" and ""(.*)"" in ""(.*)"" by ""(.*)"" grid")]
        public void GivenUserCapturesAllReconAndInByGrid(int numberOfJobs, string jobID, string jobName, string storageContext, String RefreshBtn)
        {
            try
            {
                RequestPage rp = new RequestPage();
                Dictionary<String, String> jobdetails = rp.UserCaptureAllJobDetails(numberOfJobs, jobID, jobName, _featureContext, RefreshBtn);
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


        [Given(@"User captured all (.*) recons ""(.*)"" and ""(.*)"" in ""(.*)"" by ""(.*)"" grid")]
        public void GivenUserCapturedAllReconsAndInByGrid(int numberOfJobs, string jobID, string jobName, string storageContext, String RefreshBtn)
        {
            try
            {
                RequestPage rp = new RequestPage();
                Dictionary<String, String> jobdetails = rp.UserCapturedAllJobDetails(numberOfJobs, jobID, jobName, _featureContext, RefreshBtn);
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
        [Given("User captures the {string} in {string}")]
        public void GivenUserCapturesTheIn(string name, string storageContext)
        {

            var requestPage = new RequestPage();
            //String uprnValue = (string)_featureContext[uprn];
            String miloid = requestPage.milodetails(name, _scenarioContext);
            Console.WriteLine("Unique ID : " + miloid);
            if (storageContext.Equals("scenarioContext", StringComparison.InvariantCultureIgnoreCase))
            {
                _scenarioContext["MiloID"] = miloid;
            }
            if (storageContext.Equals("featureContext", StringComparison.InvariantCultureIgnoreCase))
            {
                _featureContext["MiloID"] = miloid;
            }

        }
        [Given("User captures {string} in {string}")]
        public void GivenUserCapturesIn(string jobID, string storageContext)
        {

            try
            {
                RequestPage rp = new RequestPage();
                Dictionary<String, String> jobdetails = rp.UserCaptureJobDetails_abort(jobID, storageContext);
                if (storageContext.Equals("featureContext", StringComparison.InvariantCultureIgnoreCase))
                {
                    _featureContext[jobID] = jobdetails[jobID];
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
        [Given(@"User captures ""(.*)"" and ""(.*)"" in ""(.*)"" by ""(.*)"" grid")]
        public void GivenUserCapturesAndIn(string jobID, string jobName, string storageContext, String RefreshBtn)
        {
            try
            {
                RequestPage rp = new RequestPage();
                Dictionary<String, String> jobdetails = rp.UserCaptureJobDetails(jobID, jobName, storageContext, RefreshBtn);

                if (storageContext.Equals("scenarioContext", StringComparison.InvariantCultureIgnoreCase))
                {
                    _scenarioContext[jobID] = jobdetails[jobID];
                    _scenarioContext[jobName] = jobdetails[jobName];
                    String JobName = (string)_scenarioContext[jobName];
                    Debug.WriteLine("Job ID : " + _scenarioContext[jobID]);
                    Debug.WriteLine("Job Name : " + _scenarioContext[jobName]);

                }
                if (storageContext.Equals("featureContext", StringComparison.InvariantCultureIgnoreCase))
                {
                    _featureContext[jobID] = jobdetails[jobID];
                    if (Config.EnvironmentVal != "DEV")
                        _featureContext[jobName] = jobdetails[jobName];

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

        [Given(@"User captures ""(.*)"" in ""(.*)"" from estate files grid")]
        public void GivenUserCapturesInFromEstateFilesGrid(string fieldName, string storageContext)
        {
            RequestPage rp = new RequestPage();
            Dictionary<String, String> estateFileDetails = rp.UserCaptureEstateDetails(fieldName, storageContext);
            try
            {
                if (storageContext.Equals("scenarioContext", StringComparison.InvariantCultureIgnoreCase))
                {
                    _scenarioContext[fieldName] = estateFileDetails[fieldName];
                    Debug.WriteLine("Estate File Name : " + _scenarioContext[fieldName]);
                }
                if (storageContext.Equals("featureContext", StringComparison.InvariantCultureIgnoreCase))
                {
                    _featureContext[fieldName] = estateFileDetails[fieldName];
                    Debug.WriteLine("Estate File Name : " + _scenarioContext[fieldName]);
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


        [Given(@"User captures correspondence ""(.*)"" in ""(.*)"" by ""(.*)"" grid")]
        public void GivenUserCapturesCorrespondenceInByGrid(string jobID, string storageContext, string RefreshBtn)
        {
            try
            {
                RequestPage rp = new RequestPage();
                Dictionary<String, String> jobdetails = rp.UserCaptureJobDetails(jobID, storageContext, RefreshBtn);

                if (storageContext.Equals("scenarioContext", StringComparison.InvariantCultureIgnoreCase))
                {
                    _scenarioContext["Correspondence" + jobID] = jobdetails["Correspondence" + jobID];
                    Debug.WriteLine("Job ID : " + _scenarioContext["Correspondence" + jobID]);
                }
                if (storageContext.Equals("featureContext", StringComparison.InvariantCultureIgnoreCase))
                {
                    _featureContext["Correspondence" + jobID] = jobdetails["Correspondence" + jobID];
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





        [Given(@"User click on correspondence ""(.*)"" element")]
        public void GivenUserClickOnCorrespondenceElement(string p0)
        {
            try
            {
                var requestPage = new RequestPage();
                requestPage.clickOnCorrespondenceJobID();
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

        [Given(@"user waits till Spinner disappears")]
        public void GivenUserWaitsTillSpinnerDisappears()
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                commonpage.waitTillSpinnerDisaddpears();
                Thread.Sleep(2000);
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

        [Given(@"User validates '(.*)' contains '(.*)'")]
        public void GivenUserValidatesContains(string fieldName, string msg)
        {
            try
            {
                waitHelpers wh = new waitHelpers();
                String eleLoc = $"//textarea[@aria-label='{fieldName}' and contains(text(),'{msg}')]";
                Assert.IsTrue(wh.isElementDisplayed(By.XPath(eleLoc), 60));
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


        [Given(@"user waits till '(.*)' '(.*)' disappears")]
        public void GivenUserWaitsTillDisappears(string label, string roleType)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                //commonpage.waitTillSavingDisaddpears(label, roleType);
                commonpage.waitTillSavingDisaddpears();
                Thread.Sleep(2000);
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

        [Given(@"user waits till '(.*)' '(.*)' disappears and validate no check acceptance")]
        public void GivenUserWaitsTillDisappearsAndValidateNoCheckAcceptance(string label, string roleType)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                commonpage.waitTillSavingDisaddpearswithnocheck(label, roleType);
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



        [Given(@"user waits till '(.*)' disappears on dialog")]
        public void GivenUserWaitsTillDisappearsOnDialog(string label)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                commonpage.waitTillSavingDisaddpearsDialog(label);
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



        [Given(@"User waits till record gets '(.*)'")]
        public void GivenUserWaitsTillRecordGets(string label)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                commonpage.waitTillRecordSaved(label);
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


        [Given(@"user wait till '(.*)' '(.*)' disappears")]
        public void GivenUserWaitTillDisappears(string label, string roleType)
        {
            CommonPage commonpage = new CommonPage();
            commonpage.waitTillSavingOnlyDisaddpearsMilliSec(label, roleType);
        }


        [Given(@"user waits till '(.*)' '(.*)' disappears and wait till record gets '(.*)'")]
        public void GivenUserWaitsTillDisappearsAndWaitTillRecordGets(string label, string roleType, string savedLabel)
        {
            CommonPage commonpage = new CommonPage();
            commonpage.waitTillSavingDisaddpears(label, roleType, savedLabel);
        }

        [Given(@"user waits till progress indicator disappears")]
        public void GivenUserWaitsTillProgressIndicatorDisappears()
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                commonpage.waitTillappProgressIndicatorDisaddpears_();
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

        [Given(@"user waits till app progress indicator disappears")]
        public void GivenUserWaitsTillAppProgressIndicatorDisappears()
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                commonpage.waitTillappProgressIndicatorDisaddpears_();
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


        [Given(@"user waits till '(.*)' icon disappears")]
        public void GivenUserWaitsTillIconDisappears(string labelName)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                commonpage.waitTillunlockstageProgressIndicatorDisaddpears(labelName);
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



        [Given(@"user waits till loading spinner disappears")]
        public void GivenUserWaitsTillloadingSpinnerDisappears()
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                commonpage.waitTillSpinnerDisable();
                //waitTillappProgressIndicatorDisaddpears();
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



        [Given(@"User waits till Find Hereditament dialog disappears")]
        public void GivenUserWaitsTillFindHereditamentDialogDisappears()
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                commonpage.waitTillFindHeridetamentDisaddpears();
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

        [Given(@"User waits till '(.*)' disappears")]
        public void GivenUserWaitsTillDisappears(string roleType)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                commonpage.waitTillDialogDisaddpears(roleType);
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

        [Given(@"User closes '(.*)' dialog if still present")]
        public void GivenUserClosesDialogIfStillPresent(string title)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                commonpage.closeDialogIfPresent(title);
            }
            catch (ElementClickInterceptedException e)
            {
                CommonPage commonpage = new CommonPage();
                commonpage.waitTillappProgressIndicatorDisaddpears_();
                commonpage.waitTillSavingDisaddpears_Old();
                commonpage.closeDialogIfPresent(title);
            }

            catch (Exception e)
            {
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
                pdf_Util.exceptionPdFLogger(e);
                throw new Exception("Exception details : " + e.Message);
            }
        }
        [Given(@"User closed '(.*)' dialog if still present")]
        public void GivenUserClosedDialogIfStillPresent(string dialogTitle)
        {
            try
            {
                try
                {
                    CommonPage commonpage = new CommonPage();
                    commonpage.closeDialogWithTitle(dialogTitle);
                }
                catch (ElementClickInterceptedException e)
                {
                    CommonPage commonpage = new CommonPage();
                    commonpage.waitTillappProgressIndicatorDisaddpears_();
                    commonpage.waitTillSavingDisaddpears();
                    commonpage.closeDialogWithTitle(dialogTitle);
                }
            }

            catch (Exception e)
            {
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
                pdf_Util.exceptionPdFLogger(e);
            }
        }


        [Given(@"User closes dialog if still present")]
        public void GivenUserClosesDialogIfStillPresent()
        {
            try
            {
                try
                {
                    CommonPage commonpage = new CommonPage();
                    commonpage.closeDialogIfPresent();
                }
                catch (ElementClickInterceptedException e)
                {
                    CommonPage commonpage = new CommonPage();
                    commonpage.waitTillappProgressIndicatorDisaddpears_();
                    commonpage.waitTillSavingDisaddpears();
                    commonpage.closeDialogIfPresent();
                }
            }

            catch (Exception e)
            {
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
                pdf_Util.exceptionPdFLogger(e);
            }
        }


        [Given(@"User waits till ProgressRing disappears")]
        public void GivenUserWaitsTillProgressRingDisappears()
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                commonpage.waitTillProgressBarDisaddpears();
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


        [Given(@"User validate below business stages on business journey header")]
        public void GivenUserValidateBelowBusinessStagesOnBusinessJourneyHeader(Table table)
        {
            try
            {
                SeleniumExtensions.WaitForReadyStateToComplete();
                CommonPage commonpage = new CommonPage();
                List<String> businessStageDetails = commonpage.getBusinessStagesDetails();
                Assert.IsTrue(table.RowCount == businessStageDetails.Count());
                foreach (var row in table.Rows)
                {
                    string fieldName = row["BusinessStages"];
                    Assert.IsTrue(businessStageDetails.Contains(fieldName));
                }
                SeleniumExtensions.WaitForReadyStateToComplete();
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

        [Given(@"User validates questionnaire in the '(.*)' stage")]
        public void GivenUserValidatesQuestionnaireInTheStage(string stageName, Table table)
        {
            CommonPage commonpage = new CommonPage();
            SeleniumExtensions.WaitForReadyStateToComplete();
            List<String> questionaryDetails = commonpage.getQuessionairyDetails();
            int questionCount = questionaryDetails.Count();
            int tableCount = table.RowCount;
            Assert.IsTrue(table.RowCount == questionaryDetails.Count());
            foreach (var row in table.Rows)
            {
                string fieldName = row["Questions"];
                Assert.IsTrue(questionaryDetails.Contains(fieldName));
            }
            SeleniumExtensions.SwitchToDefaultFrame();
        }

        [Given("User clicked on {string} tab under {string} for proposal")]
        public void GivenUserClickedOnTabUnderForProposal(string tabName, string researchForm)
        {
            try
            {
                SeleniumExtensions.WaitForPageLoad();
                CommonPage commonpage = new CommonPage();
                //commonpage.clickOnDataResearchFormTab(tabName, researchForm);
                commonpage.clickOnTab_proposal(tabName, researchForm);
                SeleniumExtensions.WaitForPageLoad();
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

        [Given(@"User validated under '(.*)' '(.*)' template generated")]
        public void GivenUserValidatedUnderTemplateGenerated(string tabName, string templateName)
        {
            try
            {
                SeleniumExtensions.WaitForPageLoad();
                CommonPage commonpage = new CommonPage();
                commonpage.validateOutboundCorrespondanceTemplate(templateName);
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
        [Given("User click on {string} tab")]
        public void GivenUserClickOnTab(string tabName)
        {
            ValidateAndClickOnPVTTab(tabName);
        }

        [Given(@"User validates and clicks the '(.*)' tab present in the PVT tablist")]
        public void GivenUserValidatesAndClicksTheTabPresentInThePVTTablist(string tabName)
        {
            ValidateAndClickOnPVTTab(tabName);
            ChickOnChevron();
        }
        [Given("User validate milorecord and click on {string} transaction")]
        public void GivenUserValidateMilorecordAndClickOnTransaction(string buttonname)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                string Milovalue = null;
                Milovalue = (string)_scenarioContext["MiloID"];
                commonpage.validateandunlink(Milovalue, _scenarioContext, buttonname);
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


        [Given(@"User get PAD attributes of '(.*)' record")]
        public void GivenUserGetPADAttributesOfRecord(string heridetementStatus)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                Dictionary<String, String> padAttributes = commonpage.getPADattributesOfStatusRecord(heridetementStatus);
                foreach (var item in padAttributes)
                {
                    Console.WriteLine($"{item.Key} : {item.Value}");
                    _featureContext[item.Key] = item.Value;
                }
            }
            catch (Exception e)
            {
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
                pdf_Util.exceptionPdFLogger(e);
            }
        }



        [Given(@"User selects '(.*)' record")]
        public void GivenUserSelectsRecord(string heridetementStatus)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                commonpage.selectHereditemetStatusRecord(heridetementStatus);
            }
            catch (Exception e)
            {
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
                pdf_Util.exceptionPdFLogger(e);
            }
        }
        [Given("User selects {string} record row")]
        public void GivenUserSelectsRecordRow(string heridetementStatus)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                commonpage.selectHereditemetStatusRecord_Milo(heridetementStatus);
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

        [Given(@"User captures ""(.*)"" in '(.*)' selects the '(.*)' checkbox and click on '(.*)' button")]
        public void GivenUserCapturesInSelectsTheCheckboxAndClickOnButton(string Modifiedband, string p3, string Status, string Assessment)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                _featureContext[Modifiedband] = commonpage.selectassessment(Status, Assessment);

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



        [Given(@"User clicked on expand pvt")]
        public void GivenUserClickedOnExpandPvt()
        {
            CommonPage commonpage = new CommonPage();
            commonpage.expandPVTs();

        }

        [Given(@"User clicks (.*) position '(.*)' button on '(.*)' dialog")]
        public void GivenUserClicksPositionButtonOnDialog(int position, string btnName, string DialogTitle)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                commonpage.clickEleOnDialog(position, btnName, DialogTitle);
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


        [Given(@"User clicks on '(.*)' button on '(.*)' dialog")]
        public void GivenUserClicksOnButtonOnDialog(string btnName, string DialogTitle)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                commonpage.clickEleOnDialog(btnName, DialogTitle);
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


        [Given(@"User clicks on '(.*)' button on validate on the job dialog")]
        public void GivenUserClicksOnButtonOnconfirmDialog(string buttonText)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                commonpage.clickEleOnconfirmDialog(buttonText);
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

        [Given(@"User clicked on the '(.*)' button on '(.*)' dialog")]
        public void GivenUserClickedOnTheButtonOnDialog(string DialogTitle, string btnName)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                commonpage.clickconfirmbutton(btnName, DialogTitle);
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



        [Given(@"User clicks on '(.*)' button on '(.*)' dialog,if appears")]
        public void GivenUserClicksOnButtonOnDialogIfAppears(string btnName, string DialogTitle)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                commonpage.clickEleOnDialogIfappears(btnName, DialogTitle);
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


        [Given(@"User switchs to Assessment frame")]
        public void GivenUserSwitchsToAssessmentFrame()
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                commonpage.switchToAssessmentFrame();
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

        [Given(@"User asserts '(.*)' assessments row count more than '(.*)'")]
        public void GivenUserAssertsAssessmentsRowCountMoreThan(string assessmentName, int rowSize)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                Assert.IsTrue(commonpage.getRowCount() > rowSize);
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
        [Given(@"User asserts below '(.*)' details for Welsh Hereditament")]
        public void GivenUserAssertsBelowDetailsForWelshHereditament(string tabname, Table table)
        {
            try
            {
                MaintainAssessmentPage map = new MaintainAssessmentPage();
                map.validatedAssessmentwelsh(_featureContext, table);
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
        [Given(@"User asserts below '(.*)' details")]
        public void GivenUserAssertsBelowDetails(string typeOfTab, Table table)
        {
            try
            {
                MaintainAssessmentPage map = new MaintainAssessmentPage();
                if (typeOfTab.Equals("Billing Authority Links"))
                {
                    map.validatedBillingAuthorityLinks(_featureContext, table);
                }
                else
                {
                    map.validatedAssessment(_featureContext, table);
                }
            }
            catch (Exception e)
            {
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
                pdf_Util.exceptionPdFLogger(e);
            }
            //foreach (var row in table.Rows)
            //{
            //    string EffectiveFromDays = row["EffectiveFrom"];
            //    string EffectiveToDays = row["EffectiveTo"];
            //    string TaxBand = row["TaxBand"];
            //    string CompIndicator = row["CompIndicator"];
            //    string AssessmentStatus = row["AssessmentStatus"];
            //    string PublicationStatus = row["PublicationStatus"];
            //    string AssessmentAction = row["AssessmentAction"];

            //    String assesmentXpath = "//*[@data-automationid='DetailsRowCell']//*[text()='2/24/2025'] //ancestor::div[@role='row'] //*[@data-automationid='DetailsRowCell']//*[text()=''] //ancestor::div[@role='row'] //*[@data-automationid='DetailsRowCell']//*[text()='A'] //ancestor::div[@role='row'] //*[@data-automationid='DetailsRowCell']//*[text()='No'] //ancestor::div[@role='row'] //*[@data-automationid='DetailsRowCell']//*[contains(text(),'Current (live entry)')] //ancestor::div[@role='row'] //*[@data-automationid='DetailsRowCell']//*[text()='Published'] //ancestor::div[@role='row'] //*[@data-automationid='DetailsRowCell']//*[text()='Alteration']";
            //}
        }

        [Given(@"User asserts below '(.*)' details and validate effetive to date")]
        public void GivenUserAssertsBelowDetailsAndValidateEffetiveToDate(string typeOfTab, Table table)
        {
            try
            {
                MaintainAssessmentPage map = new MaintainAssessmentPage();
                if (typeOfTab.Equals("Billing Authority Links"))
                {
                    map.validatedBillingAuthorityLinks_ETD(_featureContext, table);
                }
                else
                {
                    map.validatedAssessment(_featureContext, table);
                }
            }
            catch (Exception e)
            {
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
                pdf_Util.exceptionPdFLogger(e);
            }
        }



        [Given(@"user asserts '(.*)' row count '(.*)'")]
        public void GivenUserAssertsRowCount(string assessmentName, int rowSize)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                Assert.IsTrue(commonpage.getRowCount() == rowSize);
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

        [Given(@"user asserts related jobs row count '(.*)'")]
        public void GivenUserAssertsRelatedJobsRowCount(int rowSize)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                Assert.IsTrue(commonpage.getJobIdRowCount() == rowSize);
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

        [Given(@"user asserts associated jobs row count '(.*)' by ""(.*)"" grid")]
        public void GivenUserAssertsAssociatedJobsRowCountByGrid(int rowSize, string refreshBtn)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                Assert.IsTrue(commonpage.assertAssosiatedJobs(rowSize, refreshBtn));
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
            }
            catch (Exception e)
            {
                CommonPage commonpage = new CommonPage();
                commonpage.clickEleOnDialog("OK", "Error");
                Assert.IsTrue(commonpage.assertAssosiatedJobs(rowSize, refreshBtn));
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
                // pdf_Util.exceptionPdFLogger(e);
            }
        }
        [Given(@"user asserts Proposed BA Reference Amendments  row count '(.*)' by ""(.*)"" grid")]
        public void GivenUserAssertsProposedBAReferenceAmendmentsRowCountByGrid(int rowSize, string refreshBtn)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                Assert.IsTrue(commonpage.assertProposedBAReference(rowSize, refreshBtn));
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


        [Given(@"user asserts related jobs row count '(.*)' by ""(.*)"" grid")]
        public void GivenUserAssertsRelatedJobsRowCountByGrid(int rowSize, string refreshBtn)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                Assert.IsTrue(commonpage.assertRelatedJobs(rowSize, refreshBtn));
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





        [Given(@"user selects '(.*)' Assessment and clicked on '(.*)' button")]
        public void GivenUserSelectsAssessmentAndClickedOnButton(int rowNum, string btnName)
        {
            CommonPage commonpage = new CommonPage();
            commonpage.userSelectAssesment(rowNum);
            Thread.Sleep(5000);
            SeleniumExtensions.scrollToBtnBasedOnLabelAndClick(btnName);
        }

        [Given(@"User asserts '(.*)' value as '(.*)'")]
        public void GivenUserAssetsValueAs(string fieldName, string value)
        {
            Assert.IsTrue(SeleniumExtensions.validateFieldWithTitle(fieldName, value).Displayed);
        }

        [Given(@"User validates '(.*)' value as '(.*)' on '(.*)'")]
        public void GivenUserAssetsValueAsOnDialog(string fieldName, string value, string type)
        {
            Assert.IsTrue(SeleniumExtensions.validateFieldWithTitle(fieldName, value, type).Displayed);
        }

        [Given("User asserts {string} is linked with assement {string}")]
        public void GivenUserAssertsIsLinkedWithAssement(string fieldName, string condition)
        {
            CommonPage cp = new CommonPage();
            cp.validateFieldValue(fieldName, condition);
            var pdf_Util = new PDF_Utility();
            pdf_Util.takeScreenshot();
        }

        [Given(@"User switchs to deafult frame")]
        public void GivenUserSwitchsToDeafultFrame()
        {
            SeleniumExtensions.SwitchToDefaultFrame();
        }

        [Given("User closes business process stage container,if still available")]
        public void GivenUserClosesBusinessProcessStageContainerIfStillAvailable()
        {
            try
            {
                var bpfPage = new BPFPage();
                bpfPage.closeBPFcontainerIfStillOpen();
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


        [Given(@"User closes business process stage container")]
        public void GivenUserClosesBusinessProcessStageContainer()
        {
            try
            {
                var bpfPage = new BPFPage();
                bpfPage.closeBPFcontainer();
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

        [Given(@"User click on '(.*)' button from '(.*)' untill '(.*)' stage selected")]
        public void GivenUserClickOnButtonFromUntillStageSelected(string buttonName, string roleType, string stageName)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                commonpage.isStageSelected(buttonName, stageName);
                SeleniumExtensions.WaitForReadyStateToComplete();
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


        [Given(@"User click on '(.*)' button from '(.*)' untill '(.*)' tab display")]
        public void GivenUserClickOnButtonFromUntillTabDisplay(string buttonName, string section, string tabName)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                commonpage.isTabDisplayed(buttonName, tabName);
                SeleniumExtensions.WaitForReadyStateToComplete();
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

        [Given(@"User '(.*)' from '(.*)' until '(.*)' field set to '(.*)'")]
        public void GivenUserFromUntilFieldSetTo(string buttonName, string menuBar, string fieldName, string status)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                commonpage.isFieldStatusDislayed(buttonName, fieldName, status);
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
        [Given("verify status reason as {string} in the property link page")]
        public void GivenVerifyStatusReasonAsInThePropertyLinkPage(string status)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                commonpage.userValidatestatus(status);
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


        [Given(@"User click on '(.*)' button from '(.*)' untill '(.*)' status display")]
        public void GivenUserClickOnButtonFromUntillStatusDisplay(string buttonName, string Sction, string status)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                commonpage.isStatusDisplayed(buttonName, status);
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

        [Given("User click on {string} button from {string} untill {string} has value display")]
        public void GivenUserClickOnButtonFromUntillHasValueDisplay(string buttonName, string menubar, string succeedingRequest)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                commonpage.isSucceedingRequestDisplayed(buttonName, succeedingRequest);
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

        [Given(@"User click on the '(.*)' button from '(.*)' clicks on '(.*)' button on '(.*)' dialog until '(.*)' status is displayed")]
        public void GivenUserClickOnTheButtonFromClicksOnButtonOnDialogUntilStatusIsDisplayed(string refreshBtn, string Sction, string status, string btnName, string DialogTitle)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                commonpage.isStatusDisplayed_new(refreshBtn, btnName, DialogTitle, status);
                //commonpage.clickEleOnDialog(btnName, DialogTitle);
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

        [Given(@"User click on '(.*)' button from '(.*)' untill '(.*)' status of '(.*)' display")]
        public void GivenUserClickOnButtonFromUntillStatusOfDisplay(string buttonName, string section, string status, string stage)
        {
            CommonPage commonpage = new CommonPage();
            commonpage.isStatusDisplayed(buttonName, status);
            SeleniumExtensions.WaitForReadyStateToComplete();
        }




        [Given(@"User looked for '(.*)' and reduced (.*) step from ""(.*)"" band value")]
        public void GivenUserLookedForAndReducedStepFromBandValue(string fieldName, int stepCount, string fieldName1)
        {
            try
            {
                var testData = inputoutputdata.testData;
                RequestPage rp = new RequestPage();
                CommonPage commonpage = new CommonPage();
                waitHelpers wh = new waitHelpers();
                SeleniumExtensions.scrollToBtnElement(fieldName1);
                String proposedBand = null;
                Thread.Sleep(3 * 2000);
                String currentTaxBand = wh.GetWebElement(By.XPath("//div[contains(@data-id,'voa_initialcounciltaxbandid.fieldControl-LookupResultsDropdown_voa_initialcounciltaxbandid_selected_tag_text')]")).Text;
                Console.WriteLine("currentTaxBand : " + currentTaxBand);
                if (currentTaxBand == "Deleted")
                {
                    proposedBand = commonpage.EnterDataInSingleCharLookUpField(fieldName, "A");
                }
                else
                {
                    proposedBand = commonpage.decrementCharacter(currentTaxBand, stepCount);
                    SeleniumExtensions.scrollToBtnElement(fieldName);
                    commonpage.EnterDataInSingleCharLookUpField(fieldName, proposedBand);

                }
                Console.WriteLine("proposedBand : " + proposedBand);

                _featureContext["Current Tax Band"] = currentTaxBand;
                _featureContext["Proposed Tax Band"] = proposedBand;
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


        [Given(@"user verifies ""(.*)"" in the banding page")]
        public void GivenUserVerifiesInTheBandingPage(string Modifiedband)

        {
            try
            {
                CommonPage commonpage = new CommonPage();
                commonpage.currentbandcomparison(Modifiedband, _featureContext);
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


        [Given(@"user verifies ""(.*)"" in the billing Authority section")]
        public void GivenUserVerifiesInTheBillingAuthoritySection(string fieldName)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                commonpage.billingautoritycheck(fieldName, _featureContext);
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


        [Given(@"User looked for '(.*)' and increased (.*) step from ""(.*)"" band value")]
        public void GivenUserLookedForAndIncreasedStepFromBandValue(string fieldName, int stepCount, string fieldName1)
        {
            try
            {
                var testData = inputoutputdata.testData;
                RequestPage rp = new RequestPage();
                CommonPage commonpage = new CommonPage();
                waitHelpers wh = new waitHelpers();
                SeleniumExtensions.scrollToBtnElement(fieldName1);
                String currentTaxBand = wh.GetWebElement(By.XPath("//div[contains(@data-id,'voa_initialcounciltaxbandid.fieldControl-LookupResultsDropdown_voa_initialcounciltaxbandid_selected_tag_text')]")).Text;
                String proposedBand = commonpage.incrementtCharacter(currentTaxBand, stepCount);
                SeleniumExtensions.scrollToBtnElement(fieldName);
                commonpage.EnterDataInSingleCharLookUpField(fieldName, proposedBand);
                _featureContext["Current Tax Band"] = currentTaxBand;
                _featureContext["Proposed Tax Band"] = proposedBand;
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


        [Given(@"User looked for '(.*)' (.*) single character field value")]
        public void GivenUserLookedForSingleCharacterFieldValue(string fieldName, string fieldValue)
        {
            var testData = inputoutputdata.testData;
            CommonPage commonpage = new CommonPage();
            SeleniumExtensions.scrollToBtnElement(fieldName);
            commonpage.EnterDataInSingleCharLookUpField(fieldName, fieldValue);
        }




        [Given(@"User looked for '(.*)' and selects '(.*)' option value")]
        public void GivenUserLookedForAndSelectsOptionValue(string fieldName, int position)
        {
            CommonPage commonpage = new CommonPage();
            SeleniumExtensions.scrollToBtnElement(fieldName);
            commonpage.selectLookUpFieldPosition(fieldName, position);
        }


        [Given(@"User click on '(.*)' link")]
        public void GivenUserClickOnLink(string fieldName)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                try
                {
                    SeleniumExtensions.ScrollToSectionEle(fieldName);
                }
                catch (Exception e)
                {
                    SeleniumExtensions.scrollToBtnElement(fieldName);
                }
                //SeleniumExtensions.scrollToBtnElement(fieldName);
                commonpage.clickOnFieldNameLink(fieldName);
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

        [Given("User clicked on the '(.*)' link")]
        public void GivenUserClickedOnTheLink(string assesment)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                commonpage.clickOnFieldNameLink(assesment);
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


        [Given(@"user clicked on '(.*)' link")]
        public void GivenUserClickedOnLink(string linkname)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                commonpage.clickOnlinkname(linkname);
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

        [Given(@"User click on '(.*)' button from the list")]
        public void GivenUserClickOnButtonFromTheList(string Listitems)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                commonpage.clickOnlistitems(Listitems);
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





        [Given(@"User click on job link in Header")]
        public void GivenUserClickOnJobLinkInHeader()
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                commonpage.clickOnHeaderJobLink();
            }
            catch (Exception e)
            {
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
                pdf_Util.exceptionPdFLogger(e);
            }
        }

        [Given(@"User click on job link in Header '(.*)'")]
        public void GivenUserClickOnJobLinkInHeader(string bpName)
        {
            CommonPage commonpage = new CommonPage();
            //commonpage.clickOnHeaderJobLinkwithBpname(bpName);
        }


        [Given(@"User validates and clicks the '(.*)' tab present in the '(.*)' tablist and starts the auto process")]
        public void GivenUserValidatesAndClicksTheTabPresentInTheTablistAndStartsTheAutoProcess(string tabName, string tabListsName)
        {
            ValidateAndClickOnTab(tabName, tabListsName);
            DesktopResearchPage desktopResearchPage = new DesktopResearchPage();
            desktopResearchPage.DesktopResearchCompleteDropdown();
            ValidateAndClickOnTab("BA Reference", tabListsName);
            RequestPage requestPage = new RequestPage();
            string BARefValue = "";
            // requestPage.ValidateProposedBAReferenceAmendments(BARefValue, (string)_featureContext["PostCode"]);

        }

        [Given(@"User entered data for '(.*)' field value on dialog")]
        public void GivenUserEnteredDataForFieldValueOnDialog(string fieldName)
        {
            try
            {
                var testData = inputoutputdata.testData;
                if (fieldName.Contains("Date"))
                {
                    string date = DateTime.Now.AddDays(Convert.ToDouble(testData[fieldName])).ToString("dd/MM/yyyy");
                    _scenarioContext[fieldName] = date;
                    SeleniumExtensions.SetTheDateForTheTableCalendarOnDialog(fieldName, testData);
                }
                else
                {
                    SeleniumExtensions.setFieldValue(fieldName, testData);
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

        [Given(@"User entered data for '(.*)' field value on document dialog")]
        public void GivenUserEnteredDataForFieldValueOnDocumentDialog(string fieldName)
        {
            try
            {
                var testData = inputoutputdata.testData;
                SeleniumExtensions.enterDataOnDocumentDialog(fieldName, testData);
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


        [Given(@"User entered date for '(.*)' field value on dialog")]
        public void GivenUserEnteredDateForFieldValueOnDialog(string fieldName)
        {
            try
            {
                var testData = inputoutputdata.testData;
                SeleniumExtensions.enterDateSequentiallyOnDialog(fieldName, testData[fieldName]);
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

        [Given(@"User '(.*)' button until '(.*)' status changes to '(.*)' record")]
        public void GivenUserButtonUntilStatusChangesToRecord(string refreshBtn, string proprosedStatus, string committedStatus)
        {
            CommonPage commonpage = new CommonPage();
            commonpage.changeStatus(refreshBtn, proprosedStatus, committedStatus);
        }


        [Given(@"User asserts '(.*)' records")]
        public void GivenUserAssertsRecords(string coloumName, Table table)
        {
            SeleniumExtensions.WaitForReadyStateToComplete();
            CommonPage commonpage = new CommonPage();
            List<String> statusDetails = commonpage.getStatusDetails();
            Assert.IsTrue(table.RowCount == statusDetails.Count());
            foreach (var row in table.Rows)
            {
                string fieldName = row[coloumName];
                Assert.IsTrue(statusDetails.Contains(fieldName));
            }
            SeleniumExtensions.WaitForReadyStateToComplete();
        }

        [Given(@"User clicks on '(.*)' button on hereditament dialog and asserts '(.*)' records not contains '(.*)' Pad set")]
        public void GivenUserClicksOnButtonOnHereditamentDialogAndAssertsRecordsNotContainsPadSet(string fieldName, string coloumName, string statusName)
        {
            try
            {
                bool countmatched = false;
                List<string> statusDetails = new List<string>();
                CommonPage commonpage = new CommonPage();
                waitHelpers wh = new waitHelpers();
                int i = 0;
                do
                {
                    SeleniumExtensions.scrollToBtnBasedOnLabelAndClickOnHereditamentDialog(fieldName);
                    statusDetails = commonpage.getStatusDetails();
                    countmatched = !statusDetails.Contains(statusName);
                    i = i + 1;
                    if (i > 15)
                        break;
                } while (!countmatched);
                Assert.IsTrue(!statusDetails.Contains(statusName));
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


        [Given(@"User clicks on '(.*)' button on hereditament dialog and asserts '(.*)' records")]
        public void GivenUserClicksOnButtonOnHereditamentDialogAndAssertsRecords(string fieldName, string coloumName, Table table)
        {
            try
            {
                bool countmatched = false;
                List<string> statusDetails = new List<string>();
                CommonPage commonpage = new CommonPage();
                int i = 0;
                do
                {
                    SeleniumExtensions.scrollToBtnBasedOnLabelAndClickOnHereditamentDialog(fieldName);
                    SeleniumExtensions.WaitForReadyStateToComplete();
                    statusDetails = commonpage.getStatusDetails();
                    countmatched = (table.RowCount == statusDetails.Count());
                    i = i + 1;
                    if (i > 15)
                        break;
                } while (!countmatched);
                foreach (var row in table.Rows)
                {
                    string statusName = row[coloumName];
                    Assert.IsTrue(statusDetails.Contains(statusName));
                }
                SeleniumExtensions.WaitForReadyStateToComplete();
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


        [Given(@"user asserts ""(.*)"",""(.*)"" for ""(.*)"" records")]
        public void GivenUserAssertsForRecords(string effectiveFromDate, string effectiveToDate, string padType, Table table)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                if (padType.Equals("Change Date"))
                {
                    effectiveFromDate = (string)_scenarioContext["EffectiveFromDate"];
                    effectiveToDate = (string)_scenarioContext["Enter an Effective From Date"];
                }

                if (padType.Equals("CloneToNewDate_Committed"))
                {

                    effectiveFromDate = (string)_scenarioContext["EffectiveFromDate"];
                    effectiveToDate = (string)_scenarioContext["Enter the Effective Date for the new set of PADs"];
                }
                if (padType.Equals("CloneToNewDate_Pending"))
                {
                    effectiveFromDate = (string)_scenarioContext["EffectiveFromDate"];
                    effectiveToDate = (string)_scenarioContext["Enter the Effective Date for the new set of PADs"];
                }

                if (padType.Equals("Amend"))
                {
                    effectiveFromDate = (string)_scenarioContext["EffectiveFromDate"];
                    effectiveToDate = (string)_scenarioContext["EffectiveFromDate"];
                }

                Console.WriteLine("effectiveFromDate : " + effectiveFromDate);
                Console.WriteLine("effectiveToDate : " + effectiveToDate);
                commonpage.validateRecordsForDataEnhancement(effectiveFromDate, effectiveToDate, padType, table);
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


        [Given(@"User selected '(.*)' under '(.*)' tab")]
        public void GivenUserSelectedUnderTab(string subTabName, string mainTabName)
        {
            try
            {
                var cp = new CommonPage();
                cp.clickOnSubTab(subTabName, mainTabName);
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

        [Given(@"User click on messages ""(.*)"" element")]
        public void GivenUserClickOnMessagesElement(string msgId)
        {
            try
            {
                var requestPage = new RequestPage();
                requestPage.clickOnMsgJobID();
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

        [Given(@"User validates '(.*)' contains '(.*)' feature context data")]
        public void GivenUserValidatesContainsFeatureContextData(string msgName, string expectedMsgKey)
        {
            try
            {
                string msg = (string)_featureContext[msgName];
                Console.Write($"{msgName} : {msg}");
                Console.Write($"{expectedMsgKey} : {(string)_featureContext[expectedMsgKey]}");
                bool dataContains = false;// msg.Contains((string)_featureContext[expectedMsgKey]);
                if (expectedMsgKey.Equals("Address"))
                {
                    string addressStr = (string)_featureContext[expectedMsgKey];
                    var addressStrArr = addressStr.Split(',');
                    foreach (var str in addressStrArr)
                    {
                        dataContains = msg.Contains(str);
                        Assert.True(dataContains);
                    }
                }
                else
                {
                    dataContains = msg.Contains((string)_featureContext[expectedMsgKey]);
                    Assert.True(dataContains);
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


        [Given(@"User validates '(.*)' contains '(.*)' data")]
        public void GivenUserValidatesContainsData(string msgName, string expectedMsg)
        {
            try
            {
                string msg = (string)_featureContext[msgName];
                bool dataContains = msg.Contains(expectedMsg);
                Assert.True(dataContains);
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


        [Given(@"User captures '(.*)' text area field in '(.*)'")]
        public void GivenUserCapturesTextAreaFieldIn(string fieldName, string storageContext)
        {
            try
            {
                RequestPage rp = new RequestPage();
                Dictionary<String, String> jobdetails = rp.UserCaptureTextAreaFieldTitleAtributeDetails(fieldName);
                Console.WriteLine("API message : " + jobdetails[fieldName]);
                if (storageContext.Equals("scenarioContext", StringComparison.InvariantCultureIgnoreCase))
                {
                    _scenarioContext["Quadient Message"] = jobdetails[fieldName];
                }
                if (storageContext.Equals("featureContext", StringComparison.InvariantCultureIgnoreCase))
                {
                    _featureContext["Quadient Message"] = jobdetails[fieldName];
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

        [Given(@"User modifies value '(.*)' in '(.*)' field")]
        public void GivenUserModifiesValueInField(string fieldValue, string fieldName)
        {
            try
            {
                CommonPage cp = new CommonPage();
                cp.userupdateacceptancedecision(fieldValue, fieldName);
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

        [Given(@"User modified value '(.*)' in '(.*)' field")]
        public void GivenUserModifiedValueInField(string fieldValue, string fieldName)
        {
            try
            {
                CommonPage cp = new CommonPage();
                cp.userupdatejobtype(fieldValue, fieldName);
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

        [Given(@"User changes job owner if team is assigned to it")]
        public void GivenUserChangesJobOwnerIfTeamIsAssignedToIt()
        {
            try
            {
                CommonPage cp = new CommonPage();
                cp.updateJobOwner();
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

        [Given(@"user clears and enters new linked assesment")]
        public void GivenUserClearsAndEntersNewLinkedAssesment()
        {
            try
            {
                CommonPage cp = new CommonPage();
                cp.updatenewlinkedassessment();
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


        [Given(@"User selects '(.*)' job Type for the supplementary Job")]
        public void GivenUserSelectsJobTypeForTheSupplementaryJob(string JobName)
        {
            RequestPage requestpage = new RequestPage();
            requestpage.SelectSupplementaryJobType(JobName);
        }

        [Given(@"User validates the '(.*)' value on the Job Type Column on the Associated Jobs tab")]
        public void GivenUserValidatesTheValueOnTheJobTypeColumnOnTheAssociatedJobsTab(string jobName)
        {
            var requestPage = new RequestPage();
            requestPage.ValidatetheAssociatedJobs(jobName);
        }

        [Given(@"user validates the Change Code in the message")]
        public void GivenUserValidatesTheChangeCodeInTheMessage()
        {
            CommonPage commonpage = new CommonPage();
            commonpage.ValidatesheChangeCodeeMessage();
        }

        [Given(@"User click on '(.*)' button from '(.*)' untill '(.*)' status displayed")]
        public void GivenUserClickOnButtonFromUntillStatusDisplayed(string buttonName, string Sction, string status)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                commonpage.StatusDisplayed(buttonName, status);
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


        [Given(@"User selects '(.*)' job Type for '(.*)' and clicks on Continue and Finish")]
        public void GivenUserSelectsJobTypeForNoActionAndClicksOnContinueAndFinish(string fieldValue, string fieldName)
        {
            RequestPage requestpage = new RequestPage();
            //int data = 10;
            requestpage.ValidateNoAction(fieldName, fieldValue);
        }

        //[Given(@"User Navigates to '(.*)' tab and find the address")]
        //public void GivenUserNavigatesToTab(string tabName)
        //{
        //    ValidateAndClickOnTab(tabName, "Desktop Research Form");
        //    var requestPage = new RequestPage();
        //    string postCode = (string)_featureContext["postcode"];
        //    requestPage.FindAddressForChangeOfAddress(postCode);
        //}

        [Given(@"User selects the '(.*)' from the '(.*)' tab from '(.*)'")]
        public void GivenUserSelectsTheFromTheTabFrom(string optionName, string tabName, string FormName)
        {
            CommonPage commonPage = new CommonPage();
            commonPage.ClickOnNewOptionInRelatedTabOnForm(optionName, tabName, FormName);
        }

        [Given(@"User validates the below column values from the rows")]
        public void GivenUserValidatesTheBelowColumnValuesFromTheRows(Table table)
        {
            var testData = inputoutputdata.testData;
            foreach (var row in table.Rows)
            {
                string BillingAuthority = row["Billing Authority"];
                string BAReference = row["BA Reference"];
                string EffectiveFromDays = row["EffectiveFrom"];
                string EffectiveToDays = row["EffectiveTo"];
                string Status = row["Status"];
                //_featureContext["effective_from_date"] = "3/30/2004";
                //_featureContext["
                //"] = "3/30/2004";
                //_featureContext["ba_reference"] = "32228000007000420";
                //_featureContext["ProposedBARefNum"] = "154181";

                if (EffectiveFromDays == "")
                {
                    _featureContext[EffectiveFromDays] = "";
                }
                if (EffectiveToDays == "")
                {
                    _featureContext[EffectiveToDays] = "";
                }
                ValidateColumnValuesInRows(testData[BillingAuthority], (string)_featureContext[BAReference], (string)_featureContext[EffectiveFromDays], (string)_featureContext[EffectiveToDays], Status);
            }
        }




        [Given(@"User '(.*)' '(.*)' '(.*)' supplementory through '(.*)'")]
        public void GivenUserSupplementoryThrough(string btnName, string jobtype, string colName, string filter)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                string jobName = null;
                Console.Write(_featureContext.Values);
                foreach (string value in _featureContext.Values)
                {
                    if (value.Contains(jobtype))
                    {
                        jobName = value;
                        break;
                    }
                }

                _featureContext[jobtype] = jobName;

                //(string)_featureContext["Supplementary Job Name"];//jobName
                if (colName.Equals("Job_0"))
                {
                    colName = "Job Name";
                }
                commonpage.assignJobFromAllJobs(colName, jobName, btnName);
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


        [Given(@"User '(.*)' Supplementary '(.*)' through '(.*)'")]
        public void GivenUserSupplementaryThrough(string btnName, string colName, string filter)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                string jobName = (string)_featureContext["Supplementary Job Name"];//jobName
                commonpage.assignJobFromAllJobs(colName, jobName, btnName);
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

        [Given(@"User '(.*)' '(.*)' '(.*)' through '(.*)' for Reconstitution")]
        public void GivenUserThroughForReconstitution(string btnName, string jobTitle, string colName, string filter)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                string jobName = (string)_featureContext[jobTitle];//jobName
                commonpage.assignJobFromAllJobs(colName, jobName, btnName);
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

        [Given(@"user verify '(.*)' '(.*)' is in '(.*)' status")]
        public void GivenUserVerifyIsInStatus(string jobTitle, string colName, string expectedStatus)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                string jobName = (string)_featureContext[jobTitle];//jobName
                commonpage.childjobstatus(colName, jobName, expectedStatus);
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
        [Given(@"User filters recon job '(.*)' '(.*)' through '(.*)'")]
        public void GivenUserFiltersReconJobThrough(string jobkey, string colName, string AllJobs)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                string jobName = (string)_featureContext[jobkey];//jobName
                commonpage.filterJobFromAllJobs(colName, jobName);
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
        [Given("User filters the {string} {string} through {string}")]
        public void GivenUserFiltersTheThrough(string jobName, string colName, string allJobs)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                string jobNameLatest = (string)_featureContext[jobName];//jobName
                commonpage.filterJobFromAllJobs(colName, jobNameLatest);
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



        [Given(@"User filters '(.*)' through '(.*)'")]
        public void GivenUserFiltersThrough(string colName, string allJobs)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                string jobName = (string)_featureContext[colName];//jobName
                commonpage.filterJobFromAllJobs(colName, jobName);
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

        [Given(@"User filters recon '(.*)' '(.*)' through '(.*)' for Reconstitution")]
        public void GivenUserFiltersReconThroughForReconstitution(String jobtitle, string colName, string allJobs)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                string jobName = (string)_featureContext[jobtitle];//jobName
                commonpage.filterJobFromAllJobs(colName, jobName);
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




        [Given(@"User opens '(.*)' '(.*)' through '(.*)'")]
        public void GivenUserOpensThrough(string requestName, string colName, string filetrName)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                string jobName = (string)_featureContext[requestName];//requestName
                commonpage.openRequestFromCTrequest(jobName, colName, filetrName);
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

        [Given("User clicks {string} dropdown and filters {string}")]
        public void GivenUserClicksDropdownAndFilters(string dropdown, string filter)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                commonpage.milofilter(dropdown, filter);
            }
            catch (Exception e)
            {
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
                pdf_Util.exceptionPdFLogger(e);
            }
        }
        [Given("User clicks on the  {string} dropdown and filters {string}")]
        public void GivenUserClicksOnTheDropdownAndFilters(string dropdown, string filter)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                commonpage.milofilter_new(dropdown, filter);
            }
            catch (Exception e)
            {
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
                pdf_Util.exceptionPdFLogger(e);
            }
        }


        [Given("User click on {string} on the menubar")]
        public void GivenUserClickOnOnTheMenubar(string buttonname)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                commonpage.miloclicklinkhereditament(buttonname);
            }
            catch (Exception e)
            {
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
                pdf_Util.exceptionPdFLogger(e);
            }
        }

        [Given("User filters the {string} through {string}")]
        public void GivenUserFiltersTheThrough(string colName, string filter)
        {

            try
            {
                CommonPage commonpage = new CommonPage();
                string Milovalue = null;
                Milovalue = (string)_scenarioContext["MiloID"];
                commonpage.filtermilorecord(colName, Milovalue);

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

        [Given(@"User '(.*)' '(.*)' through '(.*)'")]
        public void GivenUserThrough(string btnName, string colName, string filter)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                string jobName = null;
                if (Config.EnvironmentVal == "DEV")
                {
                    jobName = (string)_featureContext["Job ID"];//jobId
                }
                else
                {
                    jobName = (string)_featureContext[colName];//jobName
                }
                commonpage.assignJobFromAllJobs(colName, jobName, btnName);
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
        [Given("User {string} {string} through {string} for abort case")]
        public void GivenUserThroughForAbortCase(string btnName, string colName, string filter)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                string jobName = null;
                jobName = (string)_featureContext["Job ID"];//jobId
                commonpage.assignJobFromAllJobs_abort(colName, jobName, btnName);
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

        [Given(@"User filter '(.*)' coloumn filter by '(.*)' with '(.*)' and '(.*)'")]
        public void GivenUserFilterColoumnFilterByWithAnd(string colName, string filterBy, string jObName, string pickBtn)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                commonpage.pickJob(colName, filterBy, pickBtn);
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


        [Given(@"User '(.*)' job to self on '(.*)' pop-up")]
        public void GivenUserJobToSelfOnPop_Up(string assignBtn, string popupTitle)
        {
            try
            {
                CommonPage cp = new CommonPage();
                clickOnMainMenuMoreElement_New(assignBtn);
                //clickOnMainMenuMoreElement_Latest(assignBtn);
                cp.clickEleOnDialog(assignBtn, popupTitle);
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

        [Given(@"User validates the status as '(.*)' after Release and Publish")]
        public void GivenUserValidatesTheStatusAsAfterReleaseAndPublish(string statusText)
        {
            CommonPage cp = new CommonPage();
            cp.ValidateStatusAfterReleaseAndPublish(statusText);
        }




        [Given(@"User creates a New Supplimentary Record for Request Type as '(.*)' and Job Type as '(.*)' and creates the Request")]
        public void GivenUserCreatesANewSupplimentaryRecordForRequestTypeAsAndJobTypeAsAndCreatesTheRequest(string requestType, string jobType)
        {
            var testData = inputoutputdata.testData;
            //   _featureContext["town_new"] = "CARDIFF";
            //  _featureContext["postcode_new"] = "CF10 4FD";
            //  _featureContext["uprn_new"] = "10013082614";

            CreateSupplementaryJobAtJobLevel(requestType, jobType, (string)_featureContext["town_new"], (string)_featureContext["postcode_new"], (string)_featureContext["uprn_new"]);

        }

        [Given(@"User validates the Dekstop Research Questionnaire for '(.*)'")]
        public void GivenUserValidatesTheDekstopResearchQuestionnaireFor(string bpName, Table table)
        {
            CommonPage commonpage = new CommonPage();
            SeleniumExtensions.WaitForReadyStateToComplete();
            List<String> questionaryDetails = commonpage.GetDekstopResearchQuestionnaire();
            int questionCount = questionaryDetails.Count();
            int tableCount = table.RowCount;
            Assert.IsTrue(table.RowCount == questionaryDetails.Count());
            foreach (var row in table.Rows)
            {
                string fieldName = row["Questions"];
                Assert.IsTrue(questionaryDetails.Contains(fieldName));
            }

        }

        [Given(@"User selects review record")]
        public void GivenUserSelectsReviewRecord()
        {
            try
            {
                CommonPage cp = new CommonPage();
                cp.userDoubleClickRow();
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
        [Given(@"user clicked on the '(.*)' button element")]
        public void GivenUserClickedOnTheButtonElement(string btnLabel)
        {
            try
            {
                CommonPage cp = new CommonPage();
                cp.clickOnBtnEleclose(btnLabel);
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

        [Given(@"User clicks on '(.*)' button element")]
        public void GivenUserClicksOnButtonElement(string btnLabel)
        {
            try
            {
                CommonPage cp = new CommonPage();
                cp.clickOnBtnEle(btnLabel);
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

        [Given(@"User modifies '(.*)' field")]
        public void GivenUserModifiesField(string fieldName)
        {
            try
            {
                CommonPage cp = new CommonPage();
                By loc1 = By.CssSelector("");
                By loc2 = By.CssSelector("");
                String newValue = "Vishal";
                cp.userModifiesFieldValue(loc1, loc2);
                SeleniumExtensions.ScrollUntilSelectorDisplayedAndSendKeys_New(fieldName, newValue, 120);
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
        [Given(@"user validated '(.*)' contains text")]
        public void GivenUserValidatedContainsText(string text)
        {
            try
            {
                CommonPage cp = new CommonPage();
                cp.userValidateContainsText(text);
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
        [Given(@"User verifies '(.*)' Banner message")]
        public void GivenUserVerifiesBannerMessage(string text)
        {
            try
            {
                CommonPage cp = new CommonPage();
                cp.userValidatebannernotification(text);
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


        [Given(@"user validated '(.*)' text")]
        public void GivenUserValidatedText(string text)
        {
            try
            {
                CommonPage cp = new CommonPage();
                cp.userValidateText_new(text);
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


        [Given(@"User double click on '(.*)' job under Associated jobs")]
        public void GivenUserDoubleClickOnJobUnderAssociatedJobs(string jobtype)
        {
            try
            {
                CommonPage cp = new CommonPage();
                cp.userClickOnJobUnderAssociatedJobs(jobtype);
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

        [Given("user click on {string} row under file upload section")]
        public void GivenUserClickOnRowUnderFileUploadSection(string rowname)
        {
            try
            {
                CommonPage cp = new CommonPage();
                cp.userClickOnfileuploadrow(rowname);
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


        [Given("User double click on {string} link under property links")]
        public void GivenUserDoubleClickOnLinkUnderPropertyLinks(string fieldname)
        {
            try
            {
                CommonPage cp = new CommonPage();
                cp.userClickonpropertylink(fieldname);
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

        [Given(@"User scroll to '(.*)' section and enter data in '(.*)' field")]
        public void GivenUserScrollToSectionAndEnterDataInField(string sectionName, string fieldName)
        {
            try
            {
                SeleniumExtensions.scrollToBtnElement(sectionName);
                var timestamp = DateTimeOffset.Now.ToString("yyyyMMddHHmmssffff");
                SeleniumExtensions.sendKeysFieldValue(fieldName, timestamp);
                _scenarioContext[fieldName] = timestamp;
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







        [Given(@"User Assigns the Job to self by clicking on '(.*)' button from the CommandBar")]
        public void GivenUserAssignsTheJobToSelfByClickingOnButtonFromTheCommandBar(string commandBarOption)
        {
            AssignJobToSelf(commandBarOption);
        }

        [Given(@"User clicked on supplementary job id")]
        public void GivenUserClickedOnSupplementaryJobId()
        {
            CommonPage CPF = new CommonPage();
            CPF.clickOnSupplementaryJob();
        }

        [Given(@"User collapse if site map navigation expanded on left pane")]
        public void GivenUserCollapseIfSiteMapNavigationExpandedOnLeftPane()
        {
            try
            {
                CommonPage CPF = new CommonPage();
                CPF.collapseNavIfItIsOpen();
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

        [Given(@"User clicks on '(.*)' button from top Banner")]
        public void GivenUserClicksOnButtonFromTopBanner(string newrecord)
        {
            try
            {
                CommonPage CPF = new CommonPage();
                CPF.expandnewrecord(newrecord);
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


        [Given(@"User validated ""(.*)"" Notification ""(.*)"" message")]
        public void GivenUserValidatedNotificationMessage(string requestName, string msg)
        {
            try
            {
                CommonPage CPF = new CommonPage();
                CPF.CR10NotificationMsgValidate(msg);
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

        [Given(@"User clicked on '(.*)' label")]
        public void GivenUserClickedOnLabel(string labelName)
        {
            try
            {
                CommonPage CPF = new CommonPage();
                CPF.clickOnLabel(labelName);
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



        [Given(@"User filters the job and assign to himself and opens the job")]
        public void GivenUserFiltersTheJobAndAssignToHimselfAndOpensTheJob()
        {
            CommonPage CPF = new CommonPage();
            CPF.ClickAndOpenTheJob();

        }


        [Given(@"User Navigates to '(.*)' tab and find the address")]
        public void GivenUserNavigatesToTabAndFindTheAddress(string p0)
        {
            ValidateAndClickOnTab("Change of Address", "Desktop Research Form");
            String postCode = (string)_featureContext["postcode"];
            FindAddressForChangeOfAddressforManual(postCode);
            ClickCommandBarOption("Save");
            waitTillSavingDisaddpears("Saving...", "progressbar");
        }

        [Given(@"User validate '(.*)' details under '(.*)' tab")]
        public void GivenUserValidateDetailsUnderTab(string subTab, string typeOfTab, Table table)
        {
            try
            {
                MaintainAssessmentPage map = new MaintainAssessmentPage();
                switch (typeOfTab)
                {
                    case "Billing Authority Links":
                        map.validatedBillingAuthorityLinks(_featureContext, table);
                        break;
                    case "PVT":
                        switch (subTab)
                        {
                            case "Addresses":
                                map.validatePVT_Addresses(subTab, table);
                                break;
                            case "Assessments":
                                map.validatedPVT_Assessment(subTab, _featureContext, table);
                                break;
                        }
                        break;
                    default:
                        map.validatedAssessment(_featureContext, table);
                        break;

                }
                //if (typeOfTab.Equals("Billing Authority Links"))
                //{
                //    map.validatedBillingAuthorityLinks(_featureContext, table);
                //}
                //else
                //{
                //    map.validatedAssessment(_featureContext, table);
                //}
            }
            catch (Exception e)
            {
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
                pdf_Util.exceptionPdFLogger(e);
            }
        }


        [Given(@"User clicked on '(.*)' button on dialog")]
        public void GivenUserClickedOnButtonOnDialog(string fieldName)
        {
            try
            {
                SeleniumExtensions.scrollToBtnBasedOnLabelClick(fieldName);
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

        [Given("User validated {string} status for {string} SLA")]
        public void GivenUserValidatedStatusForSLA(string status, string KPI_Name)
        {
            try
            {
                SeleniumExtensions.validateSLAKPIStatus(KPI_Name, status);
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

        [Given("User validates  outbound correspondence row count {string}")]
        public void GivenUserValidatesOutboundCorrespondenceRowCount(int rowSize)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                Assert.IsTrue(commonpage.getRowCount_correspondence() == rowSize);
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

        [Given("User selects the {string} status and {string} row")]
        public void GivenUserSelectsTheStatusAndRow(string Status, string Action)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                commonpage.selectassessment_EDC(Status, Action);

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
        [Given("User asserts Property Link table")]
        public void GivenUserAssertsPropertyLinkTable(Table Table)
        {
            try
            {
                MaintainAssessmentPage map = new MaintainAssessmentPage();
                map.validatepropertylinktable(_featureContext, Table);
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
       


        [Given("User click on {string} button link")]
        public void GivenUserClickOnButtonLink(string Assessment)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                commonpage.selectlinkedAssesment(Assessment);

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

        [Given("User open consequential record")]
        public void GivenUserOpenConsequentialRecord()
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                commonpage.openRecord();
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

        [Given("User navigate to {string} from consequential review item")]
        public void GivenUserNavigateToFromConsequentialReviewItem(string p0)
        {
            try
            {
                CommonPage commonpage = new CommonPage();
                commonpage.navigateToSucceedingRequest();
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

        [Given("user asserts below fields for consequentials review item {string}")]
        public void GivenUserAssertsBelowFieldsForConsequentialsReviewItem(string bpName, Table dataTable)
        {
            try
            {
                foreach (var row in dataTable.Rows)
                {
                    //string fieldName = row["fieldName"];
                    //string value = row["value"];
                    //string fieldType = row["feieldType"];

                    ////DateTime expectedDate = ResolveExpectedDate(value);
                    //DateTime actualDate = GetActualDateFromUI(reviewItem, fieldName, fieldType);

                    //Assert.That(
                    //    actualDate.Date,
                    //    Is.EqualTo(expectedDate.Date),
                    //    $"Mismatch for field '{fieldName}'. Expected: {expectedDate:dd/MM/yyyy}, Actual: {actualDate:dd/MM/yyyy}"
                    //);
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






    }
}

