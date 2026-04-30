using POMSeleniumFrameworkPoc1.APIObjects;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace POMSeleniumFrameworkPoc1.Helpers
{
    public class ApiBase
    {
        public IRestResponse Response;

        public IRestClient GoToApiUrl(string api)
        {
            return new RestClient(Config.BaseUrl + api);
        }

        public IRestResponse ApiPostWithCookie(string url, string cookieName, string cookieValue, string body, Dictionary<string, string> headers = null)
        {
            var client = new RestClient(url);
            IRestRequest request = new RestRequest
            {
                Method = Method.POST
            };

            if (headers != null)
            {
                foreach (var header in headers)
                    request.AddHeader(header.Key, header.Value);
            }

            request.AddCookie(cookieName, cookieValue);
            request.AddJsonBody(body);
            var response = client.Execute(request);
            return response;
        }

        public IRestResponse ApiPostWithCookieList(string url, string body, CookieHeaderObjects cookieHeaders = null, Dictionary<string, string> headers = null)
        {
            var client = new RestClient(url);
            IRestRequest request = new RestRequest
            {
                Method = Method.POST
            };

            if (headers != null)
            {
                foreach (var header in headers)
                    request.AddHeader(header.Key, header.Value);
            }

            if (cookieHeaders != null)
            {
                request.SetCookieHeaders(cookieHeaders);
            }
            request.AddJsonBody(body);
            WebProxy proxy = new WebProxy();
            proxy.UseDefaultCredentials = true;
            client.Proxy = proxy;
            var response = client.Execute(request);
            return response;
        }
        public IRestResponse ApiPutWithCookie(string url, string cookieName, string cookieValue, string body, Dictionary<string, string> headers = null)
        {
            var client = new RestClient(url);
            IRestRequest request = new RestRequest
            {
                Method = Method.PUT
            };
            if (headers != null)
            {
                foreach (var header in headers)
                    request.AddHeader(header.Key, header.Value);
            }

            request.AddCookie(cookieName, cookieValue);
            request.AddJsonBody(body);
            var response = client.Execute(request);
            return response;
        }

        public IRestResponse GetApiCall(string api, Dictionary<string, string> headers = null)
        {
            var client = new RestClient(api);
            var wait = client.ReadWriteTimeout > 0;

            var request = new RestRequest(Method.GET);

            if (headers != null)
            {
                foreach (var header in headers)
                    request.AddHeader(header.Key, header.Value);
            }

            Response = client.Execute(request);
            return Response;
        }

        public IRestResponse GetApiCallByCookie(string api, Dictionary<string, string> cookies = null)
        {
            var client = new RestClient(api);
            var wait = client.ReadWriteTimeout > 0;

            var request = new RestRequest(Method.GET);

            if (cookies != null)
            {
                foreach (var cookie in cookies)
                    request.AddCookie(cookie.Key, cookie.Value);
            }

            Response = client.Execute(request);
            return Response;
        }

        public IRestResponse ApiPostCall(string api, string body, Dictionary<string, string> headers = null)
        {
            var client = GoToApiUrl(api);
            IRestRequest request = new RestRequest
            {
                Method = Method.POST
            };

            if (headers != null)
            {
                foreach (var header in headers)
                    request.AddHeader(header.Key, header.Value);
            }

            request.AddJsonBody(body);
            var response = client.Execute(request);
            return response;
        }
    }
}