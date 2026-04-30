using POMSeleniumFrameworkPoc1.DTO.UI;
using POMSeleniumFrameworkPoc1.Helpers;
using POMSeleniumFrameworkPoc1.Pages;
using POMSeleniumFrameworkPoc1.Pages.UI.RBSP_BP;
using Reqnroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POMSeleniumFrameworkPoc1.Steps.UI
{
    [Binding]
    public class CompositePropertyChangeSteps
    {
        public DTO.InputOutputData inputoutputdata;
        public ScenarioContext _scenarioContext;
        PADEntryPage padEntryPage = new PADEntryPage();
        public CompositePropertyChangeSteps(ScenarioContext context, DTO.InputOutputData _inputoutputdata)
        {
            _scenarioContext = context;
            this.inputoutputdata = _inputoutputdata;
        }

        [Given(@"User clicks on the '(.*)' in the PVT tab")]
        public void GivenUserClicksOnTheInTheTab(string chevronName)
        {
            padEntryPage.ClickOnChevronOnPVTTab(chevronName);
        }

        [Given(@"User validates and clicks the '(.*)' and '(.*)' on PVT tablist")]
        public void GivenUserValidatesAndClicksTheTabPresentOnAndTablist(string tabName, String popUpName)
        {
            padEntryPage.ValidateAndClickOnTabInPVT(tabName, popUpName);

        }
    }
}
