using Rtc.BllInterface.Entities;
using Rtc.DalInterface.Dto;

namespace Rtc.Bll.Mappers
{
    public static class RoleMapper
    {
        public static RoleEntity ToEntity(this RoleDto role)
        {
            if (role == null)
                return null;
            return new RoleEntity
            {
                Id = role.Id,
                Name = role.Name,
            };
        }

        public static RoleDto ToDto(this RoleEntity role)
        {
            if (role == null)
                return null;
            return new RoleDto
            {
                Id = role.Id,
                Name = role.Name,
            };
        }
    }
}
