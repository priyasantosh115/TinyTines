using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;
using System.Net;
using TinyTines.Model;

namespace TinyTines
{
    public class TinyTimesToday
    {
        public AllAgents _allAgents { get; set; }

        public TinyTimesToday(string url)
        {
            using (StreamReader streamReader = new StreamReader(url))
            {
                string json = streamReader.ReadToEnd();
                _allAgents = JsonConvert.DeserializeObject<AllAgents>(json);
            }
        }

        public DateTimeAgentResponse ExecuteDateTimeAgent()
        {
            var dateTimeAgent = _allAgents.Agents.Where(x => x.name == "datetime").FirstOrDefault();

            WebRequest request = WebRequest.Create(dateTimeAgent.options.Url);
            request.Method = "GET";
            WebResponse response = request.GetResponse();

            using (StreamReader rd = new StreamReader(response.GetResponseStream()))
            {
                var ServiceResult = rd.ReadToEnd();

                dynamic data = JObject.Parse(ServiceResult);

                return new DateTimeAgentResponse()
                {
                    datetime = data.datetime,
                    day_of_year = data.day_of_year
                };
            }
        }

        public string ExecuteDateTimePrintAgent(DateTimeAgentResponse dateTimeAgentResponse)
        {
            var printAgent = _allAgents.Agents.Where(x => x.name == "print_time").FirstOrDefault();
            var constructMessage = printAgent.options.Message
                .Replace("{{ datetime.datetime }}", dateTimeAgentResponse.datetime);

            return constructMessage;
        }

        public string ExecuteFactAgent(string day_of_year)
        {
            var factAgent = _allAgents.Agents.Where(x => x.name == "fact").FirstOrDefault();
            var factAgentUrl = factAgent.options.Url
                .Replace("{{ datetime.day_of_year }}", day_of_year);

            WebRequest request = WebRequest.Create(factAgentUrl);
            request.Method = "GET";
            WebResponse response = request.GetResponse();

            using (StreamReader rd = new StreamReader(response.GetResponseStream()))
            {
                var ServiceResult = rd.ReadToEnd();

                dynamic data = JObject.Parse(ServiceResult);

                return data.text;
            }
        }

        public string ExecutePrintFactAgent(string factText)
        {
            var printAgent = _allAgents.Agents.Where(x => x.name == "print_fact").FirstOrDefault();
            var constructMessage = printAgent.options.Message
                .Replace("{{ fact.text }}", factText);

            return constructMessage;
        }
    }
}
