using System.Collections.Generic;
using UnitsNet;

namespace Bibby.UnitConversion.Contracts
{
    public interface IConvertUnits
    {
        IEnumerable<(IQuantity unit, IQuantity converted)> ConvertUnits(string input);
    }
}
