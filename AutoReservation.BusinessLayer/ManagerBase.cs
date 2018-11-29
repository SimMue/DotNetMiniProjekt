using System.Collections.Generic;
using AutoReservation.BusinessLayer.Exceptions;
using AutoReservation.Dal;

namespace AutoReservation.BusinessLayer
{
    public abstract class ManagerBase<T>
    {
        public abstract List<T> GetAll();
        public abstract T GetById(int id);
        public abstract void Insert(T entry);
        public abstract void Update(T entry);
        public abstract void Delete(T entry);

        protected static OptimisticConcurrencyException<T> CreateOptimisticConcurrencyException<T>(AutoReservationContext context, T entity)
            where T : class
        {
            T dbEntity = (T)context.Entry(entity)
                .GetDatabaseValues()
                .ToObject();

            return new OptimisticConcurrencyException<T>($"Update {typeof(T).Name}: Concurrency-Fehler", dbEntity);
        }
    }
}