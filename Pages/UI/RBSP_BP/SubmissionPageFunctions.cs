using NUnit.Framework;
using OpenQA.Selenium;
using POMSeleniumFrameworkPoc1.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace POMSeleniumFrameworkPoc1.Pages.UI
{
    public partial class SubmissionPage : BasePage
    {
        public void OpenSiteMapNewTab()
        {
            SiteMapItemNewTab.Click();
        }

        public void OpenActiveSubmissionTab()
        {
            ActiveSubmissionsTab.Click();
        }

        public void ClickSubmissionMenu()
        {
            SubmissionMenuButton.ClickUsingJavascript();
        }

        public void ClickSubmissionNewPlusBtn()
        {
            NewSubmissionPlusButton.ClickUsingJavascript();
        }

        public void SelectSubmittedBy(string submittedBy)
        {

            ScrollDiv.ScrollUntilSelectorDisplayedAndSendKeys(SubmittedbyLookupInputSelector,
                submittedBy, 10);
            SubmittedByLookUpResultItem.Click();
        }

        public string SaveSubmission()
        {
            SaveSubmissionMenuButton.ClickUsingJavascript();
            SubmissionIdElement.ScrollAndClick();
            Driver.WaitUntilElementHasValue(SubmissionIdSelector, "---", false, 30000);
            SubmissionId = SubmissionIdElement.GetAttribute("title");
            return SubmissionId;
        }

        public void FillIntegrationDataSourceDetails(string IntegrationDataSourceName)
        {
            FillAndSelectLookUpResult(IntegrationDataSourcLookupInput
                , IntegrationDataSourceName);
        }

        public void ClickRelatedTabElementOnSubmissionPage()
        {
            RelatedTabElement.ClickUsingJavascript();
        }

        public void ClickOverFlowBtnUnderRelatedRequestSection()
        {
            OverflowBtnInRelatedRequests.ClickUsingJavascript();
        }

        public void SelectRequestsUnderRelatedTab()
        {
            RequestsUnderRelatedElement.Click();
        }
        public void ClickNewRequestBtnUnderRequestsTab()
        {
            NewRequestBtnUnderRelatedRequestSection.ClickUsingJavascript();

        }
        //****************Ashish's Code 240124************
        public string ClickOnTheLatestSubmission()
        {
            ClickCommandBarOption("Refresh");
            Thread.Sleep(2000);
            string LatestDateTimeValue = SeleniumExtensions.GetLatestRowByDate(ActiveSubmissionsRows, "CreatedOn");
            // Console.WriteLine(LatestDateTimeValue);
            IWebElement SubmissionIDCellValue = SubmissionIDCell(LatestDateTimeValue);
            string SubmissionIDCreated = SubmissionIDCellValue.Text;
            Console.WriteLine(SubmissionIDCreated);
            IWebElement VOANameCellValue = LatestVOANameCell(LatestDateTimeValue);
            VOANameCellValue.ClickUsingJavascript();
            Thread.Sleep(7000);
            return SubmissionIDCreated;


        }

        public void FilterColumnOnSubmissionTable(string columnName, string filterValue)
        {
            Thread.Sleep(5000);
            IWebElement colmnNameToFilter = GetFilterColumn(columnName);
            Assert.IsTrue(SeleniumExtensions.IsElementVisible(colmnNameToFilter));
            colmnNameToFilter.ClickUsingJavascript();
            Assert.IsTrue(SeleniumExtensions.IsElementVisible(FilterBy));
            FilterBy.ClickUsingJavascript();
            Assert.IsTrue(SeleniumExtensions.IsElementVisible(FilterByValue));
            FilterByValue.ClickUsingJavascript();
            IWebElement filterValueElement = SelectFilterValue(filterValue);
            Assert.IsTrue(SeleniumExtensions.IsElementVisible(filterValueElement));
            filterValueElement.ClickUsingJavascript();
            ApplyButton.ClickUsingJavascript();


        }

        public void ClickOnRequestLink()
        {
            waitHelpers wh = new waitHelpers();
            wh.elementDisplayed(RequestLink, 180);
            RequestLink.ElementVisisbleUsingExplicitWait(5);
            RequestLink.ClickUsingJavascript();

        }

        public void ValidateFieldsInDetailsSection(string expSubmissionID, string expIDSval)
        {
            string actualSubID = SeleniumExtensions.ValidateFieldValueInUI(SubmissionID);
            Assert.AreEqual(expSubmissionID, actualSubID);
            string actualIDS = SeleniumExtensions.ValidateFieldtitleInUI(IDS);
            Assert.AreEqual(expIDSval, actualIDS);
        }

        public void ValidateFieldsInRemarksSection(string expRemarksText)
        {
            string actRemarksText = SeleniumExtensions.ValidateFieldValueInUI(Remarks);
            Assert.AreEqual(expRemarksText, actRemarksText);

        }

        public void EnterMandatoryDataInSubmissionForm(string IDSVal, string submittedByVal, string relationshipRoleVal)
        {
            FillAndSelectLookUpResult(IntegrationDataSourcLookupInput, IDSVal);
            FillAndSelectLookUpResult(SubmittedbyLookupInputSelector, submittedByVal);
            FillAndSelectLookUpResult(RelationShipRoleLookUp, relationshipRoleVal);

        }

        public void SelectOverflowOption(string optionValue)
        {
            if (NewRequestBtnUnderRelatedRequestSection.ElementVisisbleUsingExplicitWait(2))
            {
                NewRequestBtnUnderRelatedRequestSection.ClickUsingJavascript();
            }
            else
            {
                MoreCommandsRelatedRequest.ClickUsingJavascript();
                IWebElement OptionToSelect = CommandOptionRelatedRequest(optionValue);
                OptionToSelect.ElementVisisbleUsingExplicitWait(2);
                OptionToSelect.ClickUsingJavascript();
            }

        }

        public void selectProposedDate(IWebElement ele, string dateCaptured)
        {
            DateTime dateToEnter = DateTime.Parse(dateCaptured);
            //M/d/yyyy
            var currentDate_02 = dateToEnter.ToString("d, MMMM, yyyy");
            string[] dateComponent = currentDate_02.Split(',');
            string DayVal = dateComponent[0].Trim();
            string MonthVal = dateComponent[1].Trim();
            string YearVal = dateComponent[2].Trim();
            ele.Click();
            SelectDateFromDialogDatePicker(DayVal, MonthVal, YearVal);
        }

        public void selectPresentationProposedDate(IWebElement ele, string dateCaptured)
        {
            DateTime dateToEnter = DateTime.Parse(dateCaptured);
            //M/d/yyyy
            var currentDate_02 = dateToEnter.ToString("d, MMMM, yyyy");
            string[] dateComponent = currentDate_02.Split(',');
            string DayVal = dateComponent[0].Trim();
            string MonthVal = dateComponent[1].Trim();
            string YearVal = dateComponent[2].Trim();
            ele.Click();
            SelectDateFromPresentationDatePicker(DayVal, MonthVal, YearVal);
        }

        public void selectProposedDate_fuiCal(IWebElement ele, string dateCaptured)
        {
            DateTime dateToEnter = DateTime.Parse(dateCaptured);
            //M/d/yyyy
            var currentDate_02 = dateToEnter.ToString("d, MMMM, yyyy");
            string[] dateComponent = currentDate_02.Split(',');
            string DayVal = dateComponent[0].Trim();
            string MonthVal = dateComponent[1].Trim();
            string YearVal = dateComponent[2].Trim();
            ele.Click();
            SelectDateFromDialogDatePicker_fuiCalender(DayVal, MonthVal, YearVal);
        }

        public void selectProposedDate_new(IWebElement ele, string dateCaptured)
        {
            DateTime dateToEnter = DateTime.Parse(dateCaptured);
            //M/d/yyyy
            var currentDate_02 = dateToEnter.ToString("d, MMMM, yyyy");
            string[] dateComponent = currentDate_02.Split(',');
            string DayVal = dateComponent[0].Trim();
            string MonthVal = dateComponent[1].Trim();
            string YearVal = dateComponent[2].Trim();
            ele.Click();
            SelectDateFromDialogDatePicker_new(DayVal, MonthVal, YearVal);
        }


    }
}
