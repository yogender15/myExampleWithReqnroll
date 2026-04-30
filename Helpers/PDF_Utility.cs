using NUnit.Framework;
using OpenQA.Selenium;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using Reqnroll;
using System;




namespace POMSeleniumFrameworkPoc1.Helpers
{

    class PDF_Utility
    {
        private static PdfDocument document;
        private static PdfPage page;
        private static XGraphics xg = null;
        private static String screenshotName = "";


        XFont font = new XFont("Arial Unicode MS", 20);
        XFont font1 = new XFont("Arial Unicode MS", 10);

        static String currentDate = getCurrentSystemDate("dd-MM-yyyy");
        String nameScreenshotsResultsFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\TestResults/" + currentDate + "/" + TestContext.CurrentContext.Test.Name.Substring(0, 16);


        public void initializeScreenshotsFile(String scenarioName)
        {
            screenshotName = scenarioName;
            try
            {
                String currentTimeStamp = getCurrentSystemDate("dd-MM-yyyy_HH-mm");

                bool exists = System.IO.Directory.Exists(nameScreenshotsResultsFile);

                if (!exists)
                    System.IO.Directory.CreateDirectory(nameScreenshotsResultsFile);
                document = new PdfDocument(scenarioName);
            }
            catch (Exception e)
            {
            }

        }

        public static String getCurrentSystemDate(String dateTimeFormat)
        {
            return DateTime.Now.ToString(dateTimeFormat); ;
        }

        public void takeScreenshot()
        {
            try
            {
                String stepDrescription = ScenarioStepContext.Current.StepInfo.Text;
                Screenshot ss = ((ITakesScreenshot)DriverHelper.Driver).GetScreenshot();
                String currentTimeStamp = getCurrentSystemDate("dd-MM-yyyy_HH-mm-ss-fff");
                String ssfp = AppDomain.CurrentDomain.BaseDirectory + @"..\..\PDFscreens\" + currentTimeStamp + ".png";
                ss.SaveAsFile(ssfp);
                page = document.AddPage();
                XImage image = XImage.FromFile(ssfp);
                xg = XGraphics.FromPdfPage(page);
                xg.DrawImage(image, 50, 50, 500, 300);
                xg.DrawString(stepDrescription, font, XBrushes.Black, new XPoint(20, 30));
            }
            catch (Exception e)
            {
            }
        }

        public void takeScreenshot(String stepDescription)
        {
            try
            {
                Screenshot ss = ((ITakesScreenshot)DriverHelper.Driver).GetScreenshot();
                String currentTimeStamp = getCurrentSystemDate("dd-MM-yyyy_HH-mm");
                String ssfp = AppDomain.CurrentDomain.BaseDirectory + @"..\..\PDFscreens\" + currentTimeStamp + ".png";
                ss.SaveAsFile(ssfp);
                page = document.AddPage();
                XImage image = XImage.FromFile(ssfp);
                xg = XGraphics.FromPdfPage(page);
                xg.DrawImage(image, 50, 50, 500, 300);
                xg.DrawString(stepDescription, font, XBrushes.Black, new XPoint(20, 30));

            }
            catch (Exception e)
            {
            }
        }



        public void logger(String logDetails)
        {
            try
            {
                if (xg != null)
                    xg.Dispose();
                xg = XGraphics.FromPdfPage(page);
                XTextFormatter tf = new XTextFormatter(xg);
                XRect rect = new XRect(new XPoint(20, 370), new XSize());

                xg.DrawString(logDetails, font1, XBrushes.Black, new XPoint(20, 370), XStringFormat.Default);
                //tf.DrawString(logDetails, font1, XBrushes.Red, rect, XStringFormat.Default);

            }
            catch (Exception e)
            {
            }
        }

        public void loggerInNewPage(String logDetails)
        {
            try
            {
                xg.Dispose();
                page = document.AddPage();
                xg = XGraphics.FromPdfPage(page);
                var tf = new XTextFormatter(xg);
                var rect = new XRect(50, 50, 500, 700);

                XPen xpen = new XPen(XColors.Navy, 0.4);

                xg.DrawRectangle(xpen, rect);
                XStringFormat format = new XStringFormat();
                format.LineAlignment = XLineAlignment.Near;
                format.Alignment = XStringAlignment.Near;

                XBrush brush = XBrushes.Red;
                tf.DrawString(logDetails,
                              font1,
                              brush,
                              new XRect(rect.X + 5, rect.Y, rect.Width - 5, rect.Height - 5), format);
                //xg.DrawString(logDetails, font1, XBrushes.Black, new XPoint(20, 370), XStringFormat.Default);
                //tf.DrawString(logDetails, font1, XBrushes.Red, rect, XStringFormat.Default);

            }
            catch (Exception e)
            {
            }
        }


        public void finalizeScreenshotsFile()
        {
            try
            {
                document.Save(nameScreenshotsResultsFile + @"\" + screenshotName + @".pdf");
                document.Close();
            }
            catch (Exception e)
            {
            }
        }

        public void exceptionPdFLogger(Exception e)
        {
            String msg = e.ToString();
            loggerInNewPage(msg);
            finalizeScreenshotsFile();
            failTest(msg);

        }

        public void failTest(String msg)
        {
            Assert.Fail(msg);

        }
    }
}







