using System;
using System.Collections.Generic;
using Bibby.UnitConversion.Contracts;
using UnitsNet;

namespace Bibby.UnitConversion.Converters
{
    public abstract class BaseConverter<TUnit> : IConvertUnits where TUnit : IQuantity
    {
        public IEnumerable<(IQuantity unit, IQuantity converted)> ConvertUnits(string input)
        {
            var foundUnits = FindUnits(input);
            var convertedUnits = ConvertUnits(foundUnits);
            return convertedUnits;
        }

        protected abstract IEnumerable<(IQuantity unit, IQuantity converted)> ConvertUnits(IEnumerable<TUnit> foundUnits);


        protected abstract bool TryParse(string split, out TUnit foundUnit);

        private IEnumerable<TUnit> FindUnits(string input)
        {
            var splits = input.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
            var lastSplit = string.Empty;
            foreach (var split in splits)
            {
                if (TryParse(split, out var found))
                {
                    yield return found;
                }
                else if (TryParse($"{lastSplit} {split}", out found))
                {
                    yield return found;
                }
                lastSplit = split;
            }
        }
    }
}
