using Bibby.Bot.Utilities.Temperature;
using NUnit.Framework;

namespace Bibby.Bot.Tests.Utilities.Temperature
{
    public class TemperatureFinderParserTests
    {
        [TestCase("0°C", 0f)]
        [TestCase("30C", 30f)]
        [TestCase("25.0°C", 25f)]
        [TestCase("30,0°C", 30f)]
        [TestCase("-30,5°C", -30.5f)]
        [TestCase("-125.0°C", -125f)]
        [TestCase("1000.0°C", 1000f)]
        [TestCase("1000°C", 1000f)]
        [TestCase("1000000C", 1000000f)]
        [TestCase("30°F", 30f)]
        [TestCase("30F", 30f)]
        [TestCase("30.0°F", 30f)]
        [TestCase("30,0°F", 30f)]
        [TestCase("-30,5°F", -30.5f)]
        [TestCase("-125.0°F", -125f)]
        [TestCase("1000.0°F", 1000f)]
        [TestCase("1000°F", 1000f)]
        [TestCase("1000000F", 1000000f)]
        [TestCase("30°K", 30f)]
        [TestCase("30.0°K", 30f)]
        [TestCase("30,0°K", 30f)]
        [TestCase("-30,5°K", -30.5f)]
        [TestCase("-125.0°K", -125f)]
        [TestCase("1000.0°K", 1000f)]
        [TestCase("1000°K", 1000f)]
        [TestCase("1000000°K", 1000000f)]
        public void GetTemperatureCelsiusRegexMentionsTest(string input, float expected)
        {
            var actual = TemperatureFinder.ParseTemperatureDegrees(input);
            Assert.AreEqual(expected, actual, double.Epsilon);
        }

        [Theory]
        [TestCase("0°C", TemperatureUnit.Celsius)]
        [TestCase("30C", TemperatureUnit.Celsius)]
        [TestCase("30.0°C", TemperatureUnit.Celsius)]
        [TestCase("30,0°C", TemperatureUnit.Celsius)]
        [TestCase("-30,5°C", TemperatureUnit.Celsius)]
        [TestCase("30°F", TemperatureUnit.Fahrenheit)]
        [TestCase("30F", TemperatureUnit.Fahrenheit)]
        [TestCase("30.0°F", TemperatureUnit.Fahrenheit)]
        [TestCase("30,0°F", TemperatureUnit.Fahrenheit)]
        [TestCase("-30,5°F", TemperatureUnit.Fahrenheit)]
        [TestCase("30°K", TemperatureUnit.Kelvin)]
        [TestCase("30.0°K", TemperatureUnit.Kelvin)]
        [TestCase("30,0°K", TemperatureUnit.Kelvin)]
        [TestCase("-30,5°K", TemperatureUnit.Kelvin)]
        public void ParseTemperatureUnitTest(string input, TemperatureUnit expected)
        {
            var actual = TemperatureFinder.ParseTemperatureUnit(input);
            Assert.AreEqual(expected, actual);
        }
    }
}
