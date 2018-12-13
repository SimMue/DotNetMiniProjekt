using System;

namespace AutoReservation.Common.DataTransferObjects
{
    public class ReservationDto
    {
        public int ReservationsNr { get; set; }

        public int AutoId { get; set; }

        public int KundeId { get; set; }

        public DateTime Von { get; set; }

        public DateTime Bis { get; set; }

        public byte[] RowVersion { get; set; }

        public override string ToString()
            => $"{ReservationsNr}; {Von}; {Bis}; {AutoId}; {KundeId}";
    }
}
