using Rtc.Dal.Dao;
using Rtc.DalInterface.Dto;

namespace Rtc.Dal.Mappers
{
    public class RoleMapper : IMapper<RoleDto, Role>
    {
        public RoleDto ToDto(Role role)
        {
            if (role == null)
                return null;
            return new RoleDto
            {
                Id = role.Id,
                Name = role.Name,
            };
        }
        public Role ToOrm(RoleDto role)
        {
            if (role == null)
                return null;
            return new Role
            {
                Id = role.Id,
                Name = role.Name,
            };
        }
    }
}