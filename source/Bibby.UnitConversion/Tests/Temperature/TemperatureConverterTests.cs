using System.Linq;
using Bibby.UnitConversion.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bibby.UnitConversion.Tests.Temperature
{
    [TestClass]
    public class TemperatureConverterTests
    {
        [TestMethod]
        [DataRow(new[] { "86 °F" }, "30°C")]
        [DataRow(new[] { "30 °C" }, "86°F")]
        [DataRow(new[] { "-273.15 °C", "-459.67 °F" }, "0°K")]
        public void FahrenheitToCelsiusTests(string[] expected, string input)
        {
            var converter = new TemperatureConverter();
            var actual = converter.ConvertUnits(input).Select(c => c.converted.ToString()).ToList();
            CollectionAssert.AreEqual(expected, actual,actual.Last());
        }
    }
}
