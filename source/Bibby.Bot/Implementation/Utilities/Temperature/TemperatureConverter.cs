namespace Bibby.Bot.Utilities.Temperature
{
    public static class TemperatureConverter
    {
        public static double KelvinToCelsius(double kelvin)
        {
            return kelvin - 273.15f;
        }
        public static double KelvinToFahrenheit(double kelvin)
        {
            var celsius = KelvinToCelsius(kelvin);
            return CelsiusToFahrenheit(celsius);
        }

        public static double CelsiusToKelvin(double celsius)
        {
            return celsius + 273.15f;
        }

        public static double CelsiusToFahrenheit(double celsius)
        {
            return celsius * 9f / 5f + 32;
        }

        public static double FahrenheitToCelsius(double fahrenheit)
        {
            return (fahrenheit - 32) * 5f / 9f;
        }

        public static double FahrenheitToKelvin(double fahrenheit)
        {
            var celsius = FahrenheitToCelsius(fahrenheit);
            return CelsiusToKelvin(celsius);
        }
    }
}
