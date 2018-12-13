using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Dal.Entities;

namespace AutoReservation.Service.Wcf.Converters
{
    public class ReservationConverter : DtoEntityConverter<ReservationDto, Reservation>
    {
        public override Reservation ConvertToEntity(ReservationDto dto)
        {
            if (dto == null) { return null; }

            Reservation reservation = new Reservation
            {
                ReservationsNr = dto.ReservationsNr,
                Von = dto.Von,
                Bis = dto.Bis,
                AutoId = dto.AutoId,
                KundeId = dto.KundeId,
                RowVersion = dto.RowVersion
            };

            return reservation;
        }

        public override ReservationDto ConvertToDto(Reservation entity)
        {
            if (entity == null) { return null; }

            return new ReservationDto
            {
                ReservationsNr = entity.ReservationsNr,
                Von = entity.Von,
                Bis = entity.Bis,
                RowVersion = entity.RowVersion,
                AutoId = entity.AutoId,
                KundeId = entity.KundeId
            };
        }

        public override List<Reservation> ConvertToEntities(List<ReservationDto> dtos)
        {
            return ConvertGenericList(dtos, ConvertToEntity);
        }

        public override List<ReservationDto> ConvertToDtos(List<Reservation> entities)
        {
            return ConvertGenericList(entities, ConvertToDto);
        }
    }
}
