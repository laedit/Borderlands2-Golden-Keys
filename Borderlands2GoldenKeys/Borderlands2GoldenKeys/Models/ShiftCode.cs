using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Borderlands2GoldenKeys.Models
{
    public class ShiftCode
    {
        public int Id { get; set; }

        [UIHint("ExpirationDate")]
        public DateTime? ExpirationDate { get; set; }

        public string Code { get; set; }

        public Platform Platform { get; set; }

        public ulong SourceStatusId { get; set; }

        public DateTime CreationDate { get; set; }
    }

    public enum Platform
    {
        [Display(Name = "PC / MAC")]
        PC_MAC,
        [Display(Name = "XBOX 360")]
        XBOX,
        [Display(Name = "Playstation 3")]
        PS3
    }
}