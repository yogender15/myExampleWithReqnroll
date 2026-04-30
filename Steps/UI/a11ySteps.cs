using POMSeleniumFrameworkPoc1.Helpers;
using System;
using System.Globalization;
using System.IO;
using Reqnroll ;


namespace POMSeleniumFrameworkPoc1.Steps.UI
{
    [Binding]
    class a11ySteps
    {
        public ScenarioContext _scenarioContext;
        public FeatureContext _featureContext;
        public DTO.InputOutputData inputoutputdata;
        private readonly ExcelTestDataUtility exceltestdatautility;



        public a11ySteps(ScenarioContext scenarioContext, FeatureContext featureContext, DTO.InputOutputData _inputoutputdata)

        {
            string TestDataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + Config.TestDataExcelFilePath);
            this.inputoutputdata = _inputoutputdata;
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
            this.exceltestdatautility = new ExcelTestDataUtility(TestDataFilePath);
        }


        [Given(@"User analyse accessibility with '(.*)' tag")]
        public void GivenUserAnalyseAccessibilityWithTag(string tag)
        {
            a11yUtility a11yUtil = new a11yUtility();
            a11yUtil.a11yPoc(tag);
        }

        [Given(@"User analyse accessibility of this page")]
        public void GivenUserAnalyseAccessibilityOfThisPage()
        {
            a11yUtility a11yUtil = new a11yUtility();
            a11yUtil.a11yPoc();
            string output1 = DateHelpers.getFutureDate(10, "dd/MM/yyyy");
            Console.WriteLine(output1);
            string output2 = DateHelpers.ToDateTime(_featureContext["effective_from_date"].ToString(), "dd/MM/yyyy");
            Console.WriteLine(output2);

            string input = "3/29/2022 6:32:05 PM";
            string output = DateTime.Parse(input).ToString("dd/MM/yyyy");
            Console.WriteLine(output);

        }

        [Given(@"User analyse accessibility of this page and generate ""(.*)"" report")]
        public void GivenUserAnalyseAccessibilityOfThisPageAndGenerateReport(string reportName)
        {
            a11yUtility a11yUtil = new a11yUtility();
            a11yUtil.a11yPoc(reportName);
        }




    }
}
