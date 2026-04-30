using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using POMSeleniumFrameworkPoc1.DTO;
using POMSeleniumFrameworkPoc1.Helpers;
using POMSeleniumFrameworkPoc1.Pages.UI.RBSP_BP;
using Reqnroll;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace POMSeleniumFrameworkPoc1.Pages.UI.CommonComponents
{
    public partial class RequestPage
    {

        public ScenarioContext _scenarioContext;
        public FeatureContext _featureContext;

        public void ClickRequestMenuItem()
        {
            RequestsMenuElement.Click();
        }

        public void ClickNewRequestPlusBtn()
        {
            NewRequestPlusBtnElement.ClickUsingJavascript();
        }

        public void ClickRequestActionDropdownMenuBtn()
        {
            RequestActionMenu.ClickUsingJavascript();
        }

        public void ClickJobActionDropdownMenuBtn()
        {
            JobActions.ClickUsingJavascript();
        }

        public void ClickValidateRequestSubMenuOption()
        {
            ValidateRequestSubMenu.Click();
        }
        public bool IsJobCreatedRequestPageIsDisplayed(string jobIdExpected)
        {
            var jobIdDisplayed = JobIdFieldOnRequestPage.GetAttribute("Title").ToString().Trim();
            return jobIdDisplayed.Equals(jobIdExpected);
            //OkButtonpopupDialog.ClickUsingJavascript()
        }

        public void ClickOnJobAssign()
        {
            waitHelpers wh = new waitHelpers();
            SeleniumExtensions.WaitForReadyStateToComplete();
            String locator = $"//button[@data-id='confirmButton']";
            By eleBy = SeleniumExtensions.getLocator(locator);
            if (wh.GetWebElement(eleBy, 20))
            {
                wh.clickOnWebElement(eleBy);
                //DriverHelper.Driver.FindElement(eleBy).Click();
            }

            AssigntouserButton.ClickUsingJavascript();

        }
        public void ClickSubmitJobSubMenu()
        {
            SubmitJobSubMenu.ClickUsingJavascript();
        }

        public void ClickNoActionRequestJobSubMenu()
        {
            NoActionJobSubMenu.ClickUsingJavascript();
        }


        public void EnterNoActionName(string noActionName)
        {
            NoActionName.SendKeys(noActionName);
        }

        public void ClickOkButtonOnDialog()
        {
            OkButtonOnDialog.Click();
        }

        public void ClickSaveAndCloseRequestValidateDialog()
        {
            SaveAndCloseOnRequestValidationDialog.IsElementDisplayed(2);
            SaveAndCloseOnRequestValidationDialog.Click();
            RequestSavedLabel.IsElementDisplayed(10);
            SeleniumExtensions.RefreshPage();
        }
        public void SetReasonForValidation(string reason)
        {
            ScrollDiv.ScrollUntilSelectorDisplayedAndSendKeys(ReasonForValidationTextAreaSelector,
               reason, 10);
        }

        public void ClickRelatedJobsSubTab()
        {
            System.Threading.Thread.Sleep(15000);
            RelatedJobsSubTab.ClickUsingJavascript();
        }

        public void ClickProceedButtonOnDialog()
        {
            ProceedButtonOnDialog.ClickUsingJavascript();
            Thread.Sleep(10000);
        }
        public void FillCodedReasonOnJobCreation(string codedReason)
        {
            JobTypeSearchIconRequestPageElement.ScrollAndClick();
            JobTypeTextBoxRequestPageElement.Click();
            JobTypeTextBoxRequestPageElement.SendKeys(Keys.Backspace);
            JobTypeTextBoxRequestPageElement.SendKeys(codedReason);
            JobTypeValueRequestPageElement.Click();
        }
        public void FillRequestTypeOnRequestCreation(string requestType)
        {
            FillAndSelectLookUpResult(RequestTypeLookupField,
                requestType);
        }

        public void FillJobSubTypeOnRequestCreation(string requestType)
        {
            FillAndSelectLookUpResult(JobSubTypeLookupField,
                requestType);
        }

        public void FillAddressLabel(string address)
        {
            FillAndSelectLookUpResult(AddressLabelLookUpTextfieldSelector,
                address);
        }
        public void EnterRemarksOnRequest(string address)
        {
            JobTypeTextBoxRequestPageElement.SendKeys(address);
        }

        public void FillJobTypeLookUpField(string jobType)
        {
            FillAndSelectLookUpResult(JobTypeLookUpTextFieldSlector,
                jobType);
        }


        public void FillProposedBillingAuthority(string proposedBillingAuthority)
        {
            FillAndSelectLookUpResult(ProposedBillingTypeLookUpTextFieldSlector, proposedBillingAuthority);
        }

        public void FillProposedBAReferenceNumber(string number)
        {
            ScrollDivRequestPage.ScrollUntilSelectorDisplayedAndSendKeys(ProposedBAReferenceNumberTextFieldSelector, number, 30);
        }
        public string GetRequestId()
        {
            ClickSaveBtnOnMainNav();
            RequestIDTextBox.ScrollAndClick();
            System.Threading.Thread.Sleep(4000);
            Driver.WaitUntilElementHasValue(RequestIDTextBoxSelector, "---", false, 30000);
            RequestId = RequestIDTextBox.GetAttribute("title");
            return RequestId;
        }

        public bool IsRequestSaved()
        {
            return RequestSavedLabel.IsElementDisplayed(10);
        }

        public void ClickRelatedJobsTab()
        {
            RelatedJobsSubTab.ClickUsingJavascript();
        }

        public bool VerifyJobCreatedDisplayedOnRelatedJobsTab()
        {
            int turns = 0;
            if (!JobCreatedRowOnRelatedJobs.IsElementDisplayed(5))
            {
                do
                {
                    SeleniumExtensions.RefreshPage();
                    ClickRelatedJobsTab();
                    turns += 1;
                    if (JobCreatedRowOnRelatedJobs.IsElementDisplayed(1))
                        return true;
                } while (turns <= 3);
            }

            return JobCreatedRowOnRelatedJobs.IsElementDisplayed(10);
        }

        //public void ValidateJobCreatedDisplayedOnRelatedJobsTab(Dictionary<string, string> testdata)
        //{
        //    IList<IWebElement> RowsElement = Driver.FindElements(MaterialRequestRows);
        //    if (RowsElement.Count!=3)
        //    {
        //       SeleniumExtensions.RefreshPage();
        //       ClickRelatedJobsTab();
        //       RowsElement = Driver.FindElements(MaterialRequestRows);
        //    }

        //    foreach (var row in RowsElement)
        //    {
        //        int rowno = RowsElement.Count;
        //        string rowindex = row.GetAttribute("row-index");
        //        IWebElement JobId = DriverHelper.Driver.WaitForElement(By.XPath($"//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row') and @row-index='{rowindex}']//*[contains(@col-id,'ticketnumber')]//label"));
        //        if (rowindex.Equals("0"))
        //        {
        //            testdata["JobID1"]=JobId.GetAttribute("aria-label");

        //        }
        //        if (rowindex.Equals("1"))
        //        {
        //            testdata["JobID2"] = JobId.GetAttribute("aria-label");

        //        }
        //        if (rowindex.Equals("2"))
        //        {
        //            testdata["JobID3"] = JobId.GetAttribute("aria-label");

        //        }
        //    }

        //    IWebElement OpenJobId = DriverHelper.Driver.WaitForElement(By.XPath($"//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row') and @row-index='0']//*[@aria-colindex='4']//a[contains(@aria-label,'Reconstitution Delete')]"));
        //    OpenJobId.DoubleClickElementUsingJSExecutor();
        //}

        public void ValidateStatusReasonforMaterialIncreaseTab(string statusText)
        {
            int findRowsTries = 0;
            bool validateFlag = false;
            bool findrowsFlag = false;
            waitHelpers wh = new waitHelpers();
            
            IList<IWebElement> RowsElement = wh.getAllWebElements(MaterialRequestRows);
            if (RowsElement.Count == 0)
            {
                while (RowsElement.Count == 0 && findRowsTries < 3)
                {
                    ClickCommandBarOption("Refresh");
                    ValidateAndClickOnTab("Related CR10s After Effective Date", "Desktop Research Form");
                    RowsElement = wh.getAllWebElements(MaterialRequestRows);
                    if (RowsElement.Count > 0)
                    {
                        findrowsFlag = true;
                        break;
                    }
                    findRowsTries++;
                }
                if (findrowsFlag == false)
                {
                    Assert.Fail("No Related CR10's detected");
                    // Log.Error("");
                }


            }

            foreach (var row in RowsElement)
            {
                string rowindex = row.GetAttribute("row-index");
                IWebElement StatusReason = DriverHelper.Driver.WaitForElement(By.XPath($"//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row') and @row-index='{rowindex}']//*[contains(@col-id,'statuscode')]//label"));

                string actStatusReasonText = StatusReason.GetAttribute("aria-label");
                if (actStatusReasonText != statusText)
                {
                    while (actStatusReasonText != statusText)
                    {
                        actStatusReasonText = StatusReason.GetAttribute("aria-label");
                        if (actStatusReasonText == statusText)
                        {
                            validateFlag = true;
                            break;
                        }

                    }
                }
                else
                {
                    validateFlag = true;

                }
                if (validateFlag == true)
                {
                    Assert.AreEqual(statusText, actStatusReasonText, $"The  status has been validated as {actStatusReasonText} within the max tries");
                    // Log.Information($"");
                }
                else
                {
                    Assert.Fail("The Related CR10 validation is failed with status as " + actStatusReasonText);
                    //Log.Error("CR10 requests are not available" + actRuntimeStatusText);


                }
            }
        }
        public string ClickOnJobCreatedAndSaveJobId()
        {
            var jobId = JobCreatedRowOnRelatedJobs.GetAttribute("aria-label").Trim();
            Console.WriteLine(jobId);
            //JobCreatedRowOnRelatedJobs.DoubleClickElementUsingJSExecutor();
            //if (NoButton.ElementVisisbleUsingExplicitWait(2))
            //    NoButton.ClickUntilNoClickInterruptable();
            //    VerifyDuplicateRecords();
            //if (OkButtonOnDialog.ElementVisisbleUsingExplicitWait(2))
            //    OkButtonOnDialog.ClickUntilNoClickInterruptable();

            //if (NoButton.IsElementDisplayed(1))
            //    NoButton.ClickUntilNoClickInterruptable();
            //VerifyDuplicateRecords();
            //if (OkButtonOnDialog.IsElementDisplayed(1))
            //    OkButtonOnDialog.ClickUntilNoClickInterruptable();
            return jobId;
        }


        public void EnterRemarks(string text)
        {
            RemarksInRequest.ClearAndEnterValueIntoTextBox(text);
        }

        public void SelectUniqueAddressLabel(string addressLabelText)
        {
            AddressLabelSearch.ClickUsingJavascript();
            AddressLabelSearch.Clear();
            AddressLabelSearch.SendKeys(addressLabelText);
            var AddressListItemsCount = AddressLabelList.Count;

            for (int i = 0; i <= AddressListItemsCount - 1; i++)
            {
                AddressLabelSearch.ClickUsingJavascript();
                while (!AddressLabelSearch.GetAttribute("value").Equals(""))
                {
                    AddressLabelSearch.SendKeys(Keys.Backspace);
                }
                AddressLabelSearch.SendKeys(addressLabelText);
                if (SeleniumExtensions.WaitUntilElementWithSelectorIsDisplayed(Driver, AddressLookUpSuggestionsList_Selector, 40) != null)
                    AddressLabelList[i].ClickUsingJavascript();

                if (SeleniumExtensions.WaitUntilElementWithSelectorIsDisplayed(Driver, AddresslabelNotificationDialog_Selector, 5) != null)
                {
                    OkBtnOnAddresslabelNotificationDialog.ClickUsingJavascript();
                }
                else
                {
                    Console.WriteLine("not a duplicated address");
                    return;
                }
            }
        }

        public bool VerifySaveBtnIsDisplayed()
        {
            return SavedTextOnRequestPage.Displayed;
        }

        //************************Ashish's code************
        public void EnterDataInCR03NewPropertySection(string estateFileName)
        {
            FillAndSelectLookUpResult(EstateFileLookup, estateFileName);
            FindPlotButton.ClickUsingJavascript();
        }

        public void FindPlotFromDialog()
        {
            IList<IWebElement> plotsAvailable = Driver.FindElements(PlotsList);


            foreach (var row in plotsAvailable)
            {

                IWebElement PlotRowCheckEle = row.FindElement(PlotRowCheck);


                PlotRowCheckEle.ClickUsingJavascript();

                break;

            }
            SelectButton.IsElementDisplayed(3);
            SelectButton.ClickUsingJavascript();
            Thread.Sleep(3000);

        }

        public void ClickOnOptionInRelatedTab(string optionToSelect)
        {
            ValidateAndClickOnTab("Related", "Hereditament Form");
            IWebElement optionToSel = OptionsFromRelatedTab(optionToSelect);
            optionToSel.ClickUsingJavascript();
            Thread.Sleep(20000);
        }

        public void ValidateAutoProcessingRuntimeStatus(string statusText)
        {
            string actualStatus = "";
            while (RunTimeStatusLabel.IsElementDisplayed(3) == false)
            {
                AutoProcRefreshBtn.ClickUsingJavascript();
                if (RunTimeStatusLabel.IsElementDisplayed(3) == true)
                {
                    actualStatus = RunTimeStatusLabel.GetAttribute("aria-label");
                }
            }
            actualStatus = RunTimeStatusLabel.GetAttribute("aria-label");
            while (actualStatus != statusText)
            {
                AutoProcRefreshBtn.ClickUsingJavascript();
                actualStatus = RunTimeStatusLabel.GetAttribute("aria-label");
                if (actualStatus == statusText)
                {
                    break;
                }
                else if (actualStatus == "Failed")
                {
                    Assert.Fail("The Auto Processing Runtime status is " + actualStatus);
                }
            }

        }

        public void ValidateAutoProcessingRuntimeStatusForBP(string statusText)
        {
            int retries = 0;
            int findRowsTries = 0;
            bool validateFlag = false;
            bool findrowsFlag = false;
            waitHelpers wh = new waitHelpers();
            IList<IWebElement> RowsElement = Driver.FindElements(TabRowsElement);
            if (RowsElement.Count == 0)
            {
                while (RowsElement.Count == 0 && findRowsTries < 10)
                {
                    AutoProcRefreshBtn.ClickUsingJavascript();
                    RowsElement = Driver.FindElements(TabRowsElement);
                    if (RowsElement.Count > 0)
                    {
                        findrowsFlag = true;
                        break;
                    }
                    findRowsTries++;
                }
                if (findrowsFlag == false)
                {
                    Assert.Fail("No Auto Processing entries were detected , check for ALerts on the Job");
                    Log.Error("No Auto Processing entries were detectedm , check for ALerts on the Job");
                }


            }

            foreach (var row in RowsElement)
            {

                string rowindex = row.GetAttribute("row-index");
                IWebElement RuntimeStatus = DriverHelper.Driver.WaitForElement(By.XPath($"//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row') and @row-index='{rowindex}']//*[@col-id='voa_runtimestatus']//label"));
                //   IWebElement runtimeStatus = DriverHelper.Driver.WaitForElement(By.XPath($"//*[@data-automationid='DetailsList']//*[@data-item-index='{rowindex}']//*[contains(@class,'cellCheck')]//following-sibling::*//*[@data-automation-key='addressstatus_column']"));

                string actRuntimeStatusText = RuntimeStatus.GetAttribute("aria-label");
                if (actRuntimeStatusText != statusText)
                {
                    while (actRuntimeStatusText != statusText && retries < 10)
                    {
                        AutoProcRefreshBtn.ClickUsingJavascript();
                        //RuntimeStatus = DriverHelper.Driver.WaitForElement(By.XPath($"//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row') and @row-index='{rowindex}']//*[@col-id='voa_runtimestatus']//label"));
                        IWebElement UpdateRunTimeStatus = wh.WaitForElementToBeReady(By.XPath($"//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row') and @row-index='{rowindex}']//*[@col-id='voa_runtimestatus']//label"), 10);
                        actRuntimeStatusText = UpdateRunTimeStatus.GetAttribute("aria-label");
                        if (actRuntimeStatusText == statusText)
                        {
                            validateFlag = true;
                            break;
                        }
                        else if (actRuntimeStatusText == "Failed")
                        {
                            validateFlag = false;
                            break;
                        }
                        else
                        {
                            continue;
                        }
                        retries++;
                    }
                }
                else
                {
                    validateFlag = true;

                }
                if (validateFlag == true)
                {
                    Assert.AreEqual(statusText, actRuntimeStatusText, $"The runtime status has been validated as {actRuntimeStatusText} within the max tries");
                    Log.Information($"The runtime status has been validated as {actRuntimeStatusText} within the max tries");
                }
                else
                {
                    Assert.Fail("The Auto Processing validation is failed with Runtime status as " + actRuntimeStatusText);
                    Log.Error("The Auto Processing validation is failed with Runtime status as " + actRuntimeStatusText);


                }
            }
        }

        public void ValidateJobStatusCode(string statusText)
        {
            string actualStatus = StatusCode.GetAttribute("aria-label");
            while (actualStatus != statusText)
            {
                ChildJobsRefreshBtn.ClickUsingJavascript();
                actualStatus = StatusCode.GetAttribute("aria-label");
                if (actualStatus == statusText)
                {
                    break;
                }
            }

        }

        public string ClickOnEtateJObID()
        {
            var jobId = JobCreatedRowOnRelatedJobs.GetAttribute("aria-label").Trim();
            SeleniumExtensions.DoubleClickElementUsingJSExecutor(JobCreatedRowOnRelatedJobs);
            bool jobDisplayed = SeleniumExtensions.IsElementDisplayed(JobCreatedRowOnRelatedJobs, 5);
            while (jobDisplayed == true)
            {
                SeleniumExtensions.DoubleClickElementUsingJSExecutor(JobCreatedRowOnRelatedJobs);
                if (SeleniumExtensions.IsElementDisplayed(JobCreatedRowOnRelatedJobs, 5) == false)
                    break;
            }
            Thread.Sleep(5000);
            return jobId;
        }

        public void EnterDetailsInCategorisationSection(string requestType, string jobType, string jobSubType)
        {
            By RTFirstValue = GetFirstLookUp(requestType);
            SeleniumExtensions.EnterInLookUpField(RTFirstValue, RequestTypeLookupField, RequestTypeSearchBtn, requestType);
            By JTFirstValue = GetFirstLookUp(jobType);
            SeleniumExtensions.EnterInLookUpField(JTFirstValue, JobTypeLookUpTextFieldSlector, JobTypeSearchBtn, jobType);
            //  By JSTFirstValue = GetFirstLookUp(jobSubType);
            //  SeleniumExtensions.EnterInLookUpField(JSTFirstValue, JobSubTypeLookupField, JobSubTypeSearchBtn, jobSubType);

        }

        public void EnterSubJobTypeAtDetails(Dictionary<string, string> testdata)
        {
            By JSTFirstValue = GetFirstLookUp(testdata["SubJobType"]);
            SeleniumExtensions.EnterInLookUpField(JSTFirstValue, JobSubTypeLookupField, JobSubTypeSearchBtn, testdata["SubJobType"]);
        }

        public void EnterDetailsInCategorisationSection(string requestType, string jobType)
        {
            //  By RTFirstValue = GetFirstLookUp(requestType);
            //  SeleniumExtensions.EnterInLookUpField(RTFirstValue, RequestTypeLookupField, RequestTypeSearchBtn, requestType);
            By JTFirstValue = GetFirstLookUp(jobType);
            SeleniumExtensions.EnterInLookUpField(JTFirstValue, JobTypeLookUpTextFieldSlector, JobTypeSearchBtn, jobType);

        }
        public void EnterDetailsInCategorisationSectionForDemolition(string requestType, string jobType, string isCaravan)
        {
            //  By RTFirstValue = GetFirstLookUp(requestType);
            // SeleniumExtensions.EnterInLookUpField(RTFirstValue, RequestTypeLookupField, RequestTypeSearchBtn, requestType);
            By JTFirstValue = GetFirstLookUp(jobType);
            SeleniumExtensions.EnterInLookUpField(JTFirstValue, JobTypeLookUpTextFieldSlector, JobTypeSearchBtn, jobType);
            IsCaravanBoat.WaitUntilElementAttached(5);
            IsCaravanBoat.ElementVisisbleUsingExplicitWait(5);
            SeleniumExtensions.SelectDropdownValue(IsCaravanBoat, isCaravan);
            //IsCaravanBoat.SelectElementByValue(isCaravan);

        }

        public void EnterDetailsInCategorisationSectionForRecon(string jobType, string ReconType)
        {
            By JTFirstValue = GetFirstLookUp(jobType);
            SeleniumExtensions.EnterInLookUpField(JTFirstValue, JobTypeLookUpTextFieldSlector, JobTypeSearchBtn, jobType);
            By RTFirstValue = GetFirstLookUp(ReconType);
            SeleniumExtensions.EnterInLookUpField(RTFirstValue, ReconTypeLookupField, ReconTypeSearchBtn, ReconType);

        }
        public void EnterFieldsInDetailsSection(float days, string reasonValue, string channelName, string DSR)
        {
            PickProposedDate(days);
            // Channel.SelectElementByText(channelName);
            SeleniumExtensions.SelectDropdownValue(Channel, channelName);
            FillAndSelectLookUpResult(DSRLookUpField, DSR.Trim());
            ReasonForValidation.ClearAndEnterValueIntoTextBox(reasonValue);

        }

        public void EnterFieldsInDetailsSectionForEFfectiveDate(string dateCaptured, string reasonValue, string channelName, string DSR, string dateCapturedReceived)
        {
            PickReceivedDateForBP(dateCapturedReceived);
            PickProposedDateForBP(dateCaptured);
            //  Channel.SelectElementByText(channelName);
            SeleniumExtensions.SelectDropdownValue(Channel, channelName);

            FillAndSelectLookUpResult(DSRLookUpField, DSR.Trim());
            ReasonForValidation.ClearAndEnterValueIntoTextBox(reasonValue);
        }

        public void EnterFieldsInDetailsSectionForEFfectiveDate(string dateCaptured, string reasonValue, string channelName, string DSR)
        {
            PickProposedDateForBP(dateCaptured);
            //  Channel.SelectElementByText(channelName);
            SeleniumExtensions.SelectDropdownValue(Channel, channelName);

            FillAndSelectLookUpResult(DSRLookUpField, DSR.Trim());
            ReasonForValidation.ClearAndEnterValueIntoTextBox(reasonValue);


        }
        public void EnterFieldsInDetailsSectionForInformalChallenge(string dateCaptured, string reasonValue, string DSR, string dateCapturedReceived, string evidenceDate, string authorizationDecision, string BARefNum)
        {
            Thread.Sleep(3000);
            SeleniumExtensions.scrollToBtnElement("Authorisation to Act Decision");
            SeleniumExtensions.SelectDropdownValue(AuthorizationDecision, authorizationDecision);
            PickReceivedDateForBP(dateCapturedReceived);
            PickProposedDateForBP(dateCaptured);
            FillAndSelectLookUpResult(DSRLookUpField, DSR.Trim());
            ReasonForValidation.ClearAndEnterValueIntoTextBox(reasonValue);
            EvidenceSufficientDateForBP(evidenceDate);
            //SuppliedBARefNumEle.ClearAndSendkeys(BARefNum);
        }

        public void EnterFieldsInDetailsSectionForCDP(string portalReferenceOmmision, string BARefNum)
        {
            ReasonForPortalReference.ClearAndEnterValueIntoTextBox(portalReferenceOmmision);
            SuppliedBARef.ClearAndSendkeys(BARefNum);

        }

        public void EnterFieldsInDetailsSectionForReEntry()
        {

            SeleniumExtensions.scrollToBtnElement("Is VT Withdrawal Confirmation Before Settlement?");
            SeleniumExtensions.scrollToBtnElement("Property Deleted By Mistake?");
            SeleniumExtensions.SelectDropdownValue(PropertyMistake, "Yes");

        }

        public void EnterFieldsCTPlayerSection(string ctPlayerName)
        {
            FillAndSelectLookUpResult(CTPlayerLookUp, ctPlayerName.Trim());

        }

        public void EnterFieldsInDetailsSectionforCR10(string dateCaptured, string reasonValue, string channelName, string DSR)
        {
            PickProposedDateForBP(dateCaptured);
            // Channel.SelectElementByText(channelName);
            SeleniumExtensions.SelectDropdownValue(Channel, channelName);

            FillAndSelectLookUpResult(DSRLookUpField, DSR.Trim());
            ReasonForValidation.ClearAndEnterValueIntoTextBox(reasonValue);
            ReasonForPortalReferenceOmmission.ClearAndEnterValueIntoTextBox(reasonValue);
            //SetOnHoldRequestStatus.SelectElementByText("Yes");
        }

        public void EnterFieldsInDetailsSectionForCR03(string ProposedDate, string reasonValue, string channelName, string DSR)
        {
            //  PickProposedDate(days);
            PickProposedDateForBP(ProposedDate);
            SeleniumExtensions.SelectDropdownValue(Channel, channelName);
            // Channel.SelectElementByText(channelName);
            FillAndSelectLookUpResult(DSRLookUpField, DSR.Trim());
            //ReasonForPortalOmission.ClearAndEnterValueIntoTextBox("TestReason");
            ReasonForValidation.ClearAndEnterValueIntoTextBox(reasonValue);

        }

        public void EnterFieldsInDetailsSectionForCR10(float days, string reasonValue, string channelName, string DSR)
        {
            PickProposedDate(days);
            Channel.SelectElementByText(channelName);
            FillAndSelectLookUpResult(DSRLookUpField, DSR.Trim());
            ReasonForPortalOmission.ClearAndEnterValueIntoTextBox("TestReason");
            ReasonForValidation.ClearAndEnterValueIntoTextBox(reasonValue);

        }

        public void EnterFieldsInDetailsSection_DE(float days, string reasonValue, string DSR)
        {
            PickTargetDate("Target Date", days);
            FillAndSelectLookUpResult(DSRLookUpField, DSR.Trim());
            remarks.ClearAndEnterValueIntoTextBox(reasonValue);

        }

        public void PickTargetDate(String fieldName, float days)
        {
            var currentDate_02 = DateTime.Now.AddDays(days).ToString("d, MMMM, yyyy");
            ScrollDiv.ScrollAndClick(TargetDatePickerSelector, 20);
            SeleniumExtensions.SetTheDateForTheTableCalendar(fieldName, currentDate_02, 1);
        }

        public void PickProposedDate(float days)
        {
            var currentDate_02 = DateTime.Now.AddDays(days).ToString("d, MMMM, yyyy");
            ScrollDiv.ScrollAndClick(ProposedDatePickerSelector, 20);
            SeleniumExtensions.SetTheDateForTheTableCalendar(currentDate_02, 1);
        }

        public void PickReceivedDate(float days)
        {
            var currentDate_02 = DateTime.Now.AddDays(days).ToString("d, MMMM, yyyy");
            ScrollDiv.ScrollAndClick(ReceivedDatePickerSelector, 20);
            SeleniumExtensions.SetTheDateForTheTableCalendar(currentDate_02, 1);
        }

        public void PickProposedDateForBP(string dateCaptured)
        {
            DateTime dateToEnter = DateTime.Parse(dateCaptured);
            //M/d/yyyy
            var currentDate_02 = dateToEnter.ToString("d, MMMM, yyyy");
            string[] dateComponent = currentDate_02.Split(',');
            string DayVal = dateComponent[0].Trim();
            string MonthVal = dateComponent[1].Trim();
            string YearVal = dateComponent[2].Trim();
            ScrollDiv.ScrollAndClick(ProposedDatePickerSelector, 20);
            SelectDateFromDatePicker(DayVal, MonthVal, YearVal);
            //SeleniumExtensions.SetTheDateForTheTableCalendar(currentDate_02, 1);

        }

        public void PickProposedDateForDialogBP(string dateCaptured)
        {
            DateTime dateToEnter = DateTime.Parse(dateCaptured);
            //M/d/yyyy
            var currentDate_02 = dateToEnter.ToString("d, MMMM, yyyy");
            string[] dateComponent = currentDate_02.Split(',');
            string DayVal = dateComponent[0].Trim();
            string MonthVal = dateComponent[1].Trim();
            string YearVal = dateComponent[2].Trim();
            ScrollDiv.ScrollAndClick(ProposedDatePickerDialogSelector, 20);
            SelectDateFromDatePicker(DayVal, MonthVal, YearVal);
            //SeleniumExtensions.SetTheDateForTheTableCalendar(currentDate_02, 1);

        }
        public void PickReceivedDateForBP(string dateCaptured)
        {
            DateTime dateToEnter = DateTime.Parse(dateCaptured);
            //M/d/yyyy
            var currentDate_02 = dateToEnter.ToString("d, MMMM, yyyy");
            string[] dateComponent = currentDate_02.Split(',');
            string DayVal = dateComponent[0].Trim();
            string MonthVal = dateComponent[1].Trim();
            string YearVal = dateComponent[2].Trim();
            ScrollDiv.ScrollAndClick(ReceivedDatePickerSelector, 20);
            SelectDateFromDatePicker(DayVal, MonthVal, YearVal);
            //SeleniumExtensions.SetTheDateForTheTableCalendar(currentDate_02, 1);

        }

        public void EvidenceSufficientDateForBP(string EvidencedateCaptured)
        {
            DateTime dateToEnter = DateTime.Parse(EvidencedateCaptured);
            //M/d/yyyy
            var currentDate_02 = dateToEnter.ToString("d, MMMM, yyyy");
            string[] dateComponent = currentDate_02.Split(',');
            string DayVal = dateComponent[0].Trim();
            string MonthVal = dateComponent[1].Trim();
            string YearVal = dateComponent[2].Trim();
            ScrollDiv.ScrollAndClick(EvidenceDatePickerSelector, 20);
            SelectDateFromDatePicker(DayVal, MonthVal, YearVal);
            //SeleniumExtensions.SetTheDateForTheTableCalendar(currentDate_02, 1);

        }
        public void PickProposedDateForBPUpd(string dateCaptured, float days)
        {
            string[] dateValue = dateCaptured.Split(' ');
            dateCaptured = dateValue[0];
            DateTime dateToEnter = DateTime.Parse(dateCaptured);
            //M/d/yyyy
            var currentDate_02 = dateToEnter.AddDays(days).ToString("d, MMMM, yyyy");
            string[] dateComponent = currentDate_02.Split(',');
            string DayVal = dateComponent[0].Trim();
            string MonthVal = dateComponent[1].Trim();
            string YearVal = dateComponent[2].Trim();
            ScrollDiv.ScrollAndClick(ProposedDatePickerSelector, 20);
            SelectDateFromDatePicker(DayVal, MonthVal, YearVal);
            //SeleniumExtensions.SetTheDateForTheTableCalendar(currentDate_02, 1);

        }
        public void ProposedEffectiveDateForIC(float days)
        {
            var currentDate_02 = DateTime.Now.AddDays(days).ToString("d, MMMM, yyyy");
            ScrollDiv.ScrollAndClick(ProposedEffectiveDatePickerSelector, 20);
            SeleniumExtensions.SetTheDateForTheTableCalendar_New(currentDate_02, 1);

            //DateTime dateToEnter = DateTime.Parse(dateCaptured);
            ////M/d/yyyy
            //var currentDate_02 = dateToEnter.ToString("d, MMMM, yyyy");
            //string[] dateComponent = currentDate_02.Split(',');
            //string DayVal = dateComponent[0].Trim();
            //string MonthVal = dateComponent[1].Trim();
            //string YearVal = dateComponent[2].Trim();
            //ScrollDiv.ScrollAndClick(ProposedEffectiveDatePickerSelector, 20);
            //SelectDateFromDatePickernew(DayVal, MonthVal, YearVal);
            ////SeleniumExtensions.SetTheDateForTheTableCalendar(currentDate_02, 1);
        }

        public void EnterFieldsInNewHereditamentDetailsSection(string billingAuthority, string postCode)
        {
            //  FillAndSelectLookUpResult(ProposedBillingTypeLookUpTextFieldSlector, billingAuthority);
            FindAddressForNewProperty(postCode);
        }

        public void EnterFieldsInNoActionPopUp(string noActionName, string noActionSubCode, string noActionCode, string internalRemarks)
        {
            // FillAndSelectLookUpResult(NoActionSubCodeLookUp, noActionSubCode);
            FillAndSelectLookUpResult(NoActionCodeInput, noActionCode);
            InternalRemarks.ClearAndSendkeys(internalRemarks);
            SaveAndCloseNoAction.ClickUsingJavascript();

        }

        public void EnterFieldsInJobActionsNoActionPopUp(string noActionName, string noActionSubCode, string noActionCode, string internalRemarks)
        {
            // FillAndSelectLookUpResult(NoActionSubCodeLookUp, noActionSubCode);
            FillAndSelectLookUpResult(JobActionsNoActionCodeInput, noActionCode);
            JobActionsInternalRemarks.ClearAndSendkeys(internalRemarks);
            JobActionsSaveandClose.ClickUsingJavascript();

        }

        public void ValidateNoActionStatus(string expNoActionStatus)
        {
            ValidateStatus(expNoActionStatus);
        }


        public void ValidateNoAction(string fieldName, String fieldValue)
        {
            SeleniumExtensions.NoActionSelectElementByText(fieldName, fieldValue);
            NoActioncontinueBtn.ClickUsingJavascript();
            Thread.Sleep(3000);
            NoActioncontinueBtn.ClickUsingJavascript();
            NoActionFinishBtn.ClickUsingJavascript();
            ClickCommandBarOption("Save");
        }
        public void ValidateNoAction_new(string fieldName, String fieldValue)
        {
            SeleniumExtensions.NoActionSelectElementByText(fieldName, fieldValue);
            NoActioncontinueBtn.ClickUsingJavascript();
            Thread.Sleep(3000);
            NoActionFinishBtn.ClickUsingJavascript();
        }

        private By getLocator(string v)
        {
            throw new NotImplementedException();
        }


        public void SelectRequestActionDropdownOption(string optionToSelect)
        {
            RequestActionMenu.ClickUsingJavascript();
            //    IWebElement RequestActionOptionSelect = RequestActionOption(optionToSelect);
            RequestActionValidateRequest.IsElementDisplayed(3);
            RequestActionValidateRequest.ClickUsingJavascript();
            Thread.Sleep(3000);

        }

        public void ClickSaveAndCloseOnDialogforValidateRequest()
        {
            DialogSaveAndClose.IsElementDisplayed(5);
            DialogSaveAndClose.ClickUsingJavascript();
            Thread.Sleep(10000);
        }
        public void ClickSaveDialogforValidateRequest()
        {
            DialogSave.IsElementDisplayed(5);
            DialogSave.ClickUsingJavascript();
            //Thread.Sleep(10000);
        }

        public void ClickCompleteAcceptanceDialog()
        {

            DialogCompleteAcceptanceCheck.IsElementDisplayed(5);
            DialogCompleteAcceptanceCheck.ClickUsingJavascript();
            Thread.Sleep(5000);
        }
        public void ValidationRemarksClickSaveAndCloseOnDialogforValidateRequest()
        {
            PopupButton.Click();
            Thread.Sleep(2000);
            //SeleniumExtensions.ScrollToElement(ValidationRemarks);
            ValidationRemarks.IsElementDisplayed(5);
            ValidationRemarks.SendKeys("Text");
            DialogSaveAndClose.IsElementDisplayed(5);
            DialogSaveAndClose.ClickUsingJavascript();
            Thread.Sleep(15000);
        }

        public void EnterFieldsInBAReferenceSection(string BARefNum)
        {
            ProposedBARefNumEle.ClearAndSendkeys(BARefNum);
            Thread.Sleep(1000);
            BALinkSection.ClickUsingJavascript();
            //  ScrollDiv.ScrollUntilSelectorDisplayedAndSendKeys(ProposedBARefNum, BARefNum, 4);

        }

        public void EnterBARef(string BARefNum)
        {
            SuppliedBARef.ClearAndSendkeys(BARefNum);
            Thread.Sleep(1000);
            BALinkSection.ClickUsingJavascript();
            //  ScrollDiv.ScrollUntilSelectorDisplayedAndSendKeys(ProposedBARefNum, BARefNum, 4);

        }

        public void FindHereditamentForProcess(string postCode)
        {
            FindHereditament.ClickUsingJavascript();
            PostCodeOnDialog.ClearAndSendkeys(postCode);
            SearchOnDialog.ClickUsingJavascript();

            IList<IWebElement> HereditamentRows = Driver.FindElements(AddressRow);


            foreach (var row in HereditamentRows)
            {
                //  SeleniumExtensions.HighlightElement(row);
                row.ElementVisisbleUsingExplicitWait(10);
                string rowindex = row.GetAttribute("data-item-index");
                IWebElement addressStatusEle = DriverHelper.Driver.WaitForElement(By.XPath($"//*[@data-automationid='DetailsList']//*[@data-item-index='{rowindex}']//*[contains(@class,'cellCheck')]//following-sibling::*//*[@data-automation-key='addressstatus_column']"));
                SeleniumExtensions.HighlightElement(addressStatusEle);

                string actAddressStatus = addressStatusEle.Text;
                if (actAddressStatus == "Committed")
                {
                    row.ClickUsingJavascript();
                    SelectOnDIalog.ClickUsingJavascript();
                    break;
                }
                else
                {
                    continue;
                }

            }


        }

        public void FindHereditamentForJobCreationUpd(string town, string postCode, string expUPRN)
        {
            FindHereditament.ClickUsingJavascript();
            TownCityOnDialog.ClearAndSendkeys(town);
            PostCodeOnDialog.ClearAndSendkeys(postCode);

            SearchOnDialog.ClickUsingJavascript();

            IList<IWebElement> HereditamentRows = Driver.FindElements(AddressRow);


            foreach (var row in HereditamentRows)
            {
                string sortOrder = FindHereditamentAddressHeaders.GetAttribute("data-icon-name");
                if (sortOrder == "SortDown")
                {
                    FindHereditamentAddressHeaders.ClickUsingJavascript();
                }
                //  SeleniumExtensions.HighlightElement(row);
                row.ElementVisisbleUsingExplicitWait(10);

                string rowindex = row.GetAttribute("data-item-index");
                IWebElement UPRNEle = DriverHelper.Driver.WaitForElement(By.XPath($"//*[@data-automationid='DetailsList']//*[@data-item-index='{rowindex}']//*[contains(@class,'cellCheck')]//following-sibling::*//*[@data-automation-key='uprn_column']"));
                // SeleniumExtensions.HighlightElement(UPRNEle);

                string actUPRN = UPRNEle.Text;
                if (actUPRN == expUPRN)
                {
                    row.ClickUsingJavascript();
                    SelectOnDIalog.ClickUsingJavascript();
                    break;
                }
                else
                {
                    continue;
                }

            }


        }

        public void waitTillSavingDisaddpears(String labelName, String roleType)
        {
            String locator = $"//*[@role='{roleType}' and text()='{labelName}'] | //*[@role='{roleType}'] | //*[text()='{labelName}']";
            By eleBy = SeleniumExtensions.getLocator(locator);
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(180));
            wait.PollingInterval = TimeSpan.FromMilliseconds(200);
            wait.Until(SeleniumExtensions.InvisibilityOfElementLocatedBy(eleBy));
        }


        //public void FindHereditamentForJobCreation(string town, string postCode, string expUPRN)
        //{
        //    FindHereditament.ClickUsingJavascript();
        //    TownCityOnDialog.ClearAndSendkeys(town);
        //    PostCodeOnDialog.ClearAndSendkeys(postCode);

        //    SearchOnDialog.ClickUsingJavascript();

        //    while (true)
        //    {

        //        IList<IWebElement> HereditamentRows = Driver.FindElements(AddressRow);

        //        bool found = false;
        //        foreach (var row in HereditamentRows)
        //        {
        //            string sortOrder = FindHereditamentAddressHeaders.GetAttribute("data-icon-name");
        //            if (sortOrder == "SortDown")
        //            {
        //                FindHereditamentAddressHeaders.ClickUsingJavascript();
        //            }
        //            row.ElementVisisbleUsingExplicitWait(10);

        //            string rowindex = row.GetAttribute("data-item-index");
        //            IWebElement UPRNEle = DriverHelper.Driver.WaitForElement(By.XPath($"//*[@data-automationid='DetailsList']//*[@data-item-index='{rowindex}']//*[contains(@class,'cellCheck')]//following-sibling::*//*[@data-automation-key='uprn_column']"));

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
        //        if (found)
        //        {
        //            break;
        //        }

        //        if (NextPageOnDialog.IsEnabled())
        //        {
        //            NextPageOnDialog.ClickUsingJavascript();
        //        }
        //        else
        //        {
        //            Console.WriteLine("Value not found and reached end of pagination.");
        //            break;
        //        }




        //    }

        //}



        public void FindHereditamentForSwapHereditament(string town, string postCode, string expUPRN)
        {
            SeleniumExtensions.ScrollToElement(FindHereditamentForSwap);
            FindHereditamentForSwap.ClickUsingJavascript();
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
                    // row.ElementVisisbleUsingExplicitWait(10);
                    SeleniumExtensions.ScrollToElement(row);

                    string rowindex = row.GetAttribute("data-item-index");
                    UPRNEle = DriverHelper.Driver.WaitForElement(By.XPath($"//*[@data-automationid='DetailsList']//*[@data-item-index='{rowindex}']//*[contains(@class,'cellCheck')]//following-sibling::*//*[@data-automation-key='uprn_column']//*[contains(@class,'ms-TooltipHost')]"));
                    //  string actUPRN = UPRNEle.Text;
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

        }
        public string FindAddressForNewProperty_Recon(string postCode)
        {
            string addressUprn = null;
            bool found = false;
            waitHelpers wh = new waitHelpers();
            wh.WaitForElementToBeDisplayed(FindAddressUsingBy, 20);
            Thread.Sleep(3000);
            SeleniumExtensions.ScrollToElement(FindAddress);
            FindAddress.ClickUsingJavascript();
            wh.WaitForElementToBeDisplayed(AddressSearchInputOnDialogUsingBy, 20);
            // Assert.IsTrue(AddressSearchInputOnDialog.ElementVisisbleUsingExplicitWait(5), "The Address Search Dialog does not appear");
            AddressSearchInputOnDialog.ClearAndSendkeys(postCode);

            BtnSearchOnDialog.ClickUsingJavascript();

            while (!found)
            {

                //IList<IWebElement> AddressesRows = Driver.FindElements(AddressRow);
                IList<IWebElement> AddressesRows = wh.getAllWebElements(AddressRow);

                
                foreach (var row in AddressesRows)
                {
                    row.ElementVisisbleUsingExplicitWait(10);

                    row.ClickUsingJavascript();
                    string rowindex = row.GetAttribute("data-item-index");
                    addressUprn = Driver.FindElement(By.XPath($"//*[@data-automationid='DetailsList']//*[@data-automationid='DetailsRow' and @data-item-index='{rowindex}']//*[@data-automation-key='uprn']//*[text()]")).Text;
                    Console.WriteLine(addressUprn);

                    BtnUseAddressOnDialog.ClickUsingJavascript();
                    if (DuplicateAddressHeader.IsElementVisibleUsingByLocator(5))
                    {
                        DuplicateAddressCloseBtn.ClickUsingJavascript();
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
                    break;
                }
            }
            Console.WriteLine(addressUprn);

            return addressUprn;
        }

        public string FindAddressForNewProperty(string postCode)
        {
            string addressUprn = null;
            waitHelpers wh = new waitHelpers();
            wh.WaitForElementToBeDisplayed(FindAddressUsingBy, 20);
            Thread.Sleep(3000);
            SeleniumExtensions.ScrollToElement(FindAddress);
            FindAddress.ClickUsingJavascript();
            wh.WaitForElementToBeDisplayed(AddressSearchInputOnDialogUsingBy, 20);
            // Assert.IsTrue(AddressSearchInputOnDialog.ElementVisisbleUsingExplicitWait(5), "The Address Search Dialog does not appear");
            AddressSearchInputOnDialog.ClearAndSendkeys(postCode);

            BtnSearchOnDialog.ClickUsingJavascript();

            while (true)
            {

                //IList<IWebElement> AddressesRows = Driver.FindElements(AddressRow);
                IList<IWebElement> AddressesRows = wh.getAllWebElements(AddressRow);

                bool found = false;
                foreach (var row in AddressesRows)
                {
                    row.ElementVisisbleUsingExplicitWait(10);

                    row.ClickUsingJavascript();
                    string rowindex = row.GetAttribute("data-item-index");
                    addressUprn = Driver.FindElement(By.XPath($"//*[@data-automationid='DetailsList']//*[@data-automationid='DetailsRow' and @data-item-index='{rowindex}']//*[@data-automation-key='uprn']//*[text()]")).Text;
                    Console.WriteLine(addressUprn);

                    BtnUseAddressOnDialog.ClickUsingJavascript();
                    if (DuplicateAddressHeader.IsElementVisibleUsingByLocator(5))
                    {
                        DuplicateAddressCloseBtn.ClickUsingJavascript();
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
                    break;
                }
            }
            Console.WriteLine(addressUprn);

            return addressUprn;
        }

        public void FindAddressForChangeOfAddressforAuto(string postCode)
        {
            Thread.Sleep(3000);
            SeleniumExtensions.ScrollToElement(FindAddress_New);
            FindAddress_New.ClickUsingJavascript();
            BtnSearchOnDialog.ClickUsingJavascript();

            while (true)
            {
                IList<IWebElement> AddressesRows = Driver.FindElements(AddressRow);
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
            Thread.Sleep(2000);
        }

        public void FindAddressForChangeOfAddressatDesktopResearch()
        {
            Thread.Sleep(3000);
            FindAddress_New.ClickUsingJavascript();
            BtnSearchOnDialog.ClickUsingJavascript();

            while (true)
            {
                IList<IWebElement> AddressesRows = Driver.FindElements(AddressRow);
                int count = AddressesRows.Count;
                for (int i = 1; i <= count; i++)
                {
                    IWebElement AddressElement = DriverHelper.Driver.WaitForElement(By.XPath($"//*[@data-automationid='DetailsList']//*[@data-automationid='DetailsRow'][@data-selection-index='{i}'][@aria-selected='false']"));

                    AddressElement.ElementVisisbleUsingExplicitWait(10);

                    AddressElement.ClickUsingJavascript();
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
                if (statusReason == "Duplicate Validation Failed")
                {
                    Console.WriteLine("Duplicate Validation Failed");
                    break;
                }
            }
        }

        public void FindAddressForNewPropertyzWithDB(string postCode)
        {
            String query = $"select uprn from property.du_label where postcode='{postCode}' and (is_bs7666_compliant is not true or is_bs7666_compliant is not false)  and uprn is not null;";
            FindAddress.ClickUsingJavascript();
            Assert.IsTrue(AddressSearchInputOnDialog.ElementVisisbleUsingExplicitWait(5), "The Address Search Dialog does not appear");
            AddressSearchInputOnDialog.ClearAndSendkeys(postCode);
            BtnSearchOnDialog.ClickUsingJavascript();
            IList<IWebElement> AddressesRows = Driver.FindElements(uprN_Li);
            IList<String> UI_uprn_Li = new List<String>();
            foreach (var ele in AddressesRows)
            {
                UI_uprn_Li.Add(ele.Text);
            }
            IList<String> DB_uprn_Li = DBtests.GetDBdataList(query);
            waitHelpers wh = new waitHelpers();
            IEnumerable<String> unique_uprn = UI_uprn_Li.Except(DB_uprn_Li);
            String uniqueUprn = unique_uprn.First();
            By Unique_uprnRow = By.XPath($"//div[@data-automation-key='uprn']/div[text()='{uniqueUprn}']//parent::*/parent::*/parent::*[@data-automationid='DetailsRow']");
            wh.GetWebElement(Unique_uprnRow).ClickUsingJavascript();
            BtnUseAddressOnDialog.ClickUsingJavascript();
        }

        public void SelectPlotFromEstateFile(string exphouseTypeName)
        {

            IList<IWebElement> HereditamentRows = Driver.FindElements(AddressRow);


            foreach (var row in HereditamentRows)
            {
                //  SeleniumExtensions.HighlightElement(row);
                string rowindex = row.GetAttribute("data-item-index");
                IWebElement houseTypeNameEle = DriverHelper.Driver.WaitForElement(By.XPath($"//*[@data-automationid='DetailsList']//*[@data-item-index='{rowindex}']//*[contains(@class,'cellCheck')]//following-sibling::*//*[@data-automation-key='houseTypeColumn']"));
                // SeleniumExtensions.HighlightElement(addressStatusEle);

                string acthouseTypeName = houseTypeNameEle.Text;
                if (!string.IsNullOrEmpty(acthouseTypeName))
                {
                    if (acthouseTypeName == exphouseTypeName)
                    {
                        row.FindElement(By.CssSelector("[class*='ms-DetailsRow-cellCheck'] [data-automationid='DetailsRowCheck']")).ClickUsingJavascript();
                        //    row.ClickUsingJavascript();
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    continue;
                }
            }
            SelectOnDIalog.ClickUsingJavascript();

        }

        public void SelectAlternateJobType(string jobType)
        {
            By JTFirstValue = GetFirstLookUp(jobType);
            SeleniumExtensions.EnterInLookUpField(JTFirstValue, AlternateJobType, AlternateJobTypeSearchBtn, jobType);
        }

        public void ClickOnOkLeaveThisPageDialog()
        {
            Assert.IsTrue(LeavethispageDialog.ElementVisisbleUsingExplicitWait(5), "The dialog did not appear");
            LeavethispageDialogOKBtn.ClickUsingJavascript();
            Thread.Sleep(10000);
        }

        public void ValidateAlternateJobCreation(string jobIdExpected)
        {
            var jobIdDisplayed = JobIdFieldOnRequestPage.GetAttribute("Title").ToString().Trim();
            Assert.AreNotEqual(jobIdExpected, jobIdDisplayed);


        }

        public void ClickSaveTillRequestCreated()
        {
            waitHelpers wh = new waitHelpers();
            ClickCommandBarOption("Save");
            Assert.IsTrue(wh.WaitTillElementInvisibleInSeconds(SavingAlert, 400), "The saving progress didnt dissapear after 180 tries");
            // Assert.IsTrue(wh.waitTillElementInvisible(SavingAlert), "The saving progress didnt dissapear after 180 tries");
            // ValidateSaveInProgressDialog();
            //   Assert.IsTrue(SeleniumExtensions.WaitElementToDissapear(SavingAlert, 180), "The saving progress didnt dissapear after 180 tries");

            var headerTitle = RequestTitle.GetAttribute("title").Trim();

            while (headerTitle == "New Request")
            {
                ClickCommandBarOption("Save");
                //ValidateSaveInProgressDialog();
                Assert.IsTrue(wh.WaitTillElementInvisibleInSeconds(SavingAlert, 400), "The saving progress didnt dissapear after 180 tries");
                headerTitle = RequestTitle.GetAttribute("title").Trim();
                var requestStatus = RequestSaveStatus.Text.Trim();
                if (headerTitle != "New Request" && requestStatus == "- Saved")
                {

                    break;
                }
            }

        }

        public void CreateRequest(string requestType, string jobType, Dictionary<string, string> testdata)
        {
            Random rand = new Random();
            int randomBAReference = rand.Next(1000000, 9999999);
            string proposedEffectiveDateChanged;
            string dateReceived;

            proposedEffectiveDateChanged = DateTime.Now.AddDays(-2).ToString("M/d/yyyy");


            Console.WriteLine(proposedEffectiveDateChanged);
            dateReceived = DateTime.Now.AddDays(0).ToString("M/d/yyyy");


            switch (jobType)
            {
                case "New Property - Individual":
                    EnterDetailsInCategorisationSection(requestType, jobType);
                    SelectDateFromDatePickerElement(DateReceivedDatePicker, 0);
                    EnterFieldsInDetailsSectionForCR03(proposedEffectiveDateChanged, testdata["ReasonForValidation"], testdata["Channel"], testdata["DSR"]);
                    EnterFieldsInBAReferenceSection(randomBAReference.ToString());
                    FindAddressForNewProperty(testdata["AddressLabel"]);
                    break;
            }
            ClickSaveTillRequestCreated();
        }



        public void CreateRequest(string requestType, string jobType, Dictionary<string, string> testdata, string estateFileName)
        {
            Random rand = new Random();
            int randomBAReference = rand.Next(100000, 999999);
            string proposedEffectiveDateChanged;

            proposedEffectiveDateChanged = DateTime.Now.AddDays(-2).ToString("M/d/yyyy");
            Console.WriteLine(proposedEffectiveDateChanged);

            switch (jobType)
            {
                case "New Property - Estate":
                    EnterDetailsInCategorisationSection(requestType, jobType);
                    SelectDateFromDatePickerElement(DateReceivedDatePicker, 0);
                    EnterFieldsInDetailsSectionForCR03(proposedEffectiveDateChanged, testdata["ReasonForValidation"], testdata["Channel"], testdata["DSR"]);
                    //EnterFieldsInDetailsSection(-2, testdata["ReasonForValidation"], testdata["Channel"], testdata["DSR"]);
                    EnterFieldsInBAReferenceSection(randomBAReference.ToString());
                    //  EnterDataInCR03NewPropertySection("Adur Council - 07/05/2024 15:26");
                    EnterDataInCR03NewPropertySection(estateFileName);
                    SelectPlotFromEstateFile(testdata["HouseTypeName"]);
                    break;


                case "Estate File":
                    EnterDetailsInCategorisationSection(requestType, jobType);
                    EnterFieldsInDetailsSection(-2, testdata["ReasonForValidation"], testdata["Channel"], testdata["DSR"]);
                    break;


            }
            ClickSaveTillRequestCreated();
        }

        //public void CreateRequest(string requestType, string jobType, Dictionary<string, string> testdata, string town, string postCode, string expUPRN)
        //{
        //    Random rand = new Random();
        //    int randomBAReference = rand.Next(100000, 999999);
        //    DateTime datetoday = DateTime.Today;
        //    string proposedEffectiveDateChanged= datetoday.ToString();
        //    switch (jobType)
        //    {

        //        case "Change of Address":
        //            EnterDetailsInCategorisationSection(requestType, jobType);
        //            SelectDateFromDatePickerElement(DateReceivedDatePicker, 0);
        //            EnterFieldsInDetailsSectionForEFfectiveDate(proposedEffectiveDateChanged, testdata["ReasonForValidation"], testdata["Channel"], testdata["DSR"]);
        //            FindHereditamentForJobCreation(town, postCode, expUPRN);
        //            FindAddressForChangeOfAddress(postCode);
        //            ClickCommandBarOption("Save");
        //            waitTillSavingDisaddpears("Saving...", "progressbar");
        //            break;

        //    }
        //    ClickSaveTillRequestCreated();
        //}

        public string CreateRequest(string requestType, string jobType, Dictionary<string, string> testdata, string dateCaptured, string town, string postCode, string expUPRN)
        {
            string proposedEffectiveDateChanged;
            string dateReceived;
            Random rand = new Random();
            string dateType = null;
            int randomBAReference = rand.Next(100000, 999999);
            if (jobType.Contains("-"))
            {
                string[] btd = jobType.Split('-');
                jobType = btd[0].Trim();
                dateType = btd[1].Trim();
                //if (jobType == "Effective Date Change")
                //{
                //    dateType = btd[1].Trim();
                //}
            }

            string[] dateValue = dateCaptured.Split(' ');
            DateTime dateToEnter = DateTime.Parse(dateValue[0]);
            if (dateType == "Forward")
            {
                proposedEffectiveDateChanged = dateToEnter.AddDays(2).ToString("M/d/yyyy");
            }
            else if (dateType == "Backward")
            {
                proposedEffectiveDateChanged = dateToEnter.AddDays(-2).ToString("M/d/yyyy");
            }
            else if (dateType == "Current")// Effective from Date fetch from DB
            {
                proposedEffectiveDateChanged = dateToEnter.ToString("M/d/yyyy");
            }
            else
            {
                proposedEffectiveDateChanged = DateTime.Now.AddDays(-2).ToString("M/d/yyyy");

            }
            Console.WriteLine(proposedEffectiveDateChanged);
            dateReceived = DateTime.Now.AddDays(0).ToString("M/d/yyyy");

            switch (jobType)
            {
                case "Effective Date Change":
                    EnterDetailsInCategorisationSection(requestType, jobType);
                    SelectDateFromDatePickerElement(DateReceivedDatePicker, 0);
                    EnterFieldsInDetailsSectionForEFfectiveDate(proposedEffectiveDateChanged, testdata["ReasonForValidation"], testdata["Channel"], testdata["DSR"]);
                    //  EnterFieldsInDetailsSection(-2, testdata["ReasonForValidation"], testdata["Channel"], testdata["DSR"]);
                    EnterBARef(randomBAReference.ToString());
                    FindHereditamentForJobCreation(town, postCode, expUPRN);
                    Thread.Sleep(3000);
                    ClickCommandBarOption("Save");
                    waitTillSavingDisaddpears("Saving...", "progressbar");
                    break;

                case "Composite Property Change":
                    EnterDetailsInCategorisationSection(requestType, jobType);
                    SelectDateFromDatePickerElement(DateReceivedDatePicker, 0);
                    EnterFieldsInDetailsSectionForEFfectiveDate(proposedEffectiveDateChanged, testdata["ReasonForValidation"], testdata["Channel"], testdata["DSR"]);
                    EnterFieldsInDetailsSectionForCDP(testdata["ReasonForPortalReference"], testdata["ProposedBARefNum"]);
                    FindHereditamentForJobCreation(town, postCode, expUPRN);
                    ClickCommandBarOption("Save");
                    waitTillSavingDisaddpears("Saving...", "progressbar");
                    break;

                case "Informal Challenge":
                    EnterDetailsInCategorisationSection(requestType, jobType);
                    EnterFieldsCTPlayerSection(testdata["CTPlayer"]);
                    FindHereditamentForJobCreationUprn(town, postCode, expUPRN);
                    EnterFieldsInDetailsSectionForInformalChallenge(proposedEffectiveDateChanged, testdata["ReasonForValidation"], testdata["DSR"], proposedEffectiveDateChanged, proposedEffectiveDateChanged, testdata["AuthorisationDecision"], randomBAReference.ToString());
                    ClickCommandBarOption("Save");
                    waitTillSavingDisaddpears("Saving...", "progressbar");
                    break;

                case "Material Reduction":
                    EnterDetailsInCategorisationSection(requestType, jobType);
                    SelectDateFromDatePickerElement(DateReceivedDatePicker, 0);
                    EnterFieldsInDetailsSectionForEFfectiveDate(proposedEffectiveDateChanged, testdata["ReasonForValidation"], testdata["Channel"], testdata["DSR"]);
                    BAReportNumber.ClearAndSendkeys(rand.Next(10000, 99999).ToString());
                    Thread.Sleep(1000);
                    FindHereditamentForJobCreation(town, postCode, expUPRN);
                    break;

                case "Change of BA Reference":
                    EnterDetailsInCategorisationSection(requestType, jobType);
                    SelectDateFromDatePickerElement(DateReceivedDatePicker, 0);
                    EnterFieldsInDetailsSectionForEFfectiveDate(proposedEffectiveDateChanged, testdata["ReasonForValidation"], testdata["Channel"], testdata["DSR"]);
                    EnterFieldsInBAReferenceSection(testdata["ProposedBARefNum"]);
                    FindHereditamentForJobCreation(town, postCode, expUPRN);
                    Thread.Sleep(3000);
                    testdata["PostCode"] = postCode;
                    ClickCommandBarOption("Save");
                    waitTillSavingDisaddpears("Saving...", "progressbar");
                    break;

                case "Data Enhancement":
                    EnterDetailsInCategorisationSection(requestType, jobType);
                    SelectDateFromDatePickerElement(DateReceivedDatePicker, 0);
                    EnterFieldsInDetailsSection_DE(+14, testdata["ReasonForValidation"], testdata["DSR"]);
                    //FindHereditamentForJobCreation("NE25 0PT", "WHITLEY BAY", "");
                    FindHereditamentForJobCreationUpd(town, postCode, expUPRN);
                    break;

                case "Deletion":
                    EnterDetailsInCategorisationSection(requestType, jobType);
                    SelectDateFromDatePickerElement(DateReceivedDatePicker, 0);
                    EnterFieldsInDetailsSectionForEFfectiveDate(proposedEffectiveDateChanged, testdata["ReasonForValidation"], testdata["Channel"], testdata["DSR"]);
                    EnterBARef(randomBAReference.ToString());
                    EnterFieldsCTPlayerSection(testdata["CTPlayer"]);
                    FindHereditamentForJobCreation(town, postCode, expUPRN);
                    Thread.Sleep(3000);
                    ClickCommandBarOption("Save");
                    waitTillSavingDisaddpears("Saving...", "progressbar");
                    break;

                case "Full Demolition":
                    EnterDetailsInCategorisationSectionForDemolition(requestType, jobType, "No");
                    SelectDateFromDatePickerElement(DateReceivedDatePicker, 0);
                    if (testdata["FullDemolitionAutoProcess"] == "Yes")
                    {
                        EnterFieldsInDetailsSectionForEFfectiveDate(proposedEffectiveDateChanged, testdata["ReasonForValidation"], testdata["Channel"], testdata["DSR"]);

                    }
                    else
                    {
                        EnterFieldsInDetailsSectionForEFfectiveDate(proposedEffectiveDateChanged, testdata["ReasonForValidation"], testdata["Channel"], testdata["DSR"]);

                    }
                    EnterBARef(randomBAReference.ToString());
                    EnterFieldsCTPlayerSection(testdata["CTPlayer"]);
                    FindHereditamentForJobCreation(town, postCode, expUPRN);
                    Thread.Sleep(3000);
                    //  FindHereditamentForJobCreation("CARDIFF", "CF24 4EQ", "100100028622");
                    ClickCommandBarOption("Save");
                    waitTillSavingDisaddpears("Saving...", "progressbar");

                    break;

                case "Material Increase":
                    EnterDetailsInCategorisationSectionforCR10(requestType, jobType);
                    SelectDateFromDatePickerElement(DateReceivedDatePicker, 0);
                    EnterFieldsInDetailsSectionforCR10(proposedEffectiveDateChanged, testdata["ReasonForValidation"], testdata["Channel"], testdata["DSR"]);
                    //EnterFieldsInBAReferenceSection(randomBAReference.ToString());
                    FindHereditamentForJobCreation(town, postCode, expUPRN);
                    //  FindHereditamentForJobCreation("CARDIFF", "CF24 4EQ", "100100028622");
                    ClickCommandBarOption("Save");
                    waitTillSavingDisaddpears("Saving...", "progressbar");
                    break;

                case "Borderline CT to NDR":
                    EnterDetailsInCategorisationSection(requestType, jobType);
                    SelectDateFromDatePickerElement(DateReceivedDatePicker, 0);
                    EnterFieldsInDetailsSectionForEFfectiveDate(proposedEffectiveDateChanged, testdata["ReasonForValidation"], testdata["Channel"], testdata["DSR"]);
                    EnterFieldsCTPlayerSection(testdata["CTPlayer"]);
                    FindHereditamentForJobCreation(town, postCode, expUPRN);
                    Thread.Sleep(3000);
                    //  FindHereditamentForJobCreation("CARDIFF", "CF24 4EQ", "100100028622");
                    ClickCommandBarOption("Save");
                    waitTillSavingDisaddpears("Saving...", "progressbar");
                    break;

                case "Reconstitution Delete":
                    string ReconType = testdata["ReconType"];
                    if (ReconType == "Split")
                    {
                        EnterDetailsInCategorisationSectionForRecon(jobType, "Split");
                    }
                    else if (ReconType == "Merger")
                    {
                        EnterDetailsInCategorisationSectionForRecon(jobType, "Merger");
                    }
                    SelectDateFromDatePickerElement(DateReceivedDatePicker, 0);
                    EnterFieldsInDetailsSectionForEFfectiveDate(proposedEffectiveDateChanged, testdata["ReasonForValidation"], testdata["Channel"], testdata["DSR"]);
                    EnterBARef(randomBAReference.ToString());
                    //   EnterFieldsCTPlayerSection(testdata["CTPlayer"]);
                    FindHereditamentForJobCreation(town, postCode, expUPRN);
                    ValidateAndClickOnTab("Reconstitution", "Request Form");
                    if (ReconType == "Split")
                    {
                        EnterDetailsInReconTabForSplit(2, testdata["AddressLabel"]);
                    }
                    ClickCommandBarOption("Save");
                    waitTillSavingDisaddpears("Saving...", "progressbar");
                    break;

                case "New Property Re":
                    EnterDetailsInCategorisationSection(requestType, jobType);
                    EnterFieldsInDetailsSectionForEFfectiveDate(proposedEffectiveDateChanged, testdata["ReasonForValidation"], testdata["Channel"], testdata["DSR"], proposedEffectiveDateChanged);
                    EnterFieldsInBAReferenceSection(testdata["ProposedBARefNum"]);
                    EnterFieldsInDetailsSectionForReEntry();
                    FindHereditamentForJobCreation(town, postCode, expUPRN);
                    Thread.Sleep(8000);
                    ClickCommandBarOption("Save");
                    waitTillSavingDisaddpears("Saving...", "progressbar");
                    break;

                case "Change of Address":
                    EnterDetailsInCategorisationSection(requestType, jobType);
                    FindHereditamentForJobCreation(town, postCode, expUPRN);
                    SelectDateFromDatePickerElement(DateReceivedDatePicker, 0);
                    if (testdata["Autoprocess"] == "Yes")
                    {
                        FindAddressForChangeOfAddressforAuto(postCode);
                    }
                    else
                    {
                        FindAddressForDuplicateChangeOfAddress(postCode);
                    }
                    EnterFieldsInDetailsSectionForEFfectiveDate(proposedEffectiveDateChanged, testdata["ReasonForValidation"], testdata["Channel"], testdata["DSR"]);
                    ClickCommandBarOption("Save");
                    waitTillSavingDisaddpears("Saving...", "progressbar");
                    break;

            }
            ClickSaveTillRequestCreated();
            return proposedEffectiveDateChanged;
        }



        public string ValidateJobCreation(string requestAction, string MenuOption, string jobType)
        {
            string jobID = null;

            switch (jobType)
            {
                case "New Property - Individual":
                    ClickMenuListOptionFromCommandBar(requestAction, MenuOption);
                    ClickProceedButtonOnDialog();
                    //Thread.Sleep(6000);
                    ValidateAndClickOnTab("Related Jobs", "Request Form");
                    Assert.IsTrue(VerifyJobCreatedDisplayedOnRelatedJobsTab(), "The job is not displayed in Related Jobs Tab");
                    jobID = ClickOnJobCreatedAndSaveJobId();
                    //IsJobCreatedRequestPageIsDisplayed(jobID);
                    //Thread.Sleep(12000);
                    //ClickOnJobAssign();
                    break;

                case "New Property - Estate":
                    ClickMenuListOptionFromCommandBar(requestAction, MenuOption);
                    ClickProceedButtonOnDialog();
                    // ClickSaveAndCloseOnDialogforValidateRequest();
                    Thread.Sleep(6000);
                    ClickOnOptionInRelatedTabOnForm("Auto Processing", "Related", "Request Form");
                    ValidateAutoProcessingRuntimeStatus("Completed");
                    ClickOnOptionInRelatedTabOnForm("Child Jobs", "Related", "Request Form");
                    jobID = ClickOnEtateJObID();
                    IsJobCreatedRequestPageIsDisplayed(jobID);
                    Thread.Sleep(8000);
                    break;

                case "Estate File":
                    ClickMenuListOptionFromCommandBar(requestAction, MenuOption);
                    ClickSaveAndCloseOnDialogforValidateRequest();
                    Thread.Sleep(6000);
                    ValidateAndClickOnTab("Related Jobs", "Request Form");
                    Assert.IsTrue(VerifyJobCreatedDisplayedOnRelatedJobsTab(), "The job is not displayed in Related Jobs Tab");
                    //jobID = ClickOnEtateJObID();
                    //IsJobCreatedRequestPageIsDisplayed(jobID);
                    break;

                case "Effective Date Change":
                    ClickMenuListOptionFromCommandBar(requestAction, MenuOption);
                    ClickSaveAndCloseOnDialogforValidateRequest();
                    Thread.Sleep(6000);
                    //  ValidateAndClickOnTab("Related", "Request Form");
                    ValidateAndClickOnTab("Related Jobs", "Request Form");
                    Assert.IsTrue(VerifyJobCreatedDisplayedOnRelatedJobsTab(), "The job is not displayed in Related Jobs Tab");
                    jobID = ClickOnJobCreatedAndSaveJobId();
                    //IsJobCreatedRequestPageIsDisplayed(jobID);
                    //Thread.Sleep(12000);
                    //ClickOnJobAssign();
                    break;

                case "Composite Property Change":
                    ClickMenuListOptionFromCommandBar(requestAction, MenuOption);
                    ClickSaveAndCloseOnDialogforValidateRequest();
                    //Thread.Sleep(6000);
                    ValidateAndClickOnTab("Related Jobs", "Request Form");
                    Assert.IsTrue(VerifyJobCreatedDisplayedOnRelatedJobsTab(), "The job is not displayed in Related Jobs Tab");
                    jobID = ClickOnJobCreatedAndSaveJobId();
                    //IsJobCreatedRequestPageIsDisplayed(jobID);
                    //Thread.Sleep(12000);
                    //ClickOnJobAssign();
                    break;

                case "Material Increase":
                    Thread.Sleep(5000);
                    ClickMenuListOptionFromCommandBar(requestAction, MenuOption);
                    ClickSaveAndCloseOnDialogforValidateRequest();
                    ValidateAndClickOnTab("Related Jobs", "Request Form");
                    Assert.IsTrue(VerifyJobCreatedDisplayedOnRelatedJobsTab(), "The job is not displayed in Related Jobs Tab");
                    jobID = ClickOnJobCreatedAndSaveJobId();
                    //IsJobCreatedRequestPageIsDisplayed(jobID);
                    //Thread.Sleep(12000);
                    //ClickOnJobAssign();
                    break;

                case "Material Reduction":
                    ClickMenuListOptionFromCommandBar(requestAction, MenuOption);
                    ClickSaveAndCloseOnDialogforValidateRequest();
                    Thread.Sleep(6000);
                    //   ValidateAndClickOnTab("Related", "Request Form");
                    ValidateAndClickOnTab("Related Jobs", "Request Form");
                    Assert.IsTrue(VerifyJobCreatedDisplayedOnRelatedJobsTab(), "The job is not displayed in Related Jobs Tab");
                    jobID = ClickOnJobCreatedAndSaveJobId();
                    IsJobCreatedRequestPageIsDisplayed(jobID);
                    Thread.Sleep(12000);
                    ClickOnJobAssign();
                    break;

                case "Deletion":
                    ClickMenuListOptionFromCommandBar(requestAction, MenuOption);
                    ClickSaveAndCloseOnDialogforValidateRequest();
                    Thread.Sleep(6000);
                    //     ValidateAndClickOnTab("Related", "Request Form");
                    ValidateAndClickOnTab("Related Jobs", "Request Form");
                    Assert.IsTrue(VerifyJobCreatedDisplayedOnRelatedJobsTab(), "The job is not displayed in Related Jobs Tab");
                    jobID = ClickOnJobCreatedAndSaveJobId();
                    //IsJobCreatedRequestPageIsDisplayed(jobID);
                    //Thread.Sleep(12000);
                    //ClickOnJobAssign();
                    break;

                case "Full Demolition":
                    ClickMenuListOptionFromCommandBar(requestAction, MenuOption);
                    ClickSaveAndCloseOnDialogforValidateRequest();
                    Thread.Sleep(6000);
                    //     ValidateAndClickOnTab("Related", "Request Form");
                    ValidateAndClickOnTab("Related Jobs", "Request Form");
                    Assert.IsTrue(VerifyJobCreatedDisplayedOnRelatedJobsTab(), "The job is not displayed in Related Jobs Tab");
                    jobID = ClickOnJobCreatedAndSaveJobId();
                    //IsJobCreatedRequestPageIsDisplayed(jobID);
                    //Thread.Sleep(12000);
                    //ClickOnJobAssign();
                    break;

                case "New Property ReEntry":
                    ClickMenuListOptionFromCommandBar(requestAction, MenuOption);
                    ClickSaveAndCloseOnDialogforValidateRequest();
                    Thread.Sleep(6000);
                    ValidateAndClickOnTab("Related Jobs", "Request Form");
                    Assert.IsTrue(VerifyJobCreatedDisplayedOnRelatedJobsTab(), "The job is not displayed in Related Jobs Tab");
                    jobID = ClickOnJobCreatedAndSaveJobId();
                    //  IsJobCreatedRequestPageIsDisplayed(jobID);
                    //Thread.Sleep(12000);
                    //ClickOnJobAssign();
                    break;

                case "Informal Challenge":
                    //Thread.Sleep(10000);
                    ClickMenuListOptionFromCommandBar(requestAction, MenuOption);
                    ClickSaveAndCloseOnDialogforValidateRequest();
                    //Thread.Sleep(6000);
                    ValidateAndClickOnTab("Related Jobs", "Request Form");
                    Assert.IsTrue(VerifyJobCreatedDisplayedOnRelatedJobsTab(), "The job is not displayed in Related Jobs Tab");
                    jobID = ClickOnJobCreatedAndSaveJobId();
                    //IsJobCreatedRequestPageIsDisplayed(jobID);
                    //Thread.Sleep(12000);
                    //ClickOnJobAssign();
                    break;

                case "Borderline CT to NDR":
                    ClickMenuListOptionFromCommandBar(requestAction, MenuOption);
                    ClickSaveAndCloseOnDialogforValidateRequest();
                    Thread.Sleep(6000);
                    //     ValidateAndClickOnTab("Related", "Request Form");
                    ValidateAndClickOnTab("Related Jobs", "Request Form");
                    Assert.IsTrue(VerifyJobCreatedDisplayedOnRelatedJobsTab(), "The job is not displayed in Related Jobs Tab");
                    jobID = ClickOnJobCreatedAndSaveJobId();
                    //IsJobCreatedRequestPageIsDisplayed(jobID);
                    //Thread.Sleep(12000);
                    //ClickOnJobAssign();
                    break;

                case "Change of Address":
                    ClickMenuListOptionFromCommandBar(requestAction, MenuOption);
                    // ClickProceedButtonOnDialog();
                    Thread.Sleep(6000);
                    //   ValidateAndClickOnTab("Related", "Request Form");
                    ValidateAndClickOnTab("Related Jobs", "Request Form");
                    Assert.IsTrue(VerifyJobCreatedDisplayedOnRelatedJobsTab(), "The job is not displayed in Related Jobs Tab");
                    jobID = ClickOnJobCreatedAndSaveJobId();
                    //IsJobCreatedRequestPageIsDisplayed(jobID);
                    //Thread.Sleep(12000);
                    //ClickOnJobAssign();
                    break;

                case "CRM Enquiries":
                    ClickMenuListOptionFromCommandBar(requestAction, MenuOption);
                    ClickSaveAndCloseOnDialogforValidateRequest();
                    Thread.Sleep(6000);
                    //     ValidateAndClickOnTab("Related", "Request Form");
                    ValidateAndClickOnTab("Related Jobs", "Request Form");
                    Assert.IsTrue(VerifyJobCreatedDisplayedOnRelatedJobsTab(), "The job is not displayed in Related Jobs Tab");
                    //jobID = ClickOnJobCreatedAndSaveJobId();
                    //Thread.Sleep(6000);
                    break;

                    //case "Reconstitution Delete":
                    //    ClickMenuListOptionFromCommandBar(requestAction, MenuOption);
                    //    ClickProceedButtonOnDialog();
                    //    ConfirmButtonOnDialog.ClickUsingJavascript();
                    //    Thread.Sleep(6000);
                    //    ValidateAndClickOnTab("Related Jobs", "Request Form");
                    //    ValidateJobCreatedDisplayedOnRelatedJobsTab(testdata);
                    //    break;
            }
            return jobID;


        }


        public string ValidateJobCreation(string requestAction, string MenuOption, string jobType, string expBARef, string town, string postcode, string uprn, Dictionary<string, string> testdata)
        {
            string jobID = null;
            string processType = null;

            if (jobType.Contains("-"))
            {
                string[] btd = jobType.Split('-');
                jobType = btd[0].Trim();
                processType = btd[1].Trim();

            }

            switch (jobType)
            {
                case "Change of BA Reference":
                    ClickMenuListOptionFromCommandBar(requestAction, MenuOption);
                    //ChecKUnsavedChanges(requestAction, MenuOption);
                    ClickSaveAndCloseOnDialogforValidateRequest();
                    ValidateAndClickOnTab("BA Reference", "Request Form");
                    if (testdata["SwapHereditament"] == "Yes")
                    {
                        ValidateDetailsOnBARefLinksTabAndSwapHereditament(testdata["SubmittedBy"], expBARef, town, postcode, uprn);

                    }
                    else
                    {
                        ValidateDetailsOnBARefLinksTabAndSave(testdata["SubmittedBy"], expBARef);
                    }
                    if (processType == "AutoProcess")
                    {
                        SeleniumExtensions.SelectDropdownValue(ValidateForAutoProcess, "Yes");
                        ClickCommandBarOption("Save");
                        waitTillSavingDisaddpears("Saving...", "progressbar");
                    }
                    ValidateAndClickOnTab("Related Jobs", "Request Form");
                    Assert.IsTrue(VerifyJobCreatedDisplayedOnRelatedJobsTab(), "The job is not displayed in Related Jobs Tab");
                    jobID = ClickOnJobCreatedAndSaveJobId();
                    // IsJobCreatedRequestPageIsDisplayed(jobID);
                    Thread.Sleep(14000);
                    break;

                case "Reconstitution Split":
                    ClickMenuListOptionFromCommandBar(requestAction, MenuOption);
                    //ClickProceedButtonOnDialog();
                    ConfirmButtonOnDialog.ClickUsingJavascript();
                    Thread.Sleep(6000);
                    ValidateAndClickOnTab("Related Jobs", "Request Form");
                    ValidateJobsCreatedAndCaptureJobID(testdata);
                    //  ValidateJobCreatedDisplayedOnRelatedJobsTab(testdata);
                    break;

                case "Reconstitution Merge":
                    EnterDetailsInReconTabForMerge(testdata["AddressLabel"], town, postcode, uprn);
                    ClickMenuListOptionFromCommandBar(requestAction, MenuOption);
                    //ClickProceedButtonOnDialog();
                    ConfirmButtonOnDialog.ClickUsingJavascript();
                    Thread.Sleep(6000);
                    ValidateAndClickOnTab("Related Jobs", "Request Form");
                    ValidateJobsCreatedAndCaptureJobID(testdata);
                    //  ValidateJobCreatedDisplayedOnRelatedJobsTab(testdata);
                    Thread.Sleep(6000);
                    break;
            }
            return jobID;


        }

        public string ValidateJobCreation(string requestAction, string MenuOption, string jobType, Dictionary<string, string> testdata)
        {
            string jobID = null;
            string processType = null;

            if (jobType.Contains("-"))
            {
                string[] btd = jobType.Split('-');
                jobType = btd[0].Trim();
                processType = btd[1].Trim();

            }

            switch (jobType)
            {
                case "New Estate":
                    ClickMenuListOptionFromCommandBar(requestAction, MenuOption);
                    ClickProceedButtonOnDialog();
                    // ClickSaveAndCloseOnDialogforValidateRequest();
                    Thread.Sleep(6000);
                    ClickOnOptionInRelatedTabOnForm("Auto Processing", "Related", "Request Form");
                    if (testdata["ExpAutoProcessStatus"] == "Suspended")
                    {
                        ValidateAutoProcessingRuntimeStatus(testdata["ExpAutoProcessStatus"]);
                        ClickOnOptionInRelatedTabOnForm("Related Jobs", "Related", "Request Form");
                        jobID = ClickOnEtateJObID();

                    }
                    else
                    {
                        ValidateAutoProcessingRuntimeStatus(testdata["ExpAutoProcessStatus"]);
                        ClickOnOptionInRelatedTabOnForm("Child Jobs", "Related", "Request Form");
                        jobID = ClickOnEtateJObID();
                        IsJobCreatedRequestPageIsDisplayed(jobID);
                    }
                    break;
            }
            return jobID;


        }



        public Dictionary<String, String> UserCaptureJobDetails(String jobID, String storageContext, String RefreshBtn)
        {
            waitHelpers wh = new waitHelpers();
            Dictionary<String, String> correspondenceJobDetails = new Dictionary<string, string>();
            bool isEleDisplayed = false;
            By templateNmeBy = SeleniumExtensions.getLocator($"//a[@aria-label='CT Notice - Notice of Alteration']");
            By jobIDBy = SeleniumExtensions.getLocator($"//*[contains(@aria-label,'OBC-')]");
            By buttonBy = SeleniumExtensions.getLocator($"ul[aria - label = 'Outbound Correspondence Commands'] button[aria - label = '{RefreshBtn}']");
            By jobIdRowBy = SeleniumExtensions.getLocator($"//*[@col-id='voa_name' and not(contains(@class,'header'))]/ancestor::*[@role='row']");
            int i = 0;
            IWebElement jobidEle, jobidEleRow;
            do
            {
                try
                {
                    if (wh.GetWebElement(jobIdRowBy, 1))
                    {
                        jobidEleRow = wh.GetWebElement(jobIDBy);
                        isEleDisplayed = jobidEleRow.Displayed;
                    }
                    else
                    {
                        wh.clickOnElement(buttonBy);
                    }

                }
                catch (Exception e)
                {
                }
                i++;
                if (i == 20) break;
            } while (!isEleDisplayed);
            correspondenceJobDetails["Correspondence" + jobID] = wh.GetWebElement(jobIDBy).GetAttribute("aria-label").Trim();
            return correspondenceJobDetails;
        }

        public string getSupplementaryJobDetails(String parentJobName)
        {
            waitHelpers wh = new waitHelpers();
            String SupplementaryJobDetails = "";
            bool isEleDisplayed = false;
            By rowCountBy = SeleniumExtensions.getLocator($"//div[contains(@class,'ag-row') and @role='row']");
            By jobTypeBy = SeleniumExtensions.getLocator($"(//*[@col-id='voa_codedreason'])//span[not((text()='Proposal'))]");
            By hereditamentBy = SeleniumExtensions.getLocator($"(//*[@col-id='voa_statutoryspatialunitid'])[2]//span");
            wh.isElementDisplayed(jobTypeBy, 120);
            wh.isElementDisplayed(rowCountBy, 60);
            if (wh.getAllWebElements(rowCountBy).Count == 2)
            {
                string jobtypeTxt = wh.getElementText(jobTypeBy);
                string hereditamentTxt = wh.getElementText(hereditamentBy);
                SupplementaryJobDetails = parentJobName.Replace("Proposal", jobtypeTxt);
                //$"CT: Job, {jobtypeTxt}, {hereditamentTxt}";
            }
            return SupplementaryJobDetails;
        }

        public string getSupplementaryJobDetails_new(String parentJobName)
        {
            waitHelpers wh = new waitHelpers();
            String SupplementaryJobDetails = "";
            bool isEleDisplayed = false;
            By rowCountBy = SeleniumExtensions.getLocator($"//div[contains(@class,'ag-row') and @role='row']");
            By jobTypeBy = SeleniumExtensions.getLocator($"(//*[@col-id='voa_codedreason'])//span[not((text()='Material Reduction'))]");
            By hereditamentBy = SeleniumExtensions.getLocator($"(//*[@col-id='voa_statutoryspatialunitid'])[2]//span");
            wh.isElementDisplayed(jobTypeBy, 120);
            wh.isElementDisplayed(rowCountBy, 60);
            if (wh.getAllWebElements(rowCountBy).Count == 2)
            {
                string jobtypeTxt = wh.getElementText(jobTypeBy);
                string hereditamentTxt = wh.getElementText(hereditamentBy);
                SupplementaryJobDetails = parentJobName.Replace("Material Reduction", jobtypeTxt);
                //$"CT: Job, {jobtypeTxt}, {hereditamentTxt}";
            }
            return SupplementaryJobDetails;
        }

        public string getAllSupplementaryJobDetails(String parentJobName)
        {
            waitHelpers wh = new waitHelpers();
            String SupplementaryJobDetails = "";
            bool isEleDisplayed = false;
            By rowCountBy = SeleniumExtensions.getLocator($"//div[contains(@class,'ag-row') and @role='row']");
            By jobTypeBy = SeleniumExtensions.getLocator($"(//*[@col-id='voa_codedreason'])//span[not((text()='Proposal'))]");
            By hereditamentBy = SeleniumExtensions.getLocator($"(//*[@col-id='voa_statutoryspatialunitid'])[2]//span");
            wh.isElementDisplayed(jobTypeBy, 120);
            wh.isElementDisplayed(rowCountBy, 60);
            if (wh.getAllWebElements(rowCountBy).Count == 3)
            {
                string jobtypeTxt = wh.getElementText(jobTypeBy);
                string hereditamentTxt = wh.getElementText(hereditamentBy);
                SupplementaryJobDetails = parentJobName.Replace("Proposal", jobtypeTxt);
            }
            return SupplementaryJobDetails;
        }


        public Dictionary<String, String> UserCaptureEstateDetails(String fieldName, String storageContext)
        {
            waitHelpers wh = new waitHelpers();
            Dictionary<String, String> estateFileName = new Dictionary<string, string>();
            By estateFileNameBy = By.XPath($"//div[@data-lp-id='form-header-title']/h1");
            //By estateFileNameBy = By.XPath($"(//div[contains(@data-lp-id,'voa_estatefile')]//span[contains(@class,'spanWrapper')])[1]");
            estateFileName[fieldName] = wh.GetWebElement(estateFileNameBy).GetAttribute("title");
            //wh.getElementText(estateFileNameBy);
            return estateFileName;
        }


        public Dictionary<String, String> UserCaptureAllJobDetails(int numOfJobs, String jobID, String JobName, FeatureContext fc, String RefreshBtn)
        {
            waitHelpers wh = new waitHelpers();
            Dictionary<String, String> jobDetails = new Dictionary<string, string>();
            bool isEleDisplayed = false;
            bool isTitleDisplayed = false;
            String jobIdRow = "//*[@col-id='ticketnumber' and not(contains(@class,'header'))]/ancestor::*[@role='row']";
            String jobName = "//*[@col-id='title' and not(contains(@class,'header'))]//a//span";
            By eleRowBy = SeleniumExtensions.getLocator(jobIdRow);
            By jobTitleBy = SeleniumExtensions.getLocator(jobName);
            int i = 0;
            IWebElement jobidEle, jobidEleRow, jobTitleName;
            int k = 0;
            int l = 0;
            int precheck = 0;
            bool isAllTitlesDisplayed_new = false;

            do
            {
                string jobTitlesRow = $"//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row')]//*[@col-id='title']//a";
                By eleByAllTitleEle = SeleniumExtensions.getLocator(jobTitlesRow);
                String selector = $"ul[aria-label='Job Commands'] button[aria-label = '{RefreshBtn}']";
                By eleBy = SeleniumExtensions.getLocator(selector);
                wh.clickOnElement(eleBy);
                try
                {
                    IList<IWebElement> allTitles = wh.getAllWebElements(eleByAllTitleEle);
                    if (allTitles.Count == 3)
                    {
                        isAllTitlesDisplayed_new = true;
                        break;
                    }
                    else
                    {
                        isAllTitlesDisplayed_new = false;

                    }
                }
                catch (Exception e)
                {
                    isAllTitlesDisplayed_new = false;

                }
                precheck++;
                if (precheck == 150) break;
            } while (!isAllTitlesDisplayed_new);

            for (int j = 0; j < numOfJobs; j++)
            {
                string Jobtitlerow = $"//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row') and @row-index='{j}']//*[@col-id='title']//a";
                string jobIdRow_new = $"//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row') and @row-index='{j}']//*[contains(@col-id,'ticketnumber')]//label";
                By JobtitlerowBy = SeleniumExtensions.getLocator(Jobtitlerow);
                By JobIDrow_NewBy = SeleniumExtensions.getLocator(jobIdRow_new);
                bool isAllTitlesDisplayed = false;
                do
                {
                    String selector = $"ul[aria-label='Job Commands'] button[aria-label = '{RefreshBtn}']";
                    By eleBy = SeleniumExtensions.getLocator(selector);
                    wh.clickOnElement(eleBy);
                    try
                    {

                        if (wh.isElementDisplayed(JobIDrow_NewBy, 1))
                        {
                            jobidEleRow = wh.GetWebElement(JobIDrow_NewBy);
                            isEleDisplayed = wh.isElementDisplayed(JobIDrow_NewBy, 2);
                            if (isEleDisplayed)
                            {
                                isTitleDisplayed = wh.isElementDisplayed(JobtitlerowBy, 3);
                                Console.WriteLine("isTitleDisplayed : " + isTitleDisplayed);
                                if (isTitleDisplayed)
                                {
                                    if (wh.GetWebElement(JobtitlerowBy).Text.Trim().Contains("Delete"))
                                    {
                                        jobDetails["ReconDel_" + l] = wh.GetWebElement(JobtitlerowBy).Text.Trim();
                                        fc["ReconDel_" + l] = wh.GetWebElement(JobtitlerowBy).Text.Trim();
                                        Console.WriteLine("Job ReconDel_" + l + " : " + wh.GetWebElement(JobtitlerowBy).Text.Trim());
                                        l = l + 1;

                                    }
                                    if (wh.GetWebElement(JobtitlerowBy).Text.Trim().Contains("New"))
                                    {
                                        jobDetails["ReconNew_" + k] = wh.GetWebElement(JobtitlerowBy).Text.Trim();
                                        fc["ReconNew_" + k] = wh.GetWebElement(JobtitlerowBy).Text.Trim();
                                        Console.WriteLine("Job ReconNew_" + k + " : " + wh.GetWebElement(JobtitlerowBy).Text.Trim());
                                        k = k + 1;
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                    }
                    i++;
                    if (i == 150) break;
                    Console.WriteLine("Condition : " + (!isEleDisplayed && !isTitleDisplayed));
                } while (!isTitleDisplayed);


            }

            return jobDetails;
        }

        public Dictionary<string, string> UserCapturedAllJobDetails(int numOfJobs, string jobID, string JobName, FeatureContext fc, string refreshBtn)
        {
            waitHelpers wh = new waitHelpers();
            Dictionary<string, string> jobDetails = new Dictionary<string, string>();
            int deleteIndex = 0;
            int newIndex = 0;

            for (int j = 0; j < numOfJobs; j++)
            {
                string titleXpath = $"//*[@role='rowgroup']//*[contains(@class,'ag-row') and @row-index='{j}']//*[@col-id='title']//a";
                string idXpath = $"//*[@role='rowgroup']//*[contains(@class,'ag-row') and @row-index='{j}']//*[@col-id='ticketnumber']//label";
                By titleLocator = SeleniumExtensions.getLocator(titleXpath);
                By idLocator = SeleniumExtensions.getLocator(idXpath);

                int retries = 0;
                bool captured = false;

                while (retries < 10 && !captured)
                {
                    try
                    {
                        // Optional: refresh grid only on first retry
                        if (retries > 0)
                        {
                            By refreshButton = SeleniumExtensions.getLocator($"ul[aria-label='Job Commands'] button[aria-label='{refreshBtn}']");
                            wh.clickOnElement(refreshButton);
                            Thread.Sleep(1000); // Small delay to allow refresh
                        }
                        if (wh.isElementDisplayed(titleLocator, 3) && wh.isElementDisplayed(idLocator, 2))
                        {
                            string jobTitle = wh.GetWebElement(titleLocator).Text.Trim();
                            string jobKey = "";

                            if (jobTitle.Contains("Delete"))
                            {
                                jobKey = $"ReconDel_{deleteIndex++}";
                            }
                            else if (jobTitle.Contains("New"))
                            {
                                jobKey = $"ReconNew_{newIndex++}";
                            }
                            else
                            {
                                jobKey = $"Job_{j}";
                            }

                            jobDetails[jobKey] = jobTitle;
                            fc[jobKey] = jobTitle;

                            Console.WriteLine($"Captured: {jobKey} → {jobTitle}");
                            captured = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Retry {retries}: Unable to capture job at row {j} – {ex.Message}");
                    }

                    retries++;
                }

                if (!captured)
                {
                    Console.WriteLine($"❌ Failed to capture job at row {j} after 10 retries");
                }
            }

            return jobDetails;
        }

        public Dictionary<String, String> UserCaptureAllJobNames(int numOfJobs, String jobID, String JobName, FeatureContext fc, String RefreshBtn)
        {
            waitHelpers wh = new waitHelpers();
            Dictionary<String, String> jobDetails = new Dictionary<string, string>();
            bool isEleDisplayed = false;
            bool isTitleDisplayed = false;
            String jobIdRow = "//*[@col-id='ticketnumber' and not(contains(@class,'header'))]/ancestor::*[@role='row']";
            String jobName = "//*[@col-id='title' and not(contains(@class,'header'))]//a//span";
            By eleRowBy = SeleniumExtensions.getLocator(jobIdRow);
            By jobTitleBy = SeleniumExtensions.getLocator(jobName);
            int i = 0;
            IWebElement jobidEle, jobidEleRow, jobTitleName;
            int k = 0;
            int l = 0;
            for (int j = 0; j < numOfJobs; j++)
            {
                string Jobtitlerow = $"//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row') and @row-index='{j}']//*[@col-id='title']//a";
                string jobIdRow_new = $"//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row') and @row-index='{j}']//*[contains(@col-id,'ticketnumber')]//label";
                By JobtitlerowBy = SeleniumExtensions.getLocator(Jobtitlerow);
                By JobIDrow_NewBy = SeleniumExtensions.getLocator(jobIdRow_new);
                bool isAllTitlesDisplayed = false;
                do
                {
                    String selector = $"ul[aria-label='Job Commands'] button[aria-label = '{RefreshBtn}']";
                    By eleBy = SeleniumExtensions.getLocator(selector);
                    wh.clickOnElement(eleBy);
                    try
                    {

                        if (wh.isElementDisplayed(JobIDrow_NewBy, 1))
                        {
                            jobidEleRow = wh.GetWebElement(JobIDrow_NewBy);
                            isEleDisplayed = wh.isElementDisplayed(JobIDrow_NewBy, 2);
                            if (isEleDisplayed)
                            {
                                isTitleDisplayed = wh.isElementDisplayed(JobtitlerowBy, 3);
                                Console.WriteLine("isTitleDisplayed : " + isTitleDisplayed);
                                if (isTitleDisplayed)
                                {
                                    string jobtitleRowText = wh.GetWebElement(JobtitlerowBy).Text.Trim();
                                    string[] jobNameSplit = jobtitleRowText.Split(',');
                                    jobDetails[jobNameSplit[1]] = jobtitleRowText;
                                    fc[jobNameSplit[1]] = jobtitleRowText;
                                    Console.WriteLine("Job type : " + jobNameSplit[1] + " , Job title : " + jobtitleRowText);

                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        isTitleDisplayed = false;
                    }
                    i++;
                    if (i == 150) break;
                    Console.WriteLine("Condition : " + (!isEleDisplayed && !isTitleDisplayed));
                } while (!isTitleDisplayed);


            }

            return jobDetails;
        }

        public Dictionary<String, String> UserCaptureAllJobDetails_New(int numOfJobs, String jobID, String JobName, FeatureContext fc, String RefreshBtn)
        {
            waitHelpers wh = new waitHelpers();
            Dictionary<String, String> jobDetails = new Dictionary<string, string>();
            bool isEleDisplayed = false;
            bool isTitleDisplayed = false;
            String jobIdRow = "//*[@col-id='ticketnumber' and not(contains(@class,'header'))]/ancestor::*[@role='row']";
            String jobName = "//*[@col-id='title' and not(contains(@class,'header'))]//a//span";
            By eleRowBy = SeleniumExtensions.getLocator(jobIdRow);
            By jobTitleBy = SeleniumExtensions.getLocator(jobName);
            int i = 0;
            IWebElement jobidEle, jobidEleRow, jobTitleName;
            int k = 0;
            int l = 0;
            for (int j = 0; j < numOfJobs; j++)
            {
                string Jobtitlerow = $"//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row') and @row-index='{j}']//*[@col-id='title']//a";
                string jobIdRow_new = $"//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row') and @row-index='{j}']//*[contains(@col-id,'ticketnumber')]//label";
                By JobtitlerowBy = SeleniumExtensions.getLocator(Jobtitlerow);
                By JobIDrow_NewBy = SeleniumExtensions.getLocator(jobIdRow_new);
                bool isAllTitlesDisplayed = false;
                do
                {
                    String selector = $"ul[aria-label='Job Commands'] button[aria-label = '{RefreshBtn}']";
                    By eleBy = SeleniumExtensions.getLocator(selector);
                    wh.clickOnElement(eleBy);
                    try
                    {

                        if (wh.isElementDisplayed(JobIDrow_NewBy, 1))
                        {
                            jobidEleRow = wh.GetWebElement(JobIDrow_NewBy);
                            isEleDisplayed = wh.isElementDisplayed(JobIDrow_NewBy, 2);
                            if (isEleDisplayed)
                            {
                                isTitleDisplayed = wh.isElementDisplayed(JobtitlerowBy, 3);
                                Console.WriteLine("isTitleDisplayed : " + isTitleDisplayed);
                                if (isTitleDisplayed)
                                {
                                    jobDetails["Job_" + l] = wh.GetWebElement(JobtitlerowBy).Text.Trim();
                                    fc["Job_" + l] = wh.GetWebElement(JobtitlerowBy).Text.Trim();
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                    }
                    i++;
                    if (i == 150) break;
                    Console.WriteLine("Condition : " + (!isEleDisplayed && !isTitleDisplayed));
                } while (!isTitleDisplayed);


            }

            return jobDetails;
        }

        public Dictionary<String, String> UserWaitsTillAllJobNamesDisplay(int numOfJobs, String RefreshBtn)
        {
            waitHelpers wh = new waitHelpers();
            Dictionary<String, String> jobDetails = new Dictionary<string, string>();
            bool isEleDisplayed = false;
            bool isTitleDisplayed = false;
            String jobIdRow = "//*[@col-id='ticketnumber' and not(contains(@class,'header'))]/ancestor::*[@role='row']";
            String jobName = "//*[@col-id='title' and not(contains(@class,'header'))]//a//span";
            By eleRowBy = SeleniumExtensions.getLocator(jobIdRow);
            By jobTitleBy = SeleniumExtensions.getLocator(jobName);
            int i = 0;
            IWebElement jobidEle, jobidEleRow, jobTitleName;
            int k = 0;
            int l = 0;
            for (int j = 0; j < numOfJobs; j++)
            {
                string Jobtitlerow = $"//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row') and @row-index='{j}']//*[@col-id='title']//a";
                string jobIdRow_new = $"//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row') and @row-index='{j}']//*[contains(@col-id,'ticketnumber')]//label";
                By JobtitlerowBy = SeleniumExtensions.getLocator(Jobtitlerow);
                By JobIDrow_NewBy = SeleniumExtensions.getLocator(jobIdRow_new);
                bool isAllTitlesDisplayed = false;
                do
                {
                    String selector = $"ul[aria-label='Job Commands'] button[aria-label = '{RefreshBtn}']";
                    By eleBy = SeleniumExtensions.getLocator(selector);
                    wh.clickOnElement(eleBy);
                    try
                    {
                        if (wh.isElementDisplayed(JobIDrow_NewBy, 1))
                        {
                            jobidEleRow = wh.GetWebElement(JobIDrow_NewBy);
                            isEleDisplayed = wh.isElementDisplayed(JobIDrow_NewBy, 2);
                            if (isEleDisplayed)
                            {
                                isTitleDisplayed = wh.isElementDisplayed(JobtitlerowBy, 3);
                                Console.WriteLine("isTitleDisplayed : " + isTitleDisplayed);
                                if (isTitleDisplayed)
                                {
                                    jobDetails["Job_" + l] = wh.GetWebElement(JobtitlerowBy).Text.Trim();
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                    }
                    i++;
                    if (i == 150) break;
                    Console.WriteLine("Condition : " + (!isEleDisplayed && !isTitleDisplayed));
                } while (!isTitleDisplayed);
            }
            return jobDetails;
        }

        public string milodetails(String MiloID, ScenarioContext sc)
        {
            waitHelpers wh = new waitHelpers();
            string miloid = "";
            //String miloidRow = "//*[@col-id='voa_name' and not(contains(@class,'header'))]/ancestor::*[@role='row']";
            String miloidRow = "(//*[@col-id='voa_name' and not(contains(@class,'header'))])[1]";
            By miloidBy = SeleniumExtensions.getLocator(miloidRow);
            miloid = wh.GetWebElement(miloidBy).Text.Trim();
            return miloid;
        }
        public Dictionary<string, string> UserCaptureJobDetails_abort(string jobID, string storageContext)
        {
            waitHelpers wh = new waitHelpers();
            Dictionary<string, string> jobDetails = new Dictionary<string, string>();

            bool isEleDisplayed = false;
            bool isTitleDisplayed = false;
            bool isIdDisplayed = false;

            string jobIdRow = "//*[@col-id='ticketnumber' and not(contains(@class,'header'))]/ancestor::*[@role='row']";
            string jobIDLoc = "//*[@col-id='ticketnumber' and not(contains(@class,'header'))]//label/div";

            By eleRowBy = SeleniumExtensions.getLocator(jobIdRow);
            By jobIDBy = SeleniumExtensions.getLocator(jobIDLoc);

        



            jobDetails[jobID] = wh.GetWebElement(jobIDBy).Text.Trim();

            return jobDetails;
        }

        public Dictionary<String, String> UserCaptureJobDetails(String jobID, String JobName, String storageContext, String RefreshBtn)
        {
            waitHelpers wh = new waitHelpers();
            Dictionary<String, String> jobDetails = new Dictionary<string, string>();
            bool isEleDisplayed = false;
            bool isTitleDisplayed = false;
            bool isIdDisplayed = false;

            String jobIdRow = "//*[@col-id='ticketnumber' and not(contains(@class,'header'))]/ancestor::*[@role='row']";
            String jobName = "//*[@col-id='title' and not(contains(@class,'header'))]//a//span";
            String jobIDLoc = "//*[@col-id='ticketnumber' and not(contains(@class,'header'))]//label/div";
            By eleRowBy = SeleniumExtensions.getLocator(jobIdRow);
            By jobTitleBy = SeleniumExtensions.getLocator(jobName);
            By jobIDBy = SeleniumExtensions.getLocator(jobIDLoc);


            int i = 0;
            IWebElement jobidEle, jobidEleRow, jobTitleName;
            do
            {
                String selector = $"ul[aria-label='Job Commands'] button[aria-label = '{RefreshBtn}']";
                By eleBy = SeleniumExtensions.getLocator(selector);
                try
                {
                    if(wh.elementClickableAndDisplayed(eleBy))
                    wh.clickOnElement(eleBy);
                }
                catch (Exception e)
                {
                    Thread.Sleep(2000);
                    wh.clickOnElement(eleBy);
                }
                try
                {

                    if (wh.isElementDisplayed(eleRowBy, 1))
                    {
                        jobidEleRow = wh.GetWebElement(eleRowBy);
                        isEleDisplayed = wh.isElementDisplayed(eleRowBy, 2);
                        //isTitleDisplayed = wh.elementDisplayed(jobTitleBy, 1);
                        if (isEleDisplayed)
                        {
                            if (Config.EnvironmentVal == "DEV")
                            {
                                isTitleDisplayed = wh.isElementDisplayed(jobIDBy, 3);
                                Console.WriteLine("isTitleDisplayed : " + isTitleDisplayed);
                            }
                            else
                            {
                                isTitleDisplayed = wh.isElementDisplayed(jobTitleBy, 3);
                                Console.WriteLine("isTitleDisplayed : " + isTitleDisplayed);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                }
                i++;
                if (i == 150) break;

                Console.WriteLine("Condition : " + (!isEleDisplayed && !isTitleDisplayed));

            } while (!isTitleDisplayed);

            jobDetails[jobID] = wh.GetWebElement(jobIDEleBy).Text.Trim();
            if (Config.EnvironmentVal != "DEV")
            {
                jobDetails[JobName] = wh.GetWebElement(jobTitleBy).Text.Trim();
            }
            return jobDetails;
        }

        public void clickTabOnRequestForm(String tabName, String formName)
        {
            waitHelpers wh = new waitHelpers();
            try
            {
                By eleBy = By.CssSelector($"ul[aria-label='{formName}'] li[aria-label='{tabName}'][role='tab']");
                if (wh.isElementDisplayed(eleBy, 30))
                {
                    wh.clickOnWebElement(eleBy);
                }
                else
                {
                    throw new Exception(tabName + " Tab is not visible on " + formName);
                }
            }
            catch (Exception e)
            {
                wh.elementToClickble(moreTabsJobBy);
                wh.jsClickOnElement(moreTabsJobBy);
                By customizeSelector = By.XPath($"//*[@role='menu']//*[@aria-label='{tabName}'] | //*[@role='menu']//*[text()='{tabName}']");
                wh.elementToClickble(customizeSelector);
                wh.clickOnElement(customizeSelector);

            }
        }

        public void clickchildjobs(String items)
        {
            waitHelpers wh = new waitHelpers();
            try
            {
                By eleBy = By.CssSelector($"div[aria-label='{items}'][role='menuitem']");
                wh.isElementDisplayed(eleBy, 120);
                wh.clickOnWebElement(eleBy);
            }
            catch (Exception e)
            {
                wh.elementToClickble(moreTabsJobBy);
                wh.jsClickOnElement(moreTabsJobBy);
                By customizeSelector = By.XPath($"//*[@aria-label='{items}'] | //*[text()='{items}']");
                wh.elementToClickble(customizeSelector);
                wh.clickOnElement(customizeSelector);

            }
        }

        public void clickTabOnDialog(String tabName, String formName)
        {
            try
            {
                String locator = $"ul[aria-label='{formName}'] li[aria-label='{tabName}'][role='tab']";
                By eleBy = SeleniumExtensions.getLocator(locator);
                SeleniumExtensions.waitForElementToBeDisplayed(eleBy, 120).Click();
            }
            catch (Exception e)
            {
                moreTabsDialog.ClickUsingJavascript();
                string customizeSelector = $"//*[@aria-label='tabName']";
                SeleniumExtensions.WaitForElementAndClick(By.XPath(customizeSelector));

            }
        }
        public void clickOnaddresslink()
        {
            waitHelpers wh = new waitHelpers();
            By locator = SeleniumExtensions.getLocator("//*[@col-id='voa_hereditament' and not(contains(@class,'header'))]");
            wh.GetWebElement(locator);
            wh.isElementDisplayed(locator, 120);
            IWebElement ele = wh.GetWebElementVisble(locator);
            Actions act = new Actions(DriverHelper.Driver);
            act.DoubleClick(ele).Build().Perform();
        }

        public void clickOnJobID()
        {
            waitHelpers wh = new waitHelpers();
            By locator = SeleniumExtensions.getLocator("//*[@col-id='ticketnumber' and not(contains(@class,'header'))]");
            wh.GetWebElement(locator);
            wh.isElementDisplayed(locator, 120);
            IWebElement ele = wh.GetWebElementVisble(locator);
            Actions act = new Actions(DriverHelper.Driver);
            act.DoubleClick(ele).Build().Perform();

        }

        public void navigateToRequest()
        {
            waitHelpers wh = new waitHelpers();
            By locator = SeleniumExtensions.getLocator("a[aria-label*='CT: Request'][role='link']");
            wh.GetWebElement(locator);
            wh.isElementDisplayed(locator, 120);
            wh.clickOnElement(locator);

        }

        public void clickOnRequestLinkUnderAddress()
        {
            waitHelpers wh = new waitHelpers();
            By locator = SeleniumExtensions.getLocator("div[col-id='voa_requestlineitemid'] a");
            wh.GetWebElement(locator);
            wh.isElementDisplayed(locator, 120);
            wh.clickOnElement(locator);
            //IWebElement ele = wh.GetWebElementVisble(locator);
            //Actions act = new Actions(DriverHelper.Driver);
            //act.DoubleClick(ele).Build().Perform();

        }

        public void clickOnCorrespondenceJobID()
        {
            waitHelpers wh = new waitHelpers();
            wh.clickOnElement(correspondencejobIDEleBy);
        }

        public void clickOnMsgJobID()
        {
            waitHelpers wh = new waitHelpers();
            wh.clickOnElement(msgJobIDEleBy);
        }

        public void EnterDetailsInCategorisationSectionforCR10(string requestType, string jobType)
        {
            jobType = "Material Increase";
            //By RTFirstValue = GetFirstLookUp(requestType);
            //SeleniumExtensions.EnterInLookUpField(RTFirstValue, RequestTypeLookupField, RequestTypeSearchBtn, requestType);
            By JTFirstValue = GetFirstLookUp(jobType);
            SeleniumExtensions.EnterInLookUpField(JTFirstValue, JobTypeLookUpTextFieldSlector, JobTypeSearchBtn, jobType);

        }


        public void clickOnNewlyCreatedJobID()
        {
            jobIDEle.DoubleClickElementUsingJSExecutor();
        }

        public void CreateandValidateNewProposedBAReference(string date, string BAVal, string BARefVal, string PostCode)
        {
            NewProposeBAReference.ClickUsingJavascript();
            var ExpBillAuthority = BillingAuthorityValue.GetAttribute("title").ToString().Trim();
            Assert.AreEqual(ExpBillAuthority, BAVal, $"The Billing Authority number is {BAVal}");
            var ExpectedBARefNO = ExpProposedBARefValue.GetAttribute("title").ToString().Trim();
            Assert.AreEqual(ExpectedBARefNO, BARefVal, $"The BA Reference number is {BARefVal}");
            var ExpPostcodeval = ExpPostcode.GetAttribute("value").ToString().Trim();
            if (ExpPostcodeval.Contains(PostCode))
            {
                Log.Information($"The Postcode is matching the BA ref postcode");
            }
            var ExpDate = ExpProposedEffectiveDateValue.GetAttribute("value").ToString().Trim();
            //Assert.AreEqual(ExpDate, date, $"The date in BA Reference is {date}");
            ClickCommandBarOption("Save & Close");

        }

        public void ValidateDetailsOnBARefLinksTabAndSave(string BAVal, string BARefVal)
        {
            NewProposeBAReference.WaitUntilElementAttached(5);
            NewProposeBAReference.ClickUsingJavascript();
            Thread.Sleep(3000);
            var ExpBillAuthority = BillingAuthorityValue.GetAttribute("title").ToString().Trim();
            Assert.AreEqual(ExpBillAuthority, BAVal, $"The Billing Authority number is {BAVal}");
            Thread.Sleep(3000);
            var ExpectedBARefNO = ExpProposedBARefValue.GetAttribute("title").ToString().Trim();
            Assert.AreEqual(BARefVal, ExpectedBARefNO, $"The BA Reference number is {BARefVal}");
            //ClickCommandBarOption("Save & Close");
            ClickSaveAndCloseOnDialogforValidateRequest();
        }
        public void ValidateDetailsOnBARefLinksTabAndSwapHereditament(string BA, string BARefVal, string town, string postcode, string uprn)
        {
            NewProposeBAReference.ClickUsingJavascript();
            var ExpBillAuthority = BillingAuthorityValue.GetAttribute("title").ToString().Trim();
            Assert.AreEqual(ExpBillAuthority, BA, $"The Billing Authority number is {BA}");
            var ExpectedBARefNO = ExpProposedBARefValue.GetAttribute("title").ToString().Trim();
            Assert.AreEqual(BARefVal, ExpectedBARefNO, $"The BA Reference number is {BARefVal}");
            FindHereditamentForSwapHereditament(town, postcode, uprn);
            ClickCommandBarOptionOnDialog("Save & Close");
        }

        public void GetDetailsfromSummarytab(out string BillingAuthority, out string ExisitingBillingAuthorityReference)
        {
            BillingAuthority = BAValue.Text.Trim();
            Assert.IsTrue(BAValue.IsElementDisplayed(3), " BillingAuthority is not displayed");
            ExisitingBillingAuthorityReference = BAReferenceValue.GetAttribute("value").ToString();
        }

        public void ValidateProposedBAReferenceAmendments(string BARefVal, string PostCode)
        {
            int findRowsTries = 0;
            bool validateFlag = false;
            bool findrowsFlag = false;
            IList<IWebElement> RowsElement = Driver.FindElements(MaterialRequestRows);
            if (RowsElement.Count == 0)
            {
                while (RowsElement.Count == 0 && findRowsTries < 3)
                {
                    ClickCommandBarOption("Refresh");
                    ValidateAndClickOnTab("BA Reference", "Desktop Research Form");
                    RowsElement = Driver.FindElements(MaterialRequestRows);
                    if (RowsElement.Count > 0)
                    {
                        findrowsFlag = true;
                        break;
                    }
                    findRowsTries++;
                }
                if (findrowsFlag == false)
                {
                    Assert.Fail("No Proposed BA Reference Amendments detected");
                    Log.Error("No Proposed BA Reference Amendments detected");
                }


            }

            foreach (var row in RowsElement)
            {
                string rowindex = row.GetAttribute("row-index");
                IWebElement ProposedBARefNumber = DriverHelper.Driver.WaitForElement(By.XPath($"//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row') and @row-index='{rowindex}']//*[contains(@col-id,'voa_proposedbareferencenumber')]//label"));
                IWebElement ProposedPostCode = DriverHelper.Driver.WaitForElement(By.XPath($"//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row') and @row-index='{rowindex}']//*[contains(@col-id,'voa_hereditamentid')]//label"));
                string ExpProposedBARefNumber = ProposedBARefNumber.GetAttribute("aria-label");
                string ExpProposedPostCode = ProposedPostCode.GetAttribute("aria-label");
                if (ExpProposedBARefNumber != BARefVal)
                {
                    while (ExpProposedBARefNumber != BARefVal && !ExpProposedPostCode.Contains(PostCode))
                    {
                        ExpProposedBARefNumber = ProposedBARefNumber.GetAttribute("aria-label");
                        ExpProposedPostCode = ProposedPostCode.GetAttribute("aria-label");
                        if (ExpProposedBARefNumber == BARefVal && ExpProposedPostCode.Contains(PostCode))
                        {
                            validateFlag = true;
                            break;
                        }

                    }
                }
                else
                {
                    validateFlag = true;

                }
                if (validateFlag == true)
                {
                    Assert.AreEqual(BARefVal, ExpProposedBARefNumber, $"The  status has been validated as {ExpProposedBARefNumber} within the max tries");
                    Assert.AreEqual(PostCode, ExpProposedPostCode, $"The  Postcode has been validated as {ExpProposedPostCode} within the max tries");
                }
                else
                {
                    Assert.Fail("The user is unable find the BA Reference Number " + ExpProposedBARefNumber);
                    Assert.Fail("The user is unable find the Postcode " + ExpProposedPostCode);
                }
            }
        }

        public void SelectSupplementaryJobType(string jobType)
        {
            string proposedEffectiveDateChanged = DateTime.Now.AddDays(-2).ToString("M/d/yyyy");
            By JTFirstValue = GetFirstLookUpValue(jobType);
            SeleniumExtensions.EnterInLookUpField(JTFirstValue, SupplementaryJobType, SupplementaryJobTypeSearchBtn, jobType);
            PickProposedDateForDialogBP(proposedEffectiveDateChanged);
            SupplementaryJobSave.ClickUsingJavascript();
            SupplementaryJobSubmit.ClickUsingJavascript();
            SupplementaryJobContinue.ClickUsingJavascript();
            SupplementaryJobClose.ClickUsingJavascript();
            Thread.Sleep(3000);
        }

        public void ValidateJobTypeforAssociatedJobsTab(string JobValue)
        {
            int findRowsTries = 0;
            bool validateFlag = false;
            bool findrowsFlag = false;
            IList<IWebElement> RowsElement = Driver.FindElements(MaterialRequestRows);
            if (RowsElement.Count == 0)
            {
                while (RowsElement.Count == 0 && findRowsTries < 3)
                {
                    ClickCommandBarOption("Refresh");
                    ValidateAndClickOnTab("Associated Jobs", "Desktop Research Form");
                    RowsElement = Driver.FindElements(MaterialRequestRows);
                    if (RowsElement.Count > 0)
                    {
                        findrowsFlag = true;
                        break;
                    }
                    findRowsTries++;
                }
                if (findrowsFlag == false)
                {
                    Assert.Fail("No Associated Jobs created");
                    // Log.Error("");
                }


            }

            foreach (var row in RowsElement)
            {
                string rowindex = row.GetAttribute("row-index");
                IWebElement JobType = DriverHelper.Driver.WaitForElement(By.XPath($"//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row') and @row-index='{rowindex}']//*[contains(@col-id,'voa_codedreason')]//a"));

                string actJobtypeText = JobType.GetAttribute("aria-label");
                if (actJobtypeText != JobValue)
                {
                    while (actJobtypeText != JobValue)
                    {
                        actJobtypeText = JobType.GetAttribute("aria-label");
                        if (actJobtypeText == JobValue)
                        {
                            validateFlag = true;
                            break;
                        }

                    }
                }
                else
                {
                    validateFlag = true;

                }
                if (validateFlag == true)
                {
                    Assert.AreEqual(JobValue, actJobtypeText, $"The  Job Type has been validated as {actJobtypeText} within the max tries");
                    // Log.Information($"");
                }
                else
                {
                    Assert.Fail("The Associated jobs validation is failed with status as " + actJobtypeText);
                    //Log.Error("CR10 requests are not available" + actRuntimeStatusText);


                }
                break;
            }
        }

        public void UserValidatestheBillingAuthorityTableValues(string tabName, string ActBillingAuthority, string ActBARefValue, string ActPostCode, string ActEffectiveChangedDate)
        {
            int findRowsTries = 0;
            bool validateFlag = false;
            bool findrowsFlag = false;
            IList<IWebElement> RowsElement = Driver.FindElements(MaterialRequestRows);
            if (RowsElement.Count == 0)
            {
                while (RowsElement.Count == 0 && findRowsTries < 3)
                {
                    ClickCommandBarOption("Refresh");
                    var requestPage = new RequestPage();
                    requestPage.ClickOnOptionInRelatedTab(tabName);
                    RowsElement = Driver.FindElements(MaterialRequestRows);
                    if (RowsElement.Count > 0)
                    {
                        findrowsFlag = true;
                        break;
                    }
                    findRowsTries++;
                }
                if (findrowsFlag == false)
                {
                    Assert.Fail("No Billing Authority Values displayed on the table");
                    Log.Error("No Billing Authority Values displayed on the table");
                }
            }
            foreach (var row in RowsElement)
            {
                string rowindex = row.GetAttribute("row-index");
                int rows = Int32.Parse(rowindex);
                //IWebElement BillingAuthority = UIBillingAuthorityText(rows);
                //IWebElement BillingAuthorityReference = UIBillingAuthorityReferenceText(rows);
                //IWebElement EffectiveFrom = UIEffectiveFromText(rows);
                //IWebElement EffectiveTo = UIEffectiveToText(rows); 
                //IWebElement StatusId = UIStatusIdText(rows);

                //string actBillingAuthorityText = BillingAuthority.GetAttribute("aria-label");
                //string actBillingAuthorityReferenceText = BillingAuthorityReference.GetAttribute("aria-label");
                //string actEffectiveFromText = EffectiveFrom.GetAttribute("aria-label");
                //string actEffectiveToText = EffectiveTo.GetAttribute("aria-label");
                //string actStatusIdText = StatusId.GetAttribute("aria-label");
                //SelectEfftoDatedropdown.ClickUsingJavascript();
                //SelectEfftoDatedropdownOption.ClickUsingJavascript();
                //if (rows == 0)
                //{
                //    if (actBillingAuthorityText == ActBillingAuthority && actBillingAuthorityReferenceText != ActBARefValue && actEffectiveFromText != ActEffectiveChangedDate && actEffectiveToText == ActEffectiveChangedDate && actStatusIdText == "Committed")
                //    {
                //        Log.Information($"The New Created BA Reference is {actBillingAuthorityReferenceText}");
                //        Log.Information($"The Effective from date is {actEffectiveFromText}");
                //        validateFlag = true;
                //        break;
                //    }
                //}
                //else if (rows == 1)
                //{
                //    if (actBillingAuthorityText == ActBillingAuthority && actBillingAuthorityReferenceText == ActBARefValue && actEffectiveFromText == ActEffectiveChangedDate && actEffectiveToText == "" && actStatusIdText == "Committed")
                //    {
                //        validateFlag = true;
                //        break;

                //    }
                //}
                //    if (validateFlag == true)
                //{
                //    Assert.AreEqual(actStatusIdText,"Committed", $"The  status has been validated as {actStatusIdText} within the max tries");
                //    Assert.AreEqual(actBillingAuthorityText, ActBillingAuthority, $"The Billing authority has been validated as {ActBillingAuthority} within the max tries");
                //    Log.Information($"Billing Authority References are available on the UI");
                //}
                //else
                //{
                //    Assert.Fail("The Billing Authority Reference has Been Failed");
                //    Log.Error("Billing Authority References are not available on the UI");


                //}
            }
        }

        public void enterPostcodeForNewProperty(string postCodeValue)
        {
            AddressSearchInputOnDialog.ClearAndSendkeys(postCodeValue);

        }

        public String GetUniqueAddressForProperty(string postCode, FeatureContext FC)
        {
            FindAddress.ClickUsingJavascript();
            Assert.IsTrue(AddressSearchInputOnDialog.ElementVisisbleUsingExplicitWait(5), "The Address Search Dialog does not appear");
            AddressSearchInputOnDialog.ClearAndSendkeys(postCode);
            BtnSearchOnDialog.ClickUsingJavascript();
            String addressValue = "";
            String uprnxpath = "";
            String uprnvalue = "";
            bool found = false;
            waitHelpers wh = new waitHelpers();
            IList<IWebElement> AddressesRows = wh.getAllWebElements(AddressRow);

            while (!found)
            {
                int uniqueAddrNum = 0;
                foreach (var row in AddressesRows)
                {
                    row.ElementVisisbleUsingExplicitWait(10);
                    uniqueAddrNum = uniqueAddrNum + 1;
                    String addressXpath = $"(//*[@data-automation-key='addressString']//div[contains(@class,'ms-TooltipHost root')])[{uniqueAddrNum}]";
                    addressValue = DriverHelper.Driver.FindElement(By.XPath(addressXpath)).Text.Trim();
                    uprnxpath = $"(//*[@data-automation-key='uprn']//div[contains(@class,'ms-TooltipHost root')])[{uniqueAddrNum}]";
                    uprnvalue = DriverHelper.Driver.FindElement(By.XPath(uprnxpath)).Text.Trim();
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

                    FC["uprn"] = uprnvalue;
                    break;
                }
                else
                {
                    if (NextPageOnDialog.IsEnabled())
                    {
                        NextPageOnDialog.ClickUsingJavascript();
                        SeleniumExtensions.WaitForPageLoad();
                        AddressesRows = wh.getAllWebElements(AddressRow);
                    }
                    else
                    {
                        throw new Exception($"No Unique Address found for {postCode} used in test data,please update test data");
                        addressValue = "";
                        break;
                    }
                }
            }
            return addressValue;
        }

        public String GetUsedAddressForPropertyForCOA()
        {
            waitHelpers wh = new waitHelpers();
            //BtnSearchOnDialog.ClickUsingJavascript();
            String addressValue = "";
            String uprnxpath = "";
            String uprnvalue = "";
            List<string> notDuplicateUPRNs = new List<string>();

            while (true)
            {
                string findAddrDialog = $"//div[contains(@id,'ModalFocusTrap')]";
                //IList<IWebElement> AddressesRows = Driver.FindElements(AddressRow);
                
                IList<IWebElement> AddressesRows = wh.getAllWebElements(Research_AddressRow);
                bool found = false;
                int uniqueAddrNum = 0;
                for (int j = 1; j < AddressesRows.Count(); j++)
                {
                    var row = AddressesRows[j];
                    row.ElementVisisbleUsingExplicitWait(10);
                    uniqueAddrNum = uniqueAddrNum + 1;
                    String addressXpath = $"(//*[@data-automation-key='addressString']//div[contains(@class,'ms-TooltipHost root')])[{uniqueAddrNum}]";
                    addressValue = DriverHelper.Driver.FindElement(By.XPath(addressXpath)).Text.Trim();
                    uprnxpath = $"(//*[@data-automation-key='uprn']//div[contains(@class,'ms-TooltipHost root')])[{uniqueAddrNum}]";
                    uprnvalue = DriverHelper.Driver.FindElement(By.XPath(uprnxpath)).Text.Trim();
                    if (notDuplicateUPRNs.Contains(uprnvalue))
                    {
                    }
                    else
                    {
                        row.ClickUsingJavascript();

                    }
                    BtnUseAddressOnDialog.ClickUsingJavascript();

                    if (DuplicateAddressHeader.IsElementVisibleUsingByLocator(10))
                    {
                        found = true;
                        break;
                    }
                    else
                    {
                        found = false;
                        notDuplicateUPRNs.Add(uprnvalue);
                        if (!(wh.isElementDisplayed(By.XPath(findAddrDialog), 10)))
                        {
                            SeleniumExtensions.scrollToBtnElementAndClick("Find Address");
                            BtnSearchOnDialog.ClickUsingJavascript();
                            Thread.Sleep(4000);
                            AddressesRows = Driver.FindElements(AddressRow);
                        }
                        continue;
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
                    Console.WriteLine("No used Address Found");
                    addressValue = "";
                    break;
                }
            }
            return addressValue;
        }


        public String GetUniqueAddressForPropertyForCOA()
        {
            waitHelpers wh = new waitHelpers();
            wh.GetWebElement(BtnSearchOnDialogBy).ClickUsingJavascript();
            String addressValue = "";
            String uprnxpath = "";
            String uprnvalue = "";
            List<string> notDuplicateUPRNs = new List<string>();
            bool found = false;

            while (!found)
            {
                string findAddrDialog = $"//div[contains(@id,'ModalFocusTrap')]";
                IList<IWebElement> AddressesRows = wh.getAllWebElements(Research_AddressRow);
                int uniqueAddrNum = 0;
                for (int j = 0; j < AddressesRows.Count(); j++)
                {
                    var row = AddressesRows[j];
                    row.ElementVisisbleUsingExplicitWait(10);
                    uniqueAddrNum = uniqueAddrNum + 1;
                    String addressXpath = $"(//*[@data-automation-key='addressString']//div[contains(@class,'ms-TooltipHost root')])[{uniqueAddrNum}]";
                    addressValue = DriverHelper.Driver.FindElement(By.XPath(addressXpath)).Text.Trim();
                    uprnxpath = $"(//*[@data-automation-key='uprn']//div[contains(@class,'ms-TooltipHost root')])[{uniqueAddrNum}]";
                    uprnvalue = DriverHelper.Driver.FindElement(By.XPath(uprnxpath)).Text.Trim();
                    if (notDuplicateUPRNs.Contains(uprnvalue))
                    {
                    }
                    else
                    {
                        wh.clickOnElement(row);
                    }
                    BtnUseAddressOnDialog.ClickUsingJavascript();

                    if (DuplicateAddressHeader.IsElementVisibleUsingByLocator(5))
                    {
                        found = false;
                        notDuplicateUPRNs.Add(uprnvalue);
                        SeleniumExtensions.scrollToBtnBasedOnLabelAndClick("Find another Address");
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
                    Console.WriteLine("No unique Address Found");
                    addressValue = "";
                    break;
                }
            }
            return addressValue;
        }

        public String GetUniqueAddressForPropertyForCOA_AP()
        {
            waitHelpers wh = new waitHelpers();
            BtnSearchOnDialog.ClickUsingJavascript();
            Thread.Sleep(5000);
            String addressValue = "";
            String uprnxpath = "";
            String uprnvalue = "";
            List<string> notDuplicateUPRNs = new List<string>();
            bool found = false;

            while (!found)
            {
                string findAddrDialog = $"//div[contains(@id,'ModalFocusTrap')]";
                IList<IWebElement> AddressesRows = Driver.FindElements(Research_AddressRow);
                int uniqueAddrNum = 0;
                for (int j = 0; j < AddressesRows.Count(); j++)
                {
                    var row = AddressesRows[j];
                    row.ElementVisisbleUsingExplicitWait(10);
                    uniqueAddrNum = uniqueAddrNum + 1;
                    String addressXpath = $"(//*[@data-automation-key='addressString']//div[contains(@class,'ms-TooltipHost root')])[{uniqueAddrNum}]";
                    addressValue = DriverHelper.Driver.FindElement(By.XPath(addressXpath)).Text.Trim();
                    uprnxpath = $"(//*[@data-automation-key='uprn']//div[contains(@class,'ms-TooltipHost root')])[{uniqueAddrNum}]";
                    uprnvalue = DriverHelper.Driver.FindElement(By.XPath(uprnxpath)).Text.Trim();
                    if (notDuplicateUPRNs.Contains(uprnvalue))
                    {
                    }
                    else
                    {
                        wh.clickOnElement(row);

                        //row.C();

                    }
                    BtnUseAddressOnDialog.ClickUsingJavascript();

                    if (DuplicateAddressHeader.IsElementVisibleUsingByLocator(5))
                    {
                        found = false;
                        notDuplicateUPRNs.Add(uprnvalue);
                        SeleniumExtensions.scrollToBtnBasedOnLabelAndClick("Find another Address");
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
                    Console.WriteLine("No unique Address Found");
                    addressValue = "";
                    break;
                }
            }
            return addressValue;
        }



        public String GetUniqueAddressForEsates(string postCode, FeatureContext FC)
        {
            String addressValue = "";
            String uprnxpath = "";
            String uprnvalue = "";
            waitHelpers wh = new waitHelpers();
            bool found = false;
            bool searchForAddr = false;
            List<string> alreadyVerfiedLi = new List<string>();
            IList<IWebElement> AddressesRows = new List<IWebElement>();
            int uniqueAddrNum = 0;
            int PageNum = 0;
        outerLoop: while (!found)
            {
                if (!searchForAddr)
                {
                    wh.GetWebElement(estatesFindAddress).ClickUsingJavascript();
                    Assert.IsTrue(AddressSearchInputOnDialog.ElementVisisbleUsingExplicitWait(5), "The Address Search Dialog does not appear");
                    AddressSearchInputOnDialog.ClearAndSendkeys(postCode);
                    BtnSearchOnDialog.ClickUsingJavascript();
                    searchForAddr = true;
                    AddressesRows = wh.getAllWebElements(AddressRow);
                }
                //Driver.FindElements(AddressRow);

                if (AddressesRows.Count == uniqueAddrNum)
                {
                    if (NextPageOnDialog.IsEnabled())
                    {
                        if (!wh.isElementDisplayed(By.XPath("//button[@title='Next Page' and @disabled]"), 60))
                        {
                            NextPageOnDialog.ClickUsingJavascript();
                            Thread.Sleep(3000);
                            AddressesRows = wh.getAllWebElements(AddressRow);
                            uniqueAddrNum = 1;
                            PageNum = PageNum + 1;
                        }
                        else
                        {
                            Console.WriteLine("No Unique Address Found");
                            addressValue = "";
                            Assert.IsTrue(false, "No Unique Address Found with given postcode :" + postCode + " , Please check Test data for this specific scenario");
                            //return "No Unique Address Found";
                        }
                    }

                }

                foreach (var row in AddressesRows)
                {

                    row.ElementVisisbleUsingExplicitWait(10);
                    uniqueAddrNum = uniqueAddrNum + 1;
                    String addressXpath = $"(//*[@data-automation-key='addressString']//div[contains(@class,'ms-TooltipHost root')])[{uniqueAddrNum}]";
                    addressValue = wh.GetWebElement(By.XPath(addressXpath)).Text.Trim();
                    if (!alreadyVerfiedLi.Contains(addressValue))
                    {
                        //DriverHelper.Driver.FindElement(By.XPath(addressXpath)).Text.Trim();
                        uprnxpath = $"(//*[@data-automation-key='uprn']//div[contains(@class,'ms-TooltipHost root')])[{uniqueAddrNum}]";
                        uprnvalue = wh.GetWebElement(By.XPath(uprnxpath)).Text.Trim();
                        //DriverHelper.Driver.FindElement(By.XPath(uprnxpath)).Text.Trim();
                        string estateFilePath = $"(//*[@data-automation-key='uprn']//div[contains(@class,'ms-TooltipHost root')])[{uniqueAddrNum}]//ancestor::div[@role='row']//div[@data-automation-key='estatefile']/div";
                        //if (!wh.isElementDisplayedInMilliSec(SeleniumExtensions.getLocator(estateFilePath), 100))
                        //{

                        if (wh.getAllWebElements(By.XPath(estateFilePath)).Count > 0)
                        {
                            continue;
                        }

                        else
                        {
                            bool isUseAddressDisplayed = false;
                            int counter = 0;
                            while (!isUseAddressDisplayed)
                            {
                                wh.jsClickOnElement(SeleniumExtensions.getLocator(addressXpath));
                                //.ClickUsingJavascript();
                                isUseAddressDisplayed = wh.isElementDisplayed(BtnUseAddressOnDialogBy, 3);
                                counter = counter + 1;
                                if (isUseAddressDisplayed || counter >= 50) break;
                            }

                            BtnUseAddressOnDialog.ClickUsingJavascript();
                            if (DuplicateAddressForEstate.IsElementVisibleUsingByLocator(5))
                            {
                                wh.GetWebElement(okBtnOnModalDialog).ClickUsingJavascript();
                                alreadyVerfiedLi.Add(addressValue);
                                addressValue = "";
                                found = false;
                                break;
                            }
                            else
                            {
                                found = true;
                                break;
                            }
                        }
                        //}
                    }
                }
                if (found)
                {

                    FC["uprn"] = uprnvalue;
                    break;
                }

            }
            return addressValue;
        }

        public void GetUniqueAddressForEsates_TestData(string postCode, FeatureContext fc)
        {
            waitHelpers wh = new waitHelpers();
            By headerCheckBox = By.XPath("//div[contains(@id,'header') and @role='checkbox']/div[contains(@class,'ms-Check')]");
            wh.GetWebElement(estatesFindAddress).ClickUsingJavascript();
            wh.GetWebElement(AddressSearchInputOnDialogBy).ClearAndSendkeys(postCode);
            wh.GetWebElement(BtnSearchOnDialogBy).ClickUsingJavascript();
            IList<IWebElement> AddressesRows = wh.getAllWebElements(AddressRow);
            wh.GetWebElement(headerCheckBox).ClickUsingJavascript();
            wh.GetWebElement(BtnUseAddressOnDialogBy).ClickUsingJavascript();
            Thread.Sleep(25000);
            if (wh.isElementDisplayed(DuplicateAddressForEstate, 240))
            {
                wh.GetWebElement(okBtnOnModalDialog).ClickUsingJavascript();
            }
            Thread.Sleep(15000);
            By numberOfPlotsCountLoc = By.CssSelector("div[class='ag-center-cols-viewport'] div[role='row']");
            int addressRowCount = 0;
            try
            {
                wh.GetWebElement(numberOfPlotsCountLoc);
                addressRowCount = DriverHelper.Driver.FindElements(numberOfPlotsCountLoc).Count;
            }
            catch (Exception e)
            {
                addressRowCount = 0;
            }
            if (addressRowCount <= 1)
            {
                Assert.IsTrue(false, "Seems no Address addeded, please check the provided test data Postcode : " + postCode);
            }
            else { fc["numberOfPlots"] = Convert.ToString(addressRowCount); }

        }
        public void GetUniqueMultipleAddressForEsates(string postCode)
        {
            String addressValue = "";
            waitHelpers wh = new waitHelpers();
            bool found = false;
            List<string> alreadyVerfiedLi = new List<string>();

            while (!found)
            {
                wh.GetWebElement(estatesFindAddress).ClickUsingJavascript();
                wh.GetWebElement(AddressSearchInputOnDialogBy).ClearAndSendkeys(postCode);
                wh.GetWebElement(BtnSearchOnDialogBy).ClickUsingJavascript();
                IList<IWebElement> AddressesRows = wh.getAllWebElements(AddressRow);
                int uniqueAddrNum = 0;
                foreach (var row in AddressesRows)
                {
                    if ((AddressesRows.Count - 1) == uniqueAddrNum)
                    {
                        if (wh.GetWebElement(NextPageOnDialogBy).IsEnabled())
                        {
                            wh.GetWebElement(NextPageOnDialogBy).ClickUsingJavascript();
                            uniqueAddrNum = 1;
                        }
                        else
                        {
                            Console.WriteLine("No Unique Address Found");
                            addressValue = "";
                            break;
                        }
                    }

                    //row.ElementVisisbleUsingExplicitWait(10);
                    wh.elementDisplayed(row, 5);
                    uniqueAddrNum = uniqueAddrNum + 1;
                    String addressXpath = $"(//*[@data-automation-key='addressString']//div[contains(@class,'ms-TooltipHost root')])[{uniqueAddrNum}]";
                    addressValue = wh.GetWebElement(By.XPath(addressXpath)).Text.Trim();
                    if (!alreadyVerfiedLi.Contains(addressValue))
                    {
                        //DriverHelper.Driver.FindElement(By.XPath(addressXpath)).Text.Trim();
                        //uprnxpath = $"(//*[@data-automation-key='uprn']//div[contains(@class,'ms-TooltipHost root')])[{uniqueAddrNum}]";
                        //DriverHelper.Driver.FindElement(By.XPath(uprnxpath)).Text.Trim();
                        string estateFilePath = $"(//*[@data-automation-key='uprn']//div[contains(@class,'ms-TooltipHost root')])[{uniqueAddrNum}]//ancestor::div[@role='row']//div[@data-automation-key='estatefile']/div";
                        if (DriverHelper.Driver.FindElements(By.XPath(estateFilePath)).Count > 0)
                        {
                            continue;
                        }
                        else
                        {
                            row.ClickUsingJavascript();
                            wh.GetWebElement(BtnUseAddressOnDialogBy).ClickUsingJavascript();
                            if (wh.isElementDisplayed(DuplicateAddressForEstate, 2))
                            //.IsElementVisibleUsingByLocator(5))
                            {
                                wh.GetWebElement(okBtnOnModalDialog).ClickUsingJavascript();
                                alreadyVerfiedLi.Add(addressValue);
                                addressValue = "";
                                found = false;
                                break;
                            }
                            else
                            {
                                found = true;
                                break;
                            }
                        }
                    }
                }
            }
        }


        public void GetUniqueMultipleAddressForEsates_New(string postCode)
        {
            String addressValue = "";
            waitHelpers wh = new waitHelpers();
            bool found = false;
            List<string> alreadyVerfiedLi = new List<string>();

            while (!found)
            {
                wh.GetWebElement(estatesFindAddress).ClickUsingJavascript();
                wh.GetWebElement(AddressSearchInputOnDialogBy).ClearAndSendkeys(postCode);
                wh.GetWebElement(BtnSearchOnDialogBy).ClickUsingJavascript();
                IList<IWebElement> AddressesRows = wh.getAllWebElements(AddressRow);
                int uniqueAddrNum = 0;
                foreach (var row in AddressesRows)
                {
                    if ((AddressesRows.Count - 1) == uniqueAddrNum)
                    {
                        if (wh.GetWebElement(NextPageOnDialogBy).IsEnabled())
                        {
                            wh.GetWebElement(NextPageOnDialogBy).ClickUsingJavascript();
                            uniqueAddrNum = 1;
                        }
                        else
                        {
                            Console.WriteLine("No Unique Address Found");
                            addressValue = "";
                            break;
                        }
                    }

                    //row.ElementVisisbleUsingExplicitWait(10);
                    wh.elementDisplayed(row, 5);
                    uniqueAddrNum = uniqueAddrNum + 1;
                    String addressXpath = $"(//*[@data-automation-key='addressString']//div[contains(@class,'ms-TooltipHost root')])[{uniqueAddrNum}]";
                    addressValue = wh.GetWebElement(By.XPath(addressXpath)).Text.Trim();
                    if (!alreadyVerfiedLi.Contains(addressValue))
                    {
                        //DriverHelper.Driver.FindElement(By.XPath(addressXpath)).Text.Trim();
                        //uprnxpath = $"(//*[@data-automation-key='uprn']//div[contains(@class,'ms-TooltipHost root')])[{uniqueAddrNum}]";
                        //DriverHelper.Driver.FindElement(By.XPath(uprnxpath)).Text.Trim();
                        string estateFilePath = $"(//*[@data-automation-key='uprn']//div[contains(@class,'ms-TooltipHost root')])[{uniqueAddrNum}]//ancestor::div[@role='row']//div[@data-automation-key='estatefile']/div";
                        if (DriverHelper.Driver.FindElements(By.XPath(estateFilePath)).Count > 0)
                        {
                            continue;
                        }
                        else
                        {
                            row.ClickUsingJavascript();
                            wh.GetWebElement(BtnUseAddressOnDialogBy).ClickUsingJavascript();
                            if (wh.isElementDisplayed(DuplicateAddressForEstate, 2))
                            //.IsElementVisibleUsingByLocator(5))
                            {
                                wh.GetWebElement(okBtnOnModalDialog).ClickUsingJavascript();
                                alreadyVerfiedLi.Add(addressValue);
                                addressValue = "";
                                found = false;
                                break;
                            }
                            else
                            {
                                found = true;
                                break;
                            }
                        }
                    }
                }
            }
        }






        public String GetUniqueAddressForProperty_Estate(string postCode)
        {
            AddressSearchInputOnDialog.ClearAndSendkeys(postCode);
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



        public Dictionary<String, String> UserCaptureFieldTitleAtributeDetails(String fieldName)
        {
            Dictionary<String, String> fieldDetails = new Dictionary<string, string>();
            String fieldlocatorStr = $"//*[contains(@title,'{fieldName}')]/parent::*/following-sibling::*//input";
            By fieldLocator = SeleniumExtensions.getLocator(fieldlocatorStr);
            String fieldValue = DriverHelper.Driver.FindElement(fieldLocator).GetAttribute("title").Trim();
            int length = fieldValue.Trim().Length;
            String finalFieldValue = fieldValue.Substring(1);
            fieldDetails[fieldName] = finalFieldValue.Trim();
            return fieldDetails;
        }

        public Dictionary<String, String> UserCaptureInPutFieldTitleAtributeDetails(String fieldName)
        {
            Dictionary<String, String> fieldDetails = new Dictionary<string, string>();
            String fieldlocatorStr = $"//input[@aria-label='{fieldName}']";
            By fieldLocator = SeleniumExtensions.getLocator(fieldlocatorStr);
            String fieldValue = DriverHelper.Driver.FindElement(fieldLocator).GetAttribute("title").Trim();
            fieldDetails[fieldName] = fieldValue.Trim();
            return fieldDetails;
        }

        public Dictionary<String, String> UserCaptureInPutFieldAtributeDetails(String fieldName, String attribute)
        {
            waitHelpers wh = new waitHelpers();
            Dictionary<String, String> fieldDetails = new Dictionary<string, string>();
            String fieldlocatorStr = $"//input[contains(@aria-label,'{fieldName}')]";
            By fieldLocator = SeleniumExtensions.getLocator(fieldlocatorStr);
            String fieldValue = wh.GetWebElement(fieldLocator).GetAttribute(attribute).Trim();
            fieldDetails[fieldName] = fieldValue.Trim();
            return fieldDetails;
        }

        public Dictionary<String, String> UserCaptureTextAreaFieldTitleAtributeDetails(String fieldName)
        {
            waitHelpers wh = new waitHelpers();
            Dictionary<String, String> fieldDetails = new Dictionary<string, string>();
            String fieldlocatorStr = $"//textarea[@aria-label='{fieldName}']";
            By fieldLocator = SeleniumExtensions.getLocator(fieldlocatorStr);
            String fieldValue = wh.GetWebElement(fieldLocator).Text;
            fieldDetails[fieldName] = fieldValue.Trim();
            return fieldDetails;
        }


        public void ValidateInspectionJob(string jobID)
        {
            waitHelpers wh = new waitHelpers();
            string actInspectionJobID = "";
            bool eleDisplayed = wh.elementDisplayed(InspectionJobIDEle, 2);
            //SeleniumExtensions.WaitForElementToDisplayed(InspectionJobIDEle, 10).Displayed;
            int i = 0;
            do
            {
                InspectionJobRefresh.ClickUsingJavascript();
                eleDisplayed = wh.elementDisplayed(InspectionJobIDEle, 2);
                //SeleniumExtensions.WaitForElementToDisplayed(InspectionJobIDEle, 10).Displayed;
                i++;
                if (i == 10)
                    break;
            } while (eleDisplayed);
            actInspectionJobID = InspectionJobIDEle.GetAttribute("title");
            Assert.IsTrue(actInspectionJobID.Contains(jobID), "The inspections job id " + actInspectionJobID + " not contains parent job id " + jobID + "");
        }

        public void ValidateUserDetailsandSelectNewValue()
        {
            Thread.Sleep(5000);
            Log.Information("User selected Approvals option from the More Options menu");
            ClickCommandBarOption("Assign");
            Log.Information("User clicked on Assign option from the Commandbar");
            IWebElement ButtonToclick = ClickButtonOnAssignDialog("Assign");
            ButtonToclick.ClickUsingJavascript();
            Log.Information("User clicked on the Assign button on the dialog");
        }

        public void clickAddExisitingRequest(string fieldName, string NameValue, string relatioshipValue, Dictionary<string, string> testdata)
        {

            SeleniumExtensions.scrollToBtnBasedOnLabelAndClick(fieldName);
            NewRequestPropertyLinkSearch.ClickUsingJavascript();
            NewRequestPropertyLink.ClickUsingJavascript();

            SeleniumExtensions.ClearAndSendkeys(NameLocator, NameValue);
            //PropertyLinkLabel.DoubleClickElementUsingJSExecutor();
            //PropertyLinklookup.ClickUsingJavascript();
            ClickonSaveandCloseDialog.ClickUsingJavascript();
            NewRequestPropertyLinklookupSearch.ClickUsingJavascript();
            NewPropertyLink.ClickUsingJavascript();
            Thread.Sleep(2000);
            if (LeavePage.IsElementDisplayed(5))
            {
                OkButtonOnDialog.ClickUsingJavascript();

            }

            EnterMandatoryFieldsOnNewPropertyLink(testdata["SubmittedBy"], relatioshipValue);
            ToggleValidationSection(testdata["ValidatePADCode"], relatioshipValue);
            ClickCommandBarOption("Save");
            SeleniumExtensions.ScrollToElement(DuprIDElement);
            string DuprId = "";
            DuprId = DuprID.GetAttribute("value");
            if (relatioshipValue == "Owner")
            {
                testdata["OwnerDuprID"] = DuprId;
            }
            else if (relatioshipValue == "Agent")
            {
                testdata["AgentDuprID"] = DuprId;
            }

            ClickCommandBarOption("Save & Close");
        }



        public void EnterMandatoryFieldsOnNewPropertyLink(string mainpartyVal, string relatioshipValue)
        {
            FillAndSelectLookUpResult(MainParty, mainpartyVal);
            FillAndSelectLookUpResult(PartyRelationshipRole, relatioshipValue);

        }

        public void ToggleValidationSection(string PADCodeValidateToggleValue, string relatioshipValue)
        {
            SeleniumExtensions.ScrollToElement(ValidateLegalInterestedPartyToggleTxt);
            SeleniumExtensions.SelectToggle(ValidateLegalInterestedPartyToggleTxt, LegallyInterestedPartyToggleBtn, PADCodeValidateToggleValue);

            SeleniumExtensions.ScrollToElement(ValidateIsValidToggleTxt);
            SeleniumExtensions.SelectToggle(ValidateIsValidToggleTxt, IsValidToggleBtn, PADCodeValidateToggleValue);

            if (relatioshipValue == "Owner")
            {
                SeleniumExtensions.ScrollToElement(ValidateIsTaxpayerToggleTxt);
                SeleniumExtensions.SelectToggle(ValidateIsTaxpayerToggleTxt, IsTaxpayerToggleBtn, PADCodeValidateToggleValue);
            }



        }

        public void AddCreatedPropertyLinkRecords(string fieldName, string NameValue, string bpName, Dictionary<string, string> testdata)
        {
            SeleniumExtensions.scrollToBtnBasedOnLabelAndClick(fieldName);

            NewRequestPropertyLinkSearch.ClickUsingJavascript();
            NewRequestPropertyLink.ClickUsingJavascript();
            SeleniumExtensions.ClearAndSendkeys(NameLocator, NameValue);
            ClickonSaveandCloseDialog.ClickUsingJavascript();
            NewRequestPropertyLinklookupSearch.ClickUsingJavascript();
            AdvancedLookup.ClickUsingJavascript();
            if (NameValue == "TaxPayer")
            {
                DuprIDSearch.ClearAndSendkeys(testdata["OwnerDuprID"]);
            }
            else if (NameValue == "Agent")
            {
                DuprIDSearch.ClearAndSendkeys(testdata["AgentDuprID"]);
            }
            Thread.Sleep(2000);
            SelectDuprIDSearch.ClickUsingJavascript();
            Thread.Sleep(4000);
            ClickonDoneDialog.ClickUsingJavascript();
            ClickonSaveandCloseDialog.ClickUsingJavascript();
            ClickonCancelDialog.ClickUsingJavascript();
        }

        public void ValidateCheckCTChallenge(string requestAction, string MenuOption, string jobType, Dictionary<string, string> testdata, String dateCaptured, string town, string postcode, string uprn)
        {

            string proposedEffectiveDateChanged;
            string dateType = null;
            Random rand = new Random();
            int randomBAReference = rand.Next(100000, 999999);
            string[] dateValue = dateCaptured.Split(' ');
            DateTime dateToEnter = DateTime.Parse(dateValue[0]);
            if (dateType == "Forward")
            {
                proposedEffectiveDateChanged = dateToEnter.AddDays(2).ToString("M/d/yyyy");
            }
            else if (dateType == "Backward")
            {
                proposedEffectiveDateChanged = dateToEnter.AddDays(-2).ToString("M/d/yyyy");
            }
            else if (dateType == "Current")// Effective from Date fetch from DB
            {
                proposedEffectiveDateChanged = dateToEnter.ToString("M/d/yyyy");
            }
            else
            {
                proposedEffectiveDateChanged = DateTime.Now.AddDays(-2).ToString("M/d/yyyy");

            }
            Console.WriteLine(proposedEffectiveDateChanged);

            switch (jobType)
            {
                case "Informal Challenge":
                    //Thread.Sleep(10000);
                    ClickMenuListOptionFromCommandBar(requestAction, MenuOption);
                    ValidateAcceptanceCheckInTheGeneralTab(testdata, proposedEffectiveDateChanged);
                    ClickSaveDialogforValidateRequest();
                    ValidateAndClickOnTab("Comparator Properties", "Validity/Acceptance Check Form");
                    ValidateComparatorPropertiesTab(testdata, proposedEffectiveDateChanged, town, postcode, uprn);
                    break;

            }
        }


        public void ValidateAcceptanceCheckInTheGeneralTab(Dictionary<string, string> testdata, string DateCaptured)
        {
            FillAndSelectLookUpResult(TaxPayerRequestPropertyLink, testdata["CTPlayer"]);
            FillAndSelectLookUpResult(InterestedPartyRequestPropertyLink, testdata["Submitted By"]);
            SeleniumExtensions.SelectDropdownValue(NewEvidenceProvided, "Yes");
            //SeleniumExtensions.SelectElementByText(NewEvidenceProvided, "Yes");
            SeleniumExtensions.ScrollToElement(RationaleforCustomersAssertion);
            SeleniumExtensions.ClearAndSendkeys(RationaleforCustomersAssertion, "TEST");
            SeleniumExtensions.ClearAndSendkeys(ProposedBand, "A");
            ProposedEffectiveDateForIC(-1);
            //SeleniumExtensions.ScrollToElement(ProceedWithAccChecks);
            //SeleniumExtensions.SelectElementByText(ProceedWithAccChecks, "Yes");

        }

        public void ValidateComparatorPropertiesTab(Dictionary<string, string> testdata, string DateCaptured, string town, string postcode, string uprn)
        {
            IdentifyComparatorPropButton.ClickUsingJavascript();
            FindHereditamentForJobCreationforComparator(town, postcode, uprn);
            Thread.Sleep(1000);
            ClickonSaveandCloseDialog.ClickUsingJavascript();
        }

        public void IdentifyComparatorPropertyDetails(string town, string postcode, string uprn)
        {   waitHelpers wh = new waitHelpers();
            //IdentifyComparatorPropButton.ClickUsingJavascript();
            wh.jsClickOnElement(dialogFindHereditament);
            FindHereditamentForJobCreationforComparator(town, postcode, uprn);
            Thread.Sleep(1000);
            //ClickonSaveandCloseDialog.ClickUsingJavascript();
        }

        public void ValidateeComparablePropertyMatchResults(string jobtype, Dictionary<string, string> testdata)
        {
            ValidateAndClickOnTab("General", "Validity/Acceptance Check Form");
            SeleniumExtensions.ScrollToElement(HouseTypeMatch);
            SeleniumExtensions.clickElementAndSelectText("House Type Match", "Yes");
            //SeleniumExtensions.SelectDropdownValue(HouseTypeMatch, "Yes");
            //SeleniumExtensions.ScrollToElement(RationaleforCustomersAssertion);
            //SeleniumExtensions.ClearAndSendkeys(RationaleforCustomersAssertion, "TEST");
            //SeleniumExtensions.ScrollToElement(PropertySizeMatch);
            SeleniumExtensions.clickElementAndSelectText("Property Size Match", "Yes");
            //SeleniumExtensions.SelectDropdownValue(PropertySizeMatch, "Yes");
            //SeleniumExtensions.ScrollToElement(RationaleforCustomersAssertion);
            //SeleniumExtensions.ClearAndSendkeys(RationaleforCustomersAssertion, "TEST");
            //SeleniumExtensions.ScrollToElement(AgeMatch);
            SeleniumExtensions.clickElementAndSelectText("Age Match", "Yes");
            //SeleniumExtensions.SelectDropdownValue(AgeMatch, "Yes");
            //SeleniumExtensions.ScrollToElement(RationaleforCustomersAssertion);
            //SeleniumExtensions.ClearAndSendkeys(RationaleforCustomersAssertion, "TEST");
            //SeleniumExtensions.ScrollToElement(GroupMatch);
            //SeleniumExtensions.SelectDropdownValue(GroupMatch, "Yes");
            SeleniumExtensions.clickElementAndSelectText("Group Match", "Yes");
            //SeleniumExtensions.ScrollToElement(RationaleforCustomersAssertion);
            //SeleniumExtensions.ClearAndSendkeys(RationaleforCustomersAssertion, "TEST");
            //SeleniumExtensions.ScrollToElement(InLowerBand);
            //SeleniumExtensions.SelectDropdownValue(InLowerBand, "Yes");
            SeleniumExtensions.clickElementAndSelectText("In Lower Band", "Yes");
            //SeleniumExtensions.ScrollToElement(RationaleforCustomersAssertion);
            //SeleniumExtensions.ClearAndSendkeys(RationaleforCustomersAssertion, "TEST");
            //SeleniumExtensions.ScrollToElement(ComparablesAccepted);
            SeleniumExtensions.clickElementAndSelectText("Comparables Accepted", "Yes");
            //SeleniumExtensions.SelectDropdownValue(ComparablesAccepted, "Yes");
            Thread.Sleep(1000);
            DialogSave.ClickUsingJavascript();
            string decision = AcceptanceDecision.GetAttribute("aria-label");
            if (decision == "Accepted")
            {
                Console.WriteLine($"Decision is updated with the value {decision}");
            }
            else
            {
                Console.WriteLine("Decision is not updated");

            }
            ClickCompleteAcceptanceDialog();
            //OkBtnOnAddresslabelNotificationDialog.ClickUsingJavascript();
            //CloseButtOnDialog.ClickUsingJavascript();

        }


        public void ValidatesTheOutboundCorrespondence(string country, string status)
        {

            ValidateAndClickOnTab("Outbound Correspondence", "Request Form");
            CommonPage commonpage = new CommonPage();
            commonpage.ValidateCorrespondenceLinkAndClick();
            string expCountry = OutboundCorressTitle.GetAttribute("title");
            if (expCountry == country)
            {

                Console.WriteLine($"Expected and Actual country matches {expCountry}");

            }
            Assert.IsTrue(OCStatusReasondropdown.ElementVisisbleUsingExplicitWait(10), "The Status reason is not displayed yet");
            OCStatusReasondropdown.ClickUsingJavascript();
            OCStatusReasonSent.ClickUsingJavascript();
            ClickonSaveanCloseDialog.ClickUsingJavascript();

        }

        public void EnterDetailsInReconTabForSplitproposal(int noOfSplit, string newPropPostCode)
        {
            CommonPage commonpage = new CommonPage();
            waitHelpers wh = new waitHelpers();
            string adressuprn = null;
            Random rand = new Random();
            int randomBAReference = rand.Next(100000, 999999);
            //string prefix = "32343";
            //int randomSuffix = rand.Next(1000, 9999);
            //string walesBAReference = prefix + randomSuffix.ToString();

            NoOfPropForReconDel.ClearAndSendkeys("1");
            NoOfPropForReconNew.ClearAndSendkeys(noOfSplit.ToString());
            ClickCommandBarOption("Save");
            AddIncomingOnRecon.ClickUsingJavascript();
            wh.elementClickableAndDisplayed(findaddressdropdownBy);
            SeleniumExtensions.ScrollToElement(findaddressdropdown);
            wh.clickOnWebElement(findaddressdropdownBy);
            textEle.ClickUsingJavascript();
            SeleniumExtensions.ScrollToElement(ProposedBARefOnDialog);
            wh.isElementDisplayed(ProposedBARefOnDialogBy, 90);
            wh.clickOnElement(ProposedBARefOnDialogBy);
            wh.GetWebElement(ProposedBARefOnDialogBy).SendKeys(randomBAReference.ToString());
            clickOnDialogMenuElement("Save");
            commonpage.waitTillSavingDisaddpears("Saving...", "progressbar");
            if (adressuprn == null)
            {
                adressuprn = FindAddressForNewProperty(newPropPostCode);
            }
            else
            {
                adressuprn = FindAddressFollowingUprn(adressuprn, newPropPostCode);
            }
            //clickOnDialogMenuElement("Save");
            //commonpage.waitTillSavingDisaddpears("Saving...", "progressbar");

        }
        public void EnterDetailsInReconTabForSplitproposal_wales_guardrails(int noOfSplit, string newPropPostCode)
        {
            CommonPage commonpage = new CommonPage();
            waitHelpers wh = new waitHelpers();
            string adressuprn = null;
            Random rand = new Random();
            int randomBAReference = rand.Next(100000, 999999);

            NoOfPropForReconDel.ClearAndSendkeys("1");
            NoOfPropForReconNew.ClearAndSendkeys(noOfSplit.ToString());
            ClickCommandBarOption("Save");
            AddIncomingOnRecon.ClickUsingJavascript();
            wh.isElementDisplayed(findaddressdropdownBy, 90);
            wh.elementClickableAndDisplayed(findaddressdropdownBy);
            SeleniumExtensions.ScrollToElement(findaddressdropdown);
            wh.clickOnWebElement(findaddressdropdownBy);
            textEle.ClickUsingJavascript();
            SeleniumExtensions.ScrollToElement(ProposedBARefOnDialog);
            wh.isElementDisplayed(ProposedBARefOnDialogBy, 90);
            wh.clickOnElement(ProposedBARefOnDialogBy);
            wh.GetWebElement(ProposedBARefOnDialogBy).SendKeys(randomBAReference.ToString());
            By errorMessageBy = By.XPath($"//span[@data-id='voa_proposedbareferencenumber-error-message']");
            bool isErrorDisplayed = wh.isElementDisplayed(errorMessageBy, 5);

        }

        public void EnterDetailsInReconTabForSplitproposal_wales(int noOfSplit, string newPropPostCode)
        {
            CommonPage commonpage = new CommonPage();
            waitHelpers wh = new waitHelpers();
            string adressuprn = null;
            Random rand = new Random();
            string prefix = "32343";
            int randomSuffix = rand.Next(1000, 9999);
            string walesBAReference = prefix + randomSuffix.ToString();

            NoOfPropForReconDel.ClearAndSendkeys("1");
            NoOfPropForReconNew.ClearAndSendkeys(noOfSplit.ToString());
            ClickCommandBarOption("Save");
            AddIncomingOnRecon.ClickUsingJavascript();
            wh.isElementDisplayed(findaddressdropdownBy, 90);
            SeleniumExtensions.ScrollToElement(findaddressdropdown);
            wh.elementClickableAndDisplayed(findaddressdropdownBy);
            wh.clickOnWebElement(findaddressdropdownBy);
            textEle.ClickUsingJavascript();
           
            SeleniumExtensions.ScrollToElement(ProposedBARefOnDialog);
            wh.isElementDisplayed(ProposedBARefOnDialogBy, 90);
            wh.clickOnElement(ProposedBARefOnDialogBy);
            wh.GetWebElement(ProposedBARefOnDialogBy).ClearAndSendkeys(walesBAReference);
            clickOnDialogMenuElement("Save");
            commonpage.waitTillSavingDisaddpears("Saving...", "progressbar");
            if (adressuprn == null)
            {
                adressuprn = FindAddressForNewProperty(newPropPostCode);
            }
            else
            {
                adressuprn = FindAddressFollowingUprn(adressuprn, newPropPostCode);
            }
         
        }


        public void EnterDetailsInReconTabForSplit(int noOfSplit, string newPropPostCode)
        {
            waitHelpers wh = new waitHelpers();
            string adressuprn = null;
            Random rand = new Random();
            NoOfPropForReconDel.ClearAndSendkeys("1");
            NoOfPropForReconNew.ClearAndSendkeys(noOfSplit.ToString());
            ClickCommandBarOption("Save");
            CommonPage commonpage = new CommonPage();
            commonpage.waitTillSavingDisaddpears();
            for (int i = 1; i <= noOfSplit; i++)
            {
                int randomBAReference = rand.Next(100000, 999999);
                AddIncomingOnRecon.ClickUsingJavascript();
                commonpage.waitTillappProgressIndicatorDisaddpears_();
                SeleniumExtensions.ScrollToElement(wh.GetWebElement(ProposedBARefOnDialogBy));
                //SeleniumExtensions.ScrollToElement(ProposedBARefOnDialog);
                wh.isElementDisplayed(ProposedBARefOnDialogBy, 90);
                wh.clickOnElement(ProposedBARefOnDialogBy);
                wh.GetWebElement(ProposedBARefOnDialogBy).SendKeys(randomBAReference.ToString());
                SeleniumExtensions.ScrollToElement(BARefOnDialog);
                wh.isElementDisplayed(BARefOnDialogBy, 90);
                wh.clickOnElement(BARefOnDialogBy);
                wh.GetWebElement(BARefOnDialogBy).ClearAndSendkeys(randomBAReference.ToString());
                if (adressuprn == null)
                {
                    adressuprn = FindAddressForNewProperty_Recon(newPropPostCode);
                }
                else
                {
                    adressuprn = FindAddressFollowingUprn(adressuprn, newPropPostCode);
                }
                clickOnDialogMenuElement("Save & Close");
                //DialogSaveAndClose.ClickUsingJavascript();
                //CommonPage commonpage = new CommonPage();
                commonpage.waitTillSavingDisaddpears_new("Saving...", "progressbar");
            }
        }


        public void EnterDetailsInReconTabForMerge(string newPropPostCode, string town, string postcode, string uprn)
        {
            waitHelpers wh = new waitHelpers();
            string adressuprn = null;
            Random rand = new Random();
            int randomBAReference = rand.Next(100000, 999999);
            //  NoOfPropForReconDel.ClearAndSendkeys("1");
            // NoOfPropForReconNew.ClearAndSendkeys(noOfSplit.ToString());
            ClickCommandBarOption("Save");
            AddOutgoingOnRecon.ClickUsingJavascript();
            FindHereditamentOnDialogForJob(town, postcode, uprn);
            DialogSaveAndClose.ClickUsingJavascript();
            // ClickCommandBarOption("Save & Close");

            AddIncomingOnRecon.ClickUsingJavascript();
            SeleniumExtensions.ScrollToElement(ProposedBARefOnDialog);
            ProposedBARefOnDialog.ClearAndSendkeys(randomBAReference.ToString());
            SeleniumExtensions.ScrollToElement(BARefOnDialog);
            BARefOnDialog.ClearAndSendkeys(randomBAReference.ToString());
            adressuprn = FindAddressForNewProperty(newPropPostCode);
            DialogSaveAndClose.ClickUsingJavascript();


        }

        public void ValidateJobCreatedDisplayedOnRelatedJobsTab(Dictionary<string, string> testdata)
        {
            IList<IWebElement> RowsElement = Driver.FindElements(MaterialRequestRows);
            if (RowsElement.Count != 3)
            {
                SeleniumExtensions.RefreshPage();
                ClickRelatedJobsTab();
                RowsElement = Driver.FindElements(MaterialRequestRows);
            }

            foreach (var row in RowsElement)
            {
                int rowno = RowsElement.Count;
                string rowindex = row.GetAttribute("row-index");
                IWebElement JobId = DriverHelper.Driver.WaitForElement(By.XPath($"//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row') and @row-index='{rowindex}']//*[contains(@col-id,'ticketnumber')]//label"));
                if (rowindex.Equals("0"))
                {
                    testdata["ReconDel"] = JobId.GetAttribute("aria-label");

                }
                if (rowindex.Equals("1"))
                {
                    testdata["ReconNew1"] = JobId.GetAttribute("aria-label");

                }
                if (rowindex.Equals("2"))
                {
                    testdata["ReconNew2"] = JobId.GetAttribute("aria-label");

                }
            }

            IWebElement OpenJobId = DriverHelper.Driver.WaitForElement(By.XPath($"//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row') and @row-index='0']//*[@aria-colindex='4']//a[contains(@aria-label,'Reconstitution Delete')]"));
            OpenJobId.DoubleClickElementUsingJSExecutor();
        }

       
        public string FindAddressFollowingUprn(string expUprn, string postCode)
        {
            string addressUprn = null;
           bool found = false;
            waitHelpers wh = new waitHelpers();
            wh.WaitForElementToBeDisplayed(FindAddressUsingBy, 20);
            Thread.Sleep(3000);
            SeleniumExtensions.ScrollToElement(FindAddress);
            FindAddress.ClickUsingJavascript();
            wh.WaitForElementToBeDisplayed(AddressSearchInputOnDialogUsingBy, 20);
            // Assert.IsTrue(AddressSearchInputOnDialog.ElementVisisbleUsingExplicitWait(5), "The Address Search Dialog does not appear");
            AddressSearchInputOnDialog.ClearAndSendkeys(postCode);

            BtnSearchOnDialog.ClickUsingJavascript();

            while (!found)
            {

                //IList<IWebElement> AddressesRows = Driver.FindElements(AddressRowsFollowingUPRN(expUprn));
                IList<IWebElement> AddressesRows = wh.getAllWebElements(AddressRowsFollowingUPRN(expUprn));

                foreach (var row in AddressesRows)
                {
                    row.ElementVisisbleUsingExplicitWait(10);

                    row.ClickUsingJavascript();
                    string rowindex = row.GetAttribute("data-item-index");
                    addressUprn = Driver.FindElement(By.XPath($"//*[@data-automationid='DetailsList']//*[@data-automationid='DetailsRow' and @data-item-index='{rowindex}']//*[@data-automation-key='uprn']//*[text()]")).Text;
                    Console.WriteLine(addressUprn);

                    BtnUseAddressOnDialog.ClickUsingJavascript();

                    if (DuplicateAddressHeader.IsElementVisibleUsingByLocator(5))
                    {
                        DuplicateAddressCloseBtn.ClickUsingJavascript();
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
                    break;
                }


            }
            return addressUprn;

        }


        public void ValidatetheAssociatedJobs(string jobName)
        {
            int findRowsTries = 0;
            bool validateFlag = false;
            bool findrowsFlag = false;
            IList<IWebElement> RowsElement = Driver.FindElements(MaterialRequestRows);
            if (RowsElement.Count == 0)
            {
                while (RowsElement.Count == 0 && findRowsTries < 3)
                {
                    ClickCommandBarOption("Refresh");
                    ValidateAndClickOnTab("Associated Jobs", "Desktop Research Form");
                    RowsElement = Driver.FindElements(MaterialRequestRows);
                    if (RowsElement.Count > 0)
                    {
                        findrowsFlag = true;
                        break;
                    }
                    findRowsTries++;
                }
                if (findrowsFlag == false)
                {
                    Assert.Fail("Associated Jobs not created");
                    // Log.Error("");
                }

            }

            foreach (var row in RowsElement)
            {
                string rowindex = row.GetAttribute("row-index");
                IWebElement JoBNameActual = DriverHelper.Driver.WaitForElement(By.XPath($"//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row') and @row-index='{rowindex}']//*[contains(@col-id,'voa_codedreason')]//a"));

                string JoBNameActualText = JoBNameActual.GetAttribute("aria-label");
                if (JoBNameActualText != jobName)
                {
                    //while (JoBNameActualText != jobName)
                    //{
                    JoBNameActualText = JoBNameActual.GetAttribute("aria-label");
                    if (JoBNameActualText == jobName)
                    {
                        validateFlag = true;
                        break;
                    }

                    //}
                }
                else
                {
                    validateFlag = true;

                }
                if (validateFlag == true)
                {
                    Assert.AreEqual(jobName, JoBNameActualText, $"The Job type has been validated as {JoBNameActualText} within the max tries");
                    // Log.Information($"");
                }
                else
                {
                    Assert.Fail("The Job type is not available and failed with" + JoBNameActualText);

                }
                break;
            }
        }

        public List<String> getTheAssociatedJobsDetails()
        {
            waitHelpers wh = new waitHelpers();
            int findRowsTries = 0;
            bool findrowsFlag = false;
            List<string> getAssociatedJobs = new List<string>();
            IList<IWebElement> RowsElement = wh.getAllWebElements(associatedJobNameRows);
            while (!findrowsFlag)
            {
                if (RowsElement.Count == 2)
                {
                    findrowsFlag = true;
                    break;
                }
                else
                {
                    wh.clickOnElement(JobCommands_refreshBtn);
                    Thread.Sleep(2000);
                    RowsElement = wh.getAllWebElements(associatedJobNameRows);
                }

                findRowsTries = findRowsTries + 1;
                if (findRowsTries == 10)
                    break;

            }
            if (findrowsFlag == false)
            {
                Assert.Fail("Associated Jobs not created");
            }


            foreach (var row in RowsElement)
            {
                string rowindex = row.GetAttribute("row-index");
                IWebElement JoBNameActual = wh.GetWebElement(By.XPath($"//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row') and @row-index='{rowindex}']//*[contains(@col-id,'voa_codedreason')]//a"));
                string JoBNameActualText = JoBNameActual.GetAttribute("aria-label");
                //JoBNameActualText = JoBNameActual.GetAttribute("aria-label");
                getAssociatedJobs.Add(JoBNameActualText);
            }
            return getAssociatedJobs;
        }



        public void FindAddressForDuplicateChangeOfAddress(string postCode)
        {
            waitHelpers wh = new waitHelpers();
            wh.WaitForElementToBeDisplayed(FindAddressUsingNewBy, 20);
            Thread.Sleep(3000);
            SeleniumExtensions.ScrollToElement(FindAddress_New);
            FindAddress_New.ClickUsingJavascript();
            BtnSearchOnDialog.ClickUsingJavascript();

            while (true)
            {
                IList<IWebElement> AddressesRows = wh.getAllWebElements(AddressRow);
                int count = AddressesRows.Count;
                for (int i = 3; i <= count; i++)
                {
                    IList<IWebElement> AddressesRow = wh.getAllWebElements(AddressRow);
                    AddressesRow[i].ElementVisisbleUsingExplicitWait(10);

                    AddressesRow[i].ClickUsingJavascript();
                    BtnUseAddressOnDialog.ClickUsingJavascript();

                    if (StatusReasonAddress.IsElementDisplayed(5))
                    {
                        string statusReasonaddressExp = StatusReasonAddress.GetAttribute("value").ToString();

                        if (statusReasonaddressExp == "Duplicate Validation Passed")
                        {
                            FindAddress_New.ClickUsingJavascript();
                            BtnSearchOnDialog.ClickUsingJavascript();
                            continue;
                        }

                    }


                    DuplicateAddressHeader.IsElementVisibleUsingByLocator(5);
                    UseProposedAddress.ClickUsingJavascript();
                    Thread.Sleep(3000);
                    break;
                }
                IWebElement status = DriverHelper.Driver.WaitForElement(By.CssSelector("[aria-label='Status Reason']"));
                string statusReason = status.GetAttribute("value");
                if (statusReason == "Duplicate Validation Failed")
                {
                    Console.WriteLine("Duplicate Validation Failed");
                    break;
                }
                else
                {
                    Assert.Fail("Duplicate Validation Passed");
                    Log.Error("Duplicate Validation Passed");
                    break;
                }
            }
            Thread.Sleep(2000);
        }


        public void ValidateJobsCreatedAndCaptureJobID(Dictionary<string, string> testdata)
        {
            testdata["ReconNew1"] = null;
            testdata["ReconNew2"] = null;
            testdata["ReconDel"] = null;
            testdata["ReconDel2"] = null;
            int rowsCounter = 0;
            int counter = 1;
            int countertemp = 1;
            ChildJobsRefreshBtn.ClickUsingJavascript();
            IList<IWebElement> RowsJobNameElements = Driver.FindElements(JObNameOnRows);
            int rowsCount = RowsJobNameElements.Count;
            while (rowsCount != 3)
            {
                ChildJobsRefreshBtn.ClickUsingJavascript();
                RowsJobNameElements = Driver.FindElements(JObNameOnRows);
                rowsCount = RowsJobNameElements.Count;
                if (rowsCount == 3)
                {
                    break;
                }
                countertemp++;
                if (countertemp == 40)
                {
                    break;
                }
            }
            Assert.AreEqual(rowsCount, 3);


            foreach (var row in RowsJobNameElements)
            {
                string JobTitle = row.GetAttribute("aria-label");
                while (!JobTitle.Contains("Reconstitution"))
                {
                    ChildJobsRefreshBtn.ClickUsingJavascript();
                    counter++;
                    if (JobTitle.Contains("Reconstitution"))
                    {
                        continue;
                    }

                }
            }
            IList<IWebElement> RowsElement = Driver.FindElements(MaterialRequestRows);
            foreach (var row in RowsElement)
            {

                int rowno = RowsElement.Count;
                string rowindex = row.GetAttribute("row-index");
                IWebElement JobTitle = DriverHelper.Driver.WaitForElement(By.XPath($"//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row') and @row-index='{rowindex}']//*[@col-id='title']//a"));
                IWebElement JobId = DriverHelper.Driver.WaitForElement(By.XPath($"//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row') and @row-index='{rowindex}']//*[contains(@col-id,'ticketnumber')]//label"));
                if (JobTitle.GetAttribute("aria-label").Contains("Reconstitution Delete"))
                {
                    if (testdata["ReconDel"] == null)
                    {
                        testdata["ReconDel"] = JobId.GetAttribute("aria-label");
                        Console.WriteLine("Recon Deletion Job ID is " + testdata["ReconDel"]);
                    }
                    else
                    {
                        testdata["ReconDel2"] = JobId.GetAttribute("aria-label");
                        Console.WriteLine("Recon Deletion Job ID2 is " + testdata["ReconDel2"]);
                    }
                }
                else if (JobTitle.GetAttribute("aria-label").Contains("Reconstitution New"))
                {
                    if (testdata["ReconNew1"] == null)
                    {
                        testdata["ReconNew1"] = JobId.GetAttribute("aria-label");
                        Console.WriteLine("Recon New Job ID1  is " + testdata["ReconNew1"]);
                    }
                    else
                    {
                        testdata["ReconNew2"] = JobId.GetAttribute("aria-label");
                        Console.WriteLine("Recon New Job ID2 is " + testdata["ReconNew2"]);
                    }

                }
                rowsCounter++;
                if (rowsCounter == 3)
                {
                    break;
                }


            }
            IWebElement OpenJobId = DriverHelper.Driver.WaitForElement(By.XPath($"//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row') and @row-index='0']//*[@aria-colindex='4']//a[contains(@aria-label,'Reconstitution Delete')]"));
            OpenJobId.DoubleClickElementUsingJSExecutor();
        }

        //  ConfirmButtonOnDialog.ClickUsingJavascript();

        public void ClickOnConfirmBtnForReconType()
        {
            ConfirmButtonOnDialog.ClickUsingJavascript();

        }

        public void ClickBtnOnAllJobsCreated(string btnName)
        {
            ConfirmButtonOnDialog.ClickUsingJavascript();

        }
        public void clickOnInspectionJobID()
        {
            waitHelpers wh = new waitHelpers();
            wh.clickOnElement(InspectionJobIDEle);
            //SeleniumExtensions.WaitForElementAndClick(InspectionJobIDEle, 120);

        }

        public void fileterNewlyCreatedRequest(String columName, String newRequestType)
        {
            waitHelpers wh = new waitHelpers();
            By filterColoumn = By.XPath($"//*[@data-testid='columnHeader']//label/div[text()='{columName}']");
            By filterBy = By.XPath("//ul[contains(@class,'ms-ContextualMenu-list')]/li/button[@name='Filter by']");
            By filterByValue1 = By.CssSelector($"input[aria-label='Filter by value']");
            By applyBtn = By.XPath("//button//*[text()='Apply']");
            By fileterdRequestLnk = By.CssSelector("[col-id = 'voa_name'] a");
            String requestNameBfr = (string)_featureContext["Request Name"];
            String filterValue = requestNameBfr.Replace("Proposal", newRequestType);
            wh.clickOnElement(filterColoumn);
            wh.clickOnElement(filterBy);
            wh.GetWebElement(filterByValue1).SendKeys(filterValue);
            wh.clickOnElement(applyBtn);
            wh.isElementDisplayed(fileterdRequestLnk, 30);
            wh.clickOnElement(fileterdRequestLnk);
        }



        public void fileterRequriedRequest(String columName, String newRequestType)
        {
            waitHelpers wh = new waitHelpers();
            By filterColoumn = By.XPath($"//*[@data-testid='columnHeader']//label/div[text()='{columName}']");
            By filterBy = By.XPath("//ul[contains(@class,'ms-ContextualMenu-list')]/li/button[@name='Filter by']");
            By filterByValue1 = By.CssSelector($"input[aria-label='Filter by value']");
            By applyBtn = By.XPath("//button//*[text()='Apply']");
            By fileterdRequestLnk = By.CssSelector("[col-id = 'voa_name'] a");
            String filterValue = newRequestType;//(string)_featureContext[newRequestType];
            wh.clickOnElement(filterColoumn);
            wh.clickOnElement(filterBy);
            wh.GetWebElement(filterByValue1).SendKeys(filterValue);
            wh.clickOnElement(applyBtn);
            wh.isElementDisplayed(fileterdRequestLnk, 30);
            wh.clickOnElement(fileterdRequestLnk);
        }


        public void filterVOA(String columName, String fieldvalue)
        {
            waitHelpers wh = new waitHelpers();
            Thread.Sleep(3000);
            By filterColoumn = By.XPath($"//*[@data-testid='columnHeader']//label/div[text()='{columName}']");
            By filterBy = By.XPath("//ul[contains(@class,'ms-ContextualMenu-list')]/li/button[@name='Filter by']");
            By filterByValue1 = By.CssSelector($"input[aria-label='Filter by value']");
            By applyBtn = By.XPath("//button//*[text()='Apply']");
            By VoaBtn = By.XPath("(//span[@data-automationid='splitbuttonprimary'])[7]");
            By fileterdRequestLnk = By.CssSelector("[col-id = 'voa_customer2id'] a");
            //By fileterdRequestLnkBy = getLocator($"[col-id = 'voa_customer2id'] a");
            String filterValue = fieldvalue;//(string)_featureContext[newRequestType];
            wh.clickOnElement(filterColoumn);
            wh.clickOnElement(filterBy);
            wh.GetWebElement(filterByValue1).SendKeys(filterValue);
            Thread.Sleep(1000);
            wh.clickOnElement(VoaBtn);
            wh.clickOnElement(applyBtn);
            wh.isElementDisplayed(fileterdRequestLnk, 30);
            wh.clickOnElement(fileterdRequestLnk);
 
            //SeleniumExtensions.ElementVisisbleUsingExplicitWait(10);
            var pdf_Util = new PDF_Utility();
            pdf_Util.takeScreenshot();


        }

        public void clickokonpopup(string text, string okbutton)
        {
            waitHelpers wh = new waitHelpers();
            CommonPage cp = new CommonPage();
            //By locator = null;
            cp.userValidateText(text);
            //locator = By.XPath($"//*[@data-id='alertdialog']//button[.//div[normalize-space(text())='{okbutton}']]");
            //wh.clickOnElement(locator);
            OkBtnOnAddresslabelNotificationDialog.ClickUsingJavascript();

        }

    }
}


















