using POMSeleniumFrameworkPoc1.Helpers;
using System;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace POMSeleniumFrameworkPoc1.Pages
{
    public partial class IndividualJobPage : BasePage
    {
        private IWebElement BPFHeaderButton => Driver.WaitForElement(By.CssSelector("[id*='businessProcessFlowFlyoutHeaderContainer'] [id*='flyoutHeaderButtonsContainer'] button"));
        private By ReasearchQuestionnaireList => By.CssSelector("[id*='processHeaderStageFlyoutInnerContainer'] [id*='fieldSectionItemContainer'] label");

        // Selecting Job from Jobs Page
        private IWebElement JobsViewSelector => Driver.WaitForElement(By.CssSelector("button[id*='ViewSelector'] i[data-icon-name='ChevronDown']"));
        private By JobRows => By.XPath("//*[contains(@class,'ag-center-cols-container') and @role='rowgroup']//*[contains(@class,'ag-row-position-absolute')]");

        private IWebElement JobsViewValue(string displayFilterValue)
        {
            string customizeSelector = $"//*[@aria-label='View Options']//ul//li//button//label[text()='{displayFilterValue}']";
            return Driver.WaitForElement(By.XPath(customizeSelector));
        }

        // Research BPF Check Facts  -> Gneeral Tab
        private IWebElement JobIDJobLevel => Driver.WaitForElement(By.CssSelector("[aria-label='Job ID']"));

        private IWebElement BillingAuth => Driver.WaitForElement(By.CssSelector("section[aria-label='General'] input[aria-label*='Billing Authority']"));
        private IWebElement BARef => Driver.WaitForElement(By.CssSelector("section[aria-label='General'] select[aria-label*='BA Reference']"));
        private IWebElement DOR => Driver.WaitForElement(By.CssSelector("section[aria-label='General'] input[aria-label*='Date of Receipt']"));
        private IWebElement PED => Driver.WaitForElement(By.CssSelector("section[aria-label='General'] input[aria-label*='Proposed Effective Date']"));
        private IWebElement IsDRC => Driver.WaitForElement(By.CssSelector("section[aria-label='General'] select[aria-label*='Is Desktop Research Complete?']"));
        private IWebElement LAC => Driver.WaitForElement(By.CssSelector("section[aria-label='General'] select[aria-label*='Linked Assessment Confirmed?']"));
        private IWebElement ResearcherRemark => Driver.WaitForElement(By.CssSelector("section[aria-label='General'] select[aria-label*='Researcher Remarks']"));
        private IWebElement CI => Driver.WaitForElement(By.CssSelector("section[aria-label='General'] textarea[aria-label*='Composite Indicator']"));
        private IWebElement AssociatedCR10 => Driver.WaitForElement(By.CssSelector("section[aria-label='General'] input[aria-label*='Has Associated CR10']"));


        // Inspection Tab
        private IWebElement InspoectionReqToggleBtn => Driver.WaitForElement(By.CssSelector("input[aria-label*='Is an Inspection Required?']"));
        private IWebElement InspectionReqToggleTxt => Driver.WaitForElement(By.CssSelector("button[aria-label*='Is an Inspection Required?']+label"));

        private IWebElement InspectionReason => Driver.WaitForElement(By.CssSelector("button[aria-label='Reason for Inspection']"));

        private IWebElement InspectionReasonComment => Driver.WaitForElement(By.CssSelector("textarea[aria-label='Reason For Inspection Comments']"));

        private By InternalInspectionSLA => By.CssSelector("input[aria-label='Date of Internal Inspection SLA']");

        private IWebElement InspectionAllocation => Driver.WaitForElement(By.CssSelector("button[aria-label='Inspection Allocation']"));

        private IWebElement InspectionJobRefresh => Driver.WaitForElement(By.CssSelector("[data-control-name='Inspections'] ul[aria-label='Inspection Task Commands'] li button[aria-label='Refresh']"));
        private IWebElement InspectionJobID => Driver.WaitForElement(By.CssSelector("[data-control-name='Inspections'] [data-id='data-set-body-container'] [aria-label='Editable Grid'] [aria-label='Data'] [data-id='cell-0-2']"));

        private IWebElement InspectionDialogMenuOption(string optionToClick)
        {
            string customizeSelector = $"ul[aria-label='Commands'][data-id='CommandBar'] li button[aria-label*='{optionToClick}']";
            return Driver.WaitForElement(By.CssSelector(customizeSelector));
        }

        private IWebElement ReasonForInsDialog => Driver.WaitForElement(By.CssSelector("[role='dialog'] section[aria-label='General'] select[aria-label='Reason for Inspection']"));
        private IWebElement ReasonForInsCommentsDialog => Driver.WaitForElement(By.CssSelector("[role='dialog'] section[aria-label='General'] textarea[aria-label='Reason for Inspection Comments']"));
        private IWebElement InspectionTypeDialog => Driver.WaitForElement(By.CssSelector("section[aria-label='General'] select[aria-label='Inspection Type']"));


        // Ask For Help
        // "[role='dialog'] section[aria-label='General'] [aria-label='Description']"
        private IWebElement AFHSubject => Driver.WaitForElement(By.CssSelector("[role='dialog'] section[aria-label='General'] [aria-label='Subject']"));
        private IWebElement AFHDescription => Driver.WaitForElement(By.CssSelector("[role='dialog'] section[aria-label='General'] [aria-label='Description']"));

        private IWebElement ASHSave => Driver.WaitForElement(By.CssSelector("[role='form'] [aria-label='Commands'][data-lp-id='commandbar-Form:voa_askforhelp'] li button[aria-label*='Save (CTRL+S)']"));
        private IWebElement AFHMarkComplete => Driver.WaitForElement(By.CssSelector("[role='form'] [aria-label='Commands'][data-lp-id='commandbar-Form:voa_askforhelp'] li button[aria-label*='Mark Complete']"));

        //PAD Entry Tab --> Property Attributes
        private By PA_GroupLookUp => By.CssSelector("section[aria-label='Property Attributes'] input[aria-label='Group, Lookup']");
        private By PA_GroupLookUpSrchBtn => By.CssSelector("section[aria-label='Property Attributes'] input[aria-label='Group, Lookup']+button[title='Search']");
        private By PA_TypeLookUp => By.CssSelector("section[aria-label='Property Attributes'] input[aria-label='Type, Lookup']");
        private By PA_TypeLookUpSearchBtn => By.CssSelector("section[aria-label='Property Attributes'] input[aria-label='Type, Lookup']+button[title='Search']");
        private IWebElement PA_AgeCode => Driver.WaitForElement(By.CssSelector("section[aria-label='Property Attributes'] input[aria-label='Age Code, Lookup']"));
        private By PA_HeatingCode => By.CssSelector("section[aria-label='Property Attributes'] input[aria-label='Heating Code, Lookup']");
        public By GrpSearchButton => By.CssSelector("[aria-label='Search records for Group, Lookup field']");
        private IWebElement PA_Area => Driver.WaitForElement(By.CssSelector("section[aria-label='Property Attributes'] input[aria-label= 'Area of Dwelling (m2)']"));
        private IWebElement PA_Rooms => Driver.WaitForElement(By.CssSelector("section[aria-label='Property Attributes'] input[aria-label='Rooms']"));
        private IWebElement PA_Bedrooms => Driver.WaitForElement(By.CssSelector("section[aria-label='Property Attributes'] input[aria-label= 'Bedrooms']"));
        private IWebElement PA_Baths => Driver.WaitForElement(By.CssSelector("section[aria-label='Property Attributes'] input[aria-label= 'Bathrooms']"));
        private IWebElement PA_Floors => Driver.WaitForElement(By.CssSelector("section[aria-label='Property Attributes'] input[aria-label= 'Floors']"));
        private IWebElement PA_Level => Driver.WaitForElement(By.CssSelector("section[aria-label='Property Attributes'] input[aria-label= 'Level']"));
        private IWebElement PA_PlotSize => Driver.WaitForElement(By.CssSelector("section[aria-label='Property Attributes'] input[aria-label= 'Plot Size']"));
        private By PA_ParkingLookUp => By.CssSelector("section[aria-label='Property Attributes'] input[aria-label= 'Parking, Lookup']");

        private IWebElement PA_Heating => Driver.WaitForElement(By.CssSelector("section[aria-label='Property Attributes'] ul[title='Heating Code'] li div[id*='voa_heatingcode.fieldControl-LookupResultsDropdown'][role='link']"));
        private IWebElement PA_RoomsErrorMessage => Driver.WaitForElement(By.CssSelector("section[aria-label='Property Attributes'] [data-id='voa_rooms'] [data-id='voa_rooms-error-message']"));

        private By PA_ConservatoryType => By.CssSelector("input[aria-label='Conservatory Type, Lookup']");
        private IWebElement PA_ConservatoryArea => Driver.WaitForElement(By.CssSelector("section[aria-label='Property Attributes'] input[aria-label='Conservatory Area (m2)']"));

        // PAD Entry --> VSC
        private IWebElement NewVSCButton => Driver.WaitForElement(By.CssSelector("section[aria-label='General'] ul[aria-label='Domestic Property Value Significant Code Commands'] li button[aria-label*='Add New Value Significant Code']"));

        // Domestic Property VSC Page
        private By DP_ValueSignificantCode => By.CssSelector("section[aria-label='General'] input[aria-label='Value Significant Code, Lookup']");
        private By DP_ValueSignificantCodeSearchBtn => By.CssSelector("section[aria-label='General'] input[aria-label='Value Significant Code, Lookup']+button[title='Search']");
        private IWebElement DP_VSCDescription => Driver.WaitForElement(By.CssSelector("section[aria-label='General'] input[aria-label='Description']"));

        // PAD Entry --> SourceCode
        private IWebElement NewSourceCodeButton => Driver.WaitForElement(By.CssSelector("section[aria-label='Source Codes'] ul[aria-label='Attribute Set Source Code Commands'] li button[aria-label*='Add New Source Code']"));

        // Source Code
        private By SourceCode => By.CssSelector("section[aria-label='General'] input[aria-label='Source Code, Lookup']");
        private IWebElement SC_Comment => Driver.WaitForElement(By.CssSelector("section[aria-label='General'] textarea[aria-label='Comment']"));


        // PAD Entry --> PAD Validation
        // "[id*='processHeaderStageFlyoutInnerContainer'] [id*='fieldSectionItemContainer'] select[aria-label='Is Banding Complete?'] option[data-selected='true']"
        private IWebElement IsBandingCompleteBPFValue => Driver.WaitForElement(By.CssSelector("[id*='processHeaderStageFlyoutInnerContainer'] [id*='fieldSectionItemContainer'] input[aria-label='Banding Outcome']"));
        private IWebElement PADValidationOutcome => Driver.WaitForElement(By.CssSelector("section[aria-label='PAD Validation'] input[aria-label='PAD Validation Outcome']"));
        private IWebElement ValidationFailureTxt => Driver.WaitForElement(By.CssSelector("[aria-label='Validation Failure Reason']"));

        // Banding Decision Section
        private IWebElement SaveInProgressError => Driver.WaitForElement(By.CssSelector("[data-id='errorDialogdialog'] [data-id='dialogTitleText']"));
        private IWebElement SaveInProgressOKBtn => Driver.WaitForElement(By.CssSelector("[data-id='errorDialogdialog'] button[aria-label='OK']"));

        private IWebElement BandingTabError => Driver.WaitForElement(By.CssSelector("[data-id ='voa_ctbandingid_container'] [id *= 'error-text']"));
        private IWebElement SubjectAttributeSection => Driver.WaitForElement(By.CssSelector("section[aria-label='Subject Attributes'] h2[title='Subject Attributes']"));

        private By ProposedTaxBand => By.CssSelector("section[aria-label='Banding Decision'] input[aria-label='Proposed Tax Band, Lookup']");
        private By ProposedTaxBandSearchBtn => By.CssSelector("section[aria-label='Banding Decision'] input[aria-label='Proposed Tax Band, Lookup']+button[title='Search']");

        private By AssessmentAction => By.CssSelector("section[aria-label= 'Banding Decision'] input[aria-label= 'Assessment Action, Lookup']");
        private IWebElement IsBandingComplete => Driver.WaitForElement(By.CssSelector("[aria-label= 'Banding Decision'] button[aria-label='Is Banding Complete?']"));
        private IWebElement IsApplyEffectiveDatechangeRules => Driver.WaitForElement(By.CssSelector("[aria-label= 'Banding Decision'] select[aria-label='Apply Effective Date change Rules?']"));

        private IWebElement ExistingTaxBand => Driver.WaitForElement(By.CssSelector("ul[title*='Current Tax Band']"));

        private IWebElement BandChangeDirection => Driver.WaitForElement(By.CssSelector("input[aria-label*='Band Change Direction']"));


        //  "//*[@data-msdyn-rteplaceholder='Enter text...']"
        private By OuterFrameCaseWorkerConclusion => By.XPath("(//iframe[contains(@title,'Designer')])[1]");
        //    private IWebElement OuterFrameCaseWorkerConclusion => By.XPath("(//iframe[contains(@title,'Designer')])[1]");
        private By InnerFrameCaseWorkerConclusion => By.XPath("//iframe[contains(@title,'Caseworker Conclusions')]");
        //    private IWebElement InnerFrameCaseWorkerConclusion => By.XPath("//iframe[contains(@title,'Caseworker Conclusions')]");
        private IWebElement TextAreaCWConclusion => Driver.WaitForElement(By.CssSelector("[aria-label*='voa_caseworkerconclusionsremarksthoughtprocess'] p"));

        // Details--> Check facts
        private IWebElement TabsInSection(string sectionName, string tabName)
        {
            string customizeSelector = $"//section[@aria-label='{sectionName}']//*[@role='tablist']//button//span[text()='{tabName}']";
            return Driver.WaitForElement(By.XPath(customizeSelector));
        }
        private IWebElement FereditamentField(string fieldName)
        {
            string customizeSelector = $"section[aria-label='Hereditament Details'] input[aria-label='{fieldName}']";
            return Driver.WaitForElement(By.CssSelector(customizeSelector));
        }
        // "section[aria-label='Hereditament Details'] ul[title='Hereditament'] li [role='link']"
        private IWebElement HereditamentLink => Driver.WaitForElement(By.CssSelector("section[aria-label='Hereditament Details'] ul[title='Hereditament'] li [role='link']"));
        private IWebElement AddressLinkOnDialog => Driver.WaitForElement(By.CssSelector("[role='dialog'] section[aria-label='Address'] ul[title='Address'] li [role='link']"));

        //  "[data-id='routerDialogdialog'] [aria-label='Leave this page?']:first"
        private IWebElement LeaveThisPagePopUp => Driver.WaitForElement(By.CssSelector("[data-id='routerDialogdialog'] [aria-label='Leave this page?']"));
        private IWebElement LeaveThisPagePopUpOKBtn => Driver.WaitForElement(By.CssSelector("[data-id='routerDialogdialog'] button[aria-label='OK']"));

        private IWebElement UPRNText => Driver.WaitForElement(By.CssSelector("section[aria-label='Address Details'] input[aria-label='UPRN']"));

        // Job resolution Pop Up

        private IWebElement ResolutionType => Driver.WaitForElement(By.CssSelector("section[data-lp-id*='incidentresolution'] select[aria-label='Resolution Type']"));
        private IWebElement BARemarks => Driver.WaitForElement(By.CssSelector("section[data-lp-id*='incidentresolution'] input[aria-label='Billing Authority Remarks']"));

        private IWebElement JR_Remarks => Driver.WaitForElement(By.CssSelector("section[data-lp-id*='incidentresolution'] textarea[aria-label='Remarks']"));

        private IWebElement SaveAndCloseNoAction => Driver.WaitForElement(By.CssSelector("section[data-lp-id*='incidentresolution'] button[aria-label='Save and Close']"));


        private IWebElement VMSHeader => Driver.WaitForElement(By.XPath("//*[@aria-label='Header Controller']//*[contains(@class,'titles')]//*[contains(@class,'jimu-title')]"));


        private IWebElement VMSError => Driver.WaitForElement(By.XPath("//*[contains(@id,'error')]//p[text()='We cannot seem to find details for the property.']"));
        private IWebElement VMSHereditamentsText => Driver.WaitForElement(By.XPath("//*[contains(@id,'CTBT-table-parent')]//summary[text()]"));
        private IWebElement VMSWindowIFrame => Driver.WaitForElement(By.CssSelector("iframe#vmsIframe"));
        private By VMSWindowIFrameSelector => By.CssSelector("iframe#vmsIframe");

        // Documents Tab
        private IWebElement DocumentMessage => Driver.WaitForElement(By.XPath("//*[contains(@class,'ms-MessageBar')]//*[contains(@class,'ms-MessageBar-innerText')]/span"));
        private IWebElement UploadButton => Driver.WaitForElement(By.XPath("//*[contains(@class,'DocumentHandler')]//*[contains(@class,'ms-MessageBar-innerText')]"));

        private IWebElement editoption(String itemName)
        {

            string xpath = $"//ul[contains(@class,'ms-ContextualMenu-list')]//span[text()='{itemName}']";
            return Driver.WaitForElement(By.XPath(xpath));

        }

        private IWebElement DocumentTabButton(string buttonName)
        {
            string customizeSelector = $"//*[contains(@class,'ms-OverflowSet')]//button//*[text()='{buttonName}']";
            return Driver.WaitForElement(By.XPath(customizeSelector));
        }
        private IWebElement UploadThisJobOption => Driver.WaitForElement(By.XPath("//ul[contains(@class,'ms-ContextualMenu')]//li//button//*[text()='This Job']"));

        private IWebElement ChooseFilesButton => Driver.WaitForElement(By.XPath("//*[contains(@class,'ms-Modal-scrollableContent')]//button[@value='Choose Files']//*[contains(@class,'ms-Button-label') and text()='Choose Files']"));
        private IWebElement DropdownOnDocumentUploadDialog(string dropdownName)
        {
            string customizeSelector = $"(//*[contains(@class,'ms-Modal-scrollableContent')]//label[text()='{dropdownName}']//following-sibling::*[contains(@class,'ms-Dropdown-container')]//i)[1]";
            return Driver.WaitForElement(By.XPath(customizeSelector));
        }

        private IWebElement DropdownOptionOnDocumentDialog(string optionName)
        {
            string customizeSelector = $"//*[contains(@class,'dropdownItemsWrapper')]//button//*[text()='{optionName}']";
            return Driver.WaitForElement(By.XPath(customizeSelector));
        }
        private IWebElement ButtonOnDocDialog(string buttonName)
        {
            string customizeSelector = $"//*[contains(@class,'ms-Modal-scrollableContent')]//button//*[text()='{buttonName}']";
            return Driver.WaitForElement(By.XPath(customizeSelector));
        }

        // Maintain Assessment
        //  //*[@class='PowerCATCommandBar']//*[@data-icon-name='Refresh']/parent::*/parent::button

        // "[id*='processHeaderStageFlyoutInnerContainer'] [id*='fieldSectionItemContainer'] ul[title='Correspondence Assessment'] li [role='link']"
        private IWebElement RefreshOnAssesmment => Driver.WaitForElement(By.XPath("//*[@class='PowerCATCommandBar']//*[@data-icon-name='Refresh']/parent::*/parent::button"));
        private IWebElement CorrespondenceAssessmentValue => Driver.WaitForElement(By.CssSelector("[id*='processHeaderStageFlyoutInnerContainer'] [id*='fieldSectionItemContainer'] ul[title='Correspondence Assessment'] li [role='link']"));
        // "[id*='processHeaderStageFlyoutInnerContainer'] [aria-label='Correspondence Assessment, Lookup']"
        private By CorrespondenceAssessmentLookup => By.CssSelector("[id*='processHeaderStageFlyoutInnerContainer'] [aria-label='Correspondence Assessment, Lookup']");

        private IWebElement SelectAssessmentCheckTab(string tabName)
        {
            string customizeSelector = $"[class='PowerCATFluentPivot'] [role='tablist'] [name='{tabName}']";
            return Driver.WaitForElement(By.CssSelector(customizeSelector));
        }
        private IWebElement AssesmentOuterIFrame => Driver.WaitForElement(By.CssSelector("iframe[title='Power Apps']"));
        private By AssessmentOuterIFrameSelector => By.CssSelector("iframe[title='Power Apps']");

        private IWebElement AssesmentInnerIFrame => Driver.WaitForElement(By.CssSelector("iframe[class='player-app-frame']"));
        private By AssessmentInnerIFrameSelector => By.CssSelector("iframe[class='player-app-frame']");


        // [data-automationid='DetailsRow'] [data-automationid='DetailsRowFields'] [data-automation-key='col99'] span
        //  "[data-automationid='DetailsList'] [class='ms-DetailsList-contentWrapper'] [class='ms-List-cell'] [data-automationid='DetailsRow'][data-selection-index='1'] [data-automation-key='col97']"

        //   "[data-automationid='DetailsRow'][data-selection-index='2']
        private IWebElement AssessmentRows(int indexNum)
        {
            string customizeSelector = $"[data-automationid='DetailsRow'][data-selection-index='{indexNum}']";
            return Driver.WaitForElement(By.CssSelector(customizeSelector));
        }

        private IWebElement UIEffectiveFromText(int indexNum)
        {
            string customizeSelector = $"[data-automationid='DetailsRow'][data-selection-index='{indexNum}'] [data-automationid='DetailsRowFields'] [data-automation-key='col95'] span";
            return Driver.WaitForElement(By.CssSelector(customizeSelector));
        }
        private IWebElement UIEffectiveToText(int indexNum)
        {
            string customizeSelector = $"[data-automationid='DetailsRow'][data-selection-index='{indexNum}'] [data-automationid='DetailsRowFields'] [data-automation-key='col96'] span";
            return Driver.WaitForElement(By.CssSelector(customizeSelector));
        }
        private IWebElement UIAssessmentBandText(int indexNum)
        {
            string customizeSelector = $"[data-automationid='DetailsRow'][data-selection-index='{indexNum}'] [data-automationid='DetailsRowFields'] [data-automation-key='col97'] span";
            return Driver.WaitForElement(By.CssSelector(customizeSelector));
        }
        private IWebElement UICompositeIndicator(int indexNum)
        {
            string customizeSelector = $"[data-automationid='DetailsRow'][data-selection-index='{indexNum}'] [data-automationid='DetailsRowFields'] [data-automation-key='col98'] span";
            return Driver.WaitForElement(By.CssSelector(customizeSelector));
        }
        private IWebElement UIAssessmentStatusText(int indexNum)
        {
            string customizeSelector = $"[data-automationid='DetailsRow'][data-selection-index='{indexNum}'] [data-automationid='DetailsRowFields'] [data-automation-key='col100'] span";
            return Driver.WaitForElement(By.CssSelector(customizeSelector));
        }
        private IWebElement UIPublicationStatusText(int indexNum)
        {
            string customizeSelector = $"[data-automationid='DetailsRow'][data-selection-index='{indexNum}'] [data-automationid='DetailsRowFields'] [data-automation-key='col101'] span";
            return Driver.WaitForElement(By.CssSelector(customizeSelector));
        }

        private IWebElement UIAssessmentActionText(int indexNum)
        {
            string customizeSelector = $"[data-automationid='DetailsRow'][data-selection-index='{indexNum}'] [data-automationid='DetailsRowFields'] [data-automation-key='col102'] span";
            return Driver.WaitForElement(By.CssSelector(customizeSelector));
        }

        private By AssessmentRowFields => By.CssSelector("[data-automationid='DetailsRow'] [data-automationid='DetailsRowFields']");
        private IWebElement AssessmentBandText => Driver.WaitForElement(By.CssSelector("[data-automationid='DetailsRow'] [data-automationid='DetailsRowFields'] [data-automation-key='col97'] span"));
        private IWebElement AssessmentStatusText => Driver.WaitForElement(By.CssSelector("[data-automationid='DetailsRow'] [data-automationid='DetailsRowFields'] [data-automation-key='col100'] span"));

        private IWebElement PublicationStatusText => Driver.WaitForElement(By.CssSelector("[data-automationid='DetailsRow'] [data-automationid='DetailsRowFields'] [data-automation-key='col101'] span"));


        private IWebElement QualityAssuranceCompleteBPF => Driver.WaitForElement(By.CssSelector("[id*='processHeaderStageFlyoutInnerContainer'] [id*='fieldSectionItemContainer'] select[aria-label='System Requires Quality Assurance?']"));

        private IWebElement CorrespondenceChecksCompleteBPF => Driver.WaitForElement(By.CssSelector("[id*='processHeaderStageFlyoutInnerContainer'] [id*='fieldSectionItemContainer'] button[aria-label='Correspondence Checks Complete?']"));
        private IWebElement MaintainAssessmentChecksCompleteBPF => Driver.WaitForElement(By.CssSelector("[id*='processHeaderStageFlyoutInnerContainer'] [id*='fieldSectionItemContainer'] button[aria-label*='Maintain Assessment Complete?']"));

        // "[data-id='confirmdialog'] [data-id='dialogTitleText']"
        private IWebElement QAQCPopUp => Driver.WaitForElement(By.CssSelector("[data-id='confirmdialog'] [data-id='dialogTitleText']"));
        private IWebElement QAQCPopUpContinueButton => Driver.WaitForElement(By.CssSelector("[data-id='confirmdialog'] button[aria-label='Continue']"));
        private IWebElement QARequestTitle => Driver.WaitForElement(By.CssSelector("[role='dialog'] [aria-label='QA Request']"));
        private IWebElement QARequestCloseButton => Driver.WaitForElement(By.CssSelector("[aria-labelledby*='defaultDialogChromeTitle'][role='dialog'] [aria-label='Close']"));
        private IWebElement QARequestNoButton => Driver.WaitForElement(By.CssSelector("[role='dialog'] [data-id*='ButtonCanvas'] button:contains('No')"));
        private IWebElement AllJobsCreatedButton => Driver.WaitForElement(By.CssSelector("[data-id='confirmdialog'] button[title='Yes']"));
        private IWebElement AllJobsCreatedNoButton => Driver.WaitForElement(By.CssSelector("[data-id='confirmdialog'] button[title='No']"));

        //Approve
        // "textarea[aria-label='Approval Decision Reason']"
        private IWebElement ApproveDropDown => Driver.WaitForElement(By.CssSelector("select[aria-label='Approved?']"));
        private IWebElement ApproveReason => Driver.WaitForElement(By.CssSelector("textarea[aria-label='Approval Decision Reason']"));

        // Check Facts
        private By IsDekstopResearchCompleteByLocator => By.CssSelector("section[aria-label='General'] select[aria-label='Is Desktop Research Complete?']");
        private IWebElement IsDekstopResearchComplete => Driver.WaitForElement(By.CssSelector("section[aria-label='General'] button[aria-label='Is Desktop Research Complete?']"));
        private IWebElement MultiUnitAccomodatnSectn => Driver.WaitForElement(By.CssSelector("section[aria-label='Multi Unit Accomodation']"));
        private By LinkedAssessmentConfirmedByLocator => By.CssSelector("section[aria-label='General'] select[aria-label='Linked Assessment Confirmed?']");
        private IWebElement LinkedAssessmentConfirmed => Driver.WaitForElement(By.CssSelector("section[aria-label='General'] select[aria-label='Linked Assessment Confirmed?']"));
        private IWebElement FormalDecisionNotes => Driver.WaitForElement(By.CssSelector("section[aria-label='General'] textarea[aria-label='Formal Decision Notes']"));
        private IWebElement LinkAssessmentLink => Driver.WaitForElement(By.CssSelector("section[aria-label='Linked Assessment'] ul[title='Linked Assessment'] li [role='link']"));


        // More Tabs Options

        public string JobId;

        public By TypeSearchButton => By.CssSelector("[aria-label='Search records for Type, Lookup field']");

        public By AgeSearchButton => By.CssSelector("[aria-label='Search records for Age Code, Lookup field']");

        private IWebElement ValidatePADToggleTxt => Driver.WaitForElement(By.CssSelector("[aria-label= 'PAD Validation'] button[aria-label*='Validate PAD Code']+label"));
        private IWebElement ValidatePADToggleBtn => Driver.WaitForElement(By.CssSelector("section[aria-label= 'PAD Validation'] button[aria-label*='Validate PAD Code']"));

        private By MaterialRequestRows => By.XPath("//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row')]");

        private By AddressRows => By.XPath("//div[@role='grid']//*[@role='row' and @data-automationid='DetailsRow']");

        private IWebElement UIBillingAuthorityText(int rowindex)
        {
            string customizeSelector = $"//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row') and @row-index='{rowindex}']//*[contains(@col-id,'voa_billingauthorityid')]//span";
            return Driver.WaitForElement(By.XPath(customizeSelector));
        }

        private IWebElement UIBillingAuthorityReferenceText(int rowindex)
        {
            string customizeSelector = $"//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row') and @row-index='{rowindex}']//*[contains(@col-id,'voa_billingauthorityreference')]//span";
            return Driver.WaitForElement(By.XPath(customizeSelector));
        }


        private IWebElement UIEffectiveFromDateText(int rowindex)
        {
            string customizeSelector = $"//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row') and @row-index='{rowindex}']//*[contains(@col-id,'voa_effectivefrom')]//span";
            return Driver.WaitForElement(By.XPath(customizeSelector));
        }

        private IWebElement UIEffectiveToDateText(int rowindex)
        {
            string customizeSelector = $"//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row') and @row-index='{rowindex}']//*[contains(@col-id,'voa_effectiveto')]//span";
            return Driver.WaitForElement(By.XPath(customizeSelector));
        }

        private IWebElement UIStatusIdText(int rowindex)
        {
            string customizeSelector = $"//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row') and @row-index='{rowindex}']//*[contains(@col-id,'voa_statusid')]//span";
            return Driver.WaitForElement(By.XPath(customizeSelector));
        }


        private IWebElement ConfirmAllJobs => Driver.WaitForElement(By.CssSelector("[data-id='confirmButton']"));
        // "section[aria-label='Handoff Administration'] input[aria-label='Routing Region Name']"
        private IWebElement RoutingRegionName => Driver.WaitForElement(By.CssSelector("section[aria-label='Handoff Administration'] input[aria-label='Routing Region Name']"));

        // "[data-id='voa_newproposedeffectivedate'] input[id*='DatePicker']"
        private By NewProposedEffectiveDate => By.CssSelector("[data-id='voa_newproposedeffectivedate'] input[id*='DatePicker']");
        private IWebElement NewProposedEffectiveDateELement => Driver.WaitForElement(By.CssSelector("[data-id='voa_newproposedeffectivedate'] input[id*='DatePicker']"));
        private IWebElement NewProposedEffectiveDateReason => Driver.WaitForElement(By.CssSelector("[data-id='voa_neweffectivedatereason'] textarea[aria-label*='New Effective Date Reason']"));
        private IWebElement ProposedDatePickerSelector => Driver.WaitForElement(By.CssSelector("input[aria-label='Date of Proposed Effective Date']"));
        private By AppSynchrinizatiomMessage => By.CssSelector("[id='appProgressIndicatorMessage'][aria-label*='A value is being synchronised between records']");

        private IWebElement CloseBtn => Driver.WaitForElement(By.CssSelector("[aria-label='Close']"));

        private IWebElement DocumentType => Driver.WaitForElement(By.XPath("//label[text()='Document Type']//following::div[1]//input"));
        private IWebElement ConfirmandLockBanding => Driver.WaitForElement(By.CssSelector("[title='Confirm and Lock Banding']"));
        private IWebElement NRDAssessmentOkButton => Driver.WaitForElement(By.CssSelector("button[data-id='confirmButton']"));
    }
}
