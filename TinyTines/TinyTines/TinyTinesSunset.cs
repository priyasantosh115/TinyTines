//using Model.TinyTines;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;
using System.Net;
using TinyTines.Model;

namespace TinyTines
{
    public class TinyTinesSunset
    {
        public AllAgents _allAgents { get; set; }

        public TinyTinesSunset(string url)
        {
            using (StreamReader streamReader = new StreamReader(url))
            {
                string json = streamReader.ReadToEnd();
                _allAgents = JsonConvert.DeserializeObject<AllAgents>(json);
            }
        }

        public LocationAgentResponse ExecuteLocationAgent()
        {
            var locationAgent = _allAgents.Agents.Where(x => x.name == "location").FirstOrDefault();

            WebRequest request = WebRequest.Create(locationAgent.options.Url);
            request.Method = "GET";
            WebResponse response = request.GetResponse();

            using (StreamReader rd = new StreamReader(response.GetResponseStream()))
            {
                var ServiceResult = rd.ReadToEnd();

                dynamic data = JObject.Parse(ServiceResult);

                return new LocationAgentResponse() { Latitude = data.longitude, 
                    Longitude = data.latitude,
                    City = data.city,
                    Country = data.country
                };
            }
        }

        public SunsetAgentResponse ExecuteSunsetAgent(LocationAgentResponse locationData)
        {
            var sunsetAgent = _allAgents.Agents.Where(x => x.name == "sunset").FirstOrDefault();
            var constructSunsetAgentUrl = sunsetAgent.options.Url
                .Replace("{{ location.latitude }}", locationData.Latitude)
                .Replace("{{ location.longitude }}", locationData.Longitude);

            WebRequest request = WebRequest.Create(constructSunsetAgentUrl);
            request.Method = "GET";
            WebResponse response = request.GetResponse();

            using (StreamReader rd = new StreamReader(response.GetResponseStream()))
            {
                var ServiceResult = rd.ReadToEnd();

                dynamic data = JObject.Parse(ServiceResult);

                return new SunsetAgentResponse() { 
                    City = locationData.City, 
                    Country = locationData.Country, 
                    Sunset = data.results.sunset 
                };
            }
        }

        public string ExecutePrintAgent(SunsetAgentResponse sunsetAgentResponse)
        {
            var printAgent = _allAgents.Agents.Where(x => x.name == "print").FirstOrDefault();
            var constructPrintAgentMessage = printAgent.options.Message
                .Replace("{{ location.city }}", sunsetAgentResponse.City)
                .Replace("{{ location.country }}", sunsetAgentResponse.Country)
                .Replace("{{ sunset.results.sunset }}", sunsetAgentResponse.Sunset);

            return constructPrintAgentMessage;
        }
    }
}
