using POMSeleniumFrameworkPoc1.Helpers;
using System;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using POMSeleniumFrameworkPoc1.APIObjects;
using Newtonsoft.Json;
using RestSharp;

namespace POMSeleniumFrameworkPoc1.Pages
{
    public class SearchJobCallModelPage : ApiBase
    {
        public IRestResponse SearchJobResultsResponse;
       public CookieHeaderObjects GetCookieHeaders()
        {
            CookieHeaderObjects cookieHeaders = new CookieHeaderObjects();
            cookieHeaders.CrmOwinAuth = Config.CrmOwinAuth;
            cookieHeaders.ReqClientId = Config.ReqClientId;
            cookieHeaders.OrgId = Config.OrgId;
            return cookieHeaders;
        }

        public SearchJobRequestObjects GetRequestBody(string term)
        {
            SearchJobRequestObjects searchJobRequestObjects = new SearchJobRequestObjects();
            searchJobRequestObjects.Search = term;
            searchJobRequestObjects.Top = 7;
            searchJobRequestObjects.UseFuzzy = true;
            searchJobRequestObjects.SuggestType = "advanced";
            return searchJobRequestObjects;

        }

        public SearchjobResponseObjects GetSearchJobCallResponse(string term, out IRestResponse searchResponse)
        {
            var headers = new Dictionary<string, string>
            {
                { "Accept-Language", "en-US,en;q=0.9" },
                { "content-type", "application/json" },
                { "Accept", "*/*" }
            };
            var cookieHeaders = GetCookieHeaders();

           SearchJobResultsResponse = ApiPostWithCookieList(Config.BaseApiUrl + Config.SearchJobRelativeUrl, JsonConvert.SerializeObject(GetRequestBody(term)),
                cookieHeaders, headers) ;
            searchResponse = SearchJobResultsResponse;
           return JsonConvert.DeserializeObject<SearchjobResponseObjects>(SearchJobResultsResponse.Content.ToString());
        }

    }
}
