

using Bibby.Bot.Utilities;
using Xunit;

namespace Bibby.Bot.Tests.Utilities
{
    public class TemperatureFinderParserTests
    {
        [Theory]
        [InlineData("0°C", 0f)]
        [InlineData("30C", 30f)]
        [InlineData("25.0°C", 25f)]
        [InlineData("30,0°C", 30f)]
        [InlineData("-30,5°C", -30.5f)]
        [InlineData("-125.0°C", -125f)]
        [InlineData("1000.0°C", 1000f)]
        [InlineData("1000°C", 1000f)]
        [InlineData("1000000C", 1000000f)]
        [InlineData("30°F", 30f)]
        [InlineData("30F", 30f)]
        [InlineData("30.0°F", 30f)]
        [InlineData("30,0°F", 30f)]
        [InlineData("-30,5°F", -30.5f)]
        [InlineData("-125.0°F", -125f)]
        [InlineData("1000.0°F", 1000f)]
        [InlineData("1000°F", 1000f)]
        [InlineData("1000000F", 1000000f)]
        [InlineData("30°K", 30f)]
        [InlineData("30.0°K", 30f)]
        [InlineData("30,0°K", 30f)]
        [InlineData("-30,5°K", -30.5f)]
        [InlineData("-125.0°K", -125f)]
        [InlineData("1000.0°K", 1000f)]
        [InlineData("1000°K", 1000f)]
        [InlineData("1000000°K", 1000000f)]
        public void GetTemperatureCelsiusRegexMentionsTest(string input, float expected)
        {
            var actual = TemperatureFinder.ParseTemperatureDegrees(input);
            Assert.Equal(expected, actual, 3);
        }

        [Theory]
        [InlineData("0°C", TemperatureUnit.Celsius)]
        [InlineData("30C", TemperatureUnit.Celsius)]
        [InlineData("30.0°C", TemperatureUnit.Celsius)]
        [InlineData("30,0°C", TemperatureUnit.Celsius)]
        [InlineData("-30,5°C", TemperatureUnit.Celsius)]
        [InlineData("30°F", TemperatureUnit.Fahrenheit)]
        [InlineData("30F", TemperatureUnit.Fahrenheit)]
        [InlineData("30.0°F", TemperatureUnit.Fahrenheit)]
        [InlineData("30,0°F", TemperatureUnit.Fahrenheit)]
        [InlineData("-30,5°F", TemperatureUnit.Fahrenheit)]
        [InlineData("30°K", TemperatureUnit.Kelvin)]
        [InlineData("30.0°K", TemperatureUnit.Kelvin)]
        [InlineData("30,0°K", TemperatureUnit.Kelvin)]
        [InlineData("-30,5°K", TemperatureUnit.Kelvin)]
        public void ParseTemperatureUnitTest(string input, TemperatureUnit expected)
        {
            var actual = TemperatureFinder.ParseTemperatureUnit(input);
            Assert.Equal(expected, actual);
        }
    }
}
