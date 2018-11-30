using System;
using System.Collections.Generic;
using System.Diagnostics;
using AutoReservation.BusinessLayer;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using AutoReservation.Dal.Entities;
using AutoReservation.Service.Wcf.Converters;

namespace AutoReservation.Service.Wcf
{
    public class AutoReservationService<TDto, TEntity> : IAutoReservationService<TDto>
    {
        private readonly ManagerBase<TEntity> _manager;
        private readonly DtoEntityConverter<TDto, TEntity> _converter;

        public AutoReservationService(ManagerBase<TEntity> manager, DtoEntityConverter<TDto, TEntity> converter)
        {
            _manager = manager;
            _converter = converter;
        }

        private static void WriteActualMethod()
            => Console.WriteLine($"Calling: {new StackTrace().GetFrame(1).GetMethod().Name}-{typeof(TDto).Namespace}");

        public List<TDto> GetAll()
        {
            WriteActualMethod();
            List<TEntity> entities = _manager.GetAll();
            return _converter.ConvertToDtos(entities);
        }

        public TDto GetById(int id)
        {
            WriteActualMethod();
            TEntity entity = _manager.GetById(id);
            return _converter.ConvertToDto(entity);
        }

        public void Insert(TDto dto)
        {
            WriteActualMethod();
            TEntity entity = _converter.ConvertToEntity(dto);
            _manager.Insert(entity);
        }

        public void Update(TDto dto)
        {
            WriteActualMethod();
            TEntity entity = _converter.ConvertToEntity(dto);
            _manager.Update(entity);
        }

        public void Delete(TDto dto)
        {
            WriteActualMethod();
            TEntity entity = _converter.ConvertToEntity(dto);
            _manager.Delete(entity);
        }
    }
}