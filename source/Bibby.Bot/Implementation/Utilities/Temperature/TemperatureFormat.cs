using System.Globalization;

namespace Bibby.Bot.Utilities.Temperature
{
    public static class TemperatureFormat
    {
        public static string CelsiusToString(double celsius)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0:0.##}°C", celsius);
        }

        public static string FahrenheitToString(double fahrenheit)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0:0.##}°F", fahrenheit);
        }

        public static string KelvinToString(double kelvin)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0:0.##}K", kelvin);
        }

        internal static string KelvinToSunSurfacePercentage(double kelvin)
        {
            var percentage = kelvin / SunSurfaceTemp * 100;
            return string.Format(CultureInfo.InvariantCulture, "{0:0.##}% of the surface of the Sun.", percentage);
        }

        private const double SunSurfaceTemp = 5778;
    }
}
