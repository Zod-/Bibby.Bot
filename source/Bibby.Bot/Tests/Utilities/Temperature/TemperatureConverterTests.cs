using Bibby.Bot.Utilities.Temperature;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bibby.Bot.Tests.Utilities.Temperature
{
    [TestClass]
    public class TemperatureConverterTests
    {
        [TestMethod]
        [DataRow(-459.67, -273.15)]
        [DataRow(-50, -45.56)]
        [DataRow(0, -17.78)]
        [DataRow(32, 0)]
        [DataRow(90, 32.22)]
        [DataRow(110, 43.33)]
        [DataRow(212, 100)]
        [DataRow(300, 148.89)]
        public void FahrenheitToCelsiusTests(double fahrenheit, double celsius)
        {
            var actual = TemperatureConverter.FahrenheitToCelsius(fahrenheit);
            Assert.AreEqual(celsius, actual, 0.01);
        }

        [TestMethod]
        [DataRow(0, -459.67)]
        [DataRow(120, -243.67)]
        [DataRow(130, -225.67)]
        [DataRow(140, -207.67)]
        [DataRow(200, -99.67)]
        [DataRow(210, -81.67)]
        [DataRow(220, -63.67)]
        [DataRow(270, 26.33)]
        [DataRow(800, 980.33)]
        [DataRow(900, 1160.33)]
        [DataRow(1000, 1340.33)]
        public void KelvinToFahrenheit(double kelvin, double fahrenheit)
        {
            var actual = TemperatureConverter.KelvinToFahrenheit(kelvin);
            Assert.AreEqual(fahrenheit, actual, 0.01);
        }

        [TestMethod]
        [DataRow(0, -459.67)]
        [DataRow(120, -243.67)]
        [DataRow(130, -225.67)]
        [DataRow(140, -207.67)]
        [DataRow(200, -99.67)]
        [DataRow(210, -81.67)]
        [DataRow(220, -63.67)]
        [DataRow(270, 26.33)]
        [DataRow(800, 980.33)]
        [DataRow(900, 1160.33)]
        [DataRow(1000, 1340.33)]
        public void FahrenheitToKelvin(double kelvin, double fahrenheit)
        {
            var actual = TemperatureConverter.FahrenheitToKelvin(fahrenheit);
            Assert.AreEqual(kelvin, actual, 0.01);
        }

        [TestMethod]
        [DataRow(-459.67, -273.15)]
        [DataRow(-50, -45.56)]
        [DataRow(0, -17.78)]
        [DataRow(32, 0)]
        [DataRow(90, 32.22)]
        [DataRow(110, 43.33)]
        [DataRow(212, 100)]
        [DataRow(300, 148.89)]
        public void CelsiusToFahrenheit(double fahrenheit, double celsius)
        {
            var actual = TemperatureConverter.CelsiusToFahrenheit(celsius);
            Assert.AreEqual(fahrenheit, actual, 0.1);
        }

        [TestMethod]
        [DataRow(-273.15, 0)]
        [DataRow(-30, 243.15)]
        [DataRow(-20, 253.15)]
        [DataRow(-10, 263.15)]
        [DataRow(0, 273.15)]
        [DataRow(90, 363.15)]
        [DataRow(900, 1173.15)]
        [DataRow(1000, 1273.15)]
        public void CelsiusToKelvinTests(double celsius, double kelvin)
        {
            var actual = TemperatureConverter.CelsiusToKelvin(celsius);
            Assert.AreEqual(kelvin, actual, 0.01);
        }

        [TestMethod]
        [DataRow(-273.15, 0)]
        [DataRow(-30, 243.15)]
        [DataRow(-20, 253.15)]
        [DataRow(-10, 263.15)]
        [DataRow(0, 273.15)]
        [DataRow(90, 363.15)]
        [DataRow(900, 1173.15)]
        [DataRow(1000, 1273.15)]
        public void KelvinToCelsius(double celsius, double kelvin)
        {
            var actual = TemperatureConverter.KelvinToCelsius(kelvin);
            Assert.AreEqual(celsius, actual, 0.01);
        }
    }
}
