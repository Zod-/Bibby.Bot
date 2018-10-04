using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using UnitsNet;
using UnitsNet.Units;

namespace Bibby.UnitConversion.Converters
{
    public class TemperatureConverter : BaseConverter<Temperature>
    {
        private const string UnitRegex = @"°?[CcFfK]";
        private const string TemperatureRegex = @"^[+-]?\d+(?:[\.,]\d+)?°?[CcFfK]$";
        private static readonly CultureInfo DotCulture = CultureInfo.InvariantCulture;
        private static readonly CultureInfo CommaCulture = CultureInfo.GetCultureInfo("EN-DE");
        private const RegexOptions RegexOpts = RegexOptions.Compiled;

        protected override IEnumerable<(IQuantity unit, IQuantity converted)> ConvertUnits(IEnumerable<Temperature> foundUnits)
        {
            foreach (var foundUnit in foundUnits)
            {
                foreach (var conversionUnit in ConversionUnitMapping(foundUnit.Unit))
                {
                    yield return (foundUnit, foundUnit.ToUnit(conversionUnit));
                }
            }
        }

        private static IEnumerable<TemperatureUnit> ConversionUnitMapping(TemperatureUnit unit)
        {
            switch (unit)
            {
                case TemperatureUnit.Undefined:
                    break;
                case TemperatureUnit.DegreeCelsius:
                    yield return TemperatureUnit.DegreeFahrenheit;
                    break;
                case TemperatureUnit.DegreeFahrenheit:
                    yield return TemperatureUnit.DegreeCelsius;
                    break;
                case TemperatureUnit.DegreeDelisle:
                case TemperatureUnit.DegreeNewton:
                case TemperatureUnit.DegreeRankine:
                case TemperatureUnit.DegreeReaumur:
                case TemperatureUnit.DegreeRoemer:
                case TemperatureUnit.Kelvin:
                    yield return TemperatureUnit.DegreeCelsius;
                    yield return TemperatureUnit.DegreeFahrenheit;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override bool TryParse(string split, out Temperature foundUnit)
        {
            return Temperature.TryParse(split, out foundUnit) || TryParseWithoutDegree(split, out foundUnit);
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
