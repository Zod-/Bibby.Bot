namespace Bibby.Bot.Utilities.Temperature
{
    public class TemperatureMention
    {
        public double Degrees { get; set; }
        public TemperatureUnit TemperatureUnit { get; set; }

        public override string ToString()
        {
            switch (TemperatureUnit)
            {
                case TemperatureUnit.Unknown:
                    return string.Empty;
                case TemperatureUnit.Celsius:
                    return TemperatureFormat.CelsiusToString(Degrees);
                case TemperatureUnit.Fahrenheit:
                    return TemperatureFormat.FahrenheitToString(Degrees);
                case TemperatureUnit.Kelvin:
                    return TemperatureFormat.KelvinToString(Degrees);
                default:
                    return string.Empty;
            }
        }

        public string ToStringConverted()
        {
            switch (TemperatureUnit)
            {
                case TemperatureUnit.Unknown:
                    return string.Empty;
                case TemperatureUnit.Celsius:
                    return CelsiusToFahrenheitToString();
                case TemperatureUnit.Fahrenheit:
                    return FahrenheitToCelsiusToString();
                case TemperatureUnit.Kelvin:
                    return KelvinToFahrenheitAndCelsiusToString();
            }
            return string.Empty;
        }

        private string CelsiusToFahrenheitToString()
        {
            var ret = string.Empty;
            var convertedFahrenheit = TemperatureConverter.CelsiusToFahrenheit(Degrees);
            ret += TemperatureFormat.FahrenheitToString(convertedFahrenheit);
            var kelvin1 = TemperatureConverter.CelsiusToKelvin(Degrees);
            ret += ", " + TemperatureFormat.KelvinToSunSurfacePercentage(kelvin1);
            return ret;
        }

        private string FahrenheitToCelsiusToString()
        {
            var ret = string.Empty;
            var convertedCelsius = TemperatureConverter.FahrenheitToCelsius(Degrees);
            ret += TemperatureFormat.CelsiusToString(convertedCelsius);
            var kelvin2 = TemperatureConverter.FahrenheitToKelvin(Degrees);
            ret += ", " + TemperatureFormat.KelvinToSunSurfacePercentage(kelvin2);
            return ret;
        }

        private string KelvinToFahrenheitAndCelsiusToString()
        {
            var ret = string.Empty;
            var convertedF = TemperatureConverter.KelvinToFahrenheit(Degrees);
            var convertedC = TemperatureConverter.KelvinToCelsius(Degrees);
            ret += TemperatureFormat.FahrenheitToString(convertedF);
            ret += ", " + TemperatureFormat.CelsiusToString(convertedC);
            ret += ", " + TemperatureFormat.KelvinToSunSurfacePercentage(Degrees);
            return ret;
        }
    }

    public enum TemperatureUnit
    {
        Unknown,
        Celsius,
        Fahrenheit,
        Kelvin,
    }
}
