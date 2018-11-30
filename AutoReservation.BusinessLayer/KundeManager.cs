using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using AutoReservation.Dal;
using AutoReservation.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoReservation.BusinessLayer
{
    public class KundeManager
        : ManagerBase<Kunde>
    {
        public override List<Kunde> GetAll()
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                return context.Kunden.ToList();
            }
        }

        public override Kunde GetById(int id)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                return context.Kunden.ToList().Single(k => k.Id == id);
            }
        }

        public override void Insert(Kunde entity)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                context.Entry(entity).State = EntityState.Added;
                context.SaveChanges();
            }

        }

        public override void Update(Kunde entity)
        {

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

        public override void Delete(Kunde entity)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                context.Entry(entity).State = EntityState.Deleted;
                context.SaveChanges();
            }

        }

    }
}