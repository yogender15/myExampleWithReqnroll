using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using POMSeleniumFrameworkPoc1.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Reqnroll;


namespace POMSeleniumFrameworkPoc1.Pages.UI.CommonComponents
{
    public partial class DesktopResearchPage : BasePage
    {
        public void EnterDetailsTabInfo(string option)
        {
            DetailsOptionTab.ClickUntilNoClickInterruptable();
            AutoProcessDropDown.SelectElementByText(option);
            Driver.WaitForElementToDissapear(DesktopOptionTab, 500);
            DetailsNextStageButton.Click();
        }

        public void ClickNextStageButtonInDetailsBPF()
        {
            System.Threading.Thread.Sleep(8000);
            NextStageButtonOnBPF.ClickUsingJavascript();
            //if(CloseButtonOnProcessStage.IsElementDisplayed(5))
            // {
            //     DetailsNextStageButton.Click();
            // }
            // System.Threading.Thread.Sleep(6000);
            //  VerifyDuplicateRecords();
            //System.Threading.Thread.Sleep(6000);

        }

        public void ClickCloseBtn()
        {
            PADCodeCorrectElement.IsElementDisplayed(5);
            CloseButtonElement.Click();
        }

        public void ClickNextStageButtonOnBPF()
        {
            System.Threading.Thread.Sleep(4000);
            NextStageButtonOnBPF.Click();
            //VerifyDuplicateRecords();
        }
        public void PickDesktopResearchDate(float days)
        {
            var currentDate = DateTime.Now.AddDays(days).ToString("dd/MM/yyyy");
            DektopResearchScrollDiv.ScrollUntilSelectorDisplayedAndSendKeys(DesktopResearchDateSelector, currentDate, 10);
        }

        public void DesktopResearchCompleteDropdown()
        {
            IsDesktopResearchComplete.SelectElementByText("Yes");
        }
        public void ReasonForInspectionDropdown(string reasonForInspection)
        {
            ReasonForInspection.SelectElementByText(reasonForInspection);
        }
        public void ReasonForInspectionInMoreDetail(string reasonForInSpectionInDetail)
        {
            ReasonForInspectionInDetail.SendKeys(reasonForInSpectionInDetail);
        }
        public void IsInspectionRequiredDropdown()
        {
            IsInspectionRequired.SelectElementByText("Yes");
        }
        public void SelectPADCodeCorrectDropdown()
        {
            PADCodeCorrectElement.SelectElementByText("Yes");
        }
        public void SelectAffectedAssessmentConfirmedDropdown()
        {
            if (AffectedAssessmentConfirmedDropdownElement.IsElementDisplayed(10))
            {
                AffectedAssessmentConfirmedDropdownElement.SelectElementByText("Yes");
            }

        }
        public void SelectCorrespondanceChecksCompleteDropdown()
        {
            if (CorrespondanceChecksCompleteDropdownElement.IsElementDisplayed(10))
            {
                CorrespondanceChecksCompleteDropdownElement.SelectElementByText("Yes");
            }
        }

        public bool VerifyAddressGridOnDeskTopReasearch()
        {
            return VerifyAddressGridElement.Displayed;
        }

        public void MoveTheToggleIstheBAReferenceMissing()
        {
            GeneraltabScrollDiv.ScrollUntilElementIsDisplayed(By.CssSelector("button[aria-label='Is the BA Reference Missing?: No']"), 20);
            IstheBAReferenceMissing.ScrollAndClick();
        }
        public void EnterBAReferenceMissingComments(string comments)
        {
            BAReferenceMissingComments.ScrollAndClick();
            BAReferenceMissingComments.SendKeys(comments);
        }

        public void MoveTheToggleBAReferenceRequestedFromLocalAuthority()
        {
            BAReferenceRequestedFromLocalAuthorityElement.Click();
        }

        public bool VerifySaveStatusIsDisplayed()
        {
            return SaveStatusElement.Displayed;
        }

