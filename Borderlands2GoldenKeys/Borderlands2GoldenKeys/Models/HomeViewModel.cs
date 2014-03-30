using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Borderlands2GoldenKeys.Models
{
    public class HomeViewModel
    {
        public ClapTrapQuote ClapTrapQuote { get; set; }

        public bool DisableShallAllButton { get; set; }

        public bool EnableMail { get; set; }

        public Mail Mail { get; set; }

        public RowsViewModel Rows { get; set; }

        public HomeViewModel()
        {
            Rows = new RowsViewModel();
        }
    }

    public class RowsViewModel
    {
        public bool HasShiftCodes { get { return ShiftCodes.Count > 0; } }
        public List<ShiftCode> ShiftCodes { get; set; }
        public int StartIndex { get; set; }

        public RowsViewModel()
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