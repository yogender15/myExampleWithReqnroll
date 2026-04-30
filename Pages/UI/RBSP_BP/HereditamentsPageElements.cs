using OpenQA.Selenium;
using POMSeleniumFrameworkPoc1.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POMSeleniumFrameworkPoc1.Pages.UI.CommonComponents
{
    public partial class HereditamentsPage : BasePage
    {
        private IWebElement HereditamentsTitle => Driver.WaitForElement(By.XPath("//div[contains(@title,'Hereditaments')]"));
        private IWebElement HereditamentNewBtn => Driver.WaitForElement(By.XPath("//*[contains(@aria-label,'Hereditament Commands')]//button[contains(@aria-label,'New')]"));
        private IWebElement HereditamentLookUpResultFirstValue => Driver.WaitForElement(By.XPath("//*[@data-id='voa_statutoryspatialunitid.fieldControl-voa_name0_0_0']"));
        private By HereditamentLookUpFieldSelector => By.XPath("//*[@data-id='voa_statutoryspatialunitid.fieldControl-LookupResultsDropdown_voa_statutoryspatialunitid_textInputBox_with_filter_new']");
        private IList<IWebElement> ListOfHereditamentsList => Driver.WaitForElements(By.CssSelector("div[aria-label='Press SPACE to select this row.']")).ToList();
        private IWebElement NewbuttonElement => Driver.WaitForElement(By.CssSelector("button[aria-label='New']"));
        private IWebElement BillingauthorityTextElement => Driver.WaitForElement(By.CssSelector("input[aria-label='Billing Authority, Lookup']"));
        private By BillingAuthorityLookupInput => By.CssSelector("input[aria-label='Billing Authority, Lookup']");
        private IWebElement BillingAuthorityLookUpFirstValue => Driver.WaitForElement(By.CssSelector("ul li[aria-label='AB TEST']"));
        private IWebElement HereditamentIDTextBox => Driver.WaitForElement(By.XPath("input[aria-label='Hereditament Reference']"));
        private IWebElement VMSTabElement => Driver.WaitForElement(By.CssSelector("button[aria-label='VMS. Valuation Mapping System']"));
        private IWebElement GeoMapUnderVMSTabUnderHereditamentsElement => Driver.WaitForElement(By.CssSelector("#map_container"), 20000);
        private IWebElement GeoMapUnderAddressSectionUnderHereditamentsElement => Driver.WaitForElement(By.CssSelector("div[data-id='voa_geohereditamenttohereditamentlookupid-FieldSectionItemContainer']"));
        private IWebElement DocumentsTab => Driver.WaitForElement(By.CssSelector("li[title='Documents']"));
        private IWebElement UploadButtonEnabledElement => Driver.WaitForElement(By.CssSelector("button[data-is-focusable='true'] i[data-icon-name='Upload']"));
        private IWebElement RefreshButtonEnabledElement => Driver.WaitForElement(By.CssSelector("button[data-is-focusable='true'] i[data-icon-name='Refresh']"));
        private IWebElement ExternalLinkButtonEnabledElement => Driver.WaitForElement(By.CssSelector("button[data-is-focusable='true'] i[data-icon-name='Link']"));
        private IWebElement VMSWindowIFrame => Driver.WaitForElement(By.CssSelector("iframe#vmsIframe"));
        private By VMSWindowIFrameSelector => By.CssSelector("iframe#vmsIframe");
        private IWebElement DownloadButtonEnabledElement => Driver.WaitForElement(By.CssSelector("button[aria-disabled='true'] i[data-icon-name='Download']"));
        private IWebElement CopyButtonEnabledElement => Driver.WaitForElement(By.CssSelector("button[aria-disabled='true'] i[data-icon-name='Copy']"));
        private IWebElement MoveButtonEnabledElement => Driver.WaitForElement(By.CssSelector("button[aria-disabled='true'] i[data-icon-name='Move']"));

        private By HereditamentIDTextBoxSelector => By.XPath("input[aria-label='Hereditament Reference']");
        public string HereditamentId;

        private IList<IWebElement> ListOfSearchedHereditamentsRadioList => Driver.WaitForElements(By.CssSelector("div[role='radio'] div[class*='Check root']")).ToList();

        private IWebElement NextPageOnDialog => Driver.WaitForElement(By.XPath("//*[contains(@class,'ms-Dialog-main')]//button[@title='Next Page']//i[@data-icon-name='Next']"));


    }
}
