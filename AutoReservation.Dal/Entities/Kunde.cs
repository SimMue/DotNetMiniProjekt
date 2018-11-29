using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AutoReservation.Dal.Entities
{
    public class Kunde : DbContext
    {
        public DateTime Geburtsdatum { get; set; }

        public int Id { get; set; }

        public string Nachname { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public string Vorname { get; set; }
    }

}
