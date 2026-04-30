using NUnit.Framework;
using POMSeleniumFrameworkPoc1.DTO;
using POMSeleniumFrameworkPoc1.Pages.UI.LAPortal;
using Reqnroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POMSeleniumFrameworkPoc1.Steps.UI
{
    [Binding]
    public class PartDemolitionSteps
    {
        public ScenarioContext _scenarioContext;
        public NewJob newJobInformation;
        public PartDemolitionSteps(ScenarioContext context, NewJob _newJob)
        {
            this.newJobInformation = _newJob;
            _scenarioContext = context;
        }

        [When(@"I select an option ""(.*)"" in the Why does the property not have a planning portal reference page")]
        public void WhenISelectAnOptionInTheWhyDoesThePropertyNotHaveAPlanningPortalReferencePage(string option)
        {
            var lAPortalDashboardPage = new LAPortalDashboardPage();
            lAPortalDashboardPage.SelectWasBuiltWithoutPlanningPermissionRadioBtn();
        }

        [Then(@"I should see error message as ""(.*)"" on property not have a planning portal reference page")]
        public void ThenIShouldSeeErrorMessageAsOnPropertyNotHaveAPlanningPortalReferencePage(string errorMessage)
        {
            var lAPortalDashboardPage = new LAPortalDashboardPage();
            Assert.IsTrue(lAPortalDashboardPage.IsSingleReportConfirmationMessageIsDisplayed(errorMessage), "Single Report Confirmation Message Is not Displayed");
        }
    }
}
