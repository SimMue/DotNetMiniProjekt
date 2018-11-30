using System.Collections.Generic;
using AutoReservation.BusinessLayer.Exceptions;
using AutoReservation.Dal;

namespace AutoReservation.BusinessLayer
{
    public abstract class ManagerBase<TEntity>
    {
        public abstract List<TEntity> GetAll();
        public abstract TEntity GetById(int id);
        public abstract void Insert(TEntity entity);
        public abstract void Update(TEntity entity);
        public abstract void Delete(TEntity entity);

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