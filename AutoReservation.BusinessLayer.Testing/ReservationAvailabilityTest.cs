using System;
using AutoReservation.BusinessLayer.Exceptions;
using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.BusinessLayer.Testing
{
    public class ReservationAvailabilityTest : TestBase
    {
        private ReservationManager target;
        private ReservationManager Target => target ?? (target = new ReservationManager());

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
            Target.Insert(reservation);

            Assert.NotNull(Target.GetById(5));
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
            Reservation changedReservation = Target.GetById(1);
            changedReservation.Von = new DateTime(2020, 01, 12);
            changedReservation.Bis = new DateTime(2020, 01, 19);

            Assert.Throws<AutoUnavailableException>(() => Target.Update(changedReservation));
        }

        [Fact]
        public void CarNotAvailableDeletedTest()
        {
            Reservation reservation = Target.GetById(1);
            Target.Delete(reservation);

            Assert.Throws<InvalidOperationException>(() => Target.Update(reservation));
        }

    }
}
