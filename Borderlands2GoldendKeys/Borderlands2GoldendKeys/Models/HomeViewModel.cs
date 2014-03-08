using System.Collections.Generic;

namespace Borderlands2GoldendKeys.Models
{
    public class HomeViewModel
    {
        public ClapTrapQuote ClapTrapQuote { get; set; }

        public List<ShiftCode> GoldenKeys { get; set; }

        public HomeViewModel()
        {
            GoldenKeys = new List<ShiftCode>();
        }
    }
}