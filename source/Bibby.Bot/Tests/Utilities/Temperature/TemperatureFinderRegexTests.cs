using Bibby.Bot.Utilities.Temperature;
using NUnit.Framework;

namespace Bibby.Bot.Tests.Utilities.Temperature
{
    public class TemperatureFinderRegexTests
    {
        [TestCase("30°C", "30°C")]
        [TestCase("30°C ", "30°C")]
        [TestCase("30°c", "30°c")]
        [TestCase(" 30°c ", "30°c")]
        [TestCase("30C", "30C")]
        [TestCase("30.0°C", "30.0°C")]
        [TestCase("413,0°C", "413,0°C")]
        [TestCase("-30,5°C", "-30,5°C")]
        [TestCase("Random text 30°C around temperature.", "30°C")]
        [TestCase("30,5°C Random text around temperature.", "30,5°C")]
        [TestCase("Random text around temperature.30.5°C", "30.5°C")]
        public void GetTemperatureCelsiusRegexMentionsTest(string input, params string[] expected)
        {
            var actual = TemperatureFinder.GetTemperatureRegexMatches(input);
            Assert.AreEqual(expected, actual);
        }

        [Theory]
        [TestCase("30°F", "30°F")]
        [TestCase("30F", "30F")]
        [TestCase("30f", "30f")]
        [TestCase(" 30f ", "30f")]
        [TestCase("30.0°F", "30.0°F")]
        [TestCase("500,0°F", "500,0°F")]
        [TestCase("-30,5°F", "-30,5°F")]
        [TestCase("Random text 30°F around temperature.", "30°F")]
        [TestCase("30,5°F Random text around temperature.", "30,5°F")]
        [TestCase("Random text around temperature.30.5°F", "30.5°F")]
        public void GetTemperatureFahrenheitRegexMentionsTest(string input, params string[] expected)
        {
            var actual = TemperatureFinder.GetTemperatureRegexMatches(input);
            Assert.AreEqual(expected, actual);
        }

        [Theory]
        [TestCase("30°K", "30°K")]
        [TestCase("30K", "30K")]
        [TestCase("30.0°K", "30.0°K")]
        [TestCase(" 30.0°K ", "30.0°K")]
        [TestCase("0,01°K", "0,01°K")]
        [TestCase("-30,5°K", "-30,5°K")]
        [TestCase("Random text 30°K around temperature.", "30°K")]
        [TestCase("30,5°K Random text around temperature.", "30,5°K")]
        [TestCase("Random text around temperature.30.5°K", "30.5°K")]
        public void GetTemperatureKelvinRegexMentionsTest(string input, params string[] expected)
        {
            var actual = TemperatureFinder.GetTemperatureRegexMatches(input);
            Assert.AreEqual(expected, actual);
        }

        [Theory]
        [TestCase("30°C 30°C", "30°C", "30°C")]
        [TestCase("30,0°C -30,5°C 0°C 25°C", "30,0°C", "-30,5°C", "0°C", "25°C")]
        [TestCase("30C 30°C 30.0°C 30.00°C", "30C", "30°C", "30.0°C", "30.00°C")]
        [TestCase("30C 30°C 367ukj6783 67367 30.0°C trzh56jhwq6uji3w5j 30.00°C wz7j3673e68", "30C", "30°C", "30.0°C", "30.00°C")]
        public void GetMultipleTemperatureCelsiusRegexMentionsTest(string input, params string[] expected)
        {
            var actual = TemperatureFinder.GetTemperatureRegexMatches(input);
            Assert.AreEqual(expected, actual);
        }

        [Theory]
        [TestCase("30°F 30°F", "30°F", "30°F")]
        [TestCase("30,0°F -30,5°F 0°F 25°F", "30,0°F", "-30,5°F", "0°F", "25°F")]
        [TestCase("876ol4897l49il4 30F 6j7373w76367367 30°F 874o45rjezztdj2652 30.0°F 87o5745 78i8 4678ik 467wrgdsfth 30.00°F", "30F", "30°F", "30.0°F", "30.00°F")]
        public void GetMultipleTemperatureFahrenheitRegexMentionsTest(string input, params string[] expected)
        {
            var actual = TemperatureFinder.GetTemperatureRegexMatches(input);
            Assert.AreEqual(expected, actual);
        }

        [Theory]
        [TestCase("30°K 30°K", "30°K", "30°K")]
        [TestCase("30,0°K -30,5°K 0°K 25°K", "30,0°K", "-30,5°K", "0°K", "25°K")]
        [TestCase("342543zwt4r5h 30°K 467ik4687654ui67i647i64 30.0°K  4768o46747 30.00°K 4678io468o46", "30°K", "30.0°K", "30.00°K")]
        public void GetMultipleTemperatureKelvingRegexMentionsTest(string input, params string[] expected)
        {
            var actual = TemperatureFinder.GetTemperatureRegexMatches(input);
            Assert.AreEqual(expected, actual);
        }

        [Theory]
        [TestCase("30Kids")]
        [TestCase("20Kids when't out to play, but 15facists killed em all")]
        public void ShouldNotParseAsTemperaturesTests(string input)
        {
            var actual = TemperatureFinder.GetTemperatureRegexMatches(input);
            CollectionAssert.IsEmpty(actual);
        }
    }
}
