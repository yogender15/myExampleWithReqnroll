using POMSeleniumFrameworkPoc1.DTO;
using POMSeleniumFrameworkPoc1.Pages.UI.RBSP_BP;
using Reqnroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POMSeleniumFrameworkPoc1.Steps.UI
{
    [Binding]
    public class CustomerEnquirySteps
    {
        public DTO.InputOutputData inputoutputdata;
        public ScenarioContext _scenarioContext;
        public FeatureContext _featureContext;
        public NewJob newJobInformation;

        public CustomerEnquirySteps(ScenarioContext scenarioContext, FeatureContext featureContext,
             NewJob _newJob, NewJob _newRequestId, DTO.InputOutputData _inputoutputdata)
        {
            this.inputoutputdata = _inputoutputdata;
            this.newJobInformation = _newJob;
            this.newJobInformation = _newRequestId;
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
        }

        [Given(@"User creates a New Supplimentary Record for Request Type as '(.*)' and Job Type as '(.*)' and creates the Request for Customer Enquiry")]
        public void GivenUserCreatesANewSupplimentaryRecordForRequestTypeAsAndJobTypeAsAndCreatesTheRequest(string requestType, string jobType)
        {
            var testData = inputoutputdata.testData;
            CustomerEnquiryPage customerEnquiryPage = new CustomerEnquiryPage();
            customerEnquiryPage.EnterDetailsOnCustEnquScreenAndCreateNewSuppRecord(requestType, jobType, testData, (string)_featureContext["town"], (string)_featureContext["postcode"], (string)_featureContext["uprn"]);

        }

        [Given(@"User enters details on the New Customer Enquiry Screen for INF")]
        public void GivenUserEntersDetailsOnTheNewCustomerEnquiryScreenForINF()
        {
            var testData = inputoutputdata.testData;
            CustomerEnquiryPage customerEnquiryPage = new CustomerEnquiryPage();
            customerEnquiryPage.EnterDetailsOnCustEnquiryScreenforInfchall(testData, (string)_featureContext["town"], (string)_featureContext["postcode"], (string)_featureContext["uprn"]);
        }


        [Given(@"User enters details on the New Customer Enquiry Screen")]
        public void GivenUserEntersDetailsOnTheNewCustomerEnquiryScreen()
        {
            var testData = inputoutputdata.testData;
            CustomerEnquiryPage customerEnquiryPage = new CustomerEnquiryPage();
            customerEnquiryPage.EnterDetailsOnCustEnquiryScreen(testData, (string)_featureContext["town"], (string)_featureContext["postcode"], (string)_featureContext["uprn"]);

        }


        [Then(@"User creates new '(.*)' Supplementary job under '(.*)'")]
        public void ThenUserCreatesNewSupplementaryJob(string jobType, string requestType)
        {
            var testData = inputoutputdata.testData;
            CustomerEnquiryPage customerEnquiryPage = new CustomerEnquiryPage();
            customerEnquiryPage.CreateNewSupplementaryJob(requestType, jobType, (string)_featureContext["town"], (string)_featureContext["postcode"], (string)_featureContext["uprn"]);

        }

        [Given(@"User selects '(.*)' as Status Reason and '(.*)' the Enquiry")]
        public void GivenUserSelectsAsStatusReasonAndTheEnquiry(string statusReason, string setStateOFEnquiry)
        {
            var testData = inputoutputdata.testData;
            CustomerEnquiryPage customerEnquiryPage = new CustomerEnquiryPage();
            customerEnquiryPage.SetStatusReasonAndEnquiryState(statusReason, setStateOFEnquiry);

        }

        [Then(@"User creates the new '(.*)' Supplementary job under '(.*)'")]
        public void ThenUserCreatestheNewSupplementaryJob(string jobType, string requestType)
        {
            var testData = inputoutputdata.testData;
            CustomerEnquiryPage customerEnquiryPage = new CustomerEnquiryPage();
            _featureContext["expEffectiveChangedDate"] = customerEnquiryPage.CreateNewSupplementaryJobforEnquiry(requestType, jobType, (string)_featureContext["town"], (string)_featureContext["postcode"], (string)_featureContext["uprn"]);

        }

        [Given(@"User validates the Supplementary Job Creation status as '(.*)' and click on Request Link")]
        public void GivenUserValidatesTheSupplementaryJobCreationStatusAsAndClickOnRequestLink(string statusText)
        {

            CustomerEnquiryPage customerEnquiryPage = new CustomerEnquiryPage();
            customerEnquiryPage.ValidateSupplementaryJobStatusAndClickOnReqLink(statusText);
            var testData = inputoutputdata.testData;
            string dateCaptured = "2/18/2025 12:00:00 AM";
            string[] dateValue = dateCaptured.Split(' ');
            DateTime dateToEnter = DateTime.Parse(dateValue[0]);
            string proposedEffectiveDateChanged = dateToEnter.ToString("M/d/yyyy");
            customerEnquiryPage.UpdateMissingRecordsOnRequest(testData, testData["AuthorisationDecision"], testData["ReasonForValidation"], testData["DSR"], proposedEffectiveDateChanged);
        }


    }
}
