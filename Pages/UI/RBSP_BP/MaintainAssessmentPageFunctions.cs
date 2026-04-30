using NUnit.Framework;
using OpenQA.Selenium;
using POMSeleniumFrameworkPoc1.Helpers;
using Reqnroll;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POMSeleniumFrameworkPoc1.Pages.UI.RBSP_BP
{
    public partial class MaintainAssessmentPage : BasePage
    {

        public void validatedAssessment(FeatureContext fc, Table t)
        {
            waitHelpers wh = new waitHelpers();
            foreach (var row in t.Rows)
            {
                if (row["Effective_From"].Equals("effective_from_date"))
                {
                    String effectiveFromDateStr = (string)fc["effective_from_date"];
                    string[] dateValue = effectiveFromDateStr.Split(' ');
                    DateTime effectiveFromDate = DateTime.Parse(dateValue[0]);
                    fc["effective_from_date"] = effectiveFromDate.ToString("M/d/yyyy");
                }

                if (row["Effective_From"].Equals("New Proposed Effective Date Updated"))
                {
                    String effectiveFromDateStr = (string)fc["New Proposed Effective Date"];
                    //string[] dateValue = effectiveFromDateStr.Split(' ');
                    DateTime effectiveFromDate = DateTime.ParseExact(effectiveFromDateStr, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    fc["New Proposed Effective Date Updated"] = effectiveFromDate.ToString("M/d/yyyy");
                }
                if (row["Effective_From"].Equals("ProposedEffectiveDateofEDC"))
                {
                    String effectiveFromDateStr = (string)fc["ProposedEffectiveDateofEDC"];
                    //string[] dateValue = effectiveFromDateStr.Split(' ');
                    DateTime effectiveFromDate = DateTime.ParseExact(effectiveFromDateStr, "M/d/yyyy", CultureInfo.InvariantCulture);
                    fc["ProposedEffectiveDateofEDC"] = effectiveFromDate.ToString("M/d/yyyy");
                }
                if (row["Effective_From"].Equals("ProposedEffectiveDateofdeletion"))
                {
                    String effectiveFromDateStr = (string)fc["ProposedEffectiveDateofdeletion"];
                    //string[] dateValue = effectiveFromDateStr.Split(' ');
                    DateTime effectiveFromDate = DateTime.ParseExact(effectiveFromDateStr, "M/d/yyyy", CultureInfo.InvariantCulture);
                    fc["ProposedEffectiveDateofdeletion"] = effectiveFromDate.ToString("M/d/yyyy");
                }

                string EffectiveFrom = (string)fc[row["Effective_From"]];
                string EffectiveTo = "";
                if (String.IsNullOrEmpty(row["Effective_To"]))
                {
                    EffectiveTo = "";
                }
                else
                {
                    EffectiveTo = (string)fc[row["Effective_To"]];
                }

                string TaxBand = "";
                if (String.IsNullOrEmpty(row["TaxBand"]))
                {
                    TaxBand = "";
                }
                else
                {
                    TaxBand = (string)fc[row["TaxBand"]];
                }

                string AssessmentAction = "";// row["AssessmentAction"];
                if (String.IsNullOrEmpty(row["AssessmentAction"]))
                {
                    AssessmentAction = "";
                }
                else
                {
                    AssessmentAction = (string)row["AssessmentAction"];
                }

                string CompIndicator = row["CompIndicator"];
                string AssessmentStatus = row["AssessmentStatus"];
                string PublicationStatus = row["PublicationStatus"];
                Console.WriteLine(EffectiveFrom);
                Console.WriteLine(EffectiveTo);
                Console.WriteLine(TaxBand);
                Console.WriteLine(CompIndicator);
                Console.WriteLine(AssessmentStatus);
                Console.WriteLine(PublicationStatus);
                Console.WriteLine(AssessmentAction);
                String assesmentXpath = $"//*[@data-automationid='DetailsRowCell']//*[text()='{EffectiveFrom}'] //ancestor::div[@role='row'] //*[@data-automationid='DetailsRowCell']//*[text()='{EffectiveTo}'] //ancestor::div[@role='row'] //*[@data-automationid='DetailsRowCell']//*[text()='{TaxBand}'] //ancestor::div[@role='row'] //*[@data-automationid='DetailsRowCell']//*[text()='{CompIndicator}'] //ancestor::div[@role='row'] //*[@data-automationid='DetailsRowCell']//*[contains(text(),'{AssessmentStatus}')] //ancestor::div[@role='row'] //*[@data-automationid='DetailsRowCell']//*[text()='{PublicationStatus}'] //ancestor::div[@role='row'] //*[@data-automationid='DetailsRowCell']//*[text()='{AssessmentAction}']";
                if (string.IsNullOrEmpty(TaxBand))
                {
                    assesmentXpath = $"//*[@data-automationid='DetailsRowCell']//*[text()='{EffectiveFrom}'] //ancestor::div[@role='row'] //*[@data-automationid='DetailsRowCell']//*[text()='{EffectiveTo}'] //ancestor::div[@role='row'] //*[@data-automationid='DetailsRowCell']//*[text()='{CompIndicator}'] //ancestor::div[@role='row'] //*[@data-automationid='DetailsRowCell']//*[contains(text(),'{AssessmentStatus}')] //ancestor::div[@role='row'] //*[@data-automationid='DetailsRowCell']//*[text()='{PublicationStatus}'] //ancestor::div[@role='row'] //*[@data-automationid='DetailsRowCell']//*[text()='{AssessmentAction}']";

                }
                if (string.IsNullOrEmpty(EffectiveTo))
                {

                    assesmentXpath = $"//*[@data-automationid='DetailsRowCell']//*[text()='{EffectiveFrom}'] //ancestor::div[@role='row'] //*[@data-automationid='DetailsRowCell']//*[text()='{CompIndicator}'] //ancestor::div[@role='row'] //*[@data-automationid='DetailsRowCell']//*[contains(text(),'{AssessmentStatus}')] //ancestor::div[@role='row'] //*[@data-automationid='DetailsRowCell']//*[text()='{PublicationStatus}'] //ancestor::div[@role='row'] //*[@data-automationid='DetailsRowCell']//*[text()='{AssessmentAction}']";

                }
                if (string.IsNullOrEmpty(AssessmentAction))
                {
                    assesmentXpath = $"//*[@data-automationid='DetailsRowCell']//*[text()='{EffectiveFrom}'] " +
                        $"//ancestor::div[@role='row'] //*[@data-automationid='DetailsRowCell']//*[text()='{EffectiveTo}'] " +
                        $"//ancestor::div[@role='row'] //*[@data-automationid='DetailsRowCell']//*[text()='{TaxBand}']" +
                        $"//ancestor::div[@role='row'] //*[@data-automationid='DetailsRowCell']//*[text()='{CompIndicator}'] " +
                        $"//ancestor::div[@role='row'] //*[@data-automationid='DetailsRowCell']//*[contains(text(),'{AssessmentStatus}')] " +
                        $"//ancestor::div[@role='row'] //*[@data-automationid='DetailsRowCell']//*[text()='{PublicationStatus}'] ";

                }
                wh.isElementDisplayed(By.XPath(assesmentXpath), 60);
                wh.GetWebElementVisble(By.XPath(assesmentXpath));

            }

        }

        public void validatedAssessmentwelsh(FeatureContext fc, Table t)
        {
            waitHelpers wh = new waitHelpers();
            foreach (var row in t.Rows)
            {
                DateTime sixYearsAgo = DateTime.Today.AddYears(-6);
                fc["effective_To_WalesIC_date"] = sixYearsAgo.ToString("M/d/yyyy");

                if (row["Effective_From"].Equals("effective_from_date"))
                {
                    String effectiveFromDateStr = (string)fc["effective_from_date"];
                    string[] dateValue = effectiveFromDateStr.Split(' ');
                    DateTime effectiveFromDate = DateTime.Parse(dateValue[0]);
                    fc["effective_from_date"] = effectiveFromDate.ToString("M/d/yyyy");
                }

                string EffectiveFrom = (string)fc[row["Effective_From"]];
                string EffectiveTo = "";

                if (String.IsNullOrEmpty(row["Effective_To"]))
                {
                    EffectiveTo = "";
                }
                else
                {
                    EffectiveTo = (string)fc[row["Effective_To"]];
                }

                string TaxBand = "";
                if (String.IsNullOrEmpty(row["TaxBand"]))
                {
                    TaxBand = "";
                }
                else
                {
                    TaxBand = (string)fc[row["TaxBand"]];
                }

                string CompIndicator = row["CompIndicator"];
                string AssessmentStatus = row["AssessmentStatus"];
                string PublicationStatus = row["PublicationStatus"];
                string AssessmentAction = row["AssessmentAction"];
                Console.WriteLine(EffectiveFrom);
                Console.WriteLine(EffectiveTo);
                Console.WriteLine(TaxBand);
                Console.WriteLine(CompIndicator);
                Console.WriteLine(AssessmentStatus);
                Console.WriteLine(PublicationStatus);
                Console.WriteLine(AssessmentAction);
                String assesmentXpath = $"//*[@data-automationid='DetailsRowCell']//*[text()='{EffectiveFrom}'] //ancestor::div[@role='row'] //*[@data-automationid='DetailsRowCell']//*[text()='{EffectiveTo}'] //ancestor::div[@role='row'] //*[@data-automationid='DetailsRowCell']//*[text()='{TaxBand}'] //ancestor::div[@role='row'] //*[@data-automationid='DetailsRowCell']//*[text()='{CompIndicator}'] //ancestor::div[@role='row'] //*[@data-automationid='DetailsRowCell']//*[contains(text(),'{AssessmentStatus}')] //ancestor::div[@role='row'] //*[@data-automationid='DetailsRowCell']//*[text()='{PublicationStatus}'] //ancestor::div[@role='row'] //*[@data-automationid='DetailsRowCell']//*[text()='{AssessmentAction}']";
                if (string.IsNullOrEmpty(TaxBand))
                {
                    assesmentXpath = $"//*[@data-automationid='DetailsRowCell']//*[text()='{EffectiveFrom}'] //ancestor::div[@role='row'] //*[@data-automationid='DetailsRowCell']//*[text()='{EffectiveTo}'] //ancestor::div[@role='row'] //*[@data-automationid='DetailsRowCell']//*[text()='{CompIndicator}'] //ancestor::div[@role='row'] //*[@data-automationid='DetailsRowCell']//*[contains(text(),'{AssessmentStatus}')] //ancestor::div[@role='row'] //*[@data-automationid='DetailsRowCell']//*[text()='{PublicationStatus}'] //ancestor::div[@role='row'] //*[@data-automationid='DetailsRowCell']//*[text()='{AssessmentAction}']";

                }
                if (string.IsNullOrEmpty(EffectiveTo))
                {

                    assesmentXpath = $"//*[@data-automationid='DetailsRowCell']//*[text()='{EffectiveFrom}'] //ancestor::div[@role='row'] //*[@data-automationid='DetailsRowCell']//*[text()='{CompIndicator}'] //ancestor::div[@role='row'] //*[@data-automationid='DetailsRowCell']//*[contains(text(),'{AssessmentStatus}')] //ancestor::div[@role='row'] //*[@data-automationid='DetailsRowCell']//*[text()='{PublicationStatus}'] //ancestor::div[@role='row'] //*[@data-automationid='DetailsRowCell']//*[text()='{AssessmentAction}']";

                }
                wh.isElementDisplayed(By.XPath(assesmentXpath), 60);
                wh.GetWebElementVisble(By.XPath(assesmentXpath));

            }


        }
        public void validatedBillingAuthorityLinks(FeatureContext fc, Table t)
        {

            waitHelpers wh = new waitHelpers();
            foreach (var row in t.Rows)
            {
                string effectiveFromDt = null;
                if (row["Effective_From"].Equals("effective_from_date"))
                {
                    String effectiveFromDateStr = (string)fc["effective_from_date"];
                    string[] dateValue = effectiveFromDateStr.Split(' ');
                    DateTime effectiveFromDate = DateTime.Parse(dateValue[0]);
                    effectiveFromDt = effectiveFromDate.ToString("dd/MM/yyyy");
                }
                else
                {
                    String effectiveFromDateStr = (string)fc[row["Effective_From"]];
                    DateTime effectiveFromDate = DateTime.Parse(effectiveFromDateStr);
                    effectiveFromDt = effectiveFromDate.ToString("dd/MM/yyyy");
                }

                string EffectiveFrom = effectiveFromDt;
                string EffectiveTo = "";
                if (String.IsNullOrEmpty(row["Effective_To"]))
                {
                    EffectiveTo = "";
                }
                else
                {
                    EffectiveTo = (string)fc[row["Effective_To"]];
                    DateTime effectiveToDate = DateTime.Parse(EffectiveTo);
                    EffectiveTo = effectiveToDate.ToString("dd/MM/yyyy");
                }

                string Status = row["Status"];
                string Submitted_By = (string)fc[row["Billing_Authority"]];
                String baRef = row["BA_Reference"];
                string BA_Reference = (string)fc[row["BA_Reference"]];
                Console.WriteLine(EffectiveFrom);
                Console.WriteLine(EffectiveTo);
                Console.WriteLine(Submitted_By);
                Console.WriteLine(BA_Reference);
                Console.WriteLine(Status);

                string billingAuthorityLinkXpath = $"//*[@col-id='voa_billingauthorityid']//*[text()='{Submitted_By}']  //ancestor::div[@role='row'] //*[@col-id='voa_billingauthorityreference']//*[text()='{BA_Reference}'] //ancestor::div[@role='row'] //*[@col-id='voa_effectivefrom']//*[text()='{EffectiveFrom}'] //ancestor::div[@role='row'] //*[@col-id='voa_effectiveto']//*[text()='{EffectiveTo}'] //ancestor::div[@role='row'] //*[@col-id='voa_statusid']//*[text()='{Status}']";
                wh.isElementDisplayed(By.XPath(billingAuthorityLinkXpath), 60);
                wh.GetWebElementVisble(By.XPath(billingAuthorityLinkXpath));

            }
            ;

        }

        public void validatedBillingAuthorityLinks_ETD(FeatureContext fc, Table table)
        {
            string firstRowEffectiveFrom = null;  // dd/MM/yyyy
            string secondRowEffectiveTo = null;   // dd/MM/yyyy

            waitHelpers wh = new waitHelpers();
            int rowIndex = 0;

            foreach (var row in table.Rows)
            {
                // --- Parse Effective From ---
                string effectiveFromStr;
                if (row["Effective_From"].Equals("effective_from_date"))
                {
                    string efRaw = (string)fc["effective_from_date"];
                    effectiveFromStr = DateTime.Parse(efRaw.Split(' ')[0]).ToString("dd/MM/yyyy");
                }
                else
                {
                    string efRaw = (string)fc[row["Effective_From"]];
                    effectiveFromStr = DateTime.Parse(efRaw).ToString("dd/MM/yyyy");
                }

                // --- Parse Effective To ---
                string effectiveToStr = "";
                if (!String.IsNullOrWhiteSpace(row["Effective_To"]))
                {
                    string etRaw = (string)fc[row["Effective_To"]];
                    effectiveToStr = DateTime.Parse(etRaw).ToString("dd/MM/yyyy");
                }

                string status = row["Status"];
                string submittedBy = (string)fc[row["Billing_Authority"]];
                string baReference = (string)fc[row["BA_Reference"]];

                // Store first/second row values for final assertions
                if (rowIndex == 0)
                    firstRowEffectiveFrom = effectiveFromStr;

                if (rowIndex == 1)
                    secondRowEffectiveTo = effectiveToStr;

                rowIndex++;

                // Build XPath for row validation
                string xpath = $@"
            //*[@col-id='voa_billingauthorityid']//*[text()='{submittedBy}']
            /ancestor::div[@role='row']
            //*[@col-id='voa_billingauthorityreference']//*[text()='{baReference}']
            /ancestor::div[@role='row']
            //*[@col-id='voa_effectivefrom']//*[text()='{effectiveFromStr}']
            /ancestor::div[@role='row']
            //*[@col-id='voa_effectiveto']//*[text()='{effectiveToStr}']
            /ancestor::div[@role='row']
            //*[@col-id='voa_statusid']//*[text()='{status}']
        ";

                // Validate the row exists
                wh.isElementDisplayed(By.XPath(xpath), 60);
                wh.GetWebElementVisble(By.XPath(xpath));
            }

            // --- Final Assertions ---
            Assert.IsNotNull(firstRowEffectiveFrom, "Could not capture Effective_From for the 1st row.");
            Assert.IsNotNull(secondRowEffectiveTo, "Could not capture Effective_To for the 2nd row.");

            Assert.AreEqual(
                firstRowEffectiveFrom,
                secondRowEffectiveTo,
                $"Expected first row Effective_From ({firstRowEffectiveFrom}) to equal second row Effective_To ({secondRowEffectiveTo})."
            );
        }

        public void validatePVT_Addresses(string SubtabName, Table t)
        {
            waitHelpers wh = new waitHelpers();
            SeleniumExtensions.scrollToBtnBasedOnLabelAndClick(SubtabName);
            var individualJobPage = new IndividualJobPage();
            By expander = By.CssSelector($"[class*='cellIsGroupExpander']");
            wh.isElementDisplayed(expander, 120);
            wh.elementClickableAndDisplayed(expander);
            wh.clickOnWebElement(expander);
            foreach (var row in t.Rows)
            {
                string Status = row["Status"];
                string AddressSource = row["AddressSource"];
                individualJobPage.ValidateAddresses(Status, AddressSource);

            }
        }
        public void validatepropertylinktable(FeatureContext fc, Table t)
        {
            waitHelpers wh = new waitHelpers();
            foreach (var row in t.Rows)
            {

                string MainParty = "";
                if (String.IsNullOrEmpty(row["Main_Party"]))
                {
                    MainParty = "";
                }
                else
                {
                    MainParty = (string)fc[row["Main_Party"]];
                }

                string EffectiveTo = "";
                if (String.IsNullOrEmpty(row["Effective_To"]))
                {
                    EffectiveTo = "";
                }
                else
                {
                    EffectiveTo = (string)fc[row["Effective_To"]];
                }
                string propertylinkxpath = $"//*[text()='{MainParty}']//ancestor::div[@role='row']";
                wh.isElementDisplayed(By.XPath(propertylinkxpath), 60);
                wh.GetWebElementVisble(By.XPath(propertylinkxpath));
            }
        }

        public void validatedPVT_Assessment(string SubtabName, FeatureContext fc, Table t)
        {
            waitHelpers wh = new waitHelpers();
            SeleniumExtensions.scrollToBtnBasedOnLabelAndClick(SubtabName);
            By expander = By.CssSelector($"[class*='cellIsGroupExpander']");
            wh.isElementDisplayed(expander, 120);
            wh.elementClickableAndDisplayed(expander);
            wh.clickOnWebElement(expander);
            foreach (var row in t.Rows)
            {
                string effectiveFromDt = null;
                if (row["Effective_From"].Equals("effective_from_date"))
                {
                    String effectiveFromDateStr = (string)fc["effective_from_date"];
                    string[] dateValue = effectiveFromDateStr.Split(' ');
                    DateTime effectiveFromDate = DateTime.Parse(dateValue[0]);
                    effectiveFromDt = effectiveFromDate.ToString("dd/MM/yyyy");
                }
                else
                {
                    String effectiveFromDateStr = (string)fc[row["Effective_From"]];
                    DateTime effectiveFromDate = DateTime.Parse(effectiveFromDateStr);
                    effectiveFromDt = effectiveFromDate.ToString("dd/MM/yyyy");
                }

                string EffectiveFrom = effectiveFromDt;
                string EffectiveTo = "";
                if (String.IsNullOrEmpty(row["Effective_To"]))
                {
                    EffectiveTo = "";
                }
                else
                {
                    EffectiveTo = (string)fc[row["Effective_To"]];
                    DateTime effectiveToDate = DateTime.Parse(EffectiveTo);
                    EffectiveTo = effectiveToDate.ToString("dd/MM/yyyy");
                }

                string TaxBand = "";
                if (String.IsNullOrEmpty(row["TaxBand"]))
                {
                    TaxBand = "";
                }
                else
                {
                    TaxBand = (string)fc[row["TaxBand"]];
                }

                string AssessmentStatus = row["AssessmentStatus"];
                string DOLU_Identifier = row["DOLU_Identifier"];
                String ListUpdateAction = row["ListUpdateAction"];
                string CompIndicator = row["CompIndicator"];
                string jobName = "";
                if (String.IsNullOrEmpty(row["Job"]))
                {
                    jobName = "";
                }
                else
                {
                    jobName = (string)fc[row["Job"]];
                }


                Console.WriteLine(EffectiveFrom);
                Console.WriteLine(EffectiveTo);
                Console.WriteLine(DOLU_Identifier);
                Console.WriteLine(ListUpdateAction);
                Console.WriteLine(AssessmentStatus);
                Console.WriteLine(CompIndicator);
                Console.WriteLine(jobName);
                Console.WriteLine(TaxBand);

                string PVT_AssesssmentXpath = null;
                if (string.IsNullOrEmpty(jobName) && !string.IsNullOrEmpty(EffectiveTo))
                {
                    PVT_AssesssmentXpath = $"//*[@data-automation-key='statusColumn']//*[contains(text(),'{AssessmentStatus}')]//ancestor::div[@role='row']//*[@data-automation-key='bandColumn']//*[contains(text(),'{TaxBand}')]//ancestor::div[@role='row'] //*[@data-automation-key='effectiveFromColumn']//*[contains(text(),'{EffectiveFrom}')]//ancestor::div[@role='row']//*[@data-automation-key='effectiveToColumn']//*[contains(text(),'{EffectiveTo}')]//ancestor::div[@role='row']//*[@data-automation-key='useListUpdateIndicatorColumn']//*[contains(text(),'{DOLU_Identifier}')]//ancestor::div[@role='row']//*[@data-automation-key='listUpdateActionColumn']//*[contains(text(),'{ListUpdateAction}')]//ancestor::div[@role='row'] //*[@data-automation-key='isCompositeColumn']//*[contains(text(),'{CompIndicator}')]//ancestor::div[@role='row']//*[@data-automation-key='jobColumn']"; ;

                }

                if (!string.IsNullOrEmpty(jobName) && string.IsNullOrEmpty(EffectiveTo))
                {
                    PVT_AssesssmentXpath = $"//*[@data-automation-key='statusColumn']//*[contains(text(),'{AssessmentStatus}')]//ancestor::div[@role='row']//*[@data-automation-key='bandColumn']//*[contains(text(),'{TaxBand}')]//ancestor::div[@role='row'] //*[@data-automation-key='effectiveFromColumn']//*[contains(text(),'{EffectiveFrom}')]//ancestor::div[@role='row']//*[@data-automation-key='useListUpdateIndicatorColumn']//*[contains(text(),'{DOLU_Identifier}')]//ancestor::div[@role='row']//*[@data-automation-key='listUpdateActionColumn']//*[contains(text(),'{ListUpdateAction}')]//ancestor::div[@role='row'] //*[@data-automation-key='isCompositeColumn']//*[contains(text(),'{CompIndicator}')]//ancestor::div[@role='row']//*[@data-automation-key='jobColumn']//button[text()='{jobName}']";
                }

                wh.isElementDisplayed(By.XPath(PVT_AssesssmentXpath), 60);
                wh.GetWebElementVisble(By.XPath(PVT_AssesssmentXpath));
            }

        }

    }
}
