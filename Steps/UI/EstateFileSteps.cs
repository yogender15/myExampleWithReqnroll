using NUnit.Framework;
using POMSeleniumFrameworkPoc1.DTO;
using POMSeleniumFrameworkPoc1.DTO.UI;
using POMSeleniumFrameworkPoc1.Helpers;
using POMSeleniumFrameworkPoc1.Pages;
using POMSeleniumFrameworkPoc1.Pages.UI;
using Reqnroll;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace POMSeleniumFrameworkPoc1.Steps.UI
{
    [Binding]
    public sealed class EstateFileSteps
    {
        public DTO.InputOutputData inputoutputdata;
        public ScenarioContext _scenarioContext;
        public FeatureContext _featureContext;

        public EstateFileSteps(ScenarioContext scenarioContext, FeatureContext featureContext, DTO.InputOutputData _inputoutputdata)
        {
            this.inputoutputdata = _inputoutputdata;
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
        }

        [Given(@"user clicks Estate Files menu")]
        public void GivenUserClicksEstateFilesMenu()
        {
            var estateFilePage = new EstateFilePage();
            estateFilePage.ClickEstateFileMenu();
        }

        [Given(@"user clicks New command on Estate form")]
        public void GivenUserClicksNewCommandOnEstateForm()
        {
            var estateFilePage = new EstateFilePage();
            estateFilePage.ClickNewCommandBtn();
        }


        [When(@"user enters Billing Authority")]
        public void WhenUserEntersBillingAuthority()
        {
            var testData = inputoutputdata.testData;
            var estateFilePage = new EstateFilePage();
            estateFilePage.FillBillingAuthority(testData["BillingAuthority"]);
        }

        [When(@"user enters Planning Reference Number")]
        public void WhenUserEntersPlanningReferenceNumber()
        {
            var testData = inputoutputdata.testData;
            var estateFilePage = new EstateFilePage();
            estateFilePage.EnterPlanningReferenceNumber(testData["PlanningRefNumber"]);
        }

        [When(@"user enters Developer Name")]
        public void WhenUserEntersDeveloperName()
        {
            var testData = inputoutputdata.testData;
            var estateFilePage = new EstateFilePage();
            estateFilePage.EnterDeveloperName(testData["DevelopmentName"]);
        }

        [When(@"user Developer details")]
        public void WhenUserDeveloperDetails()
        {
            var testData = inputoutputdata.testData;
            var estateFilePage = new EstateFilePage();
            estateFilePage.FillDeveloper(testData["Developer"]);
        }

        [When(@"user clicks Save on Estate form")]
        public void WhenUserClicksSaveOnEstateForm()
        {
            var estateFilePage = new EstateFilePage();
            estateFilePage.ClickSaveBtnOnMainNav();
        }

        [When(@"user clicks Estate Actions tab")]
        public void WhenUserClicksEstateActionsTab()
        {
            var estateFilePage = new EstateFilePage();
            estateFilePage.ClickEstateActionsTab();
        }

        [When(@"user clicks New Estate Action plus button")]
        public void WhenUserClicksNewEstateActionPlusButton()
        {
            var estateFilePage = new EstateFilePage();
            estateFilePage.ClickNewEstateActionplusBtn();
        }

        [When(@"user enters Estate Action Type")]
        public void WhenUserEntersEstateActionType()
        {
            var testData = inputoutputdata.testData;
            var estateFilePage = new EstateFilePage();
            estateFilePage.FillEstateActionType(testData["EstateActionType"]);
        }

        [When(@"user enters Number of Plots")]
        public void WhenUserEenntersNumberOfPlots()
        {
            var testData = inputoutputdata.testData;
            var estateFilePage = new EstateFilePage();
            estateFilePage.EnterNumberOfPlots(testData["NumOfPlots"].ToString());
        }

        [When(@"user enters Plot Starting Number")]
        public void WhenUserEntersPlotStartingNumber()
        {
            var testData = inputoutputdata.testData;
            var estateFilePage = new EstateFilePage();
            estateFilePage.EnterPlotStartingNumber(testData[".PlotStartingNum"].ToString());
        }

        [When(@"user toggles sumbit to yes")]
        public void WhenUserTogglesSumbitToYes()
        {
            var estateFilePage = new EstateFilePage();
            estateFilePage.ClickSubmitToggleBtn();
        }

        [When(@"user clicks Save and Close button")]
        public void WhenUserClicksSaveAndCloseButton()
        {
            var estateFilePage = new EstateFilePage();
            estateFilePage.ClickSaveandCloseBtn();
        }

        [When(@"user clicks House Types tab")]
        public void WhenUserClicksHouseTypesTab()
        {
            var estateFilePage = new EstateFilePage();
            estateFilePage.ClickHouseTypesTab();
        }

        [When(@"user clicks New House Type Plus button")]
        public void WhenUserClicksNewHouseTypePlusButton()
        {
            var estateFilePage = new EstateFilePage();
            estateFilePage.ClickNewHouseTypePlusBtn();
        }

        [When(@"user enters House type Name")]
        public void WhenUserEntersHouseTypeName()
        {
            var testData = inputoutputdata.testData;
            var estateFilePage = new EstateFilePage();
            estateFilePage.EnterNameInputField(testData["HouseTypeName"]);
        }

        [When(@"user enters Group")]
        public void WhenUserEntersGroup()
        {
            var testData = inputoutputdata.testData;
            var estateFilePage = new EstateFilePage();
            estateFilePage.FillGroupLookUp(testData["HouseTypeGroup"]);
        }

        [When(@"user enters Type")]
        public void WhenUserEntersType()
        {
            var testData = inputoutputdata.testData;
            var estateFilePage = new EstateFilePage();
            estateFilePage.FillTypeLookUp(testData["HouseType"]);
        }

        [When(@"user enters List")]
        public void WhenUserEntersList()
        {
            var testData = inputoutputdata.testData;
            var estateFilePage = new EstateFilePage();
            estateFilePage.FillListLookUp(testData["HouseTypeList"]);
        }

        //**************************Ashish's Code**************************************
        [Given(@"User fills the mandatory fields in the General tab of the Estate File form")]
        public void GivenUserFillsTheMandatoryFieldsInTheGeneralTabOfTheEstateFileForm()
        {
            var testData = inputoutputdata.testData;
            var estateFilePage = new EstateFilePage();
            estateFilePage.EnterDataInGeneralTab(testData["BillingAuthority"], testData["PlanningRefNumber"], testData["DevelopmentName"], testData["Developer"]);
        }

        [Given(@"User captures the CreatedOn field")]
        public void GivenUserCapturesTheCreatedOnField()
        {
            var testData = inputoutputdata.testData;
            var estateFilePage = new EstateFilePage();
            testData["CreatedOnDateTime"] = estateFilePage.GetCreatedOnValue();
            testData["EstateFileName"] = testData["BillingAuthority"] + " - " + testData["CreatedOnDateTime"];
            Console.WriteLine(testData["EstateFileName"]);
            _featureContext["EstateFileName"] = testData["EstateFileName"];
        }

        [Given(@"User clicks on the New Estate Action Button in the Estate Actions Tab")]
        public void GivenUserClicksOnTheNewEstateActionButtonInTheEstateActionTab()
        {
            var estateFilePage = new EstateFilePage();
            estateFilePage.ClickNewEstateActionButton();
        }

        [Given(@"User enters the mandatory fields on the Estate Action Pop-up")]
        public void GivenUserEntersTheMandatoryFieldsOnTheEstateActionPop_Up()
        {
            var testData = inputoutputdata.testData;
            var estateFilePage = new EstateFilePage();
            estateFilePage.EnterDataInEAPopUp(testData["EstateActionType"], testData["NumOfPlots"], testData["PlotStartingNum"], testData["EAPopUpSubmitToggle"]);

        }

        [Given(@"User selects '(.*)' as the Estate Action Type")]
        public void GivenUserSelectsAsTheEstateActionType(string estateActionType)
        {
            var testData = inputoutputdata.testData;
            var estateFilePage = new EstateFilePage();
            estateFilePage.BlockEstateFile(estateActionType, testData["EAPopUpSubmitToggle"]);

        }

        [Given(@"User validate the Estate Action Type Created")]
        public void GivenUserValidateTheEstateActionTypeCreated()
        {
            var testData = inputoutputdata.testData;
            var estateFilePage = new EstateFilePage();
            Assert.AreEqual(testData["EstateActionType"], estateFilePage.ValidateEstateActionType());

        }

        [Given(@"User clicks on the New House Type Button in the House Type Tab")]
        public void GivenUserClicksOnTheNewHouseTypeButtonInTheHouseTypeTab()
        {
            var estateFilePage = new EstateFilePage();
            estateFilePage.ClickNewHouseTypeButton();
        }

        [Given(@"User enters the mandatory fields on the House Type Pop-up")]
        public void GivenUserEntersTheMandatoryFieldsOnTheHouseTypePop_Up()
        {
            var testData = inputoutputdata.testData;
            var estateFilePage = new EstateFilePage();
            estateFilePage.EnterDataInHTPopUp(testData["HouseTypeName"], testData["HouseTypeGroup"], testData["HouseType"], testData["HouseTypeList"]);

        }

        [Given(@"User validate the House Type Created and click on it")]
        public void GivenUserValidateTheHouseTypeCreated()
        {
            var testData = inputoutputdata.testData;
            var estateFilePage = new EstateFilePage();
            estateFilePage.ValidateHouseTypeNameAndClick(testData["HouseTypeName"]);

        }

        [Given(@"User click on newly created house type")]
        public void GivenUserClickOnNewlyCratedHouseType()
        {
            waitHelpers wh = new waitHelpers();
            var estateFilePage = new EstateFilePage();
            wh.clickOnElement(estateFilePage.houseNameBy);
        }

        [Given(@"User fills the mandatory fields in PAD Code Details section under General tab of Validate PAD stage")]
        public void GivenUserFillsTheMandatoryFieldsInPADCodeDetailsSectionUnderGeneralTabOfValidatePADStage()
        {
            var testData = inputoutputdata.testData;
            var estateFilePage = new EstateFilePage();
            estateFilePage.EnterDataInPADCodeDetailsSection(testData["Area"], testData["Rooms"], testData["Bedrooms"], testData["Baths"], testData["Floor"], testData["Parking"]);

        }

        [Given(@"User fills the mandatory fields in PAD Validation section under General tab of Validate PAD stage for '(.*)'")]
        public void GivenUserFillsTheMandatoryFieldsInPADValidationSectionUnderGeneralTabOfValidatePADStage(string bpName)
        {
            var testData = inputoutputdata.testData;
            var estateFilePage = new EstateFilePage();
            estateFilePage.EnterDataInPADValidationSection(bpName, testData["ValidatePADCode"]);

        }

        [Given(@"User validates the text present as '(.*)' for '(.*)'")]
        public void GivenUserValidatesTheTextPresentAsFor(string expText, string expheaderSection)
        {
            var estateFilePage = new EstateFilePage();
            estateFilePage.ValidatePADOutcome(expText, expheaderSection);
        }

        [Given(@"User fills the mandatory fields in Banding Decision section under Banding tab of Undertake Banding stage for '(.*)'")]
        public void GivenUserFillsTheMandatoryFieldsInBandingDecisionSectionUnderBandingTabOfUndertakeBandingStage(string bpName)
        {
            var testData = inputoutputdata.testData;
            var estateFilePage = new EstateFilePage();
            var individualJobPage = new IndividualJobPage();
            if (bpName == "Estate")
            {
                estateFilePage.EnterDataInBandingDecisionSection(testData["ProposedTaxBand"], testData["AssessmentAction"], testData["IsBandingComplete"]);
            }
            else if (bpName == "Individual")
            {
                individualJobPage.EnterDataInBandingTab(testData["ProposedTaxBand"], testData["AssessmentAction"], testData["IsBandingComplete"]);
            }
            else if (bpName == "CompositePropertyChange" || bpName == "InformalChallenge")
            {

                _featureContext["Band"] = individualJobPage.EnterDataInBandingTabforCompositePropertyChange(testData["ProposedTaxBand"], testData["AssessmentAction"], testData["IsBandingComplete"], testData["BandDirection"]);

            }
        }

        [Given(@"User scroll to '(.*)' section")]
        public void GivenUserScrollToSection(string Headingname)
        {
            try
            {
                var estateFilePage = new EstateFilePage();
                SeleniumExtensions.ScrollToheading(Headingname);
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

        [Given(@"User validates the Approval history notification")]
        public void GivenUserValidatesTheApprovalHistoryNotification()
        {
            //var testData1 = inputoutputdata.testData;
            //var testData = inputoutputdata.testData;
            var nameValue = _featureContext["Name"].ToString();
            var estateFilePage = new EstateFilePage();
            var approvalCount = estateFilePage.ValidateApprovalHistoryText(nameValue);
            _featureContext["AprrovalHistoryNotificationCount"] = approvalCount;
        }


        [Given(@"User validates All House Type Rejected Approvals notification")]
        public void GivenUserValidatesAllHouseTypeRejectedApprovalsNotification()
        {
            //var testData1 = inputoutputdata.testData;
            //
            //testData["rejectedHistoryNotificationCount"] = estateFilePage.ValidaterejectedlHistoryText(testData["Name"]);
            var nameValue = _featureContext["Name"].ToString();
            var estateFilePage = new EstateFilePage();
            var approvalCount = estateFilePage.ValidaterejectedlHistoryText(nameValue);
            _featureContext["AprrovalHistoryNotificationCount"] = approvalCount;
            //string expectedvalue = (String)_featureContext["Name"];
            //Assert.AreEqual(actualvalue, expectedvalue);
        }




        [Given(@"User fills the mandatory fields in Approval section under Approvals tab of Approve House Type stage")]
        public void GivenUserFillsTheMandatoryFieldsInApprovalSectionUnderApprovalsTabOfApproveHouseTypeStage()
        {
            var testData = inputoutputdata.testData;
            var estateFilePage = new EstateFilePage();
            estateFilePage.EnterDataInApprovalSection(testData["HouseTypeApproved"], testData["OutcomeReason"]);

        }

        [Given(@"User validates the header title as the '(.*)'")]
        public void GivenUserValidatesTheHeaderTitleAsThe(string headerTitle)
        {
            var estateFilePage = new EstateFilePage();
            headerTitle = (string)_featureContext["EstateFileName"];
            estateFilePage.ValidateHeaderTitle(headerTitle);
        }

        [Given(@"User enters the details in '(.*)'")]
        public void GivenUserEntersTheDetailsIn(string tabName)
        {
            var testData = inputoutputdata.testData;
            var estateFilePage = new EstateFilePage();
            //  estateFilePage.EnterPlotSizeForEachPlot(testData["PlotSize"], testData["HouseTypeName"], testData["AddressCardText"]);
            estateFilePage.EnterPlotSizeForEachPlot(testData["PlotSize"], testData["HouseTypeName"], testData["AddressCardText"]);
        }

        [Given(@"User validates the Plot status as '(.*)' after Plot has been Auto Processed")]
        public void GivenUserValidatesThePlotStatusAsAfterPlotHasBeenAutoProcessed(string plotStatus)
        {
            var estateFilePage = new EstateFilePage();
            estateFilePage.ValidateStatusForAutoProcessedPlots(plotStatus);

        }


        [Given(@"User validate the House Type and Address Label after allocation")]
        public void GivenUserValidateTheHouseTypeAndAddressLabelAfterAllocation()
        {
            var testData = inputoutputdata.testData;
            var estateFilePage = new EstateFilePage();
            estateFilePage.ValidateHouseTypeAddressLabelForPLot(testData["HouseTypeName"], testData["AddressCardText"]);

        }

        //Estate File Job Type steps
        [Given(@"User clicks on New Estate File option under Hereditament Details section of Details Tab")]
        public void GivenUserClicksOnNewEstateFileOptionUnderHereditamentDetailsSectionOfDetailsTab()
        {
            var estateFilePage = new EstateFilePage();
            estateFilePage.SelectNewEsateFileOptionFromEstateFileLookUp();
        }

        [Given(@"User fills the mandatory fields in the quick create Estate File pop up")]
        public void GivenUserFillsTheMandatoryFieldsInTheQuickCreateEstateFilePopUp()
        {
            var testData = inputoutputdata.testData;
            var estateFilePage = new EstateFilePage();
            estateFilePage.EnterDetailsInEstateFilePopUp(testData["BillingAuthority"], testData["PlanningRefNumber"], testData["DevelopmentName"], testData["Developer"]);
        }

        [Given(@"User enters the Closure Date under '(.*)' journey on the headerbar")]
        public void GivenUserEntersTheClosureDateUnderJourneyOnTheHeaderbar(string journeyName)
        {
            var estateFilePage = new EstateFilePage();
            estateFilePage.EnterClosureDateInCloseJourney(journeyName);

        }

        [Given(@"User selects the Estate file created under Hereditament Details section of Details Tab")]
        public void GivenUserSelectsTheEstateFileCreatedUnderHereditamentDetailsSectionOfDetailsTab()
        {
            var testData = inputoutputdata.testData;
            var estateFilePage = new EstateFilePage();
            estateFilePage.SelectEstateFileInHereditamentDetails(testData["EstateFileName"]);
            //  estateFilePage.SelectEstateFileInHereditamentDetails("Adur Council - 13/03/2024 12:20");
        }

        [Given(@"User validates the Estate File Name after selecting the Estate File for update")]
        public void GivenUserValidatesTheEstateFileNameAfterSelectingTheEstateFileForUpdate()
        {
            var testData = inputoutputdata.testData;
            var estateFilePage = new EstateFilePage();
            estateFilePage.ValidateEstateFileName(testData["EstateFileName"]);
        }



        [Given(@"User validates the fields in PAD Code Details section under General tab of Validate PAD stage")]
        public void GivenUserValidatesTheFieldsInPADCodeDetailsSectionUnderGeneralTabOfValidatePADStage()
        {
            var testData = inputoutputdata.testData;
            var estateFilePage = new EstateFilePage();
            estateFilePage.ValidateFieldsPADCodeDetails(testData["HeatingValue"], testData["ConservatoryType"], testData["ConservatoryArea"]);
        }

        [Given(@"User clicks on the New Value Significant Code Link Button in VSC section under General tab of Validate PAD stage")]
        public void GivenUserClicksOnTheNewValueSignificantCodeLinkButtonInVSCSectionUnderGeneralTabOfValidatePADStage()
        {
            var estateFilePage = new EstateFilePage();
            estateFilePage.ClickOnNewVSCLinkButton();
        }

        [Given(@"User enters the mandatory fields on the VSC link Pop-up")]
        public void GivenUserEntersTheMandatoryFieldsOnTheVSCLinkPop_Up()
        {
            var testData = inputoutputdata.testData;
            var estateFilePage = new EstateFilePage();
            estateFilePage.EnterDataInVSCLinkPopUp(testData["VSC"], testData["VSCDescription"]);
        }

        [Given(@"User validates the field value under '(.*)' BPF on the headerbar")]
        public void GivenUserValidatesTheFieldValueUnderBPFOnTheHeaderbar(string BPFName)
        {
            var estateFilePage = new EstateFilePage();
            estateFilePage.ValidateFieldsOnBandingBPF(BPFName);
        }

        [Given(@"User clicks on New Comparable Property Sets Button to validate CTBT Integration")]
        public void GivenUserClicksOnNewComparablePropertySetsButtonToValidateCTBTIntegration()
        {
            var estateFilePage = new EstateFilePage();
            estateFilePage.ClickOnNewComparablePropertyButton();
        }

        [Given(@"User creats Estate Extent")]
        public void GivenUserCreatsEstateExtent()
        {
            try
            {
                var estateFilePage = new EstateFilePage();
                estateFilePage.createEstateExtent();
            }
            catch (Exception e)
            {
                var pdf_Util = new PDF_Utility();
                pdf_Util.takeScreenshot();
                pdf_Util.exceptionPdFLogger(e);
            }
        }

        [Given(@"User should validate the VMS title as '(.*)' and create estate extent for estate file")]
        public void GivenUserShouldValidateTheVMSTitleAsAndCreateEstateExtentForEstateFile(string expTitle)
        {
            try
            {
                var estateFilePage = new EstateFilePage();
                estateFilePage.ValidateCTBTIntegrationForEstateFileExtent(expTitle);

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



        [Given(@"User should validate the VMS title as '(.*)' after CTBT is loaded")]
        public void ThenUserShouldValidateTheVMSTitleAsAfterCTBTIsLoaded(string expTitle)
        {
            try
            {
                var estateFilePage = new EstateFilePage();
                estateFilePage.ValidateCTBTIntegrationForEstateFile(expTitle);
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



        [Given(@"User validates the fields present in Subject Attributes section under Banding tab of Banding BPF")]
        public void GivenUserValidatesTheFieldsPresentInSubjectAttributesSectionUnderBandingTabOfBandingBPF()
        {
            var estateFilePage = new EstateFilePage();
            estateFilePage.ValidateFieldsInSubjectAttributesSection();
        }

        [Given(@"User validates the Approval history notification for '(.*)' HouseType")]
        public void GivenUserValidatesTheApprovalHistoryNotificationForHouseType(string approvalAction)
        {
            //var testData = inputoutputdata.testData;
            //var estateFilePage = new EstateFilePage();
            //estateFilePage.ValidateLatestApprovalHistory(approvalAction, testData["Name"]);
            var nameValue = _featureContext["Name"].ToString();
            var estateFilePage = new EstateFilePage();
            estateFilePage.ValidateLatestApprovalHistory(approvalAction, nameValue);
        }

        [Given(@"User enters the mandatory fields on the Estate Action Pop-up to raise an Inspection")]
        public void GivenUserEntersTheMandatoryFieldsOnTheEstateActionPop_UpToRaiseAnInspection()
        {
            var testData = inputoutputdata.testData;
            var estateFilePage = new EstateFilePage();
            estateFilePage.RaiseInspectionInEAPopUp(testData["EstateActionType"], testData["ReasonForInspection"], testData["ReasonForInspectionComments"], testData["InspectionAllocation"], testData["EAPopUpSubmitToggle"]);

        }
        [Given(@"User validate and click the Inspection Created")]
        public void GivenUserValidateAndClickTheInspectionCreated()
        {
            var testData = inputoutputdata.testData;
            var estateFilePage = new EstateFilePage();
            Assert.AreEqual(testData["ReasonForInspection"], estateFilePage.ValidateAndClickInspectionCreated());

        }

        [Given(@"User validate the Inpections data in the General Section of General Tab")]
        public void GivenUserValidateTheInpectionsDataInTheGeneralSectionOfGeneralTab()
        {
            var testData = inputoutputdata.testData;
            var estateFilePage = new EstateFilePage();
            estateFilePage.ValidateInspectionDataInGeneralSection(testData["ReasonForInspection"], testData["ReasonForInspectionComments"]);
        }

        [Given(@"User validate duplicate plot error message in the dialog and click ok")]
        public void GivenUserValidateDuplicatePlotErrorMessageInTheDialogAndClickOk()
        {
            var estateFilePage = new EstateFilePage();
            estateFilePage.ValidateDuplicatePlotErrorMsgAndClickOK();

        }

        [Given(@"User displays all the columns for the plots available")]
        public void GivenUserDisplaysAllTheColumnsForThePlotsAvailable()
        {
            var estateFilePage = new EstateFilePage();
            estateFilePage.DisplayAllColumnsForPlots();
        }

        [Given(@"User validates Plot Billing Authority and enter Floor level for Plots")]
        public void GivenUserValidatesPlotBillingAuthorityAndEnterFloorLevelForPlots()
        {
            var testData = inputoutputdata.testData;
            var estateFilePage = new EstateFilePage();
            estateFilePage.ValidatePlotBillingAuthority(testData["Floor"], testData["BillingAuthority"]);
        }

        [Given(@"User Creates a job id")]
        public void GivenUserCreatesAJobId()
        {
            _scenarioContext.Add("Ind_JobID", "Hi");


        }

        [Given(@"User enters plot size (.*)")]
        public void GivenUserEntersPlotSize(string plotSize)
        {
            var estateFilePage = new EstateFilePage();
            estateFilePage.enterPlotSize(plotSize);
        }

        [Given(@"User select Address Labels for (.*),(.*)")]
        public void GivenUserSelectAddressLabelsFor(String position, string postCode)
        {
            var estateFilePage = new EstateFilePage();
            estateFilePage.searchAddressAndSelectAddr(position, postCode);
        }

        [Given("User selects Address Labels for {string} without house type")]
        public void GivenUserSelectsAddressLabelsForWithoutHouseType(string postCode)
        {
            var estateFilePage = new EstateFilePage();
            var testData = inputoutputdata.testData;
            estateFilePage.searchAddressAndSelectAddrForEstateFileWithOutHT(postCode, testData);
        }


        [Given(@"User selects Address Labels for '(.*)'")]
        public void GivenUserSelectsAddressLabelsFor(string postCode)
        {
            var estateFilePage = new EstateFilePage();
            var testData = inputoutputdata.testData;
            estateFilePage.searchAddressAndSelectAddrForEstateFile(postCode, testData);
        }

        [Given(@"user enters data in ""(.*)"" and for corresponce address")]
        public void GivenUserEntersDataInAndForCorresponceAddress(string postCode)
        {
            var estateFilePage = new EstateFilePage();
            var testData = inputoutputdata.testData;
            estateFilePage.GetUniqueAddressForProperty_Estate(postCode, testData);
        }



        [Given(@"User select Address Labels with iteration for (.*),(.*)")]
        public void GivenUserSelectAddressLabelsWithIterationFor(string position, string postCode)
        {
            var estateFilePage = new EstateFilePage();
            estateFilePage.searchAddressAndSelectAddr1(position, postCode);
        }


        [Given(@"User select Address Labels from (.*),(.*)")]
        public void GivenUserSelectAddressLabelsFrom(String positionStr, string postCode)
        {
            var estateFilePage = new EstateFilePage();
            var testData = inputoutputdata.testData;
            string value = testData["Number of Plots"];
            estateFilePage.searchAddressAndSelectAddrWithCount(value, positionStr, postCode);
        }


        [Given(@"User select Address Labels form with iterations (.*),(.*)")]
        public void GivenUserSelectAddressLabelsForWithIteration(String position, string postCode)
        {
            var estateFilePage = new EstateFilePage();
            estateFilePage.searchAddressAndSelectAddr(position, postCode);
        }


        [Given(@"user select house type")]
        public void GivenUserSelectHouseType()
        {
            var estateFilePage = new EstateFilePage();
            estateFilePage.selectHouseType();
        }

        [Given(@"User enters plot size (.*) ,select Address Labels for (.*) and select house type then click on '(.*)'")]
        public void GivenUserEntersPlotSizeSelectAddressLabelsForAndSelectHouseTypeThenClickOn(string plotSize, string postCode, string btnName)
        {
            var estateFilePage = new EstateFilePage();
            bool errMsgAvail = estateFilePage.plotsErrorMsgAvailability();
            while (!errMsgAvail)
            {
                estateFilePage.enterPlotSize(plotSize);
                estateFilePage.searchAddressAndSelectAddr(postCode);
                estateFilePage.selectHouseType();
                SeleniumExtensions.scrollToBtnBasedOnLabelAndClick(btnName);
            }

        }

        [Given(@"User opens '(.*)' from '(.*)'")]
        public void GivenUserOpenFrom(string fieldName, string storageContext)
        {
            try
            {
                String testData = null;
                if (storageContext.Equals("scenarioContext", StringComparison.InvariantCultureIgnoreCase))
                {
                    testData = (string)_scenarioContext[fieldName];//"Newcastle-upon-Tyne - 07/12/2024 19:06";//
                }
                SeleniumExtensions.openEstateFile(testData);
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

        [Given(@"User validated '(.*)' link is created under '(.*)' by clicking on '(.*)'")]
        public void GivenUserValidatedLinkIsCreatedUnderByClickingOn(string linkName, string gridName, string btnName)
        {
            try
            {
                EstateFilePage estateFile = new EstateFilePage();
                switch (gridName)
                {
                    case "Estate Actions":
                        estateFile.isDataEnhancementLiskDisplayed(linkName, btnName);
                        break;
                    case "Outbound Correspondence":
                        estateFile.isCorrespondenceLinkDisplayed(linkName, btnName);
                        break;
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

        public void GivenUserUnblocksTheEstateFileByApprovingTheReview()
        {
            var testData = inputoutputdata.testData;
            var estateFilePage = new EstateFilePage();
            estateFilePage.UnblockEstateFileByApproving();
        }

        [Given(@"User '(.*)' the House Type")]
        public void GivenUserTheHouseType(string approvalStatus)
        {
            var testData = inputoutputdata.testData;
            var estateFilePage = new EstateFilePage();
            estateFilePage.ApproveHouseType(approvalStatus, testData["OutcomeReason"]);

        }

        [Given(@"User select '(.*)' plot")]
        public void GivenUserSelectPlot(string plotType)
        {
            try
            {
                SeleniumExtensions.scrollToCheckBoxAndClick(plotType);
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

        [Given(@"User clicked '(.*)' button contains name on '(.*)' section")]
        public void GivenUserClickedButtonContainsNameOnSection(string fieldName, string sectioName)
        {
            try
            {
                SeleniumExtensions.scrollToBtnContainsLabelAndClick(fieldName, sectioName);
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
       
    
    }
}
