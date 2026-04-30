using NUnit.Framework;
using POMSeleniumFrameworkPoc1.APIObjects;
using POMSeleniumFrameworkPoc1.Pages;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Reqnroll ;

namespace POMSeleniumFrameworkPoc1.Steps.API
{
    [Binding]
    public sealed class SearchJobsCallSteps
    {
      

        private readonly ScenarioContext _scenarioContext;
        SearchjobResponseObjects _searchjobResponseObjects;

        public SearchJobsCallSteps(ScenarioContext scenarioContext, SearchjobResponseObjects searchjobResponseObjects)
        {
            _scenarioContext = scenarioContext;
            _searchjobResponseObjects = searchjobResponseObjects;
        }

        [Given(@"I search for (.*) in the job search")]
        public void GivenISearchForInTheJobSearch(string jobSearchTerm)
        {
            var searchJobs = new SearchJobCallModelPage();
            _searchjobResponseObjects = searchJobs.GetSearchJobCallResponse(jobSearchTerm, out IRestResponse response);
            Assert.IsTrue(response.StatusCode.ToString()=="OK",
                $"the response code is NOT as expected, it is {response.StatusCode}");
        }

        [Then(@"the search result should contain (.*)")]
        public void ThenTheSearchResultShouldContain(string resultTerm)
        {
            Console.WriteLine($"the response search result titles");
            _searchjobResponseObjects.Value.ForEach(t => Console.WriteLine(t.Text));
            Assert.IsTrue(_searchjobResponseObjects.Value.All(t => t.Text.ToLower().Contains(resultTerm.ToLower())),
                $"The search result doesn't contain {resultTerm}");
            
        }

    }
}
