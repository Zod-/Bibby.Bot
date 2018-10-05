using System.Linq;
using Bibby.UnitConversion.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bibby.UnitConversion.Tests.Temperature
{
    [TestClass]
    public class TemperatureFinderRegexTests
    {
        [TestMethod]
        [DataRow("30°C", "30 °C")]
        [DataRow("30 °C", "30 °C")]
        [DataRow("30C", "30 °C")]
        [DataRow("30 C", "30 °C")]
        [DataRow(" 30C ", "30 °C")]
        [DataRow(" 30 C ", "30 °C")]
        [DataRow("30.0°C", "30 °C")]
        [DataRow("30.0 °C", "30 °C")]
        [DataRow("Random text 30°C around temperature.", "30 °C")]
        [DataRow("Random text 30 °C around temperature.", "30 °C")]
        [DataRow("Random text around temperature. 30.5°C", "30.5 °C")]
        [DataRow("Random text around temperature. 30.5 °C", "30.5 °C")]
        public void GetTemperatureCelsiusRegexMentionsTest(string input, string expected)
        {
            var converter = new TemperatureConverter();
            var actual = converter.ConvertUnits(input).First().unit;
            Assert.AreEqual(expected, actual.ToString());
        }

        [TestMethod]
        [DataRow("30°F", "30 °F")]
        [DataRow("30 °F", "30 °F")]
        [DataRow("30F", "30 °F")]
        [DataRow("30 F", "30 °F")]
        [DataRow(" 30F ", "30 °F")]
        [DataRow(" 30 F ", "30 °F")]
        [DataRow("30.0°F", "30 °F")]
        [DataRow("30.0 °F", "30 °F")]
        [DataRow("Random text 30°F around temperature.", "30 °F")]
        [DataRow("Random text 30 °F around temperature.", "30 °F")]
        [DataRow("Random text around temperature. 30.5°F", "30.5 °F")]
        [DataRow("Random text around temperature. 30.5 °F", "30.5 °F")]
        public void GetTemperatureFahrenheitRegexMentionsTest(string input, string expected)
        {
            var converter = new TemperatureConverter();
            var actual = converter.ConvertUnits(input).First().unit;
            Assert.AreEqual(expected, actual.ToString());
        }

        [TestMethod]
        [DataRow("30K", "30 K")]
        [DataRow("30 K", "30 K")]
        [DataRow("30K", "30 K")]
        [DataRow("30 K", "30 K")]
        [DataRow(" 30K ", "30 K")]
        [DataRow(" 30 K ", "30 K")]
        [DataRow("30.0K", "30 K")]
        [DataRow("30.0 K", "30 K")]
        [DataRow("Random text 30K around temperature.", "30 K")]
        [DataRow("Random text 30 K around temperature.", "30 K")]
        [DataRow("Random text around temperature. 30.5K", "30.5 K")]
        [DataRow("Random text around temperature. 30.5 K", "30.5 K")]
        public void GetTemperatureKelvinRegexMentionsTest(string input, string expected)
        {
            var converter = new TemperatureConverter();
            var actual = converter.ConvertUnits(input).First().unit;
            Assert.AreEqual(expected, actual.ToString());
        }

        [TestMethod]
        [DataRow(new[] { "30 °C", "30 °C" }, "30°C 30°C")]
        [DataRow(new[] { "30 °C", "30 °C" }, "30 °C 30 °C")]
        [DataRow(new[] { "30 °C", "30 °C" }, "30°C\n30°C")]
        [DataRow(new[] { "30 °C", "30 °C", "30 °C", "30 °C" }, "30C 30°C 30.0°C 30.00°C")]
        [DataRow(new[] { "30 °C", "30 °C", "30 °C", "30 °C" }, "30C 30°C 367ukj6783 67367 30.0°C trzh56jhwq6uji3w5j 30.00°C wz7j3673e68")]
        public void GetMultipleTemperatureCelsiusRegexMentionsTest(string[] expected, string input)
        {
            var converter = new TemperatureConverter();
            var actual = converter.ConvertUnits(input).Select(pair => pair.unit.ToString()).ToList();
            CollectionAssert.AreEqual(expected,actual);
        }

        [TestMethod]
        [DataRow(new[] { "30 °F", "30 °F" }, "30°F 30°F")]
        [DataRow(new[] { "30 °F", "30 °F" }, "30 °F 30 °F")]
        [DataRow(new[] { "30 °F", "30 °F" }, "30°F\n30°F")]
        [DataRow(new[] { "30 °F", "30 °F", "30 °F", "30 °F" }, "30F 30°F 30.0°F 30.00°F")]
        [DataRow(new[] { "30 °F", "30 °F", "30 °F", "30 °F" }, "30F 30°F 367ukj6783 67367 30.0°F trzh56jhwq6uji3w5j 30.00°F wz7j3673e68")]
        public void GetMultipleTemperatureFahrenheitRegexMentionsTest(string[] expected, string input)
        {
            var converter = new TemperatureConverter();
            var actual = converter.ConvertUnits(input).Select(pair => pair.unit.ToString()).ToList();
            CollectionAssert.AreEqual(expected, actual);
        }
        
        [TestMethod]
        [DataRow("30Kids")]
        [DataRow("20Kids when't out to play, but 15facists killed em all")]
        [DataRow("10cra403kdo10ftext5c")]
        [DataRow("10Cra403Kdo10Ftext5C")]
        public void ShouldNotParseAsTemperaturesTests(string input)
        {
            var converter = new TemperatureConverter();
            var actual = converter.ConvertUnits(input);
            Assert.IsTrue(!actual.Any());
        }
    }
}
