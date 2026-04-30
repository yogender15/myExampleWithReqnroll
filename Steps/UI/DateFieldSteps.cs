using POMSeleniumFrameworkPoc1.Helpers;
using Reqnroll;
using System;
using System.IO;
namespace POMSeleniumFrameworkPoc1.Steps.UI
{

    [Binding]
    public class DateFieldSteps : BasePage
    {
        public DTO.InputOutputData inputoutputdata;
        public ScenarioContext _scenarioContext;
        public FeatureContext _featureContext;

        public DateFieldSteps(ScenarioContext scenarioContext, FeatureContext featureContext,
            DTO.InputOutputData _inputoutputdata)
        {
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
            string TestDataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + Config.TestDataExcelFilePath);
            this.inputoutputdata = _inputoutputdata;
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
        }

        [Given(@"User entered (.*) days before date for '(.*)' field value")]
        public void GivenUserEnteredDaysBeforeDateForFieldValue(int numberOfDays, string fieldName)
        {
            try
            {
                _featureContext[fieldName] = SeleniumExtensions.enterBeforeDateSequentially(numberOfDays, fieldName);
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

        [Given(@"User entered (.*) days after date for '(.*)' field value")]
        public void GivenUserEnteredDaysAfterDateForFieldValue(int numberOfDays, string fieldName)
        {
            try
            {
                _featureContext[fieldName] = SeleniumExtensions.enterAfterDateSequentially(numberOfDays, fieldName);
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

        [Given(@"User entered date for '(.*)' field value from '(.*)' for '(.*)'")]
        public void GivenUserEnteredDateForFieldValueFromFor(string fieldName, string sheetName, string testDataRow)
        {
            {
                try
                {
                    SeleniumExtensions.enterDateSequentially(fieldName, sheetName, testDataRow);
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

        [Given(@"User entered (.*) days before date of '(.*)' for '(.*)' field value")]
        public void GivenUserEnteredDaysBeforeDateOfForFieldValue(int noDays, string BaseProposedEffectiveDate, string fieldName)
        {
            try
            {
                _featureContext[fieldName] = SeleniumExtensions.enterBeforeDateToGoLegeslativeDateSequentially(noDays, BaseProposedEffectiveDate, fieldName);
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

        [Given(@"User entered (.*) days before hereditament '(.*)' for  from calender for '(.*)' field value on '(.*)'")]
        public void GivenUserEnteredDaysBeforeHereditamentForFromCalenderForFieldValueOn(int numberOfDays, string hereditamentDate, string fieldName, string roleType)
        {
            try
            {
                String hereditamentEffectiveDate = (string)_featureContext[hereditamentDate];
                _featureContext[fieldName] = SeleniumExtensions.enterBeforeCalenderDateSameAsEffectiveDate(numberOfDays, hereditamentEffectiveDate, fieldName, roleType);
                //SeleniumExtensions.enterBeforeCalenderDateSequentially(numberOfDays, fieldName, roleType);
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
        [Given(@"User entered (.*) days before from calender for '(.*)' field value on '(.*)'")]
        public void GivenUserEnteredDaysBeforeFromCalenderForFieldValueOn(int numberOfDays, string fieldName, string roleType)
        {
            try
            {
                DateTime _futureDate = DateTime.Now - TimeSpan.FromDays(numberOfDays);
                String value = _futureDate.ToString("M/d/yyyy");
                _featureContext[fieldName] = SeleniumExtensions.enterAfterCalenderDateSameAsEffectiveDate(numberOfDays, value, fieldName, roleType);
                //SeleniumExtensions.enterBeforeCalenderDateSequentially(numberOfDays, fieldName, roleType);
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


        [Given(@"User entered (.*) days after hereditament '(.*)' for  from calender for '(.*)' field value on '(.*)'")]
        public void GivenUserEnteredDaysAfterHereditamentForFromCalenderForFieldValueOn(int numberOfDays, string hereditamentDate, string fieldName, string roleType)
        {
            try
            {
                String hereditamentEffectiveDate = null;
                if (hereditamentDate.Equals("BaseProposedEffectiveDate"))
                {
                    _featureContext["BaseProposedEffectiveDate"] = Config.BaseProposedEffectiveDate;
                    hereditamentEffectiveDate = (string)_featureContext["BaseProposedEffectiveDate"];
                }
                else
                {
                    hereditamentEffectiveDate = (string)_featureContext[hereditamentDate];
                }

                _featureContext[fieldName] = SeleniumExtensions.enterAfterCalenderDateSameAsEffectiveDate(numberOfDays, hereditamentEffectiveDate, fieldName, roleType);
                //SeleniumExtensions.enterBeforeCalenderDateSequentially(numberOfDays, fieldName, roleType);
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

        [Given(@"User enters same date as '(.*)' for '(.*)' field value on '(.*)'")]
        public void GivenUserEntersSameDateAsForFieldValueOn(string proposedEffectiveDate, string fieldName, string roleType)
        {
            try
            {
                String PEDate = (string)_featureContext[proposedEffectiveDate];
                SeleniumExtensions.enterBeforeCalenderDateSequentially(PEDate, fieldName, roleType);
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



        [Given(@"User entered (.*) days before date from calender for '(.*)' field value on '(.*)'")]
        public void GivenUserEnteredDaysBeforeDateFromCalenderForFieldValueOn(int numberOfDays, string fieldName, string roleType)
        {
            try
            {
                SeleniumExtensions.enterBeforeCalenderDateSequentially(numberOfDays, fieldName, roleType);
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


        [Given(@"User entered (.*) days before date for '(.*)' field value on '(.*)'")]
        public void GivenUserEnteredDaysBeforeDateForFieldValueOn(int numberOfDays, string fieldName, string roleType)
        {
            try
            {
                SeleniumExtensions.enterBeforeDateSequentially(numberOfDays, fieldName, roleType);
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

        [Given(@"User enters '(.*)' days before hereditament '(.*)' for '(.*)' field value")]
        public void GivenUserEntersDaysBeforeHereditamentForFieldValue(int numberOfDays, string hereditamentDate, string fieldName)
        {
            try
            {
                String hereditamentEffectiveDate = (string)_featureContext[hereditamentDate];
                _featureContext[fieldName] = SeleniumExtensions.enterBeforeDateOfHereditamentEffectiveDateSequentially(numberOfDays, hereditamentEffectiveDate, fieldName);
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
        [Given("User enters {string} days before hereditament {string} for {string} field value as {string}")]
        public void GivenUserEntersDaysBeforeHereditamentForFieldValueAs(int numberOfDays, string hereditamentDate, string fieldName, string proposedEffectiveDateofEDC)
        {
            try
            {
                String hereditamentEffectiveDate = (string)_featureContext[hereditamentDate];
                _featureContext[proposedEffectiveDateofEDC] = SeleniumExtensions.enterBeforeDateOfHereditamentEffectiveDateSequentially(numberOfDays, hereditamentEffectiveDate, fieldName);
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
        [Given("User enters {string} days after hereditament {string} for {string} field value as {string}")]
        public void GivenUserEntersDaysAfterHereditamentForFieldValueAs(int numberOfDays, string hereditamentDate, string fieldName, string proposedEffectiveDateofdeletion)
        {
            try
            {
                String hereditamentEffectiveDate = (string)_featureContext[hereditamentDate];
                _featureContext[proposedEffectiveDateofdeletion] = SeleniumExtensions.enterAfterDateOfHereditamentEffectiveDateSequentially(numberOfDays, hereditamentEffectiveDate, fieldName);
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

        [Given("User enters {string} days after hereditament {string} for {string} field value as {string} for EDC job")]
        public void GivenUserEntersDaysAfterHereditamentForFieldValueAsForEDCJob(int numberOfDays, string hereditamentDate, string fieldName, string proposedEffectiveDateofEDC)
        {
            try
            {
                String hereditamentEffectiveDate = (string)_featureContext[hereditamentDate];
                _featureContext[proposedEffectiveDateofEDC] = SeleniumExtensions.enterAfterDateOfHereditamentEffectiveDateSequentially(numberOfDays, hereditamentEffectiveDate, fieldName);
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

        [Given(@"User enters random days after hereditament '(.*)' for '(.*)' field value")]
        public void GivenUserEntersRandomDaysAfterHereditamentForFieldValue(string hereditamentDate, string fieldName)
        {
            try
            {
                String hereditamentEffectiveDate = (string)_featureContext[hereditamentDate];
                _featureContext[fieldName] = SeleniumExtensions.enterAfterDateOfHereditamentEffectiveDateSequentially(hereditamentEffectiveDate, fieldName, _featureContext);
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



        [Given(@"User enters '(.*)' days after hereditament '(.*)' for '(.*)' field value")]
        public void GivenUserEntersDaysAfterHereditamentForFieldValue(int numberOfDays, string hereditamentDate, string fieldName)
        {
            try
            {
                String hereditamentEffectiveDate = (string)_featureContext[hereditamentDate];
                _featureContext[fieldName] = SeleniumExtensions.enterAfterDateOfHereditamentEffectiveDateSequentially(numberOfDays, hereditamentEffectiveDate, fieldName);
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

        [Given(@"User enters '(.*)' days after hereditament '(.*)' for '(.*)' field value for '(.*)' request")]
        public void GivenUserEntersDaysAfterHereditamentForFieldValueForRequest(int numberOfDays, string hereditamentDate, string fieldName, string jobType)
        {
            try
            {
                String hereditamentEffectiveDate = (string)_featureContext[hereditamentDate];
                _featureContext[jobType + "_" + fieldName] = SeleniumExtensions.enterAfterDateOfHereditamentEffectiveDateSequentially(numberOfDays, hereditamentEffectiveDate, fieldName); var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
            }
            catch (Exception e)
            {
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
                pdf_Util.exceptionPdFLogger(e);
            }
        }

        [Given(@"User enter '(.*)' as '(.*)' mins before current time")]
        public void GivenUserEnterAsMinsBeforeCurrentTime(string fieldAriaLabel, int timeInMin)
        {
            try
            {
                SeleniumExtensions.clearAndEnterBeforeTime(fieldAriaLabel, timeInMin);
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


        [Given(@"User enter '(.*)' as '(.*)' mins after current time")]
        public void GivenUserEnterAsMinsAfterCurrentTime(string fieldAriaLabel, int timeInMin)
        {
            try
            {
                SeleniumExtensions.clearAndEnterTime(fieldAriaLabel, timeInMin);
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

        [Given(@"User entered (.*) days before date for '(.*)' field value in (.*) position")]
        public void GivenUserEnteredDaysBeforeDateForFieldValueInPosition(int numberOfDays, string fieldName, int elePosition)
        {
            try
            {
                _featureContext[fieldName] = SeleniumExtensions.enterBeforeDateSequentially(numberOfDays, fieldName, elePosition);
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
