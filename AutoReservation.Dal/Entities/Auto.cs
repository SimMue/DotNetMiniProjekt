
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoReservation.Dal.Entities
{
    public abstract class Auto
    {
        public int Id { get; set; }

        [MaxLength(20), Required]
        public string Marke { get; set; }

        public int Tagestarif { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }

    public class StandardAuto : Auto { }

    public class LuxusklasseAuto : Auto
    {
        public int Basistarif { get; set; }
    }

    public class MittelklasseAuto : Auto { }
}
