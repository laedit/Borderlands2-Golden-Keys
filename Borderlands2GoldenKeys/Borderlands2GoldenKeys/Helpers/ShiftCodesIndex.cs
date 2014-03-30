using Borderlands2GoldenKeys.Models;
using Raven.Client.Indexes;
using System.Linq;

namespace Borderlands2GoldenKeys.Helpers
{
    public class ShiftCodesIndex : AbstractIndexCreationTask<ShiftCode>
    {
        public ShiftCodesIndex()
        {
            Map = shiftCodes => shiftCodes.Select(entity => new { });
        }
    }
}