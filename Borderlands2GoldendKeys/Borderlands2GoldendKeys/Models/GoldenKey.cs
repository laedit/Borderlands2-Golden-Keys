using System;

namespace Borderlands2GoldendKeys.Models
{
    public class GoldenKey
    {
        public int Id { get; set; }
        public DateTime? PerishDate { get; set; }
        public string Key { get; set; }
        public Platform Platform { get; set; }
    }

    public enum Platform
    {
        PC_MAC,
        XBOX,
        PS3
    }
}