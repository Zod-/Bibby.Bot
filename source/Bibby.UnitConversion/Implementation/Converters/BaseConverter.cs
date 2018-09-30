using System;
using System.Collections.Generic;
using Bibby.UnitConversion.Contracts;
using UnitsNet;

namespace Bibby.UnitConversion.Converters
{
    public abstract class BaseConverter<TUnit> : IConvertUnits where TUnit : IQuantity
    {
        public IEnumerable<(string unit, IQuantity converted)> ConvertUnits(string input)
        {
            var foundUnits = FindUnits(input);
            var convertedUnits = ConvertUnits(foundUnits);
            return convertedUnits;
        }

        protected abstract IEnumerable<(string unit, IQuantity converted)> ConvertUnits(IEnumerable<(string input, TUnit foundUnit)> foundUnits);


        protected abstract bool TryParse(string split, out TUnit foundUnit);

        private IEnumerable<(string unit, TUnit foundUnit)> FindUnits(string input)
        {
            var splits = input.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);

            foreach (var split in splits)
            {
                if (TryParse(split, out var found))
                {
                    yield return (split, found);
                }
            }
        }
    }
}
