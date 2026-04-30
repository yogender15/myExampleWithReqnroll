using OpenQA.Selenium;
using POMSeleniumFrameworkPoc1.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POMSeleniumFrameworkPoc1.Pages.UI.CommonComponents
{
    public partial class RequestPage : BasePage
    {
        public IWebElement RequestsMenuElement => Driver.WaitForElement(By.CssSelector("li[aria-label='Requests']"));
        public IWebElement NewRequestPlusBtnElement => Driver.WaitForElement(By.CssSelector("button[aria-label='New']"));
        private IWebElement JobTypeSearchIconRequestPageElement => Driver.WaitForElement(By.CssSelector("button[aria-label='Search records for Job Type, Lookup field']"));
        private IWebElement JobTypeTextBoxRequestPageElement => Driver.WaitForElement(By.CssSelector("input[aria-label='Coded Reason, Lookup']"));
        private IWebElement JobTypeValueRequestPageElement => Driver.WaitForElement(By.XPath("//span[@data-id='voa_codedreasonid.fieldControl-voa_name0_0_0']"));
        private IWebElement RequestIDTextBox => Driver.WaitForElement(By.XPath("//input[@aria-label='Request ID']"));
        private By RequestIDTextBoxSelector => By.XPath("//input[@aria-label='Request ID']");
        public By RequestTypeLookupField => By.CssSelector("section[aria-label='Categorisation'] input[aria-label='Request Type, Lookup']");

        public By RequestTypeSearchBtn => By.CssSelector("section[aria-label='Categorisation'] input[aria-label='Request Type, Lookup']+button[title='Search']");
        private IWebElement IsCaravanBoat => Driver.WaitForElement(By.CssSelector("section[aria-label='Categorisation'] button[aria-label='Is Caravan, Boat or Annex?']"));
        private By SavingAlert => By.XPath("//*[@aria-live='polite' and @role='alert' and (contains(text(),'Saving'))]");

        public By JobSubTypeLookupField => By.CssSelector("section[aria-label='Categorisation'] input[aria-label='Sub Job Type, Lookup']");
        private By JobSubTypeSearchBtn => By.CssSelector("section[aria-label='Categorisation'] input[aria-label='Sub Job Type, Lookup']+button[title='Search']");
        public By ReconTypeLookupField => By.CssSelector("section[aria-label='Categorisation'] input[aria-label='Reconstitution Type, Lookup']");
        private By ReconTypeSearchBtn => By.CssSelector("section[aria-label='Categorisation'] input[aria-label='Reconstitution Type, Lookup']+button[title='Search']");

        private By JobTypeLookUpTextFieldSlector => By.CssSelector("section[aria-label='Categorisation'] input[aria-label='Job Type, Lookup']");
        private By JobTypeSearchBtn => By.CssSelector("section[aria-label='Categorisation'] input[aria-label='Job Type, Lookup']+button[title='Search']");

        //   [data-lp-id='form-header-title'] h1
        private IWebElement RequestTitle => Driver.WaitForElement(By.CssSelector("[data-lp-id='form-header-title'] h1"));
        private IWebElement RequestSaveStatus => Driver.WaitForElement(By.CssSelector("[data-lp-id='form-header-title'] h1 [role='status']"));

        private IWebElement RequestSavedLabel => Driver.WaitForElement(By.CssSelector("[aria-label='Save status - Saved']"));
        private IWebElement RelatedJobsSubTab => Driver.WaitForElement(By.CssSelector("li[aria-label='Related Jobs']"));
        private IWebElement RequestActionMenu => Driver.WaitForElement(By.CssSelector("[aria-label='Request Action More Commands. Request Action']"));
        private IWebElement ValidateRequestSubMenu => Driver.WaitForElement(By.CssSelector("[id*='RequestAction'] li button[title*='Validate the request']"));
        private IWebElement SaveAndCloseOnRequestValidationDialog => Driver.WaitForElement(By.CssSelector("[role='dialog'] [data-id*='outerHeaderContainer'] button[id*='requestline'] [title*='Save & Close']"));
        private IWebElement JobCreatedRowOnRelatedJobs => Driver.WaitForElement(By.CssSelector("label[aria-label*='JOB-']"));
        private IWebElement JobIdFieldOnRequestPage => Driver.WaitForElement(By.CssSelector("[aria-label='Job ID']"));
        //private By ProposedBillingTypeLookUpTextFieldSlector => By.CssSelector("input[aria-label='Look for Proposed Billing Authority']");
        private By ProposedBillingTypeLookUpTextFieldSlector => By.CssSelector("input[aria-label='Proposed Billing Authority, Lookup']");
        private By AddressLabelLookUpTextfieldSelector => By.CssSelector("[placeholder*='Start typing to search for an address label']");
        private By ProposedBAReferenceNumberTextFieldSelector => By.CssSelector("section[aria-label='BA Reference'] input[aria-label='Proposed BA Reference Number']");
        private By ProposedBARefNum => By.CssSelector("section[aria-label='BA Reference'] input[aria-label='Proposed BA Reference Number']");
        private IWebElement ProposedBARefNumEle => Driver.WaitForElement(By.CssSelector("section[aria-label='Billing Authority Link'] input[aria-label='Proposed BA Reference Number']"));
        private IWebElement BALinkSection => Driver.WaitForElement(By.CssSelector("section[aria-label='Billing Authority Link']"));
        private IWebElement SuppliedBARef => Driver.WaitForElement(By.CssSelector("section[aria-label='Billing Authority Link'] input[aria-label='Supplied BA Reference Number']"));
        private IWebElement SuppliedBARefNumEle => Driver.WaitForElement(By.CssSelector("section[aria-label='Billing Authority Link'] input[aria-label='Supplied BA Reference Number']"));

        private IWebElement ScrollDivRequestPage => Driver.WaitForElement(By.CssSelector("[id*='tab-section']"));
        private IWebElement SubmitJobSubMenu => Driver.WaitForElement(By.CssSelector("button[aria-label='Submit Job. Create Hereditament and Job']"));
        private IWebElement OkButtonOnDialog => Driver.WaitForElements(By.CssSelector("button[aria-label='OK']")).LastOrDefault();
        private IWebElement ProceedButtonOnDialog => Driver.WaitForElement(By.CssSelector("[data-id='confirmdialog'] button[title='Proceed']"));
        private IWebElement ConfirmButtonOnDialog => Driver.WaitForElement(By.CssSelector("button[id*='confirmButton']"));

        private IWebElement NoButton => Driver.WaitForElements(By.CssSelector("button[aria-label='No']")).LastOrDefault();

        private List<IWebElement> AddressLabelList => Driver.WaitForElements(By.CssSelector(".ms-Callout-main .ms-Suggestions-item button")).ToList();
        private By AddresslabelNotificationDialog_Selector => By.CssSelector("[data-id='alertdialog'] span[data-id='dialogMessageText']");
        private IWebElement OkBtnOnAddresslabelNotificationDialog => Driver.WaitForElement(By.CssSelector("button[data-id='okButton']"));
        private IWebElement AddressLabelSearch => Driver.WaitForElement(By.CssSelector("input.ms-BasePicker-input"));
        private By AddressLookUpSuggestionsList_Selector => By.ClassName("ms-Suggestions-item");

        private IWebElement NoActionJobSubMenu => Driver.WaitForElement(By.CssSelector("button[aria-label='No Action']"));

        private IWebElement NoActionName => Driver.WaitForElement(By.CssSelector("section[data-id='quickCreateRoot'] input[aria-label='Name']"));

        private IWebElement Channel => Driver.WaitForElement(By.CssSelector("button[aria-label='Channel']"));
        //[role='listbox'] [role='option']:contains('Phone')
        private IWebElement AuthorizationRequired => Driver.WaitForElement(By.CssSelector("button[aria-label='Authorisation to Act Required']"));
        private IWebElement AuthorizationDecision => Driver.WaitForElement(By.CssSelector("button[aria-label='Authorisation to Act Decision']"));
        private IWebElement PropertyMistake => Driver.WaitForElement(By.CssSelector("button[aria-label='Property Deleted By Mistake?']"));
        private IWebElement BAReportNumber => Driver.WaitForElement(By.CssSelector("section[aria-label='Details'] input[aria-label='BA Report Number']"));

        public By DSRLookUpField => By.CssSelector("section[aria-label='Details'] input[aria-label='Data Source Role, Lookup']");

        public By CTPayerField => By.CssSelector("section[aria-label='Ratepayer / CT Payer Details'] [aria-label='CT Payer, Lookup']");

        public By DSRSearchBtn => By.CssSelector("section[aria-label='Details'] input[aria-label='Data Source Role, Lookup']+button[title='Search']");

        public By CTPlayerLookUp => By.CssSelector("section[aria-label*='CT Payer Details'] input[aria-label='CT Payer, Lookup']");
        public By CTPlayerLookUpSearchBtn => By.CssSelector("section[aria-label*='CT Payer Details'] input[aria-label='CT Payer, Lookup']+button[title='Search']");

        private By ReasonForValidationTextAreaSelector => By.CssSelector("textarea[aria-label='Reason For Validation']");
        private IWebElement ReasonForValidation => Driver.WaitForElement(By.CssSelector("textarea[aria-label='Reason For Validation']"));

        //private IWebElement SuppliedBARef => Driver.WaitForElement(By.CssSelector("[aria-label='Supplied BA Reference Number']"));
        private IWebElement ReasonForPortalReference => Driver.WaitForElement(By.CssSelector("textarea[aria-label='Reason for Portal Reference Omission']"));
        private IWebElement ReasonForPortalOmission => Driver.WaitForElement(By.CssSelector("section[aria-label='Details'] textarea[aria-label='Reason for Portal Reference Omission']"));
        private IWebElement remarks => Driver.WaitForElement(By.CssSelector("textarea[aria-label='Remarks']"));
        private IWebElement RemarksInRequest => Driver.WaitForElement(By.CssSelector("div textarea[aria-label='Remarks']"));

        private IWebElement SavedTextOnRequestPage => Driver.WaitForElement(By.CssSelector("span[aria-label='Save status - Saved']"));

        private IWebElement jobIDEle => Driver.WaitForElement(By.XPath("//*[@col-id='ticketnumber' and not(contains(@class,'header'))]"), 3);
        private By jobIDEleBy => By.XPath("//*[@col-id='ticketnumber' and not(contains(@class,'header'))]");
        private By correspondencejobIDEleBy => By.XPath("//*[contains(@aria-label,'OBC-')]");

        private By msgJobIDEleBy => By.XPath("//*[contains(@aria-label,'MSG-')]");

        private IWebElement jobNameEle => Driver.WaitForElement(By.XPath("//*[@col-id='title' and not(contains(@class,'header'))]"), 3);

        private IWebElement InspectionJobIDEle => Driver.WaitForElement(By.CssSelector("[data-control-name='Inspections'] [data-id='data-set-body-container'] [aria-label='Editable Grid'] [aria-label='Data'] [data-id='cell-0-2']"));

        private IWebElement InspectionJobRefresh => Driver.WaitForElement(By.CssSelector("[data-control-name='Inspections'] ul[aria-label='Inspection Task Commands'] li button[aria-label='Refresh']"));

        public string RequestJobId;
        public string RequestId;

        //************Ashish;s Code**********************
        private By EstateFileLookup => By.CssSelector("section[aria-label= 'CR03 New Property (Estate)'] input[aria-label= 'Estate File, Lookup']");
        private IWebElement FindPlotButton => Driver.WaitForElement(By.CssSelector("section[aria-label='CR03 New Property (Estate)'] button span[class*='ms-Button-textContainer']"));

        private IWebElement RequestActionValidateRequest => Driver.WaitForElement(By.CssSelector("[id*='RequestAction'] ul li button[aria-label*='Validate Request'] img"));

        // Details Section (Request Page)
        private IWebElement DatePickerDialog => Driver.WaitForElement(By.CssSelector("[role='dialog'][aria-label='Calendar']"));
        private By DatePickerDialogByLocator => By.CssSelector("[role='dialog'][aria-label='Calendar']");

        private IWebElement MonthAndYearEleOnDatePicker => Driver.WaitForElement(By.CssSelector("[role='dialog'][aria-label='Calendar'] button[class*='monthAndYear']"));
        private IWebElement PreviousEleOnDatePicker => Driver.WaitForElement(By.CssSelector("[role='dialog'][aria-label='Calendar'] button[title*='Navigate to previous'] i"));
        private IWebElement NextEleOnDatePicker => Driver.WaitForElement(By.CssSelector("[role='dialog'][aria-label='Calendar'] button[title*='Navigate to next'] i"));
        private IWebElement YearTextOnDatePicker => Driver.WaitForElement(By.CssSelector("[role='dialog'][aria-label='Calendar'] [class*='monthPickerWrapper'] button[aria-label*='Month picker for year'] *"));
        private IWebElement DayElementForDatePicker(string dayValue)
        {
            string customizeSelector = $"[role='dialog'][aria-label='Calendar'] tr td[class*='dayCell']:not([class*='dayOutsideNavigatedMonth']) button span:contains('{dayValue}')";
            return Driver.WaitForElement(By.CssSelector(customizeSelector));
        }
        private IWebElement MonthElementForDatePicker(string MonthValue)
        {
            string customizeSelector = $"[role ='dialog'][aria-label ='Calendar'] button[aria-label ='{MonthValue}']";
            return Driver.WaitForElement(By.CssSelector(customizeSelector));
        }
        private By ProposedDatePickerSelector => By.CssSelector("input[aria-label='Date of Proposed Effective Date']");

        private By ProposedDatePickerDialogSelector => By.CssSelector("div[data-id*='proposedeffectivedate'] input[aria-label='Date of Proposed Effective Date']");

        private By ReceivedDatePickerSelector => By.CssSelector("input[aria-label='Date of Date Received']");

        private By ProposedEffectiveDatePickerSelector => By.CssSelector("section[aria-label='Details'] [data-id='voa_proposedeffectivedate'] input[aria-label='Date of Proposed Effective Date']");

        private By EvidenceDatePickerSelector => By.CssSelector("input[aria-label='Date of Evidence Sufficient Date']");
        private By TargetDatePickerSelector => By.CssSelector("input[aria-label='Target Date']");
        private By DateReceivedDatePicker => By.CssSelector("input[aria-label='Date of Date Received']");

        private IWebElement DialogSaveAndClose => Driver.WaitForElement(By.CssSelector("[role='dialog'] ul[data-id='CommandBar'] li button[aria-label='Save & Close']"));
        private By DialogSaveAndCloseUsingBY => By.CssSelector("[role='dialog'] ul[data-id='CommandBar'] li button[aria-label='Save & Close']");

        private IWebElement DialogSave => Driver.WaitForElement(By.CssSelector("[role='dialog'] ul[data-id='CommandBar'] li button[aria-label='Save (CTRL+S)']"));

        private IWebElement DialogCompleteAcceptanceCheck => Driver.WaitForElement(By.CssSelector("button[aria-label='Complete Acceptance Check']"));
        private IWebElement OptionsFromRelatedTab(String optionToSelect)
        {
            string customizeSelector = $"[aria-label = '{optionToSelect} Related - Common']";
            return Driver.WaitForElement(By.CssSelector(customizeSelector));
        }

        private IWebElement RequestActionOption(String optionToSelect)
        {
            string customizeSelector = $"[id*='RequestAction'] ul li button[aria-label*='{optionToSelect}']";
            return Driver.WaitForElement(By.CssSelector(customizeSelector));
        }
        //Find Plot Dialog
        private By PlotsList => By.CssSelector("[class*='ms-Modal-scrollableContent'] [class='ms-List'] [class='ms-List-cell'] [data-automationid='DetailsRow']");

        private By PlotRowCheck => By.CssSelector("[data-automationid='DetailsRow'] [data-automationid='DetailsRowCheck']");
        private IWebElement SelectButton => Driver.WaitForElement(By.XPath("//*[contains(@class,'ms-Modal-scrollableContent')]//button//span[contains(@class,'ms-Button-label') and contains(text(),'Select')]"));

        //Auto-Processing Tab
        private IWebElement RunTimeStatusLabel => Driver.WaitForElement(By.XPath("//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row')]//*[@col-id='voa_runtimestatus']//label"));
        private IWebElement AutoProcRefreshBtn => Driver.WaitForElement(By.XPath("//ul[@aria-label='Durable Function Monitor Commands']//li//button[@aria-label='Refresh']"));
        //  "//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row')]"
        private By TabRowsElement => By.XPath("//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row')]");

        //Child Jobs Tab
        private IWebElement StatusCode => Driver.WaitForElement(By.XPath("//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row')]//*[@col-id='statecode']//label"));
        private IWebElement ChildJobsRefreshBtn => Driver.WaitForElement(By.XPath("//ul[@aria-label='Job Commands']//li//button[@aria-label='Refresh']"));

        // No Action  Pop-up Elements
        private By NoActionCodeInput => By.CssSelector("[data-control-name='drpNoActionCode'] select");
        // drpNoActionSubCode
        private By NoActionSubCodeLookUp => By.CssSelector("[data-control-name='drpNoActionSubCode'] select");

        // No Action  Pop-up Dialog Elements
        private IWebElement NoActionCode => Driver.WaitForElement(By.CssSelector("[data-control-name='drpNoActionCode'] select"));

        private IWebElement NoActionSubCode => Driver.WaitForElement(By.CssSelector("[data-control-name='drpNoActionSubCode'] select"));


        private IWebElement InternalRemarks => Driver.WaitForElement(By.CssSelector("[data-control-name='txtInternalRemarks'] textarea"));
        private IWebElement ContinueBtn => Driver.WaitForElement(By.CssSelector("[data-control-name='btnContinue_NDS'] button"));

        private IWebElement SaveAndCloseNoAction => Driver.WaitForElement(By.CssSelector("section[data-lp-id*='voa_noactiondetail'] button[aria-label='Save and Close']"));

        // Find Hereditament Dialog for Deletion
        private IWebElement FindHereditament => Driver.WaitForElement(By.XPath("//button//*[text()='Find Hereditament']"));
        private IWebElement FindHereditamentForSwap => Driver.WaitForElement(By.CssSelector("section[aria-label='Swap Hereditament'] [aria-label='Find Hereditament']"));

        private IWebElement PostCodeOnDialog => Driver.WaitForElement(By.XPath("//*[contains(@class,'ms-Dialog-main')]//label[text()='Postcode']//following-sibling::*//input"));
        private IWebElement StreetOnDialog => Driver.WaitForElement(By.XPath("//*[contains(@class,'ms-Dialog-main')]//label[text()='Street']//following-sibling::*//input"));
        private IWebElement TownCityOnDialog => Driver.WaitForElement(By.XPath("//*[contains(@class,'ms-Dialog-main')]//label[text()='Town/City']//following-sibling::*//input"));
        private IWebElement BuildingNameNumOnDialog => Driver.WaitForElement(By.XPath("//*[contains(@class,'ms-Dialog-main')]//label[text()='Building Name/Number']//following-sibling::*//input"));

        private IWebElement SearchOnDialog => Driver.WaitForElement(By.XPath("//*[contains(@class,'ms-Dialog-main')]//button//*[text()='Search']"));

        private By AddressToggle => By.XPath("//*[@data-automationid='DetailsList']//*[@data-automationid='DetailsRow']//*[@role='radio' and contains(@class,'ms-DetailsRow')]");

        private IWebElement FindHereditamentAddressHeaders => Driver.WaitForElement(By.XPath("//*[@data-automationid='DetailsHeader']//*[@role='columnheader' and @data-item-key='address_column']//i"));

        private By AddressRow => By.XPath("//*[contains(@class,'ms-Modal-scrollableContent')]//div[@data-automationid='DetailsList']//div[@data-automationid='DetailsRow']");

        private By Research_AddressRow => By.XPath("//div[@aria-label='Address search results grid']//*[@data-automationid='DetailsRow']");

        private By uprN_Li => By.XPath("//*[@data-automationid='DetailsList']//*[@data-automationid='DetailsRow']//div[@data-automation-key='uprn']/div");

        private IWebElement SelectOnDIalog => Driver.WaitForElement(By.XPath("//*[contains(@class,'ms-Dialog-main')]//button//*[text()='Select']"));
        private IWebElement NextPageOnDialog => Driver.WaitForElement(By.XPath("//*[contains(@class,'ms-Dialog-main')]//button[@title='Next Page' and @data-is-focusable='true']//i[@data-icon-name='Next']"));
            //"//*[contains(@class,'ms-Dialog-main')]//button[@title='Next Page']//i[@data-icon-name='Next']"));
        private By NextPageOnDialogBy => By.XPath("//*[contains(@class,'ms-Dialog-main')]//button[@title='Next Page']//i[@data-icon-name='Next']");

        private IWebElement NextPageButtonOnDialog => Driver.WaitForElement(By.XPath("//*[contains(@class,'ms-Dialog-main')]//button[@title='Next Page']"));
        private By AddressStatus => By.XPath("//*[contains(@class,'cellCheck')]//following-sibling::*//*[@data-automation-key='addressstatus_column']");
        ////parent::*//parent::*[@data-automationid='DetailsRow']"
        private By AddressRowByIndex => By.XPath("//parent::*//parent::*[@data-automationid='DetailsRow']");

        // Alternate Job Pop-Up
        private By AlternateJobType => By.CssSelector("[id='quickCreateTabContainer'] [aria-label='Job Details'] [aria-label='Job Type, Lookup']");
        private By AlternateJobTypeSearchBtn => By.CssSelector("[id='quickCreateTabContainer'] [aria-label='Job Details'] [aria-label='Job Type, Lookup']+button[title='Search']");
        //  "[data-id='quickCreateRoot'] [aria-label='Save and Close']"
        private IWebElement ALternateJobSaveAndClose => Driver.WaitForElement(By.CssSelector("[data-id='quickCreateRoot'] [aria-label='Save and Close']"));
        private IWebElement LeavethispageDialog => Driver.WaitForElement(By.CssSelector("[data-id*='routerDialog'] [id*='dialogTitle'] [aria-label='Leave this page?']"));
        private IWebElement LeavethispageDialogOKBtn => Driver.WaitForElement(By.CssSelector("[data-id*='routerDialog'] button[title='OK']"));

        // "[class*='ms-Panel-header'] [role='heading']:contains('Duplicate Addresses Identified')"
        private IWebElement FindAddress => Driver.WaitForElement(By.CssSelector("section[aria-label='New Hereditament Details'] button[aria-label='Find Address']"));

        private By estatesFindAddress => By.CssSelector("section[aria-label='Addresses'] button[aria-label='Find Address']");

        private IWebElement FindAddress_New => Driver.WaitForElement(By.CssSelector("button[aria-label='Find Address']"));

        private By FindAddressUsingBy => By.CssSelector("section[aria-label='New Hereditament Details'] button[aria-label='Find Address']");

        private By FindAddressUsingNewBy => By.CssSelector("section[aria-label='Change of Address'] button[aria-label='Find Address']");
        private IWebElement AddressSearchInputOnDialog => Driver.WaitForElement(By.CssSelector("[class*='ms-Modal-scrollableConten'] input[name='Address Search Input']"));
        private By AddressSearchInputOnDialogBy => By.CssSelector("[class*='ms-Modal-scrollableConten'] input[name='Address Search Input']");

        private By AddressSearchInputOnDialogUsingBy => By.CssSelector("[class*='ms-Modal-scrollableConten'] input[name='Address Search Input']");
        private IWebElement BtnSearchOnDialog => Driver.WaitForElement(By.CssSelector("[class*='ms-Modal-scrollableConten'] button[aria-label='Click to search for address']"));
        private By BtnSearchOnDialogBy => By.CssSelector("[class*='ms-Modal-scrollableConten'] button[aria-label='Click to search for address']");

        private IWebElement BtnUseAddressOnDialog => Driver.WaitForElement(By.XPath("//button//*[text()='Use Address']"));
        private By BtnUseAddressOnDialogBy => By.XPath("//button//*[text()='Use Address']");

        private IWebElement StatusReasonAddress => Driver.WaitForElement(By.CssSelector("[aria-label='Status Reason']"));
        private IWebElement VOADesc => Driver.WaitForElement(By.CssSelector("input[title*='VOA Description']"));
        private By DuplicateAddressHeader => By.XPath("//*[contains(@class,'ms-Panel-header')]//*[@role='heading' and contains(text(),'Duplicate Addresses Identified')]");

        private By DuplicateAddressForEstate_Old => By.XPath("//*[contains(text(),'Address is already Commit on the Council Tax List')]");

        private By DuplicateAddressForEstate => By.XPath("//*[contains(text(),'failed duplicate validation')]");

        private IWebElement DuplicateAddressCloseBtn => Driver.WaitForElement(By.CssSelector("[class*='ms-Panel-navigation'] button[title='Close']"));

        private By okBtnOnModalDialog => By.CssSelector("div[class*='ms-Stack'] button[class*='ms-Button ms-Button--default']");

        private IWebElement ValidationRemarks => Driver.WaitForElement(By.CssSelector("[role='dialog'] [aria-label='Validation Remarks']"));

        private IWebElement PopupButton => Driver.WaitForElement(By.CssSelector("[aria-label='Enter full screen mode']"));

        private IWebElement ReasonForPortalReferenceOmmission => Driver.WaitForElement(By.CssSelector("[aria-label='Reason for Portal Reference Omission']"));

        private IWebElement SetOnHoldRequestStatus => Driver.WaitForElement(By.CssSelector("select[aria-label='Set On Hold Request Status?']"));

        private IWebElement JobActions => Driver.WaitForElement(By.CssSelector("[aria-label='Job Actions']"));

        private By JobActionsNoActionCodeInput => By.CssSelector("section[id*='DialogContainer'] input[aria-label='No Action Code, Lookup']");

        private IWebElement JobActionsInternalRemarks => Driver.WaitForElement(By.CssSelector("section[id*='DialogContainer'] textarea[aria-label='Internal Remarks']"));
        private IWebElement JobActionsSaveandClose => Driver.WaitForElement(By.CssSelector("section[id*='DialogContainer'] button[aria-label='Save & Close']"));

        private IWebElement StatusReasonLabel => Driver.WaitForElement(By.XPath("//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row')]//*[contains(@col-id,'statuscode')]//label"));

        private By MaterialRequestRows => By.XPath("//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row')]");
        private By JobCommands_refreshBtn => By.CssSelector("ul[aria - label = 'Job Commands'] button[aria - label = 'Refresh']");
        private By associatedJobNameRows => By.XPath("//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row')]//div[@col-id='voa_codedreason']//a");
        private IWebElement NewProposeBAReference => Driver.WaitForElement(By.XPath("//span[text()='New Proposed BA Reference Amendment']/ancestor::button"));

        private IWebElement BillingAuthorityValue => Driver.WaitForElement(By.CssSelector("section[aria-label='Summary'] [data-control-name='voa_billingauthorityid'] [role='link'] [data-id*='LookupResultsDropdown_voa_billingauthorityid']"));

        private IWebElement ProposedEffectiveDateValue => Driver.WaitForElement(By.CssSelector("section[aria-label='Details'] [aria-label='Date of Proposed Effective Date']"));

        private IWebElement BAValue => Driver.WaitForElement(By.XPath("//section[@aria-label='BA Reference']//*[@title='Billing Authority']//li//div//div"));

        private IWebElement ProposedBARefValue => Driver.WaitForElement(By.CssSelector("section[aria-label='BA Reference'] [aria-label='Proposed BA Reference Number']"));

        private IWebElement HerediatamentSection => Driver.WaitForElement(By.CssSelector("section[aria-label='Hereditament Details'] h2"));

        private IWebElement HereditamentPostcode => Driver.WaitForElement(By.CssSelector("section[aria-label='Hereditament Details'] [aria-label='Postcode']"));

        private IWebElement ExpProposedBARefValue => Driver.WaitForElement(By.XPath("//input[@aria-label='Proposed BA Reference Number']"));

        private IWebElement ExpPostcode => Driver.WaitForElement(By.CssSelector("section[aria-label='Hereditament Detail'] [aria-label='Name']"));

        private IWebElement ExpProposedEffectiveDateValue => Driver.WaitForElement(By.CssSelector("section[aria-label='Summary'] [aria-label='Date of Effective From Date']"));


        private By SupplementaryJobTypeSearchBtn => By.CssSelector("section[id*='DialogContainer']  [aria-label='Job Type, Lookup']+button[title='Search']");

        private By SupplementaryJobType => By.CssSelector("section[id*='DialogContainer']  [aria-label='Job Type, Lookup']");

        private IWebElement SupplementaryJobSave => Driver.WaitForElement(By.CssSelector("li[id*='voa_supplementaryrecordrequisition'] button[aria-label='Save (CTRL+S)']"));

        private IWebElement SupplementaryJobSubmit => Driver.WaitForElement(By.CssSelector("li[id*='voa_supplementaryrecordrequisition'] button[aria-label='Submit']"));

        private IWebElement SupplementaryJobContinue => Driver.WaitForElement(By.CssSelector("[title='Continue']"));

        private IWebElement SupplementaryJobClose => Driver.WaitForElement(By.CssSelector("button[aria-label='Close']"));

        private IWebElement BAAuthorityValue => Driver.WaitForElement(By.CssSelector("section[aria-label='BA Reference'] [aria-label='Proposed BA Reference Number']"));
        private IWebElement ValidateForAutoProcess => Driver.WaitForElement(By.CssSelector("button[aria-label='Validate For Autoprocess']"));


        private IWebElement AutoProcessValue => Driver.WaitForElement(By.CssSelector("select[aria-label='Validate For Autoprocess']"));

        private IWebElement BAReferenceValue => Driver.WaitForElement(By.XPath("//section[@aria-label='BA Reference']//*[@aria-label='Billing Authority Reference']"));

        private IWebElement BuildingnumberDialog => Driver.WaitForElement(By.XPath("//*[contains(@class,'ms-Dialog-main')]//label[text()='Building Name/Number']//following-sibling::*//input"));

        private IWebElement NewRequestPropertyLinkSearch => Driver.WaitForElement(By.CssSelector("[aria-label='Search records for Select record, Multiple Selection Lookup field']"));
        private IWebElement NewRequestPropertyLink => Driver.WaitForElement(By.XPath("//section[@id='lookupDialogRoot']//span[text()='New Request Property Link']//ancestor::span"));

        private IWebElement NewRequestPropertyLinklookupSearch => Driver.WaitForElement(By.CssSelector("section[data-id='quickCreateRoot'] input[aria-label='Property Link, Lookup']+button[title='Search']"));

        private IWebElement PropertyLinklookup => Driver.WaitForElement(By.CssSelector("section[data-id='quickCreateRoot'] [aria-label='Property Link, Lookup']"));

        private IWebElement PropertyLinkLabel => Driver.WaitForElement(By.CssSelector("section[data-id='quickCreateRoot'] section[aria-label='Details'] [title*='Property Link']"));
        private IWebElement NameLocator => Driver.WaitForElement(By.CssSelector("[aria-label='Name']"));

        private IWebElement NewPropertyLink => Driver.WaitForElement(By.XPath("//span[text()='New Property Link']//ancestor::span"));

        private IWebElement AdvancedLookup => Driver.WaitForElement(By.XPath("//div[@aria-label='Property Link Lookup results']//span[text()='Advanced lookup']//ancestor::span"));

        private IWebElement LeavePage => Driver.WaitForElement(By.CssSelector("[aria-label='Leave this page?']"));

        private By MainParty => By.CssSelector("[aria-label='Main Party, Lookup']");

        private By TaxPayerRequestPropertyLink => By.CssSelector("[aria-label='Tax Payer Request Property Link, Lookup']");

        private By InterestedPartyRequestPropertyLink => By.CssSelector("[aria-label='Interested Party Request Property Link, Lookup']");

        private IWebElement RationaleforCustomersAssertion => Driver.WaitForElement(By.CssSelector("input[aria-label='Rationale for Customers Assertion']"));

        private IWebElement ProposedBand => Driver.WaitForElement(By.CssSelector("[aria-label='Proposed Band']"));
        private By PartyRelationshipRole => By.CssSelector("[aria-label='Party Relationship Role, Lookup']");

        private IWebElement ValidateLegalInterestedPartyToggleTxt => Driver.WaitForElement(By.CssSelector("button[aria-label*='Legally Interested Party']+label"));

        private IWebElement LegallyInterestedPartyToggleBtn => Driver.WaitForElement(By.CssSelector("button[aria-label*='Legally Interested Party']"));

        private IWebElement ValidateIsValidToggleTxt => Driver.WaitForElement(By.CssSelector("button[aria-label*='Is Valid']+label"));

        private IWebElement IsValidToggleBtn => Driver.WaitForElement(By.CssSelector("button[aria-label*='Is Valid']"));

        private IWebElement ValidateIsTaxpayerToggleTxt => Driver.WaitForElement(By.CssSelector("button[aria-label*='Is Taxpayer']+label"));

        private IWebElement IsTaxpayerToggleBtn => Driver.WaitForElement(By.CssSelector("button[aria-label*='Is Taxpayer']"));

        private IWebElement DuprID => Driver.WaitForElement(By.CssSelector("[aria-label='DUPR ID']"));

        private IWebElement DuprIDSearch => Driver.WaitForElement(By.CssSelector("section[id*='voa_propertylinkid'] [id*='SearchBox']"));

        private IWebElement SelectDuprIDSearch => Driver.WaitForElement(By.CssSelector("[aria-label='select or deselect the row']"));

        private IWebElement ClickonDoneDialog => Driver.WaitForElement(By.XPath("//span[text()='Done']"));

        private IWebElement ClickonSaveandCloseDialog => Driver.WaitForElement(By.XPath("//button[text()='Save and Close']//ancestor::button"));

        private IWebElement ClickonSaveanCloseDialog => Driver.WaitForElement(By.XPath("//span[text()='Save & Close']//ancestor::button"));

        private IWebElement DuprIDElement => Driver.WaitForElement(By.XPath("//Label[text()='DUPR ID']"));

        private IWebElement ClickonCancelDialog => Driver.WaitForElement(By.XPath("//span[text()='Cancel']"));

        private IWebElement ProceedWithAccChecks => Driver.WaitForElement(By.CssSelector("[aria-label*='Proceed with Acceptance Checks']"));

        private IWebElement NewEvidenceProvided => Driver.WaitForElement(By.CssSelector("button[aria-label*='New Evidence Provided']"));

        private IWebElement IdentifyComparatorPropButton => Driver.WaitForElement(By.XPath("//span[text()='Identify Comparator Property']//ancestor::button"));

        private By dialogFindHereditament => By.CssSelector("[role='dialog'] [aria-label='Find Hereditament']");
        private IWebElement HouseTypeMatch => Driver.WaitForElement(By.CssSelector("button[aria-label*='House Type Match']"));
        private IWebElement TaxPayerRights => Driver.WaitForElement(By.CssSelector("[title*='Taxpayer May Have Proposal Rights']"));

        private IWebElement PropertySizeMatch => Driver.WaitForElement(By.CssSelector("button[aria-label*='Property Size Match']"));
        private IWebElement AgeMatch => Driver.WaitForElement(By.CssSelector("button[aria-label*='Age Match']"));
        private IWebElement GroupMatch => Driver.WaitForElement(By.CssSelector("button[aria-label*='Group Match']"));

        private IWebElement InLowerBand => Driver.WaitForElement(By.CssSelector("button[aria-label*='In Lower Band']"));

        private IWebElement ComparablesAccepted => Driver.WaitForElement(By.CssSelector("button[aria-label*='Comparables Accepted']"));

        private IWebElement AcceptanceDecision => Driver.WaitForElement(By.CssSelector("[aria-label='Accepted']"));

        private IWebElement OutboundCorressTitle => Driver.WaitForElement(By.CssSelector("[role='link'] [data-id*='voa_statutorycountry']"));

        private IWebElement OCStatusReasondropdown => Driver.WaitForElement(By.CssSelector("[aria-label='Status Reason']"));
        private IWebElement OCStatusReasonSent => Driver.WaitForElement(By.XPath("//*[@role='listbox']//div[text()='Sent']"));
        private IWebElement NoOfPropForReconDel => Driver.WaitForElement(By.CssSelector("input[aria-label='Number of Properties for Reconstitution Deletion']"));
        private IWebElement NoOfPropForReconNew => Driver.WaitForElement(By.CssSelector("input[aria-label='Number of Properties for Reconstitution New']"));
        // button[aria-label='Add Incoming']
        private IWebElement AddIncomingOnRecon => Driver.WaitForElement(By.CssSelector("button[aria-label='Add Incoming']"));
        private IWebElement textEle => Driver.WaitForElement(By.XPath($"//*[@role='option' and text()='Yes']"));
        private IWebElement ProposedBARefOnDialog => Driver.WaitForElement(By.CssSelector("section[aria-label='New Hereditament Details'] input[aria-label='Proposed BA Reference Number']"));
        private IWebElement findaddressdropdown => Driver.WaitForElement(By.CssSelector($"button[aria-label*='Find Address']"));

        private IWebElement BARefOnDialog => Driver.WaitForElement(By.CssSelector("section[aria-label='General'] input[aria-label='BA Reference Number']"));
        private By ProposedBARefOnDialogBy => By.CssSelector("section[aria-label='New Hereditament Details'] input[aria-label='Proposed BA Reference Number']");
        private By findaddressdropdownBy => By.CssSelector($"button[aria-label*='Find Address']");
        private By BARefOnDialogBy => By.CssSelector("section[aria-label='General'] input[aria-label='BA Reference Number']");

        private IWebElement AddOutgoingOnRecon => Driver.WaitForElement(By.CssSelector("button[aria-label='Add Outgoing']"));


        private By SelectOptionFromDropdown(string optionToSelect)
        {
            string customizeSelector = $"[role='listbox'] [role='option']:contains('{optionToSelect}')";
            return By.CssSelector(customizeSelector);
        }

        private By AddressRowsFollowingUPRN(string expUPRN)
        {
            string customizeSelector = $"//*[@data-automationid='DetailsList']//*[@data-automationid='DetailsRow']//*[@data-automation-key='uprn']//*[text()='{expUPRN}']//following::*[@data-automationid='DetailsRow']";
            return By.XPath(customizeSelector);
        }
        private IWebElement UseProposedAddress => Driver.WaitForElement(By.CssSelector("button[title='Use Proposed Address']"));

        private By JObNameOnRows => By.XPath("//*[@class='ag-center-cols-container' and @role='rowgroup']//*[contains(@class,'ag-row')]//*[@col-id='title']//a");
        private IWebElement NoActionFinishBtn => Driver.WaitForElement(By.XPath("//*[text()='Finish']/ancestor::button"));
        private IWebElement NoActioncontinueBtn => Driver.WaitForElement(By.XPath("//*[text()='Continue']/ancestor::button"));
    }
}