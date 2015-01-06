using Rtc.DalInterface.Dto;

namespace Rtc.Dal.Mappers
{
    public interface IMapper<TDto, TOrm>
        where TDto : class, IDto
        where TOrm : class
    {
        TDto ToDto(TOrm orm);

        TOrm ToOrm(TDto dto);
    }
}