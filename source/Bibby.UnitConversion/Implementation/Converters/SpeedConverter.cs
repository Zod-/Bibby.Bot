using System;
using System.Collections.Generic;
using UnitsNet;
using UnitsNet.Units;

namespace Bibby.UnitConversion.Converters
{
    public class SpeedConverter : BaseConverter<Speed>
    {
        protected override IEnumerable<(string unit, IQuantity converted)> ConvertUnits(IEnumerable<(string input, Speed foundUnit)> foundUnits)
        {
            foreach (var (unit, foundUnit) in foundUnits)
            {
                foreach (var conversionUnit in ConversionUnitMapping(foundUnit.Unit))
                {
                    yield return (unit, foundUnit.ToUnit(conversionUnit));
                }
            }
        }

        private IEnumerable<SpeedUnit> ConversionUnitMapping(SpeedUnit foundUnitUnit)
        {
            switch (foundUnitUnit)
            {
                case SpeedUnit.Undefined:
                    break;
                case SpeedUnit.KilometerPerHour:
                    yield return SpeedUnit.MilePerHour;
                    break;
                case SpeedUnit.MilePerHour:
                    yield return SpeedUnit.KilometerPerHour;
                    break;
                case SpeedUnit.KilometerPerMinute:
                case SpeedUnit.CentimeterPerHour:
                case SpeedUnit.CentimeterPerMinute:
                case SpeedUnit.CentimeterPerSecond:
                case SpeedUnit.DecimeterPerMinute:
                case SpeedUnit.DecimeterPerSecond:
                case SpeedUnit.FootPerHour:
                case SpeedUnit.FootPerMinute:
                case SpeedUnit.FootPerSecond:
                case SpeedUnit.InchPerHour:
                case SpeedUnit.InchPerMinute:
                case SpeedUnit.InchPerSecond:
                case SpeedUnit.KilometerPerSecond:
                case SpeedUnit.Knot:
                case SpeedUnit.MeterPerHour:
                case SpeedUnit.MeterPerMinute:
                case SpeedUnit.MeterPerSecond:
                case SpeedUnit.MicrometerPerMinute:
                case SpeedUnit.MicrometerPerSecond:
                case SpeedUnit.MillimeterPerHour:
                case SpeedUnit.MillimeterPerMinute:
                case SpeedUnit.MillimeterPerSecond:
                case SpeedUnit.NanometerPerMinute:
                case SpeedUnit.NanometerPerSecond:
                case SpeedUnit.UsSurveyFootPerHour:
                case SpeedUnit.UsSurveyFootPerMinute:
                case SpeedUnit.UsSurveyFootPerSecond:
                case SpeedUnit.YardPerHour:
                case SpeedUnit.YardPerMinute:
                case SpeedUnit.YardPerSecond:
                    yield return SpeedUnit.KilometerPerHour;
                    yield return SpeedUnit.MilePerHour;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(foundUnitUnit), foundUnitUnit, null);
            }
        }

        protected override bool TryParse(string split, out Speed foundUnit)
        {
            return Speed.TryParse(split, out foundUnit);
        }
    }
}
