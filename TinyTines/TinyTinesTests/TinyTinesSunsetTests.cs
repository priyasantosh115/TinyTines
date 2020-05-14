using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TinyTines;

namespace TinyTinesTests
{
    [TestClass]
    public class TinyTinesSunsetTests
    {
        TinyTinesSunset tinyTinesSunset = new TinyTinesSunset("../../tiny-tines-sunset.json");

        [TestMethod]
        public void ExecuteDateTimeAgent()
        {
            var executeLocationAgent = tinyTinesSunset.ExecuteLocationAgent();
            Assert.IsNotNull(executeLocationAgent);
        }

        [TestMethod]
        public void ExecuteSunsetAgent()
        {
            var executeLocationAgent = tinyTinesSunset.ExecuteLocationAgent();
            var executeSunsetAgent = tinyTinesSunset.ExecuteSunsetAgent(executeLocationAgent);
            Assert.IsNotNull(executeSunsetAgent);
        }

        [TestMethod]
        public void ExecutePrintAgent()
        {
            var executeLocationAgent = tinyTinesSunset.ExecuteLocationAgent();
            var executeSunsetAgent = tinyTinesSunset.ExecuteSunsetAgent(executeLocationAgent);
            var executePrintAgent = tinyTinesSunset.ExecutePrintAgent(executeSunsetAgent);
            Assert.IsNotNull(executePrintAgent);
        }
    }
}
