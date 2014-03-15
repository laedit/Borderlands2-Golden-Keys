using System.Collections.Generic;

namespace Borderlands2GoldendKeys.Models
{
    public class HomeViewModel
    {
        public ClapTrapQuote ClapTrapQuote { get; set; }

        public List<ShiftCode> ShiftCodes { get; set; }

        public bool DisableShallAllButton { get; set; }

        public HomeViewModel()
        {
            ShiftCodes = new List<ShiftCode>();
        }
    }
}