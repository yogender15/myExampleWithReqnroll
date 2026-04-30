using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using POMSeleniumFrameworkPoc1.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace POMSeleniumFrameworkPoc1.Pages.UI.RBSP_BP
{
    public partial class CustomerEnquiryPage : BasePage
    {

        public void AddValidationDetails(Dictionary<string, string> testdata)
        {
            //  SelectDateFromDatePickerElement(ProposedEffectiveDateCE,- 2);
            //    DocumnetType.SelectElementByValue(testdata["CEDocumentType"]);
            //   AuthorityToActRequired.SelectElementByValue("Yes");
            SeleniumExtensions.SelectDropdownValue(AuthorityToActReceived, "Yes");
            //  AuthorityToActReceived.SelectElementByValue("Yes");

        }
        public void AddCallerInformationDetails(Dictionary<string, string> testdata)
        {
            By PartyProposer = GetFirstLookUp(testdata["CEPartyProposer"]);
            SeleniumExtensions.EnterInLookUpField(PartyProposer, PartyProposer, PartyProposerSearchBtn, testdata["CEPartyProposer"]);
            By RelationshipRole = GetFirstLookUp(testdata["RelationshipRole"]);
            SeleniumExtensions.EnterInLookUpField(RelationshipRole, PartyRelationshipRole, PartyRelationshipRoleSearchBtn, testdata["RelationshipRole"]);

        }

        public void AddCustomerEnquiryInformationDetails(Dictionary<string, string> testdata)
        {
            // Date Received in VOA
            //SelectDateFromDatePickerElement(DateReceivedInVOA, -12);
            // Evidence Sufficient Date
            //SelectDateFromDatePickerElement(EvidenceSufficientDate, 6);
            By TaskTypefirstValue = GetFirstLookUp(testdata["CETaskType"]);
            SeleniumExtensions.EnterInLookUpField(TaskTypefirstValue, TaskType, TaskTypeSearchBtn, testdata["CETaskType"]);

            //   By CallTypeFirstValue = GetFirstLookUp(testdata["CECallType"]);
            // SeleniumExtensions.EnterInLookUpField(CallTypeFirstValue, CallType, CallTypeSearchBtn, testdata["CECallType"]);

            By ContatctTypeFirstValue = GetFirstLookUp(testdata["CEContactType"]);
            SeleniumExtensions.EnterInLookUpField(ContatctTypeFirstValue, ContactType, ContactTypeSearchBtn, testdata["CEContactType"]);

            By CustEnqTypeFirstValue = GetFirstLookUp(testdata["CEType"]);
            SeleniumExtensions.EnterInLookUpField(CustEnqTypeFirstValue, CustEnquiryType, CustEnquiryTypeSearchBtn, testdata["CEType"]);

            By CustEnqSubTypeFirstValue = GetFirstLookUp(testdata["CESubType"]);
            SeleniumExtensions.EnterInLookUpField(CustEnqSubTypeFirstValue, CustEnquirySubType, CustEnquirySubTypeSearchBtn, testdata["CESubType"]);

            FillAndSelectLookUpResult(CouncilTaxPayerlookup, testdata["CECTPayer"]);
            // By CouncilTaxPayerFirstValue = GetFirstLookUp(testdata["CECTPayer"]);
            // SeleniumExtensions.EnterInLookUpField(CouncilTaxPayerFirstValue, CouncilTaxPayerlookup, CouncilTaxPayerSearchBtn, testdata["CECTPayer"]);
            Thread.Sleep(2000);
        }


        public void SelectNewSuplimentaryRecord()
        {
            ClickMenuListOptionFromCommandBarNew("Supplementary Job Requisition", "Job Actions");

        }

        public void SelectNewSuplimentaryRecordforEnquiry()
        {
            ClickMenuListOptionFromCommandBarNew("Supplementary Job Requisition", "Enquiry Actions");

        }

        //public void EnterDetailsForSupplimentaryRecordOnDialog(string requestType, string jobType, string town, string postCode, string expUPRN)
        //{
        //    By RTFirstValue = GetFirstLookUp(requestType);
        //    SeleniumExtensions.EnterInLookUpField(RTFirstValue, RequestTypeOnDialog, RequestTypeSearchBtnOnDialog, requestType);
        //    By JTFirstValue = GetFirstLookUp(jobType);
        //    SeleniumExtensions.EnterInLookUpField(JTFirstValue, JobSubTypeOnDialog, JobTypeSearchBtnOnDialog, jobType);
        //    FindHereditamentOnDialogForJob(town, postCode, expUPRN);
        //    ClickCommandBarOptionOnDialog("Submit");
        //    ContinueBtnOnConfirmDialog.ClickUsingJavascript();
        //}

        public void EnterDetailsOnCustEnquScreenAndCreateNewSuppRecord(string requestType, string jobType, Dictionary<string, string> testdata, string town, string postCode, string expUPRN)
        {
            AddValidationDetails(testdata);
            // AddCallerInformationDetails();
            AddCallerInformationDetails(testdata);
            // AddCustomerEnquiryInformationDetails();
            AddCustomerEnquiryInformationDetails(testdata);
            ClickCommandBarOption("Save");
            // Create new supplementatry Record
            SelectNewSuplimentaryRecord();
            EnterDetailsForSupplimentaryRecordOnDialog(requestType, jobType, town, postCode, expUPRN);
        }

        public void EnterDetailsOnCustEnquiryScreen(Dictionary<string, string> testdata, string town, string postCode, string expUPRN)
        {
            AddCustomerInformationDetails(testdata);
            AddCustomerEnquiryInformationDetails(testdata);
            SeleniumExtensions.scrollToBtnElement("Authority to Act Received");
            SeleniumExtensions.clickElementAndSelectText("Authority to Act Received", "Yes");
            ClickCommandBarOption("Save");
            //  AddValidationDetails(testdata);
            FindHereditamentForJobCreation(town, postCode, expUPRN);
            ClickCommandBarOption("Save");
            waitTillSavingDisaddpears("Saving...", "progressbar");
        }
        public void EnterDetailsOnCustEnquiryScreenforInfchall(Dictionary<string, string> testdata, string town, string postCode, string expUPRN)
        {
            AddCustomerInformationDetails(testdata);
            AddCustomerEnquiryInformationDetails(testdata);
            ClickCommandBarOption("Save");
            waitTillSavingDisaddpears("Saving...", "progressbar");
            SeleniumExtensions.SelectDropdownValue(AuthorityToActRequired, "Yes");
            FindHereditamentForJobCreation(town, postCode, expUPRN);
            ClickCommandBarOption("Save");
            waitTillSavingDisaddpears("Saving...", "progressbar");
        }

        public void AddCustomerInformationDetails(Dictionary<string, string> testdata)
        {
            By CRMCustomer = GetFirstLookUp(testdata["CRMCustomer"]);
            SeleniumExtensions.EnterInLookUpField(CRMCustomer, CustomerCRM, CustomerCRMSearchBtn, testdata["CRMCustomer"]);
            By CustomerReltnshpRoleVal = GetFirstLookUp("CT Payer");
            SeleniumExtensions.EnterInLookUpField(CustomerReltnshpRoleVal, CustomerReltnshpRole, CustomerReltnshpRoleSearchBtn, "CT Payer");

        }

        public void SetStatusReasonAndEnquiryState(string statusReason, string buttonName)
        {
            //SetStatusReasonForEnquiry.SelectElementByText(statusReason);
            SeleniumExtensions.SelectDropdownValue(SetStatusReasonForEnquiry, statusReason);
            SetStateButton(buttonName).ClickUsingJavascript();
            //   DeactivateButtonOnDialog.ClickUsingJavascript();
        }

        public void CreateNewSupplementaryJob(string requestType, string jobType, string town, string postCode, string expUPRN)
        {
            SelectNewSuplimentaryRecord();
            EnterDetailsForSupplimentaryRecordOnDialog(requestType, jobType, town, postCode, expUPRN);
        }


        public string CreateNewSupplementaryJobforEnquiry(string requestType, string jobType, string town, string postCode, string expUPRN)
        {
            SelectNewSuplimentaryRecordforEnquiry();
            string proposedEffectiveDateChanged = EnterDetailsForSupplimentaryRecordOnDialogforEnquiry(requestType, jobType, town, postCode, expUPRN);
            return proposedEffectiveDateChanged;
        }

        public void ValidateSupplementaryJobStatusAndClickOnReqLink(string statusText)
        {
            int counter = 1;
            string actualStatus = "";
            while (SupplementryJobStatusCode.IsElementDisplayed(2) == false)
            {
                RefreshBtnUnderSupplementaryReqTab.ClickUsingJavascript();
                Thread.Sleep(2000);
                if (SupplementryJobStatusCode.IsElementDisplayed(3) == true)
                {
                    actualStatus = SupplementryJobStatusCode.GetAttribute("aria-label");
                }
            }
            actualStatus = SupplementryJobStatusCode.GetAttribute("aria-label");
            if (actualStatus != statusText)
            {
                while (actualStatus != statusText)
                {
                    if (ErrorButton.IsElementDisplayed(2)) { ErrorButton.ClickUsingJavascript(); }
                    counter++;
                    RefreshBtnUnderSupplementaryReqTab.ClickUsingJavascript();
                    Thread.Sleep(3000);
                    actualStatus = SupplementryJobStatusCode.GetAttribute("aria-label");
                    if (actualStatus == statusText)
                    {
                        SupplementryRquestLink.ClickUsingJavascript();
                        ClickCommandBarOption("Save");
                        break;
                    }

                    if (counter == 10)
                    {
                        Assert.Fail("The Auto Processing Runtime status is " + actualStatus);

                    }
                    else
                    {
                        continue;
                    }
                }
            }
            else
            {
                SupplementryRquestLink.ClickUsingJavascript();
                ClickCommandBarOption("Save");

            }

        }

        public void UpdateMissingRecordsOnRequest(Dictionary<string, string> testdata, string authorizationDecision, string reasonValue, string DSR, string evidenceDate)
        {
            //SeleniumExtensions.scrollToBtnElement("Authority to Act");
            //SeleniumExtensions.SelectDropdownValue(AuthorizationDecision, "Satisfactory");
            //AuthorizationDecision.SelectElementByText(authorizationDecision);
            FillAndSelectLookUpResult(DSRLookUpField, DSR.Trim());
            ReasonForValidation.ClearAndEnterValueIntoTextBox(reasonValue);
            EvidenceSufficientDateForBP(evidenceDate);
            ClickCommandBarOption("Save");
            waitTillSavingDisaddpears("Saving...", "progressbar");
            //Thread.Sleep(6000);

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

        public void waitTillSavingDisaddpears(String labelName, String roleType)
        {
            String locator = $"//*[@role='{roleType}' and text()='{labelName}'] | //*[@role='{roleType}'] | //*[text()='{labelName}']";
            By eleBy = SeleniumExtensions.getLocator(locator);
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(180));
            wait.PollingInterval = TimeSpan.FromMilliseconds(200);
            wait.Until(SeleniumExtensions.InvisibilityOfElementLocatedBy(eleBy));
        }
    }
}