        public void ClickSaveBtnOnMainNav()
        {
            SavebtnElement.Click();
        }

        public void ClickTCTRInformationTab()
        {
            TCTRInformationTab.Click();
        }

        public void SelectAreTCTRResponsesComplete(string TCTRResponse)
        {
            AreTCTRResponsesComplete.SelectElementByText(TCTRResponse);
        }

        public void ClickResearchingBPFTab()
        {
            System.Threading.Thread.Sleep(5000);
            if (ResearchingBPFTabElement.IsElementDisplayed(10))
            {
                ResearchingBPFTabElement.Click();
            }

        }

        public void SelectReasonForIncompleteTCTRResponse(string reason)
        {
            ReasonForIncompleteTCTRResponse.Click();
            ReasonForIncompleteTCTRResponseDropdownElement.SelectElementByText(reason);
        }

        public void SelectCreatedJobIdInDetailsTabNextStage(string JobId)
        {
            if (Driver.WaitForElement(DesktopResearchDetailsSelectionSelector(JobId)).IsElementDisplayed(4))
            {
                Driver.WaitUntilElementWithSelectorIsDisplayed(DesktopResearchDetailsSelectionSelector(JobId), 20000).ClickUntilNoClickInterruptable();
            }
            else
            {
                var desktopResearchPage = new DesktopResearchPage();
                desktopResearchPage.ClickNextStageButtonOnBPF();
            }
        }

        public void SelectMaintainAssessmentOnBPF()
        {
            // System.Threading.Thread.Sleep(5000);
            MaintainAssessmentElementList.FirstOrDefault().Click();
        }

        public void ClickFinishBtnOnReleaseAndPublishBPF()
        {
            FinishReleaseAndPublishBtnBPFElement.ClickUntilNoClickInterruptable();
        }

        public void SelectMaintainAssessmentCompleteDrpBPF(string yesNoFlag)
        {
            SeleniumExtensions.SelectElementByText(MaintainAssessmentCompleteDrpBPFElement, yesNoFlag);

        }

        public bool IsSystemRequiresQualityAssuranceOptionDisplayed()
        {
            return SystemRequiresQualityAssurance.IsElementDisplayed(10);
        }

        public void SelectCompositeChangeTypeDropdown(string dropDownValue, Dictionary<string, string> testdata)
        {
            if (CompositeChangeTypeDropdownElement.IsElementDisplayed(10))
            {
                CompositeChangeTypeDropdownElement.SelectElementByText(testdata[dropDownValue]);
            }

        }

        public void ClickOnChevronOnPVTTab(string ChevronName)
        {

            ChevronRightMed.ClickUsingJavascript();
            StatusCircleCheckmark.ClickUsingJavascript();
            SelectAssessment.ClickUsingJavascript();

        }

        public void CheckHasAssociatedMaterialIncrease(string materialIncreaseValue)
        {
            var MaterialAttributevalue = MaterialIncreaseIndicator.GetAttribute("title").ToString().Trim();
            Assert.AreEqual(materialIncreaseValue, MaterialAttributevalue);
        }

