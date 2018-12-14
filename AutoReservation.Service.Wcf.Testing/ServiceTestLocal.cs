using AutoReservation.BusinessLayer;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using AutoReservation.Dal.Entities;
using AutoReservation.Service.Wcf.Converters;

namespace AutoReservation.Service.Wcf.Testing
{
    public class ServiceTestLocal 
        : ServiceTestBase
    {
        private IAutoReservationService<AutoDto> _autoTarget;
        private IAutoReservationService<KundeDto> _kundeTarget;
        private IAutoReservationService<ReservationDto> _reservationTarget;

        protected override IAutoReservationService<AutoDto> AutoTarget =>
            _autoTarget ?? (_autoTarget = new AutoReservationService<AutoDto, Auto>(new AutoManager(), new AutoConverter()));

        protected override IAutoReservationService<KundeDto> KundeTarget =>
            _kundeTarget ?? (_kundeTarget = new AutoReservationService<KundeDto, Kunde>(new KundeManager(), new KundeConverter()));

        protected override IAutoReservationService<ReservationDto> ReservationTarget =>
            _reservationTarget ?? (_reservationTarget = new ReservationService(new ReservationManager(), new ReservationConverter()));
    }
}