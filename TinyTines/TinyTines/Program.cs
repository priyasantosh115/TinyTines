using System;

namespace TinyTines
{
    class Program
    {
        static void Main(string[] args)
        {
            switch (args[0])
            {
                case "tiny-tines-sunset":
                    TinyTinesSunset tinyTinesSunset = new TinyTinesSunset("../../tiny-tines-sunset.json");
                    var locationAgent = tinyTinesSunset.ExecuteLocationAgent();
                    var sunsetAgent = tinyTinesSunset.ExecuteSunsetAgent(locationAgent);
                    var printAgentResponse = tinyTinesSunset.ExecutePrintAgent(sunsetAgent);
                    Console.WriteLine(printAgentResponse);
                    break;

                case "tiny-tines-today":
                    TinyTimesToday tinyTimesToday = new TinyTimesToday("../../tiny-tines-today.json");
                    var dateTimeAgentResponse = tinyTimesToday.ExecuteDateTimeAgent();
                    var executeDateTimePrintAgent = tinyTimesToday.ExecuteDateTimePrintAgent(dateTimeAgentResponse);
                    
                    var executeFactAgent = tinyTimesToday.ExecuteFactAgent(dateTimeAgentResponse.day_of_year);
                    var executePrintFactAgent = tinyTimesToday.ExecutePrintFactAgent(executeFactAgent);

                    Console.WriteLine(executeDateTimePrintAgent);
                    Console.WriteLine(executePrintFactAgent);
                    break;
                
                default:
                    break;
            }

        }
    }
}
