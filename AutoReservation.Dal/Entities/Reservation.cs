using System;
using System.ComponentModel.DataAnnotations;

namespace AutoReservation.Dal.Entities
{
    public class Reservation
    {
        public int ReservationsNr { get; set; }

        public Auto AutoId { get; set; }

        public Kunde KundenId { get; set; }

        public DateTime Von { get; set; }

        public DateTime Bis { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
