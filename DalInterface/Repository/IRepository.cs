using System;
using System.Collections.Generic;
using Rtc.DalInterface.Dto;

namespace Rtc.DalInterface.Repository
{
    public interface IRepository<TDto>
        where TDto : class, IDto
    {
        IEnumerable<TDto> GetAll();

        TDto Get(int id);
        
        IEnumerable<TDto> Get(Predicate<TDto> predicate);

        TDto GetFirstOrDefault(Predicate<TDto> predicate);
        
        void Create(TDto user);

        void Delete(int id);

        void Update(TDto user);

    }
}
