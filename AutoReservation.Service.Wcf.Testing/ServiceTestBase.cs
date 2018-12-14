using System;
using System.ServiceModel;
using System.Threading.Tasks;
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
            var autoDto = new AutoDto
            {
                AutoKlasse = (int) AutoKlasse.Luxusklasse,
                Basistarif = 50,
                Marke = "Audi C3"
            };

            AutoTarget.Insert(autoDto);
            var autoDtoInserted = AutoTarget.GetById(5);
            Assert.Equal(50 , autoDtoInserted.Basistarif);
        }

        [Fact]
        public void InsertKundeTest()
        {
            var kundeDto = new KundeDto
            {
                Geburtsdatum = new DateTime(),
                Vorname = "Pong",
                Nachname = "Lenis"
                
            };

            KundeTarget.Insert(kundeDto);
            var kundeDtoInserted = KundeTarget.GetById(5);
            Assert.Equal("Pong", kundeDtoInserted.Vorname);
        }

        [Fact]
        public void InsertReservationTest()
        {
            var reservationDto = new ReservationDto
            {
                AutoId = 1,
                KundeId = 1,
                Von = new DateTime(2021, 2, 23),
                Bis = new DateTime(2021, 2, 25)
            };

            ReservationTarget.Insert(reservationDto);
            var reservationDtoInserted = ReservationTarget.GetById(5);
            Assert.Equal(new DateTime(2021, 2, 23), reservationDtoInserted.Von);
        }

        #endregion

        #region Delete  

        [Fact]
        public void DeleteAutoTest()
        {
            var autoDto = AutoTarget.GetById(1);
            AutoTarget.Delete(autoDto);
            Assert.Throws<FaultException<UnknownFault>>(() => AutoTarget.GetById(1));
        }

        [Fact]
        public void DeleteKundeTest()
        {
            var kundeDto = KundeTarget.GetById(1);
            KundeTarget.Delete(kundeDto);
            Assert.Throws<FaultException<UnknownFault>>(() => KundeTarget.GetById(1));
        }

        [Fact]
        public void DeleteReservationTest()
        {
            var reservationDto = ReservationTarget.GetById(1);
            ReservationTarget.Delete(reservationDto);
            Assert.Throws<FaultException<UnknownFault>>(() => ReservationTarget.GetById(1));
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
            var reservationDto = ReservationTarget.GetById(2);
            reservationDto.Von = new DateTime(2050, 1, 09);
            reservationDto.Bis = new DateTime(2050, 1, 20);
            ReservationTarget.Update(reservationDto);
        }

        #endregion

        #region Update with optimistic concurrency violation

        [Fact]
        public void UpdateAutoWithOptimisticConcurrencyTest()
        {
            var autoDto = AutoTarget.GetById(2);
            autoDto.Marke = "Jugo";
            AutoTarget.Update(autoDto);
            autoDto.Marke = "VW";
            Assert.Throws<FaultException<OptimisticConcurrencyFault>>(() => AutoTarget.Update(autoDto));
        }

        [Fact]
        public void UpdateKundeWithOptimisticConcurrencyTest()
        {
            var kundeDto = KundeTarget.GetById(2);
            kundeDto.Nachname = "Norris";
            KundeTarget.Update(kundeDto);
            kundeDto.Nachname = "Cena";
            Assert.Throws<FaultException<OptimisticConcurrencyFault>>(() => KundeTarget.Update(kundeDto));
        }

        [Fact]
        public void UpdateReservationWithOptimisticConcurrencyTest()
        {
            var reservationDto = ReservationTarget.GetById(2);
            reservationDto.Von = new DateTime(2099, 1, 09);
            reservationDto.Bis = new DateTime(2099, 1, 20);
            ReservationTarget.Update(reservationDto);
            reservationDto.Von = new DateTime(2098, 1, 09);
            reservationDto.Bis = new DateTime(2098, 1, 20);
            Assert.Throws<FaultException<OptimisticConcurrencyFault>>(() => ReservationTarget.Update(reservationDto));
        }

        #endregion

        #region Insert / update invalid time range

        [Fact]
        public void InsertReservationWithInvalidDateRangeTest()
        {
            var reservationDto = new ReservationDto
            {
                AutoId = 1,
                KundeId = 1,
                Von = new DateTime(2021, 2, 26),
                Bis = new DateTime(2021, 2, 25)
            };

            Assert.Throws<FaultException<InvalidDateRangeFault>>(() => ReservationTarget.Insert(reservationDto));
        }

        [Fact]
        public void InsertReservationWithAutoNotAvailableTest()
        {
            var reservationDto = new ReservationDto
            {
                AutoId = 1,
                KundeId = 1,
                Von = new DateTime(2020, 1, 15),
                Bis = new DateTime(2020, 1, 20)
            };

            Assert.Throws<FaultException<AutoUnavailableFault>>(() => ReservationTarget.Insert(reservationDto));
        }

        [Fact]
        public void UpdateReservationWithInvalidDateRangeTest()
        {
            var reservationDto = ReservationTarget.GetById(1);
            reservationDto.Von = new DateTime(2020, 1, 15);
            reservationDto.Bis = new DateTime(2020, 1, 11);
            Assert.Throws<FaultException<InvalidDateRangeFault>>(() => ReservationTarget.Update(reservationDto));
        }

        [Fact]
        public void UpdateReservationWithAutoNotAvailableTest()
        {
            var reservationDto = ReservationTarget.GetById(1);
            reservationDto.AutoId = 2;
            Assert.Throws<FaultException<AutoUnavailableFault>>(() => ReservationTarget.Update(reservationDto));
        }

        #endregion

        #region Check Availability

        [Fact]
        public void CheckAvailabilityIsTrueTest()
        {
            //Von = new DateTime(2020, 01, 10), Bis = new DateTime(2020, 01, 20)
            var reservationDto = new ReservationDto
            {
                AutoId = 1,
                KundeId = 1,
                Von = new DateTime(2020, 1, 21),
                Bis = new DateTime(2020, 1, 25)
            };
            var service = (IReservableService<ReservationDto>) ReservationTarget;
            Assert.True(service.CheckAvailability(reservationDto));
        }

        [Fact]
        public void CheckAvailabilityIsFalseTest()
        {
            var reservationDto = new ReservationDto
            {
                AutoId = 1,
                KundeId = 1,
                Von = new DateTime(2020, 1, 15),
                Bis = new DateTime(2020, 1, 25)
            };
            var service = (IReservableService<ReservationDto>)ReservationTarget;
            Assert.False(service.CheckAvailability(reservationDto));
        }

        #endregion
    }
}
