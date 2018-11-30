using System.Collections.Generic;
using AutoReservation.Common.DataTransferObjects;

namespace AutoReservation.Common.Interfaces
{
    public interface IAutoReservationService<TDto>
    {
        List<TDto> GetAll();

        TDto GetById(int id);

        void Insert(TDto dto);

        void Update(TDto dto);

        void Delete(TDto dto);
    }
}
