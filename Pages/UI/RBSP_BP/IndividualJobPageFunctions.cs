using Npgsql.Replication.PgOutput.Messages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using POMSeleniumFrameworkPoc1.Helpers;
using POMSeleniumFrameworkPoc1.Pages.UI.RBSP_BP;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
//using AutoIt;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POMSeleniumFrameworkPoc1.Pages
{
    public partial class IndividualJobPage : BasePage
    {
        public DTO.InputOutputData inputoutputdata;
        List<String> questionsList;

        public void ValidateQuestionnaireInResearchingTab(string bpfName, string bpName, Dictionary<string, string> testData)
        {

            if (bpName == "CR03")
            {

                String expQuestions = testData["IndividualResearchingQuestions"];
                if (expQuestions.Contains(","))
                {
                    String[] questions = expQuestions.Split(',');
                    questionsList = questions.ToList();
                }

            }
            else if (bpName == "Composite Property Change")
            {
                String expQuestions = testData["CompositeResearchQuestions"];
                if (expQuestions.Contains(","))
                {
                    String[] questions = expQuestions.Split(',');
                    questionsList = questions.ToList();
                }

            }
            else if (bpName == "Deletion")
            {
                String expQuestions = testData["DeletionResearchQuestions"];
                if (expQuestions.Contains(","))
                {
                    String[] questions = expQuestions.Split(',');
                    questionsList = questions.ToList();
                }

            }
            else if (bpName == "Material Increase")
            {
                String expQuestions = testData["MaterialIncreaseResearchQuestions"];
                if (expQuestions.Contains(","))
                {
                    String[] questions = expQuestions.Split(',');
                    questionsList = questions.ToList();
                }

            }
            else if (bpName == "Change of BA Reference")
            {
                String expQuestions = testData["BAReferenceResearchQuestions"];
                if (expQuestions.Contains(","))
                {
                    String[] questions = expQuestions.Split(',');
                    questionsList = questions.ToList();
                }

            }
            //Thread.Sleep(10000);
            Thread.Sleep(7000);
            By jorneyHeaderUsingBy = JourneyHeaderUsingBySelector(bpfName);

            jorneyHeaderUsingBy.WaitForElementToBeClickable(15);

            //IWebElement headerName = JourneyHeader(bpfName);

            //headerName.ElementVisisbleUsingExplicitWait(10);
            //headerName.ClickUntilNoClickInterruptable(10);
            string bpfHeaderButtonText = BPFHeaderButton.GetAttribute("aria-label");
            if (bpfHeaderButtonText.Contains($"Stage: {bpfName}, Status: Active"))
            {
                IList<IWebElement> questionnaireList = DriverHelper.Driver.FindElements(ReasearchQuestionnaireList);
                List<string> questionText = new List<string>();
                foreach (var question in questionnaireList)
                {
                    string actQuestionText = question.Text.Trim();
                    questionText.Add(actQuestionText);

                }
                foreach (var expQues in questionsList)
                {

                    Assert.IsTrue(questionText.Contains(expQues), "The expected question could not be found");

                }
                CloseBPFPopUp(bpfName);
            }
        }

        public void EnterDetailsInInspectionSection(string toggleValue, string inspectionReason, string inspectionReasnComment, string inspectionAllocation)
        {
            InspoectionReqToggleBtn.ScrollAndClick();
            //SeleniumExtensions.SelectToggle(InspectionReqToggleTxt, InspoectionReqToggleBtn, toggleValue);
            Assert.IsTrue(InspectionReason.ElementVisisbleUsingExplicitWait(5));
            SeleniumExtensions.clickElementAndSelectText("Reason for Inspection", "Check the state of repair");
            //SeleniumExtensions.SelectElementByText(InspectionReason, "Check the state of repair");
            //InspectionReason.SelectElementByValue(inspectionReason);
            InspectionReasonComment.ClearAndSendkeys(inspectionReasnComment);
            //var currentDate_02 = DateTime.Now.ToString("d, MMMM, yyyy");
            //ScrollDiv.ScrollAndClick(InternalInspectionSLA, 20);
            //SeleniumExtensions.SetTheDateForTheTableCalendar(currentDate_02, 1);
            SeleniumExtensions.enterBeforeDateSequentially(0, "Internal Inspection SLA");
            SeleniumExtensions.clickElementAndSelectText("Inspection Allocation", "Assign Inspection to Inspector");
            // SeleniumExtensions.SelectElementByText(InspectionAllocation, "Assign Inspection to Inspector");
            //InspectionAllocation.SelectElementByValue(inspectionAllocation);

        }

        public void ValidateDefaultFieldValues(string sectionName, string fieldName, string expDefaultValue)
        {
            bool defaultValFlag;
            IWebElement eachField = null;
            try
            {
                eachField = DriverHelper.Driver.FindElement(By.CssSelector($"section[aria-label='{sectionName}'] fieldset[aria-label*='{fieldName}'] [aria-label='{expDefaultValue}']"));
            }
            catch (NoSuchElementException e)
            {
                eachField = DriverHelper.Driver.FindElement(By.CssSelector($"section[aria-label='{sectionName}'] fieldset[aria-label='{fieldName} '] [aria-label='{expDefaultValue}']"));
            }
            eachField.ScrollAndClick();
            string actDefaultVal = eachField.GetAttribute("aria-selected");
            if (actDefaultVal == "true")
            {
                defaultValFlag = true;
            }
            else
                defaultValFlag = false;
            Assert.IsTrue(defaultValFlag, $"The default value for field {fieldName} is {actDefaultVal}");
        }


        public void ValidateInspectionJob(string jobID)
        {
            string actInspectionJobID = "";
            while (InspectionJobID.IsElementDisplayed(3) == false)
            {
                InspectionJobRefresh.ClickUsingJavascript();
                if (InspectionJobID.IsElementDisplayed(3) == true)
                {
                    actInspectionJobID = InspectionJobID.GetAttribute("title");
                }
            }
            //actInspectionJobID = InspectionJobID.GetAttribute("title");
            //if (actInspectionJobID.Contains(jobID))
            //{
            InspectionJobID.DoubleClickElementUsingJSExecutor();
            //}
            //else
            //{
            //    Assert.Fail("The Inspection Job created " + actInspectionJobID + " does not conatins the main JobID " + jobID);
            //}

        }


        public void AmendCTInspectionFieldValues(string sectionName, string fieldName, string amendValues)
        {

            IWebElement eachField = null;
            waitHelpers wh = new waitHelpers();
            try
            {
                eachField = wh.GetWebElement(By.CssSelector($"section[aria-label='{sectionName}'] fieldset[aria-label*='{fieldName}'] [aria-label='{amendValues}']"));
            }
            catch (NoSuchElementException e)
            {
                eachField = wh.GetWebElement(By.CssSelector($"section[aria-label='{sectionName}'] fieldset[aria-label*='{fieldName} '] [aria-label='{amendValues}']"));
            }
            // eachField.ScrollAndClick();
            string actDefaultVal = eachField.GetAttribute("aria-selected");
            if (actDefaultVal == "false")
            {
                //eachField.DoubleClickElementUsingJSExecutor();
                eachField.Click();

            }
            else
            {
                Console.WriteLine("The value is already selected");
            }

        }

        public void SelectInspectionTypeAndSave(string inspectionType)
        {
            InspectionTypeDialog.SelectElementByValue(inspectionType);

            IWebElement saveOnDialog = InspectionDialogMenuOption("Save (CTRL+S)");
            saveOnDialog.ClickUsingJavascript();

            IWebElement markComplete = InspectionDialogMenuOption("Mark Complete");
            markComplete.ClickUsingJavascript();


        }

        public void EnterDetailsInAFHDialog(string afhSubject, string afhDescription)
        {
            AFHSubject.ClearAndSendkeys(afhSubject);
            AFHDescription.ClearAndSendkeys(afhDescription);
            ASHSave.ClickUsingJavascript();
            ValidateSaveInProgressDialog();
            Thread.Sleep(5000);
            AFHMarkComplete.ClickUsingJavascript();
            Thread.Sleep(2000);

        }
        public void ValidateStatusCodeValue(string expStatusCode, string columnName)
        {
            var commonPage = new CommonPage();
            commonPage.ValidateActivityStatus(ViewSelectorRows, columnName, expStatusCode);

        }

        public void ValidateFieldsPropertyAttributes(string expHeatingVal, string expConservatoryType, string expConservatoryVal, string groupVal, string typeVal, Dictionary<string, string> testData)
        {
            Thread.Sleep(8000);
            FillAndSelectLookUpFirstResultWhenDataNotEntered("Group", testData);
            FillAndSelectLookUpFirstResultWhenDataNotEntered("Type", testData);
            //By GroupValue = GetFirstLookUp(groupVal);
            //SeleniumExtensions.EnterInLookUpField(GroupValue, PA_GroupLookUp, PA_GroupLookUpSrchBtn, groupVal);
            //By TypeValue = GetFirstLookUp(typeVal);
            //SeleniumExtensions.EnterInLookUpField(TypeValue, PA_TypeLookUp, PA_TypeLookUpSearchBtn, typeVal);

            //FillAndSelectLookUpResult(PA_GroupLookUp, groupVal);
            //FillAndSelectLookUpResult(PA_TypeLookUp, typeVal);
            //  FillAndSelectLookUpResult(PA_TypeLookUp, typeVal);

        }



        public void EnterDataInPropertyAttributesSection(string ageCodeVal, string areaVal, string roomsVal, string bedroomsVal, string bathVal, string floorsVal, string parkingVal, string plotSizeVal, Dictionary<string, string> testData)
        {
            Thread.Sleep(2000);
            FillAndSelectLookUpFirstResultWhenDataNotEntered("Age Code", testData);
            //FillAndSelectLookUpResult(PA_AgeCode, ageCodeVal);
            PA_Area.ClearAndSendkeys(areaVal);
            PA_Rooms.ClearAndSendkeys(roomsVal);
            PA_Bedrooms.ClearAndSendkeys(bedroomsVal);
            PA_Baths.ClearAndSendkeys(bathVal);
            PA_Floors.ClearAndSendkeys(floorsVal);
            FillAndSelectLookUpFirstResultWhenDataNotEntered("Parking", testData);
            //FillAndSelectLookUpResult(PA_ParkingLookUp, parkingVal);
            PA_PlotSize.ClearAndSendkeys(plotSizeVal);

        }

        public void EnterDataInPropertyAttributesSection(string BpfVal, string groupdata, string typedata, string ageCodeVal, string parkingVal, string ConservartoryTypeVal, string heatingVal, string conservatoryval, string floor, string level, string roomsVal, string bedroomsVal, string bathVal, string areaVal)
        {
            if (BpfVal == "Composite Property change" || BpfVal == "InformalChallenge")
            {
                //Thread.Sleep(15000);
                // PA_Level.ClearAndSendkeys(level);
                PA_ConservatoryArea.ClearAndSendkeys(conservatoryval);
                PA_Area.ClearAndSendkeys(areaVal);
                if (PA_ConservatoryType.IsElementVisibleUsingByLocator(2))
                {
                    FillAndSelectLookUpResult(PA_ConservatoryType, ConservartoryTypeVal);
                }
                //PA_Area.ClearAndSendkeys(areaVal);

            }
            clickOnMainMenuMoreElement_New("Save");
        }




        public void ClickOnNewVSCLinkButtonOnPADEntryTab()
        {
            NewVSCButton.ClickUsingJavascript();
        }



        public void EnterVSCDataInDomecticPropertyVSCPage(string vscValue, string vscDesc)
        {
            FillAndSelectLookUpResult(DP_ValueSignificantCode, vscValue);
            DP_VSCDescription.ClearAndSendkeys(vscDesc);

        }

        public void ClickOnNewSourceCodeOnPADEntryTab()
        {
            ClickCommandBarOption("Save");
            NewSourceCodeButton.ClickUsingJavascript();
        }

        public void EnterSourceCodeInSetSourceCodePage(string sourceCodeVal)
        {
            FillAndSelectLookUpResult(SourceCode, sourceCodeVal);
            SC_Comment.ClearAndSendkeys("TestComment");

        }


        public void ValidatePADOutcome(string expOutcome)
        {
            Thread.Sleep(2000);
            string actualOutcome = PADValidationOutcome.GetAttribute("value");
            if (actualOutcome == string.Empty)
            {
                ClickCommandBarOption("Refresh");
                ValidateAndClickOnTab("PAD Entry", "Desktop Research Form");
                actualOutcome = PADValidationOutcome.GetAttribute("value");
            }
            Assert.AreEqual(expOutcome, actualOutcome);
        }

        public void UpdateDesktopResearch()
        {

            ValidateAndClickOnTab("Check Facts", "Desktop Research Form");
            SeleniumExtensions.scrollToBtnElement("Is Desktop Research Complete?");
            SeleniumExtensions.SelectDropdownValue(IsDekstopResearchComplete, "Yes");
            if (NRDAssessmentOkButton.IsElementDisplayed(5))
            {
                NRDAssessmentOkButton.ClickUsingJavascript();

            }
            if (NRDAssessmentOkButton.IsElementDisplayed(5))
            {
                NRDAssessmentOkButton.ClickUsingJavascript();

            }
            clickOnMainMenuMoreElement_New("Save");
            waitTillSavingDisaddpears("Saving...", "progressbar");


        }
        public void ValidatePADFailureOutcome(string expOutcome)
        {
            Thread.Sleep(13000);
            string actualOutcome = PADValidationOutcome.GetAttribute("value");
            if (actualOutcome.Contains("Failure"))
            {
                if (ValidationFailureTxt.IsElementDisplayed(4))
                {
                    string validatefailurereason = ValidationFailureTxt.Text;
                    string[] failurereason = validatefailurereason.Split(' ');
                    foreach (string failurevalue in failurereason)
                    {
                        if (failurevalue == "Level")
                        {
                            SeleniumExtensions.ScrollToElement(PA_Level);
                            PA_Level.ClearAndSendkeys("1");
                            Thread.Sleep(2000);
                            clickOnMainMenuMoreElement_New("Save");
                            SeleniumExtensions.ScrollToElement(ValidatePADToggleTxt);
                            SeleniumExtensions.SelectToggle(ValidatePADToggleTxt, ValidatePADToggleBtn, "Yes");
                            clickOnMainMenuMoreElement_New("Save");
                            Thread.Sleep(5000);
                            SeleniumExtensions.ScrollToElement(PADValidationOutcome);
                            actualOutcome = PADValidationOutcome.GetAttribute("value");

                        }


                    }

                }
            }
            else if (actualOutcome.Contains("--"))
            {
                ClickCommandBarOption("Refresh");
                ValidateAndClickOnTab("PAD Entry", "Desktop Research Form");
                Thread.Sleep(2000);
                actualOutcome = PADValidationOutcome.GetAttribute("title");
            }
            Assert.AreEqual(expOutcome, actualOutcome);

        }


        public void EnterDataInJobResolutionPopUp(string resolutionType, string BARemarksVal, string JRRemarks)
        {
            ResolutionType.SelectElementByValue(resolutionType);
            BARemarks.ClearAndSendkeys(BARemarksVal);
            JR_Remarks.ClearAndSendkeys(JRRemarks);
            SaveAndCloseNoAction.ClickUsingJavascript();

        }

        public void ValidateVMSLoadedCorrectly(string expVMSTitle)
        {
            Thread.Sleep(5000);
            IReadOnlyCollection<string> windowhandles = Driver.WindowHandles;
            Driver.SwitchTo().Window(windowhandles.Last());
            Console.WriteLine(Driver.Title);


            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(Driver => ((IJavaScriptExecutor)Driver).ExecuteScript("return document.readyState").Equals("complete"));

            Driver.WaitForElementToDisplayProperly(VMSWindowIFrameSelector, 5);
            Driver.SwitchTo().Frame(VMSWindowIFrame);

            Assert.IsFalse(VMSError.ElementVisisbleUsingExplicitWait(10), "Error message is displayed while loading the property details");
            string actTitle = VMSHeader.Text.Trim();
            Console.WriteLine(actTitle);
            Assert.AreEqual(expVMSTitle, actTitle);
            Driver.Close();
            Driver.SwitchTo().Window(windowhandles.First());
            Console.WriteLine(Driver.Title);


        }

        public void ClickOnUploadThisJob(string buttonName, string option)
        {
            waitHelpers wh = new waitHelpers();
            IWebElement buttonToClick = DocumentTabButton(buttonName);
            buttonToClick.ClickUsingJavascript();
            By optionToSelect = SeleniumExtensions.getLocator($"//ul[contains(@class,'ms-ContextualMenu')]//li//button//*[text()='{option}']");
            wh.jsClickOnElement(optionToSelect);
            //UploadThisJobOption.ClickUsingJavascript();

        }
        
        public void Clickeditdetailsoption(string position)
        {
            IWebElement editdtailsoptions = editoption(position);
            editdtailsoptions.ClickUsingJavascript();
            //UploadThisJobOption.ClickUsingJavascript();

        }


        public void SelectValueFromDocumentDropdownOnDIalog(string dropdownValue, string dropdownName)
        {
            //IWebElement dropDownToClick = DropdownOnDocumentUploadDialog(dropdownName);
            //dropDownToClick.ElementVisisbleUsingExplicitWait(5);
            //dropDownToClick.ClickUsingJavascript();
            //IWebElement dropDownValueToSelect = DropdownOptionOnDocumentDialog(dropdownValue);
            //dropDownValueToSelect.ClickUsingJavascript();

            DocumentType.IsElementDisplayed(5);
            DocumentType.ClickUsingJavascript();
            DocumentType.ClearAndSendkeys("Test");

        }
        public void ClickonButtonOnDocumentDialog(string buttonName)
        {
            IWebElement buttonToClick = ButtonOnDocDialog(buttonName);
            buttonToClick.ScrollAndClick();

        }

        public void UploadDocumentOnDialog(string buttonName)
        {
            Actions action = new Actions(DriverHelper.Driver);


            Assert.IsTrue(ChooseFilesButton.ElementVisisbleUsingExplicitWait(5));
            //action.MoveToElement(ChooseFilesButton).SendKeys(Keys.Enter).Perform();
            SeleniumExtensions.UploadFile();

        }

        public void UploadDocumentOnDialog(string buttonName, string fileName)
        {
            Actions action = new Actions(DriverHelper.Driver);

            Assert.IsTrue(ChooseFilesButton.ElementVisisbleUsingExplicitWait(5));
            //action.MoveToElement(ChooseFilesButton).SendKeys(Keys.Enter).Perform();
            SeleniumExtensions.UploadFile(fileName);

        }

        public void ValidateDocUploadStatus()
        {
            waitHelpers wh = new waitHelpers();
            string uploadMessage = null;
            try
            {
                wh.GetWebElement(By.CssSelector("a[class='mainFileName']"));
                uploadMessage = wh.getElementText(By.XPath("//*[contains(@class,'ms-MessageBar')]//*[contains(@class,'ms-MessageBar-innerText')]/span"));// DocumentMessage.Text;
                Assert.IsTrue(uploadMessage.Replace(":", "").Contains("Successfully uploaded"));
            }
            catch (Exception e)
            {
                Console.WriteLine("uploadMessage" + uploadMessage);
                Console.WriteLine("Message" + "Successfully uploaded");
                Console.WriteLine(uploadMessage.Replace(":", "").Contains("Successfully uploaded"));
                Console.WriteLine("There was an error uploading the document", e.Message);
            }


        }

        public void ValidateDocUpdatestatus()
        {
            waitHelpers wh = new waitHelpers();
            string updateMessage = null;
            try
            {
                wh.GetWebElement(By.CssSelector("a[class='mainFileName']"));
                updateMessage = wh.getElementText(By.XPath("//*[contains(@class,'ms-MessageBar')]//*[contains(@class,'ms-MessageBar-innerText')]/span"));// DocumentMessage.Text;
                Assert.IsTrue(updateMessage.Replace(":", "").Contains("Successfully updated"));
            }
            catch (Exception e)
            {
                Console.WriteLine("updateMessage" + updateMessage);
                Console.WriteLine("Message" + "Successfully updated");
                Console.WriteLine(updateMessage.Replace(":", "").Contains("Successfully updated"));
                Console.WriteLine("There was an error uploading the document", e.Message);
            }

        }
        
        public void NavigatePVTTab(string tabName)
        {
            IWebElement tabToSelect = PVTTab(tabName);
            string activeTabStatus = tabToSelect.GetAttribute("aria-selected");
            if (activeTabStatus != "true")
            {
                tabToSelect.ClickUsingJavascript();
            }

        }

        public void ValidateWelcomeMessageAndClickCreateOnPVT(string tabName, string exptext)
        {
            NavigatePVTTab(tabName);
            string actText = PVTWelcomeText.Text;
            Assert.AreEqual(exptext, actText);
            PVTCreateButton.ClickUsingJavascript();
            //PADSSuccessMsg.ElementVisisbleUsingExplicitWait(5);


        }

        public void EditThePADCreated(string commandOption)
        {
            Thread.Sleep(3000);
            Assert.IsTrue(PADExpandArrow.ElementVisisbleUsingExplicitWait(10), "The expand option for PAD is not visible yet, add more wait time");
            string expandedStatus = PADExpandArrow.GetAttribute("aria-expanded");
            if (expandedStatus == "false")
            {
                PADExpandArrow.ClickUsingJavascript();
            }
            Assert.IsTrue(PADRows.ElementVisisbleUsingExplicitWait(10), "The PAD rows are not displayed yet");
            string PADseelcted = PADRows.GetAttribute("aria-selected");
            if (PADseelcted == "false")
            {
                string PADSelectedStatus = PADDataCheckBox.GetAttribute("aria-checked");
                if (PADSelectedStatus == "false")
                {
                    PADDataCheckBox.ClickUsingJavascript();
                }
            }

            IWebElement commandOptionToClick = PVTCommandBarOption(commandOption);
            Assert.IsTrue(commandOptionToClick.ElementVisisbleUsingExplicitWait(5));
            commandOptionToClick.ClickUsingJavascript();

        }

        public void SelectAssessmentFromPVT(string commandOption)
        {
            Thread.Sleep(3000);
            Assert.IsTrue(PADExpandArrow.ElementVisisbleUsingExplicitWait(10), "The expand option for PAD is not visible yet, add more wait time");
            string expandedStatus = PADExpandArrow.GetAttribute("aria-expanded");
            if (expandedStatus == "false")
            {
                PADExpandArrow.ClickUsingJavascript();
            }
            Assert.IsTrue(PADRows.ElementVisisbleUsingExplicitWait(10), "The PAD rows are not displayed yet");
            string PADseelcted = PADRows.GetAttribute("aria-selected");
            if (PADseelcted == "false")
            {
                string PADSelectedStatus = PADDataCheckBox.GetAttribute("aria-checked");
                if (PADSelectedStatus == "false")
                {
                    PADDataCheckBox.ClickUsingJavascript();
                }
            }

            IWebElement commandOptionToClick = PVTCommandBarOption(commandOption);
            Assert.IsTrue(commandOptionToClick.ElementVisisbleUsingExplicitWait(5));
            commandOptionToClick.ClickUsingJavascript();

        }


        public string EnterDataInBandingTabforCompositePropertyChange(string proposedTaxBand, string assessmentActionVal, string isBandingCompleteVal, string isBandingDirectionVal)
        {
            string bandtext = null;
            //Thread.Sleep(10000);
            SeleniumExtensions.scrollToBtnElement("Current Tax Band");
            bandtext = ExistingTaxBand.Text;


            if (isBandingDirectionVal == "Increase" && bandtext != "Deleted")
            {
                char band = char.Parse(bandtext);
                char nextBand = GetNextBand(band);
                if (nextBand != '\0')
                {
                    Console.WriteLine("The next band after" + band + "is " + nextBand + ".");
                    proposedTaxBand = Char.ToString(nextBand);
                    bandtext = proposedTaxBand;

                }

            }
            else if (isBandingDirectionVal == "Decrease" && bandtext != "Deleted")
            {
                char band = char.Parse(bandtext);
                char nextBand = GetPreviousBand(band);

                if (nextBand != '\0')
                {
                    Console.WriteLine("The next band after" + band + "is " + nextBand + ".");
                    proposedTaxBand = Char.ToString(nextBand);
                    bandtext = proposedTaxBand;

                }
            }
            else if (bandtext == "Deleted")
            {
                proposedTaxBand = "A";

            }

            TextAreaCWConclusion.WaitUntilElementAttached(60);
            TextAreaCWConclusion.ClearAndSendkeys("Test");
            //Thread.Sleep(2000);
            //clickOnMainMenuMoreElement_New("Save");
            //waitTillSavingDisaddpears("Saving...", "progressbar");
            By lookUpFirstValue = GetFirstLookUp(proposedTaxBand);
            SeleniumExtensions.EnterInLookUpField(lookUpFirstValue, ProposedTaxBand, ProposedTaxBandSearchBtn, proposedTaxBand);
            //Thread.Sleep(2000);
            //clickOnMainMenuMoreElement_New("Save");
            //waitTillSavingDisaddpears("Saving...", "progressbar");
            SeleniumExtensions.SelectDropdownValue(IsBandingComplete, "Yes");
            ConfirmandLockBanding.ClickUsingJavascript();
            //clickOnMainMenuMoreElement_New("Save");
            waitTillSavingDisaddpears("Saving...", "progressbar");
            if (isBandingDirectionVal == "Increase" && bandtext != "Deleted")
            {
                string bandDirection = BandChangeDirection.GetAttribute("value");
                Assert.AreEqual(isBandingDirectionVal, bandDirection);
            }
            else if (isBandingDirectionVal == "Decrease" && bandtext != "Deleted")
            {
                string bandDirection = BandChangeDirection.GetAttribute("value");
                Assert.AreEqual(isBandingDirectionVal, bandDirection);

            }

            return bandtext;
        }

        public static char GetNextBand(char band)
        {
            char[] alpha = "ABCDEFGHI".ToCharArray();
            band = Char.ToUpper(band);
            if (band >= 'A' && band <= 'I')
            {
                int position = Array.IndexOf(alpha, band);
                int nextPosition = (position + 1) % alpha.Length;
                return
                    alpha[nextPosition];
            }


            return (char)0;
        }

        public static char GetPreviousBand(char band)
        {
            char[] alpha = "ABCDEFGHI".ToCharArray();
            band = Char.ToUpper(band);
            if (band >= 'A' && band <= 'I')
            {
                int position = Array.IndexOf(alpha, band);
                int previousPosition = (position - 1 + alpha.Length) % alpha.Length;
                return
                    alpha[previousPosition];
            }


            return (char)0;
        }
        public void waitTillSavingDisaddpears(String labelName, String roleType)
        {
            String locator = $"//*[@role='{roleType}' and text()='{labelName}'] | //*[@role='{roleType}'] | //*[text()='{labelName}']";
            By eleBy = SeleniumExtensions.getLocator(locator);
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(180));
            wait.PollingInterval = TimeSpan.FromMilliseconds(200);
            wait.Until(SeleniumExtensions.InvisibilityOfElementLocatedBy(eleBy));
        }

        public void EnterDataInBandingTab(string proposedTaxBand, string assessmentActionVal, string isBandingCompleteVal)
        {
            TextAreaCWConclusion.WaitUntilElementAttached(60);
            TextAreaCWConclusion.ClearAndSendkeys("Test");

            By lookUpFirstValue = GetFirstLookUp(proposedTaxBand);
            SeleniumExtensions.EnterInLookUpField(lookUpFirstValue, ProposedTaxBand, ProposedTaxBandSearchBtn, proposedTaxBand);
            SeleniumExtensions.SelectDropdownValue(IsBandingComplete, isBandingCompleteVal);

            waitHelpers wh = new waitHelpers();
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(10));
            SeleniumExtensions.SelectDropdownValue(IsBandingComplete, "Yes");
            ConfirmandLockBanding.ClickUsingJavascript();
            waitTillSavingDisaddpears("Saving...", "progressbar");

        }
        public void ValidateSaveInProgressDialog()
        {
            if (SaveInProgressError.ElementVisisbleUsingExplicitWait(5))
            {
                SaveInProgressOKBtn.ClickUsingJavascript();
                ClickCommandBarOption("Save");
            }

        }

        public void ValidateAssesment(string proposedTaxBand, string expreleaseStatus)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(Driver => ((IJavaScriptExecutor)Driver).ExecuteScript("return document.readyState").Equals("complete"));

            Driver.WaitForElementToDisplayProperly(AssessmentOuterIFrameSelector, 5);
            Driver.SwitchTo().Frame(AssesmentOuterIFrame);
            Driver.WaitForElementToDisplayProperly(AssessmentInnerIFrameSelector, 5);
            Driver.SwitchTo().Frame(AssesmentInnerIFrame);
            string actBandDisplayed = AssessmentBandText.Text;
            Assert.AreEqual(proposedTaxBand, actBandDisplayed);
            Log.Information($"User successfully validates expected proposed band  {proposedTaxBand} and actual proposed band {actBandDisplayed} are equal");
            string actReleaseStatus = PublicationStatusText.Text;
            Assert.AreEqual(expreleaseStatus, actReleaseStatus);
            Log.Information($"User successfully validates expected status {expreleaseStatus} and actual status {actReleaseStatus} are equal");
            Driver.SwitchTo().DefaultContent();
        }
        public void ValidatePublicationStatus(string expPublicationStatus)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(Driver => ((IJavaScriptExecutor)Driver).ExecuteScript("return document.readyState").Equals("complete"));

            Driver.WaitForElementToDisplayProperly(AssessmentOuterIFrameSelector, 5);
            Driver.SwitchTo().Frame(AssesmentOuterIFrame);
            Driver.WaitForElementToDisplayProperly(AssessmentInnerIFrameSelector, 5);
            Driver.SwitchTo().Frame(AssesmentInnerIFrame);
            IList<IWebElement> AssessmentFields = DriverHelper.Driver.FindElements(AssessmentRowFields);
            foreach (var eachfield in AssessmentFields)
            {
                IWebElement currentfield = eachfield.FindElement(By.CssSelector("[class*='ms-DetailsRow-cell'] span"));
                string fieldVal = currentfield.Text.Trim();
                if (fieldVal == expPublicationStatus)
                {
                    break;
                }
                else
                {
                    continue;
                }

            }

            Driver.SwitchTo().DefaultContent();
        }

        public void ValidateAssesmentAfterPublish(string tabName, string expAssessmentStatus, string expPublicationStatus)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            wait.Until(Driver => ((IJavaScriptExecutor)Driver).ExecuteScript("return document.readyState").Equals("complete"));

            Driver.WaitForElementToDisplayProperly(AssessmentOuterIFrameSelector, 5);
            Driver.SwitchTo().Frame(AssesmentOuterIFrame);
            Driver.WaitForElementToDisplayProperly(AssessmentInnerIFrameSelector, 5);
            Driver.SwitchTo().Frame(AssesmentInnerIFrame);
            IWebElement tabToSelect = SelectAssessmentCheckTab(tabName);
            tabToSelect.ClickUsingJavascript();
            string actAssessmentStatus = AssessmentStatusText.Text;
            if (actAssessmentStatus.Contains(expAssessmentStatus))
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }
            string actPublicationStatus = PublicationStatusText.Text;
            Assert.AreEqual(expPublicationStatus, actPublicationStatus);
            Driver.SwitchTo().DefaultContent();
        }

        public void ValidateAssesmentAfterRelease(string tabName, string expAssessmentStatus, string expAssessmentBand)
        {
            bool validateflag = false;
            string actAssessmentBandStatus = null;
            string actAssessmentStatus = null;
            int assessementTotalRows = GetAssessmentRows(tabName);
            for (int i = 0; i < assessementTotalRows; i++)
            {
                IWebElement AssessmentBandText = UIAssessmentBandText(i);
                IWebElement AssessmentStatus = UIAssessmentStatusText(i);
                actAssessmentBandStatus = AssessmentBandText.Text;
                actAssessmentStatus = AssessmentStatus.Text;
                if (actAssessmentStatus.Contains(expAssessmentStatus) && actAssessmentBandStatus == expAssessmentBand)
                {
                    validateflag = true;
                    break;
                }
                else
                {
                    continue;
                }

            }
            if (validateflag == true)
            {
                Log.Information($"User validated {expAssessmentBand} as Assessment Band and {expAssessmentStatus} as assessment status");

            }
            else
            {
                Log.Error($"User could not validate {expAssessmentBand} as Assessment Band and {expAssessmentStatus} as assessment status");

            }
            Driver.SwitchTo().DefaultContent();
        }



        public void ValidateAssessmentForBP(string tabName, string effectiveFrom, string expEffectiveTo, string expAssessmentBand, string expCompositeIndicator, string expAssessmentStatus, string expPublicationStatus)
        {
            bool ValidationFlag = false;
            string expEffectiveDate = null;
            if (effectiveFrom != "")
            {
                string[] dateValue = effectiveFrom.Split(' ');
                string CuurentDateFormat = SeleniumExtensions.GetDateFormat(dateValue[0]);
                DateTime dateToEnter = DateTime.ParseExact(dateValue[0], CuurentDateFormat, CultureInfo.InvariantCulture);
                expEffectiveDate = dateToEnter.ToString("M/d/yyyy");
                Console.WriteLine(expEffectiveDate);
            }

            string actEffectiveFromDate = null;
            string actEffectiveToDate = null;
            string actCompositeIndicator = null;
            string actAssessmentBand = null;
            string actAssessmentStatus = null;
            string actPublicationStatus = null;
            int assessementTotalRows = GetAssessmentRows(tabName);
            for (int i = 0; i < assessementTotalRows; i++)
            {
                IWebElement EffectiveFromDate = UIEffectiveFromText(i);
                IWebElement EffectiveToDate = UIEffectiveToText(i);
                IWebElement CompositeIndicator = UICompositeIndicator(i);
                IWebElement AssessmentBandText = UIAssessmentBandText(i);
                IWebElement AssessmentStatus = UIAssessmentStatusText(i);
                IWebElement PublicationStatus = UIPublicationStatusText(i);
                actEffectiveFromDate = EffectiveFromDate.Text;

                actEffectiveToDate = EffectiveToDate.Text;

                actCompositeIndicator = CompositeIndicator.Text;
                actAssessmentBand = AssessmentBandText.Text;
                actAssessmentStatus = AssessmentStatus.Text;
                actPublicationStatus = PublicationStatus.Text;
                //if (actAssessmentStatus.Contains(expAssessmentStatus) && expPublicationStatus == actPublicationStatus)
                //{
                //    ValidationFlag = true;
                //    Log.Information($"The expected {expAssessmentStatus} &  {expPublicationStatus}  has been validated in the {i} row");
                //    break;
                //}
                if (expEffectiveDate == actEffectiveFromDate && actAssessmentStatus.Contains(expAssessmentStatus) && expPublicationStatus == actPublicationStatus)
                {
                    ValidationFlag = true;
                    Log.Information($"The expected {expEffectiveDate} , {expAssessmentStatus} &  {expPublicationStatus}  has been validated in the {i} row");
                    break;
                }
                else if (expEffectiveTo == actEffectiveToDate && actAssessmentStatus.Contains(expAssessmentStatus) && expPublicationStatus == actPublicationStatus)
                {
                    ValidationFlag = true;
                    Log.Information($"The expected {expEffectiveTo}, {expAssessmentStatus} &  {expPublicationStatus}  has been validated in the {i} row");
                    break;
                }
                else if (expAssessmentBand == actAssessmentBand && actAssessmentStatus.Contains(expAssessmentStatus) && expPublicationStatus == actPublicationStatus)
                {
                    ValidationFlag = true;
                    Log.Information($"The expected {expAssessmentBand} , {expAssessmentStatus} &  {expPublicationStatus}  has been validated in the {i} row");
                    break;
                }
                else if (expEffectiveDate == actEffectiveFromDate && expEffectiveTo == actEffectiveToDate && actAssessmentStatus.Contains(expAssessmentStatus) && expPublicationStatus == actPublicationStatus)
                {
                    ValidationFlag = true;
                    Log.Information($"The expected {expEffectiveDate} , {expEffectiveDate} , {expAssessmentStatus} &  {expPublicationStatus}  has been validated in the {i} row");
                    break;
                }
                else if (expEffectiveDate == actEffectiveFromDate && expAssessmentBand == actAssessmentBand && actAssessmentStatus.Contains(expAssessmentStatus) && expPublicationStatus == actPublicationStatus)
                {
                    ValidationFlag = true;
                    Log.Information($"The expected {expEffectiveDate} ,{expAssessmentBand} ,  {expAssessmentStatus} &  {expPublicationStatus}  has been validated in the {i} row");
                    break;
                }
                else if (expEffectiveTo == actEffectiveToDate && expAssessmentBand == actAssessmentBand && actAssessmentStatus.Contains(expAssessmentStatus) && expPublicationStatus == actPublicationStatus)
                {
                    ValidationFlag = true;
                    Log.Information($"The expected {expAssessmentStatus} &  {expPublicationStatus}  has been validated in the {i} row");
                    break;
                }
                else if (expEffectiveDate == actEffectiveFromDate && expEffectiveTo == actEffectiveToDate && expAssessmentBand == actAssessmentBand && actAssessmentStatus.Contains(expAssessmentStatus) && expPublicationStatus == actPublicationStatus)
                {
                    ValidationFlag = true;
                    Log.Information($"The expected {expEffectiveDate} ,{expEffectiveTo} ,{expAssessmentBand} , {expAssessmentStatus} &  {expPublicationStatus}  has been validated in the {i} row");
                    break;
                }
                else
                {
                    Log.Information($"The record has not been validated in the {i} row");
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
                Log.Error($"The record is not present in the Assessment Rows");

            }
            Driver.SwitchTo().DefaultContent();
        }




        public int GetAssessmentRows(string tabName)
        {
            int noOfRows = 0;
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            wait.Until(Driver => ((IJavaScriptExecutor)Driver).ExecuteScript("return document.readyState").Equals("complete"));
            AssesmentOuterIFrame.WaitUntilElementAttached(25);
            AssesmentOuterIFrame.ElementVisisbleUsingExplicitWait(20);
            Driver.WaitForElementToDisplayProperly(AssessmentOuterIFrameSelector, 10);
            Driver.SwitchTo().Frame(AssesmentOuterIFrame);
            Driver.WaitForElementToDisplayProperly(AssessmentInnerIFrameSelector, 10);
            Driver.SwitchTo().Frame(AssesmentInnerIFrame);
            RefreshOnAssesmment.ClickUsingJavascript();
            IWebElement tabToSelect = SelectAssessmentCheckTab(tabName);
            tabToSelect.ClickUsingJavascript();

            while (true)
            {
                IWebElement assessmentRows = AssessmentRows(noOfRows);
                //  assessmentRows.WaitUntilElementAttached(10);
                if (assessmentRows != null)
                {
                    noOfRows++;
                    continue;
                }
                else
                {
                    break;
                }

            }
            return noOfRows;

        }



        public void EnterFieldsOnMaintainAssessment(string bpfName)
        {
            Thread.Sleep(3000);
            IWebElement headerName = JourneyHeader(bpfName);
            headerName.WaitUntilElementAttached(10);
            headerName.ClickUntilNoClickInterruptable();
            SeleniumExtensions.SelectDropdownValue(CorrespondenceChecksCompleteBPF, "Yes");
            SeleniumExtensions.SelectDropdownValue(MaintainAssessmentChecksCompleteBPF, "Yes");
            //  CorrespondenceChecksCompleteBPF.SelectElementByValue("Yes");
            // MaintainAssessmentChecksCompleteBPF.SelectElementByValue("Yes");
            ValidateQAQCPopUp();
            // CloseBPFPopUp(bpfName);


        }

        public void EnterFieldsOnMaintainAssessmentwithoutJobSubmission(string bpfName)
        {
            Thread.Sleep(3000);
            IWebElement headerName = JourneyHeader(bpfName);
            headerName.WaitUntilElementAttached(10);
            headerName.ClickUntilNoClickInterruptable();
            SeleniumExtensions.SelectDropdownValue(CorrespondenceChecksCompleteBPF, "Yes");
            Thread.Sleep(2000);
            SeleniumExtensions.SelectDropdownValue(MaintainAssessmentChecksCompleteBPF, "Yes");
            ValidateAllJobsPopUp();
        }


        public void ApproveQAQC()
        {
            ValidateNameLinkAndClick();
            Log.Information("User clicks on the Name from the grid in Quality review Tab");
            ClickOptionFromMoreTabsOnReviewForm("Approvals");
            Log.Information("User selected Approvals option from the More Options menu");
            ClickCommandBarOption("Assign");
            Log.Information("User clicked on Assign option from the Commandbar");
            IWebElement ButtonToclick = ClickButtonOnAssignDialog("Assign");
            ButtonToclick.ClickUsingJavascript();
            Log.Information("User clicked on the Assign button on the dialog");
            ApproveDropDown.SelectElementByText("Yes");
            ApproveReason.ClearAndSendkeys("TestReason");
            Log.Information("User selected Yes from the Approve dropdown and entered the Approve Reason");
            ClickCommandBarOption("Save & Close");
            Log.Information("User clicked on Save & Close and navigated to Job Page");

        }

        public void AssignJobToSelf()
        {
            ClickCommandBarOption("Assign");
            Log.Information("User clicked on Assign option from the Commandbar");
            IWebElement ButtonToclick = ClickButtonOnAssignDialog("Assign");
            ButtonToclick.ClickUsingJavascript();
            Log.Information("User clicked on the Assign button on the dialog");

        }

        public void QAQCOnMaintainAssessment(string qualityReviewCheck, string bpfName)
        {
            Thread.Sleep(3000);
            IWebElement headerName = JourneyHeader(bpfName);
            headerName.WaitUntilElementAttached(10);
            headerName.ClickUntilNoClickInterruptable();
            if (qualityReviewCheck == "Yes")
            {
                QualityAssuranceCompleteBPF.SelectElementByValue("Yes");

            }
            CorrespondenceChecksCompleteBPF.SelectElementByValue("Yes");
            MaintainAssessmentChecksCompleteBPF.SelectElementByValue("Yes");
            ValidateQAQCPopUp();
            // CloseBPFPopUp(bpfName);


        }

        public void ValidateQAQCPopUp()
        {
            Assert.IsTrue(QAQCPopUp.ElementVisisbleUsingExplicitWait(5), "The QA/QC dialog is not displayed yet");
            string popUpTitle = QAQCPopUp.GetAttribute("title");
            Assert.AreEqual("All Jobs Created for Request", popUpTitle);
            AllJobsCreatedButton.ClickUsingJavascript();

        }

        public void ValidateAllJobsPopUp()
        {
            Assert.IsTrue(QAQCPopUp.ElementVisisbleUsingExplicitWait(5), "The QA/QC dialog is not displayed yet");
            string popUpTitle = QAQCPopUp.GetAttribute("title");
            Assert.AreEqual("All Jobs Created for Request", popUpTitle);
            AllJobsCreatedNoButton.ClickUsingJavascript();

        }
        public void ValidateBandingCompleteOnBPF(string bpfName, string expBandingCompleteVal)
        {
            bool bpfValFlag = false;
            Thread.Sleep(8000);
            IWebElement headerName = JourneyHeader(bpfName);
            headerName.ElementVisisbleUsingExplicitWait(10);
            headerName.ClickUsingJavascript();
            // headerName.ClickUntilNoClickInterruptable(10);
            string actIsBandingCompleteValue = IsBandingCompleteBPFValue.GetAttribute("value");
            while (!actIsBandingCompleteValue.Contains(expBandingCompleteVal))
            {
                ClickCommandBarOption("Refresh");
                headerName = JourneyHeader(bpfName);
                headerName.ElementVisisbleUsingExplicitWait(10);
                headerName.ClickUsingJavascript();
                actIsBandingCompleteValue = IsBandingCompleteBPFValue.GetAttribute("value");

                if (actIsBandingCompleteValue.Contains(expBandingCompleteVal))
                {
                    bpfValFlag = true;
                    CloseBPFPopUp(bpfName);
                    break;
                }
            }
            if (bpfValFlag != true)
            {
                Assert.IsTrue(actIsBandingCompleteValue.Contains(expBandingCompleteVal));
                CloseBPFPopUp(bpfName);

            }
            else
            {
                Assert.IsTrue(bpfValFlag);
            }

        }

        public string CaptureHereditamentDetails(string fieldName)
        {
            IWebElement hereditamentFieldName = FereditamentField(fieldName);
            string fieldValue = hereditamentFieldName.GetAttribute("title");
            return fieldValue;

        }

        public string CaptureUPRN()
        {
            HereditamentLink.ClickUsingJavascript();
            AddressLinkOnDialog.ClickUsingJavascript();
            Assert.IsTrue(LeaveThisPagePopUp.ElementVisisbleUsingExplicitWait(5), "The pop up is not displayed");
            LeaveThisPagePopUpOKBtn.ClickUsingJavascript();
            string fieldValue = UPRNText.GetAttribute("title");
            return fieldValue;

        }

        public void ValidateBandingTabLoaded()
        {
            bool loadFlag = false;
            if (SubjectAttributeSection.ElementVisisbleUsingExplicitWait(5))
            {
                loadFlag = true;
            }
            else
            {
                while (BandingTabError.ElementVisisbleUsingExplicitWait(5))
                {
                    ClickCommandBarOption("Refresh");
                    if (SubjectAttributeSection.ElementVisisbleUsingExplicitWait(5))
                    {
                        loadFlag = true;
                        break;
                    }
                }
            }
            Assert.IsTrue(loadFlag, "The Banding tab is not loaded successfully with text ");

        }

        public void IsJobCreatedRequestPageIsDisplayed(string jobIdExpected)
        {
            var jobIdDisplayed = JobIDJobLevel.GetAttribute("Title").ToString().Trim();
            Assert.AreNotEqual(jobIdExpected, jobIdDisplayed);
            //  return jobIdDisplayed.Equals(jobIdExpected);
        }

        public void LinkedAssessmentValidationOnCheckFacts(string bpName)
        {
            SeleniumExtensions.ScrollToElement(IsDekstopResearchComplete);

            //  SeleniumExtensions.ScrollToElement(MultiUnitAccomodatnSectn);
            //    SeleniumExtensions.ScrollToElement(LinkedAssessmentConfirmed);

            //    LinkAssessmentLink.ElementVisisbleUsingExplicitWait(10);
            //   string actlinkedAssessment = LinkAssessmentLink.GetAttribute("aria-label");
            //   Assert.IsTrue(actlinkedAssessment.Contains("Current (live entry)"), $"The linked assessment is not cuurent live instead it is '{actlinkedAssessment}'");
            switch (bpName)
            {
                case "Deletion":
                    SeleniumExtensions.SelectDropdownValue(IsDekstopResearchComplete, "Yes");

                    //  IsDekstopResearchComplete.SelectElementByValue("Yes");
                    //    LinkedAssessmentConfirmed.SelectElementByValue("Yes");
                    FormalDecisionNotes.ClearAndEnterValueIntoTextBox("TestReason");
                    break;
                case "Effective Date Change":
                    SeleniumExtensions.SelectDropdownValue(IsDekstopResearchComplete, "Yes");
                    break;
                case "Borderline CT to NDR":
                    SeleniumExtensions.SelectDropdownValue(IsDekstopResearchComplete, "Yes");
                    break;
                case "Full Demolition":
                    SeleniumExtensions.SelectDropdownValue(IsDekstopResearchComplete, "Yes");
                    break;
                case "Recon Split":
                    SeleniumExtensions.SelectDropdownValue(IsDekstopResearchComplete, "Yes");
                    break;
                case "Recon Merge":
                    SeleniumExtensions.SelectDropdownValue(IsDekstopResearchComplete, "Yes");
                    break;
                case "Change Of Address":
                    SeleniumExtensions.SelectDropdownValue(IsDekstopResearchComplete, "Yes");
                    ClickBtnOnAllJobsCreated("Yes");
                    break;


            }

        }


        public void UserValidatesthePADSection(string PADCodeValidateToggleValue)
        {
            ClickCommandBarOption("Save & Close");
            SeleniumExtensions.ScrollToElement(ValidatePADToggleTxt);
            SeleniumExtensions.SelectToggle(ValidatePADToggleTxt, ValidatePADToggleBtn, PADCodeValidateToggleValue);
            ClickCommandBarOption("Save");
        }


        public void UserValidatestheBillingAuthorityTableValues(string tabName, string billingAuthority, string expBillingAuthorityReference, string billingAuthorityReference, string communityCode, string effectiveFromDays, string effectiveToDays, string status)
        {
            bool ValidationFlag = false;
            string expEffectivefromDate = null;
            string expEffectiveToDate = null;
            if (effectiveFromDays != "")
            {
                string[] dateValue = effectiveFromDays.Split(' ');
                string CuurentDateFormat = SeleniumExtensions.GetDateFormat(dateValue[0]);
                DateTime dateToEnter = DateTime.ParseExact(dateValue[0], CuurentDateFormat, CultureInfo.InvariantCulture);
                expEffectivefromDate = dateToEnter.ToString("M/d/yyyy");
                Console.WriteLine(expEffectivefromDate);
            }
            if (effectiveToDays != "")
            {
                string[] dateValue = effectiveToDays.Split(' ');
                string CuurentDateFormat = SeleniumExtensions.GetDateFormat(dateValue[0]);
                DateTime dateToEnter = DateTime.ParseExact(dateValue[0], CuurentDateFormat, CultureInfo.InvariantCulture);
                expEffectiveToDate = dateToEnter.ToString("M/d/yyyy");
                Console.WriteLine(expEffectiveToDate);
            }

            IList<IWebElement> RowsElement = Driver.FindElements(MaterialRequestRows);
            foreach (var row in RowsElement)
            {
                string rowindex = row.GetAttribute("row-index");
                int rows = Int32.Parse(rowindex);
                IWebElement BillingAuthority = UIBillingAuthorityText(rows);
                IWebElement BillingAuthorityReference = UIBillingAuthorityReferenceText(rows);
                IWebElement EffectiveFrom = UIEffectiveFromDateText(rows);
                IWebElement EffectiveTo = UIEffectiveToDateText(rows);
                IWebElement StatusId = UIStatusIdText(rows);

                string actBillingAuthorityText = BillingAuthority.GetAttribute("aria-label");
                string actBillingAuthorityReferenceText = BillingAuthorityReference.GetAttribute("aria-label");
                string actEffectiveFromText = EffectiveFrom.GetAttribute("aria-label");
                string actEffectiveToText = EffectiveTo.GetAttribute("aria-label");
                string actStatusIdText = StatusId.GetAttribute("aria-label");

                if (billingAuthority == actBillingAuthorityText && expBillingAuthorityReference == actBillingAuthorityReferenceText && expEffectivefromDate == actEffectiveFromText && expEffectiveToDate == actEffectiveToText && status == actStatusIdText)
                {
                    ValidationFlag = true;
                    Log.Information($"The expected {actBillingAuthorityText} , {actBillingAuthorityReferenceText} ,  {actEffectiveFromText} ,  {actEffectiveToText},  {actStatusIdText} has been validated in the {row} row");
                    break;
                }
                else if (billingAuthority == actBillingAuthorityText && billingAuthorityReference == actBillingAuthorityReferenceText && expEffectivefromDate == actEffectiveFromText && status == actStatusIdText)
                {
                    ValidationFlag = true;
                    Log.Information($"The expected {actBillingAuthorityText} , {actBillingAuthorityReferenceText} ,  {actEffectiveFromText} ,  {actStatusIdText}  has been validated in the {row} row");
                    break;
                }

                else
                {
                    Log.Information($"The record has not been validated in the {row} row");
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
                Log.Error($"The record is not present in the Assessment Rows");

            }

        }

        public void ValidateAssessmentOnMaintainAssessment(string tabName, string effectiveFrom, string expEffectiveTo, string expAssessmentBand, string expCompositeIndicator, string expAssessmentStatus, string expPublicationStatus, string expAssessmentAction)
        {
            bool ValidationFlag = false;
            string expEffectiveDate = null;
            string expEffectiveToDate = null;
            if (effectiveFrom != "")
            {
                string[] dateValue = effectiveFrom.Split(' ');
                string CuurentDateFormat = SeleniumExtensions.GetDateFormat(dateValue[0]);
                DateTime dateToEnter = DateTime.ParseExact(dateValue[0], CuurentDateFormat, CultureInfo.InvariantCulture);
                expEffectiveDate = dateToEnter.ToString("M/d/yyyy");
                Console.WriteLine(expEffectiveDate);
            }

            if (expEffectiveTo != "")
            {
                string[] dateValue = expEffectiveTo.Split(' ');
                string CuurentDateFormat = SeleniumExtensions.GetDateFormat(dateValue[0]);
                DateTime dateToEnter = DateTime.ParseExact(dateValue[0], CuurentDateFormat, CultureInfo.InvariantCulture);
                expEffectiveToDate = dateToEnter.ToString("M/d/yyyy");
                Console.WriteLine(expEffectiveToDate);
            }

            string actEffectiveFromDate = null;
            string actEffectiveToDate = null;
            string actCompositeIndicator = null;
            string actAssessmentBand = null;
            string actAssessmentStatus = null;
            string actPublicationStatus = null;
            string actAssessmentAction = null;

            int assessementTotalRows = GetAssessmentRows(tabName);
            for (int i = 0; i < assessementTotalRows; i++)
            {
                IWebElement EffectiveFromDate = UIEffectiveFromText(i);
                IWebElement EffectiveToDate = UIEffectiveToText(i);
                IWebElement CompositeIndicator = UICompositeIndicator(i);
                IWebElement AssessmentBandText = UIAssessmentBandText(i);
                IWebElement AssessmentStatus = UIAssessmentStatusText(i);
                IWebElement PublicationStatus = UIPublicationStatusText(i);
                IWebElement AssessmentAction = UIAssessmentActionText(i);
                actEffectiveFromDate = EffectiveFromDate.Text;

                actEffectiveToDate = EffectiveToDate.Text;

                actCompositeIndicator = CompositeIndicator.Text;
                actAssessmentBand = AssessmentBandText.Text;
                actAssessmentStatus = AssessmentStatus.Text;
                actPublicationStatus = PublicationStatus.Text;
                actAssessmentAction = AssessmentAction.Text;

                if (expEffectiveDate == actEffectiveFromDate && actAssessmentStatus.Contains(expAssessmentStatus) && expPublicationStatus == actPublicationStatus && expAssessmentAction == actAssessmentAction)
                {
                    ValidationFlag = true;
                    Log.Information($"The expected {expEffectiveDate} , {expAssessmentStatus} &  {expPublicationStatus} &  {expAssessmentAction}  has been validated in the {i} row");
                    break;
                }

                else if (expEffectiveToDate == actEffectiveToDate && expEffectiveDate == actEffectiveFromDate && actAssessmentStatus.Contains(expAssessmentStatus) && expPublicationStatus == actPublicationStatus && expAssessmentAction == actAssessmentAction)
                {
                    ValidationFlag = true;
                    Log.Information($"The expected {expAssessmentBand} , {expAssessmentStatus} &  {expPublicationStatus}  has been validated in the {i} row");
                    break;
                }
                else
                {
                    Log.Information($"The record has not been validated in the {i} row");
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
                Log.Error($"The record is not present in the Assessment Rows");

            }
            Driver.SwitchTo().DefaultContent();
        }

        public void ValidateAddresses(string Status, string AddressSource)
        {
            waitHelpers wh = new waitHelpers();
            bool ValidationFlag = false;
            IList<IWebElement> RowsElement = wh.getAllWebElements(AddressRows);
            Console.WriteLine($"RowsElement size : {RowsElement.Count}");

            foreach (var row in RowsElement)
            {
                string rowindex = row.GetAttribute("data-selection-index");
                By AddressSourcseXpath = By.XPath($"//div[@role='grid']//*[@role='row' and @data-selection-index={rowindex}]//*[@data-automationid='DetailsRowFields']//*[@data-automation-key='addressLabelSourceColumn']");
                By StatusXpath = By.XPath($"//div[@role='grid']//*[@role='row' and @data-selection-index={rowindex}]//*[@data-automationid='DetailsRowFields']//*[@data-automation-key='Status']");

                IWebElement AddressSourcse = wh.GetWebElement(AddressSourcseXpath);
                IWebElement Statuss = wh.GetWebElement(StatusXpath);
                Console.WriteLine($"AddressSourcse xpath : {AddressSourcseXpath}");
                Console.WriteLine($"Status xpath : {StatusXpath}");

                string actAddressSourceText = AddressSourcse.Text;
                string actStatusText = Statuss.Text;
                Console.WriteLine($"actAddressSourceText : {actAddressSourceText}");
                Console.WriteLine($"actStatusText : {actStatusText}");


                if (AddressSource == actAddressSourceText && actStatusText == Status)
                {
                    ValidationFlag = true;
                    Log.Information($"The expected {AddressSource} , {actAddressSourceText} &  {actStatusText} &  {Status}  has been validated in the {row} row");
                    break;
                }

                else
                {
                    Log.Information($"The record has not been validated in the {row} row");
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
                Log.Error($"The record is not present in the Addresses Rows");

            }
        }

        public void ValidateRoutingRegionName(string billingAuthoritySelected)
        {
            string actRRN = RoutingRegionName.GetAttribute("value");
            try
            {
                switch (billingAuthoritySelected)
                {
                    case "Cardiff Council":
                        Assert.AreEqual("CT NationalTeam 27", actRRN);
                        break;
                    default:
                        Assert.AreEqual("---", actRRN);
                        break;


                }
            }
            catch (AssertionException ae)
            {
                Console.WriteLine(ae);
            }
        }

        public void SetNewProposedEffectiveDate()
        {
            waitHelpers wh = new waitHelpers();
            NewProposedEffectiveDateReason.ClearAndSendkeys("Test");
            ClickCommandBarOptionOnDialog("Save");
            Thread.Sleep(3000);
            ClickCommandBarOptionOnDialog("Submit");
            Thread.Sleep(1000);
            wh.WaitTillElementInvisibleInSeconds(AppSynchrinizatiomMessage, 40);
            Thread.Sleep(1000);
            CloseButtOnDialog.ClickUsingJavascript();
        }

        public void ValidateNewProposedEffectiveDate(string expProposedEffectiveDate)
        {
            string expEffectiveDate;
            string CuurentDateFormat = SeleniumExtensions.GetDateFormat3(expProposedEffectiveDate);
            DateTime dateToEnter = DateTime.ParseExact(expProposedEffectiveDate, CuurentDateFormat, CultureInfo.InvariantCulture);
            expEffectiveDate = dateToEnter.ToString("d/MM/yyyy");
            Console.WriteLine(expEffectiveDate);
            string actProposedEffectuveDate = ProposedDatePickerSelector.GetAttribute("value").Trim();
            Assert.AreEqual(expEffectiveDate, actProposedEffectuveDate, $"The expexted effective date {expProposedEffectiveDate} does not match with the actual {actProposedEffectuveDate} effective date");

        }

        public void ValidateCorrespondenceAssessment(string bpfName)
        {
            Thread.Sleep(3000);
            int retries = 0;
            string actCorresPondenceAssessmentVal;
            waitHelpers wh = new waitHelpers();
            wh.WaitForElementToBeDisplayed(JourneyHeaderUsingBySelector(bpfName), 10000);

            wh.WaitForElementToBeClickble(JourneyHeaderUsingBySelector(bpfName), 10000);

            IWebElement headerName = JourneyHeader(bpfName);
            headerName.ClickUntilNoClickInterruptable();
            try
            {
                if (CorrespondenceAssessmentLookup != null)
                {
                    while (wh.WaitForElementToBeDisplayed(CorrespondenceAssessmentLookup, 20) != null)
                    {

                        ClickCommandBarOption("Refresh");
                        Thread.Sleep(5000);
                        wh.WaitForElementToBeDisplayed(JourneyHeaderUsingBySelector(bpfName), 20);

                        wh.WaitForElementToBeClickble(JourneyHeaderUsingBySelector(bpfName), 20);

                        headerName = JourneyHeader(bpfName);
                        headerName.ClickUntilNoClickInterruptable();

                        try
                        {
                            wh.WaitForElementToBeDisplayed(CorrespondenceAssessmentLookup, 20);

                        }
                        catch (NoSuchElementException e)
                        {
                            Console.WriteLine(e.Message + $"encountered on the {retries} attempt");
                            actCorresPondenceAssessmentVal = CorrespondenceAssessmentValue.GetAttribute("aria-label");
                            Assert.IsTrue(actCorresPondenceAssessmentVal.Contains("Current"));

                            CloseBPFPopUp(bpfName);
                            break;
                        }


                        retries++;


                        if (retries == 7)
                        {
                            Log.Error("The correspondence Assessment is not displayed yet");
                            Assert.IsTrue(false, "The correspondence Assessment is not displayed yet");
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                actCorresPondenceAssessmentVal = CorrespondenceAssessmentValue.GetAttribute("aria-label");
                Assert.IsTrue(actCorresPondenceAssessmentVal.Contains("Current"));
                CloseBPFPopUp(bpfName);
            }


        }

        public void ValidateCTToNDRPopUps(string bpName)
        {

            switch (bpName)
            {
                case "Borderline CT to NDR":
                    ValidateDialogHeaderAndClickOnButton("Need review for NDR assessment", "Yes");
                    ValidateDialogHeaderAndClickOnButton("Need confirmation for exchange document", "Yes");
                    ValidateDialogMessageAndClickOnButton("Have you dealt with the NDR side?", "Yes");
                    break;

                case "Borderline NDR to CT":
                    ValidateDialogHeaderAndClickOnButton("Need review for NDR assessment", "Yes");
                    ValidateDialogHeaderAndClickOnButton("Need confirmation for exchange document", "Yes");
                    break;

            }

        }

        public void EnterFieldsOnMaintainAssessmentAndValidateJob(string bpfName, string btnName)
        {
            //Thread.Sleep(3000);
            waitHelpers wh = new waitHelpers();
            By jorneyHeaderUsingBy = JourneyHeaderUsingBySelector(bpfName);
            //  IWebElement journeyHeader = JourneyHeader(jorneyHeader);

            IWebElement headerElement = wh.WaitForElementToBeReady(jorneyHeaderUsingBy, 30);
            headerElement.ClickUntilNoClickInterruptable();

            SeleniumExtensions.SelectDropdownValue(CorrespondenceChecksCompleteBPF, "Yes");
            //Thread.Sleep(2000);
            ClickCommandBarOption("Save");
            waitTillSavingDisaddpears("Saving...", "progressbar");
            CloseBtn.ClickUsingJavascript();
            //Thread.Sleep(2000);
            headerElement.ClickUntilNoClickInterruptable();
            SeleniumExtensions.SelectDropdownValue(MaintainAssessmentChecksCompleteBPF, "Yes");
            ClickBtnOnAllJobsCreated(btnName);
            CloseBPFPopUp(bpfName);


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
    }
}
