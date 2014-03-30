using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Borderlands2GoldendKeys.Models
{
    /// <summary>
    /// Quote from Borderlands's ClapTrap
    /// </summary>
    public class ClapTrapQuote
    {
        public int Id { get; set; }
        public string Quote { get; set; }

        public static List<ClapTrapQuote> GetBaseQuotes()
        {
            return new List<ClapTrapQuote>
            {
                new ClapTrapQuote{ Quote = "Wow! You're not dead?" },
                new ClapTrapQuote{ Quote = "Hey, check me out everybody! I'm dancin', I'm dancin'!" },
                new ClapTrapQuote{ Quote = "Unce! Unce! Unce! Unce! Ooo, oh check me out. Unce! Unce! Unce! Unce! Oh, come on get down." },
                new ClapTrapQuote{ Quote = "Yoo hoooooooooo!" },
                new ClapTrapQuote{ Quote = "I am the best robot. Yeah, yeah, yeah, I am the best robot. Ooh, ooh, here we go!" },
                new ClapTrapQuote{ Quote = "Hey! Over here! I'm over heere!" },
                new ClapTrapQuote{ Quote = "Still haven't found the Vault?" },
                new ClapTrapQuote{ Quote = "I'm over here! " },
                new ClapTrapQuote{ Quote = "Rrrrrgh...this isn't working!" },
                new ClapTrapQuote{ Quote = "Unce! Unce! I think I lost the beat... but, Unce! Unce!" },
                new ClapTrapQuote{ Quote = "Wanna hear a new dubstep song I wrote? Wub! Wub" },
                new ClapTrapQuote{ Quote = "Did you find the Vault yet?" },
                new ClapTrapQuote{ Quote = "Sure is lonely around here." },
                new ClapTrapQuote{ Quote = "I can see... the code" },
                new ClapTrapQuote{ Quote = "My servos... are seizing..." },
                new ClapTrapQuote{ Quote = "I'm detecting a motor unit malfunction... I can't move! I'm paralyzed with fear!" },
                new ClapTrapQuote{ Quote = "Please don't shoot me, please don't shoot me, please don't shoot me!" },
                new ClapTrapQuote{ Quote = "Turning off the optics... they can't see me..." },
                new ClapTrapQuote{ Quote = "The traveler will protect me. The traveler will protect me." },
                new ClapTrapQuote{ Quote = "Good as new, I think. Am I leaking?" },
                new ClapTrapQuote{ Quote = "Yeah? Well, hmph!" },
                new ClapTrapQuote{ Quote = "Good luck!" },
                new ClapTrapQuote{ Quote = "Oh, c'mon! Let's get down. C'mon everybody, I'm dancin'! I'm dancin'!" },
                new ClapTrapQuote{ Quote = "This place is filled with nothing but muderers and jerkbags, like that Hammerlock dude!" },
                new ClapTrapQuote{ Quote = "HAHAHA. I ASSCEND." },
                new ClapTrapQuote{ Quote = "Your ability to move short distances without dying will be Handsome Jack's downfall!" },
                new ClapTrapQuote{ Quote = "Together, we shall free Pandora! I will lead you into battle! I will destroy Handsome Jack with my bare hands! I will... Stairs!? NOOOOOOOOOOOOOOOOOOOOOOOOOO!" },
                new ClapTrapQuote{ Quote = "When I get out of here, some of you bitches are getting stabbed. Matter of fact, I'm going to set the stabbing record on this planet." },
                new ClapTrapQuote{ Quote = "Dr. Ned gave me the following awards this year; 'Most effective claptrap in life threatening situations', 'Hardest performer of mid '80s breakdance fighting', 'Master orator' and 'Best kisser'." },
            };
        }
    }
}