        public void SelectingDropDownValueforComposite(string dropDownValue, Dictionary<string, string> testdata)
        {
            string linkedAssessmentAvailable = "";
            if (dropDownValue == "LinkedAssessmentConfirmedDropdown")
            {
                SeleniumExtensions.scrollToBtnElement("Special Process Codes");

                if (LinkedAssessmentConfirmedLinkCurrentElement.IsElementDisplayed(3))
                {
                    SeleniumExtensions.ScrollToElement(LinkedAssessmentConfirmedLinkCurrentElement);
                    linkedAssessmentAvailable = LinkedAssessmentConfirmedLinkCurrentElement.Text;
                }
                else if (LinkedAssessmentConfirmedLinkPreviousElement.IsElementDisplayed(3))
                {
                    SeleniumExtensions.ScrollToElement(LinkedAssessmentConfirmedLinkPreviousElement);
                    linkedAssessmentAvailable = LinkedAssessmentConfirmedLinkPreviousElement.Text;
                }
                else if (NoLinkedAssessment.IsElementDisplayed(3))
                {
                    ValidateAndClickOnTab("PVT", "Desktop Research Form");
                    ValidateAndClickOnPVTTab("Assessments");
                    ClickOnChevronOnPVTTab("ChevronRightMed");
                    ValidateAndClickOnTab("Check Facts", "Desktop Research Form");
                    Console.WriteLine("The User has added new Assessment");
                }

                if (linkedAssessmentAvailable.Contains("Current") || linkedAssessmentAvailable.Contains("Previously"))
                {
                    //Assert.AreEqual("Yes", CompositeIndicatorValue
                    Console.WriteLine("The Assessment is prefilled on the page");

                }
            }
            else if (dropDownValue == "CompositeChangeTypeDropdown")
            {
                SeleniumExtensions.scrollToBtnElement("Composite Change Type");
                CompositeChangeTypeDropdownElement.IsElementDisplayed(6);
                SeleniumExtensions.SelectDropdownValue(CompositeChangeTypeDropdownElement, testdata[dropDownValue]);
                //CompositeChangeTypeDropdownElement.SelectElementByText(testdata[dropDownValue]);
                var Value = testdata[dropDownValue];
                clickOnMainMenuMoreElement_New("Save");
                if (Value == "Previously Wholly Domestic Now Composite" || Value == "Decrease in Domestic Use of Composite" || Value == "Increase in Domestic Use of Composite")
                {
                    RelatedCompositePropertyElement.IsElementDisplayed(6);
                    RelatedCompositePropertyValueElement.ClearAndSendkeys(testdata["RelatedCompositeProperty"]);
                    clickOnMainMenuMoreElement_New("Save");
                    waitTillSavingDisaddpears("Saving...", "progressbar");
                    //Thread.Sleep(20000);
                    CompositeIndicatorValueElement.IsElementDisplayed(6);
                    string CompositeIndicatorValue = CompositeIndicatorValueElement.GetAttribute("value");
                    Assert.AreEqual("Yes", CompositeIndicatorValue);
                }
                else
                {
                    RelatedCompositePropertyElement.IsElementDisplayed(6);
                    RelatedCompositePropertyValueElement.ClearAndSendkeys(testdata["RelatedCompositeProperty"]);
                    clickOnMainMenuMoreElement_New("Save");
                    CompositeIndicatorValueElement.IsElementDisplayed(6);
                    string CompositeIndicatorValue = CompositeIndicatorValueElement.GetAttribute("value");
                    Assert.AreEqual("No", CompositeIndicatorValue);

                }

            }

        }

