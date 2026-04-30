using NUnit.Framework;
using POMSeleniumFrameworkPoc1.DTO;
using POMSeleniumFrameworkPoc1.DTO.UI;
using POMSeleniumFrameworkPoc1.Pages.UI.RBSP_BP;
using Reqnroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POMSeleniumFrameworkPoc1.Steps.UI
{
    [Binding]
    public class EnquirySteps
    {
        public ScenarioContext _scenarioContext;
        public NewJob newJobInformation;
        public NewEnquiry newEnquiryInformation;
        public EnquirySteps(ScenarioContext context, NewJob _newJob, NewEnquiry _newEnquiry)
        {
            this.newJobInformation = _newJob;
            this.newEnquiryInformation = _newEnquiry;
            _scenarioContext = context;
        }

        [When(@"User select the Has Customer Used Website Or Digital Service\? dropdown as ""(.*)""")]
        public void WhenUserSelectTheHasCustomerUsedWebsiteOrDigitalServiceDropdownAs(string WebDigitalDrp)
        {
            var enquiriesPage = new EnquiresPage();
            enquiriesPage.SelectHasCustomerUsedWebsiteOrDigitalServiceDropDown(WebDigitalDrp);
        }

        [When(@"User fills Party / Proposer in Caller Information as ""(.*)""")]
        public void WhenUserFillsPartyProposerInCallerInformationAs(string partyProposer)
        {
            var enquiriesPage = new EnquiresPage();
            enquiriesPage.FillPartProposerOnEnquiriesPage(partyProposer);
        }

        [When(@"I click Save button on Enquiries Page")]
        public void WhenIClickSaveButtonOnEnquiriesPage()
        {
            var enquiriesPage = new EnquiresPage();
            newEnquiryInformation.EnquiryId = enquiriesPage.GetEnquiryIdOnEnquiryPage();
            enquiriesPage.ClickSaveBtnOnEnquiriesPage();
        }

        [When(@"I click CT Proposals Tab On Enquiries Page")]
        public void WhenIClickCTProposalsTabOnEnquiriesPage()
        {
            var enquiriesPage = new EnquiresPage();
            enquiriesPage.ClickCTProposalsTabOnEnquiriesPage();
        }

        [When(@"I select Begin Proposal Triage option to Yes")]
        public void WhenISelectBeginProposalTriageOptionToYes()
        {
            var enquiriesPage = new EnquiresPage();
            enquiriesPage.ClickBeginProposalTriageCTProposalsPage();
        }

        [When(@"I select Proposal Triage Outcome as Fail")]
        public void WhenISelectProposalTriageOutcomeAsFail()
        {
            var enquiriesPage = new EnquiresPage();
            enquiriesPage.ClickProposalTriageOutcomeFailCTProposalsPage();
        }

        [When(@"I select Proposal Triage Outcome as Pass")]
        public void WhenISelectProposalTriageOutcomeAsPass()
        {
            var enquiriesPage = new EnquiresPage();
            enquiriesPage.ClickProposalTriageOutcomePassCTProposalsPage();
        }

        [When(@"I select Proposal Validity Decision as invalid")]
        public void WhenISelectProposalValidityDecisionAsInvalid()
        {
            var enquiriesPage = new EnquiresPage();
            enquiriesPage.ClickProposalValidityDecisionInvalidCTProposalsPage();
        }

        [When(@"I select Proposal Validity Decision as Valid")]
        public void WhenISelectProposalValidityDecisionAsValid()
        {
            var enquiriesPage = new EnquiresPage();
            enquiriesPage.ClickProposalValidityDecisionValidCTProposalsPage();
        }

        [When(@"I select Invalidity Decision Reason as ""(.*)""")]
        public void WhenISelectInvalidityDecisionReasonAs(string drpItem)
        {
            var enquiriesPage = new EnquiresPage();
            enquiriesPage.FillInvalidityDecisionReasonDrpCTProposalsPage(drpItem);
        }

        [When(@"I select Does the Customer want to raise a CR15 as ""(.*)""")]
        public void WhenISelectDoesTheCustomerWantToRaiseACRAs(string reason)
        {
            var enquiriesPage = new EnquiresPage();
            enquiriesPage.FillDoesTheCustomerWantToRaiseACR15CTProposalsPage(reason);
        }

        [When(@"I select Generate Job option as Yes")]
        public void WhenISelectGenerateJobOptionAsYes()
        {
            var enquiriesPage = new EnquiresPage();
            enquiriesPage.ClickGenerateJobOptionYesOnEnquiriesPage();
        }

        [When(@"User fill the property as (.*)")]
        public void WhenUserFillThePropertyAs(string property)
        {
            var enquiriesPage = new EnquiresPage();
            enquiriesPage.FillPropertyDetails(property);
        }

        [Then(@"I should see the enquiry created")]
        public void ThenIShouldSeeTheEnquiryCreated()
        {
            var enquiriesPage = new EnquiresPage();
            newEnquiryInformation.EnquiryId = enquiriesPage.CaptureEnquiryIdCreatedOnEnquiryPage();
            Assert.IsTrue(enquiriesPage.VerifyEnquiryIdFieldContainsId(), "the enquiry ID is not created");
        }

        [When(@"I click CCYCTB Data tab")]
        public void WhenIClickCCYCTBDataTab()
        {
            var enquiriesPage = new EnquiresPage();
            enquiriesPage.ClickCCYCTBDataTabEnquiriesPage();
        }

        [Then(@"I should see ""(.*)"", ""(.*)"", ""(.*)"", ""(.*)"" headers under CCYCTB Data tab")]
        public void ThenIShouldSeeHeadersUnderCCYCTBDataTab(string header1, string header2, string header3, string header4)
        {
            var enquiriesPage = new EnquiresPage();
            Assert.IsTrue(enquiriesPage.VerifyHeadersUnderCCYCTBDataTabAreValid(header1, header2, header3, header4), "The headers are not displayed");
        }

        [When(@"User fill Request type on Enquiry Page as ""(.*)""")]
        public void WhenUserFillRequestTypeOnEnquiryPageAs(string requestType)
        {
            var enquiriesPage = new EnquiresPage();
            enquiriesPage.FillRequestTypeOnEnquiryCreation(requestType);
        }

        [When(@"User fill Coded Reason on Enquiries page as ""(.*)""")]
        public void WhenUserFillCodedReasonOnEnquiriesPageAs(string codedReason)
        {
            var enquiriesPage = new EnquiresPage();
            enquiriesPage.FillCodedReasonOnJobCreation(codedReason);
        }

        [When(@"User pick the Effective Date as (.*) days on Enquiries page")]
        public void WhenUserPickTheEffectiveDateAsDaysOnEnquiriesPage(int days)
        {
            var enquiriesPage = new EnquiresPage();
            enquiriesPage.PickEffectiveDateOnEnquiriesPage(days);
        }



    }
}

