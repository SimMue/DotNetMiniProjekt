using System;
using System.Collections.Generic;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Dal.Entities;

namespace AutoReservation.Service.Wcf.Converters
{
    public class AutoConverter : DtoEntityConverter<AutoDto, Auto>
    {
        public override Auto ConvertToEntity(AutoDto dto)
        {
            if (dto == null) { return null; }

            Auto auto = GetAutoInstance(dto);
            auto.Id = dto.Id;
            auto.Marke = dto.Marke;
            auto.Tagestarif = dto.Tagestarif;
            auto.RowVersion = dto.RowVersion;

            if (auto is LuxusklasseAuto)
            {
                ((LuxusklasseAuto)auto).Basistarif = dto.Basistarif;
            }
            return auto;
        }

        public override AutoDto ConvertToDto(Auto entity)
        {
            if (entity == null) { return null; }

            AutoDto dto = new AutoDto
            {
                Id = entity.Id,
                Marke = entity.Marke,
                Tagestarif = entity.Tagestarif,
                RowVersion = entity.RowVersion
            };

            if (entity is StandardAuto) { dto.AutoKlasse = AutoKlasse.Standard; }
            if (entity is MittelklasseAuto) { dto.AutoKlasse = AutoKlasse.Mittelklasse; }
            if (entity is LuxusklasseAuto)
            {
                dto.AutoKlasse = AutoKlasse.Luxusklasse;
                dto.Basistarif = ((LuxusklasseAuto)entity).Basistarif;
            }


            return dto;
        }

        public override List<Auto> ConvertToEntities(List<AutoDto> dtos)
        {
            return ConvertGenericList(dtos, ConvertToEntity);
        }

        public override List<AutoDto> ConvertToDtos(List<Auto> entities)
        {
            return ConvertGenericList(entities, ConvertToDto);
        }

        private Auto GetAutoInstance(AutoDto dto)
        {
            if (dto.AutoKlasse == AutoKlasse.Standard) { return new StandardAuto(); }
            if (dto.AutoKlasse == AutoKlasse.Mittelklasse) { return new MittelklasseAuto(); }
            if (dto.AutoKlasse == AutoKlasse.Luxusklasse) { return new LuxusklasseAuto(); }
            throw new ArgumentException("Unknown AutoDto implementation.", nameof(dto));
        }
    }
}
