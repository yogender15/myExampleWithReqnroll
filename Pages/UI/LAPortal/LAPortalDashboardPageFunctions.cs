using AventStack.ExtentReports.Gherkin.Model;
using NUnit.Framework;
using OpenQA.Selenium;
using POMSeleniumFrameworkPoc1.Helpers;
using Reqnroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace POMSeleniumFrameworkPoc1.Pages.UI.LAPortal
{
    public partial class LAPortalDashboardPage : BasePage
    {
        public void ClickCreateSingleReportLink()
        {
            CreateSingleReportLinkElement.ClickUsingJavascript();
        }

        public void SelectWhyAreYouCreatingFullDemolitionRadioBtnOption()
        {
            WhyAreYouCreatingFullDemolitionRadioBtnElement.ClickUsingJavascript();
        }

        public void SelectWhyAreYouCreatingMaterialIncreaseRadioBtnOption()
        {
            WhyAreYouCreatingMaterialIncreaseRadioBtnElement.ClickUsingJavascript();
        }

        public void SelectWhyAreYouCreatingPartDemolitionRadioBtnOption()
        {
            WhyAreYouCreatingPartDemolitionRadioBtnElement.ClickUsingJavascript();
        }

        public void SelectWhyAreYouCreatingNewPropertyRadioBtnOption()
        {
            WhyAreYouCreatingNewPropertyRadioBtnElement.ClickUsingJavascript();
        }

        public void ClickButtonContinue()
        {
            ButtonContinueElement.ClickUsingJavascript();
        }

        public void ClickContinueBtnOnConfirmAddressPage()
        {
            ContinueBtnOnConfirmAddressElement.ClickUsingJavascript();
        }

        public void EnterOtherPlanningPortalReferenceNumberText(string portalReferenceNumber)
        {
            OtherPlanningPortalReferenceNumber.SendKeys(portalReferenceNumber);
        }

        public void SelectPlanningPortalReferenceRadioYesBtnElement()
        {
            PlanningPortalReferenceRadioYesBtnElement.ClickUsingJavascript();
        }

        public void SelectPlanningPortalReferenceRadioNoBtnElement()
        {
            PlanningPortalReferenceRadioNoBtnElement.ClickUsingJavascript();
        }

        public void EnterBillingAuthorityReportNumber(string reportNumber)
        {
            BillingAuthorityReportNumber.SendKeys(reportNumber);
        }

        public void EnterBillingAuthorityReferenceNumber(string referenceNumber)
        {
            BillingAuthorityReferenceNumberElement.Clear();
            BillingAuthorityReferenceNumberElement.SendKeys(referenceNumber);
        }
        public void EnterPostCodeOnEnterThePostCodePropertyPage(string postCode)
        {
            PostCodeElement.SendKeys(postCode);
        }

        public void ClickFindAddress()
        {
            SeleniumExtensions.MoveToElement(FindAddressElement);
        }

        public void SelectAddressFromAddressList(string value)
        {
            System.Threading.Thread.Sleep(3000);
            SeleniumExtensions.SelectElementByValue(SelectAddressFromAddressListElement, value);
        }

        public void SelectDoesPropertyHavePlanningPortalReferenceRadioBtn()
        {
            System.Threading.Thread.Sleep(3000);
            DoesPropertyHavePlanningPortalReferenceRadioBtnElement.ClickUsingJavascript();
        }

        public void SelectWhyDoesPropertyNotHavePlanningPortalReferenceRadioBtn()
        {
            System.Threading.Thread.Sleep(3000);
            WhyDoesPropertyNotHavePlanningPortalReasonRadioBtnElement.ClickUsingJavascript();
        }

        public bool VerifyBillingAuthorityReferenceNumberText(string referenceNumber)
        {
            return BillingAuthorityReferenceNumberTextSelector(referenceNumber).GetElementWithSelector().Text.Contains(referenceNumber);
        }

        public void SelectCouncilTaxBandPropertyRadioBtn()
        {
            CouncilTaxBandPropertyRadioBtnElement.ClickUsingJavascript();
        }

        public void SelectDropdownFrom()
        {
            CouncilTaxBandPropertyRadioBtnElement.ClickUsingJavascript();
        }

        public void EnterFirstName(string name)
        {
            FirstNameElement.SendKeys(name);
        }

        public void EnterLastName(string name)
        {
            LastNameElement.SendKeys(name);
        }

        public void SelectOwnerOrOccupierRadioBtn()
        {
            OwnerOrOccupierRadioBtnElement.ClickUsingJavascript();
        }

        public void SelectAddressTheSameAsThePropertyAddressRadioBtn()
        {
            AddressTheSameAsThePropertyAddressRadioBtnElement.ClickUsingJavascript();
        }

        public void EnterEffectiveDateInChangeRequestDetailsPage(string year, string month, string day)
        {
            EffectiveDayElement.SendKeys(day);
            EffectiveMonthElement.SendKeys(month);
            EffectiveYearElement.SendKeys(year);
        }

        public void EnterRemarksInTextareaRemarks(string remarks)
        {
            TextareaRemarksTextElement.SendKeys(remarks);
        }

        public bool VerifyNotHavePlanningPortalReferenceErrorMessageIsDisplayed(string errorMessage)
        {
            return NotHavePlanningPortalReferenceErrorMessageElement.Text.Contains(errorMessage);
        }


        public bool VerifyDataDisplayedOnCheckDetailsBeforeSendingTheReportIsCorrect(string reportReason, string bAReportNumber, string billingPropertyReference, string taxBand)
        {
            return ReportReasonCheckDetailsBeforeSendingReportElement.Text.Contains(reportReason)
                && BillingPropertyReferenceNumCheckDetailsBeforeSendingReportElement.Text.Contains(billingPropertyReference)
                && BillingAuthorityReportNumberCheckDetailsBeforeSendingReportElement.Text.Contains(bAReportNumber)
                && CouncilTaxBandCheckDetailsBeforeSendingReportElement.Text.Contains(taxBand);
        }

        public bool IsSingleReportConfirmationMessageIsDisplayed(string confirmationMessage)
        {
            return SingleReportConfirmationMessageElement.Text.Contains(confirmationMessage);
        }

        public void SelectWasBuiltWithoutPlanningPermissionRadioBtn()
        {
            WasBuiltWithoutPlanningPermissionRadioBtnElement.ClickUsingJavascript();
        }

        public bool EnterABillingAuthorityReferenceReportNumbersErrorMessagesDisplayed(string BAReferencenumber, string BAReportErrorMessage)
        {
            return BillingReferenceErrorElement.Text.Contains(BAReferencenumber) && BillingReportErrorElement.Text.Contains(BAReportErrorMessage);
        }


        //*************************Ashish's Code*******************************

        public void SelectReasonCodeOnLAPortal(string reasonCode)
        {
            IWebElement reasonCodeToSelect = SelectReasonCode(reasonCode);
            reasonCodeToSelect.ClickUsingJavascript();
            ButtonContinueElement.ClickUsingJavascript();
        }

        public void CreateRequestOnLAPortal(string requestCode, Dictionary<string, string> testdata)
        {
            IWebElement reasonCodeToSelect = SelectReasonCode(requestCode);
            reasonCodeToSelect.ClickUsingJavascript();
            ButtonContinueElement.ClickUsingJavascript();

            switch (requestCode)
            {
                case "CR01":
                    EnterReferencesDataForLAPortalReq(testdata);
                    SelectBandForLAPortalRequest("Band A");
                    EnterContactDataForLAPortalReq(testdata);
                    EnterEffectiveDateAndRemarksLAPortal(testdata);

                    break;

                case "CR02":
                    break;

                case "CR03":
                    EnterReferencesDataForLAPortalReq(testdata);
                    EnterPostCodeAndSelectAddress(testdata);
                    SelectRadioValueLAPortal("Does the property have a planning portal reference?", "No");

                    break;

                case "CR04":
                    break;

                case "CR05":
                    break;

                case "CR06":
                    break;

                case "CR07":
                    break;

                case "CR09":
                    break;

                case "CR10":
                    break;

                case "CR12":
                    break;


            }
        }
        public void EnterReferencesDataForLAPortalReq(Dictionary<string, string> testdata)
        {

            BillingAuthorityReportNumber.ClearAndSendkeys(testdata["BAReportNum"]);
            BillingAuthorityReferenceNumberElement.ClearAndSendkeys(testdata["BAReferenceNum"]);
            ButtonContinueElement.ClickUsingJavascript();
            Thread.Sleep(4000);
            ButtonContinueElement.ClickUsingJavascript();
        }

        public void EnterReferencesDataForCR03LAPortalReq(Dictionary<string, string> testdata)
        {

            BillingAuthorityReportNumber.ClearAndSendkeys(testdata["BAReportNum"]);
            BillingAuthorityReferenceNumberElement.ClearAndSendkeys(testdata["BAReferenceNum"]);
            ReferencesContinueElement.ClickUsingJavascript();
        }

        public void EnterReferencesDataForCR03LAPortalReq(String BaRef, String baReportNum)
        {
            BillingAuthorityReportNumber.ClearAndSendkeys(baReportNum);
            BillingAuthorityReferenceNumberElement.ClearAndSendkeys(BaRef);
            ReferencesContinueElement.ClickUsingJavascript();
        }

        public void clickOnContinue(String pageName)
        {
            switch (pageName)
            {
                case "Check and confirm details":
                    OneResultContinueElement.Click();
                    break;
                case "Select the Council Tax band for this request":
                    continueElement1.Click();
                    break;


            }

        }

        public void SelectCouncilTaxBand()
        {
            Thread.Sleep(2000);
            SeleniumExtensions.ScrollAndClick(OneResultContinueElement);
            //OneResultContinueElement.ClickUsingJavascript();
            CouncilTaxBand.ClickUsingJavascript();
            ButtonContinueElement.ClickUsingJavascript();
            Thread.Sleep(2000);
            continueElement.ClickUsingJavascript();
        }

        public void SelectBandForLAPortalRequest(string bandCode)
        {
            IWebElement BandToSelect = SelectBand(bandCode);
            BandToSelect.ClickUsingJavascript();
            ButtonContinueElement.ClickUsingJavascript();
            Thread.Sleep(4000);
            ButtonContinueElement.ClickUsingJavascript();
        }

        public void EnterContactDataForLAPortalReq(Dictionary<string, string> testdata)
        {
            FirstNameElement.SendKeys(testdata["LAPortalFN"]);
            //LastNameElement.SendKeys(testdata["LAPortalLN"]);
            IWebElement ele = SelectRadioOptionLAPortal("What is their connection to the property?", "Owner");
            ele.ClickUsingJavascript();
            IWebElement ele1 = SelectRadioOptionLAPortal("Is their correspondence address the same as the property address?", "Yes");
            
            ele1.ClickUsingJavascript();
            ButtonContinueElement.ClickUsingJavascript();
        }

        public void SelectRadioValueLAPortal(string radioQuestion, string radioValue)
        {
            IWebElement radioValueToSelect = SelectRadioOptionLAPortal(radioQuestion, radioValue);
            radioValueToSelect.ClickUsingJavascript();

        }

        public void EnterEffectiveDateAndRemarksLAPortal(Dictionary<string, string> testdata)
        {
            var dateAndTime = DateTime.Now;
            var effectiveDate = dateAndTime.AddDays(-2);
            int year = effectiveDate.Year;
            int month = effectiveDate.Month;
            int day = effectiveDate.Day;
            EnterEffectiveDateInChangeRequestDetailsPage(year.ToString(), month.ToString(), day.ToString());
            TextareaRemarksTextElement.SendKeys(testdata["LAPortalRemarks"]);
            ButtonContinueElement.ClickUsingJavascript();
            Thread.Sleep(4000);
            ButtonContinueElement.ClickUsingJavascript();
        }

        public void EnterPostCodeAndSelectAddress(Dictionary<string, string> testdata)
        {
            PostCodeElement.SendKeys(testdata["LAPortalPostCode"]);
            Thread.Sleep(2000);
            SeleniumExtensions.MoveToElement(FindAddressElement);
            Thread.Sleep(2000);

            SelectAddressFromAddressListElement.SelectElementByText(testdata["LAPortalAddressToSelect"]);
            ButtonContinueElement.ClickUsingJavascript();
            Thread.Sleep(3000);
            ContinueBtnOnConfirmAddressElement.ClickUsingJavascript();
        }

        public void EnterPostCodeAndSelectAddress(FeatureContext fc)
        {
            PostCodeElement.SendKeys((String)fc["postcode"]);
            Thread.Sleep(2000);
            SeleniumExtensions.MoveToElement(FindAddressElement);
            Thread.Sleep(2000);
            string addressText = SeleniumExtensions.buildaddress((String)fc["building_number"], (String)fc["street"], (String)fc["locality"], (String)fc["town"], (String)fc["postcode"]);
            Console.WriteLine(addressText);
            SelectAddressFromAddressListElement.SelectElementByText(addressText);
            ButtonContinueElement.ClickUsingJavascript();
            Thread.Sleep(3000);
            ContinueBtnOnConfirmAddressElement.ClickUsingJavascript();
        }

        public void SelectValuesForPlanningPortalRef(Dictionary<string, string> testdata)
        {
            waitHelpers wh = new waitHelpers();
            SelectRadioValueLAPortal("Does the property have a planning portal reference?", "No");
            ButtonContinueElement.ClickUsingJavascript();
            SelectRadioValueLAPortal("Why does the property not have a planning portal reference?", "Planning permission is not applicable");
            ButtonContinueElement.ClickUsingJavascript();
            Thread.Sleep(4000);
            IWebElement ele = wh.GetWebElement(By.CssSelector("button[id = 'continue']"));
            ele.Click();
            //continueElement.ClickUsingJavascript();

        }

        public void UserSelectsThePlanningPortalReference(Dictionary<string, string> testdata)
        {
            Thread.Sleep(4000);
            PlanningReference.ClickUsingJavascript();
            Thread.Sleep(2000);
            ButtonContinueElement.ClickUsingJavascript();
            Thread.Sleep(2000);
            PlanningReferenceoption5.ClickUsingJavascript();
            Thread.Sleep(2000);
            ButtonContinueElement.ClickUsingJavascript();
            Thread.Sleep(2000);
            ButtonContinueElement.ClickUsingJavascript();
        }

    }
}
