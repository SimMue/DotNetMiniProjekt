using System;
using AutoReservation.BusinessLayer.Exceptions;
using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.BusinessLayer.Testing
{
    public class ReservationDateRangeTest
        : TestBase
    {
        private ReservationManager _target;
        private ReservationManager Target => _target ?? (_target = new ReservationManager());

        [Fact]
        public void MinimumDateRangeTest()
        {
            Reservation newReservation = new Reservation
            {
                AutoId = 3,
                KundeId = 1,
                Von = new DateTime(2020, 05, 19),
                Bis = new DateTime(2020, 05, 20)
            };
            Target.Insert(newReservation);

            Assert.NotNull(Target.GetById(5));
        }

        [Fact]
        //Annahme: max. 10 Tage
        public void MaximumDateRangeTest()
        {
            Reservation newReservation = new Reservation
            {
                AutoId = 3,
                KundeId = 2,
                Von = new DateTime(2020, 06, 03),
                Bis = new DateTime(2020, 06, 13)
            };
            Target.Insert(newReservation);

            Assert.NotNull(Target.GetById(5));
        }

        [Fact]
        public void DataRangeToShortTest()
        {
            Reservation reservation = new Reservation
            {
                AutoId = 3,
                KundeId = 2,
                Von = new DateTime(2020, 06, 23, 0, 0, 0),
                Bis = new DateTime(2020, 06, 23, 3, 0, 0)
            };

            Assert.Throws<InvalidDateRangeException>(() => Target.Insert(reservation));
        }

        [Fact]
        public void DateOrderWrongTest()
        {
            Reservation reservation = new Reservation
            {
                AutoId = 1,
                KundeId = 1,
                Von = new DateTime(2020, 06, 23),
                Bis = new DateTime(2020, 06, 22)
            };

            Assert.Throws<InvalidDateRangeException>(() => Target.Insert(reservation));

        }

        [Fact]
        public void DateOrderWrongAndDateRangeToShortTest()
        {
            Reservation reservation = new Reservation
            {
                AutoId = 1,
                KundeId = 1,
                Von = new DateTime(2020, 06, 23, 3, 0, 0),
                Bis = new DateTime(2020, 06, 23, 0, 0, 0)
            };

            Assert.Throws<InvalidDateRangeException>(() => Target.Insert(reservation));
        }
    }
}
