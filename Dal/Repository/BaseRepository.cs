using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Rtc.Dal.Mappers;
using Rtc.DalInterface.Dto;
using Rtc.DalInterface.Repository;

namespace Rtc.Dal.Repository
{
    public abstract class BaseRepository<TDto, TOrm> : IRepository<TDto>
        where TDto : class, IDto
        where TOrm : class
    {
        #region Ctor

        protected readonly DbContext context;
        protected readonly IMapper<TDto, TOrm> mapper;

        protected BaseRepository(DbContext context, IMapper<TDto, TOrm> mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        #endregion

        public virtual IEnumerable<TDto> GetAll()
        {
            return context.Set<TOrm>().AsEnumerable().Select(orm => mapper.ToDto(orm));
        }

        public virtual TDto Get(int id)
        {
            return mapper.ToDto(context.Set<TOrm>().Find(id));
        }

        public virtual IEnumerable<TDto> Get(Predicate<TDto> predicate)
        {
            return context.Set<TOrm>()
                .AsEnumerable()
                .Where(orm => predicate(mapper.ToDto(orm)))
                .Select(orm => mapper.ToDto(orm));
        }

        public virtual TDto GetFirstOrDefault(Predicate<TDto> predicate)
        {
            return mapper.ToDto(context.Set<TOrm>()
                .AsEnumerable()
                .FirstOrDefault(orm => predicate(mapper.ToDto(orm))));
        }

        public virtual void Create(TDto user)
        {
            var orm = mapper.ToOrm(user);
            context.Set<TOrm>().Add(orm);
        }

        public virtual void Delete(int id)
        {
            var orm = context.Set<TOrm>().Find(id);
            context.Set<TOrm>().Remove(orm);
        }

        public virtual void Update(TDto dto)
        {
            var orm = context.Set<TOrm>().Find(dto.Id);
            if (orm != null)
            {
                context.Entry(orm).CurrentValues.SetValues(mapper.ToOrm(dto));
            }
        }
    }
}