        public bool IsLinkWorking(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.AllowAutoRedirect = true;
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    response.Close();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public void validateKnowledgeSectionUrls(Table table)
        {
            SeleniumExtensions.WaitForReadyStateToComplete();
            WebDriverWait wait = new WebDriverWait(DriverHelper.Driver, TimeSpan.FromSeconds(240));
            //List<String> KnowledgeSectionUrls = new List<String>();
            String knowledge_BackBtn = $"//*[@data-id='Knowledge_Articles-ContentPaneBackButtonContainer']/parent::div";
            By knowledge_BackBtnBy = SeleniumExtensions.getLocator(knowledge_BackBtn);
            foreach (var row in table.Rows)
            {
                string sectionName = row["KnowledgeSection"];
                String knowledgeSectionUrl = $"//a[text()='{sectionName}']";
                String knowledgeSection = $"//a[@aria-label='{sectionName}']";
                By sectionBy = SeleniumExtensions.getLocator(knowledgeSection);
                By knowledgeSectionSharePointLinkBy = SeleniumExtensions.getLocator(knowledgeSectionUrl);
                SeleniumExtensions.WaitForElementAndClick(sectionBy);
                var url = SeleniumExtensions.waitForElementToBeDisplayed(knowledgeSectionSharePointLinkBy, 120).GetAttribute("href");
                IsLinkWorking(url);
                SeleniumExtensions.WaitForElementAndClick(knowledge_BackBtnBy);
            }
            //return KnowledgeSectionUrls;
        }

        public void UserUpdatesTheCheckFactsAndPVTValues(Dictionary<string, string> testdata)
        {
            SeleniumExtensions.scrollToBtnElement("Has relevant transaction? ");
            HasRelevantTransactionDropdownElement.IsElementDisplayed(10);
            HasRelevantTransactionDropdownElement.SelectElementByText("No");
            ClickCommandBarOption("Save");
            ValidateAndClickOnTab("PVT", "Desktop Research Form");
            ChevronRightMed.ClickUsingJavascript();
            StatusCircleCheckmark.ClickUsingJavascript();

        }



        public void UserSetstheAutoProcess(Dictionary<string, string> testdata)
        {
            ValidateForRelease.IsElementDisplayed(10);
            ValidateForRelease.SelectElementByText("Yes");
            ClickCommandBarOption("Save");
            QAQCContinue.ClickUsingJavascript();
            ClickCommandBarOption("Refresh");
        }

        public void UserNavigatesToHereditamentsDetails(string BAValue)
        {
            SeleniumExtensions.scrollToBtnElement("Hereditament Details");
            if (HereditamentLink.IsElementDisplayed(3))
            {
                HereditamentLink.ClickUsingJavascript();
                var requestPage = new RequestPage();
                requestPage.ClickOnOptionInRelatedTab(BAValue);
            }
        }
        public void UserClicksOnHereditamentLink(string tabName, string FormName)
        {
            ValidateAndClickOnTab(tabName, FormName);
            SeleniumExtensions.scrollToBtnElement("Hereditament Details");
            if (HereditamentLink.IsElementDisplayed(7))
            {
                HereditamentLink.ClickUsingJavascript();
            }
        }

        public void SelectValidateForReleaseValue(string selectValue)
        {
            ValidateForRelease.IsElementDisplayed(10);
            //  ValidateForRelease.SelectElementByText(selectValue);
            SeleniumExtensions.SelectDropdownValue(ValidateForRelease, "Yes");
            // ClickCommandBarOption("Save");
            ClickBtnOnAllJobsCreated(selectValue);
            //  QAQCContinue.ClickUsingJavascript();
            //  ValidateSaveInProgressDialog();
            //  ClickCommandBarOption("Refresh");
            System.Threading.Thread.Sleep(5000);

        }

        public void SelectSupllementaryFromAssociatedJobTab(string expaddressString)
        {
            IList<IWebElement> AddressStringElements = Driver.FindElements(HereditamentLinkOnAssociatedJob);
            int rowsCount = AddressStringElements.Count;
            foreach (var row in AddressStringElements)
            {
                //*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row')]
                string addressString = row.GetAttribute("aria-label");
                string rowindex = DriverHelper.Driver.WaitForElement(By.XPath("//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row')]")).GetAttribute("row-index");
                IWebElement effectiveDateelement = DriverHelper.Driver.WaitForElement(By.XPath($"//*[@class='ag-center-cols-container' and @role='rowgroup']//*[@row-index='{rowindex}']//*[@col-id='voa_effectivedate']"));

                if (!addressString.Equals(expaddressString))
                {
                    effectiveDateelement.DoubleClickElementUsingJSExecutor();
                }

            }
        }

        public string GetAddressStringFromHereditamentLink()
        {
            string addressString = null;
            waitHelpers wh = new waitHelpers();
            SeleniumExtensions.ScrollToElement(HereditamentDetailsSection);
            wh.WaitForElementToBeDisplayed(HereditamentLinkUsingBy, 10);
            SeleniumExtensions.ScrollToElement(HereditamentLink);
            if (HereditamentLink.IsElementDisplayed(3))
            {
                addressString = HereditamentLink.GetAttribute("title");
            }
            return addressString;
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
