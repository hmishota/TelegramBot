using BecomeSolid.Day1.MyRefactoring.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BecomeSolid.Day1.MyRefactoring
{
    public class WeatherService
    {
        private string _weatherApiKey = "ec259b32688dc1c1d087ebc30cbff9ed";
        private string _urlSite = "http://api.openweathermap.org/data/2.5/weather?q={0}&APPID={1}&units=metric&lang=ru";

        public Weather Get(string city)
        {
            var responseJson = SentRequest(city);
            return Parse(responseJson);
        }

        private string SentRequest(string city)
        {
            string url = string.Format(_urlSite, city, _weatherApiKey);

            WebRequest request = WebRequest.Create(url);
            WebResponse response;
            try
            {
                response = request.GetResponse();
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    return streamReader.ReadToEnd();
                }
            }
            catch (WebException e)
            {
                throw new WeatherException();
            }
        }

        private Weather Parse(string jsonString)
        {
            JObject jsonObject = JObject.Parse(jsonString);
            JObject main = (JObject)jsonObject["main"];
            double temp = (double)main["temp"];
            JObject weather = (JObject)jsonObject["weather"][0];
            string description = (string)weather["description"];
            string cityName = (string)jsonObject["name"];

            return new Weather() { City = cityName, Description = description, Temperature = temp };
        }
    }
}
