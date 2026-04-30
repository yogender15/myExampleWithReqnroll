using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using POMSeleniumFrameworkPoc1.APIObjects;
using POMSeleniumFrameworkPoc1.Enum;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace POMSeleniumFrameworkPoc1.Helpers
{
    public static class APIExtensions
    {
        public static void SetCookieHeaders(this IRestRequest request, CookieHeaderObjects cookieHeaderObjects)
        {
            if (cookieHeaderObjects.CrmOwinAuth != null)
                request.AddCookie(CookieHeaders.CrmOwinAuth.ToString(), cookieHeaderObjects.CrmOwinAuth);

            if (cookieHeaderObjects.ReqClientId != null)
                request.AddCookie(CookieHeaders.ReqClientId.ToString(), cookieHeaderObjects.ReqClientId);

            if (cookieHeaderObjects.OrgId != null)
                request.AddCookie(CookieHeaders.OrgId.ToString(), cookieHeaderObjects.OrgId);
        }

    }
}
