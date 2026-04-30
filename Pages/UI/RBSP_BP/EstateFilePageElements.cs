
using OpenQA.Selenium;
using POMSeleniumFrameworkPoc1.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POMSeleniumFrameworkPoc1.Pages.UI
{
    public partial class EstateFilePage : BasePage
    {
        private IWebElement EstateFileMenu => Driver.WaitForElement(By.CssSelector("ul[aria-label='Navigate Dynamics 365'] li[aria-label='Estate Files']"));
        private IWebElement NewCommandMenu => Driver.WaitForElement(By.CssSelector("ul[data-id='CommandBar'] button[aria-label='New']"));
        private By BillingAuthorityLookUpInputFieldSelector => By.CssSelector("input[aria-label='Billing Authority, Lookup']");
        private By DeveloperLookUpInputFieldSelector => By.CssSelector("input[aria-label='Developer, Lookup");
        private IWebElement PlanningReferenceNumberInputField => Driver.WaitForElement(By.CssSelector("section[aria-label='General'] input[aria-label='Planning Reference Number']"));
        private IWebElement DeveloperNameInputField => Driver.WaitForElement(By.CssSelector("section[aria-label='General'] input[aria-label='Development Name']"));
        private IWebElement EstateActionsTab => Driver.WaitForElement(By.CssSelector("ul[aria-label='Estate File Form'] li[aria-label='Estate Actions']"));
        private IWebElement NewEstateActionplusBtn => Driver.WaitForElement(By.CssSelector("ul[data-id='CommandBar'] button[aria-label='New Estate Action. Add New Estate Action']"));
        private By EstateActionTypeLookUpInputFieldSelector => By.CssSelector("input[aria-label='Estate Action Type, Lookup']");
        private IWebElement HouseTypeTab => Driver.WaitForElement(By.CssSelector("ul[aria-label='Estate File Form'] li[aria-label='House Types']"));
        private IWebElement NewHouseTypePlusBtn => Driver.WaitForElement(By.CssSelector("section[aria-label='New Section'] button[aria-label='New House Type. Add New House Type']"));

        //****************************Ashish's Code*******************************************
        private By BillingAuthorityLookUp => By.CssSelector("section[aria-label= 'General'] input[aria-label='Billing Authority, Lookup']");
        private IWebElement DevelopmentName => Driver.WaitForElement(By.CssSelector("section[aria-label= 'General'] input[aria-label='Development Name']"));
        private IWebElement PlanningRefNum => Driver.WaitForElement(By.CssSelector("section[aria-label= 'General'] input[aria-label='Planning Reference Number']"));
        private By DeveloperLookUp => By.CssSelector("section[aria-label= 'General'] input[aria-label='Developer, Lookup']");
        private IWebElement CreatedOnDate => Driver.WaitForElement(By.CssSelector("input[aria-label='Date of Created On']"));
        private IWebElement CreatedOnTime => Driver.WaitForElement(By.CssSelector("input[aria-label='Time of Created On']"));

        //Estate Action Tab
        private IWebElement NewEstateActionButton => Driver.WaitForElement(By.XPath("//ul[@aria-label='Estate Action Commands']//li//*[text()='New Estate Action']"));
        private IWebElement EstateActionTypeCreated => Driver.WaitForElement(By.CssSelector("div[col-id='voa_estateactiontypeid'] a span"));

        // Inspections Tab
        //  "[class*='ag-row-position-absolute'] div[col-id='voa_reasonforinspectioncode'] label"
        private IWebElement InspectionRaised => Driver.WaitForElement(By.CssSelector("[class*='ag-row-position-absolute'] div[col-id ='voa_reasonforinspectioncode'] label"));

        private IWebElement InspectionReasonText => Driver.WaitForElement(By.CssSelector("section[aria-label='General'] select[aria-label='Reason for Inspection']"));

        private IWebElement InspectionReasonComment => Driver.WaitForElement(By.CssSelector("section[aria-label='General'] textarea[aria-label='Reason for Inspection Comments']"));

        //Estate Action Pop-up

        private By EstateActionTypeLookUp => By.CssSelector("input[aria-label='Estate Action Type, Lookup']");
        private IWebElement NumberOfPlotsInputField => Driver.WaitForElement(By.CssSelector("input[aria-label='Number of Plots']"));
        private IWebElement PlotStartingNumberInputField => Driver.WaitForElement(By.CssSelector("input[aria-label='Plot Starting Number']"));
        private IWebElement SubmitToggleBtn => Driver.WaitForElement(By.CssSelector("input[aria-label*='Submit']"));
        private IWebElement SubmitToggleTxt => Driver.WaitForElement(By.XPath("//input[contains(@aria-label,'Submit')]//following-sibling::label"));
        private IWebElement EASaveandCloseBtn => Driver.WaitForElement(By.CssSelector("button[aria-label='Save and Close']"));

        private IWebElement ReasonForInspectionSelect => Driver.WaitForElement(By.CssSelector("section[data-id='quickCreateRoot'] select[aria-label='Reason for Inspection']"));
        private IWebElement ReasonForInspectionInput => Driver.WaitForElement(By.CssSelector("section[data-id='quickCreateRoot'] textarea[aria-label='Reason for Inspection Comments']"));
        private IWebElement InspectionAllocationSelect => Driver.WaitForElement(By.CssSelector("section[data-id='quickCreateRoot'] select[aria-label='Inspection Allocation']"));

        private IWebElement DuplicatePlotErrorMSgDialog => Driver.WaitForElement(By.CssSelector("[data-id='errorDialogdialog'] h2[data-id='errorDialog_subtitle']"));

        private IWebElement DuplicatePlotErrorDialogOKBtn => Driver.WaitForElement(By.CssSelector("[data-id='errorDialogdialog'] button[aria-label='OK']"));
        private IWebElement ReasonForBlocking => Driver.WaitForElement(By.CssSelector("section[data-id='quickCreateRoot'] textarea[aria-label='Reason for Blocking']"));

        //House Type Tab
        private IWebElement NewHouseTypeButton => Driver.WaitForElement(By.XPath("//ul[@aria-label='House Type Commands']//li//*[text()='New House Type']"));
        private IWebElement HouseNameCreated => Driver.WaitForElement(By.CssSelector("[aria-label='House Types'] [role='rowgroup'] [class*='ag-row-position-absolute'] [col-id='voa_name'] a"));

        public By houseNameBy = By.CssSelector("[aria-label='House Types'] [role='rowgroup'] [class*='ag-row-position-absolute'] [col-id='voa_name'] a");
        //Estate File Review Tab
        private IWebElement ReviewApproved => Driver.WaitForElement(By.CssSelector("[aria-label='Review Approved?']"));
        private IWebElement ReviewApprovedReason => Driver.WaitForElement(By.CssSelector("textarea[aria-label='Review Outcome Reason']"));

        //House Type Pop-up
        private By GroupLookUpInputFieldSelector => By.CssSelector("input[aria-label='Group, Lookup']");
        private By TypeLookUpInputFieldSelector => By.CssSelector("input[aria-label='Type, Lookup']");
        private By ListLookUpInputFieldSelector => By.CssSelector("input[aria-label='List, Lookup']");
        private IWebElement NameInputField => Driver.WaitForElement(By.CssSelector("input[aria-label='Name']"));
        private IWebElement HTSaveAndClose => Driver.WaitForElement(By.CssSelector("button[aria-label='Save and Close']"));

        //PAD Code Details Section
        private IWebElement Area => Driver.WaitForElement(By.CssSelector("section[aria-label= 'PAD Code Details'] input[aria-label= 'Area of Dwelling (m2)']"));
        private IWebElement Rooms => Driver.WaitForElement(By.CssSelector("section[aria-label= 'PAD Code Details'] input[aria-label= 'Rooms']"));
        private IWebElement Bedrooms => Driver.WaitForElement(By.CssSelector("section[aria-label= 'PAD Code Details'] input[aria-label= 'Bedrooms']"));
        private IWebElement Baths => Driver.WaitForElement(By.CssSelector("section[aria-label= 'PAD Code Details'] input[aria-label= 'Bathrooms']"));
        private IWebElement Floors => Driver.WaitForElement(By.CssSelector("section[aria-label= 'PAD Code Details'] input[aria-label= 'Floors']"));
        private By ParkingLookUp => By.CssSelector("section[aria-label= 'PAD Code Details'] input[aria-label= 'Parking, Lookup']");

        private IWebElement Heating => Driver.WaitForElement(By.CssSelector("section[aria-label='PAD Code Details'] ul[title='Heating'] li div[id*='voa_heatingcodeid.fieldControl-LookupResultsDropdown'][role='link']"));
        private IWebElement RoomsErrorMessage => Driver.WaitForElement(By.CssSelector("section[aria-label='PAD Code Details'] [data-id='voa_rooms'] [data-id='voa_rooms-error-message']"));

        private IWebElement ConservatoryType => Driver.WaitForElement(By.CssSelector("section[aria-label='PAD Code Details'] ul[title='Conservatory Type'] li div[id*='voa_conservatorytypeid.fieldControl-LookupResultsDropdown'][role='link']"));
        private IWebElement ConservatoryArea => Driver.WaitForElement(By.CssSelector("section[aria-label='PAD Code Details'] input[aria-label='Conservatory Area (m2)']"));

        // New Comparable Property Sets
        private IWebElement CTBTError => Driver.WaitForElement(By.XPath("[role='dialog'] [class*='swal-icon--error']"));

        private IWebElement VMSHeader => Driver.WaitForElement(By.XPath("//*[@aria-label='Header Controller']//*[contains(@class,'titles')]//*[contains(@class,'jimu-title')]"));
        private IWebElement NewCTBTButton => Driver.WaitForElement(By.CssSelector("section[aria-label='Comparable Property Sets'] ul[aria-label='Comparable Property Set Commands'] li button[aria-label*='New Comparable Property Set']"));
        private IWebElement VMSWindowIFrame => Driver.WaitForElement(By.CssSelector("iframe#vmsIframe"));

        private IWebElement VmsTitle => Driver.WaitForElement(By.XPath("//div[@class='header-section']//*[contains(text(),'VMS - Valuation Mapping System')]"));
        private IWebElement HMRCSignIn => Driver.WaitForElement(By.XPath("//Section[@class='enterprise-info']//*[contains(text(),'HMRC Sign In')]"));

        private By HMRCSignInBy = By.XPath("//Section[@class='enterprise-info']//*[contains(text(),'HMRC Sign In')]");
        private By VMSWindowIFrameSelectorBy => By.CssSelector("iframe#vmsIframe");

        // "[role='dialog'] [class='swal-text']"
        // Value Significant Code
        private IWebElement NewValueSignificantCodeLink => Driver.WaitForElement(By.CssSelector("section[aria-label='Value Significant Codes'] ul[aria-label='Value Significant Code Link Commands'] li button[aria-label*='New Value Significant Code Link']"));
        private IWebElement ValueSignificantCodeSection => Driver.WaitForElement(By.CssSelector("section[aria-label='Value Significant Codes']"));


        // Value Significant code pop-up
        private By ValueSignificantCode => By.CssSelector("section[data-id='quickCreateRoot'] input[aria-label='Value Significant Code, Lookup']");
        private By ValueSignificantCodeSearchBtn => By.CssSelector("section[data-id='quickCreateRoot'] input[aria-label='Value Significant Code, Lookup']+button[title='Search']");
        private IWebElement VSCDescription => Driver.WaitForElement(By.CssSelector("section[data-id='quickCreateRoot'] textarea[aria-label='VSC Description']"));
        private IWebElement VSCSaveAndClose => Driver.WaitForElement(By.CssSelector("section[data-id='quickCreateRoot'] button[aria-label='Save and Close']"));

        private IWebElement SourceCodesSection => Driver.WaitForElement(By.CssSelector("section[aria-label= 'Source Codes']"));

        //PAD Validation Section
        private IWebElement PADValidationSection => Driver.WaitForElement(By.CssSelector("section[aria-label= 'PAD Validation']"));
        private IWebElement ValidatePADToggleBtn => Driver.WaitForElement(By.CssSelector("section[aria-label= 'PAD Validation'] input[aria-label*='Validate PAD Code']"));
        private IWebElement ValidatePADToggleTxt => Driver.WaitForElement(By.XPath("//*[@aria-label='PAD Validation']//input[contains(@aria-label,'Validate PAD Code')]//following-sibling::label"));

        private IWebElement headerTextValidate(string headersection)
        {
            string customizeSelector = $"//*[text()='{headersection}']/preceding-sibling::*";
            return Driver.WaitForElement(By.XPath(customizeSelector));
        }

        // Banding Decision Section
        private By ProposedTaxBand => By.CssSelector("section[aria-label= 'Banding Decision'] input[aria-label= 'Proposed Tax Band, Lookup']");
        private By ProposedTaxBandSearchBtn => By.CssSelector("section[aria-label= 'Banding Decision'] button[aria-label='Search records for Proposed Tax Band, Lookup field']");


        private By AssessmentAction => By.CssSelector("section[aria-label= 'Banding Decision'] input[aria-label= 'Assessment Action, Lookup']");
        private IWebElement IsBandingComplete => Driver.WaitForElement(By.CssSelector("[aria-label= 'Banding Decision'] button[aria-label='Is Banding Complete?']"));

        private IList<IWebElement> SubjectAttributesList => Driver.WaitForElements(By.CssSelector("section[aria-label ='Subject Attributes'] [data-id ='voa_housetypeestateid.fieldControl-pcf-container-id'] label"));

        // Approval Section
        private IWebElement HouseTypeApproved => Driver.WaitForElement(By.CssSelector("[aria-label= 'Approval'] button[aria-label='House Type Approved?']"));
        private IWebElement OutcomeReason => Driver.WaitForElement(By.CssSelector("[aria-label= 'Approval'] textarea[aria-label='Outcome Reason']"));

        private By AprrovalNotificationHistoryRows => By.XPath("//*[@aria-label='Approval History']//*[contains(@class,'ag-center-cols-container') and @role='rowgroup']//*[contains(@class,'ag-row-position-absolute')]");

        private By rejectedNotificationHistoryRows => By.XPath("//*[@aria-label='All House Type Rejected Approvals']//*[contains(@class,'ag-center-cols-container') and @role='rowgroup']//*[contains(@class,'ag-row-position-absolute')]");

        private By AHCreatedOnColumnVal => By.XPath("//*[contains(@class,'ag-cell-value') and @col-id='createdon']//label/div[text()]");

        private IWebElement LatestSubjectCell(String LatestDate)
        {
            string xpathSelector = $"//*[contains(@class,'ag-cell-value') and @col-id='createdon']//label/div[text()='{LatestDate}']/ancestor::*[contains(@class,'ag-row') and @role='row']//*[@col-id='voa_name']//a";
            return Driver.WaitForElement(By.XPath(xpathSelector));
        }
        private IWebElement ApprovalHistoryNotificationText => Driver.WaitForElement(By.CssSelector("[aria-label ='Approval History'] [class*='ag-row'] [col-id='subject'] a"));
        private IWebElement MoreCommandsAHN => Driver.WaitForElement(By.CssSelector("ul[aria-label='Approval History Notification Commands'] li button[aria-label='More commands for Approval History Notification']"));

        private IWebElement ApprovalHistoryText(String houseTypeName)
        {
            string customizeSelector = $"[aria-label='Approval History'] [class*='ag-row'] [col-id='voa_name'] a[aria-label='House Type {houseTypeName} Approval required']";
            return Driver.WaitForElement(By.CssSelector(customizeSelector));
        }
        private IWebElement RejectedrecordText(String houseTypeName)
        {
            string customizeSelector = $"[aria-label='All House Type Rejected Approvals'] [class*='ag-row'] [col-id='voa_name'] a[aria-label='{houseTypeName}']";
            return Driver.WaitForElement(By.CssSelector(customizeSelector));
        }





        private IWebElement ApprovalHistoryMoreCommands(string optionVal)
        {
            string customizeSelector = $"ul[data-id='OverflowFlyout'] li button[aria-label='{optionVal}']";
            return Driver.WaitForElement(By.CssSelector(customizeSelector));
        }
        //Plot Manager
        private IList<IWebElement> PlotAuditHistoryColumns => Driver.WaitForElements(By.CssSelector("[class='plotHistoryDialogBody'] [data-automationid='DetailsList'] [data-automationid='DetailsHeader'] [data-automationid='ColumnsHeaderColumn'] [class*='cellName']"));

        private IList<IWebElement> AuditDetailsColumnValue => Driver.WaitForElements(By.CssSelector("[class='plotHistoryDialogBody'] [data-automationid='DetailsList'] [data-automationid='DetailsRow'] [data-automationid='DetailsRowFields'] [data-automation-key='auditDetailsColumn'] p"));

        private By PlotsAvailable => By.CssSelector("[class='ms-List'] [class='ms-List-cell'] [data-automationid='DetailsRow']");

        private IWebElement FirstRowPlots => Driver.WaitForElement(By.XPath("//*[@class='ms-List']//*[@class='ms-List-cell']//*[@data-automationid='DetailsRow' and @data-item-index='0']"));

        private By PlotRowCheck => By.CssSelector("[data-automationid='DetailsRowCheck']");

        private By PlotStatusCol => By.CssSelector("[data-automationid='DetailsRowFields'] [data-automation-key='plotStatusColumn'] input");

        private By PlotSizeInput => By.CssSelector("[data-automationid='DetailsRowFields'] [data-automation-key='PlotSizeColumn'] input");
        //"[data-automationid='DetailsRowFields'] [data-automation-key='PlotSizeColumn'] [class*='arrowButtonsContainer'] button[class*='ms-UpButton']"
        private By FloorLevelInput => By.CssSelector("[data-automationid='DetailsRowFields'] [data-automation-key='floorLevelColumn'] input");
        private By PMBillingAuthorityColumnVal => By.CssSelector("[data-automationid='DetailsRowFields'] [data-automation-key='billingAuthorityColumn']");

        private By PlotSizeUpButton => By.CssSelector("[data-automationid='DetailsRowFields'] [data-automation-key='PlotSizeColumn'] [class*='arrowButtonsContainer'] button[class*='ms-UpButton']");

        private By PlotHouseType => By.XPath("//*[@data-automationid='DetailsRowFields']//*[@data-automation-key='houseTypeColumn']//div[@class='ms-HoverCard-host']//div");
        private By PlotAddressLabel => By.XPath("//*[@data-automationid='DetailsRowFields']//*[@data-automation-key='addressLabelColumn']//div[@class='ms-HoverCard-host']//div");

        private IWebElement AddressLabelSearchBox => Driver.WaitForElement(By.CssSelector("[class*='ms-SearchBox'] input[Placeholder*='Search for address']"));
        private By AddressLabelList => By.XPath("//*[@class='addressLabels']//*[@class='addressLabelCardLines']//*");
        private IWebElement AddresLabelErrorMsg => Driver.WaitForElement(By.XPath("//*[contains(@class,'ms-MessageBar-text')]//*[contains(@class,'ms-MessageBar-innerText')]//*"));

        private By AddressLabelCloseIcon => By.XPath("//*[@data-automationid='DetailsRowFields']//*[@data-automation-key='addressLabelColumn']//button//i");

        private IWebElement BillingAuthorityPlotManager => Driver.WaitForElement(By.CssSelector("[class='houseTypeBillingAuthorityColumn'] [class='billingAuthorityList'] [class='billingAuthorityCard'] [class='billingAuthorityCardBody']"));

        private IWebElement PlotManagerShowColumns => Driver.WaitForElement(By.CssSelector("button[role='menuitem'] i[data-icon-name='ChevronDown']"));

        private By PlotManagerShowColumnsOptions => By.CssSelector("ul[class*='ms-ContextualMenu'] li button");


        private IWebElement PlotSave => Driver.WaitForElement(By.XPath("//*[contains(@class,'ms-OverflowSet')]//button//i[@data-icon-name='Save']//following-sibling::*"));
        private IWebElement AddressLabelCardToDragAndDrop(String addressCardText)
        {
            string customizeSelector = $"//*[@class='addressLabels']//*[@class='addressLabelCard']//*[text()='{addressCardText}']";
            return Driver.WaitForElement(By.XPath(customizeSelector));
        }
        private IWebElement HouseTypeToDragAndDrop(String houseTypeName)
        {
            string customizeSelector = $"//*[@class='houseTypeList']//*[@class='houseTypeCardHeader']//*[text()='{houseTypeName}']//ancestor::*[@class='houseTypeCard']//*[@class='houseTypeCardPADS']";
            return Driver.WaitForElement(By.XPath(customizeSelector));
        }
        //Estate File Job Type Details Page
        private IWebElement EstateFileSearchButton => Driver.WaitForElement(By.CssSelector("section[aria-label='Hereditament Details'] button[aria-label='Search records for Estate File, Lookup field']"));
        private IWebElement NewEstateFileButton => Driver.WaitForElement(By.CssSelector("[id*='voa_estatefileid.fieldControl'] ul button[aria-label='New Estate File']"));
        private IWebElement ExternalSLATitle => Driver.WaitForElement(By.CssSelector("section[aria-label='External SLA'] h2[title='External SLA']"));
        private IWebElement HereditamentDetailsTitle => Driver.WaitForElement(By.CssSelector("section[aria-label='Hereditament Details'] h2[title='Hereditament Details']"));
        private By HDEstateFileLookup => By.CssSelector("section[aria-label= 'Hereditament Details'] input[aria-label= 'Estate File, Lookup']");

        private IWebElement HDEstateFileNameField => Driver.WaitForElement(By.CssSelector("section[aria-label='Hereditament Details'] input[aria-label='Name']"));

        //  "section[aria-label='External SLA'] h2[title='External SLA']"
        private By ClosureDateCloseJourney => By.CssSelector("[id*='MscrmControls.Containers.ProcessStageControl-processHeaderStageFlyoutContainer'] input[aria-label='Date of Closure Date']");

        //Estate file Popup
        private By EFBillingAuthorityLookUp => By.CssSelector("section[data-id= 'quickCreateRoot'] input[aria-label='Billing Authority, Lookup']");
        private IWebElement EFDevelopmentName => Driver.WaitForElement(By.CssSelector("section[data-id= 'quickCreateRoot'] input[aria-label='Development Name']"));
        private IWebElement EFPlanningRefNum => Driver.WaitForElement(By.CssSelector("section[data-id= 'quickCreateRoot'] input[aria-label='Planning Reference Number']"));
        private By EFDeveloperLookUp => By.CssSelector("section[data-id= 'quickCreateRoot'] input[aria-label='Developer, Lookup']");

        private IWebElement EFSaveAndClose => Driver.WaitForElement(By.CssSelector("[data-id= 'quickCreateRoot'] button[aria-label='Save and Close']"));

        // Banding BPF Elements
        private IWebElement BandingCompleteBPF => Driver.WaitForElement(By.CssSelector("[id*='MscrmControls.Containers.ProcessStageControl-processHeaderStageFlyoutContainer'] select[aria-label='Banding Complete?'] option[data-selected='true']"));
        private IWebElement ResetPADValidationBPF => Driver.WaitForElement(By.CssSelector("[id*='MscrmControls.Containers.ProcessStageControl-processHeaderStageFlyoutContainer'] select[aria-label='Reset PAD Validation'] option[data-selected='true']"));

        private IWebElement AddressSearchInputOnDialog => Driver.WaitForElement(By.CssSelector("[class*='ms-Modal-scrollableConten'] input[name='Address Search Input']"));
        private IWebElement BtnSearchOnDialog => Driver.WaitForElement(By.CssSelector("[class*='ms-Modal-scrollableConten'] button[aria-label='Click to search for address']"));

        private By AddressRow => By.XPath("//*[@data-automationid='DetailsList']//*[@data-automationid='DetailsRow']");

        private IWebElement BtnUseAddressOnDialog => Driver.WaitForElement(By.XPath("//button//*[text()='Use Address']"));

        private By DuplicateAddressHeader => By.XPath("//*[contains(@class,'ms-Panel-header')]//*[@role='heading' and contains(text(),'Duplicate Addresses Identified')]");
        private IWebElement DuplicateAddressCloseBtn => Driver.WaitForElement(By.CssSelector("[class*='ms-Panel-navigation'] button[title='Close']"));
        private IWebElement NextPageOnDialog => Driver.WaitForElement(By.XPath("//*[contains(@class,'ms-Dialog-main')]//button[@title='Next Page']//i[@data-icon-name='Next']"));

    }
}
