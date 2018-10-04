
using System;
using System.Collections.Generic;
using Bibby.UnitConversion.Contracts;
using UnitsNet;
using UnitsNet.Units;

namespace Bibby.UnitConversion.Converters
{
    public class LengthConverter : BaseConverter<Length>
    {
        protected override IEnumerable<(string unit, IQuantity converted)> ConvertUnits(IEnumerable<(string input, Length foundUnit)> foundUnits)
        {
            foreach (var (unit, length) in foundUnits)
            {
                foreach (var conversionUnit in ConversionUnitMapping(length.Unit))
                {
                    yield return (unit, length.ToUnit(conversionUnit));
                }
            }
        }

        private static IEnumerable<LengthUnit> ConversionUnitMapping(LengthUnit unit)
        {
            switch (unit)
            {
                case LengthUnit.Undefined:
                    break;
                case LengthUnit.Centimeter:
                case LengthUnit.Decimeter:
                case LengthUnit.Millimeter:
                    yield return LengthUnit.Inch;
                    break;
                case LengthUnit.Inch:
                case LengthUnit.Foot:
                    yield return LengthUnit.Centimeter;
                    break;
                case LengthUnit.Kilometer:
                    yield return LengthUnit.Mile;
                    break;
                case LengthUnit.Mile:
                    yield return LengthUnit.Kilometer;
                    break;
                case LengthUnit.Microinch:
                case LengthUnit.Nanometer:
                case LengthUnit.Micrometer:
                    yield return LengthUnit.Inch;
                    yield return LengthUnit.Millimeter;
                    break;
                case LengthUnit.Yard:
                    yield return LengthUnit.Meter;
                    break;
                case LengthUnit.Meter:
                    yield return LengthUnit.Yard;
                    break;
                case LengthUnit.DtpPica:
                case LengthUnit.DtpPoint:
                case LengthUnit.Fathom:
                case LengthUnit.Mil:
                case LengthUnit.NauticalMile:
                case LengthUnit.PrinterPica:
                case LengthUnit.PrinterPoint:
                case LengthUnit.Shackle:
                case LengthUnit.Twip:
                case LengthUnit.UsSurveyFoot:
                    yield return LengthUnit.Meter;
                    yield return LengthUnit.Yard;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override bool TryParse(string split, out Length foundUnit)
        {
            return Length.TryParse(split, out foundUnit);
        }
    }
}
