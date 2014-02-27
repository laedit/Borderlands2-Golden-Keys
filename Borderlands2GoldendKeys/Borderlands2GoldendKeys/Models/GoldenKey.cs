using System;
using System.Collections.Generic;
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

        public static List<GoldenKey> GetDummyData()
        {
            return new List<GoldenKey> {
                new GoldenKey { Key = "C3KJ3-WCZW3-H5TFK-BTB3T-9RKTZ", ExpirationDate = DateTime.Now.AddDays(1), Platform = Platform.PC_MAC },
                new GoldenKey { Key = "C3KJ3-WCZW3-H5TFK-BTB3T-9RKTZ", ExpirationDate = new DateTime(2014, 02, 24), Platform = Platform.XBOX },
                new GoldenKey { Key = "C3KJ3-WCZW3-H5TFK-BTB3T-9RKTZ", ExpirationDate = null, Platform = Platform.PS3 },
                 new GoldenKey { Key = "C3KJ3-WCZW3-H5TFK-BTB3T-9RKTZ", ExpirationDate = DateTime.Now.AddDays(1), Platform = Platform.PC_MAC },
                new GoldenKey { Key = "C3KJ3-WCZW3-H5TFK-BTB3T-9RKTZ", ExpirationDate = new DateTime(2014, 02, 24), Platform = Platform.XBOX },
                new GoldenKey { Key = "C3KJ3-WCZW3-H5TFK-BTB3T-9RKTZ", ExpirationDate = null, Platform = Platform.PS3 },
                 new GoldenKey { Key = "C3KJ3-WCZW3-H5TFK-BTB3T-9RKTZ", ExpirationDate = DateTime.Now.AddDays(1), Platform = Platform.PC_MAC },
                new GoldenKey { Key = "C3KJ3-WCZW3-H5TFK-BTB3T-9RKTZ", ExpirationDate = new DateTime(2014, 02, 24), Platform = Platform.XBOX },
                new GoldenKey { Key = "C3KJ3-WCZW3-H5TFK-BTB3T-9RKTZ", ExpirationDate = null, Platform = Platform.PS3 },
                 new GoldenKey { Key = "C3KJ3-WCZW3-H5TFK-BTB3T-9RKTZ", ExpirationDate = DateTime.Now.AddDays(1), Platform = Platform.PC_MAC },
                new GoldenKey { Key = "C3KJ3-WCZW3-H5TFK-BTB3T-9RKTZ", ExpirationDate = new DateTime(2014, 02, 24), Platform = Platform.XBOX },
                new GoldenKey { Key = "C3KJ3-WCZW3-H5TFK-BTB3T-9RKTZ", ExpirationDate = null, Platform = Platform.PS3 }
            };
        }
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