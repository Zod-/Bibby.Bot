using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Bibby.Bot.Utilities
{
    public static class TemperatureFinder
    {
        public const string TemperatureRegex = @"([+-]?\d+(?:[\.,]\d+)?(?:(?:°?[CF])|(?:°K)))";

        public static IEnumerable<TemperatureMention> GetTemperatureMentions(string text)
        {
            var matches = GetTemperatureRegexMatches(text);
            foreach (var match in matches)
            {
                yield return ParseTemperatureMention(match);
            }
        }

        internal static TemperatureMention ParseTemperatureMention(string match)
        {
            return null;
        }

        internal static IEnumerable<string> GetTemperatureRegexMatches(string input)
        {
            var matchCollection = Regex.Matches(input, TemperatureRegex, RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Multiline);

            foreach (Match match in matchCollection)
            {
                yield return match.Groups[1].Value;
            }
        }
    }
}
