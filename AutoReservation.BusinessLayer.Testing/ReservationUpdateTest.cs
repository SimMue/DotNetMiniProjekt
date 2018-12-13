using System;
using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.BusinessLayer.Testing
{
    public class ReservationUpdateTest
        : TestBase
    {
        private ReservationManager target;
        private ReservationManager Target => target ?? (target = new ReservationManager());

        [Fact]
        public void UpdateReservationTest()
        {
            Reservation changedReservation = Target.GetById(1);
            changedReservation.AutoId = 2;
            changedReservation.KundeId = 2;
            changedReservation.Von = new DateTime(2020, 01, 21);
            changedReservation.Bis = new DateTime(2020, 01, 23);

            target.Update(changedReservation);

            Reservation dbReservation = Target.GetById(1);

            Assert.Equal(changedReservation.AutoId, dbReservation.AutoId);
            Assert.Equal(changedReservation.KundeId, dbReservation.KundeId);
            Assert.Equal(changedReservation.Von, dbReservation.Von);
            Assert.Equal(changedReservation.Bis, dbReservation.Bis);
            Assert.Equal(changedReservation.RowVersion, dbReservation.RowVersion);
        }

       
    }
}
