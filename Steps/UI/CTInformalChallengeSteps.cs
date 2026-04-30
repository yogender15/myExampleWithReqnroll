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
    public class CTInformalChallengeSteps
    {
        public ScenarioContext _scenarioContext;
        public NewJob newJobInformation;
        public CTInformalChallengeSteps(ScenarioContext context, NewJob _newJob)
        {
            this.newJobInformation = _newJob;
            _scenarioContext = context;
        }

        [When(@"I click the Enquiries tab")]
        public void WhenIClickTheEnquiriesTab()
        {
            var jobPage = new Pages.JobPage();
            jobPage.ClickEnquiriesTabOnJobsPage();
        }

        [When(@"I click New Enquiries button")]
        public void WhenIClickNewEnquiriesButton()
        {
            var jobPage = new Pages.JobPage();
            jobPage.ClickNewEnquiriesBtnUnderEnquiriesTab();
        }

        [When(@"I click Informal Challenge tab on Enquiries Page")]
        public void WhenIClickInformalChallengeTabOnEnquiriesPage()
        {
            var informalChallengePage = new InformalChallengePage();
            informalChallengePage.ClickInformalChallengeTabUnderEnquiries();
        }

        [When(@"I search for the property on Informal Challenge page as ""(.*)""")]
        public void WhenISearchForThePropertyOnInformalChallengePageAs(string property)
        {
            var informalChallengePage = new InformalChallengePage();
            informalChallengePage.ClickInformalChallengeTabUnderEnquiries();
        }


    }
}
