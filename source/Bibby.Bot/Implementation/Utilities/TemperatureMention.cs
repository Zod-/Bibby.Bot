namespace Bibby.Bot.Utilities
{
    public class TemperatureMention
    {
        public double Degrees { get; set; }
        public TemperatureUnit TemperatureUnit { get; set; }
    }

    public enum TemperatureUnit
    {
        Unknown,
        Celsius,
        Fahrenheit,
        Kelvin,
    }
}