using System;
using System.ComponentModel.DataAnnotations;

namespace Borderlands2GoldendKeys.Models
{
    public class GoldenKey
    {
        public int Id { get; set; }

        [UIHint("ExpirationDate")]
        public DateTime? ExpirationDate { get; set; }

        public string Key { get; set; }

        public Platform Platform { get; set; }
    }

    public enum Platform
    {
        [Display(Name = "PC / MAC")]
        PC_MAC,
        [Display(Name = "XBOX 360")]
        XBOX,
        PS3
    }
}