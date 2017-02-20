using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BecomeSolid.Day1.MyRefactoring.Model;

namespace BecomeSolid.Day1.MyRefactoring.Services
{
    public class QuoteService
    {
        private string _urlSite = "http://api.forismatic.com/api/1.0/";

        public Quote Get()
        {
            return SendRequest();
        }

        private Quote SendRequest()
        {
            var client = new RestClient(_urlSite);
            var request = new RestRequest(Method.POST);
            request.AddParameter("method", "getQuote");
            request.AddParameter("format", "json");
            IRestResponse response = client.Execute(request);
            return client.Execute<Quote>(request).Data; 
        }
    }
}
