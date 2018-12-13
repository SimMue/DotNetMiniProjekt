using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using AutoReservation.BusinessLayer.Exceptions;
using AutoReservation.Dal;
using AutoReservation.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoReservation.BusinessLayer
{
    public class ReservationManager
        : ManagerBase<Reservation>
    {
        public override List<Reservation> GetAll()
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                return context.Reservationen.ToList();
            }
        }

        public override Reservation GetById(int id)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                var reservationen = context.Reservationen.ToList();
                return reservationen.Find(r => r.ReservationsNr == id);
            }
        }

        public Reservation GetByAutoId(int id)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                List<Reservation>  reservationList = context.Reservationen.ToList();
                foreach (Reservation reservation in reservationList)
                {
                    if (reservation.AutoId == id)
                    {
                        return reservation;
                    }
                }
                return null;
            }
        }

        public override void Insert(Reservation entity)
        {
            if (!DateRangeCheck(entity))
            {
                throw new InvalidDateRangeException($"Hours is lesser than 24 Hours, date is {(entity.Bis - entity.Von).TotalHours}");
            }

            if (!CheckAvailability(entity))
            {
                throw new AutoUnavailableException($"No Car available for this date");
            }

            using (AutoReservationContext context = new AutoReservationContext())
            {
                context.Entry(entity).State = EntityState.Added;
                context.SaveChanges();
            }

        }

        public override void Update(Reservation entity)
        {
            if (!DateRangeCheck(entity))
            {
                throw new InvalidDateRangeException($"Hours is lesser than 24 Hours");
            }

            if (!CheckAvailability(entity))
            {
                throw new AutoUnavailableException($"No Car available for this date");
            }

            using (AutoReservationContext context = new AutoReservationContext())
            {
                try
                {
                    context.Entry(entity).State = EntityState.Modified;
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    throw CreateOptimisticConcurrencyException(context, entity);
                }
            }
        }

        public override void Delete(Reservation entity)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                context.Entry(entity).State = EntityState.Deleted;
                context.SaveChanges();
            }

        }
        
        private bool DateRangeCheck(Reservation reservation)
        {
            return reservation != null && (reservation.Bis - reservation.Von).TotalHours >= 24;
        }

        public bool CheckAvailability(Reservation reservation)
        {
            if (reservation == null)
            {
                return false;
            }

            Reservation validReservation = GetByAutoId(reservation.AutoId);
            return validReservation != null && validReservation.Bis <= reservation.Von;
        }
    }
}