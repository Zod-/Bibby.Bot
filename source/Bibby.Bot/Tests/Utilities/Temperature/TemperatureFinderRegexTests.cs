using System;
using System.Linq;
using Bibby.Bot.Utilities.Temperature;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bibby.Bot.Tests.Utilities.Temperature
{
    [TestClass]
    public class TemperatureFinderRegexTests
    {
        [TestMethod]
        [DataRow(" 30°C", "30°C")]
        [DataRow("30°C ", "30°C")]
        [DataRow(" 30°C ", "30°C")]
        [DataRow("30C", "30C")]
        [DataRow("30.0°C", "30.0°C")]
        [DataRow("413,0°C", "413,0°C")]
        [DataRow("-30,5°C", "-30,5°C")]
        [DataRow("Random text 30°C around temperature.", "30°C")]
        [DataRow("30,5°C Random text around temperature.", "30,5°C")]
        [DataRow("Random text around temperature. 30.5°C", "30.5°C")]
        public void GetTemperatureCelsiusRegexMentionsTest(string input, string expected)
        {
            var actual = TemperatureFinder.GetTemperatureRegexMatches(input);
            Assert.AreEqual(expected, actual.First());
        }

        [TestMethod]
        [DataRow("30°F", "30°F")]
        [DataRow("30F", "30F")]
        [DataRow(" 30F ", "30F")]
        [DataRow("30.0°F", "30.0°F")]
        [DataRow("500,0°F", "500,0°F")]
        [DataRow("-30,5°F", "-30,5°F")]
        [DataRow("Random text 30°F around temperature.", "30°F")]
        [DataRow("30,5°F Random text around temperature.", "30,5°F")]
        [DataRow("Random text around temperature. 30.5°F", "30.5°F")]
        public void GetTemperatureFahrenheitRegexMentionsTest(string input, string expected)
        {
            var actual = TemperatureFinder.GetTemperatureRegexMatches(input);
            Assert.AreEqual(expected, actual.First());
        }

        [TestMethod]
        [DataRow("30°K", "30°K")]
        [DataRow("30K", "30K")]
        [DataRow("30.0°K", "30.0°K")]
        [DataRow(" 30.0°K ", "30.0°K")]
        [DataRow("0,01°K", "0,01°K")]
        [DataRow("-30,5°K", "-30,5°K")]
        [DataRow("Random text 30°K around temperature.", "30°K")]
        [DataRow("30,5°K Random text around temperature.", "30,5°K")]
        [DataRow("Random text around temperature. 30.5°K", "30.5°K")]
        public void GetTemperatureKelvinRegexMentionsTest(string input, string expected)
        {
            var actual = TemperatureFinder.GetTemperatureRegexMatches(input);
            Assert.AreEqual(expected, actual.First());
        }

        [TestMethod]
        [DataRow(new[] { "30°C", "30°C" }, "30°C 30°C")]
        [DataRow(new[] { "30°C", "30°C" }, "30°C\n30°C")]
        [DataRow(new[] { "30,0°C", "-30,5°C", "0°C", "25°C" }, "30,0°C -30,5°C 0°C 25°C")]
        [DataRow(new[] { "30C", "30°C", "30.0°C", "30.00°C" }, "30C 30°C 30.0°C 30.00°C")]
        [DataRow(new[] { "30C", "30°C", "30.0°C", "30.00°C" }, "30C 30°C 367ukj6783 67367 30.0°C trzh56jhwq6uji3w5j 30.00°C wz7j3673e68")]
        public void GetMultipleTemperatureCelsiusRegexMentionsTest(string[] expected, string input)
        {
            var actual = TemperatureFinder.GetTemperatureRegexMatches(input);
            CollectionAssert.AreEquivalent(expected, actual.ToList());
        }

        [TestMethod]
        [DataRow(new[] { "30°F", "30°F" }, "30°F 30°F")]
        [DataRow(new[] { "30°F", "30°F" }, "30°F\n30°F")]
        [DataRow(new[] { "30,0°F", "-30,5°F", "0°F", "25°F" }, "30,0°F -30,5°F 0°F 25°F")]
        [DataRow(new[] { "30F", "30°F", "30.0°F", "30.00°F" }, "30F 30°F 30.0°F 30.00°F")]
        [DataRow(new[] { "30F", "30°F", "30.0°F", "30.00°F" }, "30F 30°F 367ukj6783 67367 30.0°F trzh56jhwq6uji3w5j 30.00°F wz7j3673e68")]
        public void GetMultipleTemperatureFahrenheitRegexMentionsTest(string[] expected, string input)
        {
            var actual = TemperatureFinder.GetTemperatureRegexMatches(input);
            CollectionAssert.AreEquivalent(expected, actual.ToList());
        }

        [TestMethod]
        [DataRow(new[] { "30°K", "30°K" }, "30°K 30°K")]
        [DataRow(new[] { "30°K", "30°K" }, "30°K\n30°K")]
        [DataRow(new[] { "30,0°K", "-30,5°K", "0°K", "25°K" }, "30,0°K -30,5°K 0°K 25°K")]
        [DataRow(new[] { "30K", "30°K", "30.0°K", "30.00°K" }, "30K 30°K 30.0°K 30.00°K")]
        [DataRow(new[] { "30K", "30°K", "30.0°K", "30.00°K" }, "30K 30°K 367ukj6783 67367 30.0°K trzh56jhwq6uji3w5j 30.00°K wz7j3673e68")]
        public void GetMultipleTemperatureKelvingRegexMentionsTest(string[] expected, string input)
        {
            var actual = TemperatureFinder.GetTemperatureRegexMatches(input);
            CollectionAssert.AreEquivalent(expected, actual.ToList());
        }

        [TestMethod]
        [DataRow("30Kids")]
        [DataRow("20Kids when't out to play, but 15facists killed em all")]
        [DataRow("10cra403kdo10ftext5c")]
        [DataRow("10Cra403Kdo10Ftext5C")]
        public void ShouldNotParseAsTemperaturesTests(string input)
        {
            var actual = TemperatureFinder.GetTemperatureRegexMatches(input);
            Assert.IsTrue(!actual.Any());
        }
    }
}
