using System.Collections.Generic;
using AutoReservation.Common.DataTransferObjects;

namespace AutoReservation.Common.Interfaces
{
    public interface IAutoReservationService
    {
        List<AutoDto> GetAllAutos();

        List<KundeDto> GetAllKunden();

        List<ReservationDto> GetAllReservationen();

        AutoDto GetAutoById(int id);

        KundeDto GetKundeById(int id);

        ReservationDto GetReservationById(int id);

        void InsertAuto(AutoDto auto);

        void InsertKunde(KundeDto kunde);

        void InsertReservation(ReservationDto reservation);

        void UpdateAuto(AutoDto auto);

        void UpdateKunde(KundeDto kunde);

        void UpdateReservation(ReservationDto reservation);

        void DeleteAuto(AutoDto auto);

        void DeleteKunde(KundeDto kunde);

        void DeleteReservation(ReservationDto reservation);

        bool CheckAvailability(AutoDto auto);
    }
}
