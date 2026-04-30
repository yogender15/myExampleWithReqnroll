using OpenQA.Selenium;
using POMSeleniumFrameworkPoc1.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POMSeleniumFrameworkPoc1.Pages.UI.LAPortal
{
    public partial class LAPortalDashboardPage : BasePage
    {
        public IWebElement CreateSingleReportLinkElement => Driver.WaitForElement(By.XPath("//a[contains(text(),'Create single reports')]"));
        public IWebElement WhyAreYouCreatingFullDemolitionRadioBtnElement => Driver.WaitForElement(By.CssSelector("input[id='report-reason']"));
        public IWebElement WhyAreYouCreatingNewPropertyRadioBtnElement => Driver.WaitForElement(By.CssSelector("input[id='report-reason-3']"));
        public IWebElement WhyAreYouCreatingPartDemolitionRadioBtnElement => Driver.WaitForElement(By.CssSelector("input[id='report-reason-7']"));
        public IWebElement WhyAreYouCreatingMaterialIncreaseRadioBtnElement => Driver.WaitForElement(By.CssSelector("input[id='report-reason-9']"));
        public IWebElement ButtonContinueElement => Driver.WaitForElement(By.CssSelector("button[id='ButtonContinue']"));
        public IWebElement ReferencesContinueElement => Driver.WaitForElement(By.CssSelector("button[id='ReferencesContinue']"));


        public IWebElement OneResultContinueElement => Driver.WaitForElement(By.CssSelector("button[id='OneResultContinue']"));
        public IWebElement continueElement => Driver.WaitForElement(By.CssSelector("button[id='continue']"));

        public IWebElement continueElement1 => Driver.WaitForElement(By.CssSelector("button[id='ButtonContinue']"));


        public IWebElement CouncilTaxBand => Driver.WaitForElement(By.CssSelector("[id='council-tax-band']"));

        public IWebElement ContinueBtnOnConfirmAddressElement => Driver.WaitForElement(By.CssSelector("button[id='ButtonContinue']"));
        public IWebElement OtherPlanningPortalReferenceNumber => Driver.WaitForElement(By.CssSelector("input[id='planning-reference-other']"));
        public IWebElement PlanningPortalReferenceRadioYesBtnElement => Driver.WaitForElement(By.CssSelector("input[id='planning-reference']"));
        public IWebElement PlanningPortalReferenceRadioNoBtnElement => Driver.WaitForElement(By.CssSelector("input[id='planning-reference-2']"));
        public IWebElement BillingAuthorityReferenceNumberElement
         => Driver.WaitForElement(By.CssSelector("input[id='billing-reference']"));
        public IWebElement PostCodeElement
        => Driver.WaitForElement(By.CssSelector("input[id='postcode']"));
        public IWebElement FindAddressElement => Driver.WaitForElement(By.CssSelector("button[id='FindAddress']"));
        public IWebElement SelectAddressFromAddressListElement =>
        Driver.WaitForElement(By.XPath("//select[contains(@id,'AddressFromPostcode')]"));
        public IWebElement DoesPropertyHavePlanningPortalReferenceRadioBtnElement =>
        Driver.WaitForElement(By.CssSelector("input[id='planning-reference-2']"));

        public IWebElement WhyDoesPropertyNotHavePlanningPortalReasonRadioBtnElement =>
        Driver.WaitForElement(By.CssSelector("input[id='no-planning-reference-1']"));

        public IWebElement BillingAuthorityReportNumber => Driver.WaitForElement(By.CssSelector("input[id='billing-report']"));
        public By BillingAuthorityReferenceNumberTextSelector(string referenceNumber) => By.XPath($"//*[text()='{referenceNumber}']");
        public IWebElement CouncilTaxBandPropertyRadioBtnElement => Driver.WaitForElement(By.CssSelector("input[id='council-tax-band-0']"));
        public IWebElement FirstNameElement => Driver.WaitForElement(By.CssSelector("input[id='first-name']"));
        public IWebElement LastNameElement => Driver.WaitForElement(By.CssSelector("input[id='last-name']"));
        public IWebElement OwnerOrOccupierRadioBtnElement => Driver.WaitForElement(By.CssSelector("input[name='owner-occupier']"));
        public IWebElement AddressTheSameAsThePropertyAddressRadioBtnElement => Driver.WaitForElement(By.CssSelector("input[name='same-address']"));
        public IWebElement EffectiveDayElement => Driver.WaitForElement(By.CssSelector("input[id='effective-date-day']"));
        public IWebElement EffectiveMonthElement => Driver.WaitForElement(By.CssSelector("input[id='effective-date-month']"));
        public IWebElement EffectiveYearElement => Driver.WaitForElement(By.CssSelector("input[id='effective-date-year']"));
        public IWebElement TextareaRemarksTextElement => Driver.WaitForElement(By.CssSelector("textarea[id='textarea_remarks']"));
        public IWebElement ReportReasonCheckDetailsBeforeSendingReportElement => Driver.WaitForElement(By.XPath("//*[text()='Reason for creating report']/parent::*//*[contains(@class,'govuk-summary-list__value')]"));
        public IWebElement BillingPropertyReferenceNumCheckDetailsBeforeSendingReportElement => Driver.WaitForElement(By.XPath("//*[text()='Billing property reference number']/parent::*//*[contains(@class,'govuk-summary-list__value')]"));
        public IWebElement BillingAuthorityReportNumberCheckDetailsBeforeSendingReportElement => Driver.WaitForElement(By.XPath("//*[text()='Billing authority report number']/parent::*//*[contains(@class,'govuk-summary-list__value')]"));
        public IWebElement CouncilTaxBandCheckDetailsBeforeSendingReportElement => Driver.WaitForElement(By.XPath("//dt[text()='Council Tax band']/parent::*//*[contains(@class,'govuk-summary-list__value')]"));
        public IWebElement SingleReportConfirmationMessageElement => Driver.WaitForElement(By.CssSelector("div[class='govuk-panel govuk-panel--confirmation']"));
        public IWebElement NotHavePlanningPortalReferenceErrorMessageElement => Driver.WaitForElement(By.CssSelector("p[id='no-planning-reference-error']"));
        public IWebElement WasBuiltWithoutPlanningPermissionRadioBtnElement => Driver.WaitForElement(By.CssSelector("input[id='no-planning-reference-0']"));
        public IWebElement BillingReferenceErrorElement => Driver.WaitForElement(By.CssSelector("p[id='billing-reference-error']"));
        public IWebElement BillingReportErrorElement => Driver.WaitForElement(By.CssSelector("p[id='billing-report-error']"));

        //***********************Ashish's Code****************************************
        // "//label[contains(text(),'CR01')]//preceding-sibling::input"

        public IWebElement SelectReasonCode(String reasonCode)
        {
            string customizeSelector = $"//label[contains(text(),'{reasonCode}')]//preceding-sibling::input";
            return Driver.WaitForElement(By.XPath(customizeSelector));
        }

        public IWebElement SelectBand(String bandCode)
        {
            string customizeSelector = $"//label[contains(text(),'{bandCode}')]//preceding-sibling::input";
            return Driver.WaitForElement(By.XPath(customizeSelector));
        }

        // "//h1[contains(text(),'Are they the owner or occupier?')]//parent::legend//following-sibling::*[@data-module='govuk-radios']//label[contains(text(),'Owner')]//preceding-sibling::input"

        public IWebElement SelectRadioOptionLAPortal(String radioQuestion, string radioValue)
        {
            string customizeSelector = $"//*[contains(text(),'{radioQuestion}')]//parent::legend//following-sibling::*[@data-module='govuk-radios']//label[contains(text(),'{radioValue}')]//preceding-sibling::input";
            return Driver.WaitForElement(By.XPath(customizeSelector));
        }

        public IWebElement SelectRadioOptionLAPortal_1(String radioQuestion, string radioValue)
        {
            string customizeSelector = $"//*[contains(text(),'{radioQuestion}')]//ancestor::fieldset//following-sibling::*[@data-module='govuk-radios']//label[contains(text(),'{radioValue}')]//preceding-sibling::input";
            return Driver.WaitForElement(By.XPath(customizeSelector));
        }

        //   "//ul[@id='navigation']//li//a[contains(text(),'Sign out')]"
        public IWebElement SignOut => Driver.WaitForElement(By.XPath("//ul[@id='navigation']//li//a[contains(text(),'Sign out')]"));
        public IWebElement ConfirmButton => Driver.WaitForElement(By.CssSelector("button[id='ButtonConfirm']"));

        public IWebElement PlanningReference => Driver.WaitForElement(By.CssSelector("[id='planning-reference-2']"));

        public IWebElement PlanningReferenceoption5 => Driver.WaitForElement(By.CssSelector("[id='no-planning-reference-5']"));

    }
}
