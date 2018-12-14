using System;
using AutoReservation.BusinessLayer.Exceptions;
using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.BusinessLayer.Testing
{
    public class ReservationAvailabilityTest : TestBase
    {
        private ReservationManager _target;
        private ReservationManager Target => _target ?? (_target = new ReservationManager());

        [Fact]
        public void CarAvailabilityTest()
        {
            Reservation reservation = new Reservation
            {
                AutoId = 3,
                KundeId = 3,
                Von = new DateTime(2020, 02, 10),
                Bis = new DateTime(2020, 02, 26)
            };

            Assert.True(Target.CheckAvailability(reservation));
        }

        [Fact]
        public void CarSeamlesslyAvailableTest()
        {
            Reservation reservation = new Reservation
            {
                AutoId = 3,
                KundeId = 3,
                Von = new DateTime(2020, 01, 20, 0, 0, 1),
                Bis = new DateTime(2020, 02, 26)
            };
            Target.Insert(reservation);

            Assert.NotNull(Target.GetById(5));
        }
        
        [Fact]
        public void CarNotAvailableInsertTest()
        {
            Reservation reservation = new Reservation
            {
                AutoId = 3,
                KundeId = 3,
                Von = new DateTime(2020, 01, 12),
                Bis = new DateTime(2020, 01, 19)
            };

            Assert.Throws<AutoUnavailableException>(() => Target.Insert(reservation));
        }

        [Fact]
        public void CarNotAvailableUpdateTest()
        {
            Reservation reservation = new Reservation
            {
                AutoId = 3,
                KundeId = 3,
                Von = new DateTime(2020, 01, 10),
                Bis = new DateTime(2020, 01, 20)
            };

            Assert.Throws<AutoUnavailableException>(() => Target.Insert(reservation));
        }
    }
}
