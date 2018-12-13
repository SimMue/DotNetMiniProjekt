using System.Collections.Generic;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Dal.Entities;

namespace AutoReservation.Service.Wcf.Converters
{
    class KundeConverter : DtoEntityConverter<KundeDto, Kunde>
    {
        public override Kunde ConvertToEntity(KundeDto dto)
        {
            if (dto == null) { return null; }

            return new Kunde
            {
                Id = dto.Id,
                Nachname = dto.Nachname,
                Vorname = dto.Vorname,
                Geburtsdatum = dto.Geburtsdatum,
                RowVersion = dto.RowVersion
            };
        }

        public override KundeDto ConvertToDto(Kunde entity)
        {
            if (entity == null) { return null; }

            return new KundeDto
            {
                Id = entity.Id,
                Nachname = entity.Nachname,
                Vorname = entity.Vorname,
                Geburtsdatum = entity.Geburtsdatum,
                RowVersion = entity.RowVersion
            };
        }

        public override List<Kunde> ConvertToEntities(List<KundeDto> dtos)
        {
            return ConvertGenericList(dtos, ConvertToEntity);
        }

        public override List<KundeDto> ConvertToDtos(List<Kunde> entities)
        {
            return ConvertGenericList(entities, ConvertToDto);
        }
    }
}
