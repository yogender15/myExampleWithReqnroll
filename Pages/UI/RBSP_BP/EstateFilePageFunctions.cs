using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using POMSeleniumFrameworkPoc1.Helpers;
using POMSeleniumFrameworkPoc1.Pages.UI.CommonComponents;
using POMSeleniumFrameworkPoc1.Pages.UI.RBSP_BP;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace POMSeleniumFrameworkPoc1.Pages.UI
{
    public partial class EstateFilePage : BasePage
    {
        public void FillBillingAuthority(string billingAuthority)
        {
            FillAndSelectLookUpResult(BillingAuthorityLookUpInputFieldSelector
                , billingAuthority);
        }

        public void FillDeveloper(string developerDetails)
        {

            FillAndSelectLookUpResult(DeveloperLookUpInputFieldSelector
                , developerDetails);
        }

        public void ClickNewCommandBtn()
        {
            NewCommandMenu.ClickUsingJavascript();
        }

        public void ClickEstateFileMenu()
        {
            EstateFileMenu.ClickUsingJavascript();
        }

        public void EnterPlanningReferenceNumber(string planningRefrenceNumber)
        {
            PlanningReferenceNumberInputField.ClearAndSendkeys(planningRefrenceNumber);
        }
        public void EnterDeveloperName(string developerName)
        {
            DeveloperNameInputField.ClearAndSendkeys(developerName);
        }

        public void ClickEstateActionsTab()
        {
            EstateActionsTab.ClickUsingJavascript();
        }

        public void ClickNewEstateActionplusBtn()
        {
            NewEstateActionplusBtn.ClickUsingJavascript();
        }

        public void FillEstateActionType(string estateActionType)
        {

            FillAndSelectLookUpResult(EstateActionTypeLookUpInputFieldSelector
                , estateActionType);
        }
        public void EnterNumberOfPlots(string numberOfPlots)
        {
            NumberOfPlotsInputField.ClearAndSendkeys(numberOfPlots);
        }

        public void EnterPlotStartingNumber(string plotStartingNumber)
        {
            PlotStartingNumberInputField.ClearAndSendkeys(plotStartingNumber);
        }

        public void ClickSubmitToggleBtn()
        {
            SubmitToggleBtn.ClickUsingJavascript();
        }

        public void ClickSaveandCloseBtn()
        {
            EASaveandCloseBtn.ClickUsingJavascript();
        }

        public void ClickHouseTypesTab()
        {
            HouseTypeTab.ClickUsingJavascript();
        }

        public void ClickNewHouseTypePlusBtn()
        {
            NewHouseTypePlusBtn.ClickUsingJavascript();
        }

        public void EnterNameInputField(string name)
        {
            NameInputField.ClearAndSendkeys(name);
        }

        public void FillGroupLookUp(string group)
        {

            FillAndSelectLookUpResult(GroupLookUpInputFieldSelector, group);
        }
        public void FillTypeLookUp(string type)
        {

            FillAndSelectLookUpResult(TypeLookUpInputFieldSelector, type);
        }
        public void FillListLookUp(string list)
        {

            FillAndSelectLookUpResult(ListLookUpInputFieldSelector, list);
        }

        //***************************************Ashish's Functions******************************
        public void EnterDataInGeneralTab(string BAInputVal, string PlanningRefNumVal, string DevelopmentNameVal, string DeveloperVal)
        {
            FillAndSelectLookUpResult(BillingAuthorityLookUp, BAInputVal);
            PlanningRefNum.ClearAndEnterValueIntoTextBox(PlanningRefNumVal);
            DevelopmentName.ClearAndEnterValueIntoTextBox(DevelopmentNameVal);
            FillAndSelectLookUpResult(DeveloperLookUp, DeveloperVal);

        }

        public string GetCreatedOnValue()
        {
            var createdOnDate = CreatedOnDate.GetAttribute("value");
            Thread.Sleep(2000);
            var createdOnTime = CreatedOnTime.GetAttribute("value");
            Thread.Sleep(2000);
            var createdOnDateAndTime = createdOnDate + " " + createdOnTime;
            return createdOnDateAndTime;

        }

        public void ClickNewEstateActionButton()
        {
            NewEstateActionButton.ClickUsingJavascript();
        }

        public void EnterDataInEAPopUp(string EATVal, string NumOfPlotsVal, string PlotStartNumVal, string SubmitToggleVal)
        {
            FillAndSelectLookUpResult(EstateActionTypeLookUp, EATVal);
            NumberOfPlotsInputField.ClearAndSendkeys(NumOfPlotsVal);
            PlotStartingNumberInputField.ClearAndSendkeys(PlotStartNumVal);
            SeleniumExtensions.SelectToggle(SubmitToggleTxt, SubmitToggleBtn, SubmitToggleVal);
            EASaveandCloseBtn.ClickUsingJavascript();
        }

        public string ValidateEstateActionType()
        {
            var actualEstateActionType = EstateActionTypeCreated.Text;
            return actualEstateActionType;
        }

        public void ClickNewHouseTypeButton()
        {
            NewHouseTypeButton.ClickUsingJavascript();
        }

        public void EnterDataInHTPopUp(string nameVal, string groupVal, string typeVal, string listVal)
        {
            NameInputField.ClearAndSendkeys(nameVal);
            FillAndSelectLookUpResult(GroupLookUpInputFieldSelector, groupVal);
            FillAndSelectLookUpResult(TypeLookUpInputFieldSelector, typeVal);
            FillAndSelectLookUpResult(ListLookUpInputFieldSelector, listVal);
            HTSaveAndClose.ClickUsingJavascript();
        }

        public void ValidateHouseTypeNameAndClick(string ExpHouseTypeName)
        {
            HouseNameCreated.ElementVisisbleUsingExplicitWait(5);
            var actualEstateActionType = HouseNameCreated.GetAttribute("aria-label");
            Assert.AreEqual(ExpHouseTypeName, actualEstateActionType);
            HouseNameCreated.ClickUsingJavascript();
            Thread.Sleep(7000);
        }

        public void EnterDataInPADCodeDetailsSection(string areaVal, string roomsVal, string bedroomsVal, string bathVal, string floorsVal, string parkingVal)
        {
            Area.ClearAndSendkeys(areaVal);
            Rooms.ClearAndSendkeys(roomsVal);
            Bedrooms.ClearAndSendkeys(bedroomsVal);
            Baths.ClearAndSendkeys(bathVal);
            Floors.ClearAndSendkeys(floorsVal);
            FillAndSelectLookUpResult(ParkingLookUp, parkingVal);

        }

        public void EnterDataInPADValidationSection(string bpName, string PADCodeValidateToggleValue)
        {
            if (bpName == "Estate")
            {
                SeleniumExtensions.ScrollToElement(ValueSignificantCodeSection);
                SeleniumExtensions.ScrollToElement(PADValidationSection);
            }
            else
            {
                SeleniumExtensions.ScrollToElement(SourceCodesSection);
                SeleniumExtensions.ScrollToElement(PADValidationSection);
            }
            SeleniumExtensions.ScrollToElement(ValidatePADToggleTxt);
            SeleniumExtensions.SelectToggle(ValidatePADToggleTxt, ValidatePADToggleBtn, PADCodeValidateToggleValue);
        }

        public void ValidatePADOutcome(string expText, string expheaderSection)
        {
            IWebElement expTextElement = headerTextValidate(expheaderSection);
            Assert.IsTrue(SeleniumExtensions.IsElementVisible(expTextElement), "The element is not visible yet");
            var getStatus = expTextElement.Text;
            while (getStatus != expText)
            {
                ClickCommandBarOption("Refresh");
                if (getStatus == expText)
                {
                    Console.WriteLine("The status " + getStatus + "has been validated");
                    break;
                }
            }


        }

        public void EnterDataInBandingDecisionSection(string proposedTaxBand, string assessmentActionVal, string isBandingCompleteVal)
        {
            By lookUpFirstValue = GetFirstLookUp(proposedTaxBand);
            SeleniumExtensions.EnterInLookUpField(lookUpFirstValue, ProposedTaxBand, ProposedTaxBandSearchBtn, proposedTaxBand);
            //   FillAndSelectLookUpResult(AssessmentAction, assessmentActionVal);
            // IsBandingComplete.SelectElementByText(isBandingCompleteVal);
            SeleniumExtensions.scrollToBtnElement("Is Banding Complete?");
            SeleniumExtensions.SelectDropdownValue(IsBandingComplete, isBandingCompleteVal);
            Thread.Sleep(2000);



        }

        public void EnterDataInApprovalSection(string houseTypeApprovedVal, string outcomeReasonVal)
        {

            //  HouseTypeApproved.SelectElementByText(houseTypeApprovedVal);
            SeleniumExtensions.SelectDropdownValue(HouseTypeApproved, houseTypeApprovedVal);

            OutcomeReason.ClearAndEnterValueIntoTextBox(outcomeReasonVal);

        }

        public void ValidateHeaderTitle(string headerTitle)
        {
            string actHeaderTitle = GetHeaderTitle();
            Assert.AreEqual(headerTitle, actHeaderTitle);
        }

        public void EnterPlotSizeForEachPlot(string plotSize, string houseTypeName, string addressCardText)
        {
            IList<IWebElement> plotsAvailable = Driver.FindElements(PlotsAvailable);


            foreach (var row in plotsAvailable)
            {
                IWebElement PlotRowCheckEle = row.FindElement(PlotRowCheck);
                IWebElement plotSizeInputbox = row.FindElement(PlotSizeInput);
                IWebElement plotSizeUpBtn = row.FindElement(PlotSizeUpButton);


                PlotRowCheckEle.ClickUsingJavascript();
                plotSizeInputbox.ClearAndSendkeys(plotSize);
                Thread.Sleep(1000);
                string PlotSizeVal = plotSizeInputbox.GetAttribute("value");

                IWebElement housetypeDraggable = HouseTypeToDragAndDrop(houseTypeName);
                housetypeDraggable.ClickUsingJavascript();

                ValidateAndClickOnTab("General", "Estate File Form");
                ValidateAndClickOnTab("Plot Manager", "Estate File Form");


                AddressLabelSearchBox.ClearAndSendkeys(addressCardText);
                AddressLabelSearchBox.PressEnter();
                Thread.Sleep(7000);

                IList<IWebElement> AddressLabelsAvailable = Driver.FindElements(AddressLabelList);

                foreach (var addressLabel in AddressLabelsAvailable)
                {
                    if (addressLabel.IsElementDisplayed(5) && addressLabel.Text.Contains(addressCardText))
                    {
                        string PlotrowcheckStatus = FirstRowPlots.FindElement(PlotRowCheck).GetAttribute("aria-checked");

                        if (PlotrowcheckStatus.Equals("true"))
                        {

                        }
                        else
                        {
                            FirstRowPlots.FindElement(PlotRowCheck).ClickUsingJavascript();
                        }
                        addressLabel.ClickUsingJavascript();
                        PlotSave.ClickUsingJavascript();
                        Thread.Sleep(4000);
                        IWebElement housetypeDraggableAfterRefresh = HouseTypeToDragAndDrop(houseTypeName);
                        PlotrowcheckStatus = FirstRowPlots.FindElement(PlotRowCheck).GetAttribute("aria-checked");
                        if (AddresLabelErrorMsg.IsElementDisplayed(5) && AddresLabelErrorMsg.Text.Contains("400 BadRequest"))
                        {
                            if (PlotrowcheckStatus.Equals("true"))
                            {
                                FirstRowPlots.FindElement(PlotSizeInput).ClearAndSendkeys(plotSize);
                                Thread.Sleep(2000);
                                ValidateAndClickOnTab("General", "Estate File Form");
                                ValidateAndClickOnTab("Plot Manager", "Estate File Form");
                                housetypeDraggableAfterRefresh.ClickUsingJavascript();
                                //PlotSave.ClickUsingJavascript();
                                //Thread.Sleep(4000);

                                continue;

                            }
                            else
                            {
                                FirstRowPlots.FindElement(PlotRowCheck).ClickUsingJavascript();
                                FirstRowPlots.FindElement(PlotSizeInput).ClearAndSendkeys(plotSize);
                                Thread.Sleep(2000);
                                ValidateAndClickOnTab("General", "Estate File Form");
                                ValidateAndClickOnTab("Plot Manager", "Estate File Form");
                                housetypeDraggableAfterRefresh.ClickUsingJavascript();
                                //PlotSave.ClickUsingJavascript();
                                //Thread.Sleep(4000);

                                continue;
                            }

                        }
                        else
                        {

                            break;
                        }

                    }
                }

                break;

            }

        }

        public void ValidateHouseTypeAddressLabelForPLot(string houseTypeName, string addressCardText)
        {
            IList<IWebElement> plotsAvailable = Driver.FindElements(PlotsAvailable);


            foreach (var row in plotsAvailable)
            {
                string actHouseType = row.FindElement(PlotHouseType).Text;
                string actAddressLabel = row.FindElement(PlotAddressLabel).Text;

                if (actHouseType == houseTypeName && actAddressLabel == addressCardText)
                {
                    break;
                }
                else
                {
                    while (actHouseType == null && actAddressLabel == null)
                    {
                        ClickCommandBarOption("Refresh");
                        ClickOnTab("Plot Manager", "Estate File Form");
                        if (actHouseType == houseTypeName && actAddressLabel == addressCardText)
                        {
                            break;
                        }
                    }
                    break;
                }



            }

        }

        public void ValidateStatusForAutoProcessedPlots(string plotStatus)
        {
            IList<IWebElement> plotsAvailable = Driver.FindElements(PlotsAvailable);


            foreach (var row in plotsAvailable)
            {
                string actPlotStatus = row.FindElement(PlotStatusCol).GetAttribute("value");
                Assert.AreEqual(plotStatus, actPlotStatus, $"The expected plot status {plotStatus} has been validated");
                break;
            }

        }

        public string ValidateApprovalHistoryText(string houseTypeName)
        {
            IWebElement ApprovalNotification = ApprovalHistoryText(houseTypeName);
            waitHelpers wh = new waitHelpers();
            wh.isElementDisplayed(By.CssSelector($"[aria-label='Approval History'] [class*='ag-row'] [col-id='voa_name'] a[aria-label='House Type {houseTypeName} Approval required']"), 60);
            //$"//[aria-label='Approval History'] [class*='ag-row'] [col-id='subject'] a[aria-label='House Type {houseTypeName} requires approval.']"), 60);
            Assert.IsTrue(ApprovalNotification.IsElementVisible(), "The approval notification is visible for house Type");
            IList<IWebElement> AHNrows = DriverHelper.Driver.FindElements(AprrovalNotificationHistoryRows);
            int sizeOfAHN = AHNrows.Count();
            return sizeOfAHN.ToString();

        }

        public int ValidaterejectedlHistoryText(string houseTypeName)
        {

            IWebElement rejectedNotification = RejectedrecordText(houseTypeName);
            waitHelpers wh = new waitHelpers();
            bool eleVisble = wh.isElementDisplayed(By.CssSelector($"[aria-label='All House Type Rejected Approvals'] [class*='ag-row'] [col-id='voa_name'] a[aria-label='{houseTypeName}']"), 60);
            Assert.IsTrue(rejectedNotification.IsElementVisible(), "The approval notification is visible for house Type");
            IList<IWebElement> AHNrows = DriverHelper.Driver.FindElements(rejectedNotificationHistoryRows);
            int sizeOfAHN = AHNrows.Count();
            return sizeOfAHN;
        }


        public void SelectNewEsateFileOptionFromEstateFileLookUp()
        {
            HereditamentDetailsTitle.WaitUntilElementAttached(5);
            HereditamentDetailsTitle.ScrollAndClick();
            EstateFileSearchButton.ScrollAndClick();
            NewEstateFileButton.ScrollAndClick();
        }
        public void EnterDetailsInEstateFilePopUp(string BAInputVal, string PlanningRefNumVal, string DevelopmentNameVal, string DeveloperVal)
        {
            FillAndSelectLookUpResult(EFBillingAuthorityLookUp, BAInputVal);
            EFPlanningRefNum.ClearAndEnterValueIntoTextBox(PlanningRefNumVal);
            EFDevelopmentName.ClearAndEnterValueIntoTextBox(DevelopmentNameVal);
            FillAndSelectLookUpResult(EFDeveloperLookUp, DeveloperVal);
            EFSaveAndClose.ClickUsingJavascript();

        }

        public void EnterClosureDateInCloseJourney(string journeyName)
        {
            IWebElement headerName = JourneyHeader(journeyName);
            headerName.ClickUntilNoClickInterruptable();
            var currentDate_02 = DateTime.Now.ToString("d, MMMM, yyyy");
            ScrollDiv.ScrollAndClick(ClosureDateCloseJourney, 20);
            SeleniumExtensions.SetTheDateForTheTableCalendar(currentDate_02, 1);

        }

        public void SelectEstateFileInHereditamentDetails(string estateFileName)
        {
            ExternalSLATitle.ScrollAndClick();
            FillAndSelectLookUpResult(HDEstateFileLookup, estateFileName);

        }

        public void ValidateEstateFileName(string estateFileName)
        {
            string expEstateFile = HDEstateFileNameField.GetAttribute("title");
            Assert.AreEqual(estateFileName, expEstateFile);

        }


        public void ValidateFieldsPADCodeDetails(string expHeatingVal, string expConservatoryType, string expConservatoryVal)
        {
            string actHeatingValue = Heating.GetAttribute("aria-label").Trim();
            Assert.AreEqual(expHeatingVal, actHeatingValue);

            string actConservatoryType = ConservatoryType.GetAttribute("aria-label").Trim();
            Assert.AreEqual(expConservatoryType, actConservatoryType);

            string actConservatoryVal = ConservatoryArea.GetAttribute("value").Trim();
            Assert.AreEqual(expConservatoryVal, actConservatoryVal);

            Rooms.ClearAndSendkeys("1000");
            ClickCommandBarOption("Save");
            bool roomsErrFlag = RoomsErrorMessage.ElementVisisbleUsingExplicitWait(2);
            Assert.IsTrue(roomsErrFlag, "The error message is visible");


        }

        public bool drawEstateExtent()
        {
            bool extentCreated = false;


            return extentCreated;
        }

        public void ClickOnNewComparablePropertyButton()
        {
            NewCTBTButton.ClickUsingJavascript();
        }

        public void createEstateExtent()
        {
            waitHelpers wh = new waitHelpers();
            Actions act = new Actions(DriverHelper.Driver);
            //Thread.Sleep(10000);
            By clsEleSlector = By.CssSelector("[title='Close']");
            wh.isElementDisplayed(clsEleSlector, 180);
            IWebElement clsEle = wh.GetWebElement(clsEleSlector);
            wh.clickOnElement(clsEleSlector);
            IWebElement ele = wh.GetWebElement(By.XPath("//div[contains(@id,'map_WebTiled')]//img[14]"));
            int width = ele.Size.Width;
            int height = ele.Size.Height;
            Thread.Sleep(5000);
            wh.jsClickOnElement(By.XPath("//button[@id='estate-draw']"));
            Thread.Sleep(2000);
            act.MoveToElement(ele, -width / 6, -height / 6).Click().Pause(TimeSpan.FromMilliseconds(1000)).Build().Perform();
            Thread.Sleep(2000);
            act.MoveToElement(ele, width / 6, -height / 6).Click().Pause(TimeSpan.FromMilliseconds(1000)).Build().Perform();
            Thread.Sleep(500);
            act.MoveToElement(ele, width / 6, height / 6).Click().Pause(TimeSpan.FromMilliseconds(1000)).Build().Perform();
            Thread.Sleep(500);
            act.MoveToElement(ele, -width / 6, height / 6).DoubleClick().Pause(TimeSpan.FromMilliseconds(4000)).Build().Perform();
            Thread.Sleep(2000);
            wh.WaitForElementToBeClickble(By.XPath("//button[@id='estate-save' and not(@disabled)]"), 120);
            wh.clickOnElement(By.XPath("//button[@id='estate-save' and not(@disabled)]"));
            wh.clickOnElement(By.CssSelector("button[id='send-to-cms-btn']"));
            wh.clickOnElement(By.XPath("//div[@id='vms-to-cms-success']//button[contains(text(),'BACK')]"));
        }

        public void ValidateCTBTIntegrationForEstateFileExtent(string expVMSTitle)
        {
            Thread.Sleep(5000);
            waitHelpers wh = new waitHelpers();

            IReadOnlyCollection<string> windowhandles = Driver.WindowHandles;
            foreach (var window in windowhandles)
            {
                Driver.SwitchTo().Window(window);
                Console.WriteLine(Driver.Title);
                if (Driver.Title.Contains("VMS"))
                {
                    break;
                }

            }
            wh.isElementDisplayed(VMSWindowIFrameSelectorBy, 30);
            Driver.SwitchTo().Frame(VMSWindowIFrame);
            if (wh.isElementDisplayed(HMRCSignInBy, 30))
            {
                HMRCSignIn.ClickUsingJavascript();
                Thread.Sleep(20000);

            }
            CommonPage commonpage = new CommonPage();
            commonpage.waitTillCTBTLoadingDisaddpears();
            SeleniumExtensions.WaitForReadyStateToComplete();
            if (VmsTitle.IsElementDisplayed(10))
            {
                Console.WriteLine(VmsTitle.Text.Trim());

            }
            By WebMap = By.XPath("//div[contains(@id,'map_layers')]");
            Assert.IsTrue(wh.isElementDisplayed(WebMap, 180));
            //Thread.Sleep(20000);
            createEstateExtent();
            Driver.Close();
            Driver.SwitchTo().Window(windowhandles.First());
            Console.WriteLine(Driver.Title);


        }

        public void ValidateCTBTIntegrationForEstateFile(string expVMSTitle)
        {
            Thread.Sleep(5000);
            waitHelpers wh = new waitHelpers();

            IReadOnlyCollection<string> windowhandles = Driver.WindowHandles;
            foreach (var window in windowhandles)
            {
                Driver.SwitchTo().Window(window);
                Console.WriteLine(Driver.Title);
                if (Driver.Title.Contains("VMS"))
                {
                    break;
                }

            }


            //WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            //wait.Until(Driver => ((IJavaScriptExecutor)Driver).ExecuteScript("return document.readyState").Equals("complete"));
            wh.isElementDisplayed(VMSWindowIFrameSelectorBy, 30);
            //Driver.WaitForElementToDisplayProperly(VMSWindowIFrameSelectorBy, 5);
            Driver.SwitchTo().Frame(VMSWindowIFrame);

            if (wh.isElementDisplayed(HMRCSignInBy, 30))
            {
                HMRCSignIn.ClickUsingJavascript();
            }
            CommonPage commonpage = new CommonPage();
            commonpage.waitTillCTBTLoadingDisaddpears();
            SeleniumExtensions.WaitForReadyStateToComplete();
            if (VmsTitle.IsElementDisplayed(10))
            {
                Console.WriteLine(VmsTitle.Text.Trim());

            }
            By WebMap = By.XPath("//div[contains(@id,'map_WebTiled'')]");
            //By hereditamentInfo = By.XPath("//*[contains(text(),'Hereditament Info')]");
            //By addAdditionalLayers = By.XPath("//*[contains(text(),'Add Additional Layers')]");

            By subjectProperty = By.XPath("//*[contains(text(),'Subject Property')]");
            By AdvancedSelectionTools = By.XPath("//*[contains(text(),'Advanced Selection Tools')]");

            Assert.IsTrue(wh.isElementDisplayed(subjectProperty, 180));
            Assert.IsTrue(wh.isElementDisplayed(AdvancedSelectionTools, 30));
            Driver.Close();
            Driver.SwitchTo().Window(windowhandles.First());
            Console.WriteLine(Driver.Title);
        }


        public void ClickOnNewVSCLinkButton()
        {
            NewValueSignificantCodeLink.ClickUsingJavascript();
        }

        public void EnterDataInVSCLinkPopUp(string vscValue, string vscDesc)
        {
            FillAndSelectLookUpResult(ValueSignificantCode, vscValue);
            VSCDescription.ClearAndSendkeys(vscDesc);
            VSCSaveAndClose.ClickUsingJavascript();
        }

        public void ValidateFieldsOnBandingBPF(string journeyName)
        {
            IWebElement headerName = JourneyHeader(journeyName);
            headerName.ClickUntilNoClickInterruptable();

            string actBandingCompleteVal = BandingCompleteBPF.Text;
            Assert.AreEqual("No", actBandingCompleteVal);

            string actResetPADValidationVal = ResetPADValidationBPF.Text;
            Assert.AreEqual("No", actResetPADValidationVal);
            CloseBPFPopUp(journeyName);

        }

        public void ValidateFieldsInSubjectAttributesSection()
        {
            IList<IWebElement> subjectAttributeTexts = SubjectAttributesList;
            List<string> eleLabelList = new List<string>();

            foreach (var ele in subjectAttributeTexts)
            {
                string eleLabel = ele.Text.Trim();
                eleLabelList.Add(eleLabel);

            }
            if (eleLabelList.Contains("Property Attributes") && eleLabelList.Contains("Additional Attributes") && eleLabelList.Contains("Value Significant Codes") && eleLabelList.Contains("Plot Information"))
            {
                Assert.IsTrue(true, "All the attributes are present");
            }

        }

        public void ValidateLatestApprovalHistory(string approvalAction, string houseTypeName)
        {
            //IList<IWebElement> AHNrows = DriverHelper.Driver.FindElements(AprrovalNotificationHistoryRows);
            //int sizeOfAHN = AHNrows.Count();
            //while (sizeOfAHN == currAHNCount)
            //{
            //    MoreCommandsAHN.ClickUsingJavascript();
            //    IWebElement refreshAHN = ApprovalHistoryMoreCommands("Refresh");
            //    refreshAHN.ClickUsingJavascript();

            //    AHNrows = DriverHelper.Driver.FindElements(AprrovalNotificationHistoryRows);
            //    sizeOfAHN = AHNrows.Count();

            //    if (sizeOfAHN > currAHNCount)
            //    {
            //        break;
            //    }

            //}

            string LatestDateTimeValue = SeleniumExtensions.GetLatestRowByDate(AprrovalNotificationHistoryRows, "CreatedOn");
            // Console.WriteLine(LatestDateTimeValue);
            IWebElement VOANameCellValue = LatestSubjectCell(LatestDateTimeValue);
            string latestApprovalNotification = VOANameCellValue.GetAttribute("aria-label");
            if (approvalAction == "Approve")
            {
                Assert.AreEqual($"House Type {houseTypeName} requires approval.", latestApprovalNotification);
            }
            else if (approvalAction == "Reject")
            {
                Assert.AreEqual($"House Type {houseTypeName} has been Rejected", latestApprovalNotification);
            }

        }

        public void RaiseInspectionInEAPopUp(string EATVal, string reasonForInspection, string reasonForInspectionComments, string inspectionAllocation, string SubmitToggleVal)
        {
            FillAndSelectLookUpResult(EstateActionTypeLookUp, EATVal);
            ReasonForInspectionSelect.SelectElementByText(reasonForInspection);
            ReasonForInspectionInput.ClearAndSendkeys(reasonForInspectionComments);
            InspectionAllocationSelect.SelectElementByText(inspectionAllocation);
            SeleniumExtensions.SelectToggle(SubmitToggleTxt, SubmitToggleBtn, SubmitToggleVal);
            EASaveandCloseBtn.ClickUsingJavascript();
        }

        public string ValidateAndClickInspectionCreated()
        {
            var actualEstateActionType = InspectionRaised.GetAttribute("aria-label");
            InspectionRaised.DoubleClickElementUsingJSExecutor();
            return actualEstateActionType;
        }

        public void ValidateInspectionDataInGeneralSection(string expInspectionReason, string expInspectionReasonComment)
        {
            var actInspectionReason = InspectionReasonText.GetAttribute("title").Trim();
            Assert.AreEqual(expInspectionReason, actInspectionReason);
            var actInspectionReasonComment = InspectionReasonComment.Text.Trim();
            Assert.AreEqual(expInspectionReasonComment, actInspectionReasonComment);
        }

        public void ValidateDuplicatePlotErrorMsgAndClickOK()
        {
            if (DuplicatePlotErrorMSgDialog.ElementVisisbleUsingExplicitWait(5))
            {
                var actInspectionReason = DuplicatePlotErrorMSgDialog.Text.Trim();
                Assert.IsTrue(actInspectionReason.Contains("The following PlotNumbers already exist in estateId"), "The error message was not for duplicate plots");
                DuplicatePlotErrorDialogOKBtn.ClickUsingJavascript();
            }
        }

        public void DisplayAllColumnsForPlots()
        {
            var count = 0;
            if (PlotManagerShowColumns.ElementVisisbleUsingExplicitWait(10))
            {
                PlotManagerShowColumns.ClickUsingJavascript();
            }
            IList<IWebElement> plotColumns = Driver.FindElements(PlotManagerShowColumnsOptions);

            foreach (var option in plotColumns)
            {

                string optionChecked = option.GetAttribute("aria-checked");
                if (optionChecked == "false")
                {
                    option.ClickUsingJavascript();
                    break;

                }
                else
                {
                    count++;
                    continue;
                }

            }
            if (count != plotColumns.Count)
            {
                DisplayAllColumnsForPlots();
            }
            else
            {
                PlotManagerShowColumns.ClickUsingJavascript();
            }
        }


        public void ValidatePlotBillingAuthority(string floorLevel, string expBillingAuthority)
        {
            IList<IWebElement> plotsAvailable = Driver.FindElements(PlotsAvailable);


            foreach (var row in plotsAvailable)
            {
                IWebElement PlotRowCheckEle = row.FindElement(PlotRowCheck);
                IWebElement FloorLavelInputbox = row.FindElement(FloorLevelInput);


                PlotRowCheckEle.ClickUsingJavascript();
                string BillingAuthorityName = row.FindElement(PMBillingAuthorityColumnVal).Text;
                Assert.AreEqual(expBillingAuthority, BillingAuthorityName);
                FloorLavelInputbox.ClearAndSendkeys(floorLevel);
                PlotSave.ClickUsingJavascript();

                break;

            }

        }

        public void enterPlotSize(String value)
        {
            waitHelpers wh = new waitHelpers();
            By plotSizeInPut = By.CssSelector("div[data-automation-key='PlotSizeColumn'] input[class*='ms-spinButton-input']");
            wh.GetWebElement(plotSizeInPut).ClearAndSendkeys(value);
            //wh.GetWebElement(plotSizeInPut).SendKeys(value);
            By rowRadioBtn = By.XPath("(//div[@data-automationid='DetailsRowCheck'])[2]");
            string ariaChecked = wh.GetWebElement(rowRadioBtn).GetAttribute("aria-checked");
            if (!ariaChecked.Equals("true"))
            {
                wh.clickOnWebElement(rowRadioBtn);
            }
        }

        public void enterPlotSize(String position, String value)
        {
            waitHelpers wh = new waitHelpers();
            By plotSizeInPut = By.XPath($"(//div[@data-automation-key='PlotSizeColumn']//input[contains(@class,'ms-spinButton-input')])[{position}]");
            wh.GetWebElement(plotSizeInPut).SendKeys(value);
            By rowRadioBtn = By.XPath($"(//div[@class='ms-List-surface']//div[@data-automationid='DetailsRowCheck'])[{position}]");
            wh.clickOnWebElement(rowRadioBtn);
        }

        public void searchAddressAndSelectAddr(String postcode)
        {
            waitHelpers wh = new waitHelpers();
            By addressSearch = By.XPath("//input[@placeholder='Search for address...']");
            wh.GetWebElement(addressSearch).SendKeys(postcode);
            By hereditament = By.XPath("//div[@class='addressLabels']/div[@class='ms-HoverCard-host']/div[@class='addressLabelCard']");
            wh.clickOnWebElement(hereditament);
        }

        public void searchAddressAndSelectAddrForEstateFile(String fieldName, Dictionary<string, string> testData)
        {
            string value = testData[fieldName];
            waitHelpers wh = new waitHelpers();
            By addressSearch = By.XPath("//input[@placeholder='Search for address...']");
            By errMsg = By.CssSelector("[class='plotsErrorMessage']");
            By unsavedMsg = By.Id("message-unsavedchangesnotification");
            bool isEleDisplayed = false;
            bool isPlotDisplayed = false;
            var requestPage = new RequestPage();
            int i = 1;
            while (!isEleDisplayed)
            {
                wh.GetWebElement(addressSearch).ClearAndSendkeys(value);
                Thread.Sleep(5 * 1000);
                enterPlotSize("180");
                Thread.Sleep(3 * 1000);
                By hereditament = By.XPath($"(//div[@id='addresses']//div[contains(@class,'ms-FocusZone')]//div[@class='addressLabelCardLines'])[{i}]");
                // $"//div[@class='addressLabels']/div[contains(@class,'ms-TooltipHost')][{i}]/div[@class='addressLabelCard']");
                //By hereditamentCount = By.XPath($"//div[@class='addressLabels']/div[@class='ms-HoverCard-host']/div[@class='addressLabelCard']");

                isPlotDisplayed = wh.isElementDisplayed(hereditament, 60);
                if (isPlotDisplayed)
                {
                    wh.clickOnWebElement(hereditament);
                    Thread.Sleep(3 * 1000);
                    By rowRadioBtn = By.XPath("(//div[@data-automationid='DetailsRowCheck'])[2]");
                    string ariaChecked = wh.GetWebElement(rowRadioBtn).GetAttribute("aria-checked");
                    if (!ariaChecked.Equals("true"))
                    {
                        wh.clickOnWebElement(rowRadioBtn);
                    }
                    selectHouseType();
                }
                SeleniumExtensions.scrollToBtnBasedOnLabelAndClick("Save", "Plot Manager");
                Thread.Sleep(2 * 1000);
                isEleDisplayed = SeleniumExtensions.waitUntillElementInvisible(unsavedMsg);
                if (!isEleDisplayed || i > 50)
                {
                    break;
                }
                // if (isEleDisplayed)
                // {
                //     clickOnMainMenuMoreElement_New("Refresh");
                //     Thread.Sleep(5 * 1000);
                //     requestPage.clickTabOnRequestForm("Plot Manager", "Estate File Form");
                //     isEleDisplayed = false;
                // }
                //int hereditamentCountVar = wh.getAllWebElements(hereditamentCount).Count;

                i = i + 1;
            }
        }

        public void searchAddressAndSelectAddrForEstateFileWithOutHT(String fieldName, Dictionary<string, string> testData)
        {
            string value = testData[fieldName];
            waitHelpers wh = new waitHelpers();
            By addressSearch = By.XPath("//input[@placeholder='Search for address...']");
            By errMsg = By.CssSelector("[class='plotsErrorMessage']");
            By unsavedMsg = By.Id("message-unsavedchangesnotification");
            bool isEleDisplayed = false;
            bool isPlotDisplayed = false;
            var requestPage = new RequestPage();
            int i = 1;
            while (!isEleDisplayed)
            {
                wh.GetWebElement(addressSearch).ClearAndSendkeys(value);
                Thread.Sleep(5 * 1000);
                enterPlotSize("180");
                Thread.Sleep(3 * 1000);
                By hereditament = By.XPath($"(//div[@id='addresses']//div[contains(@class,'ms-FocusZone')]//div[@class='addressLabelCardLines'])[{i}]");
                isPlotDisplayed = wh.isElementDisplayed(hereditament, 60);
                if (isPlotDisplayed)
                {
                    wh.clickOnWebElement(hereditament);
                    Thread.Sleep(3 * 1000);
                    By rowRadioBtn = By.XPath("(//div[@data-automationid='DetailsRowCheck'])[2]");
                    string ariaChecked = wh.GetWebElement(rowRadioBtn).GetAttribute("aria-checked");
                    if (!ariaChecked.Equals("true"))
                    {
                        wh.clickOnWebElement(rowRadioBtn);
                    }
                   // selectHouseType();
                }
                SeleniumExtensions.scrollToBtnBasedOnLabelAndClick("Save", "Plot Manager");
                Thread.Sleep(2 * 1000);
                isEleDisplayed = SeleniumExtensions.waitUntillElementInvisible(unsavedMsg);
                if (!isEleDisplayed || i > 50)
                {
                    break;
                }
                
                i = i + 1;
            }
        }





        public void searchAddressAndSelectAddr(String position, String postcode)
        {
            waitHelpers wh = new waitHelpers();
            By addressSearch = By.XPath("//input[@placeholder='Search for address...']");
            wh.GetWebElement(addressSearch).SendKeys(postcode);
            By hereditament = By.XPath($"(//div[@class='addressLabels']/div[@class='ms-HoverCard-host']/div[@class='addressLabelCard'])[{position}]");
            Actions act = new Actions(DriverHelper.Driver);
            // act.Click(wh.GetWebElement(hereditament)).Build().Perform();
            act.Click(wh.GetWebElement(hereditament)).Pause(TimeSpan.FromMilliseconds(500)).Build().Perform();

            //wh.clickOnElement(hereditament);
        }

        public void searchAddressAndSelectAddr1(String position, String postcode)
        {
            waitHelpers wh = new waitHelpers();
            By addressSearch = By.XPath("//input[@placeholder='Search for address...']");
            //wh.GetWebElement(addressSearch).SendKeys(postcode);
            //int i = 15;
            By hereditaments = By.XPath($"(//div[@id='addresses']//div[@class='addressLabelCard'])");
            IList<IWebElement> Eles = wh.GetWebElements(hereditaments);
            for (int pos = 1; pos < Eles.Count; pos++)
            {
                Random rnd = new Random();
                int plotSizeInt = rnd.Next(300);
                enterPlotSize(pos.ToString(), (String)plotSizeInt.ToString());
                By hereditament = By.XPath($"(//div[@id='addresses']//div[@class='addressLabelCard'])[{position}]");
                bool isPlotDisplayed = wh.isElementDisplayed(hereditament, 60);
                if (isPlotDisplayed)
                {
                    wh.clickOnWebElement(hereditament);
                    Thread.Sleep(3 * 1000);
                    By rowRadioBtn = By.XPath($"(//div[@class='ms-List-surface']//div[@data-automationid='DetailsRowCheck'])[{pos}]");
                    string ariaChecked = wh.GetWebElement(rowRadioBtn).GetAttribute("aria-checked");
                    if (!ariaChecked.Equals("true"))
                    {
                        wh.clickOnWebElement(rowRadioBtn);
                    }
                    selectHouseType();
                    //wh.clickOnWebElement(rowRadioBtn);
                    SeleniumExtensions.scrollToBtnBasedOnLabelAndClick("Save", "Plot Manager");
                    //i++;
                }
            }
        }

        public void searchAddressAndSelectAddrWithCount(String plotCount, String positionStr, String postcode)
        {

            waitHelpers wh = new waitHelpers();
            //String[] positionArr = positionStr.Split(',');
            By addressSearch = By.XPath("//input[@placeholder='Search for address...']");
            wh.GetWebElement(addressSearch).SendKeys(postcode);
            for (int position = 1; position < Int32.Parse(plotCount); position++)
            {
                Random rnd = new Random();
                int plotSizeInt = rnd.Next(300);
                enterPlotSize((String)position.ToString(), (String)plotSizeInt.ToString());
                selectHouseType();
                By hereditament = By.XPath($"(//div[@class='addressLabels']/div[@class='ms-HoverCard-host']/div[@class='addressLabelCard'])[1]");
                Thread.Sleep(1000);
                wh.jsClickOnElement(hereditament);
                By rowRadioBtn = By.XPath($"(//div[@class='ms-List-surface']//div[@data-automationid='DetailsRowCheck'])[{position}]");
                wh.clickOnWebElement(rowRadioBtn);
                //Actions act = new Actions(DriverHelper.Driver);
                //act.Click(wh.GetWebElement(hereditament)).Pause(TimeSpan.FromMilliseconds(1000)).Build().Perform();
            }
        }

        public void selectHouseType()
        {
            waitHelpers wh = new waitHelpers();
            By housetype = By.CssSelector("div[class='houseTypeCard']");
            //Actions act = new Actions(DriverHelper.Driver);
            //act.Click(wh.GetWebElement(housetype)).Pause(TimeSpan.FromSeconds(90)).Build().Perform();

            wh.clickOnElement(housetype);
        }

        public bool plotsErrorMsgAvailability()
        {
            waitHelpers wh = new waitHelpers();
            By errMsg = By.CssSelector("[class='plotsErrorMessage']");
            return wh.GetWebElement(errMsg).Displayed;
        }

        public void clickOnEstateRefreshBtn(String btnName)
        {
            waitHelpers wh = new waitHelpers();
            WebDriverWait ww = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(3));
            try
            {
                By eleBy = By.XPath($"//ul[@aria-label='Estate Action Commands']//button[contains(@aria-label,'{btnName}')]");
                wh.clickOnElement(eleBy, ww);
            }
            catch (Exception e)
            {
                String moreCommandsLocator = $"//ul[@aria-label='Estate Action Commands']//button[contains(@aria-label,'More commands')]";
                By moreCommandsBy = SeleniumExtensions.getLocator(moreCommandsLocator);
                String btnLocator = $"//div[contains(@id,'OverflowButton_buttonid')]//button[@aria-label='{btnName}']";
                By btnBy = SeleniumExtensions.getLocator(btnLocator);
                wh.clickOnElement(moreCommandsBy, ww);
                wh.clickOnElement(btnBy, ww);
            }
        }


        public void clickOnCorrespondenceRefreshBtn(String btnName)
        {
            waitHelpers wh = new waitHelpers();
            WebDriverWait ww = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(3));
            try
            {
                By eleBy = By.XPath($"//ul[@aria-label='Outbound Correspondence Commands']//button[contains(@aria-label,'{btnName}')]");
                wh.clickOnElement(eleBy, ww);
            }
            catch (Exception e)
            {
                String moreCommandsLocator = $"//ul[@aria-label='Outbound Correspondence Commands']//button[contains(@aria-label,'More commands')]";
                By moreCommandsBy = SeleniumExtensions.getLocator(moreCommandsLocator);
                String btnLocator = $"//div[contains(@id,'OverflowButton_buttonid')]//button[@aria-label='{btnName}']";
                By btnBy = SeleniumExtensions.getLocator(btnLocator);
                wh.clickOnElement(moreCommandsBy, ww);
                wh.clickOnElement(btnBy, ww);
            }
        }


        public bool isDataEnhancementLiskDisplayed(String linkName, String refreshBtn)
        {
            bool isStatusDisplayed = false;
            int i = 0;
            waitHelpers wh = new waitHelpers();
            do
            {
                By locator = By.XPath($"//a[@aria-label='{linkName}']");
                clickOnEstateRefreshBtn(refreshBtn);
                isStatusDisplayed = wh.GetWebElement(locator, 5);
                SeleniumExtensions.WaitForReadyStateToComplete();
                i++;
                if (i == 20)
                {
                    Assert.IsTrue(isStatusDisplayed);
                    break;
                }
            } while (!isStatusDisplayed);
            return isStatusDisplayed;
        }

        public bool isCorrespondenceLinkDisplayed(String linkName, String refreshBtn)
        {
            bool isStatusDisplayed = false;
            int i = 0;
            waitHelpers wh = new waitHelpers();
            do
            {
                By locator = By.XPath($"//a[@aria-label='{linkName}']");
                clickOnCorrespondenceRefreshBtn(refreshBtn);
                isStatusDisplayed = wh.GetWebElement(locator, 5);
                SeleniumExtensions.WaitForReadyStateToComplete();
                i++;
                if (i == 20)
                {
                    Assert.IsTrue(isStatusDisplayed);
                    break;
                }
            } while (!isStatusDisplayed);
            return isStatusDisplayed;
        }

        public String GetUniqueAddressForProperty_Estate(string postCode, Dictionary<string, string> testData)
        {
            string value = testData[postCode];
            AddressSearchInputOnDialog.ClearAndSendkeys(value);
            BtnSearchOnDialog.ClickUsingJavascript();
            String addressValue = "";
            while (true)
            {
                IList<IWebElement> AddressesRows = Driver.FindElements(AddressRow);
                bool found = false;
                int uniqueAddrNum = 0;
                foreach (var row in AddressesRows)
                {
                    row.ElementVisisbleUsingExplicitWait(10);
                    uniqueAddrNum = uniqueAddrNum + 1;
                    String addressXpath = $"(//*[@data-automation-key='addressString']//div[contains(@class,'ms-TooltipHost root')])[{uniqueAddrNum}]";
                    addressValue = DriverHelper.Driver.FindElement(By.XPath(addressXpath)).Text.Trim();
                    row.ClickUsingJavascript();
                    BtnUseAddressOnDialog.ClickUsingJavascript();
                    if (DuplicateAddressHeader.IsElementVisibleUsingByLocator(5))
                    {
                        DuplicateAddressCloseBtn.ClickUsingJavascript();
                        addressValue = "";
                        continue;
                    }
                    else
                    {
                        found = true;
                        break;
                    }
                }
                if (found)
                {
                    break;
                }
                if (NextPageOnDialog.IsEnabled())
                {
                    NextPageOnDialog.ClickUsingJavascript();
                }
                else
                {
                    Console.WriteLine("No Unique Address Found");
                    addressValue = "";
                    break;
                }
            }
            return addressValue;
        }


        public void BlockEstateFile(string EATVal, string SubmitToggleVal)
        {
            FillAndSelectLookUpResult(EstateActionTypeLookUp, EATVal);
            ReasonForBlocking.ClearAndSendkeys("Test");
            SeleniumExtensions.SelectToggle(SubmitToggleTxt, SubmitToggleBtn, SubmitToggleVal);
            EASaveandCloseBtn.ClickUsingJavascript();
        }

        public void UnblockEstateFileByApproving()
        {
            SeleniumExtensions.SelectDropdownValue(ReviewApproved, "Yes");
            ReviewApprovedReason.ClearAndSendkeys("Test");
        }

        public void ApproveHouseType(string approvalStatus, string outcomeReasonVal)
        {

            if (approvalStatus == "Approves")
            {
                SeleniumExtensions.SelectDropdownValue(HouseTypeApproved, "Yes");

            }
            else
            {
                SeleniumExtensions.SelectDropdownValue(HouseTypeApproved, "No");

            }

            OutcomeReason.ClearAndEnterValueIntoTextBox(outcomeReasonVal);

        }
    }
}
