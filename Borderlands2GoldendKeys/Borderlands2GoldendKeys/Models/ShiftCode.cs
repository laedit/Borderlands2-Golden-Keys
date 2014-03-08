using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Borderlands2GoldendKeys.Models
{
    public class ShiftCode
    {
        public int Id { get; set; }

        [UIHint("ExpirationDate")]
        public DateTime? ExpirationDate { get; set; }

        public string Code { get; set; }

        public Platform Platform { get; set; }

        public ulong SourceStatusId { get; set; }

        public static List<ShiftCode> GetDummyData()
        {
            return new List<ShiftCode> {
                new ShiftCode { Code = "C3KJ3-WCZW3-H5TFK-BTB3T-9RKTZ", ExpirationDate = DateTime.Now.AddDays(1), Platform = Platform.PC_MAC, SourceStatusId = 440879761649180673 },
                new ShiftCode { Code = "C3KJ3-WCZW3-H5TFK-BTB3T-9RKTZ", ExpirationDate = new DateTime(2014, 02, 24), Platform = Platform.XBOX, SourceStatusId = 440879761649180673 },
                new ShiftCode { Code = "C3KJ3-WCZW3-H5TFK-BTB3T-9RKTZ", ExpirationDate = null, Platform = Platform.PS3, SourceStatusId = 440879761649180673 },
                 new ShiftCode { Code = "C3KJ3-WCZW3-H5TFK-BTB3T-9RKTZ", ExpirationDate = DateTime.Now.AddDays(1), Platform = Platform.PC_MAC, SourceStatusId = 440879761649180673 },
                new ShiftCode { Code = "C3KJ3-WCZW3-H5TFK-BTB3T-9RKTZ", ExpirationDate = new DateTime(2014, 02, 24), Platform = Platform.XBOX, SourceStatusId = 440879761649180673 },
                new ShiftCode { Code = "C3KJ3-WCZW3-H5TFK-BTB3T-9RKTZ", ExpirationDate = null, Platform = Platform.PS3, SourceStatusId = 440879761649180673 },
                 new ShiftCode { Code = "C3KJ3-WCZW3-H5TFK-BTB3T-9RKTZ", ExpirationDate = DateTime.Now.AddDays(1), Platform = Platform.PC_MAC, SourceStatusId = 440879761649180673 },
                new ShiftCode { Code = "C3KJ3-WCZW3-H5TFK-BTB3T-9RKTZ", ExpirationDate = new DateTime(2014, 02, 24), Platform = Platform.XBOX, SourceStatusId = 440879761649180673 },
                new ShiftCode { Code = "C3KJ3-WCZW3-H5TFK-BTB3T-9RKTZ", ExpirationDate = null, Platform = Platform.PS3, SourceStatusId = 440879761649180673 },
                 new ShiftCode { Code = "C3KJ3-WCZW3-H5TFK-BTB3T-9RKTZ", ExpirationDate = DateTime.Now.AddDays(1), Platform = Platform.PC_MAC, SourceStatusId = 440879761649180673 },
                new ShiftCode { Code = "C3KJ3-WCZW3-H5TFK-BTB3T-9RKTZ", ExpirationDate = new DateTime(2014, 02, 24), Platform = Platform.XBOX, SourceStatusId = 440879761649180673 },
                new ShiftCode { Code = "C3KJ3-WCZW3-H5TFK-BTB3T-9RKTZ", ExpirationDate = null, Platform = Platform.PS3, SourceStatusId = 440879761649180673 }
            };
        }
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