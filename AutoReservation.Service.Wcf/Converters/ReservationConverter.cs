using System.Collections.Generic;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Dal.Entities;

namespace AutoReservation.Service.Wcf.Converters
{
    class ReservationConverter : DtoEntityConverter<ReservationDto, Reservation>
    {
        private readonly DtoEntityConverter<AutoDto, Auto> _autoConverter;
        private readonly DtoEntityConverter<KundeDto, Kunde> _kundeConverter;

        public ReservationConverter(DtoEntityConverter<AutoDto, Auto> autoConverter, DtoEntityConverter<KundeDto, Kunde> kundeConverter)
        {
            _autoConverter = autoConverter;
            _kundeConverter = kundeConverter;
        }
        public override Reservation ConvertToEntity(ReservationDto dto)
        {
            if (dto == null) { return null; }

            Reservation reservation = new Reservation
            {
                ReservationsNr = dto.ReservationsNr,
                Von = dto.Von,
                Bis = dto.Bis,
                AutoId = dto.Auto.Id,
                KundeId = dto.Kunde.Id,
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
                Auto = _autoConverter.ConvertToDto(entity.Auto),
                Kunde = _kundeConverter.ConvertToDto(entity.Kunde)
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
