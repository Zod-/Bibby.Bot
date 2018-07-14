namespace Bibby.Bot.Utilities
{
    public class TemperatureMention
    {
        public float Degrees { get; set; }
        public TemperatureUnit TemperatureUnit { get; set; }
    }

    public enum TemperatureUnit
    {
        Celsius,
        Fahrenheit,
        Kelvin
    }
}