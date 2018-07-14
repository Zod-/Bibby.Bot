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
            var result = ToString() + " **=>** ";
            switch (TemperatureUnit)
            {
                case TemperatureUnit.Unknown:
                    return string.Empty;
                case TemperatureUnit.Celsius:
                    var convertedFahrenheit = TemperatureConverter.CelsiusToFahrenheit(Degrees);
                    result += TemperatureFormat.FahrenheitToString(convertedFahrenheit);
                    break;
                case TemperatureUnit.Fahrenheit:
                    var convertedCelsius = TemperatureConverter.FahrenheitToCelsius(Degrees);
                    result += TemperatureFormat.CelsiusToString(convertedCelsius);
                    break;
                case TemperatureUnit.Kelvin:
                    var convertedF = TemperatureConverter.KelvinToFahrenheit(Degrees);
                    var convertedC = TemperatureConverter.KelvinToCelsius(Degrees);
                    result += TemperatureFormat.FahrenheitToString(convertedF);
                    result += ", " + TemperatureFormat.CelsiusToString(convertedC);
                    break;
            }
            return result;
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