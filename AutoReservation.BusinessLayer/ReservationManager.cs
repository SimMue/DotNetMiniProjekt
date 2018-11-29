using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
            using (AutoReservationContext context = new AutoReservationContext())
            {
                // Insert
                context.Entry(reservation).State = EntityState.Added;
                context.SaveChanges();
            }

        }

        public override void Update(Reservation reservation)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                try
                {
                    // Update
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
                // Delete
                context.Entry(reservation).State = EntityState.Deleted;
                context.SaveChanges();
            }

        }
    }
}