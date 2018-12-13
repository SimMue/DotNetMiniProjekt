using System;
using System.ServiceModel;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.DataTransferObjects.Faults;
using AutoReservation.Common.Interfaces;
using AutoReservation.Dal.Entities;
using AutoReservation.Service.Wcf.Converters;
using AutoReservation.TestEnvironment;
using Xunit;

namespace AutoReservation.Service.Wcf.Testing
{
    public abstract class ServiceTestBase
        : TestBase
    {
        protected abstract IAutoReservationService<AutoDto> AutoTarget { get; }
        protected abstract IAutoReservationService<KundeDto> KundeTarget { get; }
        protected abstract IAutoReservationService<ReservationDto> ReservationTarget { get; }

        #region Read all entities

        [Fact]
        public void GetAutosTest()
        {
            Assert.Equal(4, AutoTarget.GetAll().Count);
        }

        [Fact]
        public void GetKundenTest()
        {
            Assert.Equal(4, KundeTarget.GetAll().Count);
        }

        [Fact]
        public void GetReservationenTest()
        {
            Assert.Equal(4, ReservationTarget.GetAll().Count);
        }

        #endregion

        #region Get by existing ID

        [Fact]
        public void GetAutoByIdTest()
        {
            Assert.Equal("Fiat Punto", AutoTarget.GetById(1).Marke);
        }

        [Fact]
        public void GetKundeByIdTest()
        {
            Assert.Equal("Nass", KundeTarget.GetById(1).Nachname);
        }

        [Fact]
        public void GetReservationByNrTest()
        {
            Assert.Equal(1, ReservationTarget.GetById(1).KundeId);
        }

        #endregion

        #region Get by not existing ID

        [Fact]
        public void GetAutoByIdWithIllegalIdTest()
        {
            Assert.Throws<FaultException<UnknownFault>>(() => AutoTarget.GetById(100));
        }

        [Fact]
        public void GetKundeByIdWithIllegalIdTest()
        {
            Assert.Throws<FaultException<UnknownFault>>(() => KundeTarget.GetById(100));
        }

        [Fact]
        public void GetReservationByNrWithIllegalIdTest()
        {
            Assert.Throws<FaultException<UnknownFault>>(() => ReservationTarget.GetById(100));
        }

        #endregion

        #region Insert

        [Fact]
        public void InsertAutoTest()
        {
            AutoDto autoDto = new AutoDto
            {
                AutoKlasse = (int) AutoKlasse.Luxusklasse,
                Basistarif = 50,
                Marke = "Audi C3"
            };

            AutoTarget.Insert(autoDto);
        }

        [Fact]
        public void InsertKundeTest()
        {
            KundeDto kundeDto = new KundeDto
            {
                Geburtsdatum = new DateTime(),
                Nachname = "Pong",
                Vorname = "Lenis"
            };

            KundeTarget.Insert(kundeDto);
        }

        [Fact]
        public void InsertReservationTest()
        {
            ReservationDto reservationDto = new ReservationDto
            {
                Von = new DateTime(2021, 2, 23),
                Bis = new DateTime(2021, 2, 25)
            };

            ReservationTarget.Insert(reservationDto);
        }

        #endregion

        #region Delete  

        [Fact]
        public void DeleteAutoTest()
        {
            var autoDto = AutoTarget.GetById(1);
            AutoTarget.Delete(autoDto);
        }

        [Fact]
        public void DeleteKundeTest()
        {
            var kundeDto = KundeTarget.GetById(1);
            KundeTarget.Delete(kundeDto);
        }

        [Fact]
        public void DeleteReservationTest()
        {
            var reservationDto = ReservationTarget.GetById(1);
            ReservationTarget.Delete(reservationDto);
        }

        #endregion

        #region Update

        [Fact]
        public void UpdateAutoTest()
        {
            var autoDto = AutoTarget.GetById(2);
            autoDto.Marke = "Jugo";
            AutoTarget.Update(autoDto);
        }

        [Fact]
        public void UpdateKundeTest()
        {
            var kundeDto = KundeTarget.GetById(2);
            kundeDto.Nachname = "Norris";
            KundeTarget.Update(kundeDto);
        }

        [Fact]
        public void UpdateReservationTest()
        {
            var reservationTarget = ReservationTarget.GetById(2);
            reservationTarget.Bis = DateTime.Now;
            ReservationTarget.Update(reservationTarget);
        }

        #endregion

        #region Update with optimistic concurrency violation

        [Fact]
        public void UpdateAutoWithOptimisticConcurrencyTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void UpdateKundeWithOptimisticConcurrencyTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void UpdateReservationWithOptimisticConcurrencyTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        #endregion

        #region Insert / update invalid time range

        [Fact]
        public void InsertReservationWithInvalidDateRangeTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void InsertReservationWithAutoNotAvailableTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void UpdateReservationWithInvalidDateRangeTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void UpdateReservationWithAutoNotAvailableTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        #endregion

        #region Check Availability

        [Fact]
        public void CheckAvailabilityIsTrueTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        [Fact]
        public void CheckAvailabilityIsFalseTest()
        {
            throw new NotImplementedException("Test not implemented.");
        }

        #endregion
    }
}
