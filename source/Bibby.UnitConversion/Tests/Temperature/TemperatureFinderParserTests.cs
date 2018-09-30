using Bibby.UnitConversion.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitsNet.Units;

namespace Bibby.UnitConversion.Tests.Temperature
{
    [TestClass]
    public class TemperatureFinderParserTests
    {
        [TestMethod]
        [DataRow("0°C", 0f)]
        [DataRow("30C", 30f)]
        [DataRow("25.0°C", 25f)]
        [DataRow("30,0°C", 30f)]
        [DataRow("-30,5°C", -30.5f)]
        [DataRow("-125.0°C", -125f)]
        [DataRow("1000.0°C", 1000f)]
        [DataRow("1000°C", 1000f)]
        [DataRow("1000000C", 1000000f)]
        [DataRow("30°F", 30f)]
        [DataRow("30F", 30f)]
        [DataRow("30.0°F", 30f)]
        [DataRow("30,0°F", 30f)]
        [DataRow("-30,5°F", -30.5f)]
        [DataRow("-125.0°F", -125f)]
        [DataRow("1000.0°F", 1000f)]
        [DataRow("1000°F", 1000f)]
        [DataRow("1000000F", 1000000f)]
        [DataRow("30°K", 30f)]
        [DataRow("30.0°K", 30f)]
        [DataRow("30,0°K", 30f)]
        [DataRow("-30,5°K", -30.5f)]
        [DataRow("-125.0°K", -125f)]
        [DataRow("1000.0°K", 1000f)]
        [DataRow("1000°K", 1000f)]
        [DataRow("1000000°K", 1000000f)]
        public void GetTemperatureCelsiusRegexMentionsTest(string input, float expected)
        {
            var actual = TemperatureConverter.ParseTemperatureValue(input);
            Assert.AreEqual(expected, actual, double.Epsilon);
        }

        [TestMethod]
        [DataRow("0°C", TemperatureUnit.DegreeCelsius)]
        [DataRow("30C", TemperatureUnit.DegreeCelsius)]
        [DataRow("30.0°C", TemperatureUnit.DegreeCelsius)]
        [DataRow("30,0°C", TemperatureUnit.DegreeCelsius)]
        [DataRow("-30,5°C", TemperatureUnit.DegreeCelsius)]
        [DataRow("30°F", TemperatureUnit.DegreeFahrenheit)]
        [DataRow("30F", TemperatureUnit.DegreeFahrenheit)]
        [DataRow("30.0°F", TemperatureUnit.DegreeFahrenheit)]
        [DataRow("30,0°F", TemperatureUnit.DegreeFahrenheit)]
        [DataRow("-30,5°F", TemperatureUnit.DegreeFahrenheit)]
        [DataRow("30°K", TemperatureUnit.Kelvin)]
        [DataRow("30.0°K", TemperatureUnit.Kelvin)]
        [DataRow("30,0°K", TemperatureUnit.Kelvin)]
        [DataRow("-30,5°K", TemperatureUnit.Kelvin)]
        public void ParseTemperatureUnitTest(string input, TemperatureUnit expected)
        {
            var actual = TemperatureConverter.ParseTemperatureUnit(input);
            Assert.AreEqual(expected, actual);
        }
    }
}
