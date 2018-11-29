using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
                return context.Reservationen.ToList().Single(r => r.ReservationsNr == id);
            }
        }

        public override void Insert(Reservation reservation)
        {
            if (!DateRangeCheck(reservation))
            {
                throw new InvalidDateRangeException($"Hours is lesser than 24 Hours, date is {(reservation.Bis - reservation.Von).TotalHours}");
            }

            if (!CheckAvailability(reservation))
            {
                throw new AutoUnaviableException($"No Car available for this date");
            }

            using (AutoReservationContext context = new AutoReservationContext())
            {
                context.Entry(reservation).State = EntityState.Added;
                context.SaveChanges();
            }

        }

        public override void Update(Reservation reservation)
        {
            if (!DateRangeCheck(reservation))
            {
                throw new InvalidDateRangeException($"Hours is lesser than 24 Hours");
            }

            if (!CheckAvailability(reservation))
            {
                throw new AutoUnaviableException($"No Car available for this date");
            }

            using (AutoReservationContext context = new AutoReservationContext())
            {
                try
                {
                    context.Entry(reservation).State = EntityState.Modified;
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    throw CreateOptimisticConcurrencyException(context, reservation);
                }
            }
        }

        public override void Delete(Reservation reservation)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                context.Entry(reservation).State = EntityState.Deleted;
                context.SaveChanges();
            }

        }
        
        private bool DateRangeCheck(Reservation reservation)
        {
            return reservation != null && (reservation.Bis - reservation.Von).TotalHours >= 24;
        }

        private bool CheckAvailability(Reservation reservation)
        {
            return reservation != null && 
                   GetById(reservation.AutoId) != null && 
                   GetById(reservation.AutoId).Bis <= reservation.Von;
        }
    }
}