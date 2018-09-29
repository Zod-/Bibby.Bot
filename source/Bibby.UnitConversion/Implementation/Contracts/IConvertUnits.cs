using System.Collections.Generic;
using UnitsNet;

namespace Bibby.UnitConversion.Contracts
{
    public interface IConvertUnits
    {
        IEnumerable<(string unit, IQuantity converted)> ConvertUnits(string input);
    }
}
