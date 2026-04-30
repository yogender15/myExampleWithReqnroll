using NUnit.Framework;
using OpenQA.Selenium;
using POMSeleniumFrameworkPoc1.DTO;
using POMSeleniumFrameworkPoc1.Helpers;
using POMSeleniumFrameworkPoc1.Pages;
using POMSeleniumFrameworkPoc1.Pages.UI.CommonComponents;
using POMSeleniumFrameworkPoc1.Pages.UI.LAPortal;
using POMSeleniumFrameworkPoc1.Pages.UI.RBSP_BP;
using Reqnroll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace POMSeleniumFrameworkPoc1.Steps
{
    [Binding]
    public class FullDemolitionSteps
    {
        public ScenarioContext _scenarioContext;
        public NewJob newJobInformation;
        public FullDemolitionSteps(ScenarioContext context, NewJob _newJob)
        {
            this.newJobInformation = _newJob;
            _scenarioContext = context;
        }


        [Given(@"User is logged in")]
        public void GivenUserIsLoggedIn()
        {
            try
            {
                var loginPage = new LoginPage();
                if (Config.EnvironmentVal.Equals("UAT"))
                {
                    loginPage.GoToUATloginPage(Config.BaseUrl);

                }
                else
                {
                    loginPage.GoToLoginPage(Config.BaseUrl);

                }
                if (Config.BrowserType.Equals("edge"))
                {

                }
                else
                {
                    loginPage.LoginWithExistingUser();
                }
                SeleniumExtensions.WaitForReadyStateToComplete();
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

        

        [Given("User {string} is logged in to Dynamics application to work for {string} case")]
        public void GivenUserIsLoggedInToDynamicsApplicationToWorkForCase(string userRole, string caseName)
        {
            string userID = "";
            string pwd = "";
            foreach (KeyValuePair<string, string> userData in new userConfigHelper().getUserData(userRole))
            {
                userID = userData.Key;
                pwd = passwordEncryptor.Decrypt(userData.Value);
                Console.WriteLine($"userID: {userID}, pwd: {pwd}");
            }
            var loginPage = new LoginPage();
            loginPage.GoToLoginPage(Config.BaseUrl);
            if (Config.BrowserType.Equals("edge"))
            {

            }
            else
            {
                loginPage.LoginWithExistingUser();
            }
            PDF_Utility pdf_util = new PDF_Utility();
            pdf_util.initializeScreenshotsFile(caseName);
        }


       


        [Given(@"User is logged in on UAT")]
        public void GivenUserIsLoggedInOnUAT()
        {
            var loginPage = new LoginPage();
            loginPage.GoToLoginPage(Config.BaseUrl);
            // loginPage.LoginWithExistingUser();
        }

        //[Given(@"User is logged in to LA Portal")]
        //public void GivenUserIsLoggedInToLAPortal()
        //{
        //    var loginPage = new LoginPage();
        //    loginPage.GoToLoginPage(Config.LAPortalBaseUrl);
        //    var lAPortalLoginPage = new LAPortalLoginPage();
        //    lAPortalLoginPage.ClickStartNowBtnElement();
        //    lAPortalLoginPage.EnterLoginCredentialsAndClickSignInToLAPortal(Config.LAPortalUsername, Config.LAPortalPassword);
        //}

        [Given(@"User choose job menu")]
        public void GivenUserChooseJobMenu()
        {
            var jobPage = new JobPage();
            jobPage.ClickJobsMenu();
            jobPage.ClickOkOnScriptErrorWindow();
        }

        [Given(@"User choose Jobs menu under Services section")]
        public void GivenUserChooseJobsMenuUnderServicesSection()
        {
            var jobPage = new JobPage();
            jobPage.ClickCasesMenu();
        }

        [Given(@"User go to new job page")]
        public void GivenUserGoToNewJobPage()
        {
            var jobPage = new JobPage();
            jobPage.ClickPlusNewJobBtn();
        }

        [Given(@"User clicks Details tab")]
        public void GivenUserClicksDetailsTab()
        {
            var jobPage = new JobPage();
            jobPage.ClickDetailsTabUnderJobPage();
        }


        [Given(@"User select the origin as (.*)")]
        public void WhenUserSelectTheOriginAs(string origin)
        {
            var jobPage = new JobPage();
            jobPage.SelectOrigin(origin);
        }

        [When(@"User fill the party as (.*)")]
        public void WhenUserFillThePartyAsAndPartyTypeAs(string party)
        {
            var jobPage = new JobPage();
            jobPage.FillPartyDetails(party);
        }

        [When(@"User fill the hereditament as (.*)")]
        public void WhenUserFillTheHereditamentAs(string hereditament)
        {
            var jobPage = new JobPage();
            jobPage.FillHereditamentDetails(hereditament);
        }

        [When(@"User fill Request type as (.*)")]
        public void WhenUserFillRequestTypeAs(string requestType)
        {
            var jobPage = new JobPage();
            jobPage.FillRequestTypeOnJobCreation(requestType);
        }

        [When(@"User fill Coded Reason as (.*)")]
        public void WhenUserFillCodedReasonAs(string codedReason)
        {
            var jobPage = new JobPage();
            jobPage.FillCodedReasonOnJobCreation(codedReason);
        }

        [When(@"User pick the target date as \+(.*) days")]
        public void WhenUserPickTheTargetDateAsDays(int days)
        {
            var jobPage = new JobPage();
            jobPage.PickTargetDate(days);
        }

        [When(@"User enter the remarks as ""(.*)""")]
        public void WhenUserEnterTheRemarksAs(string remarks)
        {
            var requestPage = new RequestPage();
            requestPage.EnterRemarks(remarks);
        }


        [When(@"User selects ""(.*)"" from CCACType dropdown")]
        public void WhenUserSelectsFromCCACTypeDropdown(string categoryText)
        {
            var jobPage = new JobPage();
            jobPage.SelectCCACTypeCategorisation(categoryText);
        }

        [When(@"user selects proposed addess as ""(.*)""")]
        public void WhenUserSelectsProposedAddessAs(string proposedAddress)
        {
            var jobPage = new JobPage();
            jobPage.FillProposedAddress(proposedAddress);
        }


        [When(@"User selects Reason/Ground as ""(.*)""")]
        public void WhenUserSelectsReasonGroundAs(string reasonText)
        {
            var jobPage = new JobPage();
            jobPage.FillCCACTypeCategorisation(reasonText);
        }

        [Given(@"User selects RelationshipRole as ""(.*)""")]
        public void WhenUserSelectsRelationshipRoleAs(string relationshipRole)
        {
            var jobPage = new JobPage();
            jobPage.FillRelationshipRole(relationshipRole);
        }


        [When(@"User add TCTR Details with Ratepayer as (.*)")]
        public void WhenUserAddTCTRDetailsWithRatepayerAs(string tctrText)
        {
            var jobPage = new JobPage();
            jobPage.SetTCTRDetails(tctrText);
        }

        [When(@"User save the job")]
        public void WhenUserSaveTheJob()
        {
            var jobPage = new JobPage();
            newJobInformation.JobId = jobPage.SaveJob();
        }

        [When(@"User set auto process in the details tab as (.*)")]
        public void WhenUserSetAutoProcessInTheDetailsTabAs(string tctrText)
        {
            var jobPage = new JobPage();
            jobPage.EnterDetailsTabInfo(tctrText);
        }

        [When(@"User finish the release and publish with publish option as (.*)")]
        public void WhenUserFinishTheReleaseAndPublishWithPublishOptionAs(string publishOption)
        {
            var jobPage = new JobPage();
            jobPage.EnterReleaseAndPublishDetails(publishOption);
        }

        [Then(@"the created job is searchable")]
        public void ThenTheCreatedJobIsSearchable()
        {
            var jobPage = new JobPage();
            Assert.IsTrue(jobPage.IsJobCreatedSearchable(newJobInformation.JobId));
        }

        //[When(@"I click Single report tab")]
        //public void WhenIClickSingleReportTab()
        //{
        //    var lAPortalDashboardPage = new LAPortalDashboardPage();
        //    lAPortalDashboardPage.ClickCreateSingleReportLink();
        //}

        [When(@"I choose full demolition reason in Why are you creating this report\\? options")]
        public void WhenIChooseFullDemolitionReasonInWhyAreYouCreatingThisReportOptions()
        {
            var lAPortalDashboardPage = new LAPortalDashboardPage();
            lAPortalDashboardPage.SelectWhyAreYouCreatingFullDemolitionRadioBtnOption();
        }

        [When(@"I choose Material Increase reason in Why are you creating this report\\? options")]
        public void WhenIChooseMaterialIncreaseReasonInWhyAreYouCreatingThisReportOptions()
        {
            var lAPortalDashboardPage = new LAPortalDashboardPage();
            lAPortalDashboardPage.SelectWhyAreYouCreatingMaterialIncreaseRadioBtnOption();
        }


        [When(@"I enter Other Planning Portal Reference Number as ""(.*)""")]
        public void WhenIEnterOtherPlanningPortalReferenceNumberAs(string OtherPlanningPortalReference)
        {
            var lAPortalDashboardPage = new LAPortalDashboardPage();
            lAPortalDashboardPage.EnterOtherPlanningPortalReferenceNumberText(OtherPlanningPortalReference);
        }

        [When(@"I select Does the property have a planning portal reference as Yes")]
        public void WhenISelectDoesThePropertyHaveAPlanningPortalReferenceAsYes()
        {
            var lAPortalDashboardPage = new LAPortalDashboardPage();
            lAPortalDashboardPage.SelectPlanningPortalReferenceRadioYesBtnElement();
        }

        [When(@"I select Does the property have a planning portal reference as No")]
        public void WhenISelectDoesThePropertyHaveAPlanningPortalReferenceAsNo()
        {
            var lAPortalDashboardPage = new LAPortalDashboardPage();
            lAPortalDashboardPage.SelectPlanningPortalReferenceRadioNoBtnElement();
        }

        [When(@"I choose New Property CR(.*) reason in Why are you creating this report\\? options")]
        public void WhenIChooseNewPropertyCRReasonInWhyAreYouCreatingThisReportOptions(string businessProcess)
        {
            var lAPortalDashboardPage = new LAPortalDashboardPage();
            lAPortalDashboardPage.SelectWhyAreYouCreatingNewPropertyRadioBtnOption();
        }


        [When(@"I click confirm and report button")]
        [When(@"I click continue Button")]
        public void WhenIClickContinueButton()
        {
            var lAPortalDashboardPage = new LAPortalDashboardPage();
            lAPortalDashboardPage.ClickButtonContinue();
        }

        [When(@"I click continue Button on Confirm address Page")]
        public void WhenIClickContinueButtonOnConfirmAddressPage()
        {
            var lAPortalDashboardPage = new LAPortalDashboardPage();
            lAPortalDashboardPage.ClickContinueBtnOnConfirmAddressPage();
        }


        [When(@"I enter Use Billing Authority Report Number as ""(.*)""")]
        public void WhenIEnterUseBillingAuthorityReportNumberAs(string reportNumber)
        {
            var lAPortalDashboardPage = new LAPortalDashboardPage();
            lAPortalDashboardPage.EnterBillingAuthorityReportNumber(reportNumber);
        }

        [When(@"I enter Use Unique Property Reference Number as ""(.*)""")]
        public void WhenIEnterUseUniquePropertyReferenceNumberAs(string referenceNumber)
        {
            var lAPortalDashboardPage = new LAPortalDashboardPage();
            lAPortalDashboardPage.EnterBillingAuthorityReferenceNumber(referenceNumber);
        }

        [When(@"I enter the post code as ""(.*)""")]
        public void WhenIEnterThePostCodeAs(string postCode)
        {
            var lAPortalDashboardPage = new LAPortalDashboardPage();
            lAPortalDashboardPage.EnterPostCodeOnEnterThePostCodePropertyPage(postCode);
        }

        [When(@"I click FindAddress button")]
        public void WhenIClickFindAddressButton()
        {
            var lAPortalDashboardPage = new LAPortalDashboardPage();
            lAPortalDashboardPage.ClickFindAddress();
        }

        [When(@"I select the address ""(.*)"" from dropdown address list")]
        public void WhenISelectTheAddressFromDropdownAddressList(string address)
        {
            var lAPortalDashboardPage = new LAPortalDashboardPage();
            lAPortalDashboardPage.SelectAddressFromAddressList(address);
        }

        [When(@"I select Does the property have a planning portal reference as ""(.*)""")]
        public void WhenISelectDoesThePropertyHaveAPlanningPortalReferenceAs(string yesNoFlag)
        {
            var lAPortalDashboardPage = new LAPortalDashboardPage();
            lAPortalDashboardPage.SelectDoesPropertyHavePlanningPortalReferenceRadioBtn();
        }

        [When(@"I select Why does the property not have a planning portal reference as ""(.*)""")]
        public void WhenISelectWhyDoesThePropertyNotHaveAPlanningPortalReferenceAs(string reason)
        {
            var lAPortalDashboardPage = new LAPortalDashboardPage();
            lAPortalDashboardPage.SelectWhyDoesPropertyNotHavePlanningPortalReferenceRadioBtn();
        }


        [Then(@"I should be able to see Check and confirm details page with Unique Property Reference Number as ""(.*)""")]
        public void ThenIShouldBeAbleToSeeCheckAndConfirmDetailsPageWithUniquePropertyReferenceNumberAs(string referenceNumber)
        {
            var lAPortalDashboardPage = new LAPortalDashboardPage();
            Assert.IsTrue(lAPortalDashboardPage.VerifyBillingAuthorityReferenceNumberText(referenceNumber), "the Unique Property Reference Number is not dispalyed");
        }

        [When(@"I select Council Tax Band Property Radio button")]
        public void WhenISelectCouncilTaxBandPropertyRadioButton()
        {
            var lAPortalDashboardPage = new LAPortalDashboardPage();
            lAPortalDashboardPage.SelectCouncilTaxBandPropertyRadioBtn();
        }

        [When(@"I enter First Name as ""(.*)"" in Contact details for property")]
        public void WhenIEnterFirstNameAsInContactDetailsForProperty(string name)
        {
            var lAPortalDashboardPage = new LAPortalDashboardPage();
            lAPortalDashboardPage.EnterFirstName(name);
        }

        [When(@"I enter Last Name as ""(.*)"" in Contact details for property")]
        public void WhenIEnterLastNameAsInContactDetailsForProperty(string name)
        {
            var lAPortalDashboardPage = new LAPortalDashboardPage();
            lAPortalDashboardPage.EnterLastName(name);
        }

        [When(@"I select Owner Or Occupier Radio Button")]
        public void WhenISelectOwnerOrOccupierRadioButton()
        {
            var lAPortalDashboardPage = new LAPortalDashboardPage();
            lAPortalDashboardPage.SelectOwnerOrOccupierRadioBtn();
        }

        [When(@"I select Address The Same As The Property Address Radio Button")]
        public void WhenISelectAddressTheSameAsThePropertyAddressRadioButton()
        {
            var lAPortalDashboardPage = new LAPortalDashboardPage();
            lAPortalDashboardPage.SelectAddressTheSameAsThePropertyAddressRadioBtn();
        }

        [When(@"I enter Effective date in Change request details page")]
        public void WhenIEnterEffectiveDateInChangeRequestDetailsPage()
        {
            var lAPortalDashboardPage = new LAPortalDashboardPage();
            var dateAndTime = DateTime.Now;
            var effectiveDate = dateAndTime.AddDays(-2);
            int year = effectiveDate.Year;
            int month = effectiveDate.Month;
            int day = effectiveDate.Day;
            lAPortalDashboardPage.EnterEffectiveDateInChangeRequestDetailsPage(year.ToString(), month.ToString(), day.ToString());
        }

        [When(@"I enter remarks in Change request details page as ""(.*)""")]
        public void WhenIEnterRemarksInChangeRequestDetailsPageAs(string remarks)
        {
            var lAPortalDashboardPage = new LAPortalDashboardPage();
            lAPortalDashboardPage.EnterRemarksInTextareaRemarks(remarks);
        }

        [Then(@"I should be navigated to Check details before sending the report page that contains report reason as ""(.*)"", Property references as ""(.*)"", ""(.*)"", Council Tax band as ""(.*)""")]
        public void ThenIShouldBeNavigatedToCheckDetailsBeforeSendingTheReportPageThatContainsReportReasonAsPropertyReferencesAsCouncilTaxBandAs(string reportReason, string billingReference, string billingReport, string band)
        {
            var lAPortalDashboardPage = new LAPortalDashboardPage();
            Assert.IsTrue(lAPortalDashboardPage.VerifyDataDisplayedOnCheckDetailsBeforeSendingTheReportIsCorrect(reportReason, billingReference, billingReport, band), "the report reason or billing reference or billing report or council band are not displayed ");
        }

        [Then(@"I should be able to see ""(.*)"" message on the screen")]
        public void ThenIShouldBeAbleToSeeMessageOnTheScreen(string submittedMessage)
        {
            try
            {
                var lAPortalDashboardPage = new LAPortalDashboardPage();
                Assert.IsTrue(lAPortalDashboardPage.IsSingleReportConfirmationMessageIsDisplayed(submittedMessage), "You have successfully submitted your report to the Valuation Office Agency message is not displayed");
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

        [Then(@"user should be see list of jobs")]
        public void ThenUserShouldBeSeeListOfJobs()
        {
            var jobPage = new JobPage();
            Assert.IsTrue(jobPage.IsListOfJobsDisplayed(), "The list of jobs are not displayed");
        }

        [When(@"user clicks the Enquires tab")]
        public void WhenUserClicksTheEnquiresTab()
        {
            var enquiresPage = new EnquiresPage();
            enquiresPage.ClickEnquiriesTab();
        }

        [When(@"user selects \\+ New button")]
        public void WhenUserSelectsNewButton()
        {
            var enquiresPage = new EnquiresPage();
            enquiresPage.ClickEnquiriesNewBtn();
        }

        [Then(@"New Enquires page is displayed")]
        public void ThenNewEnquiresPageIsDisplayed()
        {
            var enquiresPage = new EnquiresPage();
            Assert.IsTrue(enquiresPage.VerifyNewEnquiryHeadlineTextIsDisplayed("New Enquiry"), "the new enquires headline text is not displayed");
        }

        [When(@"I pick Date received in VOA as (.*) days")]
        public void WhenIPickDateReceivedInVOAAsDays(int days)
        {
            var enquiresPage = new EnquiresPage();
            enquiresPage.PickDateReceivedInVOADateEnquiryPage(days);
        }

        [When(@"I enter Task Type as '(.*)'")]
        public void WhenIEnterTaskTypeAs(string taskType)
        {
            var enquiresPage = new EnquiresPage();
            enquiresPage.FillTaskTypeOnEnquiriesPage(taskType);
        }

        [When(@"I enter Call Type as '(.*)'")]
        public void WhenIEnterCallTypeAs(string callType)
        {
            var enquiresPage = new EnquiresPage();
            enquiresPage.FillCallTypeOnEnquiriesPage(callType);
        }

        [When(@"I enter Enquiry Type as '(.*)'")]
        public void WhenIEnterEnquiryTypeAs(string enquiryType)
        {
            var enquiresPage = new EnquiresPage();
            enquiresPage.FillEnquiryTypeOnEnquiriesPage(enquiryType);
        }

        [When(@"I enter Enquiry Sub Type as '(.*)'")]
        public void WhenIEnterEnquirySubTypeAs(string subEnquiryType)
        {
            var enquiresPage = new EnquiresPage();
            enquiresPage.FillEnquirySubTypeOnEnquiriesPage(subEnquiryType);
        }

        [When(@"User set flag for Has the Decision Letter / Notice been created\? as ""(.*)""")]
        public void WhenUserSetFlagForHasTheDecisionLetterNoticeBeenCreatedAs(string dropDown)
        {
            var bPfPage = new BPFPage();
            bPfPage.SelectHasTheDecisionLetterNoticeBeenCreatedDropDown(dropDown);
        }


        [Then(@"an error message ""(.*)"" ""(.*)"" should be displayed")]
        public void ThenAnErrorMessageShouldBeDisplayed(string BAReferenceError, string BAReportError)
        {
            var lAPortalDashboardPage = new LAPortalDashboardPage();
            Assert.IsTrue(lAPortalDashboardPage.EnterABillingAuthorityReferenceReportNumbersErrorMessagesDisplayed(BAReferenceError, BAReportError), "A Billing Authority Reference and Report Number Error Messages are not Displayed");
        }

        [When(@"User clicks Coded Reason and enter ""(.*)""")]
        public void WhenUserClicksCodedReasonAndEnter(string codedReason)
        {
            var jobPage = new JobPage();
            jobPage.ClickCodedReason(codedReason);
        }

        [When(@"User clicks Advanced lookup to fill on coded reason")]
        public void WhenUserClicksAdvancedLookupToFillOnCodedReason()
        {
            var jobPage = new JobPage();
            jobPage.ClickAdvancedLookupOnCodedReasonlink();
        }

        [When(@"User clicks Coded Reason Lookup View \(default\) dropdown")]
        public void WhenUserClicksCodedReasonLookupViewDefaultDropdown()
        {
            var jobPage = new JobPage();
            jobPage.ClickCodedReasonLookupViewOnAdvancedLookUpInJobpge();
        }

        [When(@"Users selects Active Coded Reason from DropDown")]
        public void WhenUsersSelectsActiveCodedReasonFromDropDown()
        {
            var jobPage = new JobPage();
            jobPage.SelectActiveCodedReasonDropDownOnAdvancedLookUpInJobPage();
        }

        [When(@"User selects ""(.*)"" option from the list of coded reasons")]
        public void WhenUserSelectsOptionFromTheListOfCodedReasons(string codedReasonItem)
        {
            var jobPage = new JobPage();
            jobPage.SelectReconstitutionMergeCTOptionAdvancedLookUp();

        }

        [When(@"User clicks Done button")]
        public void WhenUserClicksDoneButton()
        {
            var jobPage = new JobPage();
            jobPage.ClickDoneBtnInAdvancedLookUpCodedReasonJobPage();
        }

        [When(@"User clicks Bulk Creation of Jobs tab")]
        public void WhenUserClicksBulkCreationOfJobsTab()
        {
            var jobPage = new JobPage();
            jobPage.ClickBulkCreationOfJobsTabOnJobPage();
        }

        [When(@"I click Add Existing Hereditament link")]
        public void WhenIClickAddExistingHereditamentLink()
        {
            var jobPage = new JobPage();
            jobPage.ClickAddExistingHereditamentLinkUnderBulkCreationJobs();
        }

        [When(@"I search for the hereditament as ""(.*)""")]
        public void WhenISearchForTheHereditamentAs(string hereditament)
        {
            var jobPage = new JobPage();
            jobPage.FillHereditamentDetails(hereditament);
        }

        [When(@"I search for the hereditament under search box on Bulk creation of Jobs page as ""(.*)""")]
        public void WhenISearchForTheHereditamentUnderSearchBoxOnBulkCreationOfJobsPageAs(string hereditament)
        {
            var bulkCreationOfJobsPage = new BulkCreationOfJobsPage();
            bulkCreationOfJobsPage.FillHereditamentDetailsUnderBulkCreationJobspage(hereditament);
        }

        [When(@"I click Add button")]
        public void WhenIClickAddButton()
        {
            var jobPage = new JobPage();
            jobPage.ClickAddBtnUnderBulkCreationOfJobsTab();
        }

        [When(@"I click Create Bulk Jobs button")]
        public void WhenIClickCreateBulkJobsButton()
        {
            var bulkCreationOfJobsPage = new BulkCreationOfJobsPage();
            bulkCreationOfJobsPage.ClickCreateBulkJobsBtnUnderBulkCreationOfJobsTab();
        }

        [When(@"I click Continue button on Continue to Create Bulk jobs pop up window")]
        public void WhenIClickContinueButtonOnContinueToCreateBulkJobsPopUpWindow()
        {
            var jobPage = new JobPage();
            jobPage.ClickContinueBtnUnderBulkCreationOfJobsTab();
        }

        [When(@"the user clicks Overview tab")]
        public void WhenTheUserClicksOverviewTab()
        {
            var jobPage = new JobPage();
            jobPage.ClickOverviewTabOnJobsPage();
        }

        [Then(@"I should see the Geo map loaded")]
        public void ThenIShouldSeeTheGeoMapLoaded()
        {
            var jobPage = new JobPage();
            Assert.IsTrue(jobPage.VerifyMapViewUnderOverviewTabOnJobsPage(), "The Geo Map is not displayed under Overview Tab");
        }

        [When(@"the user clicks VMS tab")]
        public void WhenTheUserClicksVMSTab()
        {
            var hereditamentsPage = new HereditamentsPage();
            hereditamentsPage.ClickVMSTabUnderHereditaments();
        }

        [Then(@"I should see the Geo map loaded on a separate tab")]
        public void ThenIShouldSeeTheGeoMapLoadedOnASeparateTab()
        {
            var hereditamentsPage = new HereditamentsPage();
            Assert.IsTrue(hereditamentsPage.VerifyGeoMapLoadedUnderVMSUnderHereditaments(), "the Geo Map is not loaded under VMS Tab under hereditaments in a new window");
        }

        [Then(@"I should see the Geo map loaded under address section")]
        public void ThenIShouldSeeTheGeoMapLoadedUnderAddressSection()
        {
            var hereditamentsPage = new HereditamentsPage();
            Assert.IsTrue(hereditamentsPage.VerifyGeoMapLoadedUnderAddressSectionUnderHereditaments(), "the Geo Map is loaded under Address section under Hereditaments");
        }

        [When(@"I click Job Actions drop down on the main Nav")]
        public void WhenIClickJobActionsDropDownOnTheMainNav()
        {
            var raiseAnAlternativeJobPage = new RaiseAnAlternativeJob();
            raiseAnAlternativeJobPage.ClickJobActionsDropDownOnMainNav();
        }

        [When(@"I select Raise an Alternative Job option")]
        public void WhenISelectRaiseAnAlternativeJobOption()
        {
            var raiseAnAlternativeJobPage = new RaiseAnAlternativeJob();
            raiseAnAlternativeJobPage.SelectRaiseAnAlternativeJobDropDownOnMainNav();
        }

        [When(@"I fill Alternate Job Type as ""(.*)""")]
        public void WhenIFillAlternateJobTypeAs(string alternateJobName)
        {
            var raiseAnAlternativeJobPage = new RaiseAnAlternativeJob();
            raiseAnAlternativeJobPage.FillAlternateJobCodedReasonLookUpField(alternateJobName);
        }

        [When(@"I click Alternate Job Coded Reason search button")]
        public void WhenIClickAlternateJobCodedReasonSearchButton()
        {
            var raiseAnAlternativeJobPage = new RaiseAnAlternativeJob();
            raiseAnAlternativeJobPage.ClickAlternateJobCodedReasonSearchField();
        }

        [When(@"I click Advanced Lookup option")]
        public void WhenIClickAdvancedLookupOption()
        {
            var raiseAnAlternativeJobPage = new RaiseAnAlternativeJob();
            raiseAnAlternativeJobPage.ClickAlternateJobAdvancedLookUpFieldOption();
        }

        [When(@"I click Coded Reason default Chevron list")]
        public void WhenIClickCodedReasonDefaultChevronList()
        {
            var raiseAnAlternativeJobPage = new RaiseAnAlternativeJob();
            raiseAnAlternativeJobPage.ClickCodedReasonChevron();
        }

        [When(@"I select Active coded reasons from coded reason drop down list")]
        public void WhenISelectActiveCodedReasonsFromCodedReasonDropDownList()
        {

            var raiseAnAlternativeJobPage = new RaiseAnAlternativeJob();
            raiseAnAlternativeJobPage.SelectActiveCodedReasonFromDrpDownList();
        }

        [When(@"I click Choose coded reason for Job Type search box")]
        public void WhenIClickChooseCodedReasonForJobTypeSearchBox()
        {
            var raiseAnAlternativeJobPage = new RaiseAnAlternativeJob();
            raiseAnAlternativeJobPage.SelectActiveCodedReasonFromDrpDownList();
        }

        [When(@"I fill coded reason as ""(.*)""")]
        public void WhenIFillCodedReasonAs(string codedReason)
        {
            var raiseAnAlternativeJobPage = new RaiseAnAlternativeJob();
            raiseAnAlternativeJobPage.SelectTheDeletionCodedReasonInResultList(codedReason);
        }

        [When(@"I click close on the dialog window")]
        public void WhenIClickCloseOnTheDialogWindow()
        {
            var raiseAnAlternativeJobPage = new RaiseAnAlternativeJob();
            raiseAnAlternativeJobPage.ClickCloseButtonOnDialog();
        }


        [When(@"I click Done button")]
        public void WhenIClickDoneButton()
        {
            var raiseAnAlternativeJobPage = new RaiseAnAlternativeJob();
            raiseAnAlternativeJobPage.ClickDoneButtonOnAdvancedLookUpWindow();
        }

        [When(@"I pick the Alternate Job Proposed Effective Date as (.*) days")]
        public void WhenIPickTheAlternateJobProposedEffectiveDateAsDays(int days)
        {
            var raiseAnAlternativeJobPage = new RaiseAnAlternativeJob();
            raiseAnAlternativeJobPage.PickProposedAlternativeJobDate(days);
        }

        [When(@"I click Save and Close button")]
        public void WhenIClickSaveAndCloseButton()
        {
            var raiseAnAlternativeJobPage = new RaiseAnAlternativeJob();
            raiseAnAlternativeJobPage.ClickSaveAndCloseOnDialog();
        }

        [Then(@"I should see Alternate text in header of the alternative job created")]
        public void ThenIShouldSeeAlternateTextInHeaderOfTheAlternativeJobCreated()
        {
            var raiseAnAlternativeJobPage = new RaiseAnAlternativeJob();
            Assert.IsTrue(raiseAnAlternativeJobPage.VerifyHeaderTitleHasAlternative());
        }

        [When(@"I select Council tax App")]
        public void WhenISelectCouncilTaxApp()
        {
            var appsListPage = new AppsListPage();
            appsListPage.ClickCouncilTaxAppIcon();
        }

        [When(@"I select Council tax App on UAT")]
        public void WhenISelectCouncilTaxAppOnUAT()
        {
            var appsListPage = new AppsListPage();
            appsListPage.ClickCouncilTaxAppIcon();
        }


    }
}

