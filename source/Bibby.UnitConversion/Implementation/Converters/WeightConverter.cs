using System;
using System.Collections.Generic;
using UnitsNet;
using UnitsNet.Units;

namespace Bibby.UnitConversion.Converters
{
    public class MassConverter : BaseConverter<Mass>
    {
        protected override IEnumerable<(IQuantity unit, IQuantity converted)> ConvertUnits(IEnumerable<Mass> foundUnits)
        {
            foreach (var foundUnit in foundUnits)
            {
                foreach (var conversionUnit in ConversionUnitMapping(foundUnit.Unit))
                {
                    yield return (foundUnit, foundUnit.ToUnit(conversionUnit));
                }
            }
        }

        private IEnumerable<MassUnit> ConversionUnitMapping(MassUnit foundUnit)
        {
            switch (foundUnit)
            {
                case MassUnit.Undefined:
                case MassUnit.Centigram:
                case MassUnit.Decagram:
                case MassUnit.Decigram:
                case MassUnit.Microgram:
                case MassUnit.Nanogram:
                case MassUnit.Milligram:
                    yield return MassUnit.Gram;
                    yield return MassUnit.Ounce;
                    break;
                case MassUnit.Gram:
                    yield return MassUnit.Ounce;
                    break;
                case MassUnit.Ounce:
                    yield return MassUnit.Gram;
                    break;
                case MassUnit.ShortHundredweight:
                case MassUnit.LongHundredweight:
                case MassUnit.Pound:
                    yield return MassUnit.Kilogram;
                    break;
                case MassUnit.Kilogram:
                    yield return MassUnit.Pound;
                    break;
                case MassUnit.Stone:
                    yield return MassUnit.Kilogram;
                    yield return MassUnit.Pound;
                    break;
                case MassUnit.Hectogram:
                case MassUnit.Kilotonne:
                case MassUnit.Megatonne:
                case MassUnit.Tonne:
                    yield return MassUnit.ShortTon;
                    break;
                case MassUnit.ShortTon:
                case MassUnit.Megapound:
                case MassUnit.Kilopound:
                    yield return MassUnit.Tonne;
                    break;
                case MassUnit.LongTon:
                    yield return MassUnit.Tonne;
                    yield return MassUnit.ShortTon;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(foundUnit), foundUnit, null);
            }
        }

        protected override bool TryParse(string split, out Mass foundUnit)
        {
            return Mass.TryParse(split, out foundUnit);
        }
    }
}
