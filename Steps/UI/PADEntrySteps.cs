using POMSeleniumFrameworkPoc1.DTO;
using POMSeleniumFrameworkPoc1.DTO.UI;
using POMSeleniumFrameworkPoc1.Helpers;
using POMSeleniumFrameworkPoc1.Pages;
using POMSeleniumFrameworkPoc1.Pages.UI.RBSP_BP;
using Reqnroll;
using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POMSeleniumFrameworkPoc1.Steps.UI
{
    public class PADEntrySteps
    {
        public ScenarioContext _scenarioContext;
        public NewJob newJobInformation;
        public PADEntrySteps(ScenarioContext context, NewJob _newJob, NewJob _newRequestId)
        {
            this.newJobInformation = _newJob;
            this.newJobInformation = _newRequestId;
            _scenarioContext = context;
        }





    }
}
