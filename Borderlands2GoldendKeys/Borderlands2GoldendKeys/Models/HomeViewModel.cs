﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Borderlands2GoldendKeys.Models
{
    public class HomeViewModel
    {
        public ClapTrapQuote ClapTrapQuote { get; set; }

        public List<ShiftCode> ShiftCodes { get; set; }

        public bool DisableShallAllButton { get; set; }

        public bool EnableMail { get; set; }

        public Mail Mail { get; set; }

        public HomeViewModel()
        {
            ShiftCodes = new List<ShiftCode>();
        }
    }

    public class Mail
    {
        [Required, Display(Name = "Mail from")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9-]+)*\\.([a-z]{2,6})$", ErrorMessage = "Not a valid email address")]
        public string MailFrom { get; set; }
        [Required]
        [MinLength(10)]
        public string Message { get; set; }
    }
}