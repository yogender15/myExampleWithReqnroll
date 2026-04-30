using Microsoft.CodeAnalysis.Options;
using POMSeleniumFrameworkPoc1.DTO.UI;
using POMSeleniumFrameworkPoc1.Helpers;
using POMSeleniumFrameworkPoc1.Pages;
using POMSeleniumFrameworkPoc1.Pages.UI.RBSP_BP;
using Reqnroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace POMSeleniumFrameworkPoc1.Steps.UI
{
    [Binding]
    public sealed class IndividualSteps
    {
        public DTO.InputOutputData inputoutputdata;
        public ScenarioContext _scenarioContext;
        public FeatureContext _featureContext;

        public IndividualSteps(ScenarioContext scenarioContext, FeatureContext featureContext, DTO.InputOutputData _inputoutputdata)
        {
            this.inputoutputdata = _inputoutputdata;
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
        }

        [Given(@"User validates the questionnaire in the '(.*)' BPF pop-up for '(.*)'")]
        public void GivenUserValidatesTheQuestiionaireInTheBPFPop_Up(string bpfName, string bpName)
        {
            var testData = inputoutputdata.testData;
            var individualJobPage = new IndividualJobPage();
            individualJobPage.ValidateQuestionnaireInResearchingTab(bpfName, bpName, testData);
        }

        [Given(@"User 
the View Selector to '(.*)'")]
        public void GivenUserFiltersTheJobDisplayTo(string filterValue)
        {
            var commonPage = new CommonPage();
            commonPage.FilterViewSelectorWithValue(filterValue);
        }

        [Given(@"User click on '(.*)' under '(.*)' dropdown")]
        public void GivenUserClickOnUnderDropdown(string dropdownvalue, string dropdownvalue_sub)
        {
            var commonPage = new CommonPage();
            commonPage.Filterestateteam(dropdownvalue, dropdownvalue_sub);
        }

        [Given(@"User filters the '(.*)' column with the '(.*)' created")]
        public void GivenUserFiltersTheColumnWithTheCreated(string columnName, string filterValue)
        {
            var testData = inputoutputdata.testData;
            var commonPage = new CommonPage();
            if (filterValue == "Job")
            {
                //  commonPage.FilterColumnWithValue(columnName, "JOB-117848-D0N0Y");

                //   Console.WriteLine(_featureContext["Job_ID"]);
                commonPage.FilterColumnWithValue(columnName, (string)_featureContext["Job ID"]);
            }
            else if (filterValue == "SubjectText")
            {
                commonPage.FilterColumnWithValue(columnName, "Automation testing");
            }
            else if (filterValue == "Estate File")
            {
                commonPage.FilterColumnWithValue(columnName, "Bristol - 23/09/2024 14:02");
                //   commonPage.FilterColumnWithValue(columnName, (string)_featureContext["EstateFileName"]);
            }
        }

        [Given(@"User filters the '(.*)' job created from the '(.*)' column")]
        public void GivenUserFiltersTheJobCreatedFromTheColumn(string businessProcess, string columnName)
        {
            var testData = inputoutputdata.testData;
            var commonPage = new CommonPage();
            // commonPage.FilterColumnWithValue(columnName, "JOB-06077-D2V4V");
            commonPage.FilterColumnWithValue(columnName, (string)_featureContext[businessProcess]);
            // "JOB-125994-Q8G0W"
        }

        [Given(@"User filters the '(.*)' job created from the '(.*)' column for Recon")]
        public void GivenUserFiltersTheJobCreatedFromTheColumn_new(string JobID, string columnName)
        {
            var commonPage = new CommonPage();
            var testdata = inputoutputdata.testData;
            string Job = testdata[$"{JobID}"];
            commonPage.FilterColumnWithValue(columnName, Job);
        }

        [Given(@"User selects the '(.*)' job created from the '(.*)' column")]
        public void GivenUserSelectsTheJobCreatedFromTheColumn(string businessProcess, string columnName)
        {
            var commonPage = new CommonPage();
            //commonPage.SelectFilteredRowValue(columnName, "JOB-06077-D2V4V");
            commonPage.SelectFilteredRowValue(columnName, (string)_featureContext[businessProcess]);

        }

        [Given(@"User selects the '(.*)' job created from the '(.*)' column for Recon")]
        public void GivenUserSelectsTheJobCreatedFromTheColumn_new(string JobID, string columnName)
        {
            var commonPage = new CommonPage();
            var testdata = inputoutputdata.testData;
            string Job = testdata[$"{JobID}"];
            commonPage.SelectFilteredRowValue(columnName, Job);

        }

        [Given(@"User validates the new Job is created which is not equal to old Job ID for '(.*)'")]
        public void WhenUserValidatesTheNewJobIsCreatedWhichIsNotEqualToOldJobID(string jobType)
        {
            var individualJobPage = new IndividualJobPage();
            individualJobPage.IsJobCreatedRequestPageIsDisplayed((string)_featureContext["Job ID"]);
        }


        [Given(@"User selects the '(.*)' from the '(.*)' column")]
        public void GivenUserSelectsTheFromTheColumn(string filterValue, string columnName)
        {
            var testData = inputoutputdata.testData;
            var commonPage = new CommonPage();
            if (filterValue == "Job")
            {
                commonPage.SelectFilteredRowValue(columnName, (string)_featureContext["Del_JobID"]);
            }
            else if (filterValue == "SubjectText")
            {
                commonPage.SelectFilteredRowValue(columnName, testData["AFHSubject"]);
            }
            else if (filterValue == "Estate File")
            {
                commonPage.SelectFilteredRowValue(columnName, "Bristol - 23/09/2024 14:02");
                //   commonPage.SelectFilteredRowValue(columnName, (string)_featureContext["EstateFileName"]);
            }
        }

        [Given(@"User validates the '(.*)' column value as '(.*)'")]
        public void GivenUserValidatesTheColumnValueAs(string columnName, string columnValue)
        {
            var individualJobPage = new IndividualJobPage();
            individualJobPage.ValidateStatusCodeValue(columnName, columnValue);
        }
        [Given(@"User validates the Activity Status column value as '(.*)' for latest '(.*)'")]
        public void GivenUserValidatesTheActivityStatusColumnValueAsForLatest(string columnValue, string columnName)
        {
            var individualJobPage = new IndividualJobPage();
            individualJobPage.ValidateStatusCodeValue(columnValue, columnName);
        }


        [Given(@"User fills in the mandatory details in the Inspection section")]
        public void GivenUserFillsInTheMandatoryDetailsInTheInspectionSection()
        {
            var testData = inputoutputdata.testData;
            var individualJobPage = new IndividualJobPage();
            individualJobPage.EnterDetailsInInspectionSection(testData["InspectionToggle"], testData["ReasonForInspection"], testData["ReasonForInspectionComments"], testData["InspectionAllocation"]);

        }


        [Given(@"User validates the default values for below fields under '(.*)' section")]
        public void GivenUserValidatesTheDefaultValuesForBelowFieldsUnderSection(string sectionName, Table table)
        {
            foreach (var row in table.Rows)
            {
                string fieldName = row["fieldName"];
                string expdefaultfieldValue = row["fieldValue"];
                var individualJobPage = new IndividualJobPage();
                individualJobPage.ValidateDefaultFieldValues(sectionName, fieldName, expdefaultfieldValue);
            }
        }

        [Given(@"User validates the Inspection Job created and clicks on it")]
        public void GivenUserValidatesTheInspectionJobCreated()
        {
            var individualJobPage = new IndividualJobPage();
            individualJobPage.ValidateInspectionJob("JOB-116084-G2Y7B");
        }

        [Given(@"The inspector makes changes to the below default values under '(.*)'")]
        public void GivenTheInspectorMakesChangesToTheBelowDefaultValuesUnder(string sectionName, Table table)
        {
            foreach (var row in table.Rows)
            {
                string fieldName = row["fieldName"];
                string amendValues = row["fieldValue"];
                var individualJobPage = new IndividualJobPage();
                individualJobPage.AmendCTInspectionFieldValues(sectionName, fieldName, amendValues);
            }
        }

        [Given(@"User selects '(.*)' as the Inspection Type and Save & Mark as Complete")]
        public void GivenUserSelectsAsTheInspectionType(string inspectionType)
        {
            var individualJobPage = new IndividualJobPage();
            individualJobPage.SelectInspectionTypeAndSave(inspectionType);
        }

        [Given(@"User enters details on AskForHelp dialog and Mark as Complete")]
        public void GivenUserEntersDetailsOnAskForHelpDialogAndMarkAsComplete()
        {
            var testData = inputoutputdata.testData;
            var individualJobPage = new IndividualJobPage();
            individualJobPage.EnterDetailsInAFHDialog(testData["AFHSubject"], testData["AFHDescription"]);
        }

        [Given(@"User validates the fields in Property Attributes section under PAD Entry tab of Researching stage")]
        public void GivenUserValidatesTheFieldsInPropertyAttributesSectionUnderPADEntryTabOfResearchingStage()
        {
            var testData = inputoutputdata.testData;
            var individualJobPage = new IndividualJobPage();
            individualJobPage.ValidateFieldsPropertyAttributes(testData["HeatingValue"], testData["ConservatoryType"], testData["ConservatoryArea"], testData["HouseTypeGroup"], testData["HouseType"], testData);
        }

        [Given(@"User fills the mandatory fields in Property Attributes section under PAD Entry tab of Researching stage")]
        public void GivenUserFillsTheMandatoryFieldsInPropertyAttributesSectionUnderPADEntryTabOfResearchingStage()
        {
            var testData = inputoutputdata.testData;
            var individualJobPage = new IndividualJobPage();
            individualJobPage.EnterDataInPropertyAttributesSection(testData["AgeCode"], testData["Area"], testData["Rooms"], testData["Bedrooms"], testData["Baths"], testData["Floor"], testData["Parking"], testData["PlotSize"], testData);
        }

        [Given(@"User fills the mandatory fields in Property Attributes section under PAD Entry tab of Researching stage for '(.*)' process")]
        public void GivenUserFillsTheMandatoryFieldsInPropertyAttributesSectionUnderPADEntryTabOfResearching(string BpfVal)
        {
            var testData = inputoutputdata.testData;
            var individualJobPage = new IndividualJobPage();
            individualJobPage.EnterDataInPropertyAttributesSection(BpfVal, testData["Group"], testData["Type"], testData["AgeCode"], testData["Parking"], testData["ConservatoryType"], testData["HeatingValue"], testData["ConservatoryArea"], testData["Floor"], testData["Level"], testData["Rooms"], testData["Bedrooms"], testData["Baths"], testData["Area"]);
        }


        [Given(@"User clicks on the New Value Significant Code Button in General section under PAD Entry tab of Researching stage")]
        public void GivenUserClicksOnTheNewValueSignificantCodeButtonInGeneralSectionUnderPADEntryTabOfResearchingStage()
        {
            var individualJobPage = new IndividualJobPage();
            individualJobPage.ClickOnNewVSCLinkButtonOnPADEntryTab();
        }

        [Given(@"User enters the mandatory fields in the General tab of New Domestic Property VSC Page")]
        public void GivenUserEntersTheMandatoryFieldsInTheGeneralTabOfNewDomesticPropertyVSCPage()
        {
            var testData = inputoutputdata.testData;
            var individualJobPage = new IndividualJobPage();
            individualJobPage.EnterVSCDataInDomecticPropertyVSCPage(testData["VSC"], testData["VSCDescription"]);
        }

        [Given(@"User clicks on the New Source Code Button in Source Codes section under PAD Entry tab of Researching stage")]
        public void GivenUserClicksOnTheNewSourceCodeButtonInSourceCodesSectionUnderPADEntryTabOfResearchingStage()
        {
            var individualJobPage = new IndividualJobPage();
            individualJobPage.ClickOnNewSourceCodeOnPADEntryTab();
        }

        [Given(@"User enters the mandatory fields in the General tab of New Attribute Set Source Code Page")]
        public void GivenUserEntersTheMandatoryFieldsInTheGeneralTabOfNewAttributeSetSourceCodePage()
        {
            var testData = inputoutputdata.testData;
            var individualJobPage = new IndividualJobPage();
            individualJobPage.EnterSourceCodeInSetSourceCodePage(testData["SourceCode"]);

        }

        [Given(@"User click on '(.*)' button for NDR Assessment")]
        public void GivenUserClickOnButtonForNDRAssessment(string value)
        {
            var PADEntryPage = new PADEntryPage();
            PADEntryPage.SelectNDRAssesmentValue(value);
        }

        [Given(@"User validates the '(.*)' as '(.*)'")]
        public void GivenUserValidatesTheAs(string fieldName, string expOutCome)
        {
            var individualJobPage = new IndividualJobPage();
            individualJobPage.ValidatePADOutcome(expOutCome);
            individualJobPage.UpdateDesktopResearch();
        }

        [Given(@"User validates the presence of below fields in '(.*)' section of the '(.*)' Tab at '(.*)' BPF")]
        public void GivenUserValidatesThePresenceOfBelowFieldsInSectionOfTheTabAtBPF(string sectionName, string tabName, string bpfStage, Table table)
        {
            foreach (var row in table.Rows)
            {
                string fieldName = row["fieldName"];
                var commonPage = new CommonPage();
                commonPage.ValidateFieldsInSection(sectionName, fieldName);
            }
        }

        [Given(@"User validates the below tabs present in the '(.*)' section of the '(.*)' Tab at '(.*)' BPF")]
        public void GivenUserValidatesTheBelowTabsPresentInTheSectionOfTheTabAtBPF(string sectionName, string tabName, string bpfName, Table table)
        {
            foreach (var row in table.Rows)
            {
                string exptabName = row["tabName"];
                var commonPage = new CommonPage();
                commonPage.ValidatetabsInSection(sectionName, exptabName);
            }
        }


        [Given(@"User validates the below links present in the '(.*)' section of the '(.*)' Tab at '(.*)' BPF")]
        public void GivenUserValidatesTheBelowLinksPresentInTheSectionOfTheTabAtBPF(string sectionName, string tabName, string bpfName, Table table)
        {
            foreach (var row in table.Rows)
            {
                string expLinkName = row["linkName"];
                var commonPage = new CommonPage();
                commonPage.ValidateLinksInSection(sectionName, expLinkName);
            }
        }

        [Given(@"User fills the mandaory fields in the Job Resolution Pop-up")]
        public void GivenUserFillsTheMandaoryFieldsInTheJobResolutionPop_Up()
        {
            var testData = inputoutputdata.testData;
            var individualJobPage = new IndividualJobPage();
            individualJobPage.EnterDataInJobResolutionPopUp(testData["JobResolution"], testData["BARemarks"], testData["JR_Remarks"]);
        }

        [Then(@"User should validate the VMS title as '(.*)' after VMS is loaded")]
        public void ThenUserShouldValidateTheVMSTitleAsAfterVMSIsLoaded(string expVMSTilte)
        {
            var individualJobPage = new IndividualJobPage();
            individualJobPage.ValidateVMSLoadedCorrectly(expVMSTilte);

        }

        [Given(@"User clicks on '(.*)' button and selects '(.*)'")]
        public void GivenUserClicksOnButtonAndSelects(string buttonName, string option)
        {
            var individualJobPage = new IndividualJobPage();
            individualJobPage.ClickOnUploadThisJob(buttonName, option);
        }


        [Given("User click on {string} from the options")]
        public void GivenUserClickOnFromTheOptions(string fieldname)
        {
            try
            {
                var individualJobPage = new IndividualJobPage();
                individualJobPage.Clickeditdetailsoption(fieldname);
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
            }
            catch (Exception ex)
            {
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
                pdf_Util.exceptionPdFLogger(ex);
            }

        }




        [Given(@"User selects '(.*)' from '(.*)' dropdown on the Document Dialog")]
        public void GivenUserSelectsFromDropdownOnTheDocumentDialog(string dropdownOption, string dropdownName)
        {
            try
            {
                var individualJobPage = new IndividualJobPage();
                individualJobPage.SelectValueFromDocumentDropdownOnDIalog(dropdownOption, dropdownName);
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
            }
            catch (Exception e)
            {
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
                pdf_Util.exceptionPdFLogger(e);
            }

        }

        [Given(@"User clicks on '(.*)' on the Document Dialog")]
        public void GivenUserClicksOnOnTheDocumentDialog(string buttonName)
        {
            try
            {
                var individualJobPage = new IndividualJobPage();
                individualJobPage.ClickonButtonOnDocumentDialog(buttonName);
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
            }
            catch (Exception e)
            {
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
                pdf_Util.exceptionPdFLogger(e);
            }

        }

        [Given(@"User clicks on '(.*)' and uploads the document")]
        public void GivenUserClicksOnAndUploadsTheDocument(string buttonName)
        {
            try
            {
                var individualJobPage = new IndividualJobPage();
                individualJobPage.UploadDocumentOnDialog(buttonName);
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
            }
            catch (Exception e)
            {
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
                pdf_Util.exceptionPdFLogger(e);
            }


        }

        [Given("User clicks on {string} and uploads the {string} document")]
        public void GivenUserClicksOnAndUploadsTheDocument(string buttonName, string fileNmae)
        {
            try
            {
                var individualJobPage = new IndividualJobPage();
                individualJobPage.UploadDocumentOnDialog(buttonName, fileNmae);
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
            }
            catch (Exception e)
            {
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
                pdf_Util.exceptionPdFLogger(e);
            }
        }


        [Given(@"User validates the upload status message")]
        public void GivenUserValidatesTheUploadStatusMessage()
        {
            try
            {
                var individualJobPage = new IndividualJobPage();
                individualJobPage.ValidateDocUploadStatus();
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
            }
            catch (Exception e)
            {
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
                pdf_Util.exceptionPdFLogger(e);
            }

        }
        [Given("User validates the status message after file update")]
        public void GivenUserValidatesTheStatusMessageAfterFileUpdate()
        {
            try
            {
                var individualJobPage = new IndividualJobPage();
                individualJobPage.ValidateDocUpdatestatus();
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
            }
            catch (Exception e)
            {
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
                pdf_Util.exceptionPdFLogger(e);
            }
        }

        
        [Given(@"User validates the '(.*)' message and click on Create Button in '(.*)' tab")]
        public void GivenUserValidatesTheMessageAndClickOnCreateButton(string messageText, string tabName)
        {
            var individualJobPage = new IndividualJobPage();
            individualJobPage.ValidateWelcomeMessageAndClickCreateOnPVT(tabName, messageText);
        }

        [Given(@"User selects the PAD and clicks on '(.*)'")]
        public void GivenUserSelectsThePADAndClicksOn(string commandOption)
        {
            var individualJobPage = new IndividualJobPage();
            individualJobPage.EditThePADCreated(commandOption);
        }

        [Given(@"User validate the Band entered and Publication status as '(.*)'")]
        public void GivenUserValidateTheBandEnteredAndPublicationStatusAs(string releaseStatus)
        {
            var testData = inputoutputdata.testData;
            var individualJobPage = new IndividualJobPage();
            //    individualJobPage.ValidatePublicationStatus(releaseStatus);
            individualJobPage.ValidateAssesment(testData["ValidateAssessmentTax"], releaseStatus);
        }

        [Given(@"User navigates to '(.*)' tab to validate Assessment Status as '(.*)' and Publication Status as '(.*)'")]
        public void GivenUserNavigatesToTabToValidateAssessmentStatusAsAndPublicationStatusAs(string tabName, string assessmentStatus, string publicationStatus)
        {
            var individualJobPage = new IndividualJobPage();
            individualJobPage.ValidateAssesmentAfterPublish(tabName, assessmentStatus, publicationStatus);
        }

        [Given(@"User navigates to '(.*)' tab to validate the tax Band and Assessment Status as '(.*)' for '(.*)'")]
        public void GivenUserNavigatesToTabToValidateTheTaxBandAndAssessmentStatusAsFor(string tabName, string assessmentStatus, string bpName)
        {
            var testData = inputoutputdata.testData;
            var individualJobPage = new IndividualJobPage();
            individualJobPage.ValidateAssesmentAfterRelease(tabName, assessmentStatus, testData["ValidateAssessmentTax"]);

        }


        [Given(@"User navigates to '(.*)' tab to validate the below components of Assessments")]
        public void GivenUserNavigatesToTabToValidateTheBelowComponentsOfAssessments(string tabName, Table table)
        {

            var testData = inputoutputdata.testData;
            var individualJobPage = new IndividualJobPage();
            foreach (var row in table.Rows)
            {
                string EffectiveFromDays = row["EffectiveFrom"];
                string EffectiveToDays = row["EffectiveTo"];
                string CompIndicator = row["CompIndicator"];
                string TaxBand = row["TaxBand"];
                string AssessmentStatus = row["AssessmentStatus"];
                string PublicationStatus = row["PublicationStatus"];
                //_featureContext["effective_from_date"] = "6/25/2018";
                // _featureContext["expEffectiveChangedDate"] = "9/30/2024";
                if (EffectiveFromDays == "")
                {
                    _featureContext[EffectiveFromDays] = "";
                }
                if (EffectiveToDays == "")
                {
                    _featureContext[EffectiveToDays] = "";
                }
                individualJobPage.ValidateAssessmentForBP(tabName, (string)_featureContext[EffectiveFromDays], (string)_featureContext[EffectiveToDays], TaxBand, CompIndicator, AssessmentStatus, PublicationStatus);
                //   individualJobPage.ValidateAssessmentForBP(tabName, "03/07/2018 12:00", "03/07/2018 12:00", TaxBand, CompIndicator, AssessmentStatus, PublicationStatus);

            }
        }


        [Given(@"User enters the fields on '(.*)' BPF Journey pop-up")]
        public void GivenUserEntersTheFieldsOnBPFJourneyPop_Up(string bpfName)
        {
            var individualJobPage = new IndividualJobPage();
            individualJobPage.EnterFieldsOnMaintainAssessment(bpfName);
        }

        [Given(@"User enters the fields on '(.*)' BPF Journey without AllJobs pop-up")]
        public void GivenUserEntersTheFieldsOnBPFJourneywithoutAllJobs(string bpfName)
        {
            var individualJobPage = new IndividualJobPage();
            individualJobPage.EnterFieldsOnMaintainAssessmentwithoutJobSubmission(bpfName);
        }

        [Given(@"User selects '(.*)' for QA/QC on '(.*)' BPF Journey pop-up")]
        public void GivenUserSelectsForQAQCOnBPFJourneyPop_Up(string qualityReviewCheck, string bpfName)
        {
            var individualJobPage = new IndividualJobPage();
            individualJobPage.QAQCOnMaintainAssessment(qualityReviewCheck, bpfName);

        }

        [Given(@"User Approves the Job")]
        public void GivenUserApprovesTheJob()
        {
            var individualJobPage = new IndividualJobPage();
            individualJobPage.ApproveQAQC();
        }



        [Given(@"User validates '(.*)' for Is Banding Complete on '(.*)' BPF journey")]
        public void GivenUserValidatesForIsBandingCompleteOnBPFJourney(string expValue, string bpfName)
        {
            var individualJobPage = new IndividualJobPage();
            individualJobPage.ValidateBandingCompleteOnBPF(bpfName, expValue);
        }

        [Given(@"User captures the below Hereditament Details")]
        public void GivenUserCapturesTheBelowHereditamentDetails(Table table)
        {
            var testData = inputoutputdata.testData;
            foreach (var row in table.Rows)
            {
                string fieldName = row["fieldName"];
                var individualJobPage = new IndividualJobPage();
                _featureContext[fieldName] = individualJobPage.CaptureHereditamentDetails(fieldName);

            }

        }

        [Given(@"User captures the '(.*)','(.*)' data in the '(.*)' output sheet")]
        public void GivenUserCapturesTheDataCapturedInTheOutputSheet(string jobID, string addressString, string sheetName)
        {
            SeleniumExtensions.captureTestDataToExcel((string)_scenarioContext[jobID], (string)_scenarioContext[addressString], sheetName);
        }


        [Given(@"User appends the '(.*)' data captured in the output sheet")]
        public void GivenUserAppendsTheDataCapturedInTheOutputSheet(string jobType)
        {
            SeleniumExtensions.AppendToExcel((string)_featureContext["Town"], (string)_featureContext["PostCode"], (string)_featureContext["UPRN"], (string)_featureContext[jobType]);

        }



        [Given(@"User captures the '(.*)' from the Address Details screen")]
        public void GivenUserCapturesTheFromTheAddressDetailsScreen(string fieldName)
        {
            var testData = inputoutputdata.testData;
            var individualJobPage = new IndividualJobPage();
            _featureContext[fieldName] = individualJobPage.CaptureUPRN();
            Console.WriteLine(_featureContext[fieldName]);
            testData[fieldName] = (string)_featureContext[fieldName];

        }


        [Given(@"User validates that the '(.*)' tab loads totally")]
        public void GivenUserValidatesThatTheTabLoadsTotally(string tabName)
        {
            var individualJobPage = new IndividualJobPage();
            individualJobPage.ValidateBandingTabLoaded();

        }

        [Given(@"User enter details and validates linked Assessment in the Check Facts tab at the Researching stage for '(.*)'")]
        public void GivenUserEnterDetailsAndValidatesLinkedAssessmentInTheCheckFactsTabAtTheResearchingStage(string bpName)
        {
            var individualJobPage = new IndividualJobPage();
            individualJobPage.LinkedAssessmentValidationOnCheckFacts(bpName);
        }

        [Given(@"User Validates the PADs on the PVT Tab and navigates to Banding Stage")]
        public void GivenUserValidatesThePADsOnThePVTTabAndNavigatesToBandingStage()
        {
            var testData = inputoutputdata.testData;
            var individualJobPage = new IndividualJobPage();
            individualJobPage.UserValidatesthePADSection(testData["ValidatePADCode"]);
            individualJobPage.ValidatePADOutcome("PAD Validation Outcome");
            Thread.Sleep(60000);
            var PADEntryPage = new PADEntryPage();
            PADEntryPage.SelectNDRAssesmentValue("No");

        }

        [Given(@"User navigates to '(.*)' tab to validate the components of Assessments")]
        public void GivenUserNavigatesToTabToValidateTheComponentsOfAssessments(string tabName, Table table)
        {

            var testData = inputoutputdata.testData;
            var individualJobPage = new IndividualJobPage();
            foreach (var row in table.Rows)
            {
                string EffectiveFromDays = row["EffectiveFrom"];
                string EffectiveToDays = row["EffectiveTo"];
                string TaxBand = row["TaxBand"];
                string CompIndicator = row["CompIndicator"];
                string AssessmentStatus = row["AssessmentStatus"];
                string PublicationStatus = row["PublicationStatus"];
                string AssessmentAction = row["AssessmentAction"];
                //_featureContext["effective_from_date"] = "6/25/2018";
                //_featureContext["
                //"] = "8/25/2024";
                if (EffectiveFromDays == "")
                {
                    _featureContext[EffectiveFromDays] = "";
                }
                if (EffectiveToDays == "")
                {
                    _featureContext[EffectiveToDays] = "";
                }
                individualJobPage.ValidateAssessmentOnMaintainAssessment(tabName, (string)_featureContext[EffectiveFromDays], (string)_featureContext[EffectiveToDays], (string)_featureContext[TaxBand], CompIndicator, AssessmentStatus, PublicationStatus, AssessmentAction);
                //   individualJobPage.ValidateAssessmentForBP(tabName, "03/07/2018 12:00", "03/07/2018 12:00", TaxBand, CompIndicator, AssessmentStatus, PublicationStatus);

            }
        }

        [Given(@"User navigates to Addresses tab to validate the Addresses data")]
        public void GivenUserNavigatesToAddressestoValidateAddress(Table table)
        {

            var testData = inputoutputdata.testData;
            var individualJobPage = new IndividualJobPage();
            foreach (var row in table.Rows)
            {
                string Status = row["Status"];
                string AddressSource = row["AddressSource"];
                individualJobPage.ValidateAddresses(Status, AddressSource);

            }
        }


        [Given(@"User validates the Routing Region Name for Billing Authority selected")]
        public void GivenUserValidatesTheRoutingRegionNameForBillingAuthoritySelected()
        {
            var testData = inputoutputdata.testData;
            var individualJobPage = new IndividualJobPage();
            individualJobPage.ValidateRoutingRegionName(testData["SubmittedBy"]);

        }


        [Given(@"User sets the revised effective date")]
        public void GivenUserSetsTheRevisedEffectiveDate()
        {
            var individualJobPage = new IndividualJobPage();
            individualJobPage.SetNewProposedEffectiveDate();
        }


        [Given(@"User enters data for '(.*)' field value on dialog")]
        public void GivenUserEntersDataForFieldValueOnDialog(string fieldName)
        {
            try
            {
                var testData = inputoutputdata.testData;
                if (fieldName.Contains("Date"))
                {
                    string date = DateTime.Now.AddDays(Convert.ToDouble(testData[fieldName])).ToString("dd/MM/yyyy");
                    _scenarioContext[fieldName] = date;
                    _featureContext["expNewProposedEffectiveChangeDate"] = SeleniumExtensions.SelectDateFromDatePicker(fieldName, testData);

                }
                else
                {
                    SeleniumExtensions.setFieldValue(fieldName, testData);
                }
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
            }
            catch (Exception e)
            {
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
                pdf_Util.exceptionPdFLogger(e);
            }

        }

        [Given(@"User Validate the new Proposed Effective Date Change on Researching Screen")]
        public void GivenUserValidateTheNewProposedEffectiveDateChangeOnResearchingScreenOnTabFromTablist()
        {
            var individualJobPage = new IndividualJobPage();
            //individualJobPage.ValidateNewProposedEffectiveDate((string)_featureContext["expNewEffectiveChangeDate"]);
            individualJobPage.ValidateNewProposedEffectiveDate((string)_featureContext["expNewProposedEffectiveChangeDate"]);

        }

        [Given(@"User validate the Corerspondence Assessment filled on the '(.*)' BPF Flow")]
        public void GivenUserValidateTheCorerspondenceAssessmentFilledOnTheBPFFlow(string bpfName)
        {
            var individualJobPage = new IndividualJobPage();
            individualJobPage.ValidateCorrespondenceAssessment(bpfName);
        }

        [Given(@"User validates the Pop-ups for '(.*)' on the Dekstop Reaserch stage")]
        public void GivenUserValidatesThePop_UpsForOnTheDekstopReaserchStage(string bpName)
        {
            var individualJobPage = new IndividualJobPage();
            individualJobPage.ValidateCTToNDRPopUps(bpName);
        }

        [Given(@"User enters the fields on '(.*)' BPF Journey pop-up and clicks on '(.*)' for All Jobs Created")]
        public void GivenUserEntersTheFieldsOnBPFJourneyPop_UpAndClicksOnForAllJobsCreated(string bpfName, string btnName)
        {
            var individualJobPage = new IndividualJobPage();
            individualJobPage.EnterFieldsOnMaintainAssessmentAndValidateJob(bpfName, btnName);
        }

        [Given(@"User opens the '(.*)' job from '(.*)' column")]
        public void GivenUserOpensTheJobFromColumn(string jobIDName, string columnName)
        {
            var testData = inputoutputdata.testData;
            var commonPage = new CommonPage();
            //   commonPage.FilterColumnWithValue(columnName, testData[jobIDName]);
            commonPage.FilterColumnWithValue(columnName, "JOB-04002-L1H7B");

        }

        [Given(@"User selects the '(.*)' job from '(.*)' column")]
        public void GivenUserSelectsTheJobFromColumn(string jobIDName, string columnName)
        {
            var testData = inputoutputdata.testData;
            var commonPage = new CommonPage();
            //  commonPage.SelectFilteredRowValue(columnName, testData[jobIDName]);
            commonPage.SelectFilteredRowValue(columnName, "JOB-04002-L1H7B");

        }

        [Given(@"User validates the Failure in the '(.*)' as '(.*)'")]
        public void GivenUserValidatesThePADFailure(string fieldName, string expOutCome)
        {
            var individualJobPage = new IndividualJobPage();
            individualJobPage.ValidatePADFailureOutcome(expOutCome);
            individualJobPage.UpdateDesktopResearch();
        }


    }
}
