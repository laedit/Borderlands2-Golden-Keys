using System.Collections.Generic;

namespace Borderlands2GoldendKeys.Models
{
    public class HomeViewModel
    {
        public ClapTrapQuote ClapTrapQuote { get; set; }

        public List<GoldenKey> GoldenKeys { get; set; }

        public HomeViewModel()
        {
            GoldenKeys = new List<GoldenKey>();
        }
    }
}