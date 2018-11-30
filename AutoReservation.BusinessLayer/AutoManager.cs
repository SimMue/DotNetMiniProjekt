using System;
using System.Collections.Generic;
using System.Linq;
using AutoReservation.Dal;
using AutoReservation.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoReservation.BusinessLayer
{
    public class AutoManager 
        : ManagerBase<Auto>
    {
        public override List<Auto> GetAll()
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                return context.Autos.ToList();
            }
        }

        public override Auto GetById(int id)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                return context.Autos.Single(a => a.Id == id);
            }
        }

        public override void Insert(Auto entity)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                context.Entry(entity).State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public override void Update(Auto entity)
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

        public override void Delete(Auto entity)
        {
            using (AutoReservationContext context = new AutoReservationContext())
            {
                context.Entry(entity).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}