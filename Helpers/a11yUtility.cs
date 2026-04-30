using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POMSeleniumFrameworkPoc1.Helpers
{
    class a11yUtility : BasePage
    {
        public void a11yPoc_Tag(String tag)
        {
            DriverHelper.Driver.Navigate().GoToUrl(Config.BaseUrl);
            //        AxeResult axeResult = new AxeBuilder(DriverHelper.Driver)
            //.WithTags(tag)
            //.Analyze();

            //        var violationCount = axeResult.Violations.Count();
            //        foreach (AxeResultItem s in axeResult.Violations)
            //        {
            //            Console.Write("AxeResultItem : " + s.ToString());
            //        }

        }


        public void a11yPoc()
        {
            DriverHelper.Driver.Navigate().GoToUrl(Config.BaseUrl);
            //AxeResult axeResult = new AxeBuilder(DriverHelper.Driver)
            //        .Analyze();
            //String axeReport = (AppDomain.CurrentDomain.BaseDirectory + @"..\..\AxeResult\axe-results.html");

            //var violationCount = axeResult.Violations.Count();
            //foreach (AxeResultItem s in axeResult.Violations)
            //{
            //    Console.Write("AxeResultItem : " + s.ToString());
            //}

            //DriverHelper.Driver.CreateAxeHtmlReport(axeResult, axeReport);

        }

        public void a11yPoc(String reportName)
        {
            DriverHelper.Driver.Navigate().GoToUrl(Config.BaseUrl);
            //AxeResult axeResult = new AxeBuilder(DriverHelper.Driver)
            //        .Analyze();
            //String axeReport = (AppDomain.CurrentDomain.BaseDirectory + @"..\..\AxeResult\" + reportName + ".html");

            //var violationCount = axeResult.Violations.Count();
            //foreach (AxeResultItem s in axeResult.Violations)
            //{
            //    Console.Write("AxeResultItem : " + s.ToString());
            //}

            //DriverHelper.Driver.CreateAxeHtmlReport(axeResult, axeReport);

        }
    }

}
