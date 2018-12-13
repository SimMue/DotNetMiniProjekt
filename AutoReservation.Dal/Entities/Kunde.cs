using System;
using System.ComponentModel.DataAnnotations;

namespace AutoReservation.Dal.Entities
{
    public class Kunde
    {
        public int Id { get; set; }

        [MaxLength(20), Required]
        public string Nachname { get; set; }

        [MaxLength(20), Required]
        public string Vorname { get; set; }

        public DateTime Geburtsdatum { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
