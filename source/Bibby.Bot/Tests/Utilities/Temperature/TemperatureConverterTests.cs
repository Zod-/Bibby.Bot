using Bibby.Bot.Utilities.Temperature;
using NUnit.Framework;

namespace Bibby.Bot.Tests.Utilities.Temperature
{
    public class TemperatureConverterTests
    {
        [TestCase(-459.67, -273.15)]
        [TestCase(-50, -45.56)]
        [TestCase(0, -17.78)]
        [TestCase(32, 0)]
        [TestCase(90, 32.22)]
        [TestCase(110, 43.33)]
        [TestCase(212, 100)]
        [TestCase(300, 148.89)]
        public void FahrenheitToCelsiusTests(double fahrenheit, double celsius)
        {
            var actual = TemperatureConverter.FahrenheitToCelsius(fahrenheit);
            Assert.AreEqual(celsius, actual, 0.01);
        }

        [Theory]
        [TestCase(0, -459.67)]
        [TestCase(120, -243.67)]
        [TestCase(130, -225.67)]
        [TestCase(140, -207.67)]
        [TestCase(200, -99.67)]
        [TestCase(210, -81.67)]
        [TestCase(220, -63.67)]
        [TestCase(270, 26.33)]
        [TestCase(800, 980.33)]
        [TestCase(900, 1160.33)]
        [TestCase(1000, 1340.33)]
        public void KelvinToFahrenheit(double kelvin, double fahrenheit)
        {
            var actual = TemperatureConverter.KelvinToFahrenheit(kelvin);
            Assert.AreEqual(fahrenheit, actual, 0.01);
        }

        [Theory]
        [TestCase(0, -459.67)]
        [TestCase(120, -243.67)]
        [TestCase(130, -225.67)]
        [TestCase(140, -207.67)]
        [TestCase(200, -99.67)]
        [TestCase(210, -81.67)]
        [TestCase(220, -63.67)]
        [TestCase(270, 26.33)]
        [TestCase(800, 980.33)]
        [TestCase(900, 1160.33)]
        [TestCase(1000, 1340.33)]
        public void FahrenheitToKelvin(double kelvin, double fahrenheit)
        {
            var actual = TemperatureConverter.FahrenheitToKelvin(fahrenheit);
            Assert.AreEqual(kelvin, actual, 0.01);
        }

        [Theory]
        [TestCase(-459.67, -273.15)]
        [TestCase(-50, -45.56)]
        [TestCase(0, -17.78)]
        [TestCase(32, 0)]
        [TestCase(90, 32.22)]
        [TestCase(110, 43.33)]
        [TestCase(212, 100)]
        [TestCase(300, 148.89)]
        public void CelsiusToFahrenheit(double fahrenheit, double celsius)
        {
            var actual = TemperatureConverter.CelsiusToFahrenheit(celsius);
            Assert.AreEqual(fahrenheit, actual, 0.1);
        }

        [Theory]
        [TestCase(-273.15, 0)]
        [TestCase(-30, 243.15)]
        [TestCase(-20, 253.15)]
        [TestCase(-10, 263.15)]
        [TestCase(0, 273.15)]
        [TestCase(90, 363.15)]
        [TestCase(900, 1173.15)]
        [TestCase(1000, 1273.15)]
        public void CelsiusToKelvinTests(double celsius, double kelvin)
        {
            var actual = TemperatureConverter.CelsiusToKelvin(celsius);
            Assert.AreEqual(kelvin, actual, 0.01);
        }

        [Theory]
        [TestCase(-273.15, 0)]
        [TestCase(-30, 243.15)]
        [TestCase(-20, 253.15)]
        [TestCase(-10, 263.15)]
        [TestCase(0, 273.15)]
        [TestCase(90, 363.15)]
        [TestCase(900, 1173.15)]
        [TestCase(1000, 1273.15)]
        public void KelvinToCelsius(double celsius, double kelvin)
        {
            var actual = TemperatureConverter.KelvinToCelsius(kelvin);
            Assert.AreEqual(celsius, actual, 0.01);
        }
    }
}
