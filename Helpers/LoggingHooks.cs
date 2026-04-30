using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reqnroll ;
using Reqnroll .Infrastructure;

namespace POMSeleniumFrameworkPoc1.Helpers
{
    class LoggingHooks
    {
        private readonly IReqnrollOutputHelper _specFlowOutputHelper;

        public LoggingHooks(IReqnrollOutputHelper specFlowOutputHelper) 
        {
            _specFlowOutputHelper = specFlowOutputHelper;
        }

        [AfterStep()]
        public void TakeScreenShotAfterEachStep() 
        {

            if (DriverHelper.Driver is ITakesScreenshot screenshottaker) 
            {
                var fileName = Path.ChangeExtension(Path.GetRandomFileName(), "png");
                screenshottaker.GetScreenshot().SaveAsFile(fileName);
                _specFlowOutputHelper.AddAttachment(fileName);
            }
           
        }

    }
}
