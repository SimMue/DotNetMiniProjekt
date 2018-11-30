using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoReservation.Service.Wcf.Converters
{
    public abstract class DtoEntityConverter<TDto, TEntity>
    {
        public abstract TEntity ConvertToEntity(TDto dto);

        public abstract TDto ConvertToDto(TEntity entity);

        public abstract List<TEntity> ConvertToEntities(List<TDto> dtos);

        public abstract List<TDto> ConvertToDtos(List<TEntity> entities);

        protected List<TTarget> ConvertGenericList<TSource, TTarget>(IEnumerable<TSource> source, Func<TSource, TTarget> converter)
        {
            if (source == null) { return null; }
            if (converter == null) { return null; }

            return source.Select(converter).ToList();
        }
    }
}
