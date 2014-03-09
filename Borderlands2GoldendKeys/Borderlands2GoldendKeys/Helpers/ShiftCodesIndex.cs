using Borderlands2GoldendKeys.Models;
using Raven.Client.Indexes;
using System.Linq;

namespace Borderlands2GoldendKeys.Helpers
{
    public class ShiftCodesIndex : AbstractIndexCreationTask<ShiftCode>
    {
        public ShiftCodesIndex()
        {
            Map = shiftCodes => shiftCodes.Select(entity => new { });
        }
    }
}