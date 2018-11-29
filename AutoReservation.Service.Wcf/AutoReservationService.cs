using System;
using System.Collections.Generic;
using System.Diagnostics;
using AutoReservation.BusinessLayer;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;

namespace AutoReservation.Service.Wcf
{
    public class AutoReservationService : IAutoReservationService
    {
        private KundeManager kundeManager;

        private static void WriteActualMethod()
            => Console.WriteLine($"Calling: {new StackTrace().GetFrame(1).GetMethod().Name}");

        public List<AutoDto> GetAllAutos()
        {
            throw new NotImplementedException();
        }

        public List<KundeDto> GetAllKunden()
        {
            throw new NotImplementedException();
        }

        public List<ReservationDto> GetAllReservationen()
        {
            throw new NotImplementedException();
        }

        public AutoDto GetAutoById(int id)
        {
            throw new NotImplementedException();
        }

        public KundeDto GetKundeById(int id)
        {
            throw new NotImplementedException();
        }

        public ReservationDto GetReservationById(int id)
        {
            throw new NotImplementedException();
        }

        public void InsertAuto(AutoDto auto)
        {
            throw new NotImplementedException();
        }

        public void InsertKunde(KundeDto kunde)
        {
            throw new NotImplementedException();
        }

        public void InsertReservation(ReservationDto reservation)
        {
            throw new NotImplementedException();
        }

        public void UpdateAuto(AutoDto auto)
        {
            throw new NotImplementedException();
        }

        public void UpdateKunde(KundeDto kunde)
        {
            throw new NotImplementedException();
        }

        public void UpdateReservation(ReservationDto reservation)
        {
            throw new NotImplementedException();
        }

        public void DeleteAuto(AutoDto auto)
        {
            throw new NotImplementedException();
        }

        public void DeleteKunde(KundeDto kunde)
        {
            throw new NotImplementedException();
        }

        public void DeleteReservation(ReservationDto reservation)
        {
            throw new NotImplementedException();
        }

        public bool CheckAvailability(AutoDto auto)
        {
            throw new NotImplementedException();
        }
    }
}