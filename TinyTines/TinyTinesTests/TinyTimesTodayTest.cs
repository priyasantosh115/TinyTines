using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TinyTines;

namespace TinyTinesTests
{
    [TestClass]
    public class TinyTimesTodayTest
    {
        TinyTimesToday tinyTinesToday = new TinyTimesToday("../../tiny-tines-today.json");
        
        [TestMethod]
        public void ExecuteDateTimeAgent()
        {
            var dateTimeAgent = tinyTinesToday.ExecuteDateTimeAgent();
            Assert.IsNotNull(dateTimeAgent);
        }

        [TestMethod]
        public void ExecuteDateTimePrintAgent()
        {
            var dateTimeAgent = tinyTinesToday.ExecuteDateTimeAgent();
            var executeDateTimePrintAgent = tinyTinesToday.ExecuteDateTimePrintAgent(dateTimeAgent);
            Assert.IsNotNull(executeDateTimePrintAgent);
        }

        [TestMethod]
        public void ExecuteFactAgent()
        {
            var dateTimeAgent = tinyTinesToday.ExecuteDateTimeAgent();
            var executeFactAgent = tinyTinesToday.ExecuteFactAgent(dateTimeAgent.day_of_year);
            Assert.IsNotNull(executeFactAgent);
        }

        [TestMethod]
        public void ExecutePrintFactAgent()
        {
            var dateTimeAgent = tinyTinesToday.ExecuteDateTimeAgent();
            var executeFactAgent = tinyTinesToday.ExecuteFactAgent(dateTimeAgent.day_of_year);
            var executePrintFactAgent = tinyTinesToday.ExecutePrintFactAgent(executeFactAgent);
            Assert.IsNotNull(executePrintFactAgent);
        }
    }
}
