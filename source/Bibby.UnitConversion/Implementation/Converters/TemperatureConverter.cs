using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Bibby.UnitConversion.Contracts;
using UnitsNet;
using UnitsNet.Units;

namespace Bibby.UnitConversion.Converters
{
    public class TemperatureConverter : IConvertUnits
    {
        private const string UnitRegex = @"°?[CcFfK]";
        private const string TemperatureRegex = @"^[+-]?\d+(?:[\.,]\d+)?°?[CcFfK]$";
        private static readonly CultureInfo DotCulture = CultureInfo.InvariantCulture;
        private static readonly CultureInfo CommaCulture = CultureInfo.GetCultureInfo("EN-DE");
        private const RegexOptions RegexOpts = RegexOptions.Compiled;

        public IEnumerable<(string unit, IQuantity converted)> ConvertUnits(string input)
        {
            var foundUnits = FindTemperatures(input);
            var ret = ConvertTemperatures(foundUnits);
            return ret;
        }

        private static IEnumerable<(string unit, IQuantity converted)> ConvertTemperatures(IEnumerable<(string unit, Temperature temperature)> foundUnits)
        {
            foreach (var (unit, temperature) in foundUnits)
            {
                switch (temperature.Unit)
                {
                    case TemperatureUnit.Undefined:
                        break;
                    case TemperatureUnit.DegreeCelsius:
                        yield return (unit, temperature.ToUnit(TemperatureUnit.DegreeFahrenheit));
                        break;
                    case TemperatureUnit.DegreeFahrenheit:
                        yield return (unit, temperature.ToUnit(TemperatureUnit.DegreeCelsius));
                        break;
                    case TemperatureUnit.DegreeDelisle:
                    case TemperatureUnit.DegreeNewton:
                    case TemperatureUnit.DegreeRankine:
                    case TemperatureUnit.DegreeReaumur:
                    case TemperatureUnit.DegreeRoemer:
                    case TemperatureUnit.Kelvin:
                        yield return (unit, temperature.ToUnit(TemperatureUnit.DegreeCelsius));
                        yield return (unit, temperature.ToUnit(TemperatureUnit.DegreeFahrenheit));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private static IEnumerable<(string unit, Temperature temperature)> FindTemperatures(string message)
        {
            var splits = message.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);

            foreach (var split in splits)
            {
                if (Temperature.TryParse(split, out var temperature))
                {
                    yield return (split, temperature);
                }
                else if(TryParseWithoutDegree(split, out temperature))
                {
                    yield return (split, temperature);
                }
            }
        }
        internal static double ParseTemperatureValue(string input)
        {
            input = Regex.Replace(input, UnitRegex, string.Empty, RegexOpts);
            if (double.TryParse(input, NumberStyles.Float, DotCulture, out var dotResult))
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
                return TemperatureUnit.DegreeFahrenheit;
            }
            if (input.EndsWith("C", true, CultureInfo.InvariantCulture))
            {
                return TemperatureUnit.DegreeCelsius;
            }
            if (input.EndsWith("K", true, CultureInfo.InvariantCulture))
            {
                return TemperatureUnit.Kelvin;
            }
            return TemperatureUnit.Undefined;
        }

        private static bool TryParseWithoutDegree(string split, out Temperature temperature)
        {
            var match = Regex.Match(split, TemperatureRegex, RegexOpts);
            if (match.Success)
            {
                var value = ParseTemperatureValue(split);
                var unit = ParseTemperatureUnit(split);
                temperature = new Temperature(value, unit);
                return true;
            }

            temperature = Temperature.Zero;
            return false;
        }
    }
}
