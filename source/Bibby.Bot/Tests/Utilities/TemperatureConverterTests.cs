
using Bibby.Bot.Utilities;
using Xunit;

namespace Bibby.Bot.Tests.Utilities
{
    public class TemperatureConverterTests
    {
        [Theory]
        [InlineData(-459.67, -273.15)]
        [InlineData(-50, -45.56)]
        [InlineData(0, -17.78)]
        [InlineData(32, 0)]
        [InlineData(90, 32.22)]
        [InlineData(110, 43.33)]
        [InlineData(212, 100)]
        [InlineData(300, 148.89)]
        public void FahrenheitToCelsiusTests(double fahrenheit, double celsius)
        {
            var actual = TemperatureConverter.FahrenheitToCelsius(fahrenheit);
            Assert.Equal(celsius, actual, 2);
        }

        [Theory]
        [InlineData(0, -459.67)]
        [InlineData(120, -243.67)]
        [InlineData(130, -225.67)]
        [InlineData(140, -207.67)]
        [InlineData(200, -99.67)]
        [InlineData(210, -81.67)]
        [InlineData(220, -63.67)]
        [InlineData(270, 26.33)]
        [InlineData(800, 980.33)]
        [InlineData(900, 1160.33)]
        [InlineData(1000, 1340.33)]
        public void KelvinToFahrenheit(double kelvin, double fahrenheit)
        {
            var actual = TemperatureConverter.KelvinToFahrenheit(kelvin);
            Assert.Equal(fahrenheit, actual, 2);
        }

        [Theory]
        [InlineData(0, -459.67)]
        [InlineData(120, -243.67)]
        [InlineData(130, -225.67)]
        [InlineData(140, -207.67)]
        [InlineData(200, -99.67)]
        [InlineData(210, -81.67)]
        [InlineData(220, -63.67)]
        [InlineData(270, 26.33)]
        [InlineData(800, 980.33)]
        [InlineData(900, 1160.33)]
        [InlineData(1000, 1340.33)]
        public void FahrenheitToKelvin(double kelvin, double fahrenheit)
        {
            var actual = TemperatureConverter.FahrenheitToKelvin(fahrenheit);
            Assert.Equal(kelvin, actual, 2);
        }

        [Theory]
        [InlineData(-459.67, -273.15)]
        [InlineData(-50, -45.56)]
        [InlineData(0, -17.78)]
        [InlineData(32, 0)]
        [InlineData(90, 32.22)]
        [InlineData(110, 43.33)]
        [InlineData(212, 100)]
        [InlineData(300, 148.89)]
        public void CelsiusToFahrenheit(double fahrenheit, double celsius)
        {
            var actual = TemperatureConverter.CelsiusToFahrenheit(celsius);
            Assert.Equal(fahrenheit, actual, 1);
        }

        [Theory]
        [InlineData(-273.15, 0)]
        [InlineData(-30, 243.15)]
        [InlineData(-20, 253.15)]
        [InlineData(-10, 263.15)]
        [InlineData(0, 273.15)]
        [InlineData(90, 363.15)]
        [InlineData(900, 1173.15)]
        [InlineData(1000, 1273.15)]
        public void CelsiusToKelvinTests(double celsius, double kelvin)
        {
            var actual = TemperatureConverter.CelsiusToKelvin(celsius);
            Assert.Equal(kelvin, actual, 2);
        }

        [Theory]
        [InlineData(-273.15, 0)]
        [InlineData(-30, 243.15)]
        [InlineData(-20, 253.15)]
        [InlineData(-10, 263.15)]
        [InlineData(0, 273.15)]
        [InlineData(90, 363.15)]
        [InlineData(900, 1173.15)]
        [InlineData(1000, 1273.15)]
        public void KelvinToCelsius(double celsius, double kelvin)
        {
            var actual = TemperatureConverter.KelvinToCelsius(kelvin);
            Assert.Equal(celsius, actual, 2);
        }
    }
}
