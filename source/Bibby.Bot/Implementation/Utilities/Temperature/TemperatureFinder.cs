using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Bibby.Bot.Utilities.Temperature
{
    public static class TemperatureFinder
    {
        private const string TemperatureRegex = @"[+-]?\d+(?:[\.,]\d+)?°?[CcFfK]";
        private const string ReplaceUnitRegex = @"°?[CcFfK]";
        private static readonly CultureInfo DotCulture = CultureInfo.InvariantCulture;
        private static readonly CultureInfo CommaCulture = CultureInfo.GetCultureInfo("EN-DE");

        public static IEnumerable<TemperatureMention> GetTemperatureMentions(string text)
        {
            var matches = GetTemperatureRegexMatches(text);
            foreach (var match in matches)
            {
                yield return ParseTemperatureMention(match);
            }
        }

        internal static TemperatureMention ParseTemperatureMention(string input)
        {
            var ret = new TemperatureMention
            {
                Degrees = ParseTemperatureDegrees(input),
                TemperatureUnit = ParseTemperatureUnit(input)
            };
            return ret;
        }

        internal static double ParseTemperatureDegrees(string input)
        {
            input = Regex.Replace(input, ReplaceUnitRegex, string.Empty, RegexOptions.Compiled);
            if (double.TryParse(input,NumberStyles.Float, DotCulture,out var dotResult))
            {
                return dotResult;
            }
            if (double.TryParse(input, NumberStyles.Float, CommaCulture, out var commaResult))
            {
                return commaResult;
            }
            return 0;
        }

        internal static TemperatureUnit ParseTemperatureUnit(string input)
        {
            if (input.EndsWith("F", true, CultureInfo.InvariantCulture))
            {
                return TemperatureUnit.Fahrenheit;
            }
            if (input.EndsWith("C", true, CultureInfo.InvariantCulture))
            {
                return TemperatureUnit.Celsius;
            }
            if (input.EndsWith("K", true, CultureInfo.InvariantCulture))
            {
                return TemperatureUnit.Kelvin;
            }
            return TemperatureUnit.Unknown;
        }

        internal static IEnumerable<string> GetTemperatureRegexMatches(string input)
        {
            var matchCollection = Regex.Matches(input, TemperatureRegex, RegexOptions.Compiled | RegexOptions.Multiline);

            foreach (Match match in matchCollection)
            {
                yield return match.Groups[0].Value;
            }
        }
    }
